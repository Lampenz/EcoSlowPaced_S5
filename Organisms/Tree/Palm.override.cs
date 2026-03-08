// Copyright (c) Strange Loop Games. All rights reserved.
// See LICENSE file in the project root for full license information.

namespace Eco.Mods.Organisms
{
    using System;
    using System.Collections.Generic;
    using Eco.Mods.TechTree;
    using Eco.Simulation.Types;
    using Eco.World.Blocks;
    using Range = Eco.Shared.Math.Range;

    public partial class Palm : TreeEntity
    {
        public partial class PalmSpecies : TreeSpecies
        {
            partial void SetDefaultProperties()
            {
                // Lifetime
                this.TreeHealth = 6f;
                this.LogHealth = 2f;
                // Resources
                this.ChanceToSpawnDebris = 0.3f;
                // Visuals
                this.BranchingDef = new List<TreeBranchDef>()
                {
                    new TreeBranchDef() { Name = "Branch0", Health = 3f, LeafPoints = 1, GrowthStartTime = new Range(0f, 0f), GrowthEndTime = new Range(1f, 1f) },
                };
                this.TopBranchLeafPoints = 1;
                this.TopBranchHealth = 3;
                this.BranchRotations = null;
                this.RandomYRotation = true;
                this.BranchCount = new Range(1f, 1f);
                this.BlockType = new BlockType(typeof(TreeBlock));
                this.DebrisType = typeof(PalmTreeDebrisBlock);
                this.DebrisResources = new Dictionary<Type, Range>()
                {
                    { typeof(WoodPulpItem), new Range(4, 5) },
                    { typeof(HeartOfPalmItem), new Range(0, 1) },
                    { typeof(PalmSeedItem), new Range(0, 1) },
                    { typeof(CoconutItem), new Range(0, 1) },
                };
                this.TrunkResources = new Dictionary<Type, Range>()
                {
                    { typeof(WoodPulpItem), new Range(4, 5) }
                };
                this.XZScaleRange = new Range(.8f, 1.4f);
                this.YScaleRange = new Range(.8f, 1.4f);
                this.Density = 950f; // High density is required for proper mass and fall force calculations, due to big height
            }
        }
    }
}
