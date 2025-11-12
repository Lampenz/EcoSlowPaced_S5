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

    // Left Line Ramp Form Type
    [NextRamp(typeof(RampWhiteEdgeLBFormType), 0)]
    public partial class RampWhiteEdgeLAFormType : FormType
    {
        public override string Name => "RampWhiteEdgeLA";
        public override LocString DisplayName => Localizer.DoStr("White Ramp Edge Line Left A");
        public override LocString DisplayDescription => Localizer.DoStr("White Ramp Edge Line Left A");
        public override Type GroupType => typeof(RoadExtWhiteLinesFormGroup);
        public override int SortOrder => 1;
        public override int MinTier => 1;
    }
    [NextRamp(typeof(RampWhiteEdgeLCFormType), 0)]
    public partial class RampWhiteEdgeLBFormType : FormType
    {
        public override string Name => "RampWhiteEdgeLB";
        public override LocString DisplayName => Localizer.DoStr("White Ramp Edge Line Left B");
        public override LocString DisplayDescription => Localizer.DoStr("White Ramp Edge Line Left B");
        public override Type GroupType => typeof(RoadExtWhiteLinesFormGroup);
        public override int SortOrder => 2;
        public override int MinTier => 1;
    }
    [NextRamp(typeof(RampWhiteEdgeLDFormType), 0)]
    public partial class RampWhiteEdgeLCFormType : FormType
    {
        public override string Name => "RampWhiteEdgeLC";
        public override LocString DisplayName => Localizer.DoStr("White Ramp Edge Line Left C");
        public override LocString DisplayDescription => Localizer.DoStr("White Ramp Edge Line Left C");
        public override Type GroupType => typeof(RoadExtWhiteLinesFormGroup);
        public override int SortOrder => 3;
        public override int MinTier => 1;
    }
    [NextRamp(typeof(RampWhiteEdgeLAFormType), 1)]
    public partial class RampWhiteEdgeLDFormType : FormType
    {
        public override string Name => "RampWhiteEdgeLD";
        public override LocString DisplayName => Localizer.DoStr("White Ramp Edge Line Left D");
        public override LocString DisplayDescription => Localizer.DoStr("White Ramp Edge Line Left D");
        public override Type GroupType => typeof(RoadExtWhiteLinesFormGroup);
        public override int SortOrder => 4;
        public override int MinTier => 1;
    }
	
	
	
    // Right Line Ramp Form Type
    [NextRamp(typeof(RampWhiteEdgeRBFormType), 0)]
    public partial class RampWhiteEdgeRAFormType : FormType
    {
        public override string Name => "RampWhiteEdgeRA";
        public override LocString DisplayName => Localizer.DoStr("White Ramp Edge Line Right A");
        public override LocString DisplayDescription => Localizer.DoStr("White Ramp Edge Line Right A");
        public override Type GroupType => typeof(RoadExtWhiteLinesFormGroup);
        public override int SortOrder => 5;
        public override int MinTier => 1;
    }
    [NextRamp(typeof(RampWhiteEdgeRCFormType), 0)]
    public partial class RampWhiteEdgeRBFormType : FormType
    {
        public override string Name => "RampWhiteEdgeRB";
        public override LocString DisplayName => Localizer.DoStr("White Ramp Edge Line Right B");
        public override LocString DisplayDescription => Localizer.DoStr("White Ramp Edge Line Right B");
        public override Type GroupType => typeof(RoadExtWhiteLinesFormGroup);
        public override int SortOrder => 6;
        public override int MinTier => 1;
    }
    [NextRamp(typeof(RampWhiteEdgeRDFormType), 0)]
    public partial class RampWhiteEdgeRCFormType : FormType
    {
        public override string Name => "RampWhiteEdgeRC";
        public override LocString DisplayName => Localizer.DoStr("White Ramp Edge Line Right C");
        public override LocString DisplayDescription => Localizer.DoStr("White Ramp Edge Line Right C");
        public override Type GroupType => typeof(RoadExtWhiteLinesFormGroup);
        public override int SortOrder => 7;
        public override int MinTier => 1;
    }
    [NextRamp(typeof(RampWhiteEdgeRAFormType), 1)]
    public partial class RampWhiteEdgeRDFormType : FormType
    {
        public override string Name => "RampWhiteEdgeRD";
        public override LocString DisplayName => Localizer.DoStr("White Ramp Edge Line Right D");
        public override LocString DisplayDescription => Localizer.DoStr("White Ramp Edge Line Right D");
        public override Type GroupType => typeof(RoadExtWhiteLinesFormGroup);
        public override int SortOrder => 8;
        public override int MinTier => 1;
    }
}
