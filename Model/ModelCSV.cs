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
        private TimeSeries timeseries;
        public TimeSeries Timeseries {
            set {timeseries = value;}
            get {return timeseries;}
        }

        private float percentage = 0; 
        public float Percentage { 
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

        private float[] selected_data = new float[6];
        public float[] Selected_data {
            get { return this.selected_data; }
            set {
                this.selected_data = value;
                NotifyPropertyChanged("Selected_data");
            }
        }

        private float throttle;
        public float Throttle { 
            set {
                this.throttle = value;
                NotifyPropertyChanged("Throttle");
            }
            get {return this.throttle;}
        }
        private float aileron;

        public float Aileron { 
            set {
                this.aileron = value;
                NotifyPropertyChanged("Aileron");
            } 
            get { return this.aileron;}
        }
        private float elevator { set; get; }
        public float Elevator { 
            set {
                this.elevator = value;
                NotifyPropertyChanged("Elevator");
            }
            get {
                return this.elevator;
            }
        }

        private float rudder;
        public float Rudder {
            set {
                this.rudder = value;
                NotifyPropertyChanged("Rudder");
            }
            get {return this.rudder;}
        }

      

        private List<DataMember> data_members;
        public List<DataMember> Data_members {
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


        // methods
        public ModelCSV() {
            this.telnetClient = new TelnetClient();
        }

        public void connect() {
            this.telnetClient.connect();
        }

        public void disconnet() {
            this.telnetClient.disconnect();
        }

        int get_index_from_percentage(float percetnage) {
            return (int)(this.timeseries.n_lines * this.Percentage) / 100;
        }

        void update_selected_data(int j) {
            float[] result = new float[6];
            for (int i = 0; i < timeseries.selected_data.Count(); i++) {
                result[i] = timeseries.selected_data[i].Item2[j];
            }
            this.Selected_data = result; 
        }

        void update_joystick_value(int i) {
            this.Aileron = this.timeseries.data_map["aileron"][i];
            this.Throttle = this.timeseries.data_map["throttle"][i];
            this.Rudder = this.timeseries.data_map["rudder"][i];
            this.Elevator = this.timeseries.data_map["elevator"][i];
        }

        void update_data_members(int j) {
            int index = 0; 
            for (int i =0; i<this.Data_members.Count(); i++ ) {
                Data_members[i].Points.Add(new DataPoint((double)j/10, timeseries.data_map[Data_members[i].Name][j]));
                if (this.Data_member != null) {
                    if (Data_members[i].Name.Equals(this.Data_member.Name)) {
                        index = i; 
                    }
                }
            }
            this.Points = Data_members[index].Points; 
        }


        public void start() {
            // TODO: maybe the getstream could be included with the connect of client? 
            new Thread(delegate () {
                StreamWriter output = new StreamWriter(this.telnetClient.Client.GetStream());
                while (get_index_from_percentage(this.Percentage) < this.timeseries.n_lines) {
                    if (this.is_running) {
                        int i = get_index_from_percentage(this.Percentage);
                        //Console.WriteLine("INDEX: " + i + "\n");
                        //Console.WriteLine("SPEED: " + this.speed + "\n");
                        //Console.WriteLine(this.timeseries.simple_data[i]);
                        output.WriteLine(this.timeseries.simple_data[i]);
                        this.Percentage = this.Percentage + (100 / (float)this.timeseries.n_lines);
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

        public void setFile(string file_path) {
            this.timeseries = new TimeSeries(file_path);
            this.Data_members = this.timeseries.data_members;

            connect();
            start();
            // TODO: think about where disconnect
        }
    }
}