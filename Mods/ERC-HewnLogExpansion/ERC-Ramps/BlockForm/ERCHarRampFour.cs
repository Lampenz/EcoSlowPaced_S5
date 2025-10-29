
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
	    
	[RotatedVariants(typeof(ERCHarRampFourABlock), typeof(ERCHarRampFourA90Block), typeof(ERCHarRampFourA180Block), typeof(ERCHarRampFourA270Block))]
    [Serialized]
    [MakesRoads]
    [Road(1f)]
    [Wall, Constructed, Solid]
	[BlockTier(2)] [Tag("Constructable")]
    [IsForm(typeof(ERCRampFourAFormType), typeof(HardwoodHewnLogItem))]
    public partial class ERCHarRampFourABlock : Block
    { }

    [Serialized]
    [MakesRoads]
    [Road(1f)]
    [Wall, Constructed, Solid]
	[BlockTier(2)] [Tag("Constructable")]
    public partial class ERCHarRampFourA90Block : Block
    { }

    [Serialized]
    [MakesRoads]
    [Road(1f)]
    [Wall, Constructed, Solid]
	[BlockTier(2)] [Tag("Constructable")]
    public partial class ERCHarRampFourA180Block : Block
    { }

    [Serialized]
    [MakesRoads]
    [Road(1f)]
    [Wall, Constructed, Solid]
	[BlockTier(2)] [Tag("Constructable")]
    public partial class ERCHarRampFourA270Block : Block
    { }
	
	[RotatedVariants(typeof(ERCHarRampFourBBlock), typeof(ERCHarRampFourB90Block), typeof(ERCHarRampFourB180Block), typeof(ERCHarRampFourB270Block))]
    [Serialized]
    [MakesRoads]
    [Road(1f)]
    [Wall, Constructed, Solid]
	[BlockTier(2)] [Tag("Constructable")]
    [IsForm(typeof(ERCRampFourBFormType), typeof(HardwoodHewnLogItem))]
    public partial class ERCHarRampFourBBlock : Block
    { }

    [Serialized]
    [MakesRoads]
    [Road(1f)]
    [Wall, Constructed, Solid]
	[BlockTier(2)] [Tag("Constructable")]
    public partial class ERCHarRampFourB90Block : Block
    { }

    [Serialized]
    [MakesRoads]
    [Road(1f)]
    [Wall, Constructed, Solid]
	[BlockTier(2)] [Tag("Constructable")]
    public partial class ERCHarRampFourB180Block : Block
    { }

    [Serialized]
    [MakesRoads]
    [Road(1f)]
    [Wall, Constructed, Solid]
	[BlockTier(2)] [Tag("Constructable")]
    public partial class ERCHarRampFourB270Block : Block
    { }
	
	[RotatedVariants(typeof(ERCHarRampFourCBlock), typeof(ERCHarRampFourC90Block), typeof(ERCHarRampFourC180Block), typeof(ERCHarRampFourC270Block))]
    [Serialized]
    [MakesRoads]
    [Road(1f)]
    [Wall, Constructed, Solid]
	[BlockTier(2)] [Tag("Constructable")]
    [IsForm(typeof(ERCRampFourCFormType), typeof(HardwoodHewnLogItem))]
    public partial class ERCHarRampFourCBlock : Block
    { }

    [Serialized]
    [MakesRoads]
    [Road(1f)]
    [Wall, Constructed, Solid]
	[BlockTier(2)] [Tag("Constructable")]
    public partial class ERCHarRampFourC90Block : Block
    { }

    [Serialized]
    [MakesRoads]
    [Road(1f)]
    [Wall, Constructed, Solid]
	[BlockTier(2)] [Tag("Constructable")]
    public partial class ERCHarRampFourC180Block : Block
    { }

    [Serialized]
    [MakesRoads]
    [Road(1f)]
    [Wall, Constructed, Solid]
	[BlockTier(2)] [Tag("Constructable")]
    public partial class ERCHarRampFourC270Block : Block
    { }
	
	[RotatedVariants(typeof(ERCHarRampFourDBlock), typeof(ERCHarRampFourD90Block), typeof(ERCHarRampFourD180Block), typeof(ERCHarRampFourD270Block))]
    [Serialized]
    [MakesRoads]
    [Road(1f)]
    [Wall, Constructed, Solid]
	[BlockTier(2)] [Tag("Constructable")]
    [IsForm(typeof(ERCRampFourDFormType), typeof(HardwoodHewnLogItem))]
    public partial class ERCHarRampFourDBlock : Block
    { }

    [Serialized]
    [MakesRoads]
    [Road(1f)]
    [Wall, Constructed, Solid]
	[BlockTier(2)] [Tag("Constructable")]
    public partial class ERCHarRampFourD90Block : Block
    { }

    [Serialized]
    [MakesRoads]
    [Road(1f)]
    [Wall, Constructed, Solid]
	[BlockTier(2)] [Tag("Constructable")]
    public partial class ERCHarRampFourD180Block : Block
    { }

    [Serialized]
    [MakesRoads]
    [Road(1f)]
    [Wall, Constructed, Solid]
	[BlockTier(2)] [Tag("Constructable")]
    public partial class ERCHarRampFourD270Block : Block
    { }

}