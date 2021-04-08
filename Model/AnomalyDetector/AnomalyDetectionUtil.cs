using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OxyPlot;
using System.ComponentModel;

namespace FlightSimulatorApp.Model.AnomalyDetector {
    static public class AnomalyDetectionUtil {
        static public double average(List<double> x, int size) {
            double sum = 0;
            for (int i = 0; i < size; sum += x[i], i++) ;
            return sum / size; 
        }

		static public double var(List<double> x, int size) {
			double av = average(x, size);
			double sum = 0;
			for (int i = 0; i < size; i++) {
				sum += x[i] * x[i];
			}
			return sum / size - av * av;
		}

		// returns the covariance of X and Y
		static public double cov(List<double> x, List<double> y, int size) {
			double sum = 0;
			for (int i = 0; i < size; i++) {
				sum += x[i] * y[i];
			}
			sum /= size;

			return sum - average(x, size) * average(y, size);
		}


		// returns the Pearson correlation coefficient of X and Y
		static public double pearson(List<double> x, List<double> y, int size) {
			return cov(x, y, size) / (Math.Sqrt(var(x, size)) * Math.Sqrt(var(y, size)));
		}

		// performs a linear regression and returns the line equation
		static public Line linear_reg(List<DataPoint> points, int size) {
			List<double> x = new List<double>();
			List<double> y = new List<double>();
			for (int i = 0; i < size; i++) {
				x.Add(points[i].X);
				y.Add(points[i].Y);
			}
			double a = cov(x, y, size) / var(x, size);
			double b = average(y, size) - a * (average(x, size));

			return new Line(a, b);
		}


		static public Line linear_reg(List<double> x, List<double> y, int size) {
			double a = cov(x, y, size) / var(x, size);
			double b = average(y, size) - a * (average(x, size));
			return new Line(a, b);
		}

		// returns the deviation between point p and the line equation of the points
		static public double dev(DataPoint p, List<DataPoint> points, int size) {
			Line l = linear_reg(points, size);
			return dev(p, l);
		}

		// returns the deviation between point p and the line
		static public double dev(DataPoint p, Line l) {
			return Math.Abs(p.Y - l.f(p.X));
		}

		static public double max_dev(List<double> x, List<double> y, Line l, int size) {
			double result = 0; 
			for (int i =0; i<size; i++) {
				DataPoint p = new DataPoint(x[i], y[i]);
				double currentDev = dev(p, l);
				if (currentDev > result) {
					result = currentDev; 
                }
			}
			return result; 
        }

	}
}
