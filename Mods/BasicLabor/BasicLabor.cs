using Eco.Core.Plugins.Interfaces;
using Eco.Shared.Localization;
using Eco.Shared.Logging;

namespace EcoPulse.BasicLabor
{
    internal class BasicLabor : IModInit
    {
        public static readonly string Version = "1.0.3";
        public BasicLabor()
        {
            Log.WriteLine(Localizer.Do($"BasicLabor {Version}"));
        }
        public static ModRegistration Register() => new()
        {
            ModName = "BasicLabor",
            ModDescription = "Manual tools for harvesting and road tamping",
            ModDisplayName = "Basic Labor",
        };
    }
}
