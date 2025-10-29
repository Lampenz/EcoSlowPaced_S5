using Eco.Core.Plugins.Interfaces;
using Eco.Shared.Localization;
using Eco.Shared.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoPulse.StatueMod
{
    internal class StatueMod : IModInit
    {
        public static readonly string Version = "1.0.2";
        public StatueMod()
        {
            Log.WriteLine(Localizer.Do($"StatueMod {Version}"));
        }
        public static ModRegistration Register() => new()
        {
            ModName = "StatueMod",
            ModDescription = "Add beautiful and cool copper, glass and gold statues",
            ModDisplayName = "Statue Mod",
        };
    }
}
