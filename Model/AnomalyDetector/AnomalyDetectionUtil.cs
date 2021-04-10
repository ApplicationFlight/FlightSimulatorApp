using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OxyPlot;
using System.ComponentModel;

namespace FlightSimulatorApp.Model.AnomalyDetector {
    static public class AnomalyDetectionUtil {
        
		static public double average(List<double> x, double size) {
			if (size == 0) return 0; 
            double sum = 0;
            for (int i = 0; i < size; sum += x[i], i++) ;
            return sum / size; 
        }

		static public double var(List<double> x, double size) {
			double av = average(x, size);
			double sum = 0;
			for (int i = 0; i < size; i++) {
				sum += x[i] * x[i];
			}
			if (size - av * av == 0) return 0;
			return sum / size - av * av;
		}

		// returns the covariance of X and Y
		static public double cov(List<double> x, List<double> y, double size) {
			if (size == 0) return 0; 
			double sum = 0;
			for (int i = 0; i < size; i++) {
				sum += x[i] * y[i];
			}
			sum = sum/size;
			return sum - average(x, size) * average(y, size);
		}


		// returns the Pearson correlation coefficient of X and Y
		static public double pearson(List<double> x, List<double> y, double size) {
			double divider = Math.Sqrt(var(x, size)) * Math.Sqrt(var(y, size));
			if (divider == 0) return 0; 
			return cov(x, y, size) / divider;
		}


		static public List<DataPoint> linear_reg_list(List<DataPoint> points, double size) {
			List<DataPoint> result = new List<DataPoint>(); 
		    Line l = linear_reg(points, size); 
			double minX = points[0].X;
			double maxX = points[0].X;
			for (int i = 0; i < size; i++) {
				if (points[i].X > maxX) {
					maxX = points[i].X; 
                }
				if (points[i].X < minX) {
					minX = points[i].X;
				}
			}
			DataPoint p1 = new DataPoint(minX, l.f(minX));
			DataPoint p2 = new DataPoint(maxX, l.f(maxX));
			result.Add(p1);
			result.Add(p2);
			return result;							
		}

		// performs a linear regression and returns the line equation
		static public Line linear_reg(List<DataPoint> points, double size) {
			List<double> x = new List<double>();
			List<double> y = new List<double>();
			for (int i = 0; i < size; i++) {
				x.Add(points[i].X);
				y.Add(points[i].Y);
			}
			double a;
			if (var(x, size) == 0) {
				a = 0;
			}
			else a = cov(x, y, size) / var(x, size);
			double b = average(y, size) - a * (average(x, size));
			return new Line(a, b);
		}


		static public Line linear_reg(List<double> x, List<double> y, double size) {
			double a; 
			if (var(x, size) == 0) {
				a = 0;
			}
			else a = cov(x, y, size) / var(x, size);
			double b = average(y, size) - a * (average(x, size));
			return new Line(a, b);
		}

		// returns the deviation between point p and the line equation of the points
		static public double dev(DataPoint p, List<DataPoint> points, double size) {
			Line l = linear_reg(points, size);
			return dev(p, l);
		}

		// returns the deviation between point p and the line
		static public double dev(DataPoint p, Line l) {
			return Math.Abs(p.Y - l.f(p.X));
		}

		static public double max_dev(List<double> x, List<double> y, Line l, double size) {
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
