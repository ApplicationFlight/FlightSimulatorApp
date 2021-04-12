using System.Net.Sockets;

namespace FlightSimulatorApp.Model {
    public interface ItelnetClient {
        TcpClient Client { get; set; }
        void connect();
        void disconnect();
    }
}
