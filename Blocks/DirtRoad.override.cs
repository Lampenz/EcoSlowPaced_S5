// Copyright (c) Strange Loop Games. All rights reserved.
// See LICENSE file in the project root for full license information.

namespace Eco.Mods.TechTree
{
    using System;
    using System.ComponentModel;
    using Eco.Core.Items;
    using Eco.Gameplay.Blocks;
    using Eco.Gameplay.Items;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;
    using Eco.World.Blocks;

    [Serialized, LocDisplayName("Dirt Road")]
    [Tag("Samplable", Unset = true), Tag("Tillable", Unset = true), Fertile(Unset = true), Tillable(Unset = true), BiomeBlock(null), BiomeDestructible(typeof(DesertSandBlock))]
    [Road(1.05f)]
    [UsesRamp(typeof(DirtRampItem))]
    public class DirtRoadBlock : DirtBlock, IRepresentsItem
    {
        public virtual Type RepresentedItemType => typeof(DirtItem);
    }

    public partial class DirtRampBlock
    {
        public override Type RepresentedItemType => typeof(DirtRampItem);
    }

    [Serialized]
    [LocDisplayName("Dirt Road")]
    [LocDescription("A dusty surface formed by tampering dirt with a road tool. It's servicable for any wheeled vehicle.")]
    [Weight(30000)]
    [Ecopedia("Blocks", "Roads", createAsSubPage: true)]
    [Category("Hidden")]
    public partial class DirtRoadItem : BlockItem<DirtRoadBlock>
    {
    }
}
