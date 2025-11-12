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
    [NextRamp(typeof(RampOrangeEdgeLBFormType), 0)]
    public partial class RampOrangeEdgeLAFormType : FormType
    {
        public override string Name => "RampOrangeEdgeLA";
        public override LocString DisplayName => Localizer.DoStr("Orange Ramp Edge Line Left A");
        public override LocString DisplayDescription => Localizer.DoStr("Orange Ramp Edge Line Left A");
        public override Type GroupType => typeof(RoadExtOrangeLinesFormGroup);
        public override int SortOrder => 21;
        public override int MinTier => 1;
    }
    [NextRamp(typeof(RampOrangeEdgeLCFormType), 0)]
    public partial class RampOrangeEdgeLBFormType : FormType
    {
        public override string Name => "RampOrangeEdgeLB";
        public override LocString DisplayName => Localizer.DoStr("Orange Ramp Edge Line Left B");
        public override LocString DisplayDescription => Localizer.DoStr("Orange Ramp Edge Line Left B");
        public override Type GroupType => typeof(RoadExtOrangeLinesFormGroup);
        public override int SortOrder => 22;
        public override int MinTier => 1;
    }
    [NextRamp(typeof(RampOrangeEdgeLDFormType), 0)]
    public partial class RampOrangeEdgeLCFormType : FormType
    {
        public override string Name => "RampOrangeEdgeLC";
        public override LocString DisplayName => Localizer.DoStr("Orange Ramp Edge Line Left C");
        public override LocString DisplayDescription => Localizer.DoStr("Orange Ramp Edge Line Left C");
        public override Type GroupType => typeof(RoadExtOrangeLinesFormGroup);
        public override int SortOrder => 23;
        public override int MinTier => 1;
    }
    [NextRamp(typeof(RampOrangeEdgeLAFormType), 1)]
    public partial class RampOrangeEdgeLDFormType : FormType
    {
        public override string Name => "RampOrangeEdgeLD";
        public override LocString DisplayName => Localizer.DoStr("Orange Ramp Edge Line Left D");
        public override LocString DisplayDescription => Localizer.DoStr("Orange Ramp Edge Line Left D");
        public override Type GroupType => typeof(RoadExtOrangeLinesFormGroup);
        public override int SortOrder => 24;
        public override int MinTier => 1;
    }



    // Right Line Ramp Form Type
    [NextRamp(typeof(RampOrangeEdgeRBFormType), 0)]
    public partial class RampOrangeEdgeRAFormType : FormType
    {
        public override string Name => "RampOrangeEdgeRA";
        public override LocString DisplayName => Localizer.DoStr("Orange Ramp Edge Line Right A");
        public override LocString DisplayDescription => Localizer.DoStr("Orange Ramp Edge Line Right A");
        public override Type GroupType => typeof(RoadExtOrangeLinesFormGroup);
        public override int SortOrder => 25;
        public override int MinTier => 1;
    }
    [NextRamp(typeof(RampOrangeEdgeRCFormType), 0)]
    public partial class RampOrangeEdgeRBFormType : FormType
    {
        public override string Name => "RampOrangeEdgeRB";
        public override LocString DisplayName => Localizer.DoStr("Orange Ramp Edge Line Right B");
        public override LocString DisplayDescription => Localizer.DoStr("Orange Ramp Edge Line Right B");
        public override Type GroupType => typeof(RoadExtOrangeLinesFormGroup);
        public override int SortOrder => 26;
        public override int MinTier => 1;
    }
    [NextRamp(typeof(RampOrangeEdgeRDFormType), 0)]
    public partial class RampOrangeEdgeRCFormType : FormType
    {
        public override string Name => "RampOrangeEdgeRC";
        public override LocString DisplayName => Localizer.DoStr("Orange Ramp Edge Line Right C");
        public override LocString DisplayDescription => Localizer.DoStr("Orange Ramp Edge Line Right C");
        public override Type GroupType => typeof(RoadExtOrangeLinesFormGroup);
        public override int SortOrder => 27;
        public override int MinTier => 1;
    }
    [NextRamp(typeof(RampOrangeEdgeRAFormType), 1)]
    public partial class RampOrangeEdgeRDFormType : FormType
    {
        public override string Name => "RampOrangeEdgeRD";
        public override LocString DisplayName => Localizer.DoStr("Orange Ramp Edge Line Right D");
        public override LocString DisplayDescription => Localizer.DoStr("Orange Ramp Edge Line Right D");
        public override Type GroupType => typeof(RoadExtOrangeLinesFormGroup);
        public override int SortOrder => 28;
        public override int MinTier => 1;
    }
}
