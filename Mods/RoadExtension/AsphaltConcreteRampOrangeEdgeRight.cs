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
    [RotatedVariants(typeof(AsphaltConcreteRampOrangeEdgeRABlock), typeof(AsphaltConcreteRampOrangeEdgeRA90Block), typeof(AsphaltConcreteRampOrangeEdgeRA180Block), typeof(AsphaltConcreteRampOrangeEdgeRA270Block))]
    [Serialized]
    [MakesRoads]
    [Road(1.25f)]
    [Wall, Constructed, Solid]
    [IsForm(typeof(RampOrangeEdgeRAFormType), typeof(AsphaltConcreteItem))]
    [Tag("Constructable")]
    public partial class AsphaltConcreteRampOrangeEdgeRABlock : Block, IColoredBlock
    { }

    [Serialized]
    [MakesRoads]
    [Road(1.25f)]
    [Wall, Constructed, Solid]
    [Tag("Constructable")]
    public partial class AsphaltConcreteRampOrangeEdgeRA90Block : Block, IColoredBlock
    { }

    [Serialized]
    [MakesRoads]
    [Road(1.25f)]
    [Wall, Constructed, Solid]
    [Tag("Constructable")]
    public partial class AsphaltConcreteRampOrangeEdgeRA180Block : Block, IColoredBlock
    { }

    [Serialized]
    [MakesRoads]
    [Road(1.25f)]
    [Wall, Constructed, Solid]
    [Tag("Constructable")]
    public partial class AsphaltConcreteRampOrangeEdgeRA270Block : Block, IColoredBlock
    { }


    // Right Line Ramps B
    [RotatedVariants(typeof(AsphaltConcreteRampOrangeEdgeRBBlock), typeof(AsphaltConcreteRampOrangeEdgeRB90Block), typeof(AsphaltConcreteRampOrangeEdgeRB180Block), typeof(AsphaltConcreteRampOrangeEdgeRB270Block))]
    [Serialized]
    [MakesRoads]
    [Road(1.25f)]
    [Wall, Constructed, Solid]
    [IsForm(typeof(RampOrangeEdgeRBFormType), typeof(AsphaltConcreteItem))]
    [Tag("Constructable")]
    public partial class AsphaltConcreteRampOrangeEdgeRBBlock : Block, IColoredBlock
    { }

    [Serialized]
    [MakesRoads]
    [Road(1.25f)]
    [Wall, Constructed, Solid]
    [Tag("Constructable")]
    public partial class AsphaltConcreteRampOrangeEdgeRB90Block : Block, IColoredBlock
    { }

    [Serialized]
    [MakesRoads]
    [Road(1.25f)]
    [Wall, Constructed, Solid]
    [Tag("Constructable")]
    public partial class AsphaltConcreteRampOrangeEdgeRB180Block : Block, IColoredBlock
    { }

    [Serialized]
    [MakesRoads]
    [Road(1.25f)]
    [Wall, Constructed, Solid]
    [Tag("Constructable")]
    public partial class AsphaltConcreteRampOrangeEdgeRB270Block : Block, IColoredBlock
    { }


    // Right Line Ramps C
    [RotatedVariants(typeof(AsphaltConcreteRampOrangeEdgeRCBlock), typeof(AsphaltConcreteRampOrangeEdgeRC90Block), typeof(AsphaltConcreteRampOrangeEdgeRC180Block), typeof(AsphaltConcreteRampOrangeEdgeRC270Block))]
    [Serialized]
    [MakesRoads]
    [Road(1.25f)]
    [Wall, Constructed, Solid]
    [IsForm(typeof(RampOrangeEdgeRCFormType), typeof(AsphaltConcreteItem))]
    [Tag("Constructable")]
    public partial class AsphaltConcreteRampOrangeEdgeRCBlock : Block, IColoredBlock
    { }

    [Serialized]
    [MakesRoads]
    [Road(1.25f)]
    [Wall, Constructed, Solid]
    [Tag("Constructable")]
    public partial class AsphaltConcreteRampOrangeEdgeRC90Block : Block, IColoredBlock
    { }

    [Serialized]
    [MakesRoads]
    [Road(1.25f)]
    [Wall, Constructed, Solid]
    [Tag("Constructable")]
    public partial class AsphaltConcreteRampOrangeEdgeRC180Block : Block, IColoredBlock
    { }

    [Serialized]
    [MakesRoads]
    [Road(1.25f)]
    [Wall, Constructed, Solid]
    [Tag("Constructable")]
    public partial class AsphaltConcreteRampOrangeEdgeRC270Block : Block, IColoredBlock
    { }


    // Right Line Ramps D
    [RotatedVariants(typeof(AsphaltConcreteRampOrangeEdgeRDBlock), typeof(AsphaltConcreteRampOrangeEdgeRD90Block), typeof(AsphaltConcreteRampOrangeEdgeRD180Block), typeof(AsphaltConcreteRampOrangeEdgeRD270Block))]
    [Serialized]
    [MakesRoads]
    [Road(1.25f)]
    [Wall, Constructed, Solid]
    [IsForm(typeof(RampOrangeEdgeRDFormType), typeof(AsphaltConcreteItem))]
    [Tag("Constructable")]
    public partial class AsphaltConcreteRampOrangeEdgeRDBlock : Block, IColoredBlock
    { }

    [Serialized]
    [MakesRoads]
    [Road(1.25f)]
    [Wall, Constructed, Solid]
    [Tag("Constructable")]
    public partial class AsphaltConcreteRampOrangeEdgeRD90Block : Block, IColoredBlock
    { }

    [Serialized]
    [MakesRoads]
    [Road(1.25f)]
    [Wall, Constructed, Solid]
    [Tag("Constructable")]
    public partial class AsphaltConcreteRampOrangeEdgeRD180Block : Block, IColoredBlock
    { }

    [Serialized]
    [MakesRoads]
    [Road(1.25f)]
    [Wall, Constructed, Solid]
    [Tag("Constructable")]
    public partial class AsphaltConcreteRampOrangeEdgeRD270Block : Block, IColoredBlock
    { }
}
