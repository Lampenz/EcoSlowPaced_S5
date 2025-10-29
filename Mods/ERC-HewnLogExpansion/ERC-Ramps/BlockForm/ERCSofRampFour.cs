
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
	    
	[RotatedVariants(typeof(ERCSofRampFourABlock), typeof(ERCSofRampFourA90Block), typeof(ERCSofRampFourA180Block), typeof(ERCSofRampFourA270Block))]
    [Serialized]
    [MakesRoads]
    [Road(1f)]
    [Wall, Constructed, Solid]
	[BlockTier(2)] [Tag("Constructable")]
    [IsForm(typeof(ERCRampFourAFormType), typeof(SoftwoodHewnLogItem))]
    public partial class ERCSofRampFourABlock : Block
    { }

    [Serialized]
    [MakesRoads]
    [Road(1f)]
    [Wall, Constructed, Solid]
	[BlockTier(2)] [Tag("Constructable")]
    public partial class ERCSofRampFourA90Block : Block
    { }

    [Serialized]
    [MakesRoads]
    [Road(1f)]
    [Wall, Constructed, Solid]
	[BlockTier(2)] [Tag("Constructable")]
    public partial class ERCSofRampFourA180Block : Block
    { }

    [Serialized]
    [MakesRoads]
    [Road(1f)]
    [Wall, Constructed, Solid]
	[BlockTier(2)] [Tag("Constructable")]
    public partial class ERCSofRampFourA270Block : Block
    { }
	
	[RotatedVariants(typeof(ERCSofRampFourBBlock), typeof(ERCSofRampFourB90Block), typeof(ERCSofRampFourB180Block), typeof(ERCSofRampFourB270Block))]
    [Serialized]
    [MakesRoads]
    [Road(1f)]
    [Wall, Constructed, Solid]
	[BlockTier(2)] [Tag("Constructable")]
    [IsForm(typeof(ERCRampFourBFormType), typeof(SoftwoodHewnLogItem))]
    public partial class ERCSofRampFourBBlock : Block
    { }

    [Serialized]
    [MakesRoads]
    [Road(1f)]
    [Wall, Constructed, Solid]
	[BlockTier(2)] [Tag("Constructable")]
    public partial class ERCSofRampFourB90Block : Block
    { }

    [Serialized]
    [MakesRoads]
    [Road(1f)]
    [Wall, Constructed, Solid]
	[BlockTier(2)] [Tag("Constructable")]
    public partial class ERCSofRampFourB180Block : Block
    { }

    [Serialized]
    [MakesRoads]
    [Road(1f)]
    [Wall, Constructed, Solid]
	[BlockTier(2)] [Tag("Constructable")]
    public partial class ERCSofRampFourB270Block : Block
    { }
	
	[RotatedVariants(typeof(ERCSofRampFourCBlock), typeof(ERCSofRampFourC90Block), typeof(ERCSofRampFourC180Block), typeof(ERCSofRampFourC270Block))]
    [Serialized]
    [MakesRoads]
    [Road(1f)]
    [Wall, Constructed, Solid]
	[BlockTier(2)] [Tag("Constructable")]
    [IsForm(typeof(ERCRampFourCFormType), typeof(SoftwoodHewnLogItem))]
    public partial class ERCSofRampFourCBlock : Block
    { }

    [Serialized]
    [MakesRoads]
    [Road(1f)]
    [Wall, Constructed, Solid]
	[BlockTier(2)] [Tag("Constructable")]
    public partial class ERCSofRampFourC90Block : Block
    { }

    [Serialized]
    [MakesRoads]
    [Road(1f)]
    [Wall, Constructed, Solid]
	[BlockTier(2)] [Tag("Constructable")]
    public partial class ERCSofRampFourC180Block : Block
    { }

    [Serialized]
    [MakesRoads]
    [Road(1f)]
    [Wall, Constructed, Solid]
	[BlockTier(2)] [Tag("Constructable")]
    public partial class ERCSofRampFourC270Block : Block
    { }
	
	[RotatedVariants(typeof(ERCSofRampFourDBlock), typeof(ERCSofRampFourD90Block), typeof(ERCSofRampFourD180Block), typeof(ERCSofRampFourD270Block))]
    [Serialized]
    [MakesRoads]
    [Road(1f)]
    [Wall, Constructed, Solid]
	[BlockTier(2)] [Tag("Constructable")]
    [IsForm(typeof(ERCRampFourDFormType), typeof(SoftwoodHewnLogItem))]
    public partial class ERCSofRampFourDBlock : Block
    { }

    [Serialized]
    [MakesRoads]
    [Road(1f)]
    [Wall, Constructed, Solid]
	[BlockTier(2)] [Tag("Constructable")]
    public partial class ERCSofRampFourD90Block : Block
    { }

    [Serialized]
    [MakesRoads]
    [Road(1f)]
    [Wall, Constructed, Solid]
	[BlockTier(2)] [Tag("Constructable")]
    public partial class ERCSofRampFourD180Block : Block
    { }

    [Serialized]
    [MakesRoads]
    [Road(1f)]
    [Wall, Constructed, Solid]
	[BlockTier(2)] [Tag("Constructable")]
    public partial class ERCSofRampFourD270Block : Block
    { }

}