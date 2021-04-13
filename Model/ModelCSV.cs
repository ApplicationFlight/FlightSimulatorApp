using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading;

namespace FlightSimulatorApp.Model {

    using FlightSimulatorApp.Model.AnomalyDetector;
    using OxyPlot;
    using System.Collections.Generic;
    using System.Reflection;

    public class ModelCSV : IModelCSV {

        public event PropertyChangedEventHandler PropertyChanged;
        ItelnetClient telnetClient;
        String anomaly_flight_path;
        public ModelDataBase anomaly_flight;

        StreamWriter output;
        Boolean is_connected;

        private double percentage = 0;
        public double Percentage {
            get { return percentage; }
            set {
                this.percentage = value;
                this.Relative_time = get_index_from_percentage();
                NotifyPropertyChanged("Percentage");
            }
        }

        private int speed = 100;
        public int Speed {
            get { return speed; }
            set { this.speed = value; }
        }

        private bool is_running = true;
        public bool Is_running {
            get { return is_running; }
            set { this.is_running = value; }
        }

        private int relative_time;
        public int Relative_time {
            get { return relative_time; }
            set {
                this.relative_time = value;
                NotifyPropertyChanged("Relative_time");
            }
        }

        private double[] selected_data = new double[6];
        public double[] Selected_data {
            get { return this.selected_data; }
            set {
                this.selected_data = value;
                NotifyPropertyChanged("Selected_data");
            }
        }

        private double throttle;
        public double Throttle {
            set {
                this.throttle = value;
                NotifyPropertyChanged("Throttle");
            }
            get { return this.throttle; }
        }

        private double aileron;
        public double Aileron {
            set {
                this.aileron = value;
                NotifyPropertyChanged("Aileron");
            }
            get { return this.aileron; }
        }

        private double elevator { set; get; }
        public double Elevator {
            set {
                this.elevator = value;
                NotifyPropertyChanged("Elevator");
            }
            get { return this.elevator; }
        }

        private double rudder;
        public double Rudder {
            set {
                this.rudder = value;
                NotifyPropertyChanged("Rudder");
            }
            get { return this.rudder; }
        }

        private Dictionary<string, DataMember> data_members;
        public Dictionary<string, DataMember> Data_members {
            set {
                this.data_members = value;
                NotifyPropertyChanged("Data_members");
            }
            get { return this.data_members; }
        }

        private DataMember data_member;
        public DataMember Data_member {
            set { this.data_member = value; }
            get { return this.data_member; }
        }

        private List<DataPoint> points = new List<DataPoint>();
        public List<DataPoint> Points {
            get { return this.points; }
            set {
                this.points = value;
                NotifyPropertyChanged("Points");
            }
        }

        private List<DataPoint> correlative_points = new List<DataPoint>();
        public List<DataPoint> Correlative_points {
            get { return this.correlative_points; }
            set {
                this.correlative_points = value;
                NotifyPropertyChanged("Correlative_points");
            }
        }

        private List<DataPoint> regression_points = new List<DataPoint>();
        public List<DataPoint> Regression_points {
            get { return this.regression_points; }
            set {
                this.regression_points = value;
                NotifyPropertyChanged("Regression_points");
            }
        }

        private List<DataPoint> regression_line = new List<DataPoint>();
        public List<DataPoint> Regression_line {
            get { return this.regression_line; }
            set {
                this.regression_line = value;
                NotifyPropertyChanged("Regression_line");
            }
        }

        private List<DataPoint> regression_30seconds = new List<DataPoint>();
        public List<DataPoint> Regression_30seconds {
            get { return this.regression_30seconds; }
            set {
                this.regression_30seconds = value;
                NotifyPropertyChanged("Regression_30seconds");
            }
        }

        private List<AnomalyReport> anomaly_reports = new List<AnomalyReport>();
        public List<AnomalyReport> Anomaly_reports {
            get {
                return this.anomaly_reports;
            }
            set {
                this.anomaly_reports = value;
                NotifyPropertyChanged("Anomaly_reports");
            }
        }



        public ModelCSV() {
            this.telnetClient = new TelnetClient();
        }

        public void initialize_model(string file_path) {
            this.anomaly_flight_path = file_path;
            this.anomaly_flight = new ModelDataBase(file_path);
            this.Data_members = this.anomaly_flight.data_members;
            connect();
            start();
        }

        public void connect() {
            this.telnetClient.connect();
            this.output = new StreamWriter(this.telnetClient.Client.GetStream());
            this.is_connected = true;

        }

        public void disconnet() {
            this.is_connected = false; 
            this.output.Close();
            this.telnetClient.disconnect();
        }

        int get_index_from_percentage() {
            return (int)((this.anomaly_flight.n_lines - 1) * this.Percentage) / 100;
        }

        void update_selected_data(int j) {
            double[] result = new double[6];
            for (int i = 0; i < anomaly_flight.selected_data.Count(); i++) {
                result[i] = anomaly_flight.selected_data[i].Item2[j];
            }
            this.Selected_data = result;
        }

        void update_joystick_value(int i) {
            this.Aileron = this.anomaly_flight.data_map["aileron"][i];
            this.Throttle = this.anomaly_flight.data_map["throttle"][i];
            this.Rudder = this.anomaly_flight.data_map["rudder"][i];
            this.Elevator = this.anomaly_flight.data_map["elevator"][i];
        }


        List<DataPoint> get_point_30_seconds(int j) {
            int lines = 30 * (1000 / this.Speed);
            List<DataPoint> result = new List<DataPoint>();
            for (int i = j; i >= 0 && i >= (j - lines); i--) {
                result.Add(this.Regression_points[i]);
            }
            return result;
        }


        void update_data_members(int j) {
            int index = 0;
            for (int i = 0; i < this.Data_members.Count(); i++) {
                string feature1 = Data_members.ElementAt(i).Key;
                Data_members.ElementAt(i).Value.Points.Add(new DataPoint((double)j / 10, anomaly_flight.data_map[feature1][j]));
                if (this.Data_member != null) {
                    if (feature1.Equals(this.Data_member.Name)) {
                        index = i;
                    }
                }
            }
            // updating all graphs 
            this.Points = Data_members.ElementAt(index).Value.Points;
            this.Correlative_points = Data_members[Data_members.ElementAt(index).Value.Correlative_string].Points;
            this.Regression_points = Data_members.ElementAt(index).Value.Regression_points;
            this.Regression_line = Data_members.ElementAt(index).Value.Regression_line;
            this.Regression_30seconds = get_point_30_seconds(j);
        }


        public void Add_Algorithm(string path) {
            string[] first = path.Split('\\');
            string last = first[first.Count() - 1];
            string final = last.Substring(0, last.Count() - 4);
            string class_name = final + '.' + final;
            string function_name = final.Substring(4).ToLower();          
            string p1 = "..\\..\\..\\Resources\\Documents\\reg_flight.csv";
            string p2 = this.anomaly_flight_path;
            string p3 = "..\\..\\..\\Resources\\Documents\\anomaly_reports.csv";
            var DLL = Assembly.LoadFile(path);
            var runner_dll = DLL.GetType(class_name);
            var c = Activator.CreateInstance(runner_dll);
            var method = runner_dll.GetMethod(function_name);
            method.Invoke(c, new object[] { p1, p2, p3 });



            List<AnomalyReport> result = new List<AnomalyReport>();
            StreamReader input = new StreamReader(p3);
            String line;
            while ((line = input.ReadLine()) != null) {
                string[] elements = line.Split(',');
                string feature1 = elements[0];
                string feature2 = elements[1];
                int n = int.Parse(elements[2]);
                string description = elements[3];
                result.Add(new AnomalyReport(feature1, feature2, n, description));
            }
            input.Close();
            this.Anomaly_reports = result;
        }

        public void start() {
            new Thread(delegate () {
                while (is_connected) {
                    if (this.is_running) {
                        int i = get_index_from_percentage();
                        output.WriteLine(this.anomaly_flight.simple_data[i]);
                        update_selected_data(i);
                        update_joystick_value(i);
                        update_data_members(i);
                        if (this.Percentage + 100 / (double)this.anomaly_flight.n_lines <= 100) {
                            this.Percentage = this.Percentage + (100 / (double)this.anomaly_flight.n_lines);
                        }
                        Thread.Sleep(this.Speed);
                    }
                }
            }).Start();
        }

        public void NotifyPropertyChanged(string name) {
            if (this.PropertyChanged != null) {
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}