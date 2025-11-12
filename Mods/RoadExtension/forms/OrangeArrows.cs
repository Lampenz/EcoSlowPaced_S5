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

    public partial class RoadExtOrangeArrowLFormType : FormType
    {
        public override string Name => "RoadExtOrangeArrowL";
        public override LocString DisplayName => Localizer.DoStr("Orange Arrow Left");
        public override LocString DisplayDescription => Localizer.DoStr("Orange Arrow Left");
        public override Type GroupType => typeof(RoadExtOrangeMarkingsFormGroup);
        public override int SortOrder => 1;
        public override int MinTier => 1;
    }

    public partial class RoadExtOrangeArrowRFormType : FormType
    {
        public override string Name => "RoadExtOrangeArrowR";
        public override LocString DisplayName => Localizer.DoStr("Orange Arrow Right");
        public override LocString DisplayDescription => Localizer.DoStr("Orange Arrow Right");
        public override Type GroupType => typeof(RoadExtOrangeMarkingsFormGroup);
        public override int SortOrder => 2;
        public override int MinTier => 1;
    }
    public partial class RoadExtOrangeArrowLRFormType : FormType
    {
        public override string Name => "RoadExtOrangeArrowLR";
        public override LocString DisplayName => Localizer.DoStr("Orange Arrow Left-Right");
        public override LocString DisplayDescription => Localizer.DoStr("Orange Arrow Left-Right");
        public override Type GroupType => typeof(RoadExtOrangeMarkingsFormGroup);
        public override int SortOrder => 3;
        public override int MinTier => 1;
    }
    public partial class RoadExtOrangeArrowSLFormType : FormType
    {
        public override string Name => "RoadExtOrangeArrowSL";
        public override LocString DisplayName => Localizer.DoStr("Orange Arrow Straight-Left");
        public override LocString DisplayDescription => Localizer.DoStr("Orange Arrow Straight-Left");
        public override Type GroupType => typeof(RoadExtOrangeMarkingsFormGroup);
        public override int SortOrder => 4;
        public override int MinTier => 1;
    }
    public partial class RoadExtOrangeArrowSRFormType : FormType
    {
        public override string Name => "RoadExtOrangeArrowSR";
        public override LocString DisplayName => Localizer.DoStr("Orange Arrow Straight-Right");
        public override LocString DisplayDescription => Localizer.DoStr("Orange Arrow Straight-Right");
        public override Type GroupType => typeof(RoadExtOrangeMarkingsFormGroup);
        public override int SortOrder => 5;
        public override int MinTier => 1;
    }
    public partial class RoadExtOrangeArrowSLRFormType : FormType
    {
        public override string Name => "RoadExtOrangeArrowSLR";
        public override LocString DisplayName => Localizer.DoStr("Orange Arrow Straight-Left-Right");
        public override LocString DisplayDescription => Localizer.DoStr("Orange Arrow Straight-Left-Right");
        public override Type GroupType => typeof(RoadExtOrangeMarkingsFormGroup);
        public override int SortOrder => 6;
        public override int MinTier => 1;
    }
    public partial class RoadExtOrangeArrowSLineFormType : FormType
    {
        public override string Name => "RoadExtOrangeArrowSLine";
        public override LocString DisplayName => Localizer.DoStr("Orange Arrow Straight-Line");
        public override LocString DisplayDescription => Localizer.DoStr("Orange Arrow Straight-Line");
        public override Type GroupType => typeof(RoadExtOrangeMarkingsFormGroup);
        public override int SortOrder => 7;
        public override int MinTier => 1;
    }
    public partial class RoadExtOrangeArrowSTipFormType : FormType
    {
        public override string Name => "RoadExtOrangeArrowSTip";
        public override LocString DisplayName => Localizer.DoStr("Orange Arrow Straight-Tip");
        public override LocString DisplayDescription => Localizer.DoStr("Orange Arrow Straight-Tip");
        public override Type GroupType => typeof(RoadExtOrangeMarkingsFormGroup);
        public override int SortOrder => 8;
        public override int MinTier => 1;
    }
}
