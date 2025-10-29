
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
    
	[RotatedVariants(typeof(ERCGenArchMidBlock), typeof(ERCGenArchMid90Block), typeof(ERCGenArchMid180Block), typeof(ERCGenArchMid270Block))]
    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
	[BlockTier(2)] [Tag("Constructable")]
    [IsForm(typeof(ERCArchMidFormType), typeof(HewnLogItem))]
    public partial class ERCGenArchMidBlock : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
	[BlockTier(2)] [Tag("Constructable")]
    public partial class ERCGenArchMid90Block : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
	[BlockTier(2)] [Tag("Constructable")]
    public partial class ERCGenArchMid180Block : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
	[BlockTier(2)] [Tag("Constructable")]
    public partial class ERCGenArchMid270Block : Block
    { }
	
	[RotatedVariants(typeof(ERCGenArchBotBlock), typeof(ERCGenArchBot90Block), typeof(ERCGenArchBot180Block), typeof(ERCGenArchBot270Block))]
    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
	[BlockTier(2)] [Tag("Constructable")]
    [IsForm(typeof(ERCArchBotFormType), typeof(HewnLogItem))]
    public partial class ERCGenArchBotBlock : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
	[BlockTier(2)] [Tag("Constructable")]
    public partial class ERCGenArchBot90Block : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
	[BlockTier(2)] [Tag("Constructable")]
    public partial class ERCGenArchBot180Block : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
	[BlockTier(2)] [Tag("Constructable")]
    public partial class ERCGenArchBot270Block : Block
    { }
	
	[RotatedVariants(typeof(ERCGenArchTopBlock), typeof(ERCGenArchTop90Block), typeof(ERCGenArchTop180Block), typeof(ERCGenArchTop270Block))]
    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
	[BlockTier(2)] [Tag("Constructable")]
    [IsForm(typeof(ERCArchTopFormType), typeof(HewnLogItem))]
    public partial class ERCGenArchTopBlock : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
	[BlockTier(2)] [Tag("Constructable")]
    public partial class ERCGenArchTop90Block : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
	[BlockTier(2)] [Tag("Constructable")]
    public partial class ERCGenArchTop180Block : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
	[BlockTier(2)] [Tag("Constructable")]
    public partial class ERCGenArchTop270Block : Block
    { }
	
	[RotatedVariants(typeof(ERCGenArchCornerTBlock), typeof(ERCGenArchCornerT90Block), typeof(ERCGenArchCornerT180Block), typeof(ERCGenArchCornerT270Block))]
    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
	[BlockTier(2)] [Tag("Constructable")]
    [IsForm(typeof(ERCArchCornerTFormType), typeof(HewnLogItem))]
    public partial class ERCGenArchCornerTBlock : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
	[BlockTier(2)] [Tag("Constructable")]
    public partial class ERCGenArchCornerT90Block : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
	[BlockTier(2)] [Tag("Constructable")]
    public partial class ERCGenArchCornerT180Block : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
	[BlockTier(2)] [Tag("Constructable")]
    public partial class ERCGenArchCornerT270Block : Block
    { }
	
	[RotatedVariants(typeof(ERCGenArchCornerBBlock), typeof(ERCGenArchCornerB90Block), typeof(ERCGenArchCornerB180Block), typeof(ERCGenArchCornerB270Block))]
    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
	[BlockTier(2)] [Tag("Constructable")]
    [IsForm(typeof(ERCArchCornerBFormType), typeof(HewnLogItem))]
    public partial class ERCGenArchCornerBBlock : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
	[BlockTier(2)] [Tag("Constructable")]
    public partial class ERCGenArchCornerB90Block : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
	[BlockTier(2)] [Tag("Constructable")]
    public partial class ERCGenArchCornerB180Block : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
	[BlockTier(2)] [Tag("Constructable")]
    public partial class ERCGenArchCornerB270Block : Block
    { }
	
	[RotatedVariants(typeof(ERCGenArchMidCuBlock), typeof(ERCGenArchMidCu90Block), typeof(ERCGenArchMidCu180Block), typeof(ERCGenArchMidCu270Block))]
    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
	[BlockTier(2)] [Tag("Constructable")]
    [IsForm(typeof(ERCArchMidCuFormType), typeof(HewnLogItem))]
    public partial class ERCGenArchMidCuBlock : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
	[BlockTier(2)] [Tag("Constructable")]
    public partial class ERCGenArchMidCu90Block : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
	[BlockTier(2)] [Tag("Constructable")]
    public partial class ERCGenArchMidCu180Block : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
	[BlockTier(2)] [Tag("Constructable")]
    public partial class ERCGenArchMidCu270Block : Block
    { }
	
	[RotatedVariants(typeof(ERCGenArchTopCuBlock), typeof(ERCGenArchTopCu90Block), typeof(ERCGenArchTopCu180Block), typeof(ERCGenArchTopCu270Block))]
    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
	[BlockTier(2)] [Tag("Constructable")]
    [IsForm(typeof(ERCArchTopCuFormType), typeof(HewnLogItem))]
    public partial class ERCGenArchTopCuBlock : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
	[BlockTier(2)] [Tag("Constructable")]
    public partial class ERCGenArchTopCu90Block : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
	[BlockTier(2)] [Tag("Constructable")]
    public partial class ERCGenArchTopCu180Block : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
	[BlockTier(2)] [Tag("Constructable")]
    public partial class ERCGenArchTopCu270Block : Block
    { }
	
	[RotatedVariants(typeof(ERCGenArchCornerTCuBlock), typeof(ERCGenArchCornerTCu90Block), typeof(ERCGenArchCornerTCu180Block), typeof(ERCGenArchCornerTCu270Block))]
    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
	[BlockTier(2)] [Tag("Constructable")]
    [IsForm(typeof(ERCArchCornerTCuFormType), typeof(HewnLogItem))]
    public partial class ERCGenArchCornerTCuBlock : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
	[BlockTier(2)] [Tag("Constructable")]
    public partial class ERCGenArchCornerTCu90Block : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
	[BlockTier(2)] [Tag("Constructable")]
    public partial class ERCGenArchCornerTCu180Block : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
	[BlockTier(2)] [Tag("Constructable")]
    public partial class ERCGenArchCornerTCu270Block : Block
    { }

}