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

    public partial class RoadExtWhiteDiagonal2RFormType : FormType
    {
        public override string Name => "RoadExtWhiteDiagonal2R";
        public override LocString DisplayName => Localizer.DoStr("White Diagonal-2-right");
        public override LocString DisplayDescription => Localizer.DoStr("White Diagonal-2-right");
        public override Type GroupType => typeof(RoadExtWhiteLineMarkingsFormGroup);
        public override int SortOrder => 21;
        public override int MinTier => 1;
    }
    public partial class RoadExtWhiteDiagonal2RCornerFormType : FormType
    {
        public override string Name => "RoadExtWhiteDiagonal2RCorner";
        public override LocString DisplayName => Localizer.DoStr("White Diagonal-2-right Corner");
        public override LocString DisplayDescription => Localizer.DoStr("White Diagonal-2-right Corner");
        public override Type GroupType => typeof(RoadExtWhiteLineMarkingsFormGroup);
        public override int SortOrder => 23;
        public override int MinTier => 1;
    }
    public partial class RoadExtWhiteDiagonal2REndFormType : FormType
    {
        public override string Name => "RoadExtWhiteDiagonal2REnd";
        public override LocString DisplayName => Localizer.DoStr("White Diagonal-2-right End");
        public override LocString DisplayDescription => Localizer.DoStr("White Diagonal-2-right End");
        public override Type GroupType => typeof(RoadExtWhiteLineMarkingsFormGroup);
        public override int SortOrder => 24;
        public override int MinTier => 1;
    }
    public partial class RoadExtWhiteDiagonal2RModLineFormType : FormType
    {
        public override string Name => "RoadExtWhiteDiagonal2RModLine";
        public override LocString DisplayName => Localizer.DoStr("White Diagonal-2-right Modified Line");
        public override LocString DisplayDescription => Localizer.DoStr("White Diagonal-2-right Modified Line");
        public override Type GroupType => typeof(RoadExtWhiteLineMarkingsFormGroup);
        public override int SortOrder => 25;
        public override int MinTier => 1;
    }
}
