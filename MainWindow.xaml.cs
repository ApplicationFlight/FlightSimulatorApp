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
using Microsoft.Win32;
using FlightSimulatorApp.Model;
using FlightSimulatorApp.ViewModel;
using FlightSimulatorApp.View;

namespace FlightSimulatorApp {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        CSViewModel vm;
      
        public MainWindow() {
            InitializeComponent();
            vm = new CSViewModel(new ModelCSV());
            DataContext = vm;
        }

        private void UploadCVSButton_Click(object sender, RoutedEventArgs e) {
            OpenFileDialog fileDialog = new OpenFileDialog();
            bool? response = fileDialog.ShowDialog();
            if (response == true) {
                vm.setFile(fileDialog.FileName); //path of the CSV file
            }
        }

        private void Continue_Click(object sender, RoutedEventArgs e) {
            main_f.Content = new Page2(vm);
        }
    }
}
