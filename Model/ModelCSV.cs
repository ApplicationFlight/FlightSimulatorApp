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

    public class ModelCSV : IModelCSV {


        // fields
        ItelnetClient telnetClient;
        public event PropertyChangedEventHandler PropertyChanged;
        private TimeSeries timeseries;
        
        public TimeSeries Timeseries {
            set {
                timeseries = value;
            }
            get {
                return timeseries;
            }
        }


        private float percentage = 0; 
        public float Percentage { 
            get {
                return percentage; 
            }
            set {
                this.percentage = value;
                this.Relative_time = get_index_from_percentage(this.percentage);
                NotifyPropertyChanged("Percentage");
            } 
        }

        private int speed = 100;
        public int Speed {
            get {
                return speed;
            }
            set {
                this.speed = value; 
            }
        }

        private bool is_running = true; 
        public bool Is_running {
            get {
                return is_running;
            }
            set {
                this.is_running = value;
            }
        }

        private int relative_time;
        public int Relative_time {
            get {
                return relative_time;
            }
            set {
                this.relative_time = value;
                NotifyPropertyChanged("Relative_time");
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

        public void start() {
            // TODO: maybe the getstream could be included with the connect of client? 
            new Thread(delegate () {
                StreamWriter output = new StreamWriter(this.telnetClient.Client.GetStream());
                while (get_index_from_percentage(this.Percentage) < this.timeseries.n_lines) {
                    if (this.is_running) {
                        Console.WriteLine("INDEX: " + get_index_from_percentage(this.Percentage) + "\n");
                        Console.WriteLine("SPEED: " + this.speed + "\n");
                        Console.WriteLine(this.timeseries.simple_data[get_index_from_percentage(this.Percentage)]);
                        output.WriteLine(this.timeseries.simple_data[get_index_from_percentage(this.Percentage)]);
                        this.Percentage = this.Percentage + (100 / (float)this.timeseries.n_lines);
                        Thread.Sleep(this.Speed);
                    }
                }
                output.Close();
            }).Start();
        }

        public void NotifyPropertyChanged(string name) {
            if (this.PropertyChanged != null) {
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}