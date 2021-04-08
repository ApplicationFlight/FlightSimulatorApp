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
using System.Windows.Threading;

namespace FlightSimulatorApp.View {
     
    using FlightSimulatorApp.ViewModel;
    using FlightSimulatorApp.Model;
    using FlightSimulatorApp.View.UserControls;
    using System.ComponentModel;

    public partial class Page2 : Page {

        public ModelCSV model;
        public Page2(ModelCSV model) {
            InitializeComponent();
            this.model = model;
            //DataContext = vm;
            videoplayer.VM = new VideoPlayerViewModel(this.model);
            dataflight.VM = new DataFlightViewModel(this.model);
            joystick.VM = new JoystickViewModel(this.model);

        }

        private void joystick_Loaded(object sender, RoutedEventArgs e) {

        }
    }
}