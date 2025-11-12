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

    public partial class RoadExtWhiteCornerFormType : FormType
    {
        public override string Name => "RoadExtWhiteCorner";
        public override LocString DisplayName => Localizer.DoStr("White Corner");
        public override LocString DisplayDescription => Localizer.DoStr("White Corner");
        public override Type GroupType => typeof(RoadExtWhiteLineMarkingsFormGroup);
        public override int SortOrder => 1;
        public override int MinTier => 1;
    }

    public partial class RoadExtWhiteCornerSmallFormType : FormType
    {
        public override string Name => "RoadExtWhiteCornerSmall";
        public override LocString DisplayName => Localizer.DoStr("White Corner Small");
        public override LocString DisplayDescription => Localizer.DoStr("White Corner Small");
        public override Type GroupType => typeof(RoadExtWhiteLineMarkingsFormGroup);
        public override int SortOrder => 2;
        public override int MinTier => 1;
    }
    public partial class RoadExtWhiteDiagonalOffsetFormType : FormType
    {
        public override string Name => "RoadExtWhiteDiagonalOffset";
        public override LocString DisplayName => Localizer.DoStr("White Diagonal Offset Line");
        public override LocString DisplayDescription => Localizer.DoStr("White Diagonal Offset Line");
        public override Type GroupType => typeof(RoadExtWhiteLineMarkingsFormGroup);
        public override int SortOrder => 3;
        public override int MinTier => 1;
    }
    public partial class RoadExtWhiteDiagonalBigFormType : FormType
    {
        public override string Name => "RoadExtWhiteDiagonalBig";
        public override LocString DisplayName => Localizer.DoStr("White Diagonal Big Line");
        public override LocString DisplayDescription => Localizer.DoStr("White Diagonal Big Line");
        public override Type GroupType => typeof(RoadExtWhiteLineMarkingsFormGroup);
        public override int SortOrder => 4;
        public override int MinTier => 1;
    }
    public partial class RoadExtWhiteDiagonalFormType : FormType
    {
        public override string Name => "RoadExtWhiteDiagonal";
        public override LocString DisplayName => Localizer.DoStr("White Diagonal");
        public override LocString DisplayDescription => Localizer.DoStr("White Diagonal");
        public override Type GroupType => typeof(RoadExtWhiteLineMarkingsFormGroup);
        public override int SortOrder => 5;
        public override int MinTier => 1;
    }
    public partial class RoadExtWhiteDiagonalEndLFormType : FormType
    {
        public override string Name => "RoadExtWhiteDiagonalEndL";
        public override LocString DisplayName => Localizer.DoStr("White Diagonal End Left");
        public override LocString DisplayDescription => Localizer.DoStr("White Diagonal End Left");
        public override Type GroupType => typeof(RoadExtWhiteLineMarkingsFormGroup);
        public override int SortOrder => 6;
        public override int MinTier => 1;
    }
    public partial class RoadExtWhiteDiagonalEndRFormType : FormType
    {
        public override string Name => "RoadExtWhiteDiagonalEndR";
        public override LocString DisplayName => Localizer.DoStr("White Diagonal End Right");
        public override LocString DisplayDescription => Localizer.DoStr("White Diagonal End Right");
        public override Type GroupType => typeof(RoadExtWhiteLineMarkingsFormGroup);
        public override int SortOrder => 7;
        public override int MinTier => 1;
    }
}
