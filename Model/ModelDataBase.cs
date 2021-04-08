using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using OxyPlot;

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
        // a list of dataMember. Each data members has its list of points with time, and a correlated feature
        public List<DataMember> data_members;
        // map, name of column, list of points between two correlated features 
        public Dictionary<string, List<double>> correlations_points;
        public int n_lines;
        public int n_colums;
        public SimpleAnomalyDetector ad; 


        // contructor
        public ModelDataBase(string file_path) {
            var lines = File.ReadAllLines(file_path);
            File.WriteAllLines(file_path, lines.Skip(1).ToArray());
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
            // skipping one line to remove titles, and basing on the xml. TODO: Maybe to change later 
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
            xmlDoc.Load("..\\..\\Model\\playback_small.xml");
            XmlNodeList xnList = xmlDoc.SelectNodes("/PropertyList/generic/output/chunk");
            foreach (XmlNode xn in xnList) {
                string name = xn["name"].InnerText;
                result.Add(new Tuple<string, List<double>>(name, new List<double>()));
            }
            StreamReader csvDoc = new StreamReader(file_path);
            string line;
            // skipping one line to remove titles, and basing on the xml. TODO: Maybe to change later 
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
            for (int i =0; i<this.n_colums; i++) {
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
            ad.learnNormal(new TimeSeries("..\\..\\Model\\reg_flight.csv"));
            // to add when ready ad.detect(filepath);
        }

        void find_most_correlative(List<DataMember> data_members) { 
            for (int i = 0; i< data_members.Count(); i++) {
                string feature1 = data_members[i].Name;
                // based on learning from the reg_flight
                this.data_members[i].Correlative = this.ad.simple_cf[feature1];             
            }
        }

        List<DataMember> initialize_data_members() {
            List<DataMember> result = new List<DataMember>();
            for (int i = 0; i<this.whole_data.Count(); i++) {
                DataMember current = new DataMember();
                current.Name = whole_data[i].Item1;
                List<DataPoint> points = new List<DataPoint>();
                points.Add(new DataPoint(0, whole_data[i].Item2[0]));
                current.Points = points;
                result.Add(current);
            }
            return result;
        }
    }
}
