using OxyPlot;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FlightSimulatorApp.Model {
    using FlightSimulatorApp.Model.AnomalyDetector;
    using System.Globalization;
    using System.Xml;

    public class ModelDataBase {
        // fields
        // each string is one line in the csv
        public List<string> simple_data;
        // list of pairs, name of column and list representing column
        public List<Tuple<string, List<double>>> whole_data;
        // map, name of column and list representing column
        public Dictionary<string, List<double>> data_map;
        // list like before, but only elements appearing in dashboard
        public List<Tuple<string, List<double>>> selected_data;
        // list of datamembers 
        public Dictionary<string, DataMember> data_members;

        public int n_lines;
        public int n_colums;
        public SimpleAnomalyDetector ad;

        // constructor
        public ModelDataBase(string file_path) {
            this.simple_data = initialize_simple_data(file_path);
            this.whole_data = initialize_whole_data(file_path);
            this.n_lines = this.simple_data.Count();
            this.n_colums = this.whole_data.Count();
            this.selected_data = initialize_selected_data();
            this.data_map = initialize_data_map();
            initialize_ad();
            this.data_members = initialize_data_members();
            find_most_correlative(data_members);
        }

        List<string> initialize_simple_data(string file_path) {
            List<string> result = new List<string>();
            StreamReader input = new StreamReader(file_path);
            string line;
            line = input.ReadLine();
            while ((line = input.ReadLine()) != null) {
                result.Add(line);
            }
            input.Close();
            return result;
        }

        List<Tuple<string, List<double>>> initialize_whole_data(string file_path) {
            List<Tuple<string, List<double>>> result = new List<Tuple<string, List<double>>>();
            XmlDocument xmlDoc = new XmlDocument();
            string path = "Resources\\Documents\\playback_small.xml";
            xmlDoc.Load(path);
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
        List<Tuple<string, List<double>>> initialize_selected_data() {
            List<Tuple<string, List<double>>> result = new List<Tuple<string, List<double>>>();
            for (int i = 0; i < this.n_colums; i++) {
                string l = this.whole_data[i].Item1;
                if (l.Equals("altitude-ft") || l.Equals("airspeed-kt") ||
                    l.Equals("heading-deg") || l.Equals("roll-deg") ||
                    l.Equals("pitch-deg") || l.Equals("side-slip-deg")) {
                    result.Add(this.whole_data[i]);
                }
            }
            return result;
        }

        Dictionary<string, List<double>> initialize_data_map() {
            Dictionary<string, List<double>> result = new Dictionary<string, List<double>>();
            for (int i = 0; i < this.n_colums; i++) {
                if (!result.ContainsKey(this.whole_data[i].Item1)) {
                    result.Add(this.whole_data[i].Item1, this.whole_data[i].Item2);
                }
            }
            return result;
        }

        void initialize_ad() {
            ad = new SimpleAnomalyDetector();
            ad.learnNormal(new TimeSeries(AppDomain.CurrentDomain.BaseDirectory + "Resources\\Documents\\reg_flight.csv"));
        }

        void find_most_correlative(Dictionary<string, DataMember> data_members) {
            foreach (KeyValuePair<string, DataMember> entry in data_members) {
                string feature1 = entry.Key;
                // adding name
                string feature2 = this.ad.simple_cf[feature1];
                this.data_members[feature1].Correlative_string = feature2;
                // adding list of points
                List<double> x = this.data_map[feature1];
                List<double> y = this.data_map[feature2];
                List<DataPoint> current = new List<DataPoint>();
                for (int i = 0; i < x.Count(); i++) {
                    current.Add(new DataPoint(x[i], y[i]));
                }
                this.data_members[feature1].Regression_points = current;
                this.data_members[feature1].Regression_line = AnomalyDetectionUtil.linear_reg_list(current, current.Count());
            }
        }

        Dictionary<string, DataMember> initialize_data_members() {
            Dictionary<string, DataMember> result = new Dictionary<string, DataMember>();
            for (int i = 0; i < this.whole_data.Count(); i++) {
                DataMember current = new DataMember();
                current.Name = whole_data[i].Item1;
                List<DataPoint> points = new List<DataPoint>();
                points.Add(new DataPoint(0, whole_data[i].Item2[0]));
                current.Points = points;
                if (!result.ContainsKey(current.Name)) {
                    result.Add(current.Name, current);
                }
            }
            return result;
        }
    }
}
