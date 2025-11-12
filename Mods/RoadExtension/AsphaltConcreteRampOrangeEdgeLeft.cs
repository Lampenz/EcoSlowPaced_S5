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
    [RotatedVariants(typeof(AsphaltConcreteRampOrangeEdgeLABlock), typeof(AsphaltConcreteRampOrangeEdgeLA90Block), typeof(AsphaltConcreteRampOrangeEdgeLA180Block), typeof(AsphaltConcreteRampOrangeEdgeLA270Block))]
    [Serialized]
    [MakesRoads]
    [Road(1.25f)]
    [Wall, Constructed, Solid]
    [IsForm(typeof(RampOrangeEdgeLAFormType), typeof(AsphaltConcreteItem))]
    [Tag("Constructable")]
    public partial class AsphaltConcreteRampOrangeEdgeLABlock : Block, IColoredBlock
    { }

    [Serialized]
    [MakesRoads]
    [Road(1.25f)]
    [Wall, Constructed, Solid]
    [Tag("Constructable")]
    public partial class AsphaltConcreteRampOrangeEdgeLA90Block : Block, IColoredBlock
    { }

    [Serialized]
    [MakesRoads]
    [Road(1.25f)]
    [Wall, Constructed, Solid]
    [Tag("Constructable")]
    public partial class AsphaltConcreteRampOrangeEdgeLA180Block : Block, IColoredBlock
    { }

    [Serialized]
    [MakesRoads]
    [Road(1.25f)]
    [Wall, Constructed, Solid]
    [Tag("Constructable")]
    public partial class AsphaltConcreteRampOrangeEdgeLA270Block : Block, IColoredBlock
    { }


    // Left Line Ramps B
    [RotatedVariants(typeof(AsphaltConcreteRampOrangeEdgeLBBlock), typeof(AsphaltConcreteRampOrangeEdgeLB90Block), typeof(AsphaltConcreteRampOrangeEdgeLB180Block), typeof(AsphaltConcreteRampOrangeEdgeLB270Block))]
    [Serialized]
    [MakesRoads]
    [Road(1.25f)]
    [Wall, Constructed, Solid]
    [IsForm(typeof(RampOrangeEdgeLBFormType), typeof(AsphaltConcreteItem))]
    [Tag("Constructable")]
    public partial class AsphaltConcreteRampOrangeEdgeLBBlock : Block, IColoredBlock
    { }

    [Serialized]
    [MakesRoads]
    [Road(1.25f)]
    [Wall, Constructed, Solid]
    [Tag("Constructable")]
    public partial class AsphaltConcreteRampOrangeEdgeLB90Block : Block, IColoredBlock
    { }

    [Serialized]
    [MakesRoads]
    [Road(1.25f)]
    [Wall, Constructed, Solid]
    [Tag("Constructable")]
    public partial class AsphaltConcreteRampOrangeEdgeLB180Block : Block, IColoredBlock
    { }

    [Serialized]
    [MakesRoads]
    [Road(1.25f)]
    [Wall, Constructed, Solid]
    [Tag("Constructable")]
    public partial class AsphaltConcreteRampOrangeEdgeLB270Block : Block, IColoredBlock
    { }


    // Left Line Ramps C
    [RotatedVariants(typeof(AsphaltConcreteRampOrangeEdgeLCBlock), typeof(AsphaltConcreteRampOrangeEdgeLC90Block), typeof(AsphaltConcreteRampOrangeEdgeLC180Block), typeof(AsphaltConcreteRampOrangeEdgeLC270Block))]
    [Serialized]
    [MakesRoads]
    [Road(1.25f)]
    [Wall, Constructed, Solid]
    [IsForm(typeof(RampOrangeEdgeLCFormType), typeof(AsphaltConcreteItem))]
    [Tag("Constructable")]
    public partial class AsphaltConcreteRampOrangeEdgeLCBlock : Block, IColoredBlock
    { }

    [Serialized]
    [MakesRoads]
    [Road(1.25f)]
    [Wall, Constructed, Solid]
    [Tag("Constructable")]
    public partial class AsphaltConcreteRampOrangeEdgeLC90Block : Block, IColoredBlock
    { }

    [Serialized]
    [MakesRoads]
    [Road(1.25f)]
    [Wall, Constructed, Solid]
    [Tag("Constructable")]
    public partial class AsphaltConcreteRampOrangeEdgeLC180Block : Block, IColoredBlock
    { }

    [Serialized]
    [MakesRoads]
    [Road(1.25f)]
    [Wall, Constructed, Solid]
    [Tag("Constructable")]
    public partial class AsphaltConcreteRampOrangeEdgeLC270Block : Block, IColoredBlock
    { }


    // Left Line Ramps D
    [RotatedVariants(typeof(AsphaltConcreteRampOrangeEdgeLDBlock), typeof(AsphaltConcreteRampOrangeEdgeLD90Block), typeof(AsphaltConcreteRampOrangeEdgeLD180Block), typeof(AsphaltConcreteRampOrangeEdgeLD270Block))]
    [Serialized]
    [MakesRoads]
    [Road(1.25f)]
    [Wall, Constructed, Solid]
    [IsForm(typeof(RampOrangeEdgeLDFormType), typeof(AsphaltConcreteItem))]
    [Tag("Constructable")]
    public partial class AsphaltConcreteRampOrangeEdgeLDBlock : Block, IColoredBlock
    { }

    [Serialized]
    [MakesRoads]
    [Road(1.25f)]
    [Wall, Constructed, Solid]
    [Tag("Constructable")]
    public partial class AsphaltConcreteRampOrangeEdgeLD90Block : Block, IColoredBlock
    { }

    [Serialized]
    [MakesRoads]
    [Road(1.25f)]
    [Wall, Constructed, Solid]
    [Tag("Constructable")]
    public partial class AsphaltConcreteRampOrangeEdgeLD180Block : Block, IColoredBlock
    { }

    [Serialized]
    [MakesRoads]
    [Road(1.25f)]
    [Wall, Constructed, Solid]
    [Tag("Constructable")]
    public partial class AsphaltConcreteRampOrangeEdgeLD270Block : Block, IColoredBlock
    { }
}
