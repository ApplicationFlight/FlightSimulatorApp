using OxyPlot;
using System;
using System.Collections.Generic;


namespace FlightSimulatorApp.Model {
    
    public class DataMember {

        // data string name
        private String name;
        public String Name {
            set {
                this.name = value;
            }
            get {
                return this.name;
            }
        }

        // list of points between values and time (for the graph)
        private List<DataPoint> points;
        public List<DataPoint> Points {
            set {
                this.points = value;

            }
            get {
                return this.points;
            }
        }

        // most correlative data member string name
        private string correlative_string;
        public string Correlative_string {
            set {
                this.correlative_string = value;
            }
            get {
                return this.correlative_string;
            }
        }

        // list of points between the data and its most correllated feature
        private List<DataPoint> regression_points;
        public List<DataPoint> Regression_points {
            set {
                this.regression_points = value;
            }
            get {
                return this.regression_points;
            }
        }

        // a list of points representing the regression line
        private List<DataPoint> regression_line;
        public List<DataPoint> Regression_line {
            set {
                this.regression_line = value;
            }
            get {
                return this.regression_line;
            }
        }

        public override string ToString() {
            return this.Name;
        }
    }
}
