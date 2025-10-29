using Eco.Core.Utils;
using Eco.Core.Utils.PropertyScanning;
using Eco.Gameplay.Civics.GameValues;
using Eco.Gameplay.Systems.TextLinks;
using Eco.Shared.Localization;
using Eco.Shared.Math;
using Eco.Shared.Networking;

namespace Civics
{
    [Eco(true)]
    [LocDescription("The surface position")]
    [LocDisplayName("Surface Position")]
    public partial class SurfacePosition : GameValue<Vector3i>
    {
        public override string Title => "Surface Position";
        public override LocString Description()
        {
            return Localizer.DoStr("the surface position of ") + (Position != null ? Position.Description() : Localizer.DoStr("No Location Specified").Style(Eco.Shared.Utils.Text.Styles.Error));
        }
        [Eco(true)]
        [LocDescription("The coordinate")]
        public GameValue<Vector3i> Position { get; set; }
        public override Eval<Vector3i> Value(IContextObject action)
        {
            Vector3i position = Position.Value(action).Val;
            int surfaceHeight = Eco.World.World.GetTopBlockY(Eco.World.World.GetWrappedWorldPosition(position.XZ));
            Vector3i surfacePosition = position.X_Z(surfaceHeight);
            return Eval.Make<Vector3i>(surfacePosition.UILink(), surfacePosition);
        }
    }
}