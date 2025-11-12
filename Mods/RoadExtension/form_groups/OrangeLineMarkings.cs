namespace Eco.Mods.TechTree
{
    using System;
    using Eco.Gameplay.Blocks;
    using Eco.Shared.Localization;

    public partial class RoadExtOrangeLineMarkingsFormGroup : FormGroup
    {
        public override string Name => "RoadExtOrangeLineMarkings"; //noloc
        public override LocString DisplayName => Localizer.DoStr("Orange Line Markings");
        public override LocString DisplayDescription => Localizer.DoStr("Orange Line Markings");
        public override int SortOrder => 12;
    }
}
