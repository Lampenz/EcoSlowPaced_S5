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
    using Eco.Shared.Localization;
    using Eco.Shared.Utils;
    using Eco.World;
    using Eco.World.Blocks;
    using Eco.Gameplay.Pipes;
    using Tag = Eco.Core.Items.TagAttribute;
    using Eco.World.Color;


    [Serialized]
    [MakesRoads]
    [Road(1.25f)]
    [Wall, Constructed, Solid]
    [IsForm(typeof(RoadExtControllerCubeFormType), typeof(AsphaltConcreteItem))]
    [Tag("Constructable")]
    public partial class AsphaltConcreteControllerCubeBlock : Block, IColoredBlock
    { }



    [RotatedVariants(typeof(AsphaltConcreteControllerRampABlock), typeof(AsphaltConcreteControllerRampA90Block), typeof(AsphaltConcreteControllerRampA180Block), typeof(AsphaltConcreteControllerRampA270Block))]
    [Serialized]
    [MakesRoads]
    [Road(1.25f)]
    [Wall, Constructed, Solid]
    [IsForm(typeof(RoadExtControllerRampAFormType), typeof(AsphaltConcreteItem))]
    [Tag("Constructable")]
    public partial class AsphaltConcreteControllerRampABlock : Block, IColoredBlock
    { }

    [Serialized]
    [MakesRoads]
    [Road(1.25f)]
    [Wall, Constructed, Solid]
    [Tag("Constructable")]
    public partial class AsphaltConcreteControllerRampA90Block : Block, IColoredBlock
    { }

    [Serialized]
    [MakesRoads]
    [Road(1.25f)]
    [Wall, Constructed, Solid]
    [Tag("Constructable")]
    public partial class AsphaltConcreteControllerRampA180Block : Block, IColoredBlock
    { }

    [Serialized]
    [MakesRoads]
    [Road(1.25f)]
    [Wall, Constructed, Solid]
    [Tag("Constructable")]
    public partial class AsphaltConcreteControllerRampA270Block : Block, IColoredBlock
    { }



    [RotatedVariants(typeof(AsphaltConcreteControllerRampBBlock), typeof(AsphaltConcreteControllerRampB90Block), typeof(AsphaltConcreteControllerRampB180Block), typeof(AsphaltConcreteControllerRampB270Block))]
    [Serialized]
    [MakesRoads]
    [Road(1.25f)]
    [Wall, Constructed, Solid]
    [IsForm(typeof(RoadExtControllerRampBFormType), typeof(AsphaltConcreteItem))]
    [Tag("Constructable")]
    public partial class AsphaltConcreteControllerRampBBlock : Block, IColoredBlock
    { }

    [Serialized]
    [MakesRoads]
    [Road(1.25f)]
    [Wall, Constructed, Solid]
    [Tag("Constructable")]
    public partial class AsphaltConcreteControllerRampB90Block : Block, IColoredBlock
    { }

    [Serialized]
    [MakesRoads]
    [Road(1.25f)]
    [Wall, Constructed, Solid]
    [Tag("Constructable")]
    public partial class AsphaltConcreteControllerRampB180Block : Block, IColoredBlock
    { }

    [Serialized]
    [MakesRoads]
    [Road(1.25f)]
    [Wall, Constructed, Solid]
    [Tag("Constructable")]
    public partial class AsphaltConcreteControllerRampB270Block : Block, IColoredBlock
    { }



    [RotatedVariants(typeof(AsphaltConcreteControllerRampCBlock), typeof(AsphaltConcreteControllerRampC90Block), typeof(AsphaltConcreteControllerRampC180Block), typeof(AsphaltConcreteControllerRampC270Block))]
    [Serialized]
    [MakesRoads]
    [Road(1.25f)]
    [Wall, Constructed, Solid]
    [IsForm(typeof(RoadExtControllerRampCFormType), typeof(AsphaltConcreteItem))]
    [Tag("Constructable")]
    public partial class AsphaltConcreteControllerRampCBlock : Block, IColoredBlock
    { }

    [Serialized]
    [MakesRoads]
    [Road(1.25f)]
    [Wall, Constructed, Solid]
    [Tag("Constructable")]
    public partial class AsphaltConcreteControllerRampC90Block : Block, IColoredBlock
    { }

    [Serialized]
    [MakesRoads]
    [Road(1.25f)]
    [Wall, Constructed, Solid]
    [Tag("Constructable")]
    public partial class AsphaltConcreteControllerRampC180Block : Block, IColoredBlock
    { }

    [Serialized]
    [MakesRoads]
    [Road(1.25f)]
    [Wall, Constructed, Solid]
    [Tag("Constructable")]
    public partial class AsphaltConcreteControllerRampC270Block : Block, IColoredBlock
    { }



    [RotatedVariants(typeof(AsphaltConcreteControllerRampDBlock), typeof(AsphaltConcreteControllerRampD90Block), typeof(AsphaltConcreteControllerRampD180Block), typeof(AsphaltConcreteControllerRampD270Block))]
    [Serialized]
    [MakesRoads]
    [Road(1.25f)]
    [Wall, Constructed, Solid]
    [IsForm(typeof(RoadExtControllerRampDFormType), typeof(AsphaltConcreteItem))]
    [Tag("Constructable")]
    public partial class AsphaltConcreteControllerRampDBlock : Block, IColoredBlock
    { }

    [Serialized]
    [MakesRoads]
    [Road(1.25f)]
    [Wall, Constructed, Solid]
    [Tag("Constructable")]
    public partial class AsphaltConcreteControllerRampD90Block : Block, IColoredBlock
    { }

    [Serialized]
    [MakesRoads]
    [Road(1.25f)]
    [Wall, Constructed, Solid]
    [Tag("Constructable")]
    public partial class AsphaltConcreteControllerRampD180Block : Block, IColoredBlock
    { }

    [Serialized]
    [MakesRoads]
    [Road(1.25f)]
    [Wall, Constructed, Solid]
    [Tag("Constructable")]
    public partial class AsphaltConcreteControllerRampD270Block : Block, IColoredBlock
    { }

}
