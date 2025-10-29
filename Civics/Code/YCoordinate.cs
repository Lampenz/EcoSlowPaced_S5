using Eco.Core.Utils;
using Eco.Core.Utils.PropertyScanning;
using Eco.Gameplay.Civics.GameValues;
using Eco.Shared.Localization;
using Eco.Shared.Math;
using Eco.Shared.Networking;

namespace Civics
{
    [Eco(true)]
    [LocDescription("The Y coordinate at the position")]
    [LocDisplayName("Y Coordinate At Position")]
    public partial class YCoordinate : GameValue<float>
    {
        public override string Title => "Y Coordinate";
        public override LocString Description()
        {
            return Localizer.DoStr("the Y coordinate of ") + (Position != null ? Position.Description() : Localizer.DoStr("No Location Specified").Style(Eco.Shared.Utils.Text.Styles.Error));
        }
        [Eco(true)]
        [LocDescription("The position")]
        public GameValue<Vector3i> Position { get; set; }
        public override Eval<float> Value(IContextObject action)
        {
            Vector3i position = Position.Value(action).Val;
            float y = position.Y;

            return Eval.Make<float>(Localizer.DoStr("Y:") + y.ToString(), y);
        }
    }
}