// МОД создан Plex: 3D-модель и код.  
// Последнее обновление мода: 16/10/2025  
// Перевод от Skull90  

// Пожалуйста, не удаляйте раздел "Registered Mod" из кода, так как он позволяет получать вознаграждение от Strange Loop Games при его использовании на онлайн-сервере.

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
                displayName: Localizer.DoStr("Ящик"),

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
            this.Initialize(displayText: Localizer.DoStr("Ящик"), recipeType: typeof(BoxRecipe));
            this.ModsPostInitialize();

            CraftingComponent.AddRecipe(tableType: typeof(CarpentryTableObject), recipeFamily: this);
        }

        partial void ModsPreInitialize();
        partial void ModsPostInitialize();
    }


    [Serialized]
    [LocDisplayName("Ящик")]
    [Weight(500)]
    [Tag("Currency")]
    [Currency]
    [Ecopedia("Items", "Products", createAsSubPage: true)]
    [LocDescription("Деревянный Ящик, используемый для изготовления Цветочных ящиков")]
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
    [Ecopedia("Housing Objects", "Outdoor", subPageName: "Ящик с пальмой")]
    public partial class Box_Arbre_PalmObject : WorldObject, IRepresentsItem
    {
        public virtual Type RepresentedItemType => typeof(Box_Arbre_PalmItem);
        public override LocString DisplayName => Localizer.DoStr("Ящик с пальмой");
        public override TableTextureMode TableTexture => TableTextureMode.Wood;

        partial void ModsPreInitialize();
        partial void ModsPostInitialize();
    }

    [Serialized]
    [LocDisplayName("Ящик с пальмой")]
    [LocDescription("Хотите отдохнуть? Этот Ящик с пальмой подарит вам тропическую атмосферу, но при этом у вас не будет песка между пальцами ног!")]
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
                displayName: Localizer.DoStr("Ящик с пальмой"),

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
            this.Initialize(displayText: Localizer.DoStr("Ящик с пальмой"), recipeType: typeof(Box_Arbre_PalmRecipe));
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
        public override LocString DisplayName => Localizer.DoStr("Ящик с дубом");
        public override TableTextureMode TableTexture => TableTextureMode.Wood;

        partial void ModsPreInitialize();
        partial void ModsPostInitialize();
    }

    [Serialized]
    [LocDisplayName("Ящик с дубом")]
    [LocDescription("С этим дубом в ящике природа приходит к вам сама — без желудей... но стильно!")]
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
                displayName: Localizer.DoStr("Ящик с дубом"),

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
            this.Initialize(displayText: Localizer.DoStr("Ящик с дубом"), recipeType: typeof(Box_Arbre_OakRecipe));
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
        public override LocString DisplayName => Localizer.DoStr("Ящик с секвойей");
        public override TableTextureMode TableTexture => TableTextureMode.Wood;

        partial void ModsPreInitialize();
        partial void ModsPostInitialize();
    }

    [Serialized]
    [LocDisplayName("Ящик с секвойей")]
    [LocDescription("Это красное дерево в горшке мечтает о том, чтобы пустить корни... но пока его можно быстро переставить в ящик!")]
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
                displayName: Localizer.DoStr("Ящик с секвойей"),

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
            this.Initialize(displayText: Localizer.DoStr("Ящик с секвойей"), recipeType: typeof(Box_Arbre_RedwoodRecipe));
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
        public override LocString DisplayName => Localizer.DoStr("Ящик с березой");
        public override TableTextureMode TableTexture => TableTextureMode.Wood;

        partial void ModsPreInitialize();
        partial void ModsPostInitialize();
    }

    [Serialized]
    [LocDisplayName("Ящик с березой")]
    [LocDescription("Береза, которая мечтает стать бонсай... но в коробке XXL, потому что стиль важен и для деревьев тоже!")]
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
                displayName: Localizer.DoStr("Ящик с березой"),

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
            this.Initialize(displayText: Localizer.DoStr("Ящик с березой"), recipeType: typeof(Box_Arbre_BirchRecipe));
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
        public override LocString DisplayName => Localizer.DoStr("Ящик с елью");
        public override TableTextureMode TableTexture => TableTextureMode.Wood;

        partial void ModsPreInitialize();
        partial void ModsPostInitialize();
    }

    [Serialized]
    [LocDisplayName("Ящик с елью")]
    [LocDescription("Потому что сосна в деревянном ящике - это природа в движении. Кто сказал, что нельзя носить с собой лес?")]
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
                displayName: Localizer.DoStr("Ящик с елью"),

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
            this.Initialize(displayText: Localizer.DoStr("Ящик с елью"), recipeType: typeof(Box_Arbre_SpruceRecipe));
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
        public override LocString DisplayName => Localizer.DoStr("Ящик с кактусом");
        public override TableTextureMode TableTexture => TableTextureMode.Wood;

        partial void ModsPreInitialize();
        partial void ModsPostInitialize();
    }

    [Serialized]
    [LocDisplayName("Ящик с кактусом")]
    [LocDescription("Кактус, прочно посаженный в ящик, потому что обычный горшок был слишком простым... и слишком хрупким!")]
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
                displayName: Localizer.DoStr("Ящик с кактусом"),

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
            this.Initialize(displayText: Localizer.DoStr("Ящик с кактусом"), recipeType: typeof(Box_Arbre_CactusRecipe));
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
        public override LocString DisplayName => Localizer.DoStr("Декоративная пальма");
        public override TableTextureMode TableTexture => TableTextureMode.Wood;

        partial void ModsPreInitialize();
        partial void ModsPostInitialize();
    }

    [Serialized]
    [LocDisplayName("Декоративная пальма")]
    [LocDescription("Декоративная пальма в игре Eco привносит экзотический тропический оттенок в ваше окружение и идеально подходит для украшения ваших открытых пространств.")]
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
                displayName: Localizer.DoStr("Декоративная пальма"),

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
            this.Initialize(displayText: Localizer.DoStr("Декоративная пальма"), recipeType: typeof(Fake_Arbre_PalmRecipe));
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
        public override LocString DisplayName => Localizer.DoStr("Декоративный дуб");
        public override TableTextureMode TableTexture => TableTextureMode.Wood;

        partial void ModsPreInitialize();
        partial void ModsPostInitialize();
    }

    [Serialized]
    [LocDisplayName("Декоративный дуб")]
    [LocDescription("Декоративный дуб привносит естественную и элегантную атмосферу в ваши пейзажи в игре Eco, идеально подходящую для создания реалистичных зеленых насаждений.")]
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
                displayName: Localizer.DoStr("Декоративный дуб"),

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
            this.Initialize(displayText: Localizer.DoStr("Декоративный дуб"), recipeType: typeof(Fake_Arbre_OakRecipe));
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
        public override LocString DisplayName => Localizer.DoStr("Декоративная секвойя");
        public override TableTextureMode TableTexture => TableTextureMode.Wood;

        partial void ModsPreInitialize();
        partial void ModsPostInitialize();
    }

    [Serialized]
    [LocDisplayName("Декоративная секвойя")]
    [LocDescription("Декоративное красное дерево придаст вашему интерьеру в эко-стиле величественный и импозантный оттенок, предлагая естественную красоту благодаря своим большим пропорциям.")]
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
                displayName: Localizer.DoStr("Декоративная секвойя"),

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
            this.Initialize(displayText: Localizer.DoStr("Декоративная секвойя"), recipeType: typeof(Fake_Arbre_RedwoodRecipe));
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
        public override LocString DisplayName => Localizer.DoStr("Декоративная береза");
        public override TableTextureMode TableTexture => TableTextureMode.Wood;

        partial void ModsPreInitialize();
        partial void ModsPostInitialize();
    }

    [Serialized]
    [LocDisplayName("Декоративная береза")]
    [LocDescription("Декоративная береза с ее характерной белой корой и нежными листьями придает вашим пейзажам в Эко-стиле простую элегантность и умиротворяющую атмосферу.")]
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
                displayName: Localizer.DoStr("Декоративная береза"),

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
            this.Initialize(displayText: Localizer.DoStr("Декоративная береза"), recipeType: typeof(Fake_Arbre_BirchRecipe));
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
        public override LocString DisplayName => Localizer.DoStr("Декоративный кактус");
        public override TableTextureMode TableTexture => TableTextureMode.Wood;

        partial void ModsPreInitialize();
        partial void ModsPostInitialize();
    }

    [Serialized]
    [LocDisplayName("Декоративный кактус")]
    [LocDescription("Декоративный кактус придает вашим экологическим ландшафтам сходство с пустынным озером и неповторимую нотку, идеально подходящую для создания уникальной атмосферы")]
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
                displayName: Localizer.DoStr("Декоративный кактус"),

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
            this.Initialize(displayText: Localizer.DoStr("Декоративный кактус"), recipeType: typeof(Fake_Arbre_CactusRecipe));
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
    [Ecopedia("Housing Objects", "Decoration", subPageName: "Левый ставень окна")]
    [Tag(nameof(SurfaceTags.HasTableSurface))]
    public partial class Volet_HardWood_GaucheObject : WorldObject, IRepresentsItem
    {
        public virtual Type RepresentedItemType => typeof(Volet_HardWood_GaucheItem);
        public override LocString DisplayName => Localizer.DoStr("Левый ставень окна");
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
    [LocDisplayName("Левый ставень окна")]
    [LocDescription("Прочный деревянный ставень для крепления на окна, добавляющий домам шарм и аутентичность.")]
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
    [Ecopedia("Housing Objects", "Decoration", subPageName: "Левый ставень окна")]
    public partial class Volet_HardWood_GaucheRecipe : RecipeFamily
    {
        public Volet_HardWood_GaucheRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "Volet_HardWood_gauche",
                displayName: Localizer.DoStr("Левый ставень окна"),

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
            this.Initialize(displayText: Localizer.DoStr("Левый ставень окна"), recipeType: typeof(Volet_HardWood_GaucheRecipe));
            this.ModsPostInitialize();

            CraftingComponent.AddRecipe(tableType: typeof(CarpentryTableObject), recipeFamily: this);
        }

        partial void ModsPreInitialize();
        partial void ModsPostInitialize();
    }
    #endregion



    #region Volet Droite
    // ______________________________________________________ Правый ставень окна ______________________________________________________ \\

    [Serialized]
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(HousingComponent))]
    [RequireComponent(typeof(OccupancyRequirementComponent))]
    [RequireComponent(typeof(ForSaleComponent))]
    [RequireComponent(typeof(RoomRequirementsComponent))]
    [Tag("Usable")]
    [Ecopedia("Housing Objects", "Decoration", subPageName: "Правый ставень окна")]
    [Tag(nameof(SurfaceTags.HasTableSurface))]
    public partial class Volet_HardWood_DroiteObject : WorldObject, IRepresentsItem
    {
        public virtual Type RepresentedItemType => typeof(Volet_HardWood_DroiteItem);
        public override LocString DisplayName => Localizer.DoStr("Правый ставень окна");
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
    [LocDisplayName("Правый ставень окна")]
    [LocDescription("Прочный деревянный ставень для крепления на окна, добавляющий домам шарм и аутентичность.")]
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
    [Ecopedia("Housing Objects", "Decoration", subPageName: "Правый ставень окна")]
    public partial class Volet_HardWood_DroiteRecipe : RecipeFamily
    {
        public Volet_HardWood_DroiteRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "Volet_HardWood_Droite",
                displayName: Localizer.DoStr("Правый ставень окна"),

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
            this.Initialize(displayText: Localizer.DoStr("Правый ставень окна"), recipeType: typeof(Volet_HardWood_DroiteRecipe));
            this.ModsPostInitialize();

            CraftingComponent.AddRecipe(tableType: typeof(CarpentryTableObject), recipeFamily: this);
        }

        partial void ModsPreInitialize();
        partial void ModsPostInitialize();
    }
    #endregion



    #region Fougère en pot
    // ______________________________________________________ Папоротник в горшке ______________________________________________________ \\

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
    [Ecopedia("Housing Objects", "Decoration", subPageName: "Папоротник в горшке")]
    public partial class Pot01Object : WorldObject, IRepresentsItem
    {
        public virtual Type RepresentedItemType => typeof(Pot01Item);
        public override LocString DisplayName => Localizer.DoStr("Папоротник в горшке");
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
    [LocDisplayName("Папоротник в горшке")]
    [LocDescription("Декоративное растение для украшения вашего интерьера.")]
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
    [Ecopedia("Housing Objects", "Decoration", subPageName: "Папоротник в горшке")]
    public partial class Pot01Recipe : RecipeFamily
    {
        public Pot01Recipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "Fougère en pot",
                displayName: Localizer.DoStr("Папоротник в горшке"),

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
            this.Initialize(displayText: Localizer.DoStr("Папоротник в горшке"), recipeType: typeof(Pot01Recipe));
            this.ModsPostInitialize();

            CraftingComponent.AddRecipe(tableType: typeof(MasonryTableObject), recipeFamily: this);
        }

        partial void ModsPreInitialize();
        partial void ModsPostInitialize();
    }
    #endregion



    #region Suspension florale
    // ______________________________________________________ Цветочная подвеска ______________________________________________________ \\

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
    [Ecopedia("Housing Objects", "Decoration", subPageName: "Цветочная подвеска")]
    public partial class Pot02Object : WorldObject, IRepresentsItem
    {
        public virtual Type RepresentedItemType => typeof(Pot02Item);
        public override LocString DisplayName => Localizer.DoStr("Цветочная подвеска");
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
    [LocDisplayName("Цветочная подвеска")]
    [LocDescription("Декоративное подвесное растение.")]
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
    [Ecopedia("Housing Objects", "Decoration", subPageName: "Цветочная подвеска")]
    public partial class Pot02Recipe : RecipeFamily
    {
        public Pot02Recipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "Pot02",
                displayName: Localizer.DoStr("Цветочная подвеска"),

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
            this.Initialize(displayText: Localizer.DoStr("Цветочная подвеска"), recipeType: typeof(Pot02Recipe));
            this.ModsPostInitialize();

            CraftingComponent.AddRecipe(tableType: typeof(PotteryTableObject), recipeFamily: this);
        }

        partial void ModsPreInitialize();
        partial void ModsPostInitialize();
    }
    #endregion



    #region Bac de jardin 1x1
    // ______________________________________________________ Садовый контейнер 1x1 ______________________________________________________ \\

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
    [Ecopedia("Housing Objects", "Decoration", subPageName: "Садовый контейнер 1x1")]
    public partial class Pot03Object : WorldObject, IRepresentsItem
    {
        public virtual Type RepresentedItemType => typeof(Pot03Item);
        public override LocString DisplayName => Localizer.DoStr("Садовый контейнер 1x1");
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
    [LocDisplayName("Садовый контейнер 1x1")]
    [LocDescription("Небольшой декоративный контейнер с зелеными растениями.")]
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
    [Ecopedia("Housing Objects", "Decoration", subPageName: "Садовый контейнер 1x1")]
    public partial class Pot03Recipe : RecipeFamily
    {
        public Pot03Recipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "Bac de jardin 1x1",
                displayName: Localizer.DoStr("Садовый контейнер 1x1"),

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
            this.Initialize(displayText: Localizer.DoStr("Садовый контейнер 1x1"), recipeType: typeof(Pot03Recipe));
            this.ModsPostInitialize();

            CraftingComponent.AddRecipe(tableType: typeof(MasonryTableObject), recipeFamily: this);
        }

        partial void ModsPreInitialize();
        partial void ModsPostInitialize();
    }
    #endregion



    #region Bac de jardin 2x2
    // ______________________________________________________ Садовый контейнер 2x2 ______________________________________________________ \\

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
    [Ecopedia("Housing Objects", "Decoration", subPageName: "Садовый контейнер 2x2")]
    public partial class Pot04Object : WorldObject, IRepresentsItem
    {
        public virtual Type RepresentedItemType => typeof(Pot04Item);
        public override LocString DisplayName => Localizer.DoStr("Садовый контейнер 2x2");
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
    [LocDisplayName("Садовый контейнер 2x2")]
    [LocDescription("Прочный бетонный контейнер, заполненный растительностью, идеально подходит для украшения городских зон.")]
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
    [Ecopedia("Housing Objects", "Decoration", subPageName: "Садовый контейнер 2x2")]
    public partial class Pot04Recipe : RecipeFamily
    {
        public Pot04Recipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "Bac de jardin 2x2",
                displayName: Localizer.DoStr("Садовый контейнер 2x2"),

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
            this.Initialize(displayText: Localizer.DoStr("Садовый контейнер 2x2"), recipeType: typeof(Pot04Recipe));
            this.ModsPostInitialize();

            CraftingComponent.AddRecipe(tableType: typeof(MasonryTableObject), recipeFamily: this);
        }

        partial void ModsPreInitialize();
        partial void ModsPostInitialize();
    }
    #endregion



    #region Bac Rectangulaire Moderne
    // ______________________________________________________ Современный прямоугольный контейнер ______________________________________________________ \\

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
    [Ecopedia("Housing Objects", "Decoration", subPageName: "Современный прямоугольный контейнер")]
    public partial class Pot05Object : WorldObject, IRepresentsItem
    {
        public virtual Type RepresentedItemType => typeof(Pot05Item);
        public override LocString DisplayName => Localizer.DoStr("Современный прямоугольный контейнер");
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
    [LocDisplayName("Современный прямоугольный контейнер")]
    [LocDescription("Элегантная клумба из темного камня, идеально подходит для добавления зелени на тротуары и здания.")]
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
    [Ecopedia("Housing Objects", "Decoration", subPageName: "Современный прямоугольный контейнер")]
    public partial class Pot05Recipe : RecipeFamily
    {
        public Pot05Recipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "Bac Rectangulaire Moderne",
                displayName: Localizer.DoStr("Современный прямоугольный контейнер"),

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
            this.Initialize(displayText: Localizer.DoStr("Современный прямоугольный контейнер"), recipeType: typeof(Pot05Recipe));
            this.ModsPostInitialize();

            CraftingComponent.AddRecipe(tableType: typeof(MasonryTableObject), recipeFamily: this);
        }

        partial void ModsPreInitialize();
        partial void ModsPostInitialize();
    }
    #endregion



    #region Pot Sphérique en Pierre
    // ______________________________________________________ Круглый каменный горшок ______________________________________________________ \\

    [Serialized]
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(HousingComponent))]
    [RequireComponent(typeof(OccupancyRequirementComponent))]
    [RequireComponent(typeof(ForSaleComponent))]
    [RequireComponent(typeof(PaintableComponent))]
    [Tag("Usable")]
    [Ecopedia("Housing Objects", "Decoration", subPageName: "Круглый каменный горшок")]
    public partial class Pot06Object : WorldObject, IRepresentsItem
    {
        public virtual Type RepresentedItemType => typeof(Pot06Item);
        public override LocString DisplayName => Localizer.DoStr("Agave_Fake Белый");
        public override TableTextureMode TableTexture => TableTextureMode.Stone;

        partial void ModsPreInitialize();
        partial void ModsPostInitialize();
    }

    [Serialized]
    [LocDisplayName("Круглый каменный горшок")]
    [LocDescription("Небольшой круглый горшок с лаконичным дизайном, идеально подходит для украшения интерьеров и экстерьеров.")]
    [Ecopedia("Housing Objects", "Decoration", createAsSubPage: true)]
    [Tag("Housing")]
    [Weight(100)]
    [Tag(nameof(SurfaceTags.CanBeOnSurface))]
    public partial class Pot06Item : WorldObjectItem<Pot06Object>
    {
        protected override OccupancyContext GetOccupancyContext => new SideAttachedContext(0 | DirectionAxisFlags.Down, WorldObject.GetOccupancyInfo(this.WorldObjectType));
    }

    [RequiresSkill(typeof(MasonrySkill), 1)]
    [Ecopedia("Housing Objects", "Decoration", subPageName: "Круглый каменный горшок")]
    public partial class Pot06Recipe : RecipeFamily
    {
        public Pot06Recipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "Pot Sphérique en Pierre",
                displayName: Localizer.DoStr("Круглый каменный горшок"),

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
            this.Initialize(displayText: Localizer.DoStr("Круглый каменный горшок"), recipeType: typeof(Pot06Recipe));
            this.ModsPostInitialize();

            CraftingComponent.AddRecipe(tableType: typeof(MasonryTableObject), recipeFamily: this);
        }

        partial void ModsPreInitialize();
        partial void ModsPostInitialize();
    }
    #endregion



    #region Bac de jardin 2x3
    // ______________________________________________________ Цветочный ящик 2x3 ______________________________________________________ \\

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
    [Ecopedia("Housing Objects", "Decoration", subPageName: "Цветочный ящик 2x3")]
    public partial class Pot07Object : WorldObject, IRepresentsItem
    {
        public virtual Type RepresentedItemType => typeof(Pot07Item);
        public override LocString DisplayName => Localizer.DoStr("Цветочный ящик 2x3");
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
    [LocDisplayName("Цветочный ящик 2x3")]
    [LocDescription("Большой прочный бетонный ящик с растительностью, идеально подходит для украшения городских зон.")]
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
    [Ecopedia("Housing Objects", "Decoration", subPageName: "Цветочный ящик 2x3")]
    public partial class Pot07Recipe : RecipeFamily
    {
        public Pot07Recipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "Bac de jardin 2x3",
                displayName: Localizer.DoStr("Цветочный ящик 2x3"),

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
            this.Initialize(displayText: Localizer.DoStr("Цветочный ящик 2x3"), recipeType: typeof(Pot07Recipe));
            this.ModsPostInitialize();

            CraftingComponent.AddRecipe(tableType: typeof(MasonryTableObject), recipeFamily: this);
        }

        partial void ModsPreInitialize();
        partial void ModsPostInitialize();
    }
    #endregion



    #region Bac en bois de jardin
    // ______________________________________________________ Деревянный садовый ящик ______________________________________________________ \\

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
    [Ecopedia("Housing Objects", "Decoration", subPageName: "Деревянный садовый ящик")]
    public partial class Pot08Object : WorldObject, IRepresentsItem
    {
        public virtual Type RepresentedItemType => typeof(Pot08Item);
        public override LocString DisplayName => Localizer.DoStr("Деревянный садовый ящик");
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
    [LocDisplayName("Деревянный садовый ящик")]
    [LocDescription("Небольшой декоративный деревянный ящик с зелёными растениями.")]
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
    [Ecopedia("Housing Objects", "Decoration", subPageName: "Деревянный садовый ящик")]
    public partial class Pot08Recipe : RecipeFamily
    {
        public Pot08Recipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "Bac en bois de jardin",
                displayName: Localizer.DoStr("Деревянный садовый ящик"),

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
            this.Initialize(displayText: Localizer.DoStr("Деревянный садовый ящик"), recipeType: typeof(Pot08Recipe));
            this.ModsPostInitialize();

            CraftingComponent.AddRecipe(tableType: typeof(SawmillObject), recipeFamily: this);
        }

        partial void ModsPreInitialize();
        partial void ModsPostInitialize();
    }
    #endregion

}