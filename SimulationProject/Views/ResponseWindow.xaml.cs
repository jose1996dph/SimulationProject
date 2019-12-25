using SimulationProject.ViewModels;
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
using System.Windows.Shapes;

using SimulationProject.Models;

namespace SimulationProject.Views
{
    /// <summary>
    /// Lógica de interacción para ResponseWindow.xaml
    /// </summary>
    public partial class ResponseWindow : Window
    {
        ResponseViewModel viewModel;
        public ResponseWindow(int p, List<double> items)
        {
            InitializeComponent();

            DataContext = viewModel = new ResponseViewModel(p, items);

            viewModel.LoadItemsCommad.Execute(null);
        }
    }
}
