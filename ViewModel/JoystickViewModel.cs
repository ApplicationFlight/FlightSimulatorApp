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

        public double VM_Throttle {
            get {
                return modelCSV.Throttle;
            }
            set {
                modelCSV.Throttle = value;
            }
        }
        public double VM_Aileron {
            get {
                // return modelCSV.Aileron;
                return (modelCSV.Aileron + 1) * 18;
            }
            set {
                modelCSV.Aileron = value;
            }
        }
        public double VM_Elevator {
            get {
                return 18 * (1 - modelCSV.Elevator);
            }
            set {
                modelCSV.Elevator = value;
            }
        }
        public double VM_Rudder {
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
