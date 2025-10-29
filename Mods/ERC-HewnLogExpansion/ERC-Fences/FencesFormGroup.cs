// Eco Russian Community

namespace Eco.Mods.TechTree
{
    using System;
    using Eco.Gameplay.Blocks;
    using Eco.Shared.Localization;

    public partial class FencesFormGroup : FormGroup
    {
        public override string Name => "Fences"; //noloc
        public override LocString DisplayName => Localizer.DoStr("Fences");
        public override LocString DisplayDescription => Localizer.DoStr("Fences");
        public override int SortOrder => 8;
    }
}
