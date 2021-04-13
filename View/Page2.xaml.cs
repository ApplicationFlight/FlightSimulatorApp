using Microsoft.Win32;
using System;
using System.Linq;
using System.Windows.Controls;


namespace FlightSimulatorApp.View {

    using FlightSimulatorApp.Model;
    using FlightSimulatorApp.Model.AnomalyDetector;
    using FlightSimulatorApp.ViewModel;
    using System.Windows;

    public partial class Page2 : Page {

        public ModelCSV model;
        public Page2ViewModel vm;
        public VideoPlayerViewModel vvm;

        public Page2(ModelCSV model) {
            InitializeComponent();
            this.model = model;
            this.vm = new Page2ViewModel(this.model);
            DataContext = vm;
            this.vvm = new VideoPlayerViewModel(this.model);
            videoplayer.VM = vvm;
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
                        this.vm.Add_Algorithm(fileDialog.FileName);
                    }
                    break;
                case "Circle":
                    fileDialog = new OpenFileDialog();
                    response = fileDialog.ShowDialog();
                    if (response == true) {
                        this.vm.Add_Algorithm(fileDialog.FileName);
                    }
                    break;
                case "Other":
                    fileDialog = new OpenFileDialog();
                    response = fileDialog.ShowDialog();
                    if (response == true) {
                        this.vm.Add_Algorithm(fileDialog.FileName);
                    }
                    break;
            }
        }

        void List_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            AnomalyReport selected = (AnomalyReport)((ListView)sender).SelectedItem;
            this.vvm.VM_Percentage = ((double)selected.line / (double)this.vm.modelCSV.anomaly_flight.n_lines) * 100;
        }
    }
}