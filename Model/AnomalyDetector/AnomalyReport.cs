using System;

namespace FlightSimulatorApp.Model.AnomalyDetector {
    // this class defines an anomaly report
    public class AnomalyReport {
        string feature1;
        public string Feature1 {
            set {
                this.feature1 = value;
            }
            get {
                return this.feature1;
            }
        }

        string feature2;
        public string Feature2 {
            set {
                this.feature2 = value;
            }
            get {
                return this.feature2;
            }
        }


        public int line;
        public string Time {
            get {
                return get_time_from_seconds(this.line / 10);
            }
        }


        string description;
        public string Description {
            set {
                this.description = value;
            }
            get {
                return this.description;
            }
        }

        public AnomalyReport(string feature1, string feature2, int line, string description) {
            this.feature1 = feature1;
            this.feature2 = feature2;
            this.line = line;
            this.description = description;
        }

        string get_time_from_seconds(int seconds) {
            TimeSpan t = TimeSpan.FromSeconds(seconds);
            return string.Format("{0:D2}:{1:D2}:{2:D2}s", t.Hours, t.Minutes, t.Seconds, t.Milliseconds);
        }
    }
}
