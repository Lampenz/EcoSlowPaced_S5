using Eco.Core.Utils;
using Eco.Core.Utils.PropertyScanning;
using Eco.Gameplay.Civics.GameValues;
using Eco.Shared.Localization;
using Eco.Shared.Math;
using Eco.Shared.Utils;
using Eco.Shared.Networking;
using Eco.Core.Controller;

namespace Civics
{
    [Eco(true)]
    [LocDescription("A random decimal number between two values.")]
    [LocDisplayName("Random Decimal Number")]
    [LocCategory("Math")]
    public partial class RandomFloat : GameValue<float>
    {
        public override LocString Description()
        {
            LocStringBuilder descriptionBuilder = new LocStringBuilder();
            descriptionBuilder.Append("a random decimal number between ");
            descriptionBuilder.Append((LowerBound == null || LowerBound is GameValueWrapper<float>) ? LowerBound.DescribeNullSafe() : $"({LowerBound.DescribeNullSafe()})");
            descriptionBuilder.Append(" and ");
            descriptionBuilder.Append((UpperBound == null || UpperBound is GameValueWrapper<float>) ? UpperBound.DescribeNullSafe() : $"({UpperBound.DescribeNullSafe()})");
            return descriptionBuilder.ToLocString();
        }
        [Eco(true), NegativeAllowed]
        [LocDescription("The lower bound.")]
        public GameValue<float> LowerBound { get; set; }
        [Eco(true), NegativeAllowed]
        [LocDescription("The upper bound.")]
        public GameValue<float> UpperBound { get; set; }

        public override Eval<float> Value(IContextObject action)
        {
            float val = RandomUtil.Range(LowerBound.Value(action).Val, UpperBound.Value(action).Val);
            return Eval.Make<float>(Localizer.DoStr("Random decimal number: ") + val.ToString(), val);
        }
    }
}