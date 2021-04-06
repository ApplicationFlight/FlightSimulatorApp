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

<<<<<<< HEAD
        
=======
        public void NotifyPropertyChanged(string name) {
            if (this.PropertyChanged != null) {
                //Console.WriteLine("(NOTIFY) data was changed!!!!!!!!!!!!!!!!!!!!!!\n");
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));

            }
        }
>>>>>>> 412fea775eb6bff312a4370332fc532dffbaa47c

        public List<DataMember> VM_Data_members {
            get {
                return modelCSV.Data_members;
            }
            set {
                modelCSV.Data_members = value;

            }
        }

        //private List<DataPoint> VM_points = new List<DataPoint>();
        public List<DataPoint> VM_Points {
            get {
                return this.modelCSV.Points;
            }
            set {
<<<<<<< HEAD
=======
                //Console.WriteLine("WE CHANGED POINTS IN VIEWMODEL");
>>>>>>> 412fea775eb6bff312a4370332fc532dffbaa47c
                this.modelCSV.Points = value; 
            }
        }

<<<<<<< HEAD
        public void NotifyPropertyChanged(string name) {
            if (this.PropertyChanged != null) {
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

=======
>>>>>>> 412fea775eb6bff312a4370332fc532dffbaa47c
    }
}

