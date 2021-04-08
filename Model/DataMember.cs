using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using OxyPlot;
using System.ComponentModel;


namespace FlightSimulatorApp.Model {
    public class DataMember : INotifyPropertyChanged {
        
        public event PropertyChangedEventHandler PropertyChanged;

        // the name of the data
        private String name; 
        public String Name {
            set {
                this.name = value;
            }
            get {
                return this.name;
            }
        }

        // the list of points between values and time (for the graph)
        private List<DataPoint> points;
        public List<DataPoint> Points {
            set {
                this.points = value;
                
            }
            get {
                return this.points;
            }
        }

        // the most correlative data member
        private string correlative;


        public string Correlative {
            set {
                this.correlative = value;
            }
            get {
                return this.correlative;
            }
        }

        public override string ToString() {
            return this.Name;
        }

        public void NotifyPropertyChanged(string name) {
            if (this.PropertyChanged != null) {
                Console.WriteLine("We changed the Point!");
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
