using FlightSimulatorApp.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FlightSimulatorApp.Model.AnomalyDetector {

	public struct correlatedFeatures {
		public string feature1, feature2;  // names of the correlated features
		public double corrlation;
		public Line line_reg;
		public double threshold;
		//double cx, cy;  //TODO: check for circle what to add
	};

	public class SimpleAnomalyDetector {
		public List<correlatedFeatures> cf;
		// simple fc is a dcitionary in which each string is mapped to the most correlative other string
		// not necesseraly checked with algorithm, only based on pearson
		public Dictionary<string, string> simple_cf; 
		double threshold; 


		public SimpleAnomalyDetector() {
			this.threshold = 0.9;
			cf = new List<correlatedFeatures>();
			simple_cf = new Dictionary<string, string>(); 
        }

		public void learnNormal(TimeSeries ts) {
			StreamWriter output = new StreamWriter("correlations.txt");
			for (int i = 0; i < ts.n_colums; i++) {
				string f1 = ts.whole_data[i].Item1;
				List<double> x = ts.whole_data[i].Item2;
				double max_correlation = 0;
				int max_index = 0;
				for (int j = i + 1; j < ts.n_colums; j++) {
					string feature2 = ts.whole_data[j].Item1; 
					double correlation = Math.Abs(AnomalyDetectionUtil.pearson(x, ts.whole_data[j].Item2, ts.n_lines));
					if (correlation > max_correlation) {
						max_correlation = correlation;
						max_index = j;
					}
				}
				string f2 = ts.whole_data[max_index].Item1;
				// always add to fc:
				if (!simple_cf.ContainsKey(f1)) {
					simple_cf.Add(f1, f2);
				}
				output.WriteLine(f1 + ','+f2 + " CORR: " + Math.Abs(AnomalyDetectionUtil.pearson(x, ts.whole_data[max_index].Item2, ts.n_lines))); 

				// NOW IN TERMS OF CORRELATIVE FEATURES, based on algorithm!
				// regression case: 
				correlatedFeatures cf_found;
				List<double> y = ts.whole_data[max_index].Item2;
				if (max_correlation > threshold) {
					cf_found = createCF(f1, f2, max_correlation, x,y, ts.n_lines);
					cf.Add(cf_found);
				}

				// circle case: 
			}
		}

		correlatedFeatures createCF(string feature1, string feature2, double correlation, List<double> x, List<double> y, int size) {
			Line line_reg = AnomalyDetectionUtil.linear_reg(x, y, size);
			double threshold = AnomalyDetectionUtil.max_dev(x, y, line_reg, size);
			correlatedFeatures cf_found = new correlatedFeatures {
				feature1 = feature1,
				feature2 = feature2,
				corrlation = correlation,
				line_reg = line_reg,
				threshold = threshold
			};
			return cf_found; 
        }
    }
}
