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
    [MakesRoads]
    [Road(1f)]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [BlockTier(2)] [Tag("Constructable")]
    [IsForm(typeof(ERCBridgeFormType), typeof(HardwoodHewnLogItem))]
    public partial class ERCHarBridgeBlock :
        Block, IRepresentsItem
    {
        public Type RepresentedItemType { get { return typeof(HardwoodHewnLogItem); } }
    }
	[Serialized]
    [MakesRoads]
    [Road(1f)]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [BlockTier(2)] [Tag("Constructable")]
    [IsForm(typeof(ERCBridgeInvFormType), typeof(HardwoodHewnLogItem))]
    public partial class ERCHarBridgeInvBlock :
        Block, IRepresentsItem
    {
        public Type RepresentedItemType { get { return typeof(HardwoodHewnLogItem); } }
    }
		
}