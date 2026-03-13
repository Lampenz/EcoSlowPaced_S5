namespace Eco.Gameplay.Components.VehicleModules
{
    using System.Linq;
    using Eco.Core.Controller;
    using Eco.Core.Items;
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
    using Eco.Shared.Utils;
    using Eco.World;
    using Eco.World.Blocks;
    using Vector3 = System.Numerics.Vector3;

    [Serialized]
    [RequireComponent(typeof(VehicleComponent))]
    [NoIcon]
    public class RoadCompactorComponent : WorldObjectComponent
    {
        // Roller width: center + 1 block to the right (perpendicular to movement)
        private static readonly Vector3i[] compactionArea = new Vector3i[]
        {
            new Vector3i(0, -1, 0),
            new Vector3i(1, -1, 0),
        };

        // Calorie cost reduced by BasicEngineering skill level (same curve as vanilla tools)
        private static readonly SkillModifiedValue caloriesPerAction = new SkillModifiedValue(
            15, // base calories
            new MultiplicativeStrategy(new float[] { 1, 0.95f, 0.93f, 0.9f, 0.88f, 0.85f, 0.83f, 0.8f }),
            typeof(BasicEngineeringSkill),
            typeof(RoadCompactorComponent),
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

            // Offset detection 0.5 to the left (local -X) so compaction aligns with the roller
            var leftOffset  = this.Parent.Rotation.RotateVector(new Vector3(-0.5f, 0, 0));
            var detectionPos = (this.Parent.Position + leftOffset).XYZi();

            var targets = compactionArea
                .MoveAndRotate(detectionPos, this.Parent.Rotation)
                .Where(pos => World.GetBlock(pos).GetType().HasTag(BlockTags.CanBeRoad));

            if (!targets.Any()) return;

            AtomicActions.ChangeBlockNow(
                newType: typeof(DirtRoadBlock),
                context: new MultiblockActionContext()
                {
                    Player                = this.vehicle.Driver,
                    Area                  = targets,
                    ToolUsed              = this.Parent.CreatingItem as Item,
                    ActionDescription     = GameActionDescription.DoStr("compact a block", "compacting a block"),
                    GameActionConstructor = () => new TampRoad(),
                    CaloriesPerAction     = caloriesPerAction.GetCurrentValue(this.vehicle.Driver?.User)
                }, notify: true, genericNotify: false);
        }
    }
}
