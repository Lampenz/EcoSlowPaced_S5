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

    public partial class RoadExtWhiteArrowLFormType : FormType
    {
        public override string Name => "RoadExtWhiteArrowL";
        public override LocString DisplayName => Localizer.DoStr("White Arrow Left");
        public override LocString DisplayDescription => Localizer.DoStr("White Arrow Left");
        public override Type GroupType => typeof(RoadExtWhiteMarkingsFormGroup);
        public override int SortOrder => 1;
        public override int MinTier => 1;
    }

    public partial class RoadExtWhiteArrowRFormType : FormType
    {
        public override string Name => "RoadExtWhiteArrowR";
        public override LocString DisplayName => Localizer.DoStr("White Arrow Right");
        public override LocString DisplayDescription => Localizer.DoStr("White Arrow Right");
        public override Type GroupType => typeof(RoadExtWhiteMarkingsFormGroup);
        public override int SortOrder => 2;
        public override int MinTier => 1;
    }
    public partial class RoadExtWhiteArrowLRFormType : FormType
    {
        public override string Name => "RoadExtWhiteArrowLR";
        public override LocString DisplayName => Localizer.DoStr("White Arrow Left-Right");
        public override LocString DisplayDescription => Localizer.DoStr("White Arrow Left-Right");
        public override Type GroupType => typeof(RoadExtWhiteMarkingsFormGroup);
        public override int SortOrder => 3;
        public override int MinTier => 1;
    }
    public partial class RoadExtWhiteArrowSLFormType : FormType
    {
        public override string Name => "RoadExtWhiteArrowSL";
        public override LocString DisplayName => Localizer.DoStr("White Arrow Straight-Left");
        public override LocString DisplayDescription => Localizer.DoStr("White Arrow Straight-Left");
        public override Type GroupType => typeof(RoadExtWhiteMarkingsFormGroup);
        public override int SortOrder => 4;
        public override int MinTier => 1;
    }
    public partial class RoadExtWhiteArrowSRFormType : FormType
    {
        public override string Name => "RoadExtWhiteArrowSR";
        public override LocString DisplayName => Localizer.DoStr("White Arrow Straight-Right");
        public override LocString DisplayDescription => Localizer.DoStr("White Arrow Straight-Right");
        public override Type GroupType => typeof(RoadExtWhiteMarkingsFormGroup);
        public override int SortOrder => 5;
        public override int MinTier => 1;
    }
    public partial class RoadExtWhiteArrowSLRFormType : FormType
    {
        public override string Name => "RoadExtWhiteArrowSLR";
        public override LocString DisplayName => Localizer.DoStr("White Arrow Straight-Left-Right");
        public override LocString DisplayDescription => Localizer.DoStr("White Arrow Straight-Left-Right");
        public override Type GroupType => typeof(RoadExtWhiteMarkingsFormGroup);
        public override int SortOrder => 6;
        public override int MinTier => 1;
    }
    public partial class RoadExtWhiteArrowSLineFormType : FormType
    {
        public override string Name => "RoadExtWhiteArrowSLine";
        public override LocString DisplayName => Localizer.DoStr("White Arrow Straight-Line");
        public override LocString DisplayDescription => Localizer.DoStr("White Arrow Straight-Line");
        public override Type GroupType => typeof(RoadExtWhiteMarkingsFormGroup);
        public override int SortOrder => 7;
        public override int MinTier => 1;
    }
    public partial class RoadExtWhiteArrowSTipFormType : FormType
    {
        public override string Name => "RoadExtWhiteArrowSTip";
        public override LocString DisplayName => Localizer.DoStr("White Arrow Straight-Tip");
        public override LocString DisplayDescription => Localizer.DoStr("White Arrow Straight-Tip");
        public override Type GroupType => typeof(RoadExtWhiteMarkingsFormGroup);
        public override int SortOrder => 8;
        public override int MinTier => 1;
    }
}
