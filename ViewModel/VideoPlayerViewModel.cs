using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace FlightSimulatorApp.ViewModel {

    using FlightSimulatorApp.Model;
    public class VideoPlayerViewModel : INotifyPropertyChanged {

        private IModelCSV modelCSV;
        public event PropertyChangedEventHandler PropertyChanged; 

        public VideoPlayerViewModel(ModelCSV model) {
            this.modelCSV = model;
            this.modelCSV.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e) {
                NotifyPropertyChanged("VM_" + e.PropertyName);
            };
            //VM_overall_time = get_time_from_seconds(model.Timeseries.n_lines / 10);
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

        private string VM_overall_time;
        public string VM_Overall_time {
            get {
                return VM_overall_time;
            }
        }

        public string VM_Relative_time {
            get {
                return get_time_from_seconds(modelCSV.Relative_time/10);
            }
        }
        
        public void NotifyPropertyChanged(string name) {
            if (this.PropertyChanged != null) {
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        string get_time_from_seconds(int seconds) {
            TimeSpan t = TimeSpan.FromSeconds(seconds);
            return string.Format("{0:D2}:{1:D2}:{2:D2}s",t.Hours,t.Minutes,t.Seconds,t.Milliseconds);
        }
    }
}
