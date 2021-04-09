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
        //vieoplayer
        double Percentage { set; get; }
        int Speed { set; get; }
        bool Is_running { set; get; }
        int Relative_time { set; get; }
        // dashboard
        double[] Selected_data { set; get; }
        // joystick
        double Throttle { set; get; }
        double Aileron { set; get; }
        double Elevator { set; get; }
        double Rudder { set; get; }
        // graphs
        Dictionary<string, DataMember> Data_members { set; get; }
        DataMember Data_member { set; get; }
        List<DataPoint> Points { set; get; }
        List<DataPoint> Correlative_points { set; get; }
        // regression
        List<DataPoint> Regression_points { set; get; }
        List<DataPoint> Regression_line { set; get; }
        //List<DataPoint> Line { set; get; }
    }
}
