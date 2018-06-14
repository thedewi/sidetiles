using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using WpfAppBar;

namespace SideTiles
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool _barred;

        public MainWindow()
        {
            InitializeComponent();
        }
        protected override void OnMouseDoubleClick(MouseButtonEventArgs e)
        {
            base.OnMouseDoubleClick(e);

            AppBarFunctions.SetAppBar(this, _barred ? ABEdge.None : ABEdge.Right);
            _barred = !_barred;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            AppBarFunctions.SetAppBar(this, ABEdge.None);
        }
    }
}
