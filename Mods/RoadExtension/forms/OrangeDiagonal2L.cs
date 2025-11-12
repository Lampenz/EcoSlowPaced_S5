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

    public partial class RoadExtOrangeDiagonal2LFormType : FormType
    {
        public override string Name => "RoadExtOrangeDiagonal2L";
        public override LocString DisplayName => Localizer.DoStr("Orange Diagonal-2-left");
        public override LocString DisplayDescription => Localizer.DoStr("Orange Diagonal-2-left");
        public override Type GroupType => typeof(RoadExtOrangeLineMarkingsFormGroup);
        public override int SortOrder => 11;
        public override int MinTier => 1;
    }
    public partial class RoadExtOrangeDiagonal2LCornerFormType : FormType
    {
        public override string Name => "RoadExtOrangeDiagonal2LCorner";
        public override LocString DisplayName => Localizer.DoStr("Orange Diagonal-2-left Corner");
        public override LocString DisplayDescription => Localizer.DoStr("Orange Diagonal-2-left Corner");
        public override Type GroupType => typeof(RoadExtOrangeLineMarkingsFormGroup);
        public override int SortOrder => 13;
        public override int MinTier => 1;
    }
    public partial class RoadExtOrangeDiagonal2LEndFormType : FormType
    {
        public override string Name => "RoadExtOrangeDiagonal2LEnd";
        public override LocString DisplayName => Localizer.DoStr("Orange Diagonal-2-left End");
        public override LocString DisplayDescription => Localizer.DoStr("Orange Diagonal-2-left End");
        public override Type GroupType => typeof(RoadExtOrangeLineMarkingsFormGroup);
        public override int SortOrder => 14;
        public override int MinTier => 1;
    }
    public partial class RoadExtOrangeDiagonal2LModLineFormType : FormType
    {
        public override string Name => "RoadExtOrangeDiagonal2LModLine";
        public override LocString DisplayName => Localizer.DoStr("Orange Diagonal-2-left Modified Line");
        public override LocString DisplayDescription => Localizer.DoStr("Orange Diagonal-2-left Modified Line");
        public override Type GroupType => typeof(RoadExtOrangeLineMarkingsFormGroup);
        public override int SortOrder => 15;
        public override int MinTier => 1;
    }
}
