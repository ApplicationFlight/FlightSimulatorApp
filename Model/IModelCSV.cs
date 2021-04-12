using OxyPlot;
using System.Collections.Generic;
using System.ComponentModel;

namespace FlightSimulatorApp.Model {
    using FlightSimulatorApp.Model.AnomalyDetector;

    public interface IModelCSV : INotifyPropertyChanged {
        // connection to FG
        void connect();
        void disconnet();
        void start();

        void Add_Algorithm(string path);

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
        List<DataPoint> Regression_30seconds { set; get; }
        // anomaly report
        List<AnomalyReport> Anomaly_reports { set; get; }
    }
}
