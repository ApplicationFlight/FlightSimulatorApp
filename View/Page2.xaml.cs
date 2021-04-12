using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Linq;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Microsoft.Win32;
using FlightSimulatorApp.Model;
using FlightSimulatorApp.ViewModel;
using FlightSimulatorApp.View;


namespace FlightSimulatorApp.View {
     
    using FlightSimulatorApp.ViewModel;
    using FlightSimulatorApp.Model;
    using FlightSimulatorApp.View.UserControls;
    using System.ComponentModel;
    using System.Windows;

    public partial class Page2 : Page {

        public ModelCSV model;
        public Page2ViewModel vm; 
        public Page2(ModelCSV model) {
            InitializeComponent();
            this.model = model;
            this.vm = new Page2ViewModel(this.model);
            DataContext = vm;
            videoplayer.VM = new VideoPlayerViewModel(this.model);
            dataflight.VM = new DataFlightViewModel(this.model);
            joystick.VM = new JoystickViewModel(this.model);
        }

        public void Go_Data(object sender, RoutedEventArgs e) {
            _mainFrame.Navigate(new Page3(model));
        }

        public void Algo_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            Console.WriteLine("entered also changed");
            ComboBox cmb = sender as ComboBox;
            OpenFileDialog fileDialog;
            bool? response;
            switch (Algo.SelectedItem.ToString().Split(new string[] { ": " }, StringSplitOptions.None).Last()) {
                case "Regression":
                    fileDialog = new OpenFileDialog();
                    response = fileDialog.ShowDialog();
                    if (response == true) {
                        this.vm.Add_Algorithm("regression", fileDialog.FileName);
                    }
                    break;
                case "Circle":
                    fileDialog = new OpenFileDialog();
                    response = fileDialog.ShowDialog();
                    if (response == true) {
                        this.vm.Add_Algorithm("circle", fileDialog.FileName);
                    }
                    break;
                case "Other":
                    fileDialog = new OpenFileDialog();
                    response = fileDialog.ShowDialog();
                    if (response == true) {
                        this.vm.Add_Algorithm("other", fileDialog.FileName);
                    }
                    break;
            }
        }
    }
}