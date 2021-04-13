using System.Windows.Controls;

namespace FlightSimulatorApp.View.UserControls {
    using FlightSimulatorApp.ViewModel;

    /// <summary>
    /// Interaction logic for Joystick.xaml
    /// </summary>
    public partial class Joystick : UserControl {

        private JoystickViewModel vm;
        public JoystickViewModel VM {
            set {
                this.vm = value;
                DataContext = vm;
            }
            get {
                return this.vm;
            }
        }
        public Joystick() {
            InitializeComponent();
        }
    }
}
