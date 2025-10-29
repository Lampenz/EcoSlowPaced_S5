
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
	    
	[RotatedVariants(typeof(ERCSofArchMidBlock), typeof(ERCSofArchMid90Block), typeof(ERCSofArchMid180Block), typeof(ERCSofArchMid270Block))]
    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
	[BlockTier(2)] [Tag("Constructable")]
    [IsForm(typeof(ERCArchMidFormType), typeof(SoftwoodHewnLogItem))]
    public partial class ERCSofArchMidBlock : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
	[BlockTier(2)] [Tag("Constructable")]
    public partial class ERCSofArchMid90Block : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
	[BlockTier(2)] [Tag("Constructable")]
    public partial class ERCSofArchMid180Block : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
	[BlockTier(2)] [Tag("Constructable")]
    public partial class ERCSofArchMid270Block : Block
    { }
	
	[RotatedVariants(typeof(ERCSofArchBotBlock), typeof(ERCSofArchBot90Block), typeof(ERCSofArchBot180Block), typeof(ERCSofArchBot270Block))]
    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
	[BlockTier(2)] [Tag("Constructable")]
    [IsForm(typeof(ERCArchBotFormType), typeof(SoftwoodHewnLogItem))]
    public partial class ERCSofArchBotBlock : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
	[BlockTier(2)] [Tag("Constructable")]
    public partial class ERCSofArchBot90Block : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
	[BlockTier(2)] [Tag("Constructable")]
    public partial class ERCSofArchBot180Block : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
	[BlockTier(2)] [Tag("Constructable")]
    public partial class ERCSofArchBot270Block : Block
    { }
	
	[RotatedVariants(typeof(ERCSofArchTopBlock), typeof(ERCSofArchTop90Block), typeof(ERCSofArchTop180Block), typeof(ERCSofArchTop270Block))]
    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
	[BlockTier(2)] [Tag("Constructable")]
    [IsForm(typeof(ERCArchTopFormType), typeof(SoftwoodHewnLogItem))]
    public partial class ERCSofArchTopBlock : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
	[BlockTier(2)] [Tag("Constructable")]
    public partial class ERCSofArchTop90Block : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
	[BlockTier(2)] [Tag("Constructable")]
    public partial class ERCSofArchTop180Block : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
	[BlockTier(2)] [Tag("Constructable")]
    public partial class ERCSofArchTop270Block : Block
    { }
	
	[RotatedVariants(typeof(ERCSofArchCornerTBlock), typeof(ERCSofArchCornerT90Block), typeof(ERCSofArchCornerT180Block), typeof(ERCSofArchCornerT270Block))]
    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
	[BlockTier(2)] [Tag("Constructable")]
    [IsForm(typeof(ERCArchCornerTFormType), typeof(SoftwoodHewnLogItem))]
    public partial class ERCSofArchCornerTBlock : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
	[BlockTier(2)] [Tag("Constructable")]
    public partial class ERCSofArchCornerT90Block : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
	[BlockTier(2)] [Tag("Constructable")]
    public partial class ERCSofArchCornerT180Block : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
	[BlockTier(2)] [Tag("Constructable")]
    public partial class ERCSofArchCornerT270Block : Block
    { }
	
	[RotatedVariants(typeof(ERCSofArchCornerBBlock), typeof(ERCSofArchCornerB90Block), typeof(ERCSofArchCornerB180Block), typeof(ERCSofArchCornerB270Block))]
    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
	[BlockTier(2)] [Tag("Constructable")]
    [IsForm(typeof(ERCArchCornerBFormType), typeof(SoftwoodHewnLogItem))]
    public partial class ERCSofArchCornerBBlock : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
	[BlockTier(2)] [Tag("Constructable")]
    public partial class ERCSofArchCornerB90Block : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
	[BlockTier(2)] [Tag("Constructable")]
    public partial class ERCSofArchCornerB180Block : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
	[BlockTier(2)] [Tag("Constructable")]
    public partial class ERCSofArchCornerB270Block : Block
    { }
	
	[RotatedVariants(typeof(ERCSofArchMidCuBlock), typeof(ERCSofArchMidCu90Block), typeof(ERCSofArchMidCu180Block), typeof(ERCSofArchMidCu270Block))]
    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
	[BlockTier(2)] [Tag("Constructable")]
    [IsForm(typeof(ERCArchMidCuFormType), typeof(SoftwoodHewnLogItem))]
    public partial class ERCSofArchMidCuBlock : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
	[BlockTier(2)] [Tag("Constructable")]
    public partial class ERCSofArchMidCu90Block : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
	[BlockTier(2)] [Tag("Constructable")]
    public partial class ERCSofArchMidCu180Block : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
	[BlockTier(2)] [Tag("Constructable")]
    public partial class ERCSofArchMidCu270Block : Block
    { }
	
	[RotatedVariants(typeof(ERCSofArchTopCuBlock), typeof(ERCSofArchTopCu90Block), typeof(ERCSofArchTopCu180Block), typeof(ERCSofArchTopCu270Block))]
    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
	[BlockTier(2)] [Tag("Constructable")]
    [IsForm(typeof(ERCArchTopCuFormType), typeof(SoftwoodHewnLogItem))]
    public partial class ERCSofArchTopCuBlock : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
	[BlockTier(2)] [Tag("Constructable")]
    public partial class ERCSofArchTopCu90Block : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
	[BlockTier(2)] [Tag("Constructable")]
    public partial class ERCSofArchTopCu180Block : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
	[BlockTier(2)] [Tag("Constructable")]
    public partial class ERCSofArchTopCu270Block : Block
    { }
	
	[RotatedVariants(typeof(ERCSofArchCornerTCuBlock), typeof(ERCSofArchCornerTCu90Block), typeof(ERCSofArchCornerTCu180Block), typeof(ERCSofArchCornerTCu270Block))]
    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
	[BlockTier(2)] [Tag("Constructable")]
    [IsForm(typeof(ERCArchCornerTCuFormType), typeof(SoftwoodHewnLogItem))]
    public partial class ERCSofArchCornerTCuBlock : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
	[BlockTier(2)] [Tag("Constructable")]
    public partial class ERCSofArchCornerTCu90Block : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
	[BlockTier(2)] [Tag("Constructable")]
    public partial class ERCSofArchCornerTCu180Block : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
	[BlockTier(2)] [Tag("Constructable")]
    public partial class ERCSofArchCornerTCu270Block : Block
    { }

}