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
    /// Interaction logic for DataFlight.xaml
    /// </summary>
    public partial class DataFlight : UserControl {
        private DataFlightViewModel vm;
        public DataFlightViewModel VM {
            set {
                this.vm = value;
                DataContext = vm;
                //this.listBox.ItemsSource = new int[] { 1, 3, 5, 7, 9 };
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
