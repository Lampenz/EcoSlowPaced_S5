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

    // Left Line Ramps A
    [RotatedVariants(typeof(AsphaltConcreteRampWhiteEdgeLABlock), typeof(AsphaltConcreteRampWhiteEdgeLA90Block), typeof(AsphaltConcreteRampWhiteEdgeLA180Block), typeof(AsphaltConcreteRampWhiteEdgeLA270Block))]
    [Serialized]
    [MakesRoads]
    [Road(1.25f)]
    [Wall, Constructed, Solid]
    [IsForm(typeof(RampWhiteEdgeLAFormType), typeof(AsphaltConcreteItem))]
    [Tag("Constructable")]
    public partial class AsphaltConcreteRampWhiteEdgeLABlock : Block, IColoredBlock
    { }

    [Serialized]
    [MakesRoads]
    [Road(1.25f)]
    [Wall, Constructed, Solid]
    [Tag("Constructable")]
    public partial class AsphaltConcreteRampWhiteEdgeLA90Block : Block, IColoredBlock
    { }

    [Serialized]
    [MakesRoads]
    [Road(1.25f)]
    [Wall, Constructed, Solid]
    [Tag("Constructable")]
    public partial class AsphaltConcreteRampWhiteEdgeLA180Block : Block, IColoredBlock
    { }

    [Serialized]
    [MakesRoads]
    [Road(1.25f)]
    [Wall, Constructed, Solid]
    [Tag("Constructable")]
    public partial class AsphaltConcreteRampWhiteEdgeLA270Block : Block, IColoredBlock
    { }


    // Left Line Ramps B
    [RotatedVariants(typeof(AsphaltConcreteRampWhiteEdgeLBBlock), typeof(AsphaltConcreteRampWhiteEdgeLB90Block), typeof(AsphaltConcreteRampWhiteEdgeLB180Block), typeof(AsphaltConcreteRampWhiteEdgeLB270Block))]
    [Serialized]
    [MakesRoads]
    [Road(1.25f)]
    [Wall, Constructed, Solid]
    [IsForm(typeof(RampWhiteEdgeLBFormType), typeof(AsphaltConcreteItem))]
    [Tag("Constructable")]
    public partial class AsphaltConcreteRampWhiteEdgeLBBlock : Block, IColoredBlock
    { }

    [Serialized]
    [MakesRoads]
    [Road(1.25f)]
    [Wall, Constructed, Solid]
    [Tag("Constructable")]
    public partial class AsphaltConcreteRampWhiteEdgeLB90Block : Block, IColoredBlock
    { }

    [Serialized]
    [MakesRoads]
    [Road(1.25f)]
    [Wall, Constructed, Solid]
    [Tag("Constructable")]
    public partial class AsphaltConcreteRampWhiteEdgeLB180Block : Block, IColoredBlock
    { }

    [Serialized]
    [MakesRoads]
    [Road(1.25f)]
    [Wall, Constructed, Solid]
    [Tag("Constructable")]
    public partial class AsphaltConcreteRampWhiteEdgeLB270Block : Block, IColoredBlock
    { }


    // Left Line Ramps C
    [RotatedVariants(typeof(AsphaltConcreteRampWhiteEdgeLCBlock), typeof(AsphaltConcreteRampWhiteEdgeLC90Block), typeof(AsphaltConcreteRampWhiteEdgeLC180Block), typeof(AsphaltConcreteRampWhiteEdgeLC270Block))]
    [Serialized]
    [MakesRoads]
    [Road(1.25f)]
    [Wall, Constructed, Solid]
    [IsForm(typeof(RampWhiteEdgeLCFormType), typeof(AsphaltConcreteItem))]
    [Tag("Constructable")]
    public partial class AsphaltConcreteRampWhiteEdgeLCBlock : Block, IColoredBlock
    { }

    [Serialized]
    [MakesRoads]
    [Road(1.25f)]
    [Wall, Constructed, Solid]
    [Tag("Constructable")]
    public partial class AsphaltConcreteRampWhiteEdgeLC90Block : Block, IColoredBlock
    { }

    [Serialized]
    [MakesRoads]
    [Road(1.25f)]
    [Wall, Constructed, Solid]
    [Tag("Constructable")]
    public partial class AsphaltConcreteRampWhiteEdgeLC180Block : Block, IColoredBlock
    { }

    [Serialized]
    [MakesRoads]
    [Road(1.25f)]
    [Wall, Constructed, Solid]
    [Tag("Constructable")]
    public partial class AsphaltConcreteRampWhiteEdgeLC270Block : Block, IColoredBlock
    { }


    // Left Line Ramps D
    [RotatedVariants(typeof(AsphaltConcreteRampWhiteEdgeLDBlock), typeof(AsphaltConcreteRampWhiteEdgeLD90Block), typeof(AsphaltConcreteRampWhiteEdgeLD180Block), typeof(AsphaltConcreteRampWhiteEdgeLD270Block))]
    [Serialized]
    [MakesRoads]
    [Road(1.25f)]
    [Wall, Constructed, Solid]
    [IsForm(typeof(RampWhiteEdgeLDFormType), typeof(AsphaltConcreteItem))]
    [Tag("Constructable")]
    public partial class AsphaltConcreteRampWhiteEdgeLDBlock : Block, IColoredBlock
    { }

    [Serialized]
    [MakesRoads]
    [Road(1.25f)]
    [Wall, Constructed, Solid]
    [Tag("Constructable")]
    public partial class AsphaltConcreteRampWhiteEdgeLD90Block : Block, IColoredBlock
    { }

    [Serialized]
    [MakesRoads]
    [Road(1.25f)]
    [Wall, Constructed, Solid]
    [Tag("Constructable")]
    public partial class AsphaltConcreteRampWhiteEdgeLD180Block : Block, IColoredBlock
    { }

    [Serialized]
    [MakesRoads]
    [Road(1.25f)]
    [Wall, Constructed, Solid]
    [Tag("Constructable")]
    public partial class AsphaltConcreteRampWhiteEdgeLD270Block : Block, IColoredBlock
    { }
}
