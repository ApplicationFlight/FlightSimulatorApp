using System;
using System.Collections.Generic;
using System.IO;


namespace FlightSimulatorApp.Model.AnomalyDetector {
    
    public struct correlatedFeatures {
        public string feature1, feature2;  
        public double corrlation;
        public Line line_reg;
        public double threshold;
    };

    // this class is used ONLY to detect maximum correlated features and show the graph
    // the detction is up to the DLL
    public class SimpleAnomalyDetector {
        public Dictionary<string, string> simple_cf;
        double threshold;


        public SimpleAnomalyDetector() {
            this.threshold = 0.9;
            // maps each feature string to its most correlated feature string
            simple_cf = new Dictionary<string, string>();
        }

        public void learnNormal(TimeSeries ts) {
            for (int i = 0; i < ts.n_colums; i++) {
                string f1 = ts.whole_data[i].Item1;
                List<double> x = ts.whole_data[i].Item2;
                double max_correlation = 0;
                int max_index = 0;
                for (int j = i + 1; j < ts.n_colums; j++) {
                    double correlation = Math.Abs(AnomalyDetectionUtil.pearson(x, ts.whole_data[j].Item2, ts.n_lines));
                    if (correlation > max_correlation) {
                        max_correlation = correlation;
                        max_index = j;
                    }
                }
                string f2 = ts.whole_data[max_index].Item1;
                if (!simple_cf.ContainsKey(f1)) {
                    simple_cf.Add(f1, f2);
                }
            }
        }
    }
}
