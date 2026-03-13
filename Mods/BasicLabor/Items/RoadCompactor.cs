namespace Eco.Mods.TechTree
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using Eco.Core.Controller;
    using Eco.Core.Items;
    using Eco.Core.Utils;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Components.Auth;
    using Eco.Gameplay.Components.VehicleModules;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Objects;
    using Eco.Gameplay.Occupancy;
    using Eco.Gameplay.Systems.Exhaustion;
    using Eco.Gameplay.Systems.NewTooltip;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Shared.Math;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;
    using Eco.Shared.Items;
    using Eco.Gameplay.Items.Recipes;
    using static Eco.Gameplay.Components.PartsComponent;

    [Serialized]
    [LocDisplayName("Road Compactor")]
    [LocDescription("A simple hand-pushed roller that compacts dirt into roads — no engine required, just determination.")]
    [IconGroup("World Object Minimap")]
    [Weight(8000)]
    [Tag("Vehicles")]
    [Ecopedia("Crafted Objects", "Vehicles", createAsSubPage: true)]
    public partial class RoadCompactorItem : WorldObjectItem<RoadCompactorObject>, IPersistentData
    {
        [Serialized, SyncToView, NewTooltipChildren(CacheAs.Instance, flags: TTFlags.AllowNonControllerTypeForChildren)]
        public object PersistentData { get; set; }
    }

    [Serialized]
    [RequireComponent(typeof(StandaloneAuthComponent))]
    [RequireComponent(typeof(PaintableComponent))]
    [RequireComponent(typeof(VehicleComponent))]
    [RequireComponent(typeof(MinimapComponent))]
    [RequireComponent(typeof(PartsComponent))]
    [RequireComponent(typeof(RoadCompactorComponent))]
    [RepairRequiresSkill(typeof(BlacksmithSkill), 1)]
    [ExhaustableUnlessOverridenVehicle]
    [Ecopedia("Crafted Objects", "Vehicles", subPageName: "Road Compactor Item")]
    public partial class RoadCompactorObject : PhysicsWorldObject, IRepresentsItem
    {
        static RoadCompactorObject()
        {
            WorldObject.AddOccupancy<RoadCompactorObject>(new List<BlockOccupancy>(0));
        }

        public override TableTextureMode TableTexture => TableTextureMode.Metal;
        public override bool PlacesBlocks            => false;
        public override LocString DisplayName => Localizer.DoStr("Road Compactor");
        public Type RepresentedItemType => typeof(RoadCompactorItem);

        private RoadCompactorObject() { }

        protected override void Initialize()
        {
            this.ModsPreInitialize();
            base.Initialize();
            this.GetComponent<VehicleComponent>().HumanPowered(2.0f);
            this.GetComponent<MinimapComponent>().InitAsMovable();
            this.GetComponent<MinimapComponent>().SetCategory(Localizer.DoStr("Vehicles"));
            this.GetComponent<VehicleComponent>().Initialize(2, 1, 1);
            this.GetComponent<VehicleComponent>().FailDriveMsg =
                Localizer.Do($"You are too hungry to push this {this.DisplayName}!");
            this.ModsPostInitialize();
            {
                this.GetComponent<PartsComponent>().Config(() => LocString.Empty, new PartInfo[]
                {
                    new() { TypeName = nameof(WoodenGearItem), Quantity = 2 },
                    new() { TypeName = nameof(CompactorRollerItem), Quantity = 1 },
                });
            }
        }

        partial void ModsPreInitialize();
        partial void ModsPostInitialize();
    }

    [RequiresSkill(typeof(BasicEngineeringSkill), 2)]
    public partial class RoadCompactorRecipe : RecipeFamily
    {
        public RoadCompactorRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "RoadCompactor",  //noloc
                displayName: Localizer.DoStr("Road Compactor"),

                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(HandleBarItem), 1, true),
                    new IngredientElement(typeof(CompactorRollerItem), 1, true),
                    new IngredientElement(typeof(WoodenGearItem), 2, true),
                },

                items: new List<CraftingElement>
                {
                    new CraftingElement<RoadCompactorItem>()
                });

            this.Recipes = new List<Recipe> { recipe };
            this.ExperienceOnCraft = 10;

            this.LaborInCalories = CreateLaborInCaloriesValue(250, typeof(BasicEngineeringSkill));

            this.CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(RoadCompactorRecipe),
                start: 10,
                skillType: typeof(BasicEngineeringSkill),
                typeof(BasicEngineeringFocusedSpeedTalent),
                typeof(BasicEngineeringParallelSpeedTalent));

            this.ModsPreInitialize();
            this.Initialize(
                displayText: Localizer.DoStr("Road Compactor"),
                recipeType: typeof(RoadCompactorRecipe));
            this.ModsPostInitialize();

            CraftingComponent.AddRecipe(tableType: typeof(WainwrightTableObject), recipeFamily: this);
        }

        partial void ModsPreInitialize();
        partial void ModsPostInitialize();
    }
}
