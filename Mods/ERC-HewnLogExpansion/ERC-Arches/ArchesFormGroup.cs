// Eco Russian Community

namespace Eco.Mods.TechTree
{
    using System;
    using Eco.Gameplay.Blocks;
    using Eco.Shared.Localization;

    public partial class ArchesFormGroup : FormGroup
    {
        public override string Name => "Arches"; //noloc
        public override LocString DisplayName => Localizer.DoStr("Arches");
        public override LocString DisplayDescription => Localizer.DoStr("Arches");
        public override int SortOrder => 8;
    }
}
