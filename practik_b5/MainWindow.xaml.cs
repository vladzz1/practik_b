using Microsoft.WindowsAPICodePack.Dialogs;
using PropertyChanged;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;

namespace practik_b5;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    ViewModel model = new ViewModel();
    public MainWindow()
    {
        InitializeComponent();
        DataContext = model;
    }
    private void SourceBtn(object sender, RoutedEventArgs e)
    {
        CommonOpenFileDialog dialog = new CommonOpenFileDialog();
        if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
        {
            model.Source = sourceTb.Text = dialog.FileName;
        }
    }
    private void DestBtn(object sender, RoutedEventArgs e)
    {
        CommonOpenFileDialog dialog = new CommonOpenFileDialog();
        dialog.IsFolderPicker = true;
        if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
        {
            model.Destination = destTb.Text = dialog.FileName;
        }
    }
    private void CopyBtn(object sender, RoutedEventArgs e)
    {
        try
        {
            string filename = Path.GetFileName(model.Source)!;
            string destFilename = Path.Combine(model.Destination!, filename);
            CopyProcessesInfo info = new CopyProcessesInfo(filename);
            model.AddProcess(info);
            copyFileAsync(model.Source!, destFilename, info);
        }
        catch (Exception x)
        {
            MessageBox.Show(x.Message);
        }
    }
    void copyFileAsync(string src, string dest, CopyProcessesInfo info)
    {
        Task.Run(() =>
        {
            using FileStream srcStream = new FileStream(src, FileMode.Open, FileAccess.Read);
            using FileStream desStream = new FileStream(dest, FileMode.Create, FileAccess.Write);
            byte[] buffer = new byte[1024 * 8];//8Kb
            int bytes = 0;
            do
            {
                bytes = srcStream.Read(buffer, 0, buffer.Length);
                desStream.Write(buffer, 0, bytes);

                float percentage = desStream.Length / (srcStream.Length / 100);
                info.Percentage = percentage;

            } while (bytes > 0);
        });
        info.Percentage = 100;
    }
}
[AddINotifyPropertyChangedInterface]
class ViewModel
{
    ObservableCollection<CopyProcessesInfo> processes;
    public string? Source { get; set; }
    public string? Destination { get; set; }
    public IEnumerable<CopyProcessesInfo> Processes => processes;
    public ViewModel() { processes = []; }
    public void AddProcess(CopyProcessesInfo info)
    {
        processes.Add(info);
    }
}
[AddINotifyPropertyChangedInterface]
class CopyProcessesInfo
{
    public string? Filename { get; set; }
    public float Percentage { get; set; }
    public CopyProcessesInfo(string filename, float percentage = 0)
    {
        Filename = filename;
        Percentage = percentage;
    }
}