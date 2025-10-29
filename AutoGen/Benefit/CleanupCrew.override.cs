// Copyright (c) Strange Loop Games. All rights reserved.
// See LICENSE file in the project root for full license information.

namespace Eco.Mods.TechTree
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Players;
    using Eco.Gameplay.Skills;
    using Eco.Mods.TechTree;
    using Eco.Shared.Items;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;
    using Eco.Shared.Utils;
    using Eco.Shared.View;

    /// <summary>
    /// Base talent definition for "PerfectCut"
    /// </summary>
    public partial class PerfectCutTalent : Talent
    {
        public override bool Base => true;
    }

    /// <summary>
    /// Talent group definition for "PerfectCut"
    /// </summary>
    [Serialized]
    [LocDisplayName("Perfect Cut: Logging")]
    [LocDescription("10% chance to automatically split logs into pieces when chopping logs.")]
    public partial class LoggingPerfectCutTalentGroup : TalentGroup
    {
        public LoggingPerfectCutTalentGroup()
        {
            Talents = new Type[]
            {
                typeof(LoggingPerfectCutTalent),
            };
            this.OwningSkill = typeof(LoggingSkill);
            this.Level = 6;
        }
    }

    [Serialized]
    public partial class LoggingPerfectCutTalent : PerfectCutTalent
    {
        public override bool Base { get { return false; } }
        public override Type TalentGroupType { get { return typeof(LoggingPerfectCutTalentGroup); } }
        public LoggingPerfectCutTalent()
        {
            this.Value = 6;
        }
    }
}