namespace Eco.Gameplay.Components.VehicleModules
{
    using System.Linq;
    using Eco.Gameplay.Components.Storage;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.GameActions;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Objects;
    using Eco.Gameplay.Skills;
    using Eco.Mods.TechTree;
    using Eco.Shared.Localization;
    using Eco.Shared.Math;
    using Eco.Shared.Serialization;
    using Eco.Shared.SharedTypes;
    using Eco.Core.Controller;
    using Eco.World;
    using Eco.World.Blocks;
    using Vector3 = System.Numerics.Vector3;

    [Serialized]
    [RequireComponent(typeof(VehicleComponent))]
    [RequireComponent(typeof(PublicStorageComponent))]
    [NoIcon]
    public class WheeledReaperComponent : WorldObjectComponent
    {
        // Harvesting strip: 2 blocks wide centered on the reaper, at and below vehicle level
        private static readonly Vector3i[] harvestArea = new Vector3i[]
        {
            new Vector3i(0, 0, 0),
            new Vector3i(1, 0, 0),
            new Vector3i(0, -1, 0),
            new Vector3i(1, -1, 0),
        };

        // Base 15 cal, reduced by Gathering skill level
        private static readonly SkillModifiedValue caloriesPerAction = new SkillModifiedValue(
            15,
            new MultiplicativeStrategy(new float[] { 1, 0.95f, 0.93f, 0.9f, 0.88f, 0.85f, 0.83f, 0.8f }),
            typeof(GatheringSkill),
            typeof(WheeledReaperComponent),
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

            // Offset detection 0.5 to the left (local -X) so harvest area aligns centered on the harrow
            var leftOffset   = this.Parent.Rotation.RotateVector(new Vector3(-0.5f, 0, 0));
            var detectionPos = (this.Parent.Position + leftOffset).XYZi();

            var targets = harvestArea
                .MoveAndRotate(detectionPos, this.Parent.Rotation)
                .Where(pos => World.GetBlock(pos).GetType().HasTag(BlockTags.Reapable)
                           || World.GetBlock(pos).GetType().HasTag(BlockTags.Clearable));

            if (!targets.Any()) return;

            AtomicActions.HarvestPlantNow(
                context: new MultiblockActionContext()
                {
                    Player            = this.vehicle.Driver,
                    Area              = targets,
                    ToolUsed          = this.Parent.CreatingItem as Item,
                    ActionDescription = GameActionDescription.DoStr("harvest crops", "harvesting crops"),
                    CaloriesPerAction = caloriesPerAction.GetCurrentValue(this.vehicle.Driver?.User)
                },
                harvestTo: storage.Inventory,
                reapableOnly: false,
                notify: true);
        }
    }
}
