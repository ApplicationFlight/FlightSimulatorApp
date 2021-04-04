using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace FlightSimulatorApp.Model  {

    using FlightSimulatorApp.ViewModel;

    public interface IModelCSV : INotifyPropertyChanged {
        // connection to FG
        void connect();
        void disconnet();
        void start();


        // fields
        float Percentage { set; get; }
        int Speed { set; get; }
        bool Is_running { set; get; }
        int Relative_time { set; get; }
        TimeSeries Timeseries { set; get; }

    }
}
