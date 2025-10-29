using Eco.Core.Utils;
using Eco.Core.Utils.PropertyScanning;
using Eco.Gameplay.Civics.GameValues;
using Eco.Shared.Localization;
using Eco.Shared.Utils;
using Eco.Shared.Networking;
using Eco.Core.Controller;
using System;

namespace Civics
{
    [Eco(true)]
    [LocDescription("A random integer between two values.")]
    [LocDisplayName("Random Integer")]
    [LocCategory("Math")]
    public partial class RandomInteger : GameValue<float>
    {
        public override LocString Description()
        {
            LocStringBuilder descriptionBuilder = new LocStringBuilder();
            descriptionBuilder.Append("a random integer between ");
            descriptionBuilder.Append(Text.StyledNum(LowerBound));
            descriptionBuilder.Append(" and ");
            descriptionBuilder.Append(Text.StyledNum(UpperBound));
            return descriptionBuilder.ToLocString();
        }
        [Eco(true), NegativeAllowed]
        [LocDescription("The lower bound, inclusive.")]
        public int LowerBound { get; set; }
        [Eco(true), NegativeAllowed]
        [LocDescription("The upper bound, inclusive.")]
        public int UpperBound { get; set; }

        public override Eval<float> Value(IContextObject action)
        {
            // ensure min and max are the right way around
            int min = Math.Min(LowerBound, UpperBound);
            int max = Math.Max(LowerBound, UpperBound);
            int val = RandomUtil.Range(min, max + 1); //the Range function uses an exclusive upper bound, so we need to add one to make it inclusive
            return Eval.Make<float>(Localizer.DoStr("Random integer: ") + val.ToString(), val);
        }
    }
}