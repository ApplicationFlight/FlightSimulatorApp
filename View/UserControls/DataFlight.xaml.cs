using System.Windows.Controls;

namespace FlightSimulatorApp.View.UserControls {
    using FlightSimulatorApp.ViewModel;
    /// <summary>
    /// Interaction logic for DataFlight.xaml
    /// </summary>
    public partial class DataFlight : UserControl {
        private DataFlightViewModel vm;
        public DataFlightViewModel VM {
            set {
                this.vm = value;
                DataContext = vm;
            }
            get {
                return this.vm;
            }
        }
        public DataFlight() {
            InitializeComponent();
        }
    }
}
