﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FlightSimulatorApp.ViewModel {
    using FlightSimulatorApp.Model;
    using System.Globalization;
    using System.Xml;

    public class TimeSeries {
        // fields
        public List<string> simple_data;
        List<Tuple<string, List<float>>> whole_data;
        public int n_lines;
        public int n_colums;

        // contructor
        public TimeSeries(string file_path) {
            this.simple_data = get_simple_data(file_path);
            this.whole_data = get_whole_data(file_path);
            this.n_lines = this.simple_data.Count();
            this.n_colums = this.whole_data.Count();
        }

        List<string> get_simple_data(string file_path) {
            List<string> result = new List<string>();
            StreamReader input = new StreamReader(file_path);
            string line;
            while ((line = input.ReadLine()) != null) {
                result.Add(line);
            }
            input.Close();
            return result; 
        }

        List<Tuple<string, List<float>>> get_whole_data(string file_path) {
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
                int i = 0; 
                foreach (var word in elements) {
                    result[i++].Item2.Add(float.Parse(word, CultureInfo.InvariantCulture.NumberFormat));
                }
            }
            /*StreamWriter output = new StreamWriter("..\\..\\Model\\output.csv");
            for (int i = 0; i<result[0].Item2.Count(); i++) {
                for (int j= 0; j<result.Count(); j++) {
                    output.Write(result[j].Item2[i].ToString());
                    output.Write(',');
                }
                output.WriteLine();
            }*/
            csvDoc.Close();
            return result; 
        }

        // methods
    }
}
