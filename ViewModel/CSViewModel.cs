using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace FlightSimulatorApp.ViewModel {

    using FlightSimulatorApp.Model;

    public class CSViewModel : INotifyPropertyChanged {

        private IModelCSV modelCSV;
        private TimeSeries timeseries;
        public event PropertyChangedEventHandler PropertyChanged; 

        public CSViewModel(IModelCSV modelCSV) {
            this.modelCSV = modelCSV;
            this.modelCSV.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e) {
                NotifyPropertyChanged("VM_" + e.PropertyName);
            };
        }

        public float VM_Percentage {
            get {
                return modelCSV.Percentage;
            }
            set {
                modelCSV.Percentage = value;
            }
        }

        public bool VM_Is_running {
            get {
                return modelCSV.Is_running;
            }
            set {
                modelCSV.Is_running = value;
            }
        }
        public int VM_Speed {
            get { 
                return modelCSV.Speed; 
            }
            set {
                modelCSV.Speed = value;
            }
        }

        /*private string VM_overall_time;
        public string VM_Overall_time {
            get {
                return VM_overall_time;
            }
        }

        private string VM_relative_time;
        public string VM_Relative_time {
            get {
                return VM_relative_time;
            }
        }*/

        
        
        public void NotifyPropertyChanged(string name) {
            if (this.PropertyChanged != null) {
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public void setFile(string file_path) {
            this.timeseries = new TimeSeries(file_path);
            this.modelCSV.Timeseries = this.timeseries;
            //VM_overall_time = (this.timeseries.n_lines * 100).ToString();
            this.modelCSV.connect();
            this.modelCSV.start();
            // TODO: think about where disconnect
        }
    }
}
