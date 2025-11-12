namespace Eco.Mods.TechTree
{
    using System;
    using Eco.Gameplay.Blocks;
    using Eco.Shared.Localization;

    public partial class RoadExtWhiteLineMarkingsFormGroup : FormGroup
    {
        public override string Name => "RoadExtWhiteLineMarkings"; //noloc
        public override LocString DisplayName => Localizer.DoStr("White Line Markings");
        public override LocString DisplayDescription => Localizer.DoStr("White Line Markings");
        public override int SortOrder => 9;
    }
}
