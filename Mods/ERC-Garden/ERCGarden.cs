using Eco.Core.Plugins.Interfaces;

namespace Eco.Mods.ERC.ERCGarden
{
	internal class ERCGarden : IModInit
	{
		public static ModRegistration Register() => new()
		{
			ModName = "ERCGarden",
			ModDescription = "Let's green your life!",
			ModDisplayName = "ERC Garden",
		};
	}
}
