using Serilog.Events;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projetEducationNationale.SaveManager
{
    public static class LoggerConfig
    {

        public static void Configuration()
        {
            var path = Directory.GetCurrentDirectory();
            var test = path.Split("bin");
            path = test[0];
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File(path  + "donnees.log", LogEventLevel.Information)
                .WriteTo.Console()
                .CreateLogger();
        }

    }
}
