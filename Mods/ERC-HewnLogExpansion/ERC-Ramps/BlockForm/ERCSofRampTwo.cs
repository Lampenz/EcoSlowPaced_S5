
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
	    
	[RotatedVariants(typeof(ERCSofRampTwoABlock), typeof(ERCSofRampTwoA90Block), typeof(ERCSofRampTwoA180Block), typeof(ERCSofRampTwoA270Block))]
    [Serialized]
    [MakesRoads]
    [Road(1f)]
    [Wall, Constructed, Solid]
	[BlockTier(2)] [Tag("Constructable")]
    [IsForm(typeof(ERCRampTwoAFormType), typeof(SoftwoodHewnLogItem))]
    public partial class ERCSofRampTwoABlock : Block
    { }

    [Serialized]
    [MakesRoads]
    [Road(1f)]
    [Wall, Constructed, Solid]
	[BlockTier(2)] [Tag("Constructable")]
    public partial class ERCSofRampTwoA90Block : Block
    { }

    [Serialized]
    [MakesRoads]
    [Road(1f)]
    [Wall, Constructed, Solid]
	[BlockTier(2)] [Tag("Constructable")]
    public partial class ERCSofRampTwoA180Block : Block
    { }

    [Serialized]
    [MakesRoads]
    [Road(1f)]
    [Wall, Constructed, Solid]
	[BlockTier(2)] [Tag("Constructable")]
    public partial class ERCSofRampTwoA270Block : Block
    { }
	
	[RotatedVariants(typeof(ERCSofRampTwoBBlock), typeof(ERCSofRampTwoB90Block), typeof(ERCSofRampTwoB180Block), typeof(ERCSofRampTwoB270Block))]
    [Serialized]
    [MakesRoads]
    [Road(1f)]
    [Wall, Constructed, Solid]
	[BlockTier(2)] [Tag("Constructable")]
    [IsForm(typeof(ERCRampTwoBFormType), typeof(SoftwoodHewnLogItem))]
    public partial class ERCSofRampTwoBBlock : Block
    { }

    [Serialized]
    [MakesRoads]
    [Road(1f)]
    [Wall, Constructed, Solid]
	[BlockTier(2)] [Tag("Constructable")]
    public partial class ERCSofRampTwoB90Block : Block
    { }

    [Serialized]
    [MakesRoads]
    [Road(1f)]
    [Wall, Constructed, Solid]
	[BlockTier(2)] [Tag("Constructable")]
    public partial class ERCSofRampTwoB180Block : Block
    { }

    [Serialized]
    [MakesRoads]
    [Road(1f)]
    [Wall, Constructed, Solid]
	[BlockTier(2)] [Tag("Constructable")]
    public partial class ERCSofRampTwoB270Block : Block
    { }

}