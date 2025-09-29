using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace practik_b;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }
    private void Button_Click1(object sender, RoutedEventArgs e)
    {
        grid.ItemsSource = Process.GetProcesses();
    }
    private void Button_Click2(object sender, RoutedEventArgs e)
    {
        Process? pr = grid.SelectedItem as Process;
        pr?.Kill();
    }
    private void Button_Click3(object sender, RoutedEventArgs e)
    {
        Process? pr = grid.SelectedItem as Process;
        string message = "";
        message += "process name: " + pr?.ProcessName + "\n" + "id: " + pr?.Id + "\n" + "base priority: " + pr?.BasePriority + "\n" + "machine name: " + pr?.MachineName;

        MessageBox.Show(message, "Process info", MessageBoxButton.OK, MessageBoxImage.Information);
    }
    private void Button_Click4(object sender, RoutedEventArgs e)
    {
        MessageBox.Show(nameProc.Text);
        Process.Start(nameProc.Text);
    }
}