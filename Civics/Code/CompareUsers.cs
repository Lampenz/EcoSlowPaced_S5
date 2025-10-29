using Eco.Core.Utils;
using Eco.Core.Utils.PropertyScanning;
using Eco.Gameplay.Civics.GameValues;
using Eco.Gameplay.Players;
using Eco.Gameplay.Systems.TextLinks;
using Eco.Shared.Localization;
using Eco.Shared.Networking;
using Eco.Shared.Serialization;

namespace Civics
{
    [Eco(true)]
    [LocCategory("Logic")]
    [LocDescription("Check if two players are the same or not")]
    [LocDisplayName("Compare Players")]
    public partial class CompareUsers : GameValue<bool>
    {
        public override LocString Description()
        {
            LocStringBuilder descriptionBuilder = new LocStringBuilder();
            descriptionBuilder.Append(FirstUser.DescribeNullSafe());
            descriptionBuilder.Append(Comparison == BasicComparisonType.Same ? " is " : " is not ");
            descriptionBuilder.Append(SecondUser.DescribeNullSafe());
            return descriptionBuilder.ToLocString();
        }
        [Eco(true)]
        [LocDescription("The first player.")]
        [LocDisplayName("First Player")]
        public GameValue<User> FirstUser { get; set; }
        [Eco(true)]
        [LocDescription("The type of the comparison.")]
        public BasicComparisonType Comparison { get; set; }

        [Eco(true)]
        [LocDescription("The second player.")]
        [LocDisplayName("Second Player")]
        public GameValue<User> SecondUser { get; set; }

        public override Eval<bool> Value(IContextObject action)
        {
            User firstUser = FirstUser?.Value(action).Val;
            User secondUser = SecondUser?.Value(action).Val;

            bool comparisonPassed;
            if (firstUser == null || secondUser == null)
            {
                comparisonPassed = Comparison == BasicComparisonType.Different;
            }
            else
            {
                comparisonPassed = ((firstUser == secondUser) == (Comparison == BasicComparisonType.Same));
            }
            LocString description;
            switch (Comparison)
            {
                case BasicComparisonType.Same:
                    description = firstUser.UILinkNullSafe() + " is " + secondUser.UILinkNullSafe();
                    break;
                default:
                    description = firstUser.UILinkNullSafe() + " is not " + secondUser.UILinkNullSafe();
                    break;
            }
            
            return Eval.Make<bool>(description, comparisonPassed);
        }

        [Eco(true)]
        [Localized(true, false, "", false)]
        [Serialized]
        public enum BasicComparisonType
        {
            Same = 0,
            Different = 1
        }
    }
}