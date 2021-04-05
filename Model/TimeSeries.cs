using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FlightSimulatorApp.ViewModel {
    using FlightSimulatorApp.Model;
    public class TimeSeries {
        // fields
        public List<string> data;
        public int n_lines; 

        // contructor
        public TimeSeries(string file_path) {
            this.data = extractData(file_path);
            this.n_lines = this.data.Count();
        }

        List<string> extractData(string file_path) {
            List<string> result = new List<string>();
            StreamReader input = new StreamReader(file_path);
            string line;
            while ((line = input.ReadLine()) != null) {
                result.Add(line);
            }
            input.Close();
            return result; 
        }

        // methods
    }
}
