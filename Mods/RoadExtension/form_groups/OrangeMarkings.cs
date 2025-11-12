namespace Eco.Mods.TechTree
{
    using System;
    using Eco.Gameplay.Blocks;
    using Eco.Shared.Localization;

    public partial class RoadExtOrangeMarkingsFormGroup : FormGroup
    {
        public override string Name => "RoadExtOrangeMarkings"; //noloc
        public override LocString DisplayName => Localizer.DoStr("Orange Markings");
        public override LocString DisplayDescription => Localizer.DoStr("Orange Markings");
        public override int SortOrder => 13;
    }
}
