using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotWpf
{
    public class ConsoleWrapper: BotHandler.IConsole
     {
         private ConsoleControl.WPF.ConsoleControl _console;

         public ConsoleWrapper(ConsoleControl.WPF.ConsoleControl console)
         {
             _console = console;
         }

         public void Write(string text, Color color)
         {
             _console.WriteOutput(text, System.Windows.Media.Color.FromArgb(color.A, color.R, color.G, color.B));
         }
     }
}
