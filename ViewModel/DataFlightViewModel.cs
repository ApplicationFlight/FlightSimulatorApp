using System;
using System.ComponentModel;


namespace FlightSimulatorApp.ViewModel {

    using FlightSimulatorApp.Model;
    public class DataFlightViewModel : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;
        private IModelCSV modelCSV;

        public DataFlightViewModel(ModelCSV model) {
            this.modelCSV = model;
            this.modelCSV.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e) {
                NotifyPropertyChanged("VM_" + e.PropertyName);
            };
        }

        public void NotifyPropertyChanged(string name) {
            if (this.PropertyChanged != null) {
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));

            }
        }

        public double[] VM_Selected_data {
            get {
                return modelCSV.Selected_data;
            }
            set {
                modelCSV.Selected_data = value;
            }
        }
    }
}
