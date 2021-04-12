using System.Net.Sockets;

namespace FlightSimulatorApp.Model {
    public class TelnetClient : ItelnetClient {
        private TcpClient client;
        public TcpClient Client {
            get {
                return this.client;
            }
            set {
                this.client = value;
            }
        }
        public void connect() {
            this.client = new TcpClient("localhost", 5400);
        }

        public void disconnect() {
            this.client.Close();
        }
    }
}
