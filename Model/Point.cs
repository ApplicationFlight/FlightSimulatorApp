using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorApp.Model {
    public class Point {
        // fields 
        private Double x;
        
        public Double X {
            set {
                this.x = value;
            }
            get {
                return this.x;
            }
        }
        private Double y;
        public Double Y {
            set {
                this.y = value;
            }
            get {
                return this.y;
            }
        }

        public Point(double x, double y){
            this.X = x;    
            this.Y = y; 
        }
    }
}
