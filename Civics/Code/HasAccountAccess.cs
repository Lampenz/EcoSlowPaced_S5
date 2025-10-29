using Eco.Core.Utils;
using Eco.Core.Utils.PropertyScanning;
using Eco.Gameplay.Civics.GameValues;
using Eco.Gameplay.Civics.GameValues.Values;
using Eco.Gameplay.Economy;
using Eco.Gameplay.GameActions;
using Eco.Gameplay.Players;
using Eco.Shared.Items;
using Eco.Shared.Localization;
using Eco.Shared.Networking;
using Eco.Shared.Serialization;

namespace Civics
{
    [Eco(true)]
    [LocCategory("Economy")]
    [LocDescription("Check whether a player has access to a bank account")]
    [LocDisplayName("Has Account Access")]
    public partial class HasAccountAccess : GameValue<bool>
    {
        public override LocString Description()
        {
            LocStringBuilder descriptionBuilder = new LocStringBuilder();
            descriptionBuilder.Append(Citizen?.DescribeNullSafe());
            switch (TargetAccessType)
            {
                case AccountAccessType.IsManager:
                    descriptionBuilder.Append(Localizer.DoStr(" is a manager of "));
                    break;
                case AccountAccessType.IsNotManager:
                    descriptionBuilder.Append(Localizer.DoStr(" is not a manager of "));
                    break;
                case AccountAccessType.IsUser:
                    descriptionBuilder.Append(Localizer.DoStr(" is a user of "));
                    break;
                case AccountAccessType.IsUserOnly:
                    descriptionBuilder.Append(Localizer.DoStr(" is a user but not a manager of "));
                    break;
                default:
                    descriptionBuilder.Append(Localizer.DoStr(" does not have access to "));
                    break;
            }
            descriptionBuilder.Append(Account?.DescribeNullSafe());
            return descriptionBuilder.ToLocString();
        }

        [Eco(true)]
        [LocDescription("The citizen.")]
        public GameValue<User> Citizen { get; set; }

        [Eco(true)]
        [LocDescription("Access Type")]
        public AccountAccessType TargetAccessType { get; set; }

        [Eco(true)]
        [LocDescription("The account.")]
        public GameValue<BankAccount> Account { get; set; }
        public override Eval<bool> Value(IContextObject action)
        {
            //If either the citizen or account is invalid or its value is null, return a failure value.
            //Failure means the citizen has no access to the account, so return false for every case except when we're specifically checking for no access.
            if (!this.GetValueOrFailNullSafe(Citizen?.Value(action), nameof(Citizen), out User citizen, out Eval<bool> failure, TargetAccessType == AccountAccessType.HasNoAccess))
            {
                return failure;
            }
            if (!this.GetValueOrFailNullSafe(Account?.Value(action), nameof(Account), out BankAccount account, out failure, TargetAccessType == AccountAccessType.HasNoAccess))
            {
                return failure;
            }

            LocStringBuilder descriptionBuilder = new LocStringBuilder();
            descriptionBuilder.Append(citizen?.UILinkNullSafe());

            bool isUser = account.DualPermissions.InList(citizen, AccountAccess.Use);
            bool isManager = account.DualPermissions.InList(citizen, AccountAccess.Manage);

            bool result;
            switch (TargetAccessType)
            {
                case AccountAccessType.IsManager:
                    result = isManager;
                    if (result)
                    {
                        descriptionBuilder.Append(Localizer.DoStr(" is a manager of "));
                    }
                    else
                    {
                        descriptionBuilder.Append(Localizer.DoStr(" is not a manager of "));
                    }
                    break;
                case AccountAccessType.IsNotManager:
                    result = !isManager;
                    if (result)
                    {
                        descriptionBuilder.Append(Localizer.DoStr(" is not a manager of "));
                    }
                    else
                    {
                        descriptionBuilder.Append(Localizer.DoStr(" is a manager of "));
                    }
                    break;
                case AccountAccessType.IsUser:
                    result = isUser;
                    if (result)
                    {
                        descriptionBuilder.Append(Localizer.DoStr(" is a user of "));
                    }
                    else
                    {
                        descriptionBuilder.Append(Localizer.DoStr(" is not a user of "));
                    }
                    break;
                case AccountAccessType.IsUserOnly:
                    result = isUser && !isManager;
                    if (result)
                    {
                        descriptionBuilder.Append(Localizer.DoStr(" is a user but not a manager of "));
                    }
                    else if (isUser && isManager)
                    {
                        descriptionBuilder.Append(Localizer.DoStr(" is a user but also a manager of "));
                    }
                    else
                    {
                        descriptionBuilder.Append(Localizer.DoStr(" does not have access to "));
                    }
                    break;
                default:
                    result = !isUser && !isManager;
                    if (result)
                    {
                        descriptionBuilder.Append(Localizer.DoStr(" does not have access to "));
                    }
                    else
                    {
                        descriptionBuilder.Append(Localizer.DoStr(" does have access to "));
                    }
                    break;
            }
            descriptionBuilder.Append(account?.UILinkNullSafe());
            return Eval.Make<bool>(descriptionBuilder.ToLocString(), result);
        }
        [Eco(true)]
        [Localized(true, false, "", false)]
        [Serialized]
        public enum AccountAccessType
        {
            IsManager = 0,
            IsNotManager = 1,
            IsUser = 2,
            IsUserOnly = 3,
            HasNoAccess = 4,
        }
    }
}