using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace FlightSimulatorApp.ViewModel {

    using FlightSimulatorApp.Model;

    class CSViewModel : INotifyPropertyChanged {
       
        private ModelCSV modelCSV;

        public CSViewModel(ModelCSV model) {
            this.modelCSV = model;
        }
        
        public void setFile(string f) {
            modelCSV.fileCSV = f;
            modelCSV.connect();
        }
        
        public event PropertyChangedEventHandler PropertyChanged;
        /*public void NotifyPropertyChanged(string name) {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
                modelCSV.NotifyPropertyChanged(fileCSV);
        }*/

    }
}
