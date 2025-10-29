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
    using Eco.Shared.Utils;
    using Eco.World;
	using Eco.World.Water;
    using Eco.World.Blocks;
    using Eco.Gameplay.Pipes;
    using Tag = Eco.Core.Items.TagAttribute;
	
	[Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [BlockTier(2)]
	[Tag("Constructable")]
    [IsForm(typeof(ERCFenceSixFormType), typeof(SoftwoodHewnLogItem))]
    public partial class ERCSofFenceSixBlock :
        Block, IWaterLoggedBlock
    {
        public Type RepresentedItemType { get { return typeof(SoftwoodHewnLogItem); } }
    }
	
	[Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [BlockTier(2)]
	[Tag("Constructable")]
    [IsForm(typeof(ERCFenceFiveFormType), typeof(SoftwoodHewnLogItem))]
    public partial class ERCSofFenceFiveBlock :
        Block, IWaterLoggedBlock
    {
        public Type RepresentedItemType { get { return typeof(SoftwoodHewnLogItem); } }
    }
	
	[Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [BlockTier(2)]
	[Tag("Constructable")]
    [IsForm(typeof(ERCFenceThreeFormType), typeof(SoftwoodHewnLogItem))]
    public partial class ERCSofFenceThreeBlock :
        Block, IWaterLoggedBlock
    {
        public Type RepresentedItemType { get { return typeof(SoftwoodHewnLogItem); } }
    }
	
	[Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [BlockTier(2)]
	[Tag("Constructable")]
    [IsForm(typeof(ERCFenceTwoFormType), typeof(SoftwoodHewnLogItem))]
    public partial class ERCSofFenceTwoBlock :
        Block, IWaterLoggedBlock
    {
        public Type RepresentedItemType { get { return typeof(SoftwoodHewnLogItem); } }
    }
	
	[Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [BlockTier(2)]
	[Tag("Constructable")]
    [IsForm(typeof(ERCFenceCrossFormType), typeof(SoftwoodHewnLogItem))]
    public partial class ERCSofFenceCrossBlock :
        Block, IWaterLoggedBlock
    {
        public Type RepresentedItemType { get { return typeof(SoftwoodHewnLogItem); } }
    }
 
	[RotatedVariants(typeof(ERCSofFenceEndBlock), typeof(ERCSofFenceEnd90Block), typeof(ERCSofFenceEnd180Block), typeof(ERCSofFenceEnd270Block))]
    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
	[BlockTier(2)]
	[Tag("Constructable")]
    [IsForm(typeof(ERCFenceEndFormType), typeof(SoftwoodHewnLogItem))]
    public partial class ERCSofFenceEndBlock : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
	[Tag("Constructable")]
    public partial class ERCSofFenceEnd90Block : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
	[Tag("Constructable")]
    public partial class ERCSofFenceEnd180Block : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
	[Tag("Constructable")]
    public partial class ERCSofFenceEnd270Block : Block
    { }
	
}