using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml;

namespace FlightSimulatorApp.Model.AnomalyDetector {
    // this timeseries is only used by the SimpleAnomalyDetect to detect correlated features
    public class TimeSeries {

        public List<Tuple<string, List<double>>> whole_data;
        public int n_colums;
        public int n_lines;

        public TimeSeries(string file_path) {
            this.whole_data = initialize_whole_data(file_path);
            this.n_colums = this.whole_data.Count();
            this.n_lines = this.whole_data[0].Item2.Count();
        }


        List<Tuple<string, List<double>>> initialize_whole_data(string file_path) {
            List<Tuple<string, List<double>>> result = new List<Tuple<string, List<double>>>();
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("Resources\\Documents\\playback_small.xml");
            XmlNodeList xnList = xmlDoc.SelectNodes("/PropertyList/generic/output/chunk");
            foreach (XmlNode xn in xnList) {
                string name = xn["name"].InnerText;
                result.Add(new Tuple<string, List<double>>(name, new List<double>()));
            }
            StreamReader csvDoc = new StreamReader(file_path);
            string line;
            line = csvDoc.ReadLine();
            while ((line = csvDoc.ReadLine()) != null) {
                string[] elements = line.Split(',');
                int i = 0;
                foreach (var word in elements) {
                    result[i++].Item2.Add(double.Parse(word, CultureInfo.InvariantCulture.NumberFormat));
                }
            }
            csvDoc.Close();
            return result;
        }
    }
}
