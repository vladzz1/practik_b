using System.IO;
using System.Windows;

namespace practik_b5;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }
    private void Button_Click(object sender, RoutedEventArgs e)
    {
        copyAsync();
    }
    void copyAsync()
    {
        Task.Run(() =>
        {
            try
            {
                File.Copy(text1.Text, text2.Text);
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message);
            }
        });
    }
}