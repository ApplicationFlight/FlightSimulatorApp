using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorApp.Model {
    public interface ItelnetClient {
        TcpClient Client { get; set; }
        void connect();
        void disconnect();

    }
}
