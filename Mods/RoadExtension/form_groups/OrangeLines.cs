namespace Eco.Mods.TechTree
{
    using System;
    using Eco.Gameplay.Blocks;
    using Eco.Shared.Localization;

    public partial class RoadExtOrangeLinesFormGroup : FormGroup
    {
        public override string Name => "RoadExtOrangeLines"; //noloc
        public override LocString DisplayName => Localizer.DoStr("Orange Lines");
        public override LocString DisplayDescription => Localizer.DoStr("Orange Lines");
        public override int SortOrder => 11;
    }
}
