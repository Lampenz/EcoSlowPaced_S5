// MOD erstellt von Plex: 3D-Modell und Code.
// Letztes Update des Mods: 16/10/2025
// Bitte entfernen Sie nicht den Abschnitt "Registered Mod" aus dem Code, da er es ermöglicht, eine Vergütung von Strange Loop Games zu erhalten, wenn er auf einem Online-Server verwendet wird.

namespace Eco.Mods.TechTree
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using Eco.Core.Items;
    using Eco.Gameplay.Blocks;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Components.Auth;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Economy;
    using Eco.Gameplay.Housing;
    using Eco.Gameplay.Interactions;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Modules;
    using Eco.Gameplay.Minimap;
    using Eco.Gameplay.Objects;
    using Eco.Gameplay.Occupancy;
    using Eco.Gameplay.Players;
    using Eco.Gameplay.Property;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Systems;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Gameplay.Pipes.LiquidComponents;
    using Eco.Gameplay.Pipes.Gases;
    using Eco.Shared;
    using Eco.Shared.Math;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;
    using Eco.Shared.Utils;
    using Eco.Shared.View;
    using Eco.Shared.Items;
    using Eco.Shared.Networking;
    using Eco.Gameplay.Pipes;
    using Eco.World.Blocks;
    using Eco.Gameplay.Housing.PropertyValues;
    using Eco.Gameplay.Civics.Objects;
    using Eco.Gameplay.Settlements;
    using Eco.Gameplay.Systems.NewTooltip;
    using Eco.Core.Controller;
    using Eco.Core.Utils;
    using Eco.Gameplay.Components.Storage;
    using static Eco.Gameplay.Housing.PropertyValues.HomeFurnishingValue;
    using Eco.Gameplay.Items.Recipes;
    using Eco.Core.Plugins.Interfaces;

    #region ModRegistration
    public class DecoTreeMod : IModInit
    {
        public static ModRegistration Register() => new()
        {
            ModName = "DecoTreeMod",
            ModDescription = "DecoTreeMod vous permet d'ajouter des décorations extérieures sous forme d'arbres dans le jeu. Vous pouvez choisir d'avoir des arbres dans des caisses en bois ou des faux arbres, que vous pouvez placer où bon vous semble.",
            ModDisplayName = "DecoTreeMod",
        };
    }
    #endregion



    #region Box_Item
    // ______________________________________________________ Box_Item ______________________________________________________ \\


    [RequiresSkill(typeof(LoggingSkill), 1)]
    [Ecopedia("Items", "Products", subPageName: "Box Item")]
    public partial class BoxRecipe : RecipeFamily
    {
        public BoxRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "Box",
                displayName: Localizer.DoStr("Box"),

                ingredients: new List<IngredientElement>
                {
                    new IngredientElement("WoodBoard", 8, typeof(LoggingSkill)),
                },

                items: new List<CraftingElement>
                {
                    new CraftingElement<BoxItem>()
                });
            this.Recipes = new List<Recipe> { recipe };
            this.ExperienceOnCraft = 0.6f;

            this.LaborInCalories = CreateLaborInCaloriesValue(50, typeof(LoggingSkill));

            this.CraftMinutes = CreateCraftTimeValue(beneficiary: typeof(BoxRecipe), start: 0.2f, skillType: typeof(LoggingSkill));

            this.ModsPreInitialize();
            this.Initialize(displayText: Localizer.DoStr("Box"), recipeType: typeof(BoxRecipe));
            this.ModsPostInitialize();

            CraftingComponent.AddRecipe(tableType: typeof(CarpentryTableObject), recipeFamily: this);
        }

        partial void ModsPreInitialize();
        partial void ModsPostInitialize();
    }


    [Serialized]
    [LocDisplayName("Box")]
    [Weight(500)]
    [Tag("Currency")]
    [Currency]
    [Ecopedia("Items", "Products", createAsSubPage: true)]
    [LocDescription("Holzkiste, verwendet für die Herstellung von Blumenkästen.")]
    public partial class BoxItem : Item
    {


    }
    #endregion



    #region Box_Arbre_Palm
    // ______________________________________________________ Box_Arbre_Palm ______________________________________________________ \\

    [Serialized]
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(HousingComponent))]
    [RequireComponent(typeof(OccupancyRequirementComponent))]
    [RequireComponent(typeof(ForSaleComponent))]
    [RequireComponent(typeof(PaintableComponent))]
    [Tag("Usable")]
    [Ecopedia("Housing Objects", "Outdoor", subPageName: "Palme Box")]
    public partial class Box_Arbre_PalmObject : WorldObject, IRepresentsItem
    {
        public virtual Type RepresentedItemType => typeof(Box_Arbre_PalmItem);
        public override LocString DisplayName => Localizer.DoStr("Palme Box");
        public override TableTextureMode TableTexture => TableTextureMode.Wood;

        partial void ModsPreInitialize();
        partial void ModsPostInitialize();
    }

    [Serialized]
    [LocDisplayName("Palme Box")]
    [LocDescription("Brauchen Sie Urlaub ? Diese Palme in der Kiste bringt tropisches Flair, ganz ohne den Sand zwischen den Zehen !")]
    [Ecopedia("Housing Objects", "Outdoor", createAsSubPage: true)]
    [Tag("Housing")]
    [Weight(5000)]
    [Tag(nameof(SurfaceTags.CanBeOnSurface))]
    public partial class Box_Arbre_PalmItem : WorldObjectItem<Box_Arbre_PalmObject>
    {
        protected override OccupancyContext GetOccupancyContext => new SideAttachedContext(0 | DirectionAxisFlags.Down, WorldObject.GetOccupancyInfo(this.WorldObjectType));

    }

    [RequiresSkill(typeof(FarmingSkill), 1)]
    [Ecopedia("Housing Objects", "Outdoor", subPageName: "Palme Box")]
    public partial class Box_Arbre_PalmRecipe : RecipeFamily
    {
        public Box_Arbre_PalmRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "Palme Box",
                displayName: Localizer.DoStr("Palme Box"),

                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(PalmSeedItem), 1, typeof(FarmingSkill), typeof(FarmingLavishResourcesTalent)),
                    new IngredientElement(typeof(HeliconiaSeedItem), 2, typeof(FarmingSkill), typeof(FarmingLavishResourcesTalent)),
                    new IngredientElement(typeof(BoxItem), 1, typeof(FarmingSkill), typeof(FarmingLavishResourcesTalent)),
                    new IngredientElement(typeof(DirtItem), 4, typeof(FarmingSkill), typeof(FarmingLavishResourcesTalent)),
                },

                items: new List<CraftingElement>
                {
                    new CraftingElement<Box_Arbre_PalmItem>()
                });
            this.Recipes = new List<Recipe> { recipe };

            this.ExperienceOnCraft = 4;

            this.LaborInCalories = CreateLaborInCaloriesValue(60, typeof(FarmingSkill));

            this.CraftMinutes = CreateCraftTimeValue(beneficiary: typeof(Box_Arbre_PalmRecipe), start: 2, skillType: typeof(FarmingSkill), typeof(FarmingFocusedSpeedTalent), typeof(FarmingParallelSpeedTalent));

            this.ModsPreInitialize();
            this.Initialize(displayText: Localizer.DoStr("Box Palme"), recipeType: typeof(Box_Arbre_PalmRecipe));
            this.ModsPostInitialize();

            CraftingComponent.AddRecipe(tableType: typeof(FarmersTableObject), recipeFamily: this);
        }

        partial void ModsPreInitialize();

        partial void ModsPostInitialize();
    }
    #endregion



    #region Box_Arbre_Oak
    // ______________________________________________________ Box_Arbre_Oak ______________________________________________________ \\


    [Serialized]
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(HousingComponent))]
    [RequireComponent(typeof(OccupancyRequirementComponent))]
    [RequireComponent(typeof(ForSaleComponent))]
    [RequireComponent(typeof(PaintableComponent))]
    [Tag("Usable")]
    [Ecopedia("Housing Objects", "Outdoor", subPageName: "Eiche Box")]
    public partial class Box_Arbre_OakObject : WorldObject, IRepresentsItem
    {
        public virtual Type RepresentedItemType => typeof(Box_Arbre_OakItem);
        public override LocString DisplayName => Localizer.DoStr("Eiche Box");
        public override TableTextureMode TableTexture => TableTextureMode.Wood;

        partial void ModsPreInitialize();
        partial void ModsPostInitialize();
    }

    [Serialized]
    [LocDisplayName("Eiche Box")]
    [LocDescription("Mit dieser Eiche in der Kiste kommt die Natur zu Ihnen, ganz ohne Eicheln... aber mit Stil !")]
    [Ecopedia("Housing Objects", "Outdoor", createAsSubPage: true)]
    [Tag("Housing")]
    [Weight(5000)]
    [Tag(nameof(SurfaceTags.CanBeOnSurface))]
    public partial class Box_Arbre_OakItem : WorldObjectItem<Box_Arbre_OakObject>
    {
        protected override OccupancyContext GetOccupancyContext => new SideAttachedContext(0 | DirectionAxisFlags.Down, WorldObject.GetOccupancyInfo(this.WorldObjectType));

    }

    [RequiresSkill(typeof(FarmingSkill), 1)]
    [Ecopedia("Housing Objects", "Outdoor", subPageName: "Eiche Box")]
    public partial class Box_Arbre_OakRecipe : RecipeFamily
    {
        public Box_Arbre_OakRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "Eiche Box",
                displayName: Localizer.DoStr("Eiche Box"),

                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(AcornItem), 1, typeof(FarmingSkill), typeof(FarmingLavishResourcesTalent)),
                    new IngredientElement(typeof(HeliconiaSeedItem), 2, typeof(FarmingSkill), typeof(FarmingLavishResourcesTalent)),
                    new IngredientElement(typeof(BoxItem), 1, typeof(FarmingSkill), typeof(FarmingLavishResourcesTalent)),
                    new IngredientElement(typeof(DirtItem), 4, typeof(FarmingSkill), typeof(FarmingLavishResourcesTalent)),
                },

                items: new List<CraftingElement>
                {
                    new CraftingElement<Box_Arbre_OakItem>()
                });
            this.Recipes = new List<Recipe> { recipe };

            this.ExperienceOnCraft = 4;

            this.LaborInCalories = CreateLaborInCaloriesValue(60, typeof(FarmingSkill));

            this.CraftMinutes = CreateCraftTimeValue(beneficiary: typeof(Box_Arbre_OakRecipe), start: 2, skillType: typeof(FarmingSkill), typeof(FarmingFocusedSpeedTalent), typeof(FarmingParallelSpeedTalent));

            this.ModsPreInitialize();
            this.Initialize(displayText: Localizer.DoStr("Box Eiche"), recipeType: typeof(Box_Arbre_OakRecipe));
            this.ModsPostInitialize();

            CraftingComponent.AddRecipe(tableType: typeof(FarmersTableObject), recipeFamily: this);
        }

        partial void ModsPreInitialize();

        partial void ModsPostInitialize();
    }
    #endregion



    #region Box_Arbre_Redwood
    // ______________________________________________________ Box_Arbre_Redwood ______________________________________________________ \\


    [Serialized]
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(HousingComponent))]
    [RequireComponent(typeof(OccupancyRequirementComponent))]
    [RequireComponent(typeof(ForSaleComponent))]
    [RequireComponent(typeof(PaintableComponent))]
    [Tag("Usable")]
    [Ecopedia("Housing Objects", "Outdoor", subPageName: "Sekuoya Box")]
    public partial class Box_Arbre_RedwoodObject : WorldObject, IRepresentsItem
    {
        public virtual Type RepresentedItemType => typeof(Box_Arbre_RedwoodItem);
        public override LocString DisplayName => Localizer.DoStr("Sekuoya Box");
        public override TableTextureMode TableTexture => TableTextureMode.Wood;

        partial void ModsPreInitialize();
        partial void ModsPostInitialize();
    }

    [Serialized]
    [LocDisplayName("Sekuoya Box")]
    [LocDescription("Dieser Redwood im Topf träumt davon, Wurzeln zu schlagen... aber im Moment begnügt er sich mit einem schnellen Umzug in die Kiste !")]
    [Ecopedia("Housing Objects", "Outdoor", createAsSubPage: true)]
    [Tag("Housing")]
    [Weight(5000)]
    [Tag(nameof(SurfaceTags.CanBeOnSurface))]
    public partial class Box_Arbre_RedwoodItem : WorldObjectItem<Box_Arbre_RedwoodObject>
    {
        protected override OccupancyContext GetOccupancyContext => new SideAttachedContext(0 | DirectionAxisFlags.Down, WorldObject.GetOccupancyInfo(this.WorldObjectType));

    }

    [RequiresSkill(typeof(FarmingSkill), 1)]
    [Ecopedia("Housing Objects", "Outdoor", subPageName: "Sekuoya Box")]
    public partial class Box_Arbre_RedwoodRecipe : RecipeFamily
    {
        public Box_Arbre_RedwoodRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "Sekuoya Box",
                displayName: Localizer.DoStr("Sekuoya Box"),

                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(RedwoodSeedItem), 1, typeof(FarmingSkill), typeof(FarmingLavishResourcesTalent)),
                    new IngredientElement(typeof(HeliconiaSeedItem), 2, typeof(FarmingSkill), typeof(FarmingLavishResourcesTalent)),
                    new IngredientElement(typeof(BoxItem), 1, typeof(FarmingSkill), typeof(FarmingLavishResourcesTalent)),
                    new IngredientElement(typeof(DirtItem), 4, typeof(FarmingSkill), typeof(FarmingLavishResourcesTalent)),
                },

                items: new List<CraftingElement>
                {
                    new CraftingElement<Box_Arbre_RedwoodItem>()
                });
            this.Recipes = new List<Recipe> { recipe };

            this.ExperienceOnCraft = 4;

            this.LaborInCalories = CreateLaborInCaloriesValue(60, typeof(FarmingSkill));

            this.CraftMinutes = CreateCraftTimeValue(beneficiary: typeof(Box_Arbre_RedwoodRecipe), start: 2, skillType: typeof(FarmingSkill), typeof(FarmingFocusedSpeedTalent), typeof(FarmingParallelSpeedTalent));

            this.ModsPreInitialize();
            this.Initialize(displayText: Localizer.DoStr("Box Sekuoya"), recipeType: typeof(Box_Arbre_RedwoodRecipe));
            this.ModsPostInitialize();

            CraftingComponent.AddRecipe(tableType: typeof(FarmersTableObject), recipeFamily: this);
        }

        partial void ModsPreInitialize();

        partial void ModsPostInitialize();
    }
    #endregion



    #region Box_Arbre_Birch
    // ______________________________________________________ Box_Arbre_Birch ______________________________________________________ \\


    [Serialized]
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(HousingComponent))]
    [RequireComponent(typeof(OccupancyRequirementComponent))]
    [RequireComponent(typeof(ForSaleComponent))]
    [RequireComponent(typeof(PaintableComponent))]
    [Tag("Usable")]
    [Ecopedia("Housing Objects", "Outdoor", subPageName: "Birke Box")]
    public partial class Box_Arbre_BirchObject : WorldObject, IRepresentsItem
    {
        public virtual Type RepresentedItemType => typeof(Box_Arbre_BirchItem);
        public override LocString DisplayName => Localizer.DoStr("Birke Box");
        public override TableTextureMode TableTexture => TableTextureMode.Wood;

        partial void ModsPreInitialize();
        partial void ModsPostInitialize();
    }

    [Serialized]
    [LocDisplayName("Birke Box")]
    [LocDescription("Die Birke, die davon träumt, ein Bonsai zu sein... aber mit einer XXL-Kiste, weil Stil auch für Bäume wichtig ist !")]
    [Ecopedia("Housing Objects", "Outdoor", createAsSubPage: true)]
    [Tag("Housing")]
    [Weight(5000)]
    [Tag(nameof(SurfaceTags.CanBeOnSurface))]
    public partial class Box_Arbre_BirchItem : WorldObjectItem<Box_Arbre_BirchObject>
    {
        protected override OccupancyContext GetOccupancyContext => new SideAttachedContext(0 | DirectionAxisFlags.Down, WorldObject.GetOccupancyInfo(this.WorldObjectType));

    }

    [RequiresSkill(typeof(FarmingSkill), 1)]
    [Ecopedia("Housing Objects", "Outdoor", subPageName: "Birke Box")]
    public partial class Box_Arbre_BirchRecipe : RecipeFamily
    {
        public Box_Arbre_BirchRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "Birke Box",
                displayName: Localizer.DoStr("Birke Box"),

                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(BirchSeedItem), 1, typeof(FarmingSkill), typeof(FarmingLavishResourcesTalent)),
                    new IngredientElement(typeof(HeliconiaSeedItem), 2, typeof(FarmingSkill), typeof(FarmingLavishResourcesTalent)),
                    new IngredientElement(typeof(BoxItem), 1, typeof(FarmingSkill), typeof(FarmingLavishResourcesTalent)),
                    new IngredientElement(typeof(DirtItem), 4, typeof(FarmingSkill), typeof(FarmingLavishResourcesTalent)),
                },

                items: new List<CraftingElement>
                {
                    new CraftingElement<Box_Arbre_BirchItem>()
                });
            this.Recipes = new List<Recipe> { recipe };

            this.ExperienceOnCraft = 4;

            this.LaborInCalories = CreateLaborInCaloriesValue(60, typeof(FarmingSkill));

            this.CraftMinutes = CreateCraftTimeValue(beneficiary: typeof(Box_Arbre_BirchRecipe), start: 2, skillType: typeof(FarmingSkill), typeof(FarmingFocusedSpeedTalent), typeof(FarmingParallelSpeedTalent));

            this.ModsPreInitialize();
            this.Initialize(displayText: Localizer.DoStr("Box Birke"), recipeType: typeof(Box_Arbre_BirchRecipe));
            this.ModsPostInitialize();

            CraftingComponent.AddRecipe(tableType: typeof(FarmersTableObject), recipeFamily: this);
        }

        partial void ModsPreInitialize();

        partial void ModsPostInitialize();
    }
    #endregion



    #region Box_Arbre_Spruce
    // ______________________________________________________ Box_Arbre_Spruce ______________________________________________________ \\


    [Serialized]
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(HousingComponent))]
    [RequireComponent(typeof(OccupancyRequirementComponent))]
    [RequireComponent(typeof(ForSaleComponent))]
    [RequireComponent(typeof(PaintableComponent))]
    [Tag("Usable")]
    [Ecopedia("Housing Objects", "Outdoor", subPageName: "Fichte Box")]
    public partial class Box_Arbre_SpruceObject : WorldObject, IRepresentsItem
    {
        public virtual Type RepresentedItemType => typeof(Box_Arbre_SpruceItem);
        public override LocString DisplayName => Localizer.DoStr("Fichte Box");
        public override TableTextureMode TableTexture => TableTextureMode.Wood;

        partial void ModsPreInitialize();
        partial void ModsPostInitialize();
    }

    [Serialized]
    [LocDisplayName("Fichte Box")]
    [LocDescription("Weil ein Tannenbaum in einer Holzkiste die Natur in tragbarer Version ist. Wer hat gesagt, dass man einen Wald nicht transportieren kann ?")]
    [Ecopedia("Housing Objects", "Outdoor", createAsSubPage: true)]
    [Tag("Housing")]
    [Weight(5000)]
    [Tag(nameof(SurfaceTags.CanBeOnSurface))]
    public partial class Box_Arbre_SpruceItem : WorldObjectItem<Box_Arbre_SpruceObject>
    {
        protected override OccupancyContext GetOccupancyContext => new SideAttachedContext(0 | DirectionAxisFlags.Down, WorldObject.GetOccupancyInfo(this.WorldObjectType));

    }

    [RequiresSkill(typeof(FarmingSkill), 1)]
    [Ecopedia("Housing Objects", "Outdoor", subPageName: "Fichte Box")]
    public partial class Box_Arbre_SpruceRecipe : RecipeFamily
    {
        public Box_Arbre_SpruceRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "Fichte Box",
                displayName: Localizer.DoStr("Fichte Box"),

                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(SpruceSeedItem), 1, typeof(FarmingSkill), typeof(FarmingLavishResourcesTalent)),
                    new IngredientElement(typeof(HeliconiaSeedItem), 2, typeof(FarmingSkill), typeof(FarmingLavishResourcesTalent)),
                    new IngredientElement(typeof(BoxItem), 1, typeof(FarmingSkill), typeof(FarmingLavishResourcesTalent)),
                    new IngredientElement(typeof(DirtItem), 4, typeof(FarmingSkill), typeof(FarmingLavishResourcesTalent)),
                },

                items: new List<CraftingElement>
                {
                    new CraftingElement<Box_Arbre_SpruceItem>()
                });
            this.Recipes = new List<Recipe> { recipe };

            this.ExperienceOnCraft = 4;

            this.LaborInCalories = CreateLaborInCaloriesValue(60, typeof(FarmingSkill));

            this.CraftMinutes = CreateCraftTimeValue(beneficiary: typeof(Box_Arbre_SpruceRecipe), start: 2, skillType: typeof(FarmingSkill), typeof(FarmingFocusedSpeedTalent), typeof(FarmingParallelSpeedTalent));

            this.ModsPreInitialize();
            this.Initialize(displayText: Localizer.DoStr("Box Fichte"), recipeType: typeof(Box_Arbre_SpruceRecipe));
            this.ModsPostInitialize();

            CraftingComponent.AddRecipe(tableType: typeof(FarmersTableObject), recipeFamily: this);
        }

        partial void ModsPreInitialize();

        partial void ModsPostInitialize();
    }
    #endregion



    #region Box_Arbre_Cactus
    // ______________________________________________________ Box_Arbre_Cactus ______________________________________________________ \\


    [Serialized]
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(HousingComponent))]
    [RequireComponent(typeof(OccupancyRequirementComponent))]
    [RequireComponent(typeof(ForSaleComponent))]
    [RequireComponent(typeof(PaintableComponent))]
    [Tag("Usable")]
    [Ecopedia("Housing Objects", "Outdoor", subPageName: "Cactus Box")]
    public partial class Box_Arbre_CactusObject : WorldObject, IRepresentsItem
    {
        public virtual Type RepresentedItemType => typeof(Box_Arbre_CactusItem);
        public override LocString DisplayName => Localizer.DoStr("Cactus Box");
        public override TableTextureMode TableTexture => TableTextureMode.Wood;

        partial void ModsPreInitialize();
        partial void ModsPostInitialize();
    }

    [Serialized]
    [LocDisplayName("Kaktus Box")]
    [LocDescription("Ein Kaktus, gut gepflanzt in einer Kiste, weil ein klassischer Topf viel zu einfach... und viel zu zerbrechlich war !")]
    [Ecopedia("Housing Objects", "Outdoor", createAsSubPage: true)]
    [Tag("Housing")]
    [Weight(5000)]
    [Tag(nameof(SurfaceTags.CanBeOnSurface))]
    public partial class Box_Arbre_CactusItem : WorldObjectItem<Box_Arbre_CactusObject>
    {
        protected override OccupancyContext GetOccupancyContext => new SideAttachedContext(0 | DirectionAxisFlags.Down, WorldObject.GetOccupancyInfo(this.WorldObjectType));

    }

    [RequiresSkill(typeof(FarmingSkill), 1)]
    [Ecopedia("Housing Objects", "Outdoor", subPageName: "Kaktus Box")]
    public partial class Box_Arbre_CactusRecipe : RecipeFamily
    {
        public Box_Arbre_CactusRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "Kaktus Box",  
                displayName: Localizer.DoStr("Kaktus Box"),

                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(SaguaroSeedItem), 1, typeof(FarmingSkill), typeof(FarmingLavishResourcesTalent)),
                    new IngredientElement(typeof(HeliconiaSeedItem), 2, typeof(FarmingSkill), typeof(FarmingLavishResourcesTalent)),
                    new IngredientElement(typeof(BoxItem), 1, typeof(FarmingSkill), typeof(FarmingLavishResourcesTalent)),
                    new IngredientElement(typeof(DirtItem), 4, typeof(FarmingSkill), typeof(FarmingLavishResourcesTalent)),
                },

                items: new List<CraftingElement>
                {
                    new CraftingElement<Box_Arbre_CactusItem>()
                });
            this.Recipes = new List<Recipe> { recipe };

            this.ExperienceOnCraft = 4;

            this.LaborInCalories = CreateLaborInCaloriesValue(60, typeof(FarmingSkill));

            this.CraftMinutes = CreateCraftTimeValue(beneficiary: typeof(Box_Arbre_CactusRecipe), start: 2, skillType: typeof(FarmingSkill), typeof(FarmingFocusedSpeedTalent), typeof(FarmingParallelSpeedTalent));

            this.ModsPreInitialize();
            this.Initialize(displayText: Localizer.DoStr("Box Kaktus"), recipeType: typeof(Box_Arbre_CactusRecipe));
            this.ModsPostInitialize();

            CraftingComponent.AddRecipe(tableType: typeof(FarmersTableObject), recipeFamily: this);
        }

        partial void ModsPreInitialize();

        partial void ModsPostInitialize();
    }
    #endregion



    #region Fake_Arbre_Palm
    // ______________________________________________________ Fake_Arbre_Palm ______________________________________________________ \\

    [Serialized]
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(HousingComponent))]
    [RequireComponent(typeof(OccupancyRequirementComponent))]
    [RequireComponent(typeof(ForSaleComponent))]
    [RequireComponent(typeof(PaintableComponent))]
    [Tag("Usable")]
    [Ecopedia("Housing Objects", "Outdoor", subPageName: "Dekorative Palmenbaum")]
    public partial class Fake_Arbre_PalmObject : WorldObject, IRepresentsItem
    {
        public virtual Type RepresentedItemType => typeof(Fake_Arbre_PalmItem);
        public override LocString DisplayName => Localizer.DoStr("Dekorative Palmenbaum");
        public override TableTextureMode TableTexture => TableTextureMode.Wood;

        partial void ModsPreInitialize();
        partial void ModsPostInitialize();
    }

    [Serialized]
    [LocDisplayName("Dekorative Palmenbaum")]
    [LocDescription("Der dekorative Palmbaum fügt Ihrer Umgebung im Spiel Eco einen exotischen und tropischen Touch hinzu, perfekt, um Ihre Außenbereiche zu verschönern.")]
    [Ecopedia("Housing Objects", "Outdoor", createAsSubPage: true)]
    [Tag("Housing")]
    [Weight(5000)]
    [Tag(nameof(SurfaceTags.CanBeOnSurface))]
    public partial class Fake_Arbre_PalmItem : WorldObjectItem<Fake_Arbre_PalmObject>
    {
        protected override OccupancyContext GetOccupancyContext => new SideAttachedContext(0 | DirectionAxisFlags.Down, WorldObject.GetOccupancyInfo(this.WorldObjectType));

    }

    [RequiresSkill(typeof(FarmingSkill), 1)]
    [Ecopedia("Housing Objects", "Outdoor", subPageName: "Dekorative Palmenbaum")]
    public partial class Fake_Arbre_PalmRecipe : RecipeFamily
    {
        public Fake_Arbre_PalmRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "Dekorative Palmenbaum",
                displayName: Localizer.DoStr("Dekorative Palmenbaum"),

                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(PalmSeedItem), 10, typeof(FarmingSkill), typeof(FarmingLavishResourcesTalent)),
                },

                items: new List<CraftingElement>
                {
                    new CraftingElement<Fake_Arbre_PalmItem>()
                });
            this.Recipes = new List<Recipe> { recipe };

            this.ExperienceOnCraft = 4;

            this.LaborInCalories = CreateLaborInCaloriesValue(120, typeof(FarmingSkill));

            this.CraftMinutes = CreateCraftTimeValue(beneficiary: typeof(Fake_Arbre_PalmRecipe), start: 5, skillType: typeof(FarmingSkill), typeof(FarmingFocusedSpeedTalent), typeof(FarmingParallelSpeedTalent));

            this.ModsPreInitialize();
            this.Initialize(displayText: Localizer.DoStr("Dekorative Palmenbaum"), recipeType: typeof(Fake_Arbre_PalmRecipe));
            this.ModsPostInitialize();

            CraftingComponent.AddRecipe(tableType: typeof(FarmersTableObject), recipeFamily: this);
        }

        partial void ModsPreInitialize();

        partial void ModsPostInitialize();
    }
    #endregion



    #region Fake_Arbre_Oak
    // ______________________________________________________ Fake_Arbre_Oak ______________________________________________________ \\

    [Serialized]
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(HousingComponent))]
    [RequireComponent(typeof(OccupancyRequirementComponent))]
    [RequireComponent(typeof(ForSaleComponent))]
    [RequireComponent(typeof(PaintableComponent))]
    [Tag("Usable")]
    [Ecopedia("Housing Objects", "Outdoor", subPageName: "Dekorativer Eichenbaum")]
    public partial class Fake_Arbre_OakObject : WorldObject, IRepresentsItem
    {
        public virtual Type RepresentedItemType => typeof(Fake_Arbre_OakItem);
        public override LocString DisplayName => Localizer.DoStr("Dekorativer Eichenbaum");
        public override TableTextureMode TableTexture => TableTextureMode.Wood;

        partial void ModsPreInitialize();
        partial void ModsPostInitialize();
    }

    [Serialized]
    [LocDisplayName("Dekorativer Eichenbaum")]
    [LocDescription("Der dekorative Eichenbaum verleiht Ihren Landschaften im Spiel Eco eine natürliche und elegante Atmosphäre, ideal, um realistische grüne Räume zu schaffen.")]
    [Ecopedia("Housing Objects", "Outdoor", createAsSubPage: true)]
    [Tag("Housing")]
    [Weight(5000)]
    [Tag(nameof(SurfaceTags.CanBeOnSurface))]
    public partial class Fake_Arbre_OakItem : WorldObjectItem<Fake_Arbre_OakObject>
    {
        protected override OccupancyContext GetOccupancyContext => new SideAttachedContext(0 | DirectionAxisFlags.Down, WorldObject.GetOccupancyInfo(this.WorldObjectType));

    }

    [RequiresSkill(typeof(FarmingSkill), 1)]
    [Ecopedia("Housing Objects", "Outdoor", subPageName: "Dekorativer Eichenbaum")]
    public partial class Fake_Arbre_OakRecipe : RecipeFamily
    {
        public Fake_Arbre_OakRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "Dekorativer Eichenbaum",
                displayName: Localizer.DoStr("Dekorativer Eichenbaum"),

                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(AcornItem), 10, typeof(FarmingSkill), typeof(FarmingLavishResourcesTalent)),
                },

                items: new List<CraftingElement>
                {
                    new CraftingElement<Fake_Arbre_OakItem>()
                });
            this.Recipes = new List<Recipe> { recipe };

            this.ExperienceOnCraft = 4;

            this.LaborInCalories = CreateLaborInCaloriesValue(120, typeof(FarmingSkill));

            this.CraftMinutes = CreateCraftTimeValue(beneficiary: typeof(Fake_Arbre_OakRecipe), start: 5, skillType: typeof(FarmingSkill), typeof(FarmingFocusedSpeedTalent), typeof(FarmingParallelSpeedTalent));

            this.ModsPreInitialize();
            this.Initialize(displayText: Localizer.DoStr("Dekorativer Eichenbaum"), recipeType: typeof(Fake_Arbre_OakRecipe));
            this.ModsPostInitialize();

            CraftingComponent.AddRecipe(tableType: typeof(FarmersTableObject), recipeFamily: this);
        }

        partial void ModsPreInitialize();

        partial void ModsPostInitialize();
    }
    #endregion



    #region Fake_Arbre_Redwood
    // ______________________________________________________ Fake_Arbre_Redwood ______________________________________________________ \\

    [Serialized]
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(HousingComponent))]
    [RequireComponent(typeof(OccupancyRequirementComponent))]
    [RequireComponent(typeof(ForSaleComponent))]
    [RequireComponent(typeof(PaintableComponent))]
    [Tag("Usable")]
    [Ecopedia("Housing Objects", "Outdoor", subPageName: "Dekorativer Redwood-Baum")]
    public partial class Fake_Arbre_RedwoodObject : WorldObject, IRepresentsItem
    {
        public virtual Type RepresentedItemType => typeof(Fake_Arbre_RedwoodItem);
        public override LocString DisplayName => Localizer.DoStr("Dekorativer Redwood-Baum");
        public override TableTextureMode TableTexture => TableTextureMode.Wood;

        partial void ModsPreInitialize();
        partial void ModsPostInitialize();
    }

    [Serialized]
    [LocDisplayName("Dekorativer Redwood-Baum")]
    [LocDescription("Der dekorative Redwood-Baum verleiht Ihren Dekoren im Spiel Eco eine majestätische und imposante Note und bietet natürliche Schönheit mit seinen großen Proportionen.")]
    [Ecopedia("Housing Objects", "Outdoor", createAsSubPage: true)]
    [Tag("Housing")]
    [Weight(5000)]
    [Tag(nameof(SurfaceTags.CanBeOnSurface))]
    public partial class Fake_Arbre_RedwoodItem : WorldObjectItem<Fake_Arbre_RedwoodObject>
    {
        protected override OccupancyContext GetOccupancyContext => new SideAttachedContext(0 | DirectionAxisFlags.Down, WorldObject.GetOccupancyInfo(this.WorldObjectType));

    }

    [RequiresSkill(typeof(FarmingSkill), 1)]
    [Ecopedia("Housing Objects", "Outdoor", subPageName: "Dekorativer Redwood-Baum")]
    public partial class Fake_Arbre_RedwoodRecipe : RecipeFamily
    {
        public Fake_Arbre_RedwoodRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "Dekorativer Redwood-Baum",
                displayName: Localizer.DoStr("Dekorativer Redwood-Baum"),

                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(RedwoodSeedItem), 10, typeof(FarmingSkill), typeof(FarmingLavishResourcesTalent)),
                },

                items: new List<CraftingElement>
                {
                    new CraftingElement<Fake_Arbre_RedwoodItem>()
                });
            this.Recipes = new List<Recipe> { recipe };

            this.ExperienceOnCraft = 4;

            this.LaborInCalories = CreateLaborInCaloriesValue(120, typeof(FarmingSkill));

            this.CraftMinutes = CreateCraftTimeValue(beneficiary: typeof(Fake_Arbre_RedwoodRecipe), start: 5, skillType: typeof(FarmingSkill), typeof(FarmingFocusedSpeedTalent), typeof(FarmingParallelSpeedTalent));

            this.ModsPreInitialize();
            this.Initialize(displayText: Localizer.DoStr("Dekorativer Redwood-Baum"), recipeType: typeof(Fake_Arbre_RedwoodRecipe));
            this.ModsPostInitialize();

            CraftingComponent.AddRecipe(tableType: typeof(FarmersTableObject), recipeFamily: this);
        }

        partial void ModsPreInitialize();

        partial void ModsPostInitialize();
    }
    #endregion



    #region Fake_Arbre_Birch
    // ______________________________________________________ Fake_Arbre_Birch ______________________________________________________ \\

    [Serialized]
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(HousingComponent))]
    [RequireComponent(typeof(OccupancyRequirementComponent))]
    [RequireComponent(typeof(ForSaleComponent))]
    [RequireComponent(typeof(PaintableComponent))]
    [Tag("Usable")]
    [Ecopedia("Housing Objects", "Outdoor", subPageName: "Dekorativer Birkenbaum")]
    public partial class Fake_Arbre_BirchObject : WorldObject, IRepresentsItem
    {
        public virtual Type RepresentedItemType => typeof(Fake_Arbre_BirchItem);
        public override LocString DisplayName => Localizer.DoStr("Dekorativer Birkenbaum");
        public override TableTextureMode TableTexture => TableTextureMode.Wood;

        partial void ModsPreInitialize();
        partial void ModsPostInitialize();
    }

    [Serialized]
    [LocDisplayName("Dekorativer Birkenbaum")]
    [LocDescription("Der dekorative Birkenbaum bringt eine schlichte Eleganz und eine beruhigende Atmosphäre in Ihre Landschaften im Spiel Eco, mit seiner charakteristischen weißen Rinde und seinen zarten Blättern.")]
    [Ecopedia("Housing Objects", "Outdoor", createAsSubPage: true)]
    [Tag("Housing")]
    [Weight(5000)]
    [Tag(nameof(SurfaceTags.CanBeOnSurface))]
    public partial class Fake_Arbre_BirchItem : WorldObjectItem<Fake_Arbre_BirchObject>
    {
        protected override OccupancyContext GetOccupancyContext => new SideAttachedContext(0 | DirectionAxisFlags.Down, WorldObject.GetOccupancyInfo(this.WorldObjectType));

    }

    [RequiresSkill(typeof(FarmingSkill), 1)]
    [Ecopedia("Housing Objects", "Outdoor", subPageName: "Dekorativer Birkenbaum")]
    public partial class Fake_Arbre_BirchRecipe : RecipeFamily
    {
        public Fake_Arbre_BirchRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "Dekorativer Birkenbaum",
                displayName: Localizer.DoStr("Dekorativer Birkenbaum"),

                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(BirchSeedItem), 10, typeof(FarmingSkill), typeof(FarmingLavishResourcesTalent)),
                },

                items: new List<CraftingElement>
                {
                    new CraftingElement<Fake_Arbre_BirchItem>()
                });
            this.Recipes = new List<Recipe> { recipe };

            this.ExperienceOnCraft = 4;

            this.LaborInCalories = CreateLaborInCaloriesValue(120, typeof(FarmingSkill));

            this.CraftMinutes = CreateCraftTimeValue(beneficiary: typeof(Fake_Arbre_BirchRecipe), start: 5, skillType: typeof(FarmingSkill), typeof(FarmingFocusedSpeedTalent), typeof(FarmingParallelSpeedTalent));

            this.ModsPreInitialize();
            this.Initialize(displayText: Localizer.DoStr("Dekorativer Birkenbaum"), recipeType: typeof(Fake_Arbre_BirchRecipe));
            this.ModsPostInitialize();

            CraftingComponent.AddRecipe(tableType: typeof(FarmersTableObject), recipeFamily: this);
        }

        partial void ModsPreInitialize();

        partial void ModsPostInitialize();
    }
    #endregion



    #region Fake_Arbre_Cactus
    // ______________________________________________________ Fake_Arbre_Cactus ______________________________________________________ \\

    [Serialized]
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(HousingComponent))]
    [RequireComponent(typeof(OccupancyRequirementComponent))]
    [RequireComponent(typeof(ForSaleComponent))]
    [RequireComponent(typeof(PaintableComponent))]
    [Tag("Usable")]
    [Ecopedia("Housing Objects", "Outdoor", subPageName: "Dekorativer Kaktusbaum")]
    public partial class Fake_Arbre_CactusObject : WorldObject, IRepresentsItem
    {
        public virtual Type RepresentedItemType => typeof(Fake_Arbre_CactusItem);
        public override LocString DisplayName => Localizer.DoStr("Dekorativer Kaktusbaum");
        public override TableTextureMode TableTexture => TableTextureMode.Wood;

        partial void ModsPreInitialize();
        partial void ModsPostInitialize();
    }

    [Serialized]
    [LocDisplayName("Dekorativer Kaktusbaum")]
    [LocDescription("Der dekorative Kaktusbaum verleiht Ihren Dekoren im Spiel Eco einen wüstenhaften und originellen Touch, perfekt, um karge und einzigartige Atmosphären zu schaffen.")]
    [Ecopedia("Housing Objects", "Outdoor", createAsSubPage: true)]
    [Tag("Housing")]
    [Weight(5000)]
    [Tag(nameof(SurfaceTags.CanBeOnSurface))]
    public partial class Fake_Arbre_CactusItem : WorldObjectItem<Fake_Arbre_CactusObject>
    {
        protected override OccupancyContext GetOccupancyContext => new SideAttachedContext(0 | DirectionAxisFlags.Down, WorldObject.GetOccupancyInfo(this.WorldObjectType));

    }

    [RequiresSkill(typeof(FarmingSkill), 1)]
    [Ecopedia("Housing Objects", "Outdoor", subPageName: "Dekorativer Kaktusbaum")]
    public partial class Fake_Arbre_CactusRecipe : RecipeFamily
    {
        public Fake_Arbre_CactusRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "Dekorativer Kaktusbaum",
                displayName: Localizer.DoStr("Dekorativer Kaktusbaum"),

                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(SaguaroSeedItem), 10, typeof(FarmingSkill), typeof(FarmingLavishResourcesTalent)),
                },

                items: new List<CraftingElement>
                {
                    new CraftingElement<Fake_Arbre_CactusItem>()
                });
            this.Recipes = new List<Recipe> { recipe };

            this.ExperienceOnCraft = 4;

            this.LaborInCalories = CreateLaborInCaloriesValue(120, typeof(FarmingSkill));

            this.CraftMinutes = CreateCraftTimeValue(beneficiary: typeof(Fake_Arbre_CactusRecipe), start: 5, skillType: typeof(FarmingSkill), typeof(FarmingFocusedSpeedTalent), typeof(FarmingParallelSpeedTalent));

            this.ModsPreInitialize();
            this.Initialize(displayText: Localizer.DoStr("Dekorativer Kaktusbaum"), recipeType: typeof(Fake_Arbre_CactusRecipe));
            this.ModsPostInitialize();

            CraftingComponent.AddRecipe(tableType: typeof(FarmersTableObject), recipeFamily: this);
        }

        partial void ModsPreInitialize();

        partial void ModsPostInitialize();
    }
    #endregion



    #region Volet Gauche
    // ______________________________________________________ Volet_HardWood_Gauche ______________________________________________________ \\

    [Serialized]
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(HousingComponent))]
    [RequireComponent(typeof(OccupancyRequirementComponent))]
    [RequireComponent(typeof(ForSaleComponent))]
    [RequireComponent(typeof(RoomRequirementsComponent))]
    [Tag("Usable")]
    [Ecopedia("Housing Objects", "Decoration", subPageName: "Linker Fensterladen")]
    [Tag(nameof(SurfaceTags.HasTableSurface))]
    public partial class Volet_HardWood_GaucheObject : WorldObject, IRepresentsItem
    {
        public virtual Type RepresentedItemType => typeof(Volet_HardWood_GaucheItem);
        public override LocString DisplayName => Localizer.DoStr("Linker Fensterladen");
        public override TableTextureMode TableTexture => TableTextureMode.Wood;

        protected override void Initialize()
        {
            this.ModsPreInitialize();
            this.ModsPostInitialize();
        }

        partial void ModsPreInitialize();
        partial void ModsPostInitialize();
    }

    [Serialized]
    [LocDisplayName("Linker Fensterladen")]
    [LocDescription("Ein robuster Fensterladen aus Holz, der an Fenstern angebracht wird, um Häusern Charme und Authentizität zu verleihen.")]
    [Ecopedia("Housing Objects", "Decoration", createAsSubPage: true)]
    [Tag("Housing")]
    [Weight(150)]
    [Tag(nameof(SurfaceTags.CanBeOnSurface))]
    public partial class Volet_HardWood_GaucheItem : WorldObjectItem<Volet_HardWood_GaucheObject>, IPersistentData
    {
        protected override OccupancyContext GetOccupancyContext => new SideAttachedContext(0 | DirectionAxisFlags.Backward, WorldObject.GetOccupancyInfo(this.WorldObjectType));

        [Serialized, SyncToView, NewTooltipChildren(CacheAs.Instance, flags: TTFlags.AllowNonControllerTypeForChildren)] public object PersistentData { get; set; }
    }

    [RequiresSkill(typeof(LoggingSkill), 3)]
    [Ecopedia("Housing Objects", "Decoration", subPageName: "Linker Fensterladen")]
    public partial class Volet_HardWood_GaucheRecipe : RecipeFamily
    {
        public Volet_HardWood_GaucheRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "Volet_HardWood_gauche",
                displayName: Localizer.DoStr("Linker Fensterladen"),

                ingredients: new List<IngredientElement>
                {
                new IngredientElement("HewnLog", 3, typeof(LoggingSkill)),
                new IngredientElement("WoodBoard", 6, typeof(LoggingSkill)),
                },

                items: new List<CraftingElement>
                {
                new CraftingElement<Volet_HardWood_GaucheItem>()
                });
            this.Recipes = new List<Recipe> { recipe };
            this.ExperienceOnCraft = 2;

            this.LaborInCalories = CreateLaborInCaloriesValue(60, typeof(LoggingSkill));

            this.CraftMinutes = CreateCraftTimeValue(beneficiary: typeof(Volet_HardWood_GaucheRecipe), start: 2, skillType: typeof(LoggingSkill));

            this.ModsPreInitialize();
            this.Initialize(displayText: Localizer.DoStr("Linker Fensterladen"), recipeType: typeof(Volet_HardWood_GaucheRecipe));
            this.ModsPostInitialize();

            CraftingComponent.AddRecipe(tableType: typeof(CarpentryTableObject), recipeFamily: this);
        }

        partial void ModsPreInitialize();
        partial void ModsPostInitialize();
    }
    #endregion



    #region Volet Droite
    // ______________________________________________________ Rechtes Fensterladen ______________________________________________________ \\

    [Serialized]
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(HousingComponent))]
    [RequireComponent(typeof(OccupancyRequirementComponent))]
    [RequireComponent(typeof(ForSaleComponent))]
    [RequireComponent(typeof(RoomRequirementsComponent))]
    [Tag("Usable")]
    [Ecopedia("Housing Objects", "Decoration", subPageName: "Rechtes Fensterladen")]
    [Tag(nameof(SurfaceTags.HasTableSurface))]
    public partial class Volet_HardWood_DroiteObject : WorldObject, IRepresentsItem
    {
        public virtual Type RepresentedItemType => typeof(Volet_HardWood_DroiteItem);
        public override LocString DisplayName => Localizer.DoStr("Rechtes Fensterladen");
        public override TableTextureMode TableTexture => TableTextureMode.Wood;

        protected override void Initialize()
        {
            this.ModsPreInitialize();
            this.ModsPostInitialize();
        }

        partial void ModsPreInitialize();
        partial void ModsPostInitialize();
    }

    [Serialized]
    [LocDisplayName("Rechtes Fensterladen")]
    [LocDescription("Ein robuster Holzladen, der an Fenstern angebracht wird, um Häusern Charme und Authentizität zu verleihen.")]
    [Ecopedia("Housing Objects", "Decoration", createAsSubPage: true)]
    [Tag("Housing")]
    [Weight(150)]
    [Tag(nameof(SurfaceTags.CanBeOnSurface))]
    public partial class Volet_HardWood_DroiteItem : WorldObjectItem<Volet_HardWood_DroiteObject>, IPersistentData
    {
        protected override OccupancyContext GetOccupancyContext => new SideAttachedContext(0 | DirectionAxisFlags.Backward, WorldObject.GetOccupancyInfo(this.WorldObjectType));

        [Serialized, SyncToView, NewTooltipChildren(CacheAs.Instance, flags: TTFlags.AllowNonControllerTypeForChildren)] public object PersistentData { get; set; }
    }

    [RequiresSkill(typeof(LoggingSkill), 3)]
    [Ecopedia("Housing Objects", "Decoration", subPageName: "Rechtes Fensterladen")]
    public partial class Volet_HardWood_DroiteRecipe : RecipeFamily
    {
        public Volet_HardWood_DroiteRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "Volet_HardWood_Droite",
                displayName: Localizer.DoStr("Rechtes Fensterladen"),

                ingredients: new List<IngredientElement>
                {
                new IngredientElement("HewnLog", 3, typeof(LoggingSkill)),
                new IngredientElement("WoodBoard", 6, typeof(LoggingSkill)),
                },

                items: new List<CraftingElement>
                {
                new CraftingElement<Volet_HardWood_DroiteItem>()
                });
            this.Recipes = new List<Recipe> { recipe };
            this.ExperienceOnCraft = 2;

            this.LaborInCalories = CreateLaborInCaloriesValue(60, typeof(LoggingSkill));

            this.CraftMinutes = CreateCraftTimeValue(beneficiary: typeof(Volet_HardWood_DroiteRecipe), start: 2, skillType: typeof(LoggingSkill));

            this.ModsPreInitialize();
            this.Initialize(displayText: Localizer.DoStr("Rechtes Fensterladen"), recipeType: typeof(Volet_HardWood_DroiteRecipe));
            this.ModsPostInitialize();

            CraftingComponent.AddRecipe(tableType: typeof(CarpentryTableObject), recipeFamily: this);
        }

        partial void ModsPreInitialize();
        partial void ModsPostInitialize();
    }
    #endregion



    #region Fougère en pot
    // ______________________________________________________ Fougère en pot ______________________________________________________ \\
    [Serialized]
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(LinkComponent))]
    [RequireComponent(typeof(HousingComponent))]
    [RequireComponent(typeof(OccupancyRequirementComponent))]
    [RequireComponent(typeof(FakePlantComponent))]
    [RequireComponent(typeof(ForSaleComponent))]
    [RequireComponent(typeof(RoomRequirementsComponent))]
    [RequireRoomVolume(4)]
    [Tag("Usable")]
    [Ecopedia("Housing Objects", "Decoration", subPageName: "Topffarn")]
    public partial class Pot01Object : WorldObject, IRepresentsItem
    {
        public virtual Type RepresentedItemType => typeof(Pot01Item);
        public override LocString DisplayName => Localizer.DoStr("Topffarn");
        public override TableTextureMode TableTexture => TableTextureMode.Brick;

        protected override void Initialize()
        {
            this.ModsPreInitialize();
            this.GetComponent<HousingComponent>().HomeValue = Pot01Item.homeValue;
            this.GetComponent<FakePlantComponent>().Initialize();
            this.ModsPostInitialize();
        }

        partial void ModsPreInitialize();
        partial void ModsPostInitialize();
    }

    [Serialized]
    [LocDisplayName("Topffarn")]
    [LocDescription("Dekorative Pflanze zur Verschönerung Ihres Innenraums.")]
    [Ecopedia("Housing Objects", "Decoration", createAsSubPage: true)]
    [Tag("Housing")]
    [Weight(1000)]
    [Tag(nameof(SurfaceTags.CanBeOnSurface))]
    public partial class Pot01Item : WorldObjectItem<Pot01Object>
    {
        protected override OccupancyContext GetOccupancyContext => new SideAttachedContext(0 | DirectionAxisFlags.Down, WorldObject.GetOccupancyInfo(this.WorldObjectType));
        public override HomeFurnishingValue HomeValue => homeValue;
        public static readonly HomeFurnishingValue homeValue = new HomeFurnishingValue()
        {
            ObjectName = typeof(Pot01Object).UILink(),
            Category = HousingConfig.GetRoomCategory("Decoration"),
            BaseValue = 1.5f,
            TypeForRoomLimit = Localizer.DoStr("Decoration"),
            DiminishingReturnMultiplier = 0.4f
        };
    }

    [RequiresSkill(typeof(MasonrySkill), 1)]
    [Ecopedia("Housing Objects", "Decoration", subPageName: "Topffarn")]
    public partial class Pot01Recipe : RecipeFamily
    {
        public Pot01Recipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "Fougère en pot",
                displayName: Localizer.DoStr("Topffarn"),

                ingredients: new List<IngredientElement>
                {
                new IngredientElement("Rock", 4, typeof(MasonrySkill), typeof(MasonryLavishResourcesTalent)),
                new IngredientElement(typeof(HeliconiaSeedItem), 1, typeof(FarmingSkill), typeof(FarmingLavishResourcesTalent)),
                new IngredientElement(typeof(DirtItem), 2, typeof(FarmingSkill), typeof(FarmingLavishResourcesTalent)),
                },

                items: new List<CraftingElement>
                {
                new CraftingElement<Pot01Item>()
                });
            this.Recipes = new List<Recipe> { recipe };
            this.ExperienceOnCraft = 4;

            this.LaborInCalories = CreateLaborInCaloriesValue(40, typeof(MasonrySkill));

            this.CraftMinutes = CreateCraftTimeValue(beneficiary: typeof(Pot01Recipe), start: 2, skillType: typeof(MasonrySkill), typeof(MasonryFocusedSpeedTalent), typeof(MasonryParallelSpeedTalent));

            this.ModsPreInitialize();
            this.Initialize(displayText: Localizer.DoStr("Topffarn"), recipeType: typeof(Pot01Recipe));
            this.ModsPostInitialize();

            CraftingComponent.AddRecipe(tableType: typeof(MasonryTableObject), recipeFamily: this);
        }

        partial void ModsPreInitialize();
        partial void ModsPostInitialize();
    }
    #endregion



    #region Suspension florale
    // ______________________________________________________ Suspension florale ______________________________________________________ \\
    [Serialized]
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(LinkComponent))]
    [RequireComponent(typeof(HousingComponent))]
    [RequireComponent(typeof(OccupancyRequirementComponent))]
    [RequireComponent(typeof(FakePlantComponent))]
    [RequireComponent(typeof(ForSaleComponent))]
    [RequireComponent(typeof(RoomRequirementsComponent))]
    [RequireRoomVolume(4)]
    [Tag("Usable")]
    [Ecopedia("Housing Objects", "Decoration", subPageName: "Blumenampel")]
    public partial class Pot02Object : WorldObject, IRepresentsItem
    {
        public virtual Type RepresentedItemType => typeof(Pot02Item);
        public override LocString DisplayName => Localizer.DoStr("Blumenampel");
        public override TableTextureMode TableTexture => TableTextureMode.Stone;

        protected override void Initialize()
        {
            this.ModsPreInitialize();
            this.GetComponent<HousingComponent>().HomeValue = Pot02Item.homeValue;
            this.GetComponent<FakePlantComponent>().Initialize();
            this.ModsPostInitialize();
        }

        partial void ModsPreInitialize();
        partial void ModsPostInitialize();
    }

    [Serialized]
    [LocDisplayName("Blumenampel")]
    [LocDescription("Dekorative Hängepflanze.")]
    [Ecopedia("Housing Objects", "Decoration", createAsSubPage: true)]
    [Tag("Housing")]
    [Weight(1000)]
    [Tag(nameof(SurfaceTags.CanBeOnSurface))]
    public partial class Pot02Item : WorldObjectItem<Pot02Object>
    {
        protected override OccupancyContext GetOccupancyContext => new SideAttachedContext(0 | DirectionAxisFlags.Up, WorldObject.GetOccupancyInfo(this.WorldObjectType));
        public override HomeFurnishingValue HomeValue => homeValue;
        public static readonly HomeFurnishingValue homeValue = new HomeFurnishingValue()
        {
            ObjectName = typeof(Pot02Object).UILink(),
            Category = HousingConfig.GetRoomCategory("Decoration"),
            BaseValue = 1.5f,
            TypeForRoomLimit = Localizer.DoStr("Dekoration"),
            DiminishingReturnMultiplier = 0.4f
        };
    }

    [RequiresSkill(typeof(PotterySkill), 3)]
    [Ecopedia("Housing Objects", "Decoration", subPageName: "Blumenampel")]
    public partial class Pot02Recipe : RecipeFamily
    {
        public Pot02Recipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "Pot02",
                displayName: Localizer.DoStr("Blumenampel"),

                ingredients: new List<IngredientElement>
                {
                new IngredientElement(typeof(ClayItem), 4, typeof(PotterySkill), typeof(PotteryLavishResourcesTalent)),
                },

                items: new List<CraftingElement>
                {
                new CraftingElement<Pot02Item>()
                });
            this.Recipes = new List<Recipe> { recipe };
            this.ExperienceOnCraft = 1;

            this.LaborInCalories = CreateLaborInCaloriesValue(45, typeof(PotterySkill));
            this.CraftMinutes = CreateCraftTimeValue(beneficiary: typeof(Pot02Recipe), start: 2, skillType: typeof(PotterySkill), typeof(PotteryFocusedSpeedTalent), typeof(PotteryParallelSpeedTalent));

            this.ModsPreInitialize();
            this.Initialize(displayText: Localizer.DoStr("Blumenampel"), recipeType: typeof(Pot02Recipe));
            this.ModsPostInitialize();

            CraftingComponent.AddRecipe(tableType: typeof(PotteryTableObject), recipeFamily: this);
        }

        partial void ModsPreInitialize();
        partial void ModsPostInitialize();
    }
    #endregion



    #region Gartenkasten 1x1
    // ______________________________________________________ Gartenkasten 1x1 ______________________________________________________ \\
    [Serialized]
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(LinkComponent))]
    [RequireComponent(typeof(HousingComponent))]
    [RequireComponent(typeof(OccupancyRequirementComponent))]
    [RequireComponent(typeof(FakePlantComponent))]
    [RequireComponent(typeof(ForSaleComponent))]
    [RequireComponent(typeof(RoomRequirementsComponent))]
    [RequireRoomVolume(4)]
    [Tag("Usable")]
    [Ecopedia("Housing Objects", "Decoration", subPageName: "Gartenkasten 1x1")]
    public partial class Pot03Object : WorldObject, IRepresentsItem
    {
        public virtual Type RepresentedItemType => typeof(Pot03Item);
        public override LocString DisplayName => Localizer.DoStr("Gartenkasten 1x1");
        public override TableTextureMode TableTexture => TableTextureMode.Stone;

        protected override void Initialize()
        {
            this.ModsPreInitialize();
            this.GetComponent<HousingComponent>().HomeValue = Pot03Item.homeValue;
            this.GetComponent<FakePlantComponent>().Initialize();
            this.ModsPostInitialize();
        }

        partial void ModsPreInitialize();
        partial void ModsPostInitialize();
    }

    [Serialized]
    [LocDisplayName("Gartenkasten 1x1")]
    [LocDescription("Kleiner dekorativer Kasten mit Grünpflanzen.")]
    [Ecopedia("Housing Objects", "Decoration", createAsSubPage: true)]
    [Tag("Housing")]
    [Weight(1000)]
    [Tag(nameof(SurfaceTags.CanBeOnSurface))]
    public partial class Pot03Item : WorldObjectItem<Pot03Object>
    {
        protected override OccupancyContext GetOccupancyContext => new SideAttachedContext(0 | DirectionAxisFlags.Down, WorldObject.GetOccupancyInfo(this.WorldObjectType));
        public override HomeFurnishingValue HomeValue => homeValue;
        public static readonly HomeFurnishingValue homeValue = new HomeFurnishingValue()
        {
            ObjectName = typeof(Pot03Object).UILink(),
            Category = HousingConfig.GetRoomCategory("Decoration"),
            BaseValue = 1.5f,
            TypeForRoomLimit = Localizer.DoStr("Dekoration"),
            DiminishingReturnMultiplier = 0.4f
        };
    }

    [RequiresSkill(typeof(MasonrySkill), 3)]
    [Ecopedia("Housing Objects", "Decoration", subPageName: "Gartenkasten 1x1")]
    public partial class Pot03Recipe : RecipeFamily
    {
        public Pot03Recipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "Gartenkasten 1x1",
                displayName: Localizer.DoStr("Gartenkasten 1x1"),

                ingredients: new List<IngredientElement>
                {
                new IngredientElement("Rock", 8, typeof(MasonrySkill), typeof(MasonryLavishResourcesTalent)),
                new IngredientElement(typeof(HeliconiaSeedItem), 1, typeof(FarmingSkill), typeof(FarmingLavishResourcesTalent)),
                new IngredientElement(typeof(DirtItem), 4, typeof(FarmingSkill), typeof(FarmingLavishResourcesTalent)),
                },

                items: new List<CraftingElement>
                {
                new CraftingElement<Pot03Item>()
                });
            this.Recipes = new List<Recipe> { recipe };
            this.ExperienceOnCraft = 4;

            this.LaborInCalories = CreateLaborInCaloriesValue(40, typeof(MasonrySkill));
            this.CraftMinutes = CreateCraftTimeValue(beneficiary: typeof(Pot03Recipe), start: 3, skillType: typeof(MasonrySkill), typeof(MasonryFocusedSpeedTalent), typeof(MasonryParallelSpeedTalent));

            this.ModsPreInitialize();
            this.Initialize(displayText: Localizer.DoStr("Gartenkasten 1x1"), recipeType: typeof(Pot03Recipe));
            this.ModsPostInitialize();

            CraftingComponent.AddRecipe(tableType: typeof(MasonryTableObject), recipeFamily: this);
        }

        partial void ModsPreInitialize();
        partial void ModsPostInitialize();
    }
    #endregion



    #region Bac de jardin 2x2
    // ______________________________________________________ Bac de jardin 2x2 ______________________________________________________ \\
    [Serialized]
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(LinkComponent))]
    [RequireComponent(typeof(HousingComponent))]
    [RequireComponent(typeof(OccupancyRequirementComponent))]
    [RequireComponent(typeof(FakePlantComponent))]
    [RequireComponent(typeof(ForSaleComponent))]
    [RequireComponent(typeof(RoomRequirementsComponent))]
    [RequireRoomVolume(4)]
    [Tag("Usable")]
    [Ecopedia("Housing Objects", "Decoration", subPageName: "Bac de jardin 2x2")]
    public partial class Pot04Object : WorldObject, IRepresentsItem
    {
        public virtual Type RepresentedItemType => typeof(Pot04Item);
        public override LocString DisplayName => Localizer.DoStr("Gartenkübel 2x2");
        public override TableTextureMode TableTexture => TableTextureMode.Stone;

        protected override void Initialize()
        {
            this.ModsPreInitialize();
            this.GetComponent<HousingComponent>().HomeValue = Pot04Item.homeValue;
            this.GetComponent<FakePlantComponent>().Initialize();
            this.ModsPostInitialize();
        }

        partial void ModsPreInitialize();
        partial void ModsPostInitialize();
    }

    [Serialized]
    [LocDisplayName("Gartenkübel 2x2")]
    [LocDescription("Ein robuster Betonkübel voller Vegetation, ideal zur Verschönerung städtischer Bereiche.")]
    [Ecopedia("Housing Objects", "Decoration", createAsSubPage: true)]
    [Tag("Housing")]
    [Weight(1000)]
    [Tag(nameof(SurfaceTags.CanBeOnSurface))]
    public partial class Pot04Item : WorldObjectItem<Pot04Object>
    {
        protected override OccupancyContext GetOccupancyContext => new SideAttachedContext(0 | DirectionAxisFlags.Down, WorldObject.GetOccupancyInfo(this.WorldObjectType));
        public override HomeFurnishingValue HomeValue => homeValue;
        public static readonly HomeFurnishingValue homeValue = new HomeFurnishingValue()
        {
            ObjectName = typeof(Pot04Object).UILink(),
            Category = HousingConfig.GetRoomCategory("Decoration"),
            BaseValue = 1.5f,
            TypeForRoomLimit = Localizer.DoStr("Dekoration"),
            DiminishingReturnMultiplier = 0.4f
        };
    }

    [RequiresSkill(typeof(MasonrySkill), 3)]
    [Ecopedia("Housing Objects", "Decoration", subPageName: "Bac de jardin 2x2")]
    public partial class Pot04Recipe : RecipeFamily
    {
        public Pot04Recipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "Gartenkübel 2x2",
                displayName: Localizer.DoStr("Gartenkübel 2x2"),

                ingredients: new List<IngredientElement>
                {
                new IngredientElement("Rock", 12, typeof(MasonrySkill), typeof(MasonryLavishResourcesTalent)),
                new IngredientElement(typeof(HeliconiaSeedItem), 2, typeof(FarmingSkill), typeof(FarmingLavishResourcesTalent)),
                new IngredientElement(typeof(DirtItem), 6, typeof(FarmingSkill), typeof(FarmingLavishResourcesTalent)),
                },

                items: new List<CraftingElement>
                {
                new CraftingElement<Pot04Item>()
                });
            this.Recipes = new List<Recipe> { recipe };
            this.ExperienceOnCraft = 4;

            this.LaborInCalories = CreateLaborInCaloriesValue(40, typeof(MasonrySkill));

            this.CraftMinutes = CreateCraftTimeValue(beneficiary: typeof(Pot04Recipe), start: 2, skillType: typeof(MasonrySkill), typeof(MasonryFocusedSpeedTalent), typeof(MasonryParallelSpeedTalent));

            this.ModsPreInitialize();
            this.Initialize(displayText: Localizer.DoStr("Gartenkübel 2x2"), recipeType: typeof(Pot04Recipe));
            this.ModsPostInitialize();

            CraftingComponent.AddRecipe(tableType: typeof(MasonryTableObject), recipeFamily: this);
        }

        partial void ModsPreInitialize();
        partial void ModsPostInitialize();
    }
    #endregion



    #region Bac Rectangulaire Moderne
    // ______________________________________________________ Bac Rectangulaire Moderne ______________________________________________________ \\
    [Serialized]
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(LinkComponent))]
    [RequireComponent(typeof(HousingComponent))]
    [RequireComponent(typeof(OccupancyRequirementComponent))]
    [RequireComponent(typeof(FakePlantComponent))]
    [RequireComponent(typeof(ForSaleComponent))]
    [RequireComponent(typeof(RoomRequirementsComponent))]
    [RequireRoomVolume(4)]
    [Tag("Usable")]
    [Ecopedia("Housing Objects", "Decoration", subPageName: "Moderner rechteckiger Blumenkasten")]
    public partial class Pot05Object : WorldObject, IRepresentsItem
    {
        public virtual Type RepresentedItemType => typeof(Pot05Item);
        public override LocString DisplayName => Localizer.DoStr("Moderner rechteckiger Blumenkasten");
        public override TableTextureMode TableTexture => TableTextureMode.Stone;

        protected override void Initialize()
        {
            this.ModsPreInitialize();
            this.GetComponent<HousingComponent>().HomeValue = Pot05Item.homeValue;
            this.GetComponent<FakePlantComponent>().Initialize();
            this.ModsPostInitialize();
        }

        partial void ModsPreInitialize();
        partial void ModsPostInitialize();
    }

    [Serialized]
    [LocDisplayName("Moderner rechteckiger Blumenkasten")]
    [LocDescription("Ein eleganter Pflanzkübel aus dunklem Stein, ideal um Gehwege und Gebäude mit einem Hauch Grün zu verschönern.")]
    [Ecopedia("Housing Objects", "Decoration", createAsSubPage: true)]
    [Tag("Housing")]
    [Weight(1000)]
    [Tag(nameof(SurfaceTags.CanBeOnSurface))]
    public partial class Pot05Item : WorldObjectItem<Pot05Object>
    {
        protected override OccupancyContext GetOccupancyContext => new SideAttachedContext(0 | DirectionAxisFlags.Down, WorldObject.GetOccupancyInfo(this.WorldObjectType));
        public override HomeFurnishingValue HomeValue => homeValue;
        public static readonly HomeFurnishingValue homeValue = new HomeFurnishingValue()
        {
            ObjectName = typeof(Pot05Object).UILink(),
            Category = HousingConfig.GetRoomCategory("Decoration"),
            BaseValue = 1.5f,
            TypeForRoomLimit = Localizer.DoStr("Dekoration"),
            DiminishingReturnMultiplier = 0.4f
        };
    }

    [RequiresSkill(typeof(MasonrySkill), 1)]
    [Ecopedia("Housing Objects", "Decoration", subPageName: "Moderner rechteckiger Blumenkasten")]
    public partial class Pot05Recipe : RecipeFamily
    {
        public Pot05Recipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "Moderner rechteckiger Blumenkasten",
                displayName: Localizer.DoStr("Moderner rechteckiger Blumenkasten"),

                ingredients: new List<IngredientElement>
                {
                new IngredientElement("Rock", 5, typeof(MasonrySkill), typeof(MasonryLavishResourcesTalent)),
                new IngredientElement(typeof(HeliconiaSeedItem), 1, typeof(FarmingSkill), typeof(FarmingLavishResourcesTalent)),
                new IngredientElement(typeof(DirtItem), 2, typeof(FarmingSkill), typeof(FarmingLavishResourcesTalent)),
                },

                items: new List<CraftingElement>
                {
                new CraftingElement<Pot05Item>()
                });
            this.Recipes = new List<Recipe> { recipe };
            this.ExperienceOnCraft = 4;

            this.LaborInCalories = CreateLaborInCaloriesValue(40, typeof(MasonrySkill));

            this.CraftMinutes = CreateCraftTimeValue(beneficiary: typeof(Pot05Recipe), start: 2, skillType: typeof(MasonrySkill), typeof(MasonryFocusedSpeedTalent), typeof(MasonryParallelSpeedTalent));

            this.ModsPreInitialize();
            this.Initialize(displayText: Localizer.DoStr("Moderner rechteckiger Blumenkasten"), recipeType: typeof(Pot05Recipe));
            this.ModsPostInitialize();

            CraftingComponent.AddRecipe(tableType: typeof(MasonryTableObject), recipeFamily: this);
        }

        partial void ModsPreInitialize();
        partial void ModsPostInitialize();
    }
    #endregion



    #region Pot Sphérique en Pierre
    // ______________________________________________________ Pot Sphérique en Pierre ______________________________________________________ \\

    [Serialized]
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(HousingComponent))]
    [RequireComponent(typeof(OccupancyRequirementComponent))]
    [RequireComponent(typeof(ForSaleComponent))]
    [RequireComponent(typeof(PaintableComponent))]
    [Tag("Usable")]
    [Ecopedia("Housing Objects", "Decoration", subPageName: "Runder Steintopf")]
    public partial class Pot06Object : WorldObject, IRepresentsItem
    {
        public virtual Type RepresentedItemType => typeof(Pot06Item);
        public override LocString DisplayName => Localizer.DoStr("Weißer Fake-Agave");
        public override TableTextureMode TableTexture => TableTextureMode.Stone;

        partial void ModsPreInitialize();
        partial void ModsPostInitialize();
    }

    [Serialized]
    [LocDisplayName("Runder Steintopf")]
    [LocDescription("Ein kleiner runder Topf mit schlichtem Design, ideal zur Dekoration von Innen- und Außenbereichen.")]
    [Ecopedia("Housing Objects", "Decoration", createAsSubPage: true)]
    [Tag("Housing")]
    [Weight(100)]
    [Tag(nameof(SurfaceTags.CanBeOnSurface))]
    public partial class Pot06Item : WorldObjectItem<Pot06Object>
    {
        protected override OccupancyContext GetOccupancyContext => new SideAttachedContext(0 | DirectionAxisFlags.Down, WorldObject.GetOccupancyInfo(this.WorldObjectType));
    }

    [RequiresSkill(typeof(MasonrySkill), 1)]
    [Ecopedia("Housing Objects", "Decoration", subPageName: "Runder Steintopf")]
    public partial class Pot06Recipe : RecipeFamily
    {
        public Pot06Recipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "Runder Steintopf",
                displayName: Localizer.DoStr("Runder Steintopf"),

                ingredients: new List<IngredientElement>
                {
                new IngredientElement("Rock", 3, typeof(MasonrySkill), typeof(MasonryLavishResourcesTalent)),
                new IngredientElement(typeof(HeliconiaSeedItem), 1, typeof(FarmingSkill), typeof(FarmingLavishResourcesTalent)),
                new IngredientElement(typeof(DirtItem), 1, typeof(FarmingSkill), typeof(FarmingLavishResourcesTalent)),
                },

                items: new List<CraftingElement>
                {
                new CraftingElement<Pot06Item>()
                });
            this.Recipes = new List<Recipe> { recipe };
            this.ExperienceOnCraft = 4;

            this.LaborInCalories = CreateLaborInCaloriesValue(40, typeof(MasonrySkill));

            this.CraftMinutes = CreateCraftTimeValue(beneficiary: typeof(Pot06Recipe), start: 2, skillType: typeof(MasonrySkill), typeof(MasonryFocusedSpeedTalent), typeof(MasonryParallelSpeedTalent));

            this.ModsPreInitialize();
            this.Initialize(displayText: Localizer.DoStr("Runder Steintopf"), recipeType: typeof(Pot06Recipe));
            this.ModsPostInitialize();

            CraftingComponent.AddRecipe(tableType: typeof(MasonryTableObject), recipeFamily: this);
        }

        partial void ModsPreInitialize();
        partial void ModsPostInitialize();
    }
    #endregion



    #region Bac de jardin 2x3
    // ______________________________________________________ Bac de jardin 2x3 ______________________________________________________ \\

    [Serialized]
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(LinkComponent))]
    [RequireComponent(typeof(HousingComponent))]
    [RequireComponent(typeof(OccupancyRequirementComponent))]
    [RequireComponent(typeof(FakePlantComponent))]
    [RequireComponent(typeof(ForSaleComponent))]
    [RequireComponent(typeof(RoomRequirementsComponent))]
    [RequireRoomVolume(4)]
    [Tag("Usable")]
    [Ecopedia("Housing Objects", "Decoration", subPageName: "Gartenbeet 2x3")]
    public partial class Pot07Object : WorldObject, IRepresentsItem
    {
        public virtual Type RepresentedItemType => typeof(Pot07Item);
        public override LocString DisplayName => Localizer.DoStr("Gartenbeet 2x3");
        public override TableTextureMode TableTexture => TableTextureMode.Stone;

        protected override void Initialize()
        {
            this.ModsPreInitialize();
            this.GetComponent<HousingComponent>().HomeValue = Pot07Item.homeValue;
            this.GetComponent<FakePlantComponent>().Initialize();
            this.ModsPostInitialize();
        }

        partial void ModsPreInitialize();
        partial void ModsPostInitialize();
    }

    [Serialized]
    [LocDisplayName("Gartenbeet 2x3")]
    [LocDescription("Ein großer, robuster Betonbehälter, gefüllt mit Vegetation, perfekt zur Verschönerung städtischer Bereiche.")]
    [Ecopedia("Housing Objects", "Decoration", createAsSubPage: true)]
    [Tag("Housing")]
    [Weight(1000)]
    [Tag(nameof(SurfaceTags.CanBeOnSurface))]
    public partial class Pot07Item : WorldObjectItem<Pot07Object>
    {
        protected override OccupancyContext GetOccupancyContext => new SideAttachedContext(0 | DirectionAxisFlags.Down, WorldObject.GetOccupancyInfo(this.WorldObjectType));
        public override HomeFurnishingValue HomeValue => homeValue;
        public static readonly HomeFurnishingValue homeValue = new HomeFurnishingValue()
        {
            ObjectName = typeof(Pot07Object).UILink(),
            Category = HousingConfig.GetRoomCategory("Decoration"),
            BaseValue = 1.5f,
            TypeForRoomLimit = Localizer.DoStr("Dekoration"),
            DiminishingReturnMultiplier = 0.4f
        };
    }

    [RequiresSkill(typeof(MasonrySkill), 4)]
    [Ecopedia("Housing Objects", "Decoration", subPageName: "Gartenbeet 2x3")]
    public partial class Pot07Recipe : RecipeFamily
    {
        public Pot07Recipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "Gartenbeet 2x3",
                displayName: Localizer.DoStr("Gartenbeet 2x3"),

                ingredients: new List<IngredientElement>
                {
                new IngredientElement("Rock", 20, typeof(MasonrySkill), typeof(MasonryLavishResourcesTalent)),
                new IngredientElement(typeof(HeliconiaSeedItem), 4, typeof(FarmingSkill), typeof(FarmingLavishResourcesTalent)),
                new IngredientElement(typeof(DirtItem), 10, typeof(FarmingSkill), typeof(FarmingLavishResourcesTalent)),
                },

                items: new List<CraftingElement>
                {
                new CraftingElement<Pot07Item>()
                });
            this.Recipes = new List<Recipe> { recipe };
            this.ExperienceOnCraft = 4;

            this.LaborInCalories = CreateLaborInCaloriesValue(40, typeof(MasonrySkill));

            this.CraftMinutes = CreateCraftTimeValue(beneficiary: typeof(Pot07Recipe), start: 2, skillType: typeof(MasonrySkill), typeof(MasonryFocusedSpeedTalent), typeof(MasonryParallelSpeedTalent));

            this.ModsPreInitialize();
            this.Initialize(displayText: Localizer.DoStr("Gartenbeet 2x3"), recipeType: typeof(Pot07Recipe));
            this.ModsPostInitialize();

            CraftingComponent.AddRecipe(tableType: typeof(MasonryTableObject), recipeFamily: this);
        }

        partial void ModsPreInitialize();
        partial void ModsPostInitialize();
    }
    #endregion



    #region Bac en bois de jardin
    // ______________________________________________________ Bac en bois de jardin ______________________________________________________ \\

    [Serialized]
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(LinkComponent))]
    [RequireComponent(typeof(HousingComponent))]
    [RequireComponent(typeof(OccupancyRequirementComponent))]
    [RequireComponent(typeof(FakePlantComponent))]
    [RequireComponent(typeof(ForSaleComponent))]
    [RequireComponent(typeof(RoomRequirementsComponent))]
    [RequireRoomVolume(4)]
    [Tag("Usable")]
    [Ecopedia("Housing Objects", "Decoration", subPageName: "Holzgartenbeet")]
    public partial class Pot08Object : WorldObject, IRepresentsItem
    {
        public virtual Type RepresentedItemType => typeof(Pot08Item);
        public override LocString DisplayName => Localizer.DoStr("Holzgartenbeet");
        public override TableTextureMode TableTexture => TableTextureMode.Wood;

        protected override void Initialize()
        {
            this.ModsPreInitialize();
            this.GetComponent<HousingComponent>().HomeValue = Pot08Item.homeValue;
            this.GetComponent<FakePlantComponent>().Initialize();
            this.ModsPostInitialize();
        }

        partial void ModsPreInitialize();
        partial void ModsPostInitialize();
    }

    [Serialized]
    [LocDisplayName("Holzgartenbeet")]
    [LocDescription("Kleines dekoratives Holzgartenbeet mit grünen Pflanzen.")]
    [Ecopedia("Housing Objects", "Decoration", createAsSubPage: true)]
    [Tag("Housing")]
    [Weight(1000)]
    [Tag(nameof(SurfaceTags.CanBeOnSurface))]
    public partial class Pot08Item : WorldObjectItem<Pot08Object>
    {
        protected override OccupancyContext GetOccupancyContext => new SideAttachedContext(0 | DirectionAxisFlags.Down, WorldObject.GetOccupancyInfo(this.WorldObjectType));
        public override HomeFurnishingValue HomeValue => homeValue;
        public static readonly HomeFurnishingValue homeValue = new HomeFurnishingValue()
        {
            ObjectName = typeof(Pot08Object).UILink(),
            Category = HousingConfig.GetRoomCategory("Decoration"),
            BaseValue = 1.5f,
            TypeForRoomLimit = Localizer.DoStr("Dekoration"),
            DiminishingReturnMultiplier = 0.4f
        };
    }

    [RequiresSkill(typeof(CarpentrySkill), 2)]
    [Ecopedia("Housing Objects", "Decoration", subPageName: "Holzgartenbeet")]
    public partial class Pot08Recipe : RecipeFamily
    {
        public Pot08Recipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "Holzgartenbeet",
                displayName: Localizer.DoStr("Holzgartenbeet"),

                ingredients: new List<IngredientElement>
                {
                new IngredientElement(typeof(DirtItem), 2, typeof(FarmingSkill), typeof(FarmingLavishResourcesTalent)),
                new IngredientElement(typeof(IronBarItem), 4, typeof(BasicEngineeringSkill), typeof(BasicEngineeringLavishResourcesTalent)),
                new IngredientElement("Lumber", 8, true),
                },

                items: new List<CraftingElement>
                {
                new CraftingElement<Pot08Item>()
                });
            this.Recipes = new List<Recipe> { recipe };
            this.ExperienceOnCraft = 4;

            this.LaborInCalories = CreateLaborInCaloriesValue(40, typeof(CarpentrySkill));

            this.CraftMinutes = CreateCraftTimeValue(beneficiary: typeof(Pot08Recipe), start: 2, skillType: typeof(CarpentrySkill), typeof(CarpentryFocusedSpeedTalent), typeof(CarpentryParallelSpeedTalent));

            this.ModsPreInitialize();
            this.Initialize(displayText: Localizer.DoStr("Holzgartenbeet"), recipeType: typeof(Pot08Recipe));
            this.ModsPostInitialize();

            CraftingComponent.AddRecipe(tableType: typeof(SawmillObject), recipeFamily: this);
        }

        partial void ModsPreInitialize();
        partial void ModsPostInitialize();
    }
    #endregion

}