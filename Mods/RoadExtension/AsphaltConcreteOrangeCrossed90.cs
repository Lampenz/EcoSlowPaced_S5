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
    [IsForm(typeof(RoadExtOrangeCrossed90FormType), typeof(AsphaltConcreteItem))]
    [Tag("Constructable")]
    public partial class AsphaltConcreteOrangeCrossed90Block : Block, IColoredBlock
    { }

    [RotatedVariants(typeof(AsphaltConcreteOrangeCrossed90LineBlock), typeof(AsphaltConcreteOrangeCrossed90Line90Block), typeof(AsphaltConcreteOrangeCrossed90Line180Block), typeof(AsphaltConcreteOrangeCrossed90Line270Block))]
    [Serialized]
    [MakesRoads]
    [Road(1.25f)]
    [Wall, Constructed, Solid]
    [IsForm(typeof(RoadExtOrangeCrossed90LineFormType), typeof(AsphaltConcreteItem))]
    [Tag("Constructable")]
    public partial class AsphaltConcreteOrangeCrossed90LineBlock : Block, IColoredBlock
    { }

    [Serialized]
    [MakesRoads]
    [Road(1.25f)]
    [Wall, Constructed, Solid]
    [Tag("Constructable")]
    public partial class AsphaltConcreteOrangeCrossed90Line90Block : Block, IColoredBlock
    { }

    [Serialized]
    [MakesRoads]
    [Road(1.25f)]
    [Wall, Constructed, Solid]
    [Tag("Constructable")]
    public partial class AsphaltConcreteOrangeCrossed90Line180Block : Block, IColoredBlock
    { }

    [Serialized]
    [MakesRoads]
    [Road(1.25f)]
    [Wall, Constructed, Solid]
    [Tag("Constructable")]
    public partial class AsphaltConcreteOrangeCrossed90Line270Block : Block, IColoredBlock
    { }


    [RotatedVariants(typeof(AsphaltConcreteOrangeCrossed90CornerBlock), typeof(AsphaltConcreteOrangeCrossed90Corner90Block), typeof(AsphaltConcreteOrangeCrossed90Corner180Block), typeof(AsphaltConcreteOrangeCrossed90Corner270Block))]
    [Serialized]
    [MakesRoads]
    [Road(1.25f)]
    [Wall, Constructed, Solid]
    [IsForm(typeof(RoadExtOrangeCrossed90CornerFormType), typeof(AsphaltConcreteItem))]
    [Tag("Constructable")]
    public partial class AsphaltConcreteOrangeCrossed90CornerBlock : Block, IColoredBlock
    { }

    [Serialized]
    [MakesRoads]
    [Road(1.25f)]
    [Wall, Constructed, Solid]
    [Tag("Constructable")]
    public partial class AsphaltConcreteOrangeCrossed90Corner90Block : Block, IColoredBlock
    { }

    [Serialized]
    [MakesRoads]
    [Road(1.25f)]
    [Wall, Constructed, Solid]
    [Tag("Constructable")]
    public partial class AsphaltConcreteOrangeCrossed90Corner180Block : Block, IColoredBlock
    { }

    [Serialized]
    [MakesRoads]
    [Road(1.25f)]
    [Wall, Constructed, Solid]
    [Tag("Constructable")]
    public partial class AsphaltConcreteOrangeCrossed90Corner270Block : Block, IColoredBlock
    { }
}
