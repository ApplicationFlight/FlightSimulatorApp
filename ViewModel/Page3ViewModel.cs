using OxyPlot;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;


namespace FlightSimulatorApp.ViewModel {
    using FlightSimulatorApp.Model;

    class Page3ViewModel : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;
        public IModelCSV modelCSV;

        public Page3ViewModel(ModelCSV model) {
            this.modelCSV = model;
            this.modelCSV.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e) {
                NotifyPropertyChanged("VM_" + e.PropertyName);
            };
        }

        public List<DataMember> VM_Data_members {
            get {
                return modelCSV.Data_members.Values.ToList();
            }
        }

        public List<DataPoint> VM_Points {
            get {
                return this.modelCSV.Points;
            }
            set {
                this.modelCSV.Points = value;
            }
        }

        public List<DataPoint> VM_Correlative_points {
            set {
                this.modelCSV.Correlative_points = value;
            }
            get {
                return this.modelCSV.Correlative_points;
            }
        }


        public List<DataPoint> VM_Regression_points {
            set {
                this.modelCSV.Regression_points = value;
            }
            get {
                return this.modelCSV.Regression_points;
            }
        }

        public List<DataPoint> VM_Regression_line {
            set {
                this.modelCSV.Regression_line = value;
            }
            get {
                return this.modelCSV.Regression_line;
            }
        }

        public List<DataPoint> VM_Regression_30seconds {
            set {
                this.modelCSV.Regression_30seconds = value;
            }
            get {
                return this.modelCSV.Regression_30seconds;
            }
        }


        public void NotifyPropertyChanged(string name) {
            if (this.PropertyChanged != null) {
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}

