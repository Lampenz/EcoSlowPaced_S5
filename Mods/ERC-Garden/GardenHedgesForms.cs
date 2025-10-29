// Eco Russian Community

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
    using Eco.World.Water;
    using Eco.World.Blocks;
    using Eco.Gameplay.Pipes;
    using Tag = Eco.Core.Items.TagAttribute;


    [Serialized]
    [Solid, Wall, Constructed,BuildRoomMaterialOption]
    [BlockTier(0)]                                          
    [DoesntEncase]       
    [Tag("Constructable")]
    public partial class HedgeBlock : Block, IRepresentsItem   
    {
        public Type RepresentedItemType { get { return typeof(HedgeItem); } }  
    }
 
    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [BlockTier(0)]
    [IsForm(typeof(HedgeCubeFormType), typeof(HedgeItem))]
    [Tag("Constructable")]
    public partial class HedgeCubeBlock :
        Block, IRepresentsItem
    {
        public Type RepresentedItemType { get { return typeof(HedgeItem); } }
    }
    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [BlockTier(0)]
    [IsForm(typeof(HedgeHemiSphereFormType), typeof(HedgeItem))]
	[Tag("Constructable")]
    public partial class HedgeHemiSphereBlock :
        Block, IRepresentsItem
    {
        public Type RepresentedItemType { get { return typeof(HedgeItem); } }
    }
    
    [RotatedVariants(typeof(HedgeQuarterSphereBlock), typeof(HedgeQuarterSphere90Block), typeof(HedgeQuarterSphere180Block), typeof(HedgeQuarterSphere270Block))]
    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [BlockTier(0)]
    [IsForm(typeof(HedgeQuarterSphereFormType), typeof(HedgeItem))]
	[Tag("Constructable")]
    public partial class HedgeQuarterSphereBlock : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [BlockTier(0)]
	[Tag("Constructable")]
    public partial class HedgeQuarterSphere90Block : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [BlockTier(0)]
	[Tag("Constructable")]
    public partial class HedgeQuarterSphere180Block : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [BlockTier(0)]
	[Tag("Constructable")]
    public partial class HedgeQuarterSphere270Block : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [BlockTier(0)]
    [IsForm(typeof(HedgeCylinderFormType), typeof(HedgeItem))]
	[Tag("Constructable")]
    public partial class HedgeCylinderBlock :
        Block, IRepresentsItem
    {
        public Type RepresentedItemType { get { return typeof(HedgeItem); } }
    }
    

    [RotatedVariants(typeof(HedgeHalfCylinderVBlock), typeof(HedgeHalfCylinderV90Block), typeof(HedgeHalfCylinderV180Block), typeof(HedgeHalfCylinderV270Block))]
    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [BlockTier(0)]
    [IsForm(typeof(HedgeHalfCylinderVFormType), typeof(HedgeItem))]
	[Tag("Constructable")]
    public partial class HedgeHalfCylinderVBlock : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [BlockTier(0)]
	[Tag("Constructable")]
    public partial class HedgeHalfCylinderV90Block : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [BlockTier(0)]
	[Tag("Constructable")]
    public partial class HedgeHalfCylinderV180Block : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [BlockTier(0)]
	[Tag("Constructable")]
    public partial class HedgeHalfCylinderV270Block : Block
    { }
    
    [RotatedVariants(typeof(HedgeHalfCylinderGBlock), typeof(HedgeHalfCylinderG90Block))]
    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [BlockTier(0)]
    [IsForm(typeof(HedgeHalfCylinderGFormType), typeof(HedgeItem))]
	[Tag("Constructable")]
    public partial class HedgeHalfCylinderGBlock : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [BlockTier(0)]
	[Tag("Constructable")]
    public partial class HedgeHalfCylinderG90Block : Block
    { }
    
    [RotatedVariants(typeof(HedgeTopAngleCylinderBlock), typeof(HedgeTopAngleCylinder90Block), typeof(HedgeTopAngleCylinder180Block), typeof(HedgeTopAngleCylinder270Block))]
    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [BlockTier(0)]
    [IsForm(typeof(HedgeTopAngleCylinderFormType), typeof(HedgeItem))]
	[Tag("Constructable")]
    public partial class HedgeTopAngleCylinderBlock : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [BlockTier(0)]
	[Tag("Constructable")]
    public partial class HedgeTopAngleCylinder90Block : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [BlockTier(0)]
	[Tag("Constructable")]
    public partial class HedgeTopAngleCylinder180Block : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [BlockTier(0)]
	[Tag("Constructable")]
    public partial class HedgeTopAngleCylinder270Block : Block
    { }

    [RotatedVariants(typeof(HedgeBottomAngleCylinderBlock), typeof(HedgeBottomAngleCylinder90Block), typeof(HedgeBottomAngleCylinder180Block), typeof(HedgeBottomAngleCylinder270Block))]
    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [BlockTier(0)]
    [IsForm(typeof(HedgeBottomAngleCylinderFormType), typeof(HedgeItem))]
	[Tag("Constructable")]
    public partial class HedgeBottomAngleCylinderBlock : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [BlockTier(0)]
	[Tag("Constructable")]
    public partial class HedgeBottomAngleCylinder90Block : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [BlockTier(0)]
	[Tag("Constructable")]
    public partial class HedgeBottomAngleCylinder180Block : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [BlockTier(0)]
	[Tag("Constructable")]
    public partial class HedgeBottomAngleCylinder270Block : Block
    { }

    [RotatedVariants(typeof(HedgeHalfCylinderTBlock), typeof(HedgeHalfCylinderT90Block), typeof(HedgeHalfCylinderT180Block), typeof(HedgeHalfCylinderT270Block))]
    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [BlockTier(0)]
    [IsForm(typeof(HedgeHalfCylinderTFormType), typeof(HedgeItem))]
	[Tag("Constructable")]
    public partial class HedgeHalfCylinderTBlock : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [BlockTier(0)]
	[Tag("Constructable")]
    public partial class HedgeHalfCylinderT90Block : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [BlockTier(0)]
	[Tag("Constructable")]
    public partial class HedgeHalfCylinderT180Block : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [BlockTier(0)]
	[Tag("Constructable")]
    public partial class HedgeHalfCylinderT270Block : Block
    { }
    
    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [BlockTier(0)]
    [IsForm(typeof(HedgeHalfCylinderXFormType), typeof(HedgeItem))]
	[Tag("Constructable")]
    public partial class HedgeHalfCylinderXBlock :
    Block, IRepresentsItem
    {
        public Type RepresentedItemType { get { return typeof(HedgeItem); } }
    }

    [RotatedVariants(typeof(HedgeTriangleBlock), typeof(HedgeTriangle90Block), typeof(HedgeTriangle180Block), typeof(HedgeTriangle270Block))]
    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [BlockTier(0)]
    [IsForm(typeof(HedgeTriangleFormType), typeof(HedgeItem))]
	[Tag("Constructable")]
    public partial class HedgeTriangleBlock : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [BlockTier(0)]
	[Tag("Constructable")]
    public partial class HedgeTriangle90Block : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [BlockTier(0)]
	[Tag("Constructable")]
    public partial class HedgeTriangle180Block : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [BlockTier(0)]
	[Tag("Constructable")]
    public partial class HedgeTriangle270Block : Block
    { }

    [RotatedVariants(typeof(HedgeHalfCylinderTurnBlock), typeof(HedgeHalfCylinderTurn90Block), typeof(HedgeHalfCylinderTurn180Block), typeof(HedgeHalfCylinderTurn270Block))]
    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [BlockTier(0)]
    [IsForm(typeof(HedgeHalfCylinderTurnFormType), typeof(HedgeItem))]
	[Tag("Constructable")]
    public partial class HedgeHalfCylinderTurnBlock : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [BlockTier(0)]
	[Tag("Constructable")]
    public partial class HedgeHalfCylinderTurn90Block : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [BlockTier(0)]
	[Tag("Constructable")]
    public partial class HedgeHalfCylinderTurn180Block : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [BlockTier(0)]
	[Tag("Constructable")]
    public partial class HedgeHalfCylinderTurn270Block : Block
    { }

    [RotatedVariants(typeof(HedgeHalfCylinderGRBlock), typeof(HedgeHalfCylinderGR90Block))]
    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [BlockTier(0)]
    [IsForm(typeof(HedgeHalfCylinderGRFormType), typeof(HedgeItem))]
	[Tag("Constructable")]
    public partial class HedgeHalfCylinderGRBlock : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [BlockTier(0)]
	[Tag("Constructable")]
    public partial class HedgeHalfCylinderGR90Block : Block
    { }

}