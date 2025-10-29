using Eco.Core.Plugins.Interfaces;

namespace Eco.Mods.ERC.ERCHLE
{
	internal class ERCHLE : IModInit
	{
		public static ModRegistration Register() => new()
		{
			ModName = "ERCHLE",
			ModDescription = "New fences, bridges, ramps and arches from hewn logs.",
			ModDisplayName = "ERC Hewn Log Expansion",
		};
	}
}
