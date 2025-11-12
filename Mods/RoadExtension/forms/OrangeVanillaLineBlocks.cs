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

    public partial class RoadExtOrangeEdgeRotateFormType : FormType
    {
        public override string Name => "RoadExtOrangeEdgeRotate";
        public override LocString DisplayName => Localizer.DoStr("Orange Edge Rotate");
        public override LocString DisplayDescription => Localizer.DoStr("Orange Edge Rotate");
        public override Type GroupType => typeof(RoadExtOrangeLinesFormGroup);
        public override int SortOrder => 6;
        public override int MinTier => 1;
    }
    public partial class RoadExtOrangeTwoEdgeRotateFormType : FormType
    {
        public override string Name => "RoadExtOrangeTwoEdgeRotate";
        public override LocString DisplayName => Localizer.DoStr("Orange Two Edge Rotate");
        public override LocString DisplayDescription => Localizer.DoStr("Orange Two Edge Rotate");
        public override Type GroupType => typeof(RoadExtOrangeLinesFormGroup);
        public override int SortOrder => 5;
        public override int MinTier => 1;
    }
    public partial class RoadExtOrangeCubeFormType : FormType
    {
        public override string Name => "RoadExtOrangeCube";
        public override LocString DisplayName => Localizer.DoStr("Orange Cube");
        public override LocString DisplayDescription => Localizer.DoStr("Orange Cube");
        public override Type GroupType => typeof(RoadExtOrangeLinesFormGroup);
        public override int SortOrder => 1;
        public override int MinTier => 1;
    }
}
