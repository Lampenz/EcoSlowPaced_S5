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
    [IsForm(typeof(ERCFenceSixFormType), typeof(HardwoodHewnLogItem))]
    public partial class ERCHarFenceSixBlock :
        Block, IRepresentsItem
    {
        public Type RepresentedItemType { get { return typeof(HardwoodHewnLogItem); } }
    }
	
	[Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [BlockTier(2)] [Tag("Constructable")]
    [IsForm(typeof(ERCFenceFiveFormType), typeof(HardwoodHewnLogItem))]
    public partial class ERCHarFenceFiveBlock :
        Block, IRepresentsItem
    {
        public Type RepresentedItemType { get { return typeof(HardwoodHewnLogItem); } }
    }
	
	[Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [BlockTier(2)] [Tag("Constructable")]
    [IsForm(typeof(ERCFenceThreeFormType), typeof(HardwoodHewnLogItem))]
    public partial class ERCHarFenceThreeBlock :
        Block, IRepresentsItem
    {
        public Type RepresentedItemType { get { return typeof(HardwoodHewnLogItem); } }
    }
	
	[Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [BlockTier(2)] [Tag("Constructable")]
    [IsForm(typeof(ERCFenceTwoFormType), typeof(HardwoodHewnLogItem))]
    public partial class ERCHarFenceTwoBlock :
        Block, IRepresentsItem
    {
        public Type RepresentedItemType { get { return typeof(HardwoodHewnLogItem); } }
    }
	
	[Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [BlockTier(2)] [Tag("Constructable")]
    [IsForm(typeof(ERCFenceCrossFormType), typeof(HardwoodHewnLogItem))]
    public partial class ERCHarFenceCrossBlock :
        Block, IRepresentsItem
    {
        public Type RepresentedItemType { get { return typeof(HardwoodHewnLogItem); } }
    }
 
	[RotatedVariants(typeof(ERCHarFenceEndBlock), typeof(ERCHarFenceEnd90Block), typeof(ERCHarFenceEnd180Block), typeof(ERCHarFenceEnd270Block))]
    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
	[BlockTier(2)] [Tag("Constructable")]
    [IsForm(typeof(ERCFenceEndFormType), typeof(HardwoodHewnLogItem))]
    public partial class ERCHarFenceEndBlock : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
	[Tag("Constructable")]
    public partial class ERCHarFenceEnd90Block : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
	[Tag("Constructable")]
    public partial class ERCHarFenceEnd180Block : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
	[Tag("Constructable")]
    public partial class ERCHarFenceEnd270Block : Block
    { }
 
}