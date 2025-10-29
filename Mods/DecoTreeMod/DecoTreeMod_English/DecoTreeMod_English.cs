// MOD created by Plex: 3D Model and Code.
// Last update of the mod: 10/16/2025

// Please do not remove the "Registered Mod" section from the code, as it enables receiving compensation from Strange Loop Games when used on an online server.

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
    [LocDescription("Wooden crate, used for making flower boxes.")]
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
    [Ecopedia("Housing Objects", "Outdoor", subPageName: "Palm Box")]
    public partial class Box_Arbre_PalmObject : WorldObject, IRepresentsItem
    {
        public virtual Type RepresentedItemType => typeof(Box_Arbre_PalmItem);
        public override LocString DisplayName => Localizer.DoStr("Palm Box");
        public override TableTextureMode TableTexture => TableTextureMode.Wood;

        partial void ModsPreInitialize();
        partial void ModsPostInitialize();
    }

    [Serialized]
    [LocDisplayName("Palm Box")]
    [LocDescription("Need a vacation? This potted palm brings you tropical vibes without the sand between your toes!")]
    [Ecopedia("Housing Objects", "Outdoor", createAsSubPage: true)]
    [Tag("Housing")]
    [Weight(5000)]
    [Tag(nameof(SurfaceTags.CanBeOnSurface))]
    public partial class Box_Arbre_PalmItem : WorldObjectItem<Box_Arbre_PalmObject>
    {
        protected override OccupancyContext GetOccupancyContext => new SideAttachedContext(0 | DirectionAxisFlags.Down, WorldObject.GetOccupancyInfo(this.WorldObjectType));
    }

    [RequiresSkill(typeof(FarmingSkill), 1)]
    [Ecopedia("Housing Objects", "Outdoor", subPageName: "Palm Box")]
    public partial class Box_Arbre_PalmRecipe : RecipeFamily
    {
        public Box_Arbre_PalmRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "Palm Box",
                displayName: Localizer.DoStr("Palm Box"),

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
            this.Initialize(displayText: Localizer.DoStr("Palm Box"), recipeType: typeof(Box_Arbre_PalmRecipe));
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
    [Ecopedia("Housing Objects", "Outdoor", subPageName: "Oak Box")]
    public partial class Box_Arbre_OakObject : WorldObject, IRepresentsItem
    {
        public virtual Type RepresentedItemType => typeof(Box_Arbre_OakItem);
        public override LocString DisplayName => Localizer.DoStr("Oak Box");
        public override TableTextureMode TableTexture => TableTextureMode.Wood;

        partial void ModsPreInitialize();
        partial void ModsPostInitialize();
    }

    [Serialized]
    [LocDisplayName("Oak Box")]
    [LocDescription("With this oak in a box, nature comes to you—without the acorns... but with style!")]
    [Ecopedia("Housing Objects", "Outdoor", createAsSubPage: true)]
    [Tag("Housing")]
    [Weight(5000)]
    [Tag(nameof(SurfaceTags.CanBeOnSurface))]
    public partial class Box_Arbre_OakItem : WorldObjectItem<Box_Arbre_OakObject>
    {
        protected override OccupancyContext GetOccupancyContext => new SideAttachedContext(0 | DirectionAxisFlags.Down, WorldObject.GetOccupancyInfo(this.WorldObjectType));
    }

    [RequiresSkill(typeof(FarmingSkill), 1)]
    [Ecopedia("Housing Objects", "Outdoor", subPageName: "Oak Box")]
    public partial class Box_Arbre_OakRecipe : RecipeFamily
    {
        public Box_Arbre_OakRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "Oak Box",
                displayName: Localizer.DoStr("Oak Box"),

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
            this.Initialize(displayText: Localizer.DoStr("Oak Box"), recipeType: typeof(Box_Arbre_OakRecipe));
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
    [Ecopedia("Housing Objects", "Outdoor", subPageName: "Redwood Box")]
    public partial class Box_Arbre_RedwoodObject : WorldObject, IRepresentsItem
    {
        public virtual Type RepresentedItemType => typeof(Box_Arbre_RedwoodItem);
        public override LocString DisplayName => Localizer.DoStr("Redwood Box");
        public override TableTextureMode TableTexture => TableTextureMode.Wood;

        partial void ModsPreInitialize();
        partial void ModsPostInitialize();
    }

    [Serialized]
    [LocDisplayName("Redwood Box")]
    [LocDescription("This potted redwood dreams of taking root... but for now, it settles for a quick move in a box!")]
    [Ecopedia("Housing Objects", "Outdoor", createAsSubPage: true)]
    [Tag("Housing")]
    [Weight(5000)]
    [Tag(nameof(SurfaceTags.CanBeOnSurface))]
    public partial class Box_Arbre_RedwoodItem : WorldObjectItem<Box_Arbre_RedwoodObject>
    {
        protected override OccupancyContext GetOccupancyContext => new SideAttachedContext(0 | DirectionAxisFlags.Down, WorldObject.GetOccupancyInfo(this.WorldObjectType));
    }

    [RequiresSkill(typeof(FarmingSkill), 1)]
    [Ecopedia("Housing Objects", "Outdoor", subPageName: "Redwood Box")]
    public partial class Box_Arbre_RedwoodRecipe : RecipeFamily
    {
        public Box_Arbre_RedwoodRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "Redwood Box",
                displayName: Localizer.DoStr("Redwood Box"),

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
            this.Initialize(displayText: Localizer.DoStr("Redwood Box"), recipeType: typeof(Box_Arbre_RedwoodRecipe));
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
    [Ecopedia("Housing Objects", "Outdoor", subPageName: "Birch Box")]
    public partial class Box_Arbre_BirchObject : WorldObject, IRepresentsItem
    {
        public virtual Type RepresentedItemType => typeof(Box_Arbre_BirchItem);
        public override LocString DisplayName => Localizer.DoStr("Birch Box");
        public override TableTextureMode TableTexture => TableTextureMode.Wood;

        partial void ModsPreInitialize();
        partial void ModsPostInitialize();
    }

    [Serialized]
    [LocDisplayName("Birch Box")]
    [LocDescription("The birch tree that dreams of being a bonsai... but with an XXL box, because style matters for trees too!")]
    [Ecopedia("Housing Objects", "Outdoor", createAsSubPage: true)]
    [Tag("Housing")]
    [Weight(5000)]
    [Tag(nameof(SurfaceTags.CanBeOnSurface))]
    public partial class Box_Arbre_BirchItem : WorldObjectItem<Box_Arbre_BirchObject>
    {
        protected override OccupancyContext GetOccupancyContext => new SideAttachedContext(0 | DirectionAxisFlags.Down, WorldObject.GetOccupancyInfo(this.WorldObjectType));
    }

    [RequiresSkill(typeof(FarmingSkill), 1)]
    [Ecopedia("Housing Objects", "Outdoor", subPageName: "Birch Box")]
    public partial class Box_Arbre_BirchRecipe : RecipeFamily
    {
        public Box_Arbre_BirchRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "Birch Box",
                displayName: Localizer.DoStr("Birch Box"),

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
            this.Initialize(displayText: Localizer.DoStr("Birch Box"), recipeType: typeof(Box_Arbre_BirchRecipe));
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
    [RequireRoomVolume(30)]
    [Tag("Usable")]
    [Ecopedia("Housing Objects", "Outdoor", subPageName: "Spruce Box")]
    public partial class Box_Arbre_SpruceObject : WorldObject, IRepresentsItem
    {
        public virtual Type RepresentedItemType => typeof(Box_Arbre_SpruceItem);
        public override LocString DisplayName => Localizer.DoStr("Spruce Box");
        public override TableTextureMode TableTexture => TableTextureMode.Wood;

        partial void ModsPreInitialize();
        partial void ModsPostInitialize();
    }

    [Serialized]
    [LocDisplayName("Spruce Box")]
    [LocDescription("Because a pine tree in a wooden box is nature on the go. Who said you couldn’t carry a forest?")]
    [Ecopedia("Housing Objects", "Outdoor", createAsSubPage: true)]
    [Tag("Housing")]
    [Weight(5000)]
    [Tag(nameof(SurfaceTags.CanBeOnSurface))]
    public partial class Box_Arbre_SpruceItem : WorldObjectItem<Box_Arbre_SpruceObject>
    {
        protected override OccupancyContext GetOccupancyContext => new SideAttachedContext(0 | DirectionAxisFlags.Down, WorldObject.GetOccupancyInfo(this.WorldObjectType));
    }

    [RequiresSkill(typeof(FarmingSkill), 1)]
    [Ecopedia("Housing Objects", "Outdoor", subPageName: "Spruce Box")]
    public partial class Box_Arbre_SpruceRecipe : RecipeFamily
    {
        public Box_Arbre_SpruceRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "Spruce Box",
                displayName: Localizer.DoStr("Spruce Box"),

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
            this.Initialize(displayText: Localizer.DoStr("Spruce Box"), recipeType: typeof(Box_Arbre_SpruceRecipe));
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
    [LocDisplayName("Cactus Box")]
    [LocDescription("A cactus firmly planted in a box, because a regular pot was too simple... and too fragile!")]
    [Ecopedia("Housing Objects", "Outdoor", createAsSubPage: true)]
    [Tag("Housing")]
    [Weight(5000)]
    [Tag(nameof(SurfaceTags.CanBeOnSurface))]
    public partial class Box_Arbre_CactusItem : WorldObjectItem<Box_Arbre_CactusObject>
    {
        protected override OccupancyContext GetOccupancyContext => new SideAttachedContext(0 | DirectionAxisFlags.Down, WorldObject.GetOccupancyInfo(this.WorldObjectType));
    }

    [RequiresSkill(typeof(FarmingSkill), 1)]
    [Ecopedia("Housing Objects", "Outdoor", subPageName: "Cactus Box")]
    public partial class Box_Arbre_CactusRecipe : RecipeFamily
    {
        public Box_Arbre_CactusRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "Cactus Box",
                displayName: Localizer.DoStr("Cactus Box"),

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
            this.Initialize(displayText: Localizer.DoStr("Cactus Box"), recipeType: typeof(Box_Arbre_CactusRecipe));
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
    [Ecopedia("Housing Objects", "Outdoor", subPageName: "Decorative Palm Tree")]
    public partial class Fake_Arbre_PalmObject : WorldObject, IRepresentsItem
    {
        public virtual Type RepresentedItemType => typeof(Fake_Arbre_PalmItem);
        public override LocString DisplayName => Localizer.DoStr("Decorative Palm Tree");
        public override TableTextureMode TableTexture => TableTextureMode.Wood;

        partial void ModsPreInitialize();
        partial void ModsPostInitialize();
    }

    [Serialized]
    [LocDisplayName("Decorative Palm Tree")]
    [LocDescription("The Decorative Palm Tree adds an exotic, tropical touch to your environment in the game Eco, perfect for beautifying your outdoor spaces.")]
    [Ecopedia("Housing Objects", "Outdoor", createAsSubPage: true)]
    [Tag("Housing")]
    [Weight(5000)]
    [Tag(nameof(SurfaceTags.CanBeOnSurface))]
    public partial class Fake_Arbre_PalmItem : WorldObjectItem<Fake_Arbre_PalmObject>
    {
        protected override OccupancyContext GetOccupancyContext => new SideAttachedContext(0 | DirectionAxisFlags.Down, WorldObject.GetOccupancyInfo(this.WorldObjectType));
    }

    [RequiresSkill(typeof(FarmingSkill), 1)]
    [Ecopedia("Housing Objects", "Outdoor", subPageName: "Decorative Palm Tree")]
    public partial class Fake_Arbre_PalmRecipe : RecipeFamily
    {
        public Fake_Arbre_PalmRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "Decorative Palm Tree",
                displayName: Localizer.DoStr("Decorative Palm Tree"),

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
            this.Initialize(displayText: Localizer.DoStr("Decorative Palm Tree"), recipeType: typeof(Fake_Arbre_PalmRecipe));
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
    [Ecopedia("Housing Objects", "Outdoor", subPageName: "Decorative Oak Tree")]
    public partial class Fake_Arbre_OakObject : WorldObject, IRepresentsItem
    {
        public virtual Type RepresentedItemType => typeof(Fake_Arbre_OakItem);
        public override LocString DisplayName => Localizer.DoStr("Decorative Oak Tree");
        public override TableTextureMode TableTexture => TableTextureMode.Wood;

        partial void ModsPreInitialize();
        partial void ModsPostInitialize();
    }

    [Serialized]
    [LocDisplayName("Decorative Oak Tree")]
    [LocDescription("The Decorative Oak Tree brings a natural and elegant ambiance to your landscapes in the game Eco, perfect for creating realistic green spaces.")]
    [Ecopedia("Housing Objects", "Outdoor", createAsSubPage: true)]
    [Tag("Housing")]
    [Weight(5000)]
    [Tag(nameof(SurfaceTags.CanBeOnSurface))]
    public partial class Fake_Arbre_OakItem : WorldObjectItem<Fake_Arbre_OakObject>
    {
        protected override OccupancyContext GetOccupancyContext => new SideAttachedContext(0 | DirectionAxisFlags.Down, WorldObject.GetOccupancyInfo(this.WorldObjectType));
    }

    [RequiresSkill(typeof(FarmingSkill), 1)]
    [Ecopedia("Housing Objects", "Outdoor", subPageName: "Decorative Oak Tree")]
    public partial class Fake_Arbre_OakRecipe : RecipeFamily
    {
        public Fake_Arbre_OakRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "Decorative Oak Tree",
                displayName: Localizer.DoStr("Decorative Oak Tree"),

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
            this.Initialize(displayText: Localizer.DoStr("Decorative Oak Tree"), recipeType: typeof(Fake_Arbre_OakRecipe));
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
    [RequireRoomVolume(30)]
    [Tag("Usable")]
    [Ecopedia("Housing Objects", "Outdoor", subPageName: "Decorative Redwood Tree")]
    public partial class Fake_Arbre_RedwoodObject : WorldObject, IRepresentsItem
    {
        public virtual Type RepresentedItemType => typeof(Fake_Arbre_RedwoodItem);
        public override LocString DisplayName => Localizer.DoStr("Decorative Redwood Tree");
        public override TableTextureMode TableTexture => TableTextureMode.Wood;

        partial void ModsPreInitialize();
        partial void ModsPostInitialize();
    }

    [Serialized]
    [LocDisplayName("Decorative Redwood Tree")]
    [LocDescription("The Decorative Redwood Tree adds a majestic and imposing touch to your décor in Eco, offering natural beauty with its large proportions.")]
    [Ecopedia("Housing Objects", "Outdoor", createAsSubPage: true)]
    [Tag("Housing")]
    [Weight(5000)]
    [Tag(nameof(SurfaceTags.CanBeOnSurface))]
    public partial class Fake_Arbre_RedwoodItem : WorldObjectItem<Fake_Arbre_RedwoodObject>
    {
        protected override OccupancyContext GetOccupancyContext => new SideAttachedContext(0 | DirectionAxisFlags.Down, WorldObject.GetOccupancyInfo(this.WorldObjectType));
    }

    [RequiresSkill(typeof(FarmingSkill), 1)]
    [Ecopedia("Housing Objects", "Outdoor", subPageName: "Decorative Redwood Tree")]
    public partial class Fake_Arbre_RedwoodRecipe : RecipeFamily
    {
        public Fake_Arbre_RedwoodRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "Decorative Redwood Tree",
                displayName: Localizer.DoStr("Decorative Redwood Tree"),

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
            this.Initialize(displayText: Localizer.DoStr("Decorative Redwood Tree"), recipeType: typeof(Fake_Arbre_RedwoodRecipe));
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
    [Ecopedia("Housing Objects", "Outdoor", subPageName: "Decorative Birch Tree")]
    public partial class Fake_Arbre_BirchObject : WorldObject, IRepresentsItem
    {
        public virtual Type RepresentedItemType => typeof(Fake_Arbre_BirchItem);
        public override LocString DisplayName => Localizer.DoStr("Decorative Birch Tree");
        public override TableTextureMode TableTexture => TableTextureMode.Wood;

        partial void ModsPreInitialize();
        partial void ModsPostInitialize();
    }

    [Serialized]
    [LocDisplayName("Decorative Birch Tree")]
    [LocDescription("The Decorative Birch Tree adds simple elegance and a calming atmosphere to your landscapes in Eco, with its distinctive white bark and delicate leaves.")]
    [Ecopedia("Housing Objects", "Outdoor", createAsSubPage: true)]
    [Tag("Housing")]
    [Weight(5000)]
    [Tag(nameof(SurfaceTags.CanBeOnSurface))]
    public partial class Fake_Arbre_BirchItem : WorldObjectItem<Fake_Arbre_BirchObject>
    {
        protected override OccupancyContext GetOccupancyContext => new SideAttachedContext(0 | DirectionAxisFlags.Down, WorldObject.GetOccupancyInfo(this.WorldObjectType));
    }

    [RequiresSkill(typeof(FarmingSkill), 1)]
    [Ecopedia("Housing Objects", "Outdoor", subPageName: "Decorative Birch Tree")]
    public partial class Fake_Arbre_BirchRecipe : RecipeFamily
    {
        public Fake_Arbre_BirchRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "Decorative Birch Tree",
                displayName: Localizer.DoStr("Decorative Birch Tree"),

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
            this.Initialize(displayText: Localizer.DoStr("Decorative Birch Tree"), recipeType: typeof(Fake_Arbre_BirchRecipe));
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
    [Ecopedia("Housing Objects", "Outdoor", subPageName: "Decorative Cactus Tree")]
    public partial class Fake_Arbre_CactusObject : WorldObject, IRepresentsItem
    {
        public virtual Type RepresentedItemType => typeof(Fake_Arbre_CactusItem);
        public override LocString DisplayName => Localizer.DoStr("Decorative Cactus Tree");
        public override TableTextureMode TableTexture => TableTextureMode.Wood;

        partial void ModsPreInitialize();
        partial void ModsPostInitialize();
    }

    [Serialized]
    [LocDisplayName("Decorative Cactus Tree")]
    [LocDescription("The Decorative Cactus Tree adds a desert-like and unique touch to your Eco landscapes, perfect for creating arid and one-of-a-kind atmospheres.")]
    [Ecopedia("Housing Objects", "Outdoor", createAsSubPage: true)]
    [Tag("Housing")]
    [Weight(5000)]
    [Tag(nameof(SurfaceTags.CanBeOnSurface))]
    public partial class Fake_Arbre_CactusItem : WorldObjectItem<Fake_Arbre_CactusObject>
    {
        protected override OccupancyContext GetOccupancyContext => new SideAttachedContext(0 | DirectionAxisFlags.Down, WorldObject.GetOccupancyInfo(this.WorldObjectType));
    }

    [RequiresSkill(typeof(FarmingSkill), 1)]
    [Ecopedia("Housing Objects", "Outdoor", subPageName: "Decorative Cactus Tree")]
    public partial class Fake_Arbre_CactusRecipe : RecipeFamily
    {
        public Fake_Arbre_CactusRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "Decorative Cactus Tree",
                displayName: Localizer.DoStr("Decorative Cactus Tree"),

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
            this.Initialize(displayText: Localizer.DoStr("Decorative Cactus Tree"), recipeType: typeof(Fake_Arbre_CactusRecipe));
            this.ModsPostInitialize();

            CraftingComponent.AddRecipe(tableType: typeof(FarmersTableObject), recipeFamily: this);
        }

        partial void ModsPreInitialize();

        partial void ModsPostInitialize();
    }
    #endregion



    #region Left Shutter
    // ______________________________________________________ Volet_HardWood_Gauche ______________________________________________________ \\

    [Serialized]
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(HousingComponent))]
    [RequireComponent(typeof(OccupancyRequirementComponent))]
    [RequireComponent(typeof(ForSaleComponent))]
    [RequireComponent(typeof(RoomRequirementsComponent))]
    [Tag("Usable")]
    [Ecopedia("Housing Objects", "Decoration", subPageName: "Left Window Shutter")]
    [Tag(nameof(SurfaceTags.HasTableSurface))]
    public partial class Volet_HardWood_GaucheObject : WorldObject, IRepresentsItem
    {
        public virtual Type RepresentedItemType => typeof(Volet_HardWood_GaucheItem);
        public override LocString DisplayName => Localizer.DoStr("Left Window Shutter");
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
    [LocDisplayName("Left Window Shutter")]
    [LocDescription("A sturdy wooden shutter to hang on windows to add charm and authenticity to houses.")]
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
    [Ecopedia("Housing Objects", "Decoration", subPageName: "Left Window Shutter")]
    public partial class Volet_HardWood_GaucheRecipe : RecipeFamily
    {
        public Volet_HardWood_GaucheRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "Volet_HardWood_gauche",
                displayName: Localizer.DoStr("Left Window Shutter"),

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
            this.Initialize(displayText: Localizer.DoStr("Left Window Shutter"), recipeType: typeof(Volet_HardWood_GaucheRecipe));
            this.ModsPostInitialize();

            CraftingComponent.AddRecipe(tableType: typeof(CarpentryTableObject), recipeFamily: this);
        }

        partial void ModsPreInitialize();
        partial void ModsPostInitialize();
    }
    #endregion



    #region Right Shutter
    // ______________________________________________________ Right Window Shutter ______________________________________________________ \\

    [Serialized]
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(HousingComponent))]
    [RequireComponent(typeof(OccupancyRequirementComponent))]
    [RequireComponent(typeof(ForSaleComponent))]
    [RequireComponent(typeof(RoomRequirementsComponent))]
    [Tag("Usable")]
    [Ecopedia("Housing Objects", "Decoration", subPageName: "Right Window Shutter")]
    [Tag(nameof(SurfaceTags.HasTableSurface))]
    public partial class Volet_HardWood_DroiteObject : WorldObject, IRepresentsItem
    {
        public virtual Type RepresentedItemType => typeof(Volet_HardWood_DroiteItem);
        public override LocString DisplayName => Localizer.DoStr("Right Window Shutter");
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
    [LocDisplayName("Right Window Shutter")]
    [LocDescription("A sturdy wooden shutter to hang on windows to add charm and authenticity to houses.")]
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
    [Ecopedia("Housing Objects", "Decoration", subPageName: "Right Window Shutter")]
    public partial class Volet_HardWood_DroiteRecipe : RecipeFamily
    {
        public Volet_HardWood_DroiteRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "Volet_HardWood_Droite",
                displayName: Localizer.DoStr("Right Window Shutter"),

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
            this.Initialize(displayText: Localizer.DoStr("Right Window Shutter"), recipeType: typeof(Volet_HardWood_DroiteRecipe));
            this.ModsPostInitialize();

            CraftingComponent.AddRecipe(tableType: typeof(CarpentryTableObject), recipeFamily: this);
        }

        partial void ModsPreInitialize();
        partial void ModsPostInitialize();
    }
    #endregion



    #region Potted Fern
    // ______________________________________________________ Potted Fern ______________________________________________________ \\
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
    [Ecopedia("Housing Objects", "Decoration", subPageName: "Potted Fern")]
    public partial class Pot01Object : WorldObject, IRepresentsItem
    {
        public virtual Type RepresentedItemType => typeof(Pot01Item);
        public override LocString DisplayName => Localizer.DoStr("Potted Fern");
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
    [LocDisplayName("Potted Fern")]
    [LocDescription("Decorative plant to beautify your interior.")]
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
    [Ecopedia("Housing Objects", "Decoration", subPageName: "Potted Fern")]
    public partial class Pot01Recipe : RecipeFamily
    {
        public Pot01Recipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "Fougère en pot",
                displayName: Localizer.DoStr("Potted Fern"),

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
            this.Initialize(displayText: Localizer.DoStr("Potted Fern"), recipeType: typeof(Pot01Recipe));
            this.ModsPostInitialize();

            CraftingComponent.AddRecipe(tableType: typeof(MasonryTableObject), recipeFamily: this);
        }

        partial void ModsPreInitialize();
        partial void ModsPostInitialize();
    }
    #endregion



    #region Hanging Flower Pot
    // ______________________________________________________ Hanging Flower Pot ______________________________________________________ \\
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
    [Ecopedia("Housing Objects", "Decoration", subPageName: "Hanging Flower Pot")]
    public partial class Pot02Object : WorldObject, IRepresentsItem
    {
        public virtual Type RepresentedItemType => typeof(Pot02Item);
        public override LocString DisplayName => Localizer.DoStr("Hanging Flower Pot");
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
    [LocDisplayName("Hanging Flower Pot")]
    [LocDescription("Decorative hanging plant.")]
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
            TypeForRoomLimit = Localizer.DoStr("Decoration"),
            DiminishingReturnMultiplier = 0.4f
        };
    }

    [RequiresSkill(typeof(PotterySkill), 3)]
    [Ecopedia("Housing Objects", "Decoration", subPageName: "Hanging Flower Pot")]
    public partial class Pot02Recipe : RecipeFamily
    {
        public Pot02Recipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "Pot02",
                displayName: Localizer.DoStr("Hanging Flower Pot"),

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
            this.Initialize(displayText: Localizer.DoStr("Hanging Flower Pot"), recipeType: typeof(Pot02Recipe));
            this.ModsPostInitialize();

            CraftingComponent.AddRecipe(tableType: typeof(PotteryTableObject), recipeFamily: this);
        }

        partial void ModsPreInitialize();
        partial void ModsPostInitialize();
    }
    #endregion



    #region 1x1 Garden Planter
    // ______________________________________________________ 1x1 Garden Planter ______________________________________________________ \\
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
    [Ecopedia("Housing Objects", "Decoration", subPageName: "1x1 Garden Planter")]
    public partial class Pot03Object : WorldObject, IRepresentsItem
    {
        public virtual Type RepresentedItemType => typeof(Pot03Item);
        public override LocString DisplayName => Localizer.DoStr("1x1 Garden Planter");
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
    [LocDisplayName("1x1 Garden Planter")]
    [LocDescription("Small decorative planter with green plants.")]
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
            TypeForRoomLimit = Localizer.DoStr("Decoration"),
            DiminishingReturnMultiplier = 0.4f
        };
    }

    [RequiresSkill(typeof(MasonrySkill), 3)]
    [Ecopedia("Housing Objects", "Decoration", subPageName: "1x1 Garden Planter")]
    public partial class Pot03Recipe : RecipeFamily
    {
        public Pot03Recipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "Bac de jardin 1x1",
                displayName: Localizer.DoStr("1x1 Garden Planter"),

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
            this.Initialize(displayText: Localizer.DoStr("1x1 Garden Planter"), recipeType: typeof(Pot03Recipe));
            this.ModsPostInitialize();

            CraftingComponent.AddRecipe(tableType: typeof(MasonryTableObject), recipeFamily: this);
        }

        partial void ModsPreInitialize();
        partial void ModsPostInitialize();
    }
    #endregion



    #region 2x2 Garden Planter
    // ______________________________________________________ 2x2 Garden Planter ______________________________________________________ \\
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
    [Ecopedia("Housing Objects", "Decoration", subPageName: "2x2 Garden Planter")]
    public partial class Pot04Object : WorldObject, IRepresentsItem
    {
        public virtual Type RepresentedItemType => typeof(Pot04Item);
        public override LocString DisplayName => Localizer.DoStr("2x2 Garden Planter");
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
    [LocDisplayName("2x2 Garden Planter")]
    [LocDescription("A sturdy concrete planter filled with vegetation, perfect for enhancing urban areas.")]
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
            TypeForRoomLimit = Localizer.DoStr("Decoration"),
            DiminishingReturnMultiplier = 0.4f
        };
    }

    [RequiresSkill(typeof(MasonrySkill), 3)]
    [Ecopedia("Housing Objects", "Decoration", subPageName: "2x2 Garden Planter")]
    public partial class Pot04Recipe : RecipeFamily
    {
        public Pot04Recipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "Bac de jardin 2x2",
                displayName: Localizer.DoStr("2x2 Garden Planter"),

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
            this.Initialize(displayText: Localizer.DoStr("2x2 Garden Planter"), recipeType: typeof(Pot04Recipe));
            this.ModsPostInitialize();

            CraftingComponent.AddRecipe(tableType: typeof(MasonryTableObject), recipeFamily: this);
        }

        partial void ModsPreInitialize();
        partial void ModsPostInitialize();
    }
    #endregion



    #region Modern Rectangular Planter
    // ______________________________________________________ Modern Rectangular Planter ______________________________________________________ \\
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
    [Ecopedia("Housing Objects", "Decoration", subPageName: "Modern Rectangular Planter")]
    public partial class Pot05Object : WorldObject, IRepresentsItem
    {
        public virtual Type RepresentedItemType => typeof(Pot05Item);
        public override LocString DisplayName => Localizer.DoStr("Modern Rectangular Planter");
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
    [LocDisplayName("Modern Rectangular Planter")]
    [LocDescription("A stylish dark stone planter, perfect for adding greenery to sidewalks and buildings.")]
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
            TypeForRoomLimit = Localizer.DoStr("Decoration"),
            DiminishingReturnMultiplier = 0.4f
        };
    }

    [RequiresSkill(typeof(MasonrySkill), 1)]
    [Ecopedia("Housing Objects", "Decoration", subPageName: "Modern Rectangular Planter")]
    public partial class Pot05Recipe : RecipeFamily
    {
        public Pot05Recipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "Modern Rectangular Planter",
                displayName: Localizer.DoStr("Modern Rectangular Planter"),

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
            this.Initialize(displayText: Localizer.DoStr("Modern Rectangular Planter"), recipeType: typeof(Pot05Recipe));
            this.ModsPostInitialize();

            CraftingComponent.AddRecipe(tableType: typeof(MasonryTableObject), recipeFamily: this);
        }

        partial void ModsPreInitialize();
        partial void ModsPostInitialize();
    }
    #endregion



    #region Stone Spherical Pot
    // ______________________________________________________ Stone Spherical Pot ______________________________________________________ \\

    [Serialized]
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(HousingComponent))]
    [RequireComponent(typeof(OccupancyRequirementComponent))]
    [RequireComponent(typeof(ForSaleComponent))]
    [RequireComponent(typeof(PaintableComponent))]
    [Tag("Usable")]
    [Ecopedia("Housing Objects", "Decoration", subPageName: "Stone Spherical Pot")]
    public partial class Pot06Object : WorldObject, IRepresentsItem
    {
        public virtual Type RepresentedItemType => typeof(Pot06Item);
        public override LocString DisplayName => Localizer.DoStr("Stone Spherical Pot"); // Adjusted from "Agave_Fake Blanc"
        public override TableTextureMode TableTexture => TableTextureMode.Stone;

        partial void ModsPreInitialize();
        partial void ModsPostInitialize();
    }

    [Serialized]
    [LocDisplayName("Stone Spherical Pot")]
    [LocDescription("A small round pot with a sleek design, perfect for decorating both indoor and outdoor spaces.")]
    [Ecopedia("Housing Objects", "Decoration", createAsSubPage: true)]
    [Tag("Housing")]
    [Weight(100)]
    [Tag(nameof(SurfaceTags.CanBeOnSurface))]
    public partial class Pot06Item : WorldObjectItem<Pot06Object>
    {
        protected override OccupancyContext GetOccupancyContext => new SideAttachedContext(0 | DirectionAxisFlags.Down, WorldObject.GetOccupancyInfo(this.WorldObjectType));
    }

    [RequiresSkill(typeof(MasonrySkill), 1)]
    [Ecopedia("Housing Objects", "Decoration", subPageName: "Stone Spherical Pot")]
    public partial class Pot06Recipe : RecipeFamily
    {
        public Pot06Recipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "Stone Spherical Pot",
                displayName: Localizer.DoStr("Stone Spherical Pot"),

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
            this.Initialize(displayText: Localizer.DoStr("Stone Spherical Pot"), recipeType: typeof(Pot06Recipe));
            this.ModsPostInitialize();

            CraftingComponent.AddRecipe(tableType: typeof(MasonryTableObject), recipeFamily: this);
        }

        partial void ModsPreInitialize();
        partial void ModsPostInitialize();
    }
    #endregion



    #region Garden Planter 2x3
    // ______________________________________________________ Garden Planter 2x3 ______________________________________________________ \\

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
    [Ecopedia("Housing Objects", "Decoration", subPageName: "Garden Planter 2x3")]
    public partial class Pot07Object : WorldObject, IRepresentsItem
    {
        public virtual Type RepresentedItemType => typeof(Pot07Item);
        public override LocString DisplayName => Localizer.DoStr("Garden Planter 2x3");
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
    [LocDisplayName("Garden Planter 2x3")]
    [LocDescription("A large, sturdy concrete planter filled with vegetation, perfect for beautifying urban areas.")]
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
            TypeForRoomLimit = Localizer.DoStr("Decoration"),
            DiminishingReturnMultiplier = 0.4f
        };
    }

    [RequiresSkill(typeof(MasonrySkill), 4)]
    [Ecopedia("Housing Objects", "Decoration", subPageName: "Garden Planter 2x3")]
    public partial class Pot07Recipe : RecipeFamily
    {
        public Pot07Recipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "Garden Planter 2x3",
                displayName: Localizer.DoStr("Garden Planter 2x3"),

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
            this.Initialize(displayText: Localizer.DoStr("Garden Planter 2x3"), recipeType: typeof(Pot07Recipe));
            this.ModsPostInitialize();

            CraftingComponent.AddRecipe(tableType: typeof(MasonryTableObject), recipeFamily: this);
        }

        partial void ModsPreInitialize();
        partial void ModsPostInitialize();
    }
    #endregion



    #region Wooden Garden Planter
    // ______________________________________________________ Wooden Garden Planter ______________________________________________________ \\

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
    [Ecopedia("Housing Objects", "Decoration", subPageName: "Wooden Garden Planter")]
    public partial class Pot08Object : WorldObject, IRepresentsItem
    {
        public virtual Type RepresentedItemType => typeof(Pot08Item);
        public override LocString DisplayName => Localizer.DoStr("Wooden Garden Planter");
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
    [LocDisplayName("Wooden Garden Planter")]
    [LocDescription("Small decorative wooden planter with green plants.")]
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
            TypeForRoomLimit = Localizer.DoStr("Decoration"),
            DiminishingReturnMultiplier = 0.4f
        };
    }

    [RequiresSkill(typeof(CarpentrySkill), 2)]
    [Ecopedia("Housing Objects", "Decoration", subPageName: "Wooden Garden Planter")]
    public partial class Pot08Recipe : RecipeFamily
    {
        public Pot08Recipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "Wooden Garden Planter",
                displayName: Localizer.DoStr("Wooden Garden Planter"),

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
            this.Initialize(displayText: Localizer.DoStr("Wooden Garden Planter"), recipeType: typeof(Pot08Recipe));
            this.ModsPostInitialize();

            CraftingComponent.AddRecipe(tableType: typeof(SawmillObject), recipeFamily: this);
        }

        partial void ModsPreInitialize();
        partial void ModsPostInitialize();
    }
    #endregion


}