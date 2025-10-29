using Eco.Core.Controller;
using Eco.Core.Plugins.Interfaces;
using Eco.Core.Systems;
using Eco.Core.Utils;
using Eco.Gameplay.Economy;
using Eco.Gameplay.Economy.WorkParties;
using Eco.Gameplay.Objects;
using Eco.Gameplay.Players;
using Eco.Shared.IoC;
using Eco.Shared.Items;
using Eco.Shared.Localization;
using Eco.Shared.Networking;
using Eco.Shared.Services;
using Eco.Shared.Utils;
using System;
using System.Linq;

namespace Eco.Mods.TechTree
{
    public partial class ContractBoardObject : WorldObject
    {
        protected override void ComponentsInitialized()
        {
            base.ComponentsInitialized();
            ContractBoardComponent contractBoardComponent = GetComponent<ContractBoardComponent>();

            contractBoardComponent.ContainedWorkables.AllWorkables.Callbacks.OnAdd.Add((_, _) => OnWorkPartyOrContractAdded(contractBoardComponent));
        }
        private static void OnWorkPartyOrContractAdded(ContractBoardComponent contractBoardComponent)
        {
            foreach (WorkParty workParty in contractBoardComponent.WorkParties.Where(w => w.State < ProposableState.Active))
            {
                if (workParty.Creator.IsAdmin)
                    continue;
                
                CancelWorkParty(workParty);
            }
            contractBoardComponent.Changed(nameof(ContractBoardComponent.WorkParties));
        }

        private static void CancelWorkParty(WorkParty workParty)
        {
            workParty.Cancel(workParty.Creator.Player);
            workParty.Creator.MsgOrMailLoc($"Work Parties are only usable by admins!", NotificationCategory.Auth, NotificationStyle.Warning);
        }
    }
}