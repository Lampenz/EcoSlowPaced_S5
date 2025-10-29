using Eco.Core.Utils;
using Eco.Core.Utils.PropertyScanning;
using Eco.Gameplay.Civics.GameValues;
using Eco.Shared.Localization;
using Eco.Shared.Math;
using Eco.Shared.Networking;

namespace Civics
{
    [Eco(true)]
    [LocDescription("The Z coordinate at the position")]
    [LocDisplayName("Z Coordinate At Position")]
    public partial class ZCoordinate : GameValue<float>
    {
        public override string Title => "Z Coordinate";
        public override LocString Description()
        {
            return Localizer.DoStr("the Z coordinate of ") + (Position != null ? Position.Description() : Localizer.DoStr("No Location Specified").Style(Eco.Shared.Utils.Text.Styles.Error));
        }
        [Eco(true)]
        [LocDescription("The position")]
        public GameValue<Vector3i> Position { get; set; }
        public override Eval<float> Value(IContextObject action)
        {
            Vector3i position = Position.Value(action).Val;
            float z = position.Z;

            return Eval.Make<float>(Localizer.DoStr("Z:") + z.ToString(), z);
        }
    }
}