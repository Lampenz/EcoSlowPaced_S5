namespace Eco.Mods.TechTree
{
    using System;
    using Eco.Gameplay.Blocks;
    using Eco.Shared.Localization;

    public partial class RoadExtWhiteLinesFormGroup : FormGroup
    {
        public override string Name => "RoadExtWhiteLines"; //noloc
        public override LocString DisplayName => Localizer.DoStr("White Lines");
        public override LocString DisplayDescription => Localizer.DoStr("White Lines");
        public override int SortOrder => 8;
    }
}
