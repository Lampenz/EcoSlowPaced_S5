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
    using Eco.World.Blocks;
    using Eco.Gameplay.Pipes;
    using Tag = Eco.Core.Items.TagAttribute;
	
	[Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [BlockTier(2)] [Tag("Constructable")]
    [IsForm(typeof(ERCFenceSixFormType), typeof(HewnLogItem))]
    public partial class ERCGenFenceSixBlock :
        Block, IRepresentsItem
    {
        public Type RepresentedItemType { get { return typeof(HewnLogItem); } }
    }
	
	[Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [BlockTier(2)] [Tag("Constructable")]
    [IsForm(typeof(ERCFenceFiveFormType), typeof(HewnLogItem))]
    public partial class ERCGenFenceFiveBlock :
        Block, IRepresentsItem
    {
        public Type RepresentedItemType { get { return typeof(HewnLogItem); } }
    }
	
	[Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [BlockTier(2)] [Tag("Constructable")]
    [IsForm(typeof(ERCFenceThreeFormType), typeof(HewnLogItem))]
    public partial class ERCGenFenceThreeBlock :
        Block, IRepresentsItem
    {
        public Type RepresentedItemType { get { return typeof(HewnLogItem); } }
    }
	
	[Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [BlockTier(2)] [Tag("Constructable")]
    [IsForm(typeof(ERCFenceTwoFormType), typeof(HewnLogItem))]
    public partial class ERCGenFenceTwoBlock :
        Block, IRepresentsItem
    {
        public Type RepresentedItemType { get { return typeof(HewnLogItem); } }
    }
	
	[Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [BlockTier(2)] [Tag("Constructable")]
    [IsForm(typeof(ERCFenceCrossFormType), typeof(HewnLogItem))]
    public partial class ERCGenFenceCrossBlock :
        Block, IRepresentsItem
    {
        public Type RepresentedItemType { get { return typeof(HewnLogItem); } }
    }

	[RotatedVariants(typeof(ERCGenFenceEndBlock), typeof(ERCGenFenceEnd90Block), typeof(ERCGenFenceEnd180Block), typeof(ERCGenFenceEnd270Block))]
    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [BlockTier(2)] [Tag("Constructable")]
    [IsForm(typeof(ERCFenceEndFormType), typeof(HewnLogItem))]
    public partial class ERCGenFenceEndBlock : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
	[Tag("Constructable")]
    public partial class ERCGenFenceEnd90Block : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
	[Tag("Constructable")]
    public partial class ERCGenFenceEnd180Block : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
	[Tag("Constructable")]
    public partial class ERCGenFenceEnd270Block : Block
    { }
 
}