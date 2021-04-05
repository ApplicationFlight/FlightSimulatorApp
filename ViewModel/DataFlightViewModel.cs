using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                //Console.WriteLine("(NOTIFY) data was changed!!!!!!!!!!!!!!!!!!!!!!\n");
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));

            }
        }

        //public float[] check = { 1, 2, 3, 4, 5, 6 };

        public float[] VM_Selected_data {
            get {
                return modelCSV.Selected_data;
            }
            set {
                //Console.WriteLine("data was changed!!!!!!!!!!!!!!!!!!!!!!\n");
                modelCSV.Selected_data = value;
            }
        }
    }
}
