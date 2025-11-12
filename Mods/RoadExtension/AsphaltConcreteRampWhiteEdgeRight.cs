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

    // Right Line Ramps A
    [RotatedVariants(typeof(AsphaltConcreteRampWhiteEdgeRABlock), typeof(AsphaltConcreteRampWhiteEdgeRA90Block), typeof(AsphaltConcreteRampWhiteEdgeRA180Block), typeof(AsphaltConcreteRampWhiteEdgeRA270Block))]
    [Serialized]
    [MakesRoads]
    [Road(1.25f)]
    [Wall, Constructed, Solid]
    [IsForm(typeof(RampWhiteEdgeRAFormType), typeof(AsphaltConcreteItem))]
    [Tag("Constructable")]
    public partial class AsphaltConcreteRampWhiteEdgeRABlock : Block, IColoredBlock
    { }

    [Serialized]
    [MakesRoads]
    [Road(1.25f)]
    [Wall, Constructed, Solid]
    [Tag("Constructable")]
    public partial class AsphaltConcreteRampWhiteEdgeRA90Block : Block, IColoredBlock
    { }

    [Serialized]
    [MakesRoads]
    [Road(1.25f)]
    [Wall, Constructed, Solid]
    [Tag("Constructable")]
    public partial class AsphaltConcreteRampWhiteEdgeRA180Block : Block, IColoredBlock
    { }

    [Serialized]
    [MakesRoads]
    [Road(1.25f)]
    [Wall, Constructed, Solid]
    [Tag("Constructable")]
    public partial class AsphaltConcreteRampWhiteEdgeRA270Block : Block, IColoredBlock
    { }


    // Right Line Ramps B
    [RotatedVariants(typeof(AsphaltConcreteRampWhiteEdgeRBBlock), typeof(AsphaltConcreteRampWhiteEdgeRB90Block), typeof(AsphaltConcreteRampWhiteEdgeRB180Block), typeof(AsphaltConcreteRampWhiteEdgeRB270Block))]
    [Serialized]
    [MakesRoads]
    [Road(1.25f)]
    [Wall, Constructed, Solid]
    [IsForm(typeof(RampWhiteEdgeRBFormType), typeof(AsphaltConcreteItem))]
    [Tag("Constructable")]
    public partial class AsphaltConcreteRampWhiteEdgeRBBlock : Block, IColoredBlock
    { }

    [Serialized]
    [MakesRoads]
    [Road(1.25f)]
    [Wall, Constructed, Solid]
    [Tag("Constructable")]
    public partial class AsphaltConcreteRampWhiteEdgeRB90Block : Block, IColoredBlock
    { }

    [Serialized]
    [MakesRoads]
    [Road(1.25f)]
    [Wall, Constructed, Solid]
    [Tag("Constructable")]
    public partial class AsphaltConcreteRampWhiteEdgeRB180Block : Block, IColoredBlock
    { }

    [Serialized]
    [MakesRoads]
    [Road(1.25f)]
    [Wall, Constructed, Solid]
    [Tag("Constructable")]
    public partial class AsphaltConcreteRampWhiteEdgeRB270Block : Block, IColoredBlock
    { }


    // Right Line Ramps C
    [RotatedVariants(typeof(AsphaltConcreteRampWhiteEdgeRCBlock), typeof(AsphaltConcreteRampWhiteEdgeRC90Block), typeof(AsphaltConcreteRampWhiteEdgeRC180Block), typeof(AsphaltConcreteRampWhiteEdgeRC270Block))]
    [Serialized]
    [MakesRoads]
    [Road(1.25f)]
    [Wall, Constructed, Solid]
    [IsForm(typeof(RampWhiteEdgeRCFormType), typeof(AsphaltConcreteItem))]
    [Tag("Constructable")]
    public partial class AsphaltConcreteRampWhiteEdgeRCBlock : Block, IColoredBlock
    { }

    [Serialized]
    [MakesRoads]
    [Road(1.25f)]
    [Wall, Constructed, Solid]
    [Tag("Constructable")]
    public partial class AsphaltConcreteRampWhiteEdgeRC90Block : Block, IColoredBlock
    { }

    [Serialized]
    [MakesRoads]
    [Road(1.25f)]
    [Wall, Constructed, Solid]
    [Tag("Constructable")]
    public partial class AsphaltConcreteRampWhiteEdgeRC180Block : Block, IColoredBlock
    { }

    [Serialized]
    [MakesRoads]
    [Road(1.25f)]
    [Wall, Constructed, Solid]
    [Tag("Constructable")]
    public partial class AsphaltConcreteRampWhiteEdgeRC270Block : Block, IColoredBlock
    { }


    // Right Line Ramps D
    [RotatedVariants(typeof(AsphaltConcreteRampWhiteEdgeRDBlock), typeof(AsphaltConcreteRampWhiteEdgeRD90Block), typeof(AsphaltConcreteRampWhiteEdgeRD180Block), typeof(AsphaltConcreteRampWhiteEdgeRD270Block))]
    [Serialized]
    [MakesRoads]
    [Road(1.25f)]
    [Wall, Constructed, Solid]
    [IsForm(typeof(RampWhiteEdgeRDFormType), typeof(AsphaltConcreteItem))]
    [Tag("Constructable")]
    public partial class AsphaltConcreteRampWhiteEdgeRDBlock : Block, IColoredBlock
    { }

    [Serialized]
    [MakesRoads]
    [Road(1.25f)]
    [Wall, Constructed, Solid]
    [Tag("Constructable")]
    public partial class AsphaltConcreteRampWhiteEdgeRD90Block : Block, IColoredBlock
    { }

    [Serialized]
    [MakesRoads]
    [Road(1.25f)]
    [Wall, Constructed, Solid]
    [Tag("Constructable")]
    public partial class AsphaltConcreteRampWhiteEdgeRD180Block : Block, IColoredBlock
    { }

    [Serialized]
    [MakesRoads]
    [Road(1.25f)]
    [Wall, Constructed, Solid]
    [Tag("Constructable")]
    public partial class AsphaltConcreteRampWhiteEdgeRD270Block : Block, IColoredBlock
    { }
}
