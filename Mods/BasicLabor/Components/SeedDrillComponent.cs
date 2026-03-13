namespace Eco.Gameplay.Components.VehicleModules
{
    using System.Linq;
    using Eco.Gameplay.Components.Storage;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Objects;
    using Eco.Gameplay.Plants;
    using Eco.Gameplay.Skills;
    using Eco.Mods.TechTree;
    using Eco.Shared.Localization;
    using Eco.Shared.Math;
    using Eco.Shared.Serialization;
    using Eco.Core.Controller;
    using Eco.World;
    using Eco.World.Blocks;
    using Vector3 = System.Numerics.Vector3;

    [Serialized]
    [RequireComponent(typeof(VehicleComponent))]
    [RequireComponent(typeof(PublicStorageComponent))]
    [NoIcon]
    public class SeedDrillComponent : WorldObjectComponent
    {
        // Seeding strip: 2 blocks wide, at vehicle level and one below (for terrain variation)
        private static readonly Vector3i[] seedingArea = new Vector3i[]
        {
            new Vector3i(0, 0, 0),
            new Vector3i(1, 0, 0),
            new Vector3i(0, -1, 0),
            new Vector3i(1, -1, 0),
        };

        // Calorie cost per seed planted, reduced by Farming skill level
        private static readonly SkillModifiedValue caloriesPerAction = new SkillModifiedValue(
            5,
            new MultiplicativeStrategy(new float[] { 1, 0.95f, 0.93f, 0.9f, 0.88f, 0.85f, 0.83f, 0.8f }),
            typeof(FarmingSkill),
            typeof(SeedDrillComponent),
            Localizer.DoStr("calorie consumption"),
            DynamicValueType.CalorieReduction);

        protected VehicleComponent vehicle;

        public override void Initialize()
        {
            this.vehicle = this.Parent.GetComponent<VehicleComponent>();
            base.Initialize();
            this.vehicle.VehicleMovedEvent.AddUnique(this.OnMoved);
        }

        public void OnMoved()
        {
            if (this.vehicle.Driver == null) return;

            var storage = this.Parent.GetComponent<PublicStorageComponent>();
            if (storage?.Inventory == null) return;

            // Offset 0.5 to the left so the 2-wide strip is centered on the drill
            var leftOffset   = this.Parent.Rotation.RotateVector(new Vector3(-0.5f, 0, 0));
            var detectionPos = (this.Parent.Position + leftOffset).XYZi();
            var seeder       = this.vehicle.Driver.User;
            var calCost      = caloriesPerAction.GetCurrentValue(seeder);

            foreach (var targetPos in seedingArea.MoveAndRotate(detectionPos, this.Parent.Rotation))
            {
                var stack = storage.Inventory.NonEmptyStacks.FirstOrDefault(x => x.Item is SeedItem);
                if (stack == null) return;

                if (World.GetBlock(targetPos + Vector3i.Down).Is<Tilled>() && World.GetBlock(targetPos).Is<Empty>())
                {
                    var seed = (SeedItem)stack.Item;
                    var result = seed.TrySeedFromInventory(stack, storage.Inventory, targetPos, seeder);
                    if (result.Success)
                        seeder.Stomach.BurnCalories(calCost, true);
                }
            }
        }
    }
}
