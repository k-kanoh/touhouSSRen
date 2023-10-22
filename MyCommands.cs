using System.Collections;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

namespace touhouSSRen
{
    public static class MyCommands
    {
        public static RoutedCommand ApplicationExecute { get; } = new RoutedCommand(nameof(ApplicationExecute), typeof(Window));

        public static IList BindingMyCommands()
        {
            return new List<CommandBinding>()
            {
                new CommandBinding(ApplicationExecute, (s, e) =>
                {
                    Process.Start(new ProcessStartInfo()
                    {
                        UseShellExecute = true,
                        FileName = e.Parameter.ToString()!
                    });
                })
            };
        }
    }
}
