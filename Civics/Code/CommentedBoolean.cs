using Eco.Core.Controller;
using Eco.Core.Utils;
using Eco.Core.Utils.PropertyScanning;
using Eco.Gameplay.Civics.GameValues;
using Eco.Shared.Localization;
using Eco.Shared.Networking;
using Eco.Shared.Utils;

namespace Civics
{
    [Eco(true)]
    [LocDisplayName("Commented true/false value")]
    [LocDescription("A true/false value with an optional comment.")]
    [LocCategory("Logic")]
    public class CommentedBoolean : GameValue<bool>
    {
        [Eco]
        public GameValue<bool> Statement { get; set; }

        [Eco, LargeUI, AllowNullInView]
        [LocDescription("Optional comment.")]
        public string Comment { get; set; }

        public override LocString Description()
        {
            LocString description = Localizer.DoStr(Statement.DescribeNullSafe());
            if (!string.IsNullOrWhiteSpace(Comment))
            {
                description += Text.Style(Text.Styles.Info, $" ({Comment})");
            }
            return description;
        }
        public override Eval<bool> Value(IContextObject action)
        {
            return Statement?.Value(action);
        }
    }
}
