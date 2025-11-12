namespace Eco.Mods.TechTree
{
    using System;
    using Eco.Gameplay.Blocks;
    using Eco.Shared.Localization;

    public partial class RoadExtShapeControllersFormGroup : FormGroup
    {
        public override string Name => "RoadExtShapeControllers"; //noloc
        public override LocString DisplayName => Localizer.DoStr("Shape Controllers");
        public override LocString DisplayDescription => Localizer.DoStr("Shape Controllers");
        public override int SortOrder => 40;
    }
}
