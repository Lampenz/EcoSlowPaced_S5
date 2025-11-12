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

    public partial class RoadExtWhiteDiagonal2LFormType : FormType
    {
        public override string Name => "RoadExtWhiteDiagonal2L";
        public override LocString DisplayName => Localizer.DoStr("White Diagonal-2-left");
        public override LocString DisplayDescription => Localizer.DoStr("White Diagonal-2-left");
        public override Type GroupType => typeof(RoadExtWhiteLineMarkingsFormGroup);
        public override int SortOrder => 11;
        public override int MinTier => 1;
    }
    public partial class RoadExtWhiteDiagonal2LCornerFormType : FormType
    {
        public override string Name => "RoadExtWhiteDiagonal2LCorner";
        public override LocString DisplayName => Localizer.DoStr("White Diagonal-2-left Corner");
        public override LocString DisplayDescription => Localizer.DoStr("White Diagonal-2-left Corner");
        public override Type GroupType => typeof(RoadExtWhiteLineMarkingsFormGroup);
        public override int SortOrder => 13;
        public override int MinTier => 1;
    }
    public partial class RoadExtWhiteDiagonal2LEndFormType : FormType
    {
        public override string Name => "RoadExtWhiteDiagonal2LEnd";
        public override LocString DisplayName => Localizer.DoStr("White Diagonal-2-left End");
        public override LocString DisplayDescription => Localizer.DoStr("White Diagonal-2-left End");
        public override Type GroupType => typeof(RoadExtWhiteLineMarkingsFormGroup);
        public override int SortOrder => 14;
        public override int MinTier => 1;
    }
    public partial class RoadExtWhiteDiagonal2LModLineFormType : FormType
    {
        public override string Name => "RoadExtWhiteDiagonal2LModLine";
        public override LocString DisplayName => Localizer.DoStr("White Diagonal-2-left Modified Line");
        public override LocString DisplayDescription => Localizer.DoStr("White Diagonal-2-left Modified Line");
        public override Type GroupType => typeof(RoadExtWhiteLineMarkingsFormGroup);
        public override int SortOrder => 15;
        public override int MinTier => 1;
    }
}
