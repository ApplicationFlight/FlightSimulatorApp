using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OxyPlot;
using System.ComponentModel;


namespace FlightSimulatorApp.Model.AnomalyDetector {
    public class Line {
        DataPoint point1;
        DataPoint point2;

        double a, b; 

        public Line(DataPoint point1, DataPoint point2) {
            this.point1 = point1;
            this.point2 = point2; 
        }

        public Line(double a, double b) {
            this.a = a;
            this.b = b; 
        }

        public double f(double x) {
            return a * x + b; 
        }
    }
}
