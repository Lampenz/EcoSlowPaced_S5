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

    public partial class RoadExtOrangeCornerFormType : FormType
    {
        public override string Name => "RoadExtOrangeCorner";
        public override LocString DisplayName => Localizer.DoStr("Orange Corner");
        public override LocString DisplayDescription => Localizer.DoStr("Orange Corner");
        public override Type GroupType => typeof(RoadExtOrangeLineMarkingsFormGroup);
        public override int SortOrder => 1;
        public override int MinTier => 1;
    }

    public partial class RoadExtOrangeCornerSmallFormType : FormType
    {
        public override string Name => "RoadExtOrangeCornerSmall";
        public override LocString DisplayName => Localizer.DoStr("Orange Corner Small");
        public override LocString DisplayDescription => Localizer.DoStr("Orange Corner Small");
        public override Type GroupType => typeof(RoadExtOrangeLineMarkingsFormGroup);
        public override int SortOrder => 2;
        public override int MinTier => 1;
    }
    public partial class RoadExtOrangeDiagonalOffsetFormType : FormType
    {
        public override string Name => "RoadExtOrangeDiagonalOffset";
        public override LocString DisplayName => Localizer.DoStr("Orange Diagonal Offset Line");
        public override LocString DisplayDescription => Localizer.DoStr("Orange Diagonal Offset Line");
        public override Type GroupType => typeof(RoadExtOrangeLineMarkingsFormGroup);
        public override int SortOrder => 3;
        public override int MinTier => 1;
    }
    public partial class RoadExtOrangeDiagonalBigFormType : FormType
    {
        public override string Name => "RoadExtOrangeDiagonalBig";
        public override LocString DisplayName => Localizer.DoStr("Orange Diagonal Big Line");
        public override LocString DisplayDescription => Localizer.DoStr("Orange Diagonal Big Line");
        public override Type GroupType => typeof(RoadExtOrangeLineMarkingsFormGroup);
        public override int SortOrder => 4;
        public override int MinTier => 1;
    }
    public partial class RoadExtOrangeDiagonalFormType : FormType
    {
        public override string Name => "RoadExtOrangeDiagonal";
        public override LocString DisplayName => Localizer.DoStr("Orange Diagonal");
        public override LocString DisplayDescription => Localizer.DoStr("Orange Diagonal");
        public override Type GroupType => typeof(RoadExtOrangeLineMarkingsFormGroup);
        public override int SortOrder => 5;
        public override int MinTier => 1;
    }
    public partial class RoadExtOrangeDiagonalEndLFormType : FormType
    {
        public override string Name => "RoadExtOrangeDiagonalEndL";
        public override LocString DisplayName => Localizer.DoStr("Orange Diagonal End Left");
        public override LocString DisplayDescription => Localizer.DoStr("Orange Diagonal End Left");
        public override Type GroupType => typeof(RoadExtOrangeLineMarkingsFormGroup);
        public override int SortOrder => 6;
        public override int MinTier => 1;
    }
    public partial class RoadExtOrangeDiagonalEndRFormType : FormType
    {
        public override string Name => "RoadExtOrangeDiagonalEndR";
        public override LocString DisplayName => Localizer.DoStr("Orange Diagonal End Right");
        public override LocString DisplayDescription => Localizer.DoStr("Orange Diagonal End Right");
        public override Type GroupType => typeof(RoadExtOrangeLineMarkingsFormGroup);
        public override int SortOrder => 7;
        public override int MinTier => 1;
    }
}
