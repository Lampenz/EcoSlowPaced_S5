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
    [LocDescription("The owner of the deed at the position")]
    [LocDisplayName("Landowner")]
    public partial class Landowner : GameValue<User>
    {
        public override string Title => "Landowner";
        public override LocString Description()
        {
            return Localizer.DoStr("the first owner of ") + (Deed != null ? Deed.Description() : Localizer.DoStr("No Deed Specified").Style(Eco.Shared.Utils.Text.Styles.Error));
        }
        [Eco(true)]
        [LocDescription("The land deed")]
        public GameValue<Deed> Deed { get; set; } = new DeedAtPosition();

        public override Eval<User> Value(IContextObject action)
        {
            User landOwner = (Deed?.Value(action))?.Val?.Owner?.FirstUser();

            return Eval.Make<User>(Localizer.DoStr("Landowner"), landOwner);
        }
    }

    [Eco(true)]
    [LocDescription("The owners of the deed at the position")]
    [LocDisplayName("Landowner")]
    [NonSelectable]
    public partial class Landowners : GameValue<IAlias>, IController, IViewController, IHasUniversalID, INotifyPropertyChanged, IValidity, IDescribable
    {
        public override string Title => "Landowners";
        public override LocString Description()
        {
            return Localizer.DoStr("the owners of ") + (Deed != null ? Deed.Description() : Localizer.DoStr("No Deed Specified").Style(Eco.Shared.Utils.Text.Styles.Error));
        }
        [Eco(true)]
        [LocDescription("The land deed")]
        public GameValue<Deed> Deed { get; set; } = new DeedAtPosition();

        public override Eval<IAlias> Value(IContextObject action)
        {
            IAlias landOwner = (Deed?.Value(action))?.Val?.Owner;

            return Eval.Make<IAlias>(Localizer.DoStr("Landowners"), landOwner);
        }
    }
}