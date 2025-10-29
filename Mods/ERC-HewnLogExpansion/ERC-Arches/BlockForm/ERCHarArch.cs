
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
	
	[RotatedVariants(typeof(ERCHarArchMidBlock), typeof(ERCHarArchMid90Block), typeof(ERCHarArchMid180Block), typeof(ERCHarArchMid270Block))]
    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
	[BlockTier(2)] [Tag("Constructable")]
    [IsForm(typeof(ERCArchMidFormType), typeof(HardwoodHewnLogItem))]
    public partial class ERCHarArchMidBlock : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
	[BlockTier(2)] [Tag("Constructable")]
    public partial class ERCHarArchMid90Block : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
	[BlockTier(2)] [Tag("Constructable")]
    public partial class ERCHarArchMid180Block : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
	[BlockTier(2)] [Tag("Constructable")]
    public partial class ERCHarArchMid270Block : Block
    { }
	
	[RotatedVariants(typeof(ERCHarArchBotBlock), typeof(ERCHarArchBot90Block), typeof(ERCHarArchBot180Block), typeof(ERCHarArchBot270Block))]
    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
	[BlockTier(2)] [Tag("Constructable")]
    [IsForm(typeof(ERCArchBotFormType), typeof(HardwoodHewnLogItem))]
    public partial class ERCHarArchBotBlock : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
	[BlockTier(2)] [Tag("Constructable")]
    public partial class ERCHarArchBot90Block : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
	[BlockTier(2)] [Tag("Constructable")]
    public partial class ERCHarArchBot180Block : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
	[BlockTier(2)] [Tag("Constructable")]
    public partial class ERCHarArchBot270Block : Block
    { }
	
	[RotatedVariants(typeof(ERCHarArchTopBlock), typeof(ERCHarArchTop90Block), typeof(ERCHarArchTop180Block), typeof(ERCHarArchTop270Block))]
    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
	[BlockTier(2)] [Tag("Constructable")]
    [IsForm(typeof(ERCArchTopFormType), typeof(HardwoodHewnLogItem))]
    public partial class ERCHarArchTopBlock : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
	[BlockTier(2)] [Tag("Constructable")]
    public partial class ERCHarArchTop90Block : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
	[BlockTier(2)] [Tag("Constructable")]
    public partial class ERCHarArchTop180Block : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
	[BlockTier(2)] [Tag("Constructable")]
    public partial class ERCHarArchTop270Block : Block
    { }
	
	[RotatedVariants(typeof(ERCHarArchCornerTBlock), typeof(ERCHarArchCornerT90Block), typeof(ERCHarArchCornerT180Block), typeof(ERCHarArchCornerT270Block))]
    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
	[BlockTier(2)] [Tag("Constructable")]
    [IsForm(typeof(ERCArchCornerTFormType), typeof(HardwoodHewnLogItem))]
    public partial class ERCHarArchCornerTBlock : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
	[BlockTier(2)] [Tag("Constructable")]
    public partial class ERCHarArchCornerT90Block : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
	[BlockTier(2)] [Tag("Constructable")]
    public partial class ERCHarArchCornerT180Block : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
	[BlockTier(2)] [Tag("Constructable")]
    public partial class ERCHarArchCornerT270Block : Block
    { }
	
	[RotatedVariants(typeof(ERCHarArchCornerBBlock), typeof(ERCHarArchCornerB90Block), typeof(ERCHarArchCornerB180Block), typeof(ERCHarArchCornerB270Block))]
    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
	[BlockTier(2)] [Tag("Constructable")]
    [IsForm(typeof(ERCArchCornerBFormType), typeof(HardwoodHewnLogItem))]
    public partial class ERCHarArchCornerBBlock : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
	[BlockTier(2)] [Tag("Constructable")]
    public partial class ERCHarArchCornerB90Block : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
	[BlockTier(2)] [Tag("Constructable")]
    public partial class ERCHarArchCornerB180Block : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
	[BlockTier(2)] [Tag("Constructable")]
    public partial class ERCHarArchCornerB270Block : Block
    { }
	
	[RotatedVariants(typeof(ERCHarArchMidCuBlock), typeof(ERCHarArchMidCu90Block), typeof(ERCHarArchMidCu180Block), typeof(ERCHarArchMidCu270Block))]
    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
	[BlockTier(2)] [Tag("Constructable")]
    [IsForm(typeof(ERCArchMidCuFormType), typeof(HardwoodHewnLogItem))]
    public partial class ERCHarArchMidCuBlock : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
	[BlockTier(2)] [Tag("Constructable")]
    public partial class ERCHarArchMidCu90Block : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
	[BlockTier(2)] [Tag("Constructable")]
    public partial class ERCHarArchMidCu180Block : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
	[BlockTier(2)] [Tag("Constructable")]
    public partial class ERCHarArchMidCu270Block : Block
    { }
	
	[RotatedVariants(typeof(ERCHarArchTopCuBlock), typeof(ERCHarArchTopCu90Block), typeof(ERCHarArchTopCu180Block), typeof(ERCHarArchTopCu270Block))]
    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
	[BlockTier(2)] [Tag("Constructable")]
    [IsForm(typeof(ERCArchTopCuFormType), typeof(HardwoodHewnLogItem))]
    public partial class ERCHarArchTopCuBlock : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
	[BlockTier(2)] [Tag("Constructable")]
    public partial class ERCHarArchTopCu90Block : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
	[BlockTier(2)] [Tag("Constructable")]
    public partial class ERCHarArchTopCu180Block : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
	[BlockTier(2)] [Tag("Constructable")]
    public partial class ERCHarArchTopCu270Block : Block
    { }
	
	[RotatedVariants(typeof(ERCHarArchCornerTCuBlock), typeof(ERCHarArchCornerTCu90Block), typeof(ERCHarArchCornerTCu180Block), typeof(ERCHarArchCornerTCu270Block))]
    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
	[BlockTier(2)] [Tag("Constructable")]
    [IsForm(typeof(ERCArchCornerTCuFormType), typeof(HardwoodHewnLogItem))]
    public partial class ERCHarArchCornerTCuBlock : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
	[BlockTier(2)] [Tag("Constructable")]
    public partial class ERCHarArchCornerTCu90Block : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
	[BlockTier(2)] [Tag("Constructable")]
    public partial class ERCHarArchCornerTCu180Block : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
	[BlockTier(2)] [Tag("Constructable")]
    public partial class ERCHarArchCornerTCu270Block : Block
    { }

}