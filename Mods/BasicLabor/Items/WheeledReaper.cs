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
    using Eco.Gameplay.Components.Storage;
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
    [LocDisplayName("Wheeled Reaper")]
    [LocDescription("A basic wheeled harvester that gathers crops as you push it through fields. Modest but effective before mechanized farming.")]
    [IconGroup("World Object Minimap")]
    [Weight(7000)]
    [Tag("Vehicles")]
    [Ecopedia("Crafted Objects", "Vehicles", createAsSubPage: true)]
    public partial class WheeledReaperItem : WorldObjectItem<WheeledReaperObject>, IPersistentData
    {
        [Serialized, SyncToView, NewTooltipChildren(CacheAs.Instance, flags: TTFlags.AllowNonControllerTypeForChildren)]
        public object PersistentData { get; set; }
    }

    [Serialized]
    [RequireComponent(typeof(StandaloneAuthComponent))]
    [RequireComponent(typeof(PaintableComponent))]
    [RequireComponent(typeof(VehicleComponent))]
    [RequireComponent(typeof(MinimapComponent))]
    [RequireComponent(typeof(PublicStorageComponent))]
    [RequireComponent(typeof(PartsComponent))]
    [RequireComponent(typeof(WheeledReaperComponent))]
    [RequireComponent(typeof(StorageFillVisualComponent))]
    [RepairRequiresSkill(typeof(BlacksmithSkill), 1)]
    [ExhaustableUnlessOverridenVehicle]
    [Ecopedia("Crafted Objects", "Vehicles", subPageName: "Wheeled Reaper Item")]
    public partial class WheeledReaperObject : PhysicsWorldObject, IRepresentsItem
    {
        static WheeledReaperObject()
        {
            WorldObject.AddOccupancy<WheeledReaperObject>(new List<BlockOccupancy>(0));
        }

        public override TableTextureMode TableTexture => TableTextureMode.Metal;
        public override bool PlacesBlocks            => false;
        public override LocString DisplayName => Localizer.DoStr("Wheeled Reaper");
        public Type RepresentedItemType => typeof(WheeledReaperItem);

        private WheeledReaperObject() { }

        protected override void Initialize()
        {
            this.ModsPreInitialize();
            base.Initialize();
            this.GetComponent<VehicleComponent>().HumanPowered(2.0f);
            this.GetComponent<MinimapComponent>().InitAsMovable();
            this.GetComponent<MinimapComponent>().SetCategory(Localizer.DoStr("Vehicles"));
            this.GetComponent<VehicleComponent>().Initialize(3, 1, 1);
            this.GetComponent<VehicleComponent>().FailDriveMsg =
                Localizer.Do($"You are too hungry to push this {this.DisplayName}!");
            // Harvest basket: 4 slots, crops + seeds only
            var storage = this.GetComponent<PublicStorageComponent>();
            storage.Initialize(4);
            storage.Inventory.AddInvRestriction(new TagRestriction(new string[] { "Crop", "Crop Seed" }));
            this.ModsPostInitialize();
            {
                this.GetComponent<PartsComponent>().Config(() => LocString.Empty, new PartInfo[]
                {
                    new() { TypeName = nameof(CultivatorHarrowItem), Quantity = 1 },
                });
            }
        }

        partial void ModsPreInitialize();
        partial void ModsPostInitialize();
    }

    [RequiresSkill(typeof(BasicEngineeringSkill), 2)]
    public partial class WheeledReaperRecipe : RecipeFamily
    {
        public WheeledReaperRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "WheeledReaper",  //noloc
                displayName: Localizer.DoStr("Wheeled Reaper"),

                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(HandleBarItem), 1, true),
                    new IngredientElement(typeof(CultivatorHarrowItem), 1, true),
                },

                items: new List<CraftingElement>
                {
                    new CraftingElement<WheeledReaperItem>()
                });

            this.Recipes = new List<Recipe> { recipe };
            this.ExperienceOnCraft = 10;

            this.LaborInCalories = CreateLaborInCaloriesValue(250, typeof(BasicEngineeringSkill));

            this.CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(WheeledReaperRecipe),
                start: 10,
                skillType: typeof(BasicEngineeringSkill),
                typeof(BasicEngineeringFocusedSpeedTalent),
                typeof(BasicEngineeringParallelSpeedTalent));

            this.ModsPreInitialize();
            this.Initialize(
                displayText: Localizer.DoStr("Wheeled Reaper"),
                recipeType: typeof(WheeledReaperRecipe));
            this.ModsPostInitialize();

            CraftingComponent.AddRecipe(tableType: typeof(WainwrightTableObject), recipeFamily: this);
        }

        partial void ModsPreInitialize();
        partial void ModsPostInitialize();
    }
}
