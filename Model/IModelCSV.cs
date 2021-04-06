using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.Generic;
using OxyPlot;

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
        float[] Selected_data { set; get; }
        float Throttle { set; get; }
        float Aileron { set; get; }
        float Elevator { set; get; }
        float Rudder { set; get; }
        //List<DataPoint> Points { set; get; } //TODO: remove this
        List<DataMember> Data_members { set; get; }
        DataMember Data_member { set; get; }
        List<DataPoint> Points { set; get; }
        TimeSeries Timeseries { set; get; }

    }
}
