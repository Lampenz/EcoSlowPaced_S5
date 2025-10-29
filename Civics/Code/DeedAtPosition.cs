using Eco.Core.Controller;
using Eco.Core.Systems;
using Eco.Core.Utils;
using Eco.Core.Utils.PropertyScanning;
using Eco.Gameplay.Aliases;
using Eco.Gameplay.Civics.GameValues;
using Eco.Gameplay.Players;
using Eco.Gameplay.Property;
using Eco.Gameplay.Systems.Controllers;
using Eco.Shared.Localization;
using Eco.Shared.Math;
using Eco.Shared.Networking;
using Eco.Shared.View;
using System.ComponentModel;

namespace Civics
{
    [Eco(true)]
    [LocDescription("The deed at the position")]
    [LocDisplayName("Deed At Position")]
    public partial class DeedAtPosition : GameValue<Deed>
    {
        public override string Title => "Deed";
        public override LocString Description()
        {
            return Localizer.DoStr("the deed at ") + (Position != null ? Position.Description() : Localizer.DoStr("No Location Specified").Style(Eco.Shared.Utils.Text.Styles.Error));
        }
        [Eco(true)]
        [LocDescription("The position")]
        public GameValue<Vector3i> Position { get; set; }
        public override Eval<Deed> Value(IContextObject action)
        {
            Vector3i position = Position.Value(action).Val;
            Deed deed = PropertyManager.GetDeedWorldPos(position.XZ);

            return Eval.Make<Deed>(Localizer.DoStr("Deed"), deed);
        }
    }
}