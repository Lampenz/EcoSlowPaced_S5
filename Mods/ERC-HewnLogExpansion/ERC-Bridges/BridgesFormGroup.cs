// Eco Russian Community

namespace Eco.Mods.TechTree
{
    using System;
    using Eco.Gameplay.Blocks;
    using Eco.Shared.Localization;

    public partial class BridgesFormGroup : FormGroup
    {
        public override string Name => "Bridges"; //noloc
        public override LocString DisplayName => Localizer.DoStr("Bridges");
        public override LocString DisplayDescription => Localizer.DoStr("Bridges");
        public override int SortOrder => 8;
    }
}
