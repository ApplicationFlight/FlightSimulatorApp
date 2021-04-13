using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace FlightSimulatorApp.View.UserControls {
    using FlightSimulatorApp.ViewModel;
    /// <summary>
    /// Interaction logic for VideoPlayer.xaml
    /// </summary>
    public partial class VideoPlayer : UserControl {
        private VideoPlayerViewModel vm;
        public VideoPlayerViewModel VM {
            set {
                this.vm = value;
                DataContext = vm;
            }
            get {
                return this.vm;
            }
        }

        public VideoPlayer() {
            InitializeComponent();
        }

        public void Play_Click(object sender, RoutedEventArgs e) {
            vm.VM_Is_running = true;
        }

        public void Pause_Click(object sender, RoutedEventArgs e) {
            vm.VM_Is_running = false;
        }

        public void Speed_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            ComboBox cmb = sender as ComboBox;
            switch (Speed.SelectedItem.ToString().Split(new string[] { ": " }, StringSplitOptions.None).Last()) {
                case "x0.5":
                    vm.VM_Speed = (int)(100 / 0.5);
                    break;
                case "x1.0":
                    vm.VM_Speed = (int)(100 / 1);
                    break;
                case "x1.5":
                    vm.VM_Speed = (int)(100 / 1.5);
                    break;
            }
        }
    }
}
