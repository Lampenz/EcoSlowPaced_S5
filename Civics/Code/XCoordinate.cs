using Eco.Core.Utils;
using Eco.Core.Utils.PropertyScanning;
using Eco.Gameplay.Civics.GameValues;
using Eco.Shared.Localization;
using Eco.Shared.Math;
using Eco.Shared.Networking;

namespace Civics
{
    [Eco(true)]
    [LocDescription("The X coordinate at the position")]
    [LocDisplayName("X Coordinate At Position")]
    public partial class XCoordinate : GameValue<float>
    {
        public override string Title => "X Coordinate";
        public override LocString Description()
        {
            return Localizer.DoStr("the X coordinate of ") + (Position != null ? Position.Description() : Localizer.DoStr("No Location Specified").Style(Eco.Shared.Utils.Text.Styles.Error));
        }
        [Eco(true)]
        [LocDescription("The position")]
        public GameValue<Vector3i> Position { get; set; }
        public override Eval<float> Value(IContextObject action)
        {
            Vector3i position = Position.Value(action).Val;
            float x = position.X;

            return Eval.Make<float>(Localizer.DoStr("X:") + x.ToString(), x);
        }
    }
}