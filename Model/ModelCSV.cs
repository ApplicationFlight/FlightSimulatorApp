using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;


namespace FlightSimulatorApp.Model {

    using FlightSimulatorApp.ViewModel;
    using System.Collections.Generic;
    using OxyPlot;

    public class ModelCSV : IModelCSV {


        // fields
        ItelnetClient telnetClient;
        public event PropertyChangedEventHandler PropertyChanged;
        private ModelDataBase anomaly_flight;
        

        private double percentage = 0; 
        public double Percentage { 
            get {return percentage; }
            set {
                this.percentage = value;
                this.Relative_time = get_index_from_percentage(this.percentage);
                NotifyPropertyChanged("Percentage");
            } 
        }

        private int speed = 100;
        public int Speed {
            get {return speed;}
            set {this.speed = value; }
        }

        private bool is_running = true; 
        public bool Is_running {
            get {return is_running;}
            set {this.is_running = value;}
        }

        private int relative_time;
        public int Relative_time {
            get {return relative_time;}
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
            get {return this.throttle;}
        }
        private double aileron;

        public double Aileron { 
            set {
                this.aileron = value;
                NotifyPropertyChanged("Aileron");
            } 
            get { return this.aileron;}
        }
        private double elevator { set; get; }
        public double Elevator { 
            set {
                this.elevator = value;
                NotifyPropertyChanged("Elevator");
            }
            get {
                return this.elevator;
            }
        }

        private double rudder;
        public double Rudder {
            set {
                this.rudder = value;
                NotifyPropertyChanged("Rudder");
            }
            get {return this.rudder;}
        }

        private Dictionary<string, DataMember> data_members;
        public Dictionary<string, DataMember> Data_members {
            set {
                this.data_members = value;
                NotifyPropertyChanged("Data_members");
            }
            get {
                return this.data_members;
            }
        }

        private DataMember data_member;
        public DataMember Data_member {
            set {
                this.data_member = value;
               
            }
            get {
                return this.data_member;
            }
        }


        private List<DataPoint> points = new List<DataPoint>();
        public List<DataPoint> Points {
            get {
                return this.points;
            }
            set {
                this.points = value;
                NotifyPropertyChanged("Points");           
            }
        }

        private List<DataPoint> correlative_points = new List<DataPoint>();
        public List<DataPoint> Correlative_points {
            get {
                return this.correlative_points;
            }
            set {
                this.correlative_points = value;
                NotifyPropertyChanged("Correlative_points");
            }
        }

        private List<DataPoint> regression_points = new List<DataPoint>();
        public List<DataPoint> Regression_points {
            get {
                return this.regression_points;
            }
            set {
                this.regression_points = value;
                NotifyPropertyChanged("Regression_points");
            }
        }

        private List<DataPoint> regression_line = new List<DataPoint>();
        public List<DataPoint> Regression_line {
            get {
                return this.regression_line;
            }
            set {
                this.regression_line = value;
                NotifyPropertyChanged("Regression_line");
            }
        }


        // methods
        public ModelCSV() {
            this.telnetClient = new TelnetClient();
        }

        public void initialize_model(string file_path) {
            this.anomaly_flight = new ModelDataBase(file_path);
            this.Data_members = this.anomaly_flight.data_members;
            connect();
            start();
            // TODO: think about where disconnect
        }

        public void connect() {
            this.telnetClient.connect();
        }

        public void disconnet() {
            this.telnetClient.disconnect();
        }

        int get_index_from_percentage(double percetnage) {
            return (int)(this.anomaly_flight.n_lines * this.Percentage) / 100;
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

        void update_data_members(int j) {
            int index = 0; 
            for (int i =0; i<this.Data_members.Count(); i++ ) {
                string feature1 = Data_members.ElementAt(i).Key; 
                Data_members.ElementAt(i).Value.Points.Add(new DataPoint((double)j/10, anomaly_flight.data_map[feature1][j]));
                if (this.Data_member != null) {
                    if (feature1.Equals(this.Data_member.Name)) {
                        index = i; 
                    }
                }
            }
            // updating all graph 
            this.Points = Data_members.ElementAt(index).Value.Points;
            this.Correlative_points = Data_members[Data_members.ElementAt(index).Value.Correlative_string].Points;
            this.Regression_points = Data_members.ElementAt(index).Value.Regression_points;
            this.Regression_line = Data_members.ElementAt(index).Value.Regression_line;
        }


        public void start() {
            // TODO: maybe the getstream could be included with the connect of client? 
            new Thread(delegate () {
                StreamWriter output = new StreamWriter(this.telnetClient.Client.GetStream());
                while (get_index_from_percentage(this.Percentage) < this.anomaly_flight.n_lines) {
                    if (this.is_running) {
                        int i = get_index_from_percentage(this.Percentage);
                        //Console.WriteLine("INDEX: " + i + "\n");
                        //Console.WriteLine("SPEED: " + this.speed + "\n");
                        //Console.WriteLine(this.timeseries.simple_data[i]);
                        output.WriteLine(this.anomaly_flight.simple_data[i]);
                        this.Percentage = this.Percentage + (100 / (double)this.anomaly_flight.n_lines);
                        update_selected_data(i);
                        update_joystick_value(i);
                        update_data_members(i); 
                        Thread.Sleep(this.Speed);
                    }
                }
                output.Close();
            }).Start();
        }

        public void NotifyPropertyChanged(string name) {
            if (this.PropertyChanged != null) {
                //Console.WriteLine("HERE IN PROPERTY CHANGED FOR "+ name+"\n");
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }   
    }
}