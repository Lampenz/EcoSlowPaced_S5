namespace Eco.Gameplay.Components.VehicleModules
{
    using System.Linq;
    using Eco.Core.Controller;
    using Eco.Gameplay.Components.Storage;
    using Eco.Gameplay.Objects;
    using Eco.Gameplay.Players;
    using Eco.Shared.Serialization;

    [Serialized]
    [RequireComponent(typeof(PublicStorageComponent))]
    [NoIcon]
    public class StorageFillVisualComponent : WorldObjectComponent
    {
        private int currentFillCount = -1;

        public override void Initialize()
        {
            base.Initialize();
            var storage = this.Parent.GetComponent<PublicStorageComponent>();
            storage.Inventory.OnChanged.Add(this.OnInventoryChanged);
            // Restore visual state on server restart (animated states aren't persisted)
            this.UpdateFillLevel(storage);
        }

        private void OnInventoryChanged(User user = null)
        {
            var storage = this.Parent.GetComponent<PublicStorageComponent>();
            if (storage?.Inventory == null) return;
            this.UpdateFillLevel(storage);
        }

        private void UpdateFillLevel(PublicStorageComponent storage)
        {
            int nonEmpty = storage.Inventory.NonEmptyStacks.Count();
            int total    = storage.Inventory.Stacks.Count();

            if (nonEmpty == this.currentFillCount) return;

            for (int i = 1; i <= total; i++)
                this.Parent.SetAnimatedState($"Fill{i}", i <= nonEmpty);

            this.currentFillCount = nonEmpty;
        }
    }
}
