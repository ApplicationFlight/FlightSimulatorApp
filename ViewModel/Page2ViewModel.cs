using FlightSimulatorApp.Model;
using FlightSimulatorApp.Model.AnomalyDetector;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace FlightSimulatorApp.ViewModel {
    public class Page2ViewModel : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;
        public ModelCSV modelCSV;

        public Page2ViewModel(ModelCSV model) {
            this.modelCSV = model;
            this.modelCSV.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e) {
                NotifyPropertyChanged("VM_" + e.PropertyName);
            };
        }

        public List<AnomalyReport> VM_Anomaly_reports {
            set {
                this.modelCSV.Anomaly_reports = value;
            }
            get {
                return this.modelCSV.Anomaly_reports;
            }
        }

        public void Add_Algorithm(string path) {
            this.modelCSV.Add_Algorithm(path);
        }


        public void NotifyPropertyChanged(string name) {
            if (this.PropertyChanged != null) {
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
