﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using FlightSimulatorApp.Model;
using FlightSimulatorApp.Model.AnomalyDetector;

namespace FlightSimulatorApp.ViewModel {
    public class Page2ViewModel : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;
        public IModelCSV modelCSV;

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

        public void Add_Algorithm() {
            this.modelCSV.Add_Algorithm(); 
        }


        public void NotifyPropertyChanged(string name) {
            if (this.PropertyChanged != null) {
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
