using MahApps.Metro.Controls;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;

namespace touhouSSRen
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            CommandBindings.AddRange(MyCommands.BindingMyCommands());

            DragOver += (s, e) =>
            {
                if (e.Data.GetDataPresent(DataFormats.FileDrop))
                    e.Effects = DragDropEffects.Move;
            };

            Drop += (s, e) =>
            {
                if (!e.Data.GetDataPresent(DataFormats.FileDrop))
                    return;

                var fnames = (string[])e.Data.GetData(DataFormats.FileDrop);

                if (Directory.Exists(fnames[0]))
                    fnames = Directory.GetFiles(fnames[0]);

                var dir = Path.GetDirectoryName(fnames[0]);
                var num = 0;

                foreach (var fname in fnames.OrderBy(x => x))
                {
                    var res = FnPattern().Match(fname);

                    if (res.Success)
                    {
                        var dest = Path.Combine(dir!, $"{res.Groups[1].Value}{num++:000}.{res.Groups[2].Value}");
                        File.Move(fname, dest);
                    }
                }
            };
        }

        [GeneratedRegex(@"(th[0-9_]*?)[0-9]{3,}\.(bmp|png)$")]
        private static partial Regex FnPattern();
    }
}
