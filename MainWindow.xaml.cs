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
using PodCatchup.Events;
using MahApps.Metro.Controls;

namespace PodCatchup
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow :  MetroWindow
  {
    public MainWindow()
    {
      InitializeComponent();
    }

    public void MainWindow_Closing(object sender, System.EventArgs e)
    {
      ApplicationService.Instance.EventAggregator.GetEvent<ApplicationCloseEvent>().Publish(true);
    }
  }
}
