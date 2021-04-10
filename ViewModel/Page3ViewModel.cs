using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.Generic;
using OxyPlot;


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

        //private List<DataPoint> VM_points = new List<DataPoint>();
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
                //Console.WriteLine("the first point X is: " + VM_Regression_points[0].X); 
            }
            get {
                //Console.WriteLine("the first point X is: " + this.modelCSV.Regression_points[0].X);
                return this.modelCSV.Regression_points;
            }
        }

        public List<DataPoint> VM_Regression_line {
            set {
                this.modelCSV.Regression_line = value;
                //Console.WriteLine("the first point X is: " + VM_Regression_points[0].X); 
            }
            get {
                //Console.WriteLine("the first point X is: " + this.modelCSV.Regression_points[0].X);
                return this.modelCSV.Regression_line;
            }
        }

        public List<DataPoint> VM_Regression_30seconds {
            set {
                this.modelCSV.Regression_30seconds = value;
                //Console.WriteLine("the first point X is: " + VM_Regression_points[0].X); 
            }
            get {
                //Console.WriteLine("the first point X is: " + this.modelCSV.Regression_points[0].X);
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

