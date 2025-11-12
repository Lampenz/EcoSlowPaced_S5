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

    [RotatedVariants(typeof(AsphaltConcreteWhiteDiagonal2RCornerBlock), typeof(AsphaltConcreteWhiteDiagonal2RCorner90Block), typeof(AsphaltConcreteWhiteDiagonal2RCorner180Block), typeof(AsphaltConcreteWhiteDiagonal2RCorner270Block))]
    [Serialized]
    [MakesRoads]
    [Road(1.25f)]
    [Wall, Constructed, Solid]
    [IsForm(typeof(RoadExtWhiteDiagonal2RCornerFormType), typeof(AsphaltConcreteItem))]
    [Tag("Constructable")]
    public partial class AsphaltConcreteWhiteDiagonal2RCornerBlock : Block, IColoredBlock
    { }

    [Serialized]
    [MakesRoads]
    [Road(1.25f)]
    [Wall, Constructed, Solid]
    [Tag("Constructable")]
    public partial class AsphaltConcreteWhiteDiagonal2RCorner90Block : Block, IColoredBlock
    { }

    [Serialized]
    [MakesRoads]
    [Road(1.25f)]
    [Wall, Constructed, Solid]
    [Tag("Constructable")]
    public partial class AsphaltConcreteWhiteDiagonal2RCorner180Block : Block, IColoredBlock
    { }

    [Serialized]
    [MakesRoads]
    [Road(1.25f)]
    [Wall, Constructed, Solid]
    [Tag("Constructable")]
    public partial class AsphaltConcreteWhiteDiagonal2RCorner270Block : Block, IColoredBlock
    { }

    [RotatedVariants(typeof(AsphaltConcreteWhiteDiagonal2RBlock), typeof(AsphaltConcreteWhiteDiagonal2R90Block), typeof(AsphaltConcreteWhiteDiagonal2R180Block), typeof(AsphaltConcreteWhiteDiagonal2R270Block))]
    [Serialized]
    [MakesRoads]
    [Road(1.25f)]
    [Wall, Constructed, Solid]
    [IsForm(typeof(RoadExtWhiteDiagonal2RFormType), typeof(AsphaltConcreteItem))]
    [Tag("Constructable")]
    public partial class AsphaltConcreteWhiteDiagonal2RBlock : Block, IColoredBlock
    { }

    [Serialized]
    [MakesRoads]
    [Road(1.25f)]
    [Wall, Constructed, Solid]
    [Tag("Constructable")]
    public partial class AsphaltConcreteWhiteDiagonal2R90Block : Block, IColoredBlock
    { }

    [Serialized]
    [MakesRoads]
    [Road(1.25f)]
    [Wall, Constructed, Solid]
    [Tag("Constructable")]
    public partial class AsphaltConcreteWhiteDiagonal2R180Block : Block, IColoredBlock
    { }

    [Serialized]
    [MakesRoads]
    [Road(1.25f)]
    [Wall, Constructed, Solid]
    [Tag("Constructable")]
    public partial class AsphaltConcreteWhiteDiagonal2R270Block : Block, IColoredBlock
    { }

    [RotatedVariants(typeof(AsphaltConcreteWhiteDiagonal2REndBlock), typeof(AsphaltConcreteWhiteDiagonal2REnd90Block), typeof(AsphaltConcreteWhiteDiagonal2REnd180Block), typeof(AsphaltConcreteWhiteDiagonal2REnd270Block))]
    [Serialized]
    [MakesRoads]
    [Road(1.25f)]
    [Wall, Constructed, Solid]
    [IsForm(typeof(RoadExtWhiteDiagonal2REndFormType), typeof(AsphaltConcreteItem))]
    [Tag("Constructable")]
    public partial class AsphaltConcreteWhiteDiagonal2REndBlock : Block, IColoredBlock
    { }

    [Serialized]
    [MakesRoads]
    [Road(1.25f)]
    [Wall, Constructed, Solid]
    [Tag("Constructable")]
    public partial class AsphaltConcreteWhiteDiagonal2REnd90Block : Block, IColoredBlock
    { }

    [Serialized]
    [MakesRoads]
    [Road(1.25f)]
    [Wall, Constructed, Solid]
    [Tag("Constructable")]
    public partial class AsphaltConcreteWhiteDiagonal2REnd180Block : Block, IColoredBlock
    { }

    [Serialized]
    [MakesRoads]
    [Road(1.25f)]
    [Wall, Constructed, Solid]
    [Tag("Constructable")]
    public partial class AsphaltConcreteWhiteDiagonal2REnd270Block : Block, IColoredBlock
    { }

    [RotatedVariants(typeof(AsphaltConcreteWhiteDiagonal2RModLineBlock), typeof(AsphaltConcreteWhiteDiagonal2RModLine90Block), typeof(AsphaltConcreteWhiteDiagonal2RModLine180Block), typeof(AsphaltConcreteWhiteDiagonal2RModLine270Block))]
    [Serialized]
    [MakesRoads]
    [Road(1.25f)]
    [Wall, Constructed, Solid]
    [IsForm(typeof(RoadExtWhiteDiagonal2RModLineFormType), typeof(AsphaltConcreteItem))]
    [Tag("Constructable")]
    public partial class AsphaltConcreteWhiteDiagonal2RModLineBlock : Block, IColoredBlock
    { }

    [Serialized]
    [MakesRoads]
    [Road(1.25f)]
    [Wall, Constructed, Solid]
    [Tag("Constructable")]
    public partial class AsphaltConcreteWhiteDiagonal2RModLine90Block : Block, IColoredBlock
    { }

    [Serialized]
    [MakesRoads]
    [Road(1.25f)]
    [Wall, Constructed, Solid]
    [Tag("Constructable")]
    public partial class AsphaltConcreteWhiteDiagonal2RModLine180Block : Block, IColoredBlock
    { }

    [Serialized]
    [MakesRoads]
    [Road(1.25f)]
    [Wall, Constructed, Solid]
    [Tag("Constructable")]
    public partial class AsphaltConcreteWhiteDiagonal2RModLine270Block : Block, IColoredBlock
    { }
}
