using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using OxyPlot;

namespace FlightSimulatorApp.ViewModel {
    using FlightSimulatorApp.Model;
    using System.Globalization;
    using System.Xml;

    public class TimeSeries {
        // fields
        public List<string> simple_data;
        public List<Tuple<string, List<float>>> whole_data;
        public Dictionary<string, List<float>> data_map;
        public List<Tuple<string, List<float>>> selected_data;
        public List<DataMember> data_members;
        public int n_lines;
        public int n_colums;


        // contructor
        public TimeSeries(string file_path) {
            this.simple_data = initialize_simple_data(file_path);
            this.whole_data = initialize_whole_data(file_path);
            this.n_lines = this.simple_data.Count();
            this.n_colums = this.whole_data.Count();
            this.selected_data = initialize_selected_data();
            this.data_map = initialize_data_map();
            this.data_members = initialize_data_members();
    }

        List<string> initialize_simple_data(string file_path) {
            List<string> result = new List<string>();
            StreamReader input = new StreamReader(file_path);
            string line;
            while ((line = input.ReadLine()) != null) {
                result.Add(line);
            }
            input.Close();
            return result; 
        }

        List<Tuple<string, List<float>>> initialize_whole_data(string file_path) {
            List<Tuple<string, List<float>>> result = new List<Tuple<string, List<float>>>();
            //Console.WriteLine("HERE\n");
            // getting titles from xml:
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("..\\..\\Model\\playback_small.xml");
            XmlNodeList xnList = xmlDoc.SelectNodes("/PropertyList/generic/output/chunk");
            foreach (XmlNode xn in xnList) {
                string name = xn["name"].InnerText;
                result.Add(new Tuple<string, List<float>>(name, new List<float>()));
            }
            StreamReader csvDoc = new StreamReader(file_path);
            string line;
            while ((line = csvDoc.ReadLine()) != null) {
                string[] elements = line.Split(',');
                int i = 0, j=0; 
                foreach (var word in elements) {
                    result[i++].Item2.Add(float.Parse(word, CultureInfo.InvariantCulture.NumberFormat));
                }
            }
            csvDoc.Close();
            return result;
        }
        List<Tuple<string, List<float>>> initialize_selected_data() {
            List<Tuple<string, List<float>>> result = new List<Tuple<string, List<float>>>();
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

        Dictionary<string, List<float>> initialize_data_map() {
            Dictionary<string, List<float>> result = new Dictionary<string, List<float>>();
            for (int i = 0; i < this.n_colums; i++) {
                if (!result.ContainsKey(this.whole_data[i].Item1)) {
                    result.Add(this.whole_data[i].Item1, this.whole_data[i].Item2);
                }
            }
            return result; 
        }

        List<DataMember> initialize_data_members() {
            List<DataMember> result = new List<DataMember>();
            for (int i = 0; i<this.whole_data.Count(); i++) {
                DataMember current = new DataMember();
                current.Name = whole_data[i].Item1;
                List<DataPoint> points = new List<DataPoint>();
                points.Add(new DataPoint(0, whole_data[i].Item2[0]));
                current.Points = points;
                // TODO: find most correlative
                result.Add(current);
            }
            return result;
        }
    }
}
