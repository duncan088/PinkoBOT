using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotHandler
{
     public interface IConsole
     {
         public void Write(string text, Color color);
     }
}
