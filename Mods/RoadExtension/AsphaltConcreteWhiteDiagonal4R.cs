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

    [RotatedVariants(typeof(AsphaltConcreteWhiteDiagonal4RCornerBlock), typeof(AsphaltConcreteWhiteDiagonal4RCorner90Block), typeof(AsphaltConcreteWhiteDiagonal4RCorner180Block), typeof(AsphaltConcreteWhiteDiagonal4RCorner270Block))]
    [Serialized]
    [MakesRoads]
    [Road(1.25f)]
    [Wall, Constructed, Solid]
    [IsForm(typeof(RoadExtWhiteDiagonal4RCornerFormType), typeof(AsphaltConcreteItem))]
    [Tag("Constructable")]
    public partial class AsphaltConcreteWhiteDiagonal4RCornerBlock : Block, IColoredBlock
    { }

    [Serialized]
    [MakesRoads]
    [Road(1.25f)]
    [Wall, Constructed, Solid]
    [Tag("Constructable")]
    public partial class AsphaltConcreteWhiteDiagonal4RCorner90Block : Block, IColoredBlock
    { }

    [Serialized]
    [MakesRoads]
    [Road(1.25f)]
    [Wall, Constructed, Solid]
    [Tag("Constructable")]
    public partial class AsphaltConcreteWhiteDiagonal4RCorner180Block : Block, IColoredBlock
    { }

    [Serialized]
    [MakesRoads]
    [Road(1.25f)]
    [Wall, Constructed, Solid]
    [Tag("Constructable")]
    public partial class AsphaltConcreteWhiteDiagonal4RCorner270Block : Block, IColoredBlock
    { }

    [RotatedVariants(typeof(AsphaltConcreteWhiteDiagonal4RABlock), typeof(AsphaltConcreteWhiteDiagonal4RA90Block), typeof(AsphaltConcreteWhiteDiagonal4RA180Block), typeof(AsphaltConcreteWhiteDiagonal4RA270Block))]
    [Serialized]
    [MakesRoads]
    [Road(1.25f)]
    [Wall, Constructed, Solid]
    [IsForm(typeof(RoadExtWhiteDiagonal4RAFormType), typeof(AsphaltConcreteItem))]
    [Tag("Constructable")]
    public partial class AsphaltConcreteWhiteDiagonal4RABlock : Block, IColoredBlock
    { }

    [Serialized]
    [MakesRoads]
    [Road(1.25f)]
    [Wall, Constructed, Solid]
    [Tag("Constructable")]
    public partial class AsphaltConcreteWhiteDiagonal4RA90Block : Block, IColoredBlock
    { }

    [Serialized]
    [MakesRoads]
    [Road(1.25f)]
    [Wall, Constructed, Solid]
    [Tag("Constructable")]
    public partial class AsphaltConcreteWhiteDiagonal4RA180Block : Block, IColoredBlock
    { }

    [Serialized]
    [MakesRoads]
    [Road(1.25f)]
    [Wall, Constructed, Solid]
    [Tag("Constructable")]
    public partial class AsphaltConcreteWhiteDiagonal4RA270Block : Block, IColoredBlock
    { }

    [RotatedVariants(typeof(AsphaltConcreteWhiteDiagonal4RBBlock), typeof(AsphaltConcreteWhiteDiagonal4RB90Block), typeof(AsphaltConcreteWhiteDiagonal4RB180Block), typeof(AsphaltConcreteWhiteDiagonal4RB270Block))]
    [Serialized]
    [MakesRoads]
    [Road(1.25f)]
    [Wall, Constructed, Solid]
    [IsForm(typeof(RoadExtWhiteDiagonal4RBFormType), typeof(AsphaltConcreteItem))]
    [Tag("Constructable")]
    public partial class AsphaltConcreteWhiteDiagonal4RBBlock : Block, IColoredBlock
    { }

    [Serialized]
    [MakesRoads]
    [Road(1.25f)]
    [Wall, Constructed, Solid]
    [Tag("Constructable")]
    public partial class AsphaltConcreteWhiteDiagonal4RB90Block : Block, IColoredBlock
    { }

    [Serialized]
    [MakesRoads]
    [Road(1.25f)]
    [Wall, Constructed, Solid]
    [Tag("Constructable")]
    public partial class AsphaltConcreteWhiteDiagonal4RB180Block : Block, IColoredBlock
    { }

    [Serialized]
    [MakesRoads]
    [Road(1.25f)]
    [Wall, Constructed, Solid]
    [Tag("Constructable")]
    public partial class AsphaltConcreteWhiteDiagonal4RB270Block : Block, IColoredBlock
    { }

    [RotatedVariants(typeof(AsphaltConcreteWhiteDiagonal4REndBlock), typeof(AsphaltConcreteWhiteDiagonal4REnd90Block), typeof(AsphaltConcreteWhiteDiagonal4REnd180Block), typeof(AsphaltConcreteWhiteDiagonal4REnd270Block))]
    [Serialized]
    [MakesRoads]
    [Road(1.25f)]
    [Wall, Constructed, Solid]
    [IsForm(typeof(RoadExtWhiteDiagonal4REndFormType), typeof(AsphaltConcreteItem))]
    [Tag("Constructable")]
    public partial class AsphaltConcreteWhiteDiagonal4REndBlock : Block, IColoredBlock
    { }

    [Serialized]
    [MakesRoads]
    [Road(1.25f)]
    [Wall, Constructed, Solid]
    [Tag("Constructable")]
    public partial class AsphaltConcreteWhiteDiagonal4REnd90Block : Block, IColoredBlock
    { }

    [Serialized]
    [MakesRoads]
    [Road(1.25f)]
    [Wall, Constructed, Solid]
    [Tag("Constructable")]
    public partial class AsphaltConcreteWhiteDiagonal4REnd180Block : Block, IColoredBlock
    { }

    [Serialized]
    [MakesRoads]
    [Road(1.25f)]
    [Wall, Constructed, Solid]
    [Tag("Constructable")]
    public partial class AsphaltConcreteWhiteDiagonal4REnd270Block : Block, IColoredBlock
    { }

    [RotatedVariants(typeof(AsphaltConcreteWhiteDiagonal4RModLineBlock), typeof(AsphaltConcreteWhiteDiagonal4RModLine90Block), typeof(AsphaltConcreteWhiteDiagonal4RModLine180Block), typeof(AsphaltConcreteWhiteDiagonal4RModLine270Block))]
    [Serialized]
    [MakesRoads]
    [Road(1.25f)]
    [Wall, Constructed, Solid]
    [IsForm(typeof(RoadExtWhiteDiagonal4RModLineFormType), typeof(AsphaltConcreteItem))]
    [Tag("Constructable")]
    public partial class AsphaltConcreteWhiteDiagonal4RModLineBlock : Block, IColoredBlock
    { }

    [Serialized]
    [MakesRoads]
    [Road(1.25f)]
    [Wall, Constructed, Solid]
    [Tag("Constructable")]
    public partial class AsphaltConcreteWhiteDiagonal4RModLine90Block : Block, IColoredBlock
    { }

    [Serialized]
    [MakesRoads]
    [Road(1.25f)]
    [Wall, Constructed, Solid]
    [Tag("Constructable")]
    public partial class AsphaltConcreteWhiteDiagonal4RModLine180Block : Block, IColoredBlock
    { }

    [Serialized]
    [MakesRoads]
    [Road(1.25f)]
    [Wall, Constructed, Solid]
    [Tag("Constructable")]
    public partial class AsphaltConcreteWhiteDiagonal4RModLine270Block : Block, IColoredBlock
    { }
}
