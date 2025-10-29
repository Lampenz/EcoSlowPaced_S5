using Eco.Core.Utils;
using Eco.Core.Utils.PropertyScanning;
using Eco.Gameplay.Civics.GameValues;
using Eco.Gameplay.Economy;
using Eco.Shared.Localization;
using Eco.Shared.Networking;
using Eco.Shared.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Civics
{
    [Eco(true)]
    [LocCategory("Economy")]
    [LocDescription("Check the type of account")]
    [LocDisplayName("Is Account Type")]
    public partial class IsAccountType : GameValue<bool>
    {
        public override LocString Description()
        {
            LocStringBuilder descriptionBuilder = new LocStringBuilder();
            descriptionBuilder.Append(Account?.DescribeNullSafe() + " is a " + TargetAccountType.GetEnumLocDisplayName());
            return descriptionBuilder.ToLocString();
        }

        [Eco(true)]
        [LocDescription("The account.")]
        public GameValue<BankAccount> Account { get; set; }

        [Eco(true)]
        [LocDescription("Type of account to check against.\n" +
            "Personal Bank Accounts are the ones which every player starts with.\n" +
            "Regular Bank Accounts are player made but not government owned.\n" +
            "Treasury Bank Account is the default treasury account.\n" +
            "Contract Escrow Bank Account is where money is held during contracts.\n" +
            "Regular Government Bank Accounts are player made government accounts.")]
        public AccountType TargetAccountType { get; set; }

        private static readonly Dictionary<AccountType, Type> AccountTypesMap = new Dictionary<AccountType, Type>()
        {
            { AccountType.PersonalBankAccount, typeof(PersonalBankAccount) },
            { AccountType.RegularBankAccount, typeof(BankAccount) },
            { AccountType.TreasuryBankAccount, typeof(TreasuryBankAccount) },
            { AccountType.ContractEscrowBankAccount, typeof(ContractEscrowBankAccount) },
            { AccountType.RegularGovernmentBankAccount, typeof(GovernmentBankAccount) }
        };

        public override Eval<bool> Value(IContextObject action)
        {
            if (!this.GetValueOrFailNullSafe(Account?.Value(action), nameof(Account), out BankAccount account, out Eval<bool> failure, false))
            {
                return failure;
            }
            Type accountType = account?.GetType();
            bool checkPassed = AccountTypesMap[TargetAccountType] == accountType;

            LocStringBuilder descriptionBuilder = new LocStringBuilder();
            descriptionBuilder.Append(account?.UILinkNullSafe() + (checkPassed ? " is a " : " is not a ") + TargetAccountType.GetEnumLocDisplayName());
            
            return Eval.Make<bool>(descriptionBuilder.ToLocString(), checkPassed);
        }
        [Eco(true)]
        [Localized(true, false, "", false)]
        [Serialized]
        public enum AccountType
        {
            PersonalBankAccount = 0,
            RegularBankAccount = 1,
            TreasuryBankAccount = 2,
            ContractEscrowBankAccount = 3,
            RegularGovernmentBankAccount = 4,
        }
    }
}