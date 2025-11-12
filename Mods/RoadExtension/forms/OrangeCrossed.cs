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

    public partial class RoadExtOrangeCrossedFormType : FormType
    {
        public override string Name => "RoadExtOrangeCrossed";
        public override LocString DisplayName => Localizer.DoStr("Orange Crossed Area");
        public override LocString DisplayDescription => Localizer.DoStr("Orange Crossed Area");
        public override Type GroupType => typeof(RoadExtOrangeMarkingsFormGroup);
        public override int SortOrder => 11;
        public override int MinTier => 1;
    }

    public partial class RoadExtOrangeCrossedLineFormType : FormType
    {
        public override string Name => "RoadExtOrangeCrossedLine";
        public override LocString DisplayName => Localizer.DoStr("Orange Crossed Area With Line");
        public override LocString DisplayDescription => Localizer.DoStr("Orange Crossed Area With Line");
        public override Type GroupType => typeof(RoadExtOrangeMarkingsFormGroup);
        public override int SortOrder => 12;
        public override int MinTier => 1;
    }

    public partial class RoadExtOrangeCrossedCornerFormType : FormType
    {
        public override string Name => "RoadExtOrangeCrossedCorner";
        public override LocString DisplayName => Localizer.DoStr("Orange Crossed Area With Corner Line");
        public override LocString DisplayDescription => Localizer.DoStr("Orange Crossed Area With Corner Line");
        public override Type GroupType => typeof(RoadExtOrangeMarkingsFormGroup);
        public override int SortOrder => 13;
        public override int MinTier => 1;
    }

    public partial class RoadExtOrangeCrossed90FormType : FormType
    {
        public override string Name => "RoadExtOrangeCrossed90";
        public override LocString DisplayName => Localizer.DoStr("Orange Crossed Area (rotated)");
        public override LocString DisplayDescription => Localizer.DoStr("Orange Crossed Area (rotated)");
        public override Type GroupType => typeof(RoadExtOrangeMarkingsFormGroup);
        public override int SortOrder => 14;
        public override int MinTier => 1;
    }

    public partial class RoadExtOrangeCrossed90LineFormType : FormType
    {
        public override string Name => "RoadExtOrangeCrossed90Line";
        public override LocString DisplayName => Localizer.DoStr("Orange Crossed Area With Line (rotated)");
        public override LocString DisplayDescription => Localizer.DoStr("Orange Crossed Area With Line (rotated)");
        public override Type GroupType => typeof(RoadExtOrangeMarkingsFormGroup);
        public override int SortOrder => 15;
        public override int MinTier => 1;
    }

    public partial class RoadExtOrangeCrossed90CornerFormType : FormType
    {
        public override string Name => "RoadExtOrangeCrossed90Corner";
        public override LocString DisplayName => Localizer.DoStr("Orange Crossed Area With Corner Line (rotated)");
        public override LocString DisplayDescription => Localizer.DoStr("Orange Crossed Area With Corner Line (rotated)");
        public override Type GroupType => typeof(RoadExtOrangeMarkingsFormGroup);
        public override int SortOrder => 16;
        public override int MinTier => 1;
    }
}
