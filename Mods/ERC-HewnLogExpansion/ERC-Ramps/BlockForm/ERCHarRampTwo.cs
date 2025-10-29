
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
	    
	[RotatedVariants(typeof(ERCHarRampTwoABlock), typeof(ERCHarRampTwoA90Block), typeof(ERCHarRampTwoA180Block), typeof(ERCHarRampTwoA270Block))]
    [Serialized]
    [MakesRoads]
    [Road(1f)]
    [Wall, Constructed, Solid]
	[BlockTier(2)] [Tag("Constructable")]
    [IsForm(typeof(ERCRampTwoAFormType), typeof(HardwoodHewnLogItem))]
    public partial class ERCHarRampTwoABlock : Block
    { }

    [Serialized]
    [MakesRoads]
    [Road(1f)]
    [Wall, Constructed, Solid]
	[BlockTier(2)] [Tag("Constructable")]
    public partial class ERCHarRampTwoA90Block : Block
    { }

    [Serialized]
    [MakesRoads]
    [Road(1f)]
    [Wall, Constructed, Solid]
	[BlockTier(2)] [Tag("Constructable")]
    public partial class ERCHarRampTwoA180Block : Block
    { }

    [Serialized]
    [MakesRoads]
    [Road(1f)]
    [Wall, Constructed, Solid]
	[BlockTier(2)] [Tag("Constructable")]
    public partial class ERCHarRampTwoA270Block : Block
    { }
	
	[RotatedVariants(typeof(ERCHarRampTwoBBlock), typeof(ERCHarRampTwoB90Block), typeof(ERCHarRampTwoB180Block), typeof(ERCHarRampTwoB270Block))]
    [Serialized]
    [MakesRoads]
    [Road(1f)]
    [Wall, Constructed, Solid]
	[BlockTier(2)] [Tag("Constructable")]
    [IsForm(typeof(ERCRampTwoBFormType), typeof(HardwoodHewnLogItem))]
    public partial class ERCHarRampTwoBBlock : Block
    { }

    [Serialized]
    [MakesRoads]
    [Road(1f)]
    [Wall, Constructed, Solid]
	[BlockTier(2)] [Tag("Constructable")]
    public partial class ERCHarRampTwoB90Block : Block
    { }

    [Serialized]
    [MakesRoads]
    [Road(1f)]
    [Wall, Constructed, Solid]
	[BlockTier(2)] [Tag("Constructable")]
    public partial class ERCHarRampTwoB180Block : Block
    { }

    [Serialized]
    [MakesRoads]
    [Road(1f)]
    [Wall, Constructed, Solid]
	[BlockTier(2)] [Tag("Constructable")]
    public partial class ERCHarRampTwoB270Block : Block
    { }

}