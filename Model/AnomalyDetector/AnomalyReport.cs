using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorApp.Model.AnomalyDetector {
    public class AnomalyReport {
        string feature1;
        string feature2;
        int line;
        string description; 

        public AnomalyReport(string feature1, string feature2, int line, string description) {
            this.feature1 = feature1;
            this.feature2 = feature2;
            this.line = line;
            this.description = description;
        }

        public override string ToString() {
            return feature1+" "+feature2+" | Time: "+get_time_from_seconds(line/10)+"| Description: "+this.description;
        }

        string get_time_from_seconds(int seconds) {
            TimeSpan t = TimeSpan.FromSeconds(seconds);
            return string.Format("{0:D2}:{1:D2}:{2:D2}s", t.Hours, t.Minutes, t.Seconds, t.Milliseconds);
        }
    }
}
