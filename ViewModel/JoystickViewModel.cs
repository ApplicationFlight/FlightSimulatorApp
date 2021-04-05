using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace FlightSimulatorApp.ViewModel {

    using FlightSimulatorApp.Model;
    public class JoystickViewModel : INotifyPropertyChanged {

        private IModelCSV modelCSV;
        public event PropertyChangedEventHandler PropertyChanged;

        public JoystickViewModel(ModelCSV model) {
            this.modelCSV = model;
            this.modelCSV.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e) {
                NotifyPropertyChanged("VM_" + e.PropertyName);
            };
        }

        public float VM_Throttle {
            get {
                return modelCSV.Throttle;
            }
            set {
                modelCSV.Throttle = value;
            }
        }
        public float VM_Aileron {
            get {
                return modelCSV.Aileron;
            }
            set {
                modelCSV.Aileron = value;
            }
        }
        public float VM_Elevator {
            get {
                return modelCSV.Elevator;
            }
            set {
                modelCSV.Elevator = value;
            }
        }

        public float VM_Rudder {
            get {
                return modelCSV.Rudder;
            }
            set {
                modelCSV.Rudder = value;
            }
        }

        public void NotifyPropertyChanged(string name) {
            if (this.PropertyChanged != null) {
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
