namespace Eco.Mods.TechTree
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using Eco.Gameplay.Blocks;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Objects;
    using Eco.Gameplay.Players;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Shared.Serialization;
    using Eco.Shared.Localization;
    using Eco.Shared.Utils;
    using Eco.World;
    using Eco.World.Blocks;
    using Eco.Gameplay.Pipes;

    public partial class RoadExtControllerCubeFormType : FormType
    {
        public override string Name => "RoadExtControllerCube";
        public override LocString DisplayName => Localizer.DoStr("Controller (Cube)");
        public override LocString DisplayDescription => Localizer.DoStr("Controller (Cube)");
        public override Type GroupType => typeof(RoadExtShapeControllersFormGroup);
        public override int SortOrder => 1;
        public override int MinTier => 1;
    }
    public partial class RoadExtControllerRampAFormType : FormType
    {
        public override string Name => "RoadExtControllerRampA";
        public override LocString DisplayName => Localizer.DoStr("Controller (Ramp A)");
        public override LocString DisplayDescription => Localizer.DoStr("Controller (Ramp A) - Place below marker/line block to overwrite shape");
        public override Type GroupType => typeof(RoadExtShapeControllersFormGroup);
        public override int SortOrder => 2;
        public override int MinTier => 1;
    }
    public partial class RoadExtControllerRampBFormType : FormType
    {
        public override string Name => "RoadExtControllerRampB";
        public override LocString DisplayName => Localizer.DoStr("Controller (Ramp B)");
        public override LocString DisplayDescription => Localizer.DoStr("Controller (Ramp B) - Place below marker/line block to overwrite shape");
        public override Type GroupType => typeof(RoadExtShapeControllersFormGroup);
        public override int SortOrder => 3;
        public override int MinTier => 1;
    }
    public partial class RoadExtControllerRampCFormType : FormType
    {
        public override string Name => "RoadExtControllerRampC";
        public override LocString DisplayName => Localizer.DoStr("Controller (Ramp C)");
        public override LocString DisplayDescription => Localizer.DoStr("Controller (Ramp C) - Place below marker/line block to overwrite shape");
        public override Type GroupType => typeof(RoadExtShapeControllersFormGroup);
        public override int SortOrder => 4;
        public override int MinTier => 1;
    }
    public partial class RoadExtControllerRampDFormType : FormType
    {
        public override string Name => "RoadExtControllerRampD";
        public override LocString DisplayName => Localizer.DoStr("Controller (Ramp D)");
        public override LocString DisplayDescription => Localizer.DoStr("Controller (Ramp D) - Place below marker/line block to overwrite shape");
        public override Type GroupType => typeof(RoadExtShapeControllersFormGroup);
        public override int SortOrder => 5;
        public override int MinTier => 1;
    }
}
