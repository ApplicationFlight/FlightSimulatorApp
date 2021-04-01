using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace FlightSimulatorApp.Model {

    using FlightSimulatorApp.ViewModel;


    class ModelCSV : INotifyPropertyChanged {
        private string file_CSV;
        public event PropertyChangedEventHandler PropertyChanged;
        public string fileCSV {
            get {
                return file_CSV;
            }
            set {
                file_CSV = value;
            }
        }
        public void NotifyPropertyChanged(string name) {
            if (PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        public void connect() {
            TcpClient client = new TcpClient("localhost", 5400);
            StreamReader input = new StreamReader(fileCSV);
            StreamWriter output = new StreamWriter(client.GetStream());
            String line;
            while ((line = input.ReadLine()) != null) {
                output.WriteLine(line);
                System.Threading.Thread.Sleep(100);
            }
            client.Close();
            input.Close();
            output.Close();
        }
    }
}

