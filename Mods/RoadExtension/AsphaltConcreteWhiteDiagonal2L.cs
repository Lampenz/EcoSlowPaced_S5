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

    [RotatedVariants(typeof(AsphaltConcreteWhiteDiagonal2LCornerBlock), typeof(AsphaltConcreteWhiteDiagonal2LCorner90Block), typeof(AsphaltConcreteWhiteDiagonal2LCorner180Block), typeof(AsphaltConcreteWhiteDiagonal2LCorner270Block))]
    [Serialized]
    [MakesRoads]
    [Road(1.25f)]
    [Wall, Constructed, Solid]
    [IsForm(typeof(RoadExtWhiteDiagonal2LCornerFormType), typeof(AsphaltConcreteItem))]
    [Tag("Constructable")]
    public partial class AsphaltConcreteWhiteDiagonal2LCornerBlock : Block, IColoredBlock
    { }

    [Serialized]
    [MakesRoads]
    [Road(1.25f)]
    [Wall, Constructed, Solid]
    [Tag("Constructable")]
    public partial class AsphaltConcreteWhiteDiagonal2LCorner90Block : Block, IColoredBlock
    { }

    [Serialized]
    [MakesRoads]
    [Road(1.25f)]
    [Wall, Constructed, Solid]
    [Tag("Constructable")]
    public partial class AsphaltConcreteWhiteDiagonal2LCorner180Block : Block, IColoredBlock
    { }

    [Serialized]
    [MakesRoads]
    [Road(1.25f)]
    [Wall, Constructed, Solid]
    [Tag("Constructable")]
    public partial class AsphaltConcreteWhiteDiagonal2LCorner270Block : Block, IColoredBlock
    { }

    [RotatedVariants(typeof(AsphaltConcreteWhiteDiagonal2LBlock), typeof(AsphaltConcreteWhiteDiagonal2L90Block), typeof(AsphaltConcreteWhiteDiagonal2L180Block), typeof(AsphaltConcreteWhiteDiagonal2L270Block))]
    [Serialized]
    [MakesRoads]
    [Road(1.25f)]
    [Wall, Constructed, Solid]
    [IsForm(typeof(RoadExtWhiteDiagonal2LFormType), typeof(AsphaltConcreteItem))]
    [Tag("Constructable")]
    public partial class AsphaltConcreteWhiteDiagonal2LBlock : Block, IColoredBlock
    { }

    [Serialized]
    [MakesRoads]
    [Road(1.25f)]
    [Wall, Constructed, Solid]
    [Tag("Constructable")]
    public partial class AsphaltConcreteWhiteDiagonal2L90Block : Block, IColoredBlock
    { }

    [Serialized]
    [MakesRoads]
    [Road(1.25f)]
    [Wall, Constructed, Solid]
    [Tag("Constructable")]
    public partial class AsphaltConcreteWhiteDiagonal2L180Block : Block, IColoredBlock
    { }

    [Serialized]
    [MakesRoads]
    [Road(1.25f)]
    [Wall, Constructed, Solid]
    [Tag("Constructable")]
    public partial class AsphaltConcreteWhiteDiagonal2L270Block : Block, IColoredBlock
    { }

    [RotatedVariants(typeof(AsphaltConcreteWhiteDiagonal2LEndBlock), typeof(AsphaltConcreteWhiteDiagonal2LEnd90Block), typeof(AsphaltConcreteWhiteDiagonal2LEnd180Block), typeof(AsphaltConcreteWhiteDiagonal2LEnd270Block))]
    [Serialized]
    [MakesRoads]
    [Road(1.25f)]
    [Wall, Constructed, Solid]
    [IsForm(typeof(RoadExtWhiteDiagonal2LEndFormType), typeof(AsphaltConcreteItem))]
    [Tag("Constructable")]
    public partial class AsphaltConcreteWhiteDiagonal2LEndBlock : Block, IColoredBlock
    { }

    [Serialized]
    [MakesRoads]
    [Road(1.25f)]
    [Wall, Constructed, Solid]
    [Tag("Constructable")]
    public partial class AsphaltConcreteWhiteDiagonal2LEnd90Block : Block, IColoredBlock
    { }

    [Serialized]
    [MakesRoads]
    [Road(1.25f)]
    [Wall, Constructed, Solid]
    [Tag("Constructable")]
    public partial class AsphaltConcreteWhiteDiagonal2LEnd180Block : Block, IColoredBlock
    { }

    [Serialized]
    [MakesRoads]
    [Road(1.25f)]
    [Wall, Constructed, Solid]
    [Tag("Constructable")]
    public partial class AsphaltConcreteWhiteDiagonal2LEnd270Block : Block, IColoredBlock
    { }

    [RotatedVariants(typeof(AsphaltConcreteWhiteDiagonal2LModLineBlock), typeof(AsphaltConcreteWhiteDiagonal2LModLine90Block), typeof(AsphaltConcreteWhiteDiagonal2LModLine180Block), typeof(AsphaltConcreteWhiteDiagonal2LModLine270Block))]
    [Serialized]
    [MakesRoads]
    [Road(1.25f)]
    [Wall, Constructed, Solid]
    [IsForm(typeof(RoadExtWhiteDiagonal2LModLineFormType), typeof(AsphaltConcreteItem))]
    [Tag("Constructable")]
    public partial class AsphaltConcreteWhiteDiagonal2LModLineBlock : Block, IColoredBlock
    { }

    [Serialized]
    [MakesRoads]
    [Road(1.25f)]
    [Wall, Constructed, Solid]
    [Tag("Constructable")]
    public partial class AsphaltConcreteWhiteDiagonal2LModLine90Block : Block, IColoredBlock
    { }

    [Serialized]
    [MakesRoads]
    [Road(1.25f)]
    [Wall, Constructed, Solid]
    [Tag("Constructable")]
    public partial class AsphaltConcreteWhiteDiagonal2LModLine180Block : Block, IColoredBlock
    { }

    [Serialized]
    [MakesRoads]
    [Road(1.25f)]
    [Wall, Constructed, Solid]
    [Tag("Constructable")]
    public partial class AsphaltConcreteWhiteDiagonal2LModLine270Block : Block, IColoredBlock
    { }
}
