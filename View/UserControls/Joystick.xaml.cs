using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
