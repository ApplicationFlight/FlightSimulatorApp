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
    using System.ComponentModel;

    public partial class Page2 : Page {

        private CSViewModel vm;
        
        public Page2(CSViewModel vm) {
            InitializeComponent();
            this.vm = vm;
            DataContext = vm;
        }

        private void Play_Click(object sender, RoutedEventArgs e) {
            vm.VM_Is_running = true;
        }

        private void Pause_Click(object sender, RoutedEventArgs e) {
            vm.VM_Is_running = false;
        }

        private void Speed_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            ComboBox cmb = sender as ComboBox;
            switch (Speed.SelectedItem.ToString().Split(new string[] { ": " }, StringSplitOptions.None).Last()) {
                case "x0.5":
                    vm.VM_Speed = 50;
                    break;
                case "x1.0":
                    vm.VM_Speed = 100;
                    break;
                case "x1.5":
                    vm.VM_Speed = 150;
                    break;
            }
        }
    }
}