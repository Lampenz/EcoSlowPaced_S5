namespace Eco.Mods.TechTree
{
    using System;
    using Eco.Gameplay.Blocks;
    using Eco.Shared.Localization;

    public partial class RoadExtWhiteMarkingsFormGroup : FormGroup
    {
        public override string Name => "RoadExtWhiteMarkings"; //noloc
        public override LocString DisplayName => Localizer.DoStr("White Markings");
        public override LocString DisplayDescription => Localizer.DoStr("White Markings");
        public override int SortOrder => 10;
    }
}
