// MOD créé par Plex : Modèle 3D et Code.
// Dernière mise à jour du mod : 20/07/2025

// Merci de ne pas retirer la section "Registered Mod" du code, car elle permet de recevoir une rémunération de la part de Strange Loop Games lors de son utilisation sur un serveur en ligne.

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
    [LocDescription("Caisse en bois, utilisée pour la fabrication de bacs à fleurs.")]
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
    [Ecopedia("Crafted Objects", "Box Tree", subPageName: "Palmier Box")]
    public partial class Box_Arbre_PalmObject : WorldObject, IRepresentsItem
    {
        public virtual Type RepresentedItemType => typeof(Box_Arbre_PalmItem);
        public override LocString DisplayName => Localizer.DoStr("Palmier Box");
        public override TableTextureMode TableTexture => TableTextureMode.Wood;

        partial void ModsPreInitialize();
        partial void ModsPostInitialize();
    }

    [Serialized]
    [LocDisplayName("Palmier Box")]
    [LocDescription("Besoin de vacances ? Ce palmier en caisse vous apporte l'ambiance tropicale, sans le sable entre les orteils !")]
    [Ecopedia("Crafted Objects", "Box Tree", createAsSubPage: true)]
    [Tag("Housing")]
    [Weight(5000)]
    [Tag(nameof(SurfaceTags.CanBeOnSurface))]
    public partial class Box_Arbre_PalmItem : WorldObjectItem<Box_Arbre_PalmObject>
    {
        protected override OccupancyContext GetOccupancyContext => new SideAttachedContext(0 | DirectionAxisFlags.Down, WorldObject.GetOccupancyInfo(this.WorldObjectType));

    }

    [RequiresSkill(typeof(FarmingSkill), 1)]
    [Ecopedia("Crafted Objects", "Box Tree", subPageName: "Palmier Box")]
    public partial class Box_Arbre_PalmRecipe : RecipeFamily
    {
        public Box_Arbre_PalmRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "Palmier Box",
                displayName: Localizer.DoStr("Palmier Box"),

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
            this.Initialize(displayText: Localizer.DoStr("Box Palmier"), recipeType: typeof(Box_Arbre_PalmRecipe));
            this.ModsPostInitialize();

            CraftingComponent.AddRecipe(tableType: typeof(FarmersTableObject), recipeFamily: this);
        }

        partial void ModsPreInitialize();

        partial void ModsPostInitialize();
    }
    #endregion



    #region Box Arbre Oak
    // ______________________________________________________ Box Arbre Oak ______________________________________________________ \\

    [Serialized]
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(HousingComponent))]
    [RequireComponent(typeof(OccupancyRequirementComponent))]
    [RequireComponent(typeof(ForSaleComponent))]
    [RequireComponent(typeof(PaintableComponent))]
    [Tag("Usable")]
    [Ecopedia("Crafted Objects", "Box Tree", subPageName: "Box Arbre Oak")]
    public partial class Box_Arbre_OakObject : WorldObject, IRepresentsItem
    {
        public virtual Type RepresentedItemType => typeof(Box_Arbre_OakItem);
        public override LocString DisplayName => Localizer.DoStr("Box_Arbre_Oak");
        public override TableTextureMode TableTexture => TableTextureMode.Wood;

        partial void ModsPreInitialize();
        partial void ModsPostInitialize();
    }

    [Serialized]
    [LocDisplayName("Box Arbre Oak")]
    [LocDescription("Avec ce chêne en boîte, la nature vient à vous, sans les glands... mais avec style !")]
    [Ecopedia("Crafted Objects", "Box Tree", createAsSubPage: true)]
    [Tag("Housing")]
    [Weight(100)]
    [Tag(nameof(SurfaceTags.CanBeOnSurface))]
    public partial class Box_Arbre_OakItem : WorldObjectItem<Box_Arbre_OakObject>
    {
        protected override OccupancyContext GetOccupancyContext => new SideAttachedContext(0 | DirectionAxisFlags.Down, WorldObject.GetOccupancyInfo(this.WorldObjectType));

    }

    [RequiresSkill(typeof(FarmingSkill), 2)]
    [Ecopedia("Crafted Objects", "Box Tree", subPageName: "Box Arbre Oak")]
    public partial class Box_Arbre_OakRecipe : Recipe
    {
        public Box_Arbre_OakRecipe()
        {
            this.Init(
                name: "Box Arbre Oak",
                displayName: Localizer.DoStr("Box Arbre Oak"),


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

            this.ModsPostInitialize();
            CraftingComponent.AddTagProduct(typeof(FarmersTableObject), typeof(Box_Arbre_PalmRecipe), this);
        }

        partial void ModsPostInitialize();
    }
    #endregion



    #region Box Arbre Redwood
    // ______________________________________________________ Box Arbre Redwood ______________________________________________________ \\

    [Serialized]
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(HousingComponent))]
    [RequireComponent(typeof(OccupancyRequirementComponent))]
    [RequireComponent(typeof(ForSaleComponent))]
    [RequireComponent(typeof(PaintableComponent))]
    [Tag("Usable")]
    [Ecopedia("Crafted Objects", "Box Tree", subPageName: "Box Arbre Redwood")]
    public partial class Box_Arbre_RedwoodObject : WorldObject, IRepresentsItem
    {
        public virtual Type RepresentedItemType => typeof(Box_Arbre_RedwoodItem);
        public override LocString DisplayName => Localizer.DoStr("Box_Arbre_Redwood");
        public override TableTextureMode TableTexture => TableTextureMode.Wood;

        partial void ModsPreInitialize();
        partial void ModsPostInitialize();
    }

    [Serialized]
    [LocDisplayName("Box Arbre Redwood")]
    [LocDescription("Ce redwood en pot rêve de s'enraciner... mais pour l'instant, il se contente d'un déménagement en caisse express !")]
    [Ecopedia("Crafted Objects", "Box Tree", createAsSubPage: true)]
    [Tag("Housing")]
    [Weight(100)]
    [Tag(nameof(SurfaceTags.CanBeOnSurface))]
    public partial class Box_Arbre_RedwoodItem : WorldObjectItem<Box_Arbre_RedwoodObject>
    {
        protected override OccupancyContext GetOccupancyContext => new SideAttachedContext(0 | DirectionAxisFlags.Down, WorldObject.GetOccupancyInfo(this.WorldObjectType));

    }

    [RequiresSkill(typeof(FarmingSkill), 2)]
    [Ecopedia("Crafted Objects", "Box Tree", subPageName: "Box Arbre Redwood")]
    public partial class Box_Arbre_RedwoodRecipe : Recipe
    {
        public Box_Arbre_RedwoodRecipe()
        {
            this.Init(
                name: "Box Arbre Redwood",
                displayName: Localizer.DoStr("Box Arbre Redwood"),


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

            this.ModsPostInitialize();
            CraftingComponent.AddTagProduct(typeof(FarmersTableObject), typeof(Box_Arbre_PalmRecipe), this);
        }

        partial void ModsPostInitialize();
    }
    #endregion



    #region Box Arbre Birch
    // ______________________________________________________ Box Arbre Birch ______________________________________________________ \\

    [Serialized]
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(HousingComponent))]
    [RequireComponent(typeof(OccupancyRequirementComponent))]
    [RequireComponent(typeof(ForSaleComponent))]
    [RequireComponent(typeof(PaintableComponent))]
    [Tag("Usable")]
    [Ecopedia("Crafted Objects", "Box Tree", subPageName: "Box Arbre Birch")]
    public partial class Box_Arbre_BirchObject : WorldObject, IRepresentsItem
    {
        public virtual Type RepresentedItemType => typeof(Box_Arbre_BirchItem);
        public override LocString DisplayName => Localizer.DoStr("Box_Arbre_Birch");
        public override TableTextureMode TableTexture => TableTextureMode.Wood;

        partial void ModsPreInitialize();
        partial void ModsPostInitialize();
    }

    [Serialized]
    [LocDisplayName("Box Arbre Birch")]
    [LocDescription("Le bouleau qui rêve d'être un bonsaï... mais avec une caisse XXL, parce que le style, ça compte aussi pour les arbres !")]
    [Ecopedia("Crafted Objects", "Box Tree", createAsSubPage: true)]
    [Tag("Housing")]
    [Weight(100)]
    [Tag(nameof(SurfaceTags.CanBeOnSurface))]
    public partial class Box_Arbre_BirchItem : WorldObjectItem<Box_Arbre_BirchObject>
    {
        protected override OccupancyContext GetOccupancyContext => new SideAttachedContext(0 | DirectionAxisFlags.Down, WorldObject.GetOccupancyInfo(this.WorldObjectType));

    }

    [RequiresSkill(typeof(FarmingSkill), 2)]
    [Ecopedia("Crafted Objects", "Box Tree", subPageName: "Box Arbre Birch")]
    public partial class Box_Arbre_BirchRecipe : Recipe
    {
        public Box_Arbre_BirchRecipe()
        {
            this.Init(
                name: "Box Arbre Birch",
                displayName: Localizer.DoStr("Box Arbre Birch"),


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

            this.ModsPostInitialize();
            CraftingComponent.AddTagProduct(typeof(FarmersTableObject), typeof(Box_Arbre_PalmRecipe), this);
        }

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
    [Ecopedia("Crafted Objects", "Box Tree", subPageName: "Box_Arbre_Spruce")]
    public partial class Box_Arbre_SpruceObject : WorldObject, IRepresentsItem
    {
        public virtual Type RepresentedItemType => typeof(Box_Arbre_SpruceItem);
        public override LocString DisplayName => Localizer.DoStr("Box_Arbre_Spruce");
        public override TableTextureMode TableTexture => TableTextureMode.Wood;

        partial void ModsPreInitialize();
        partial void ModsPostInitialize();
    }

    [Serialized]
    [LocDisplayName("Box_Arbre_Spruce")]
    [LocDescription("Parce qu’un sapin dans une caisse en bois, c’est la nature version portable. Qui a dit qu’on ne pouvait pas transporter une forêt ?")]
    [Ecopedia("Crafted Objects", "Box Tree", createAsSubPage: true)]
    [Tag("Housing")]
    [Weight(100)]
    [Tag(nameof(SurfaceTags.CanBeOnSurface))]
    public partial class Box_Arbre_SpruceItem : WorldObjectItem<Box_Arbre_SpruceObject>
    {
        protected override OccupancyContext GetOccupancyContext => new SideAttachedContext(0 | DirectionAxisFlags.Down, WorldObject.GetOccupancyInfo(this.WorldObjectType));

    }

    [RequiresSkill(typeof(FarmingSkill), 2)]
    [Ecopedia("Crafted Objects", "Box Tree", subPageName: "Box_Arbre_Spruce")]
    public partial class Box_Arbre_SpruceRecipe : Recipe
    {
        public Box_Arbre_SpruceRecipe()
        {
            this.Init(
                name: "Box_Arbre_Spruce",
                displayName: Localizer.DoStr("Box_Arbre_Spruce"),


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

            this.ModsPostInitialize();
            CraftingComponent.AddTagProduct(typeof(FarmersTableObject), typeof(Box_Arbre_PalmRecipe), this);
        }

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
    [Ecopedia("Crafted Objects", "Box Tree", subPageName: "Box_Arbre_Cactus")]
    public partial class Box_Arbre_CactusObject : WorldObject, IRepresentsItem
    {
        public virtual Type RepresentedItemType => typeof(Box_Arbre_CactusItem);
        public override LocString DisplayName => Localizer.DoStr("Box_Arbre_Cactus");
        public override TableTextureMode TableTexture => TableTextureMode.Wood;

        partial void ModsPreInitialize();
        partial void ModsPostInitialize();
    }

    [Serialized]
    [LocDisplayName("Box_Arbre_Cactus")]
    [LocDescription("Un cactus, bien planté dans une caisse, parce qu’un pot classique, c’était trop simple... et trop fragile !")]
    [Ecopedia("Crafted Objects", "Box Tree", createAsSubPage: true)]
    [Tag("Housing")]
    [Weight(100)]
    [Tag(nameof(SurfaceTags.CanBeOnSurface))]
    public partial class Box_Arbre_CactusItem : WorldObjectItem<Box_Arbre_CactusObject>
    {
        protected override OccupancyContext GetOccupancyContext => new SideAttachedContext(0 | DirectionAxisFlags.Down, WorldObject.GetOccupancyInfo(this.WorldObjectType));

    }

    [RequiresSkill(typeof(FarmingSkill), 2)]
    [Ecopedia("Crafted Objects", "Box Tree", subPageName: "Box_Arbre_Cactus")]
    public partial class Box_Arbre_CactusRecipe : Recipe
    {
        public Box_Arbre_CactusRecipe()
        {
            this.Init(
                name: "Box_Arbre_Cactus",
                displayName: Localizer.DoStr("Box_Arbre_Cactus"),


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

            this.ModsPostInitialize();
            CraftingComponent.AddTagProduct(typeof(FarmersTableObject), typeof(Box_Arbre_PalmRecipe), this);
        }

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
    [Ecopedia("Housing Objects", "Outdoor", subPageName: "Arbre Palm décoratif")]
    public partial class Fake_Arbre_PalmObject : WorldObject, IRepresentsItem
    {
        public virtual Type RepresentedItemType => typeof(Fake_Arbre_PalmItem);
        public override LocString DisplayName => Localizer.DoStr("Arbre Palm décoratif");
        public override TableTextureMode TableTexture => TableTextureMode.Wood;

        partial void ModsPreInitialize();
        partial void ModsPostInitialize();
    }

    [Serialized]
    [LocDisplayName("Arbre Palm décoratif")]
    [LocDescription("L'Arbre Palm décoratif ajoute une touche exotique et tropicale à votre environnement dans le jeu Eco, parfait pour embellir vos espaces extérieurs.")]
    [Ecopedia("Housing Objects", "Outdoor", createAsSubPage: true)]
    [Tag("Housing")]
    [Weight(5000)]
    [Tag(nameof(SurfaceTags.CanBeOnSurface))]
    public partial class Fake_Arbre_PalmItem : WorldObjectItem<Fake_Arbre_PalmObject>
    {
        protected override OccupancyContext GetOccupancyContext => new SideAttachedContext(0 | DirectionAxisFlags.Down, WorldObject.GetOccupancyInfo(this.WorldObjectType));

    }

    [RequiresSkill(typeof(FarmingSkill), 1)]
    [Ecopedia("Housing Objects", "Outdoor", subPageName: "Arbre Palm décoratif")]
    public partial class Fake_Arbre_PalmRecipe : RecipeFamily
    {
        public Fake_Arbre_PalmRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "Arbre Palm décoratif",
                displayName: Localizer.DoStr("Arbre Palm décoratif"),

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
            this.Initialize(displayText: Localizer.DoStr("Arbre Palm décoratif"), recipeType: typeof(Fake_Arbre_PalmRecipe));
            this.ModsPostInitialize();

            CraftingComponent.AddRecipe(tableType: typeof(FarmersTableObject), recipeFamily: this);
        }

        partial void ModsPreInitialize();

        partial void ModsPostInitialize();
    }
    #endregion



    #region Fake Arbre Oak
    // ______________________________________________________ Fake Arbre Oak ______________________________________________________ \\

    [Serialized]
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(HousingComponent))]
    [RequireComponent(typeof(OccupancyRequirementComponent))]
    [RequireComponent(typeof(ForSaleComponent))]
    [RequireComponent(typeof(PaintableComponent))]
    [Tag("Usable")]
    [Ecopedia("Plants", "Fake Tree", subPageName: "Fake Arbre Oak")]
    public partial class Fake_Arbre_OakObject : WorldObject, IRepresentsItem
    {
        public virtual Type RepresentedItemType => typeof(Fake_Arbre_OakItem);
        public override LocString DisplayName => Localizer.DoStr("Fake_Arbre_Oak");
        public override TableTextureMode TableTexture => TableTextureMode.Wood;

        partial void ModsPreInitialize();
        partial void ModsPostInitialize();
    }

    [Serialized]
    [LocDisplayName("Fake Arbre Oak")]
    [LocDescription("L'Arbre Chêne décoratif apporte une ambiance naturelle et élégante à vos paysages dans le jeu Eco, idéal pour créer des espaces verts réalistes.")]
    [Ecopedia("Plants", "Fake Tree", createAsSubPage: true)]
    [Tag("Housing")]
    [Weight(100)]
    [Tag(nameof(SurfaceTags.CanBeOnSurface))]
    public partial class Fake_Arbre_OakItem : WorldObjectItem<Fake_Arbre_OakObject>
    {
        protected override OccupancyContext GetOccupancyContext => new SideAttachedContext(0 | DirectionAxisFlags.Down, WorldObject.GetOccupancyInfo(this.WorldObjectType));

    }

    [RequiresSkill(typeof(FarmingSkill), 2)]
    [Ecopedia("Plants", "Fake Tree", subPageName: "Fake Arbre Oak")]
    public partial class Fake_Arbre_OakRecipe : Recipe
    {
        public Fake_Arbre_OakRecipe()
        {
            this.Init(
                name: "Fake Arbre Oak",
                displayName: Localizer.DoStr("Fake Arbre Oak"),


                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(AcornItem), 10, typeof(FarmingSkill), typeof(FarmingLavishResourcesTalent)),
                },


                items: new List<CraftingElement>
                {
                    new CraftingElement<Fake_Arbre_OakItem>()
                });

            this.ModsPostInitialize();
            CraftingComponent.AddTagProduct(typeof(FarmersTableObject), typeof(Fake_Arbre_PalmRecipe), this);
        }

        partial void ModsPostInitialize();
    }
    #endregion



    #region Fake Arbre Redwood
    // ______________________________________________________ Fake Arbre Redwood ______________________________________________________ \\

    [Serialized]
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(HousingComponent))]
    [RequireComponent(typeof(OccupancyRequirementComponent))]
    [RequireComponent(typeof(ForSaleComponent))]
    [RequireComponent(typeof(PaintableComponent))]
    [Tag("Usable")]
    [Ecopedia("Plants", "Fake Tree", subPageName: "Fake Arbre Redwood")]
    public partial class Fake_Arbre_RedwoodObject : WorldObject, IRepresentsItem
    {
        public virtual Type RepresentedItemType => typeof(Fake_Arbre_RedwoodItem);
        public override LocString DisplayName => Localizer.DoStr("Fake_Arbre_Redwood");
        public override TableTextureMode TableTexture => TableTextureMode.Wood;

        partial void ModsPreInitialize();
        partial void ModsPostInitialize();
    }

    [Serialized]
    [LocDisplayName("Fake Arbre Redwood")]
    [LocDescription("L'Arbre RedWood décoratif ajoute une touche majestueuse et imposante à vos décors dans le jeu Eco, offrant une beauté naturelle avec ses grandes proportions.")]
    [Ecopedia("Plants", "Fake Tree", createAsSubPage: true)]
    [Tag("Housing")]
    [Weight(100)]
    [Tag(nameof(SurfaceTags.CanBeOnSurface))]
    public partial class Fake_Arbre_RedwoodItem : WorldObjectItem<Fake_Arbre_RedwoodObject>
    {
        protected override OccupancyContext GetOccupancyContext => new SideAttachedContext(0 | DirectionAxisFlags.Down, WorldObject.GetOccupancyInfo(this.WorldObjectType));

    }

    [RequiresSkill(typeof(FarmingSkill), 2)]
    [Ecopedia("Plants", "Fake Tree", subPageName: "Fake Arbre Redwood")]
    public partial class Fake_Arbre_RedwoodRecipe : Recipe
    {
        public Fake_Arbre_RedwoodRecipe()
        {
            this.Init(
                name: "Fake Arbre Redwood",
                displayName: Localizer.DoStr("Fake Arbre Redwood"),


                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(RedwoodSeedItem), 10, typeof(FarmingSkill), typeof(FarmingLavishResourcesTalent)),
                },


                items: new List<CraftingElement>
                {
                    new CraftingElement<Fake_Arbre_RedwoodItem>()
                });

            this.ModsPostInitialize();
            CraftingComponent.AddTagProduct(typeof(FarmersTableObject), typeof(Fake_Arbre_PalmRecipe), this);
        }

        partial void ModsPostInitialize();
    }
    #endregion



    #region Fake Arbre Birch
    // ______________________________________________________ Fake Arbre Birch ______________________________________________________ \\

    [Serialized]
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(HousingComponent))]
    [RequireComponent(typeof(OccupancyRequirementComponent))]
    [RequireComponent(typeof(ForSaleComponent))]
    [RequireComponent(typeof(PaintableComponent))]
    [Tag("Usable")]
    [Ecopedia("Plants", "Fake Tree", subPageName: "Fake Arbre Birch")]
    public partial class Fake_Arbre_BirchObject : WorldObject, IRepresentsItem
    {
        public virtual Type RepresentedItemType => typeof(Fake_Arbre_BirchItem);
        public override LocString DisplayName => Localizer.DoStr("Fake_Arbre_Birch");
        public override TableTextureMode TableTexture => TableTextureMode.Wood;

        partial void ModsPreInitialize();
        partial void ModsPostInitialize();
    }

    [Serialized]
    [LocDisplayName("Fake Arbre Birch")]
    [LocDescription("L'Arbre Bouleau décoratif apporte une élégance simple et une atmosphère apaisante à vos paysages dans le jeu Eco, avec son écorce blanche caractéristique et ses feuilles délicates.")]
    [Ecopedia("Plants", "Fake Tree", createAsSubPage: true)]
    [Tag("Housing")]
    [Weight(100)]
    [Tag(nameof(SurfaceTags.CanBeOnSurface))]
    public partial class Fake_Arbre_BirchItem : WorldObjectItem<Fake_Arbre_BirchObject>
    {
        protected override OccupancyContext GetOccupancyContext => new SideAttachedContext(0 | DirectionAxisFlags.Down, WorldObject.GetOccupancyInfo(this.WorldObjectType));

    }

    [RequiresSkill(typeof(FarmingSkill), 2)]
    [Ecopedia("Plants", "Fake Tree", subPageName: "Fake Arbre Birch")]
    public partial class Fake_Arbre_BirchRecipe : Recipe
    {
        public Fake_Arbre_BirchRecipe()
        {
            this.Init(
                name: "Fake Arbre Birch",
                displayName: Localizer.DoStr("Fake Arbre Birch"),


                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(BirchSeedItem), 10, typeof(FarmingSkill), typeof(FarmingLavishResourcesTalent)),
                },


                items: new List<CraftingElement>
                {
                    new CraftingElement<Fake_Arbre_BirchItem>()
                });

            this.ModsPostInitialize();
            CraftingComponent.AddTagProduct(typeof(FarmersTableObject), typeof(Fake_Arbre_PalmRecipe), this);
        }

        partial void ModsPostInitialize();
    }
    #endregion



    #region Fake Arbre Cactus
    // ______________________________________________________ Fake Arbre Cactus ______________________________________________________ \\

    [Serialized]
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(HousingComponent))]
    [RequireComponent(typeof(OccupancyRequirementComponent))]
    [RequireComponent(typeof(ForSaleComponent))]
    [RequireComponent(typeof(PaintableComponent))]
    [Tag("Usable")]
    [Ecopedia("Plants", "Fake Tree", subPageName: "Fake Arbre Cactus")]
    public partial class Fake_Arbre_CactusObject : WorldObject, IRepresentsItem
    {
        public virtual Type RepresentedItemType => typeof(Fake_Arbre_CactusItem);
        public override LocString DisplayName => Localizer.DoStr("Fake_Arbre_Cactus");
        public override TableTextureMode TableTexture => TableTextureMode.Wood;

        partial void ModsPreInitialize();
        partial void ModsPostInitialize();
    }

    [Serialized]
    [LocDisplayName("Fake Arbre Cactus")]
    [LocDescription("L'Arbre Cactus décoratif ajoute une touche désertique et originale à vos décors dans le jeu Eco, parfait pour créer des ambiances arides et uniques.")]
    [Ecopedia("Plants", "Fake Tree", createAsSubPage: true)]
    [Tag("Housing")]
    [Weight(100)]
    [Tag(nameof(SurfaceTags.CanBeOnSurface))]
    public partial class Fake_Arbre_CactusItem : WorldObjectItem<Fake_Arbre_CactusObject>
    {
        protected override OccupancyContext GetOccupancyContext => new SideAttachedContext(0 | DirectionAxisFlags.Down, WorldObject.GetOccupancyInfo(this.WorldObjectType));

    }

    [RequiresSkill(typeof(FarmingSkill), 2)]
    [Ecopedia("Plants", "Fake Tree", subPageName: "Fake Arbre Cactus")]
    public partial class Fake_Arbre_CactusRecipe : Recipe
    {
        public Fake_Arbre_CactusRecipe()
        {
            this.Init(
                name: "Fake Arbre Cactus",
                displayName: Localizer.DoStr("Fake Arbre Cactus"),


                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(SaguaroSeedItem), 10, typeof(FarmingSkill), typeof(FarmingLavishResourcesTalent)),
                },


                items: new List<CraftingElement>
                {
                    new CraftingElement<Fake_Arbre_CactusItem>()
                });

            this.ModsPostInitialize();
            CraftingComponent.AddTagProduct(typeof(FarmersTableObject), typeof(Fake_Arbre_PalmRecipe), this);
        }

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
    [Ecopedia("Housing Objects", "Decoration", subPageName: "Volet de fenêtre gauche")]
    [Tag(nameof(SurfaceTags.HasTableSurface))]
    public partial class Volet_HardWood_GaucheObject : WorldObject, IRepresentsItem
    {
        public virtual Type RepresentedItemType => typeof(Volet_HardWood_GaucheItem);
        public override LocString DisplayName => Localizer.DoStr("Volet de fenêtre gauche");
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
    [LocDisplayName("Volet de fenêtre gauche")]
    [LocDescription("Un volet en bois robuste à accrocher aux fenêtres pour ajouter charme et authenticité aux maisons.")]
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
    [Ecopedia("Housing Objects", "Decoration", subPageName: "Volet de fenêtre gauche")]
    public partial class Volet_HardWood_GaucheRecipe : RecipeFamily
    {
        public Volet_HardWood_GaucheRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "Volet_HardWood_gauche",
                displayName: Localizer.DoStr("Volet de fenêtre gauche"),

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
            this.Initialize(displayText: Localizer.DoStr("Volet de fenêtre gauche"), recipeType: typeof(Volet_HardWood_GaucheRecipe));
            this.ModsPostInitialize();

            CraftingComponent.AddRecipe(tableType: typeof(CarpentryTableObject), recipeFamily: this);
        }

        partial void ModsPreInitialize();
        partial void ModsPostInitialize();
    }
#endregion



    #region Volet Droite
    // ______________________________________________________ Volet de fenêtre droite ______________________________________________________ \\

    [Serialized]
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(HousingComponent))]
    [RequireComponent(typeof(OccupancyRequirementComponent))]
    [RequireComponent(typeof(ForSaleComponent))]
    [RequireComponent(typeof(RoomRequirementsComponent))]
    [Tag("Usable")]
    [Ecopedia("Housing Objects", "Decoration", subPageName: "Volet de fenêtre droite")]
    [Tag(nameof(SurfaceTags.HasTableSurface))]
    public partial class Volet_HardWood_DroiteObject : WorldObject, IRepresentsItem
    {
        public virtual Type RepresentedItemType => typeof(Volet_HardWood_DroiteItem);
        public override LocString DisplayName => Localizer.DoStr("Volet de fenêtre droite");
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
    [LocDisplayName("Volet de fenêtre droite")]
    [LocDescription("Un volet en bois robuste à accrocher aux fenêtres pour ajouter charme et authenticité aux maisons.")]
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
    [Ecopedia("Housing Objects", "Decoration", subPageName: "Volet de fenêtre droite")]
    public partial class Volet_HardWood_DroiteRecipe : RecipeFamily
    {
        public Volet_HardWood_DroiteRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "Volet_HardWood_Droite",  
                displayName: Localizer.DoStr("Volet de fenêtre droite"),

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
            this.Initialize(displayText: Localizer.DoStr("Volet de fenêtre droite"), recipeType: typeof(Volet_HardWood_DroiteRecipe));
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
    [Ecopedia("Housing Objects", "Decoration", subPageName: "Fougère en pot")]
    public partial class Pot01Object : WorldObject, IRepresentsItem
    {
        public virtual Type RepresentedItemType => typeof(Pot01Item);
        public override LocString DisplayName => Localizer.DoStr("Fougère en pot");
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
    [LocDisplayName("Fougère en pot")]
    [LocDescription("Plante décorative pour embellir votre intérieur.")]
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
    [Ecopedia("Housing Objects", "Decoration", subPageName: "Fougère en pot")]
    public partial class Pot01Recipe : RecipeFamily
    {
        public Pot01Recipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "Fougère en pot",
                displayName: Localizer.DoStr("Fougère en pot"),

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
            this.Initialize(displayText: Localizer.DoStr("Fougère en pot"), recipeType: typeof(Pot01Recipe));
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
    [Ecopedia("Housing Objects", "Decoration", subPageName: "Suspension florale")]
    public partial class Pot02Object : WorldObject, IRepresentsItem
    {
        public virtual Type RepresentedItemType => typeof(Pot02Item);
        public override LocString DisplayName => Localizer.DoStr("Suspension florale");
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
    [LocDisplayName("Suspension florale")]
    [LocDescription("Plante suspendue décorative.")]
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
    [Ecopedia("Housing Objects", "Decoration", subPageName: "Suspension florale")]
    public partial class Pot02Recipe : RecipeFamily
    {
        public Pot02Recipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "Pot02",
                displayName: Localizer.DoStr("Suspension florale"),

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
            this.Initialize(displayText: Localizer.DoStr("Suspension florale"), recipeType: typeof(Pot02Recipe));
            this.ModsPostInitialize();

            CraftingComponent.AddRecipe(tableType: typeof(PotteryTableObject), recipeFamily: this);
        }

        partial void ModsPreInitialize();
        partial void ModsPostInitialize();
    }
    #endregion



    #region Bac de jardin 1x1
    // ______________________________________________________ Bac de jardin 1x1 ______________________________________________________ \\
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
    [Ecopedia("Housing Objects", "Decoration", subPageName: "Bac de jardin 1x1")]
    public partial class Pot03Object : WorldObject, IRepresentsItem
    {
        public virtual Type RepresentedItemType => typeof(Pot03Item);
        public override LocString DisplayName => Localizer.DoStr("Bac de jardin 1x1");
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
    [LocDisplayName("Bac de jardin 1x1")]
    [LocDescription("Petit bac décoratif avec plantes vertes.")]
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
    [Ecopedia("Housing Objects", "Decoration", subPageName: "Bac de jardin 1x1")]
    public partial class Pot03Recipe : RecipeFamily
    {
        public Pot03Recipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "Bac de jardin 1x1",
                displayName: Localizer.DoStr("Bac de jardin 1x1"),

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
            this.Initialize(displayText: Localizer.DoStr("Bac de jardin 1x1"), recipeType: typeof(Pot03Recipe));
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
        public override LocString DisplayName => Localizer.DoStr("Bac de jardin 2x2");
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
    [LocDisplayName("Bac de jardin 2x2")]
    [LocDescription("Un bac robuste en béton rempli de végétation, parfait pour embellir les zones urbaines.")]
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
    [Ecopedia("Housing Objects", "Decoration", subPageName: "Bac de jardin 2x2")]
    public partial class Pot04Recipe : RecipeFamily
    {
        public Pot04Recipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "Bac de jardin 2x2",
                displayName: Localizer.DoStr("Bac de jardin 2x2"),

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
            this.Initialize(displayText: Localizer.DoStr("Bac de jardin 2x2"), recipeType: typeof(Pot04Recipe));
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
    [Ecopedia("Housing Objects", "Decoration", subPageName: "Bac Rectangulaire Moderne")]
    public partial class Pot05Object : WorldObject, IRepresentsItem
    {
        public virtual Type RepresentedItemType => typeof(Pot05Item);
        public override LocString DisplayName => Localizer.DoStr("Bac Rectangulaire Moderne");
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
    [LocDisplayName("Bac Rectangulaire Moderne")]
    [LocDescription("Une jardinière élégante en pierre sombre, idéale pour ajouter une touche de verdure aux trottoirs et bâtiments.")]
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
    [Ecopedia("Housing Objects", "Decoration", subPageName: "Bac Rectangulaire Moderne")]
    public partial class Pot05Recipe : RecipeFamily
    {
        public Pot05Recipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "Bac Rectangulaire Moderne",
                displayName: Localizer.DoStr("Bac Rectangulaire Moderne"),

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
            this.Initialize(displayText: Localizer.DoStr("Bac Rectangulaire Moderne"), recipeType: typeof(Pot05Recipe));
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
    [Ecopedia("Housing Objects", "Decoration", subPageName: "Pot Sphérique en Pierre")]
    public partial class Pot06Object : WorldObject, IRepresentsItem
    {
        public virtual Type RepresentedItemType => typeof(Pot06Item);
        public override LocString DisplayName => Localizer.DoStr("Agave_Fake Blanc");
        public override TableTextureMode TableTexture => TableTextureMode.Stone;

        partial void ModsPreInitialize();
        partial void ModsPostInitialize();
    }

    [Serialized]
    [LocDisplayName("Pot Sphérique en Pierre")]
    [LocDescription("Un petit pot rond au design épuré, parfait pour décorer intérieurs comme extérieurs.")]
    [Ecopedia("Housing Objects", "Decoration", createAsSubPage: true)]
    [Tag("Housing")]
    [Weight(100)]
    [Tag(nameof(SurfaceTags.CanBeOnSurface))]
    public partial class Pot06Item : WorldObjectItem<Pot06Object>
    {
        protected override OccupancyContext GetOccupancyContext => new SideAttachedContext(0 | DirectionAxisFlags.Down, WorldObject.GetOccupancyInfo(this.WorldObjectType));

    }

    [RequiresSkill(typeof(MasonrySkill), 1)]
    [Ecopedia("Housing Objects", "Decoration", subPageName: "Pot Sphérique en Pierre")]
    public partial class Pot06Recipe : RecipeFamily
    {
        public Pot06Recipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "Pot Sphérique en Pierre",
                displayName: Localizer.DoStr("Pot Sphérique en Pierre"),

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
            this.Initialize(displayText: Localizer.DoStr("Pot Sphérique en Pierre"), recipeType: typeof(Pot06Recipe));
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
    [Ecopedia("Housing Objects", "Decoration", subPageName: "Bac de jardin 2x3")]
    public partial class Pot07Object : WorldObject, IRepresentsItem
    {
        public virtual Type RepresentedItemType => typeof(Pot07Item);
        public override LocString DisplayName => Localizer.DoStr("Bac de jardin 2x3");
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
    [LocDisplayName("Bac de jardin 2x3")]
    [LocDescription("Un grand bac robuste en béton rempli de végétation, parfait pour embellir les zones urbaines.")]
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
    [Ecopedia("Housing Objects", "Decoration", subPageName: "Bac de jardin 2x3")]
    public partial class Pot07Recipe : RecipeFamily
    {
        public Pot07Recipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "Bac de jardin 2x3",
                displayName: Localizer.DoStr("Bac de jardin 2x3"),

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
            this.Initialize(displayText: Localizer.DoStr("Bac de jardin 2x3"), recipeType: typeof(Pot07Recipe));
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
    [Ecopedia("Housing Objects", "Decoration", subPageName: "Bac en bois de jardin")]
    public partial class Pot08Object : WorldObject, IRepresentsItem
    {
        public virtual Type RepresentedItemType => typeof(Pot08Item);
        public override LocString DisplayName => Localizer.DoStr("Bac en bois de jardin");
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
    [LocDisplayName("Bac en bois de jardin")]
    [LocDescription("Petit bac en bois décoratif avec plantes vertes.")]
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
    [Ecopedia("Housing Objects", "Decoration", subPageName: "Bac en bois de jardin")]
    public partial class Pot08Recipe : RecipeFamily
    {
        public Pot08Recipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "Bac en bois de jardin",
                displayName: Localizer.DoStr("Bac en bois de jardin"),

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
            this.Initialize(displayText: Localizer.DoStr("Bac en bois de jardin"), recipeType: typeof(Pot08Recipe));
            this.ModsPostInitialize();

            CraftingComponent.AddRecipe(tableType: typeof(SawmillObject), recipeFamily: this);
        }

        partial void ModsPreInitialize();
        partial void ModsPostInitialize();
    }
    #endregion


}