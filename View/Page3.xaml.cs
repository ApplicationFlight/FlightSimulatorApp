using System;
using System.Windows;
using System.Windows.Controls;

namespace FlightSimulatorApp.View {
    using FlightSimulatorApp.Model;
    using FlightSimulatorApp.ViewModel;

    /// <summary>
    /// Interaction logic for Page3.xaml
    /// </summary>
    public partial class Page3 : Page {

        private Page3ViewModel viewmodel;
        private ModelCSV model;

        public Page3(ModelCSV model) {
            InitializeComponent();
            this.model = model;
            this.viewmodel = new Page3ViewModel(model);
            DataContext = viewmodel;
            System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = TimeSpan.FromMilliseconds(100);
            dispatcherTimer.Start();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e) {
            Graph.InvalidatePlot(true);
            Graph_Correlative.InvalidatePlot(true);

        }

        private void List_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            DataMember selected = (DataMember)((ListView)sender).SelectedItem;
            Graph.Subtitle = selected.Name;
            Graph_Correlative.Subtitle = selected.Correlative_string;
            viewmodel.modelCSV.Data_member = selected;
        }

        void Go_Controls(object sender, RoutedEventArgs e) {
            _mainFrame.Navigate(new Page2(model));
        }
    }
}
