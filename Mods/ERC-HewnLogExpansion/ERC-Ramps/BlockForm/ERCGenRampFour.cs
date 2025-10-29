
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
	
	[RotatedVariants(typeof(ERCGenRampFourABlock), typeof(ERCGenRampFourA90Block), typeof(ERCGenRampFourA180Block), typeof(ERCGenRampFourA270Block))]
    [Serialized]
    [MakesRoads]
    [Road(1f)]
    [Wall, Constructed, Solid]
	[BlockTier(2)] [Tag("Constructable")]
    [IsForm(typeof(ERCRampFourAFormType), typeof(HewnLogItem))]
    public partial class ERCGenRampFourABlock : Block
    { }

    [Serialized]
    [MakesRoads]
    [Road(1f)]
    [Wall, Constructed, Solid]
	[BlockTier(2)] [Tag("Constructable")]
    public partial class ERCGenRampFourA90Block : Block
    { }

    [Serialized]
    [MakesRoads]
    [Road(1f)]
    [Wall, Constructed, Solid]
	[BlockTier(2)] [Tag("Constructable")]
    public partial class ERCGenRampFourA180Block : Block
    { }

    [Serialized]
    [MakesRoads]
    [Road(1f)]
    [Wall, Constructed, Solid]
	[BlockTier(2)] [Tag("Constructable")]
    public partial class ERCGenRampFourA270Block : Block
    { }
	
	[RotatedVariants(typeof(ERCGenRampFourBBlock), typeof(ERCGenRampFourB90Block), typeof(ERCGenRampFourB180Block), typeof(ERCGenRampFourB270Block))]
    [Serialized]
    [MakesRoads]
    [Road(1f)]
    [Wall, Constructed, Solid]
	[BlockTier(2)] [Tag("Constructable")]
    [IsForm(typeof(ERCRampFourBFormType), typeof(HewnLogItem))]
    public partial class ERCGenRampFourBBlock : Block
    { }

    [Serialized]
    [MakesRoads]
    [Road(1f)]
    [Wall, Constructed, Solid]
	[BlockTier(2)] [Tag("Constructable")]
    public partial class ERCGenRampFourB90Block : Block
    { }

    [Serialized]
    [MakesRoads]
    [Road(1f)]
    [Wall, Constructed, Solid]
	[BlockTier(2)] [Tag("Constructable")]
    public partial class ERCGenRampFourB180Block : Block
    { }

    [Serialized]
    [MakesRoads]
    [Road(1f)]
    [Wall, Constructed, Solid]
	[BlockTier(2)] [Tag("Constructable")]
    public partial class ERCGenRampFourB270Block : Block
    { }
	
	[RotatedVariants(typeof(ERCGenRampFourCBlock), typeof(ERCGenRampFourC90Block), typeof(ERCGenRampFourC180Block), typeof(ERCGenRampFourC270Block))]
    [Serialized]
    [MakesRoads]
    [Road(1f)]
    [Wall, Constructed, Solid]
	[BlockTier(2)] [Tag("Constructable")]
    [IsForm(typeof(ERCRampFourCFormType), typeof(HewnLogItem))]
    public partial class ERCGenRampFourCBlock : Block
    { }

    [Serialized]
    [MakesRoads]
    [Road(1f)]
    [Wall, Constructed, Solid]
	[BlockTier(2)] [Tag("Constructable")]
    public partial class ERCGenRampFourC90Block : Block
    { }

    [Serialized]
    [MakesRoads]
    [Road(1f)]
    [Wall, Constructed, Solid]
	[BlockTier(2)] [Tag("Constructable")]
    public partial class ERCGenRampFourC180Block : Block
    { }

    [Serialized]
    [MakesRoads]
    [Road(1f)]
    [Wall, Constructed, Solid]
	[BlockTier(2)] [Tag("Constructable")]
    public partial class ERCGenRampFourC270Block : Block
    { }
	
	[RotatedVariants(typeof(ERCGenRampFourDBlock), typeof(ERCGenRampFourD90Block), typeof(ERCGenRampFourD180Block), typeof(ERCGenRampFourD270Block))]
    [Serialized]
    [MakesRoads]
    [Road(1f)]
    [Wall, Constructed, Solid]
	[BlockTier(2)] [Tag("Constructable")]
    [IsForm(typeof(ERCRampFourDFormType), typeof(HewnLogItem))]
    public partial class ERCGenRampFourDBlock : Block
    { }

    [Serialized]
    [MakesRoads]
    [Road(1f)]
    [Wall, Constructed, Solid]
	[BlockTier(2)] [Tag("Constructable")]
    public partial class ERCGenRampFourD90Block : Block
    { }

    [Serialized]
    [MakesRoads]
    [Road(1f)]
    [Wall, Constructed, Solid]
	[BlockTier(2)] [Tag("Constructable")]
    public partial class ERCGenRampFourD180Block : Block
    { }

    [Serialized]
    [MakesRoads]
    [Road(1f)]
    [Wall, Constructed, Solid]
	[BlockTier(2)] [Tag("Constructable")]
    public partial class ERCGenRampFourD270Block : Block
    { }

}