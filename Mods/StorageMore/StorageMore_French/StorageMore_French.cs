// MOD créé par Plex : Modèle 3D et Code.
// Dernière mise à jour du mod : 23/10/25

// Certains paramètres de stockage peuvent être modifiés. Des annotations sont disponibles pour vous indiquer les modifications possibles.
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
    using Eco.Gameplay.Items.Recipes;
    using Eco.Core.Plugins.Interfaces;
    using Eco.Mods.Organisms;
    using Eco.Mods.TechTree;
    using System.Diagnostics;
    using System.Linq;

    #region ModRegistration
    public class StockageMod : IModInit
    {
        public static ModRegistration Register() => new()
        {
            ModName = "StockageMod",
            ModDescription = "Le mod de stockage ajoute 11 nouveaux éléments de stockage au jeu. Certains sont inédits et spécialement conçus pour des métiers spécifiques. En plus de leur fonctionnalité, ils apportent également une variété esthétique au jeu.",
            ModDisplayName = "Stockage Mod",
        };
    }
    #endregion



    #region Multiplicateur Inventory
    public class Inventoryx6Multiply : StackLimitRestriction
    {
        public Inventoryx6Multiply() : base(1, true) { }
        protected override int GetMaxItemsOverrider(Item item) => item.MaxStackSize * 6;
    }

    public class Inventoryx5Multiply : StackLimitRestriction
    {
        public Inventoryx5Multiply() : base(1, true) { }
        protected override int GetMaxItemsOverrider(Item item) => item.MaxStackSize * 5;
    }

    public class Inventoryx4Multiply : StackLimitRestriction
    {
        public Inventoryx4Multiply() : base(1, true) { }
        protected override int GetMaxItemsOverrider(Item item) => item.MaxStackSize * 4;
    }

    public class Inventoryx3Multiply : StackLimitRestriction
    {
        public Inventoryx3Multiply() : base(1, true) { }
        protected override int GetMaxItemsOverrider(Item item) => item.MaxStackSize * 3;
    }

    public class Inventoryx2Multiply : StackLimitRestriction
    {
        public Inventoryx2Multiply() : base(1, true) { }
        protected override int GetMaxItemsOverrider(Item item) => item.MaxStackSize * 2;
    }
    #endregion



    #region Big Bag
    // Big Bag 

    [Serialized]
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(LinkComponent))]
    [RequireComponent(typeof(OccupancyRequirementComponent))]
    [RequireComponent(typeof(ForSaleComponent))]
    [RequireComponent(typeof(ModularStockpileComponent))]
    [RequireComponent(typeof(PublicStorageComponent))]
    [Tag("Usable")]
    [Ecopedia("Crafted Objects", "Storage", subPageName: "Big Bag")]
    public partial class BigBagObject : WorldObject, IRepresentsItem
    {
        public virtual Type RepresentedItemType => typeof(BigBagItem);
        public override LocString DisplayName => Localizer.DoStr("Big Bag");
        public override TableTextureMode TableTexture => TableTextureMode.Wood;
        static BigBagObject()

        {
            var BlockOccupancyList = new List<BlockOccupancy>
            {


            new BlockOccupancy(new Vector3i(0, 0, 0)),
            new BlockOccupancy(new Vector3i(0, 0, 1)),
            new BlockOccupancy(new Vector3i(0, 1, 0)),
            new BlockOccupancy(new Vector3i(0, 1, 1)),
            new BlockOccupancy(new Vector3i(1, 0, 0)),
            new BlockOccupancy(new Vector3i(1, 0, 1)),
            new BlockOccupancy(new Vector3i(1, 1, 0)),
            new BlockOccupancy(new Vector3i(1, 1, 1)),
            };

            AddOccupancy<BigBagObject>(BlockOccupancyList);




        }




        protected override void Initialize()
        {
            this.ModsPreInitialize();
            this.GetComponent<StockpileComponent>().Initialize(new Vector3i(2, 1, 2));
            PublicStorageComponent component = base.GetComponent<PublicStorageComponent>(null);
            this.GetComponent<LinkComponent>().Initialize(12);
            component.Initialize(8);
            component.Storage.AddInvRestriction(new Inventoryx4Multiply()); // Multiplicateur de stacks
            component.Storage.AddInvRestriction(new TagRestriction(new string[] // Les tags autorisées à être utilisées dans le stockage.
            {
                "Silica",
                "Excavatable",
                "CrushedRock",
            }));
            this.ModsPostInitialize();
        }


        partial void ModsPreInitialize();

        partial void ModsPostInitialize();
    }

    [Serialized]
    [LocDisplayName("Big Bag")]
    [LocDescription("Le sac de rangement ultime pour vos pierres, terre, sable et autres débris. Assez grand pour stocker une montagne, mais assez humble pour rester au sol. Remplissez-le sans modération... sauf si vous tenez à votre dos !")]
    [Ecopedia("Crafted Objects", "Storage", createAsSubPage: true)]
    [Weight(1000)]
    [MaxStackSize(10)]
    public partial class BigBagItem : WorldObjectItem<BigBagObject>
    {
        protected override OccupancyContext GetOccupancyContext => new SideAttachedContext(0 | DirectionAxisFlags.Down, WorldObject.GetOccupancyInfo(this.WorldObjectType));

    }

    [RequiresSkill(typeof(TailoringSkill), 2)]
    public partial class BigBagRecipe : RecipeFamily
    {
        public BigBagRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "Big Bag",  
                displayName: Localizer.DoStr("Big Bag"),

                ingredients: new List<IngredientElement>
                {
                    new IngredientElement("Fabric", 20, typeof(TailoringSkill)), // Recette personnalisable : "20" tissu en lin.
                    new IngredientElement(typeof(LeatherHideItem), 10, typeof(TailoringSkill)), // Recette personnalisable : "10" Cuir.
                },

                items: new List<CraftingElement>
                {
                    new CraftingElement<BigBagItem>()
                });
            this.Recipes = new List<Recipe> { recipe };
            this.ExperienceOnCraft = 3; 

            this.LaborInCalories = CreateLaborInCaloriesValue(300, typeof(TailoringSkill)); // Quantité de calories consommées pour la fabrication.

            this.CraftMinutes = CreateCraftTimeValue(beneficiary: typeof(BigBagRecipe), start: 2, skillType: typeof(TailoringSkill)); // Durée de fabrication de l'objet : 2 minutes.

            this.ModsPreInitialize();
            this.Initialize(displayText: Localizer.DoStr("Big Bag"), recipeType: typeof(BigBagRecipe));
            this.ModsPostInitialize();

            CraftingComponent.AddRecipe(tableType: typeof(TailoringTableObject), recipeFamily: this);
        }

        partial void ModsPreInitialize();

        partial void ModsPostInitialize();
    }
#endregion



    #region Palette
    // PaletteStockage_________________________________________________________________


    [Serialized]
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(LinkComponent))]
    [RequireComponent(typeof(OccupancyRequirementComponent))]
    [RequireComponent(typeof(ForSaleComponent))]
    [RequireComponent(typeof(ModularStockpileComponent))]
    [RequireComponent(typeof(PublicStorageComponent))]
    [Tag("Usable")]
    [Ecopedia("Crafted Objects", "Storage", subPageName: "Palette")]
    public partial class PaletteObject : WorldObject, IRepresentsItem
    {
        public virtual Type RepresentedItemType => typeof(PaletteItem);
        public override LocString DisplayName => Localizer.DoStr("Palette");
        public override TableTextureMode TableTexture => TableTextureMode.Wood;
        static PaletteObject()

        {
            var BlockOccupancyList = new List<BlockOccupancy>
            {


            new BlockOccupancy(new Vector3i(0, 0, 0)),
            new BlockOccupancy(new Vector3i(0, 0, 1)),
            new BlockOccupancy(new Vector3i(0, 1, 0)),
            new BlockOccupancy(new Vector3i(0, 1, 1)),
            new BlockOccupancy(new Vector3i(0, 2, 0)),
            new BlockOccupancy(new Vector3i(0, 2, 1)),
            new BlockOccupancy(new Vector3i(0, 3, 0)),
            new BlockOccupancy(new Vector3i(0, 3, 1)),
            new BlockOccupancy(new Vector3i(0, 4, 0)),
            new BlockOccupancy(new Vector3i(0, 4, 1)),
            new BlockOccupancy(new Vector3i(1, 0, 0)),
            new BlockOccupancy(new Vector3i(1, 0, 1)),
            new BlockOccupancy(new Vector3i(1, 1, 0)),
            new BlockOccupancy(new Vector3i(1, 1, 1)),
            new BlockOccupancy(new Vector3i(1, 2, 0)),
            new BlockOccupancy(new Vector3i(1, 2, 1)),
            new BlockOccupancy(new Vector3i(1, 3, 0)),
            new BlockOccupancy(new Vector3i(1, 3, 1)),
            new BlockOccupancy(new Vector3i(1, 4, 0)),
            new BlockOccupancy(new Vector3i(1, 4, 1)),

            };

            AddOccupancy<PaletteObject>(BlockOccupancyList);




        }

        protected override void Initialize()
        {
            this.ModsPreInitialize();
            this.GetComponent<StockpileComponent>().Initialize(new Vector3i(2, 4, 2));
            PublicStorageComponent component = base.GetComponent<PublicStorageComponent>(null);
            this.GetComponent<LinkComponent>().Initialize(12);
            component.Initialize(8);
            component.Storage.AddInvRestriction(new Inventoryx4Multiply()); // Multiplicateur de stacks
            component.Storage.AddInvRestriction(new TagRestriction(new string[] // Les tags autorisées à être utilisées dans le stockage.
            {
                "Constructable",
                "Metal",
            }));
            this.ModsPostInitialize();
        }



        partial void ModsPreInitialize();

        partial void ModsPostInitialize();
    }

    [Serialized]
    [LocDisplayName("Palette")]
    [LocDescription("Parfaite pour empiler tout ce qui construit, sauf vos rêves ! Compacte, carrée, et prête à accueillir vos briques, planches et béton... sans se plaindre !")]
    [Ecopedia("Crafted Objects", "Storage", createAsSubPage: true)]
    [Weight(1000)]
    [MaxStackSize(10)]
    public partial class PaletteItem : WorldObjectItem<PaletteObject>
    {
        protected override OccupancyContext GetOccupancyContext => new SideAttachedContext(0 | DirectionAxisFlags.Down, WorldObject.GetOccupancyInfo(this.WorldObjectType));

    }


    [RequiresSkill(typeof(CarpentrySkill), 1)]
    public partial class PaletteRecipe : RecipeFamily
    {
        public PaletteRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "Palette",
                displayName: Localizer.DoStr("Palette"),

                ingredients: new List<IngredientElement>
                {
                    new IngredientElement("WoodBoard", 15, typeof(CarpentrySkill), typeof(CarpentryLavishResourcesTalent)), // Recette personnalisable : "15" Planches.
                },


                items: new List<CraftingElement>
                {
                    new CraftingElement<PaletteItem>()
                });
            this.Recipes = new List<Recipe> { recipe };
            this.ExperienceOnCraft = 2;

            this.LaborInCalories = CreateLaborInCaloriesValue(250, typeof(CarpentrySkill)); // Quantité de calories "250" consommées pour la fabrication.
            this.CraftMinutes = CreateCraftTimeValue(beneficiary: typeof(PaletteRecipe), start: 2, skillType: typeof(CarpentrySkill), typeof(CarpentryFocusedSpeedTalent), typeof(CarpentryParallelSpeedTalent)); // Durée de fabrication de l'objet : 2 minutes.

            this.ModsPreInitialize();
            this.Initialize(displayText: Localizer.DoStr("Palette"), recipeType: typeof(PaletteRecipe));
            this.ModsPostInitialize();

            CraftingComponent.AddRecipe(tableType: typeof(CarpentryTableObject), recipeFamily: this);
        }

        partial void ModsPreInitialize();

        partial void ModsPostInitialize();
    }
#endregion



    #region Support à Outils Mural
    // StockageOutils_________________________________________________________________________


    [Serialized]
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(LinkComponent))]
    [RequireComponent(typeof(OccupancyRequirementComponent))]
    [RequireComponent(typeof(ForSaleComponent))]
    [RequireComponent(typeof(ModularStockpileComponent))]
    [RequireComponent(typeof(PublicStorageComponent))]
    [Tag("Usable")]
    [Ecopedia("Crafted Objects", "Storage", subPageName: "Support à Outils Mural")]
    public partial class StockageOutilsObject : WorldObject, IRepresentsItem
    {
        public virtual Type RepresentedItemType => typeof(StockageOutilsItem);
        public override LocString DisplayName => Localizer.DoStr("Support à Outils Mural");
        public override TableTextureMode TableTexture => TableTextureMode.Wood;


            protected override void Initialize()
            {
            this.ModsPreInitialize();
            this.GetComponent<StockpileComponent>().Initialize(new Vector3i(6, 3, 2));
            PublicStorageComponent component = base.GetComponent<PublicStorageComponent>(null);
            this.GetComponent<LinkComponent>().Initialize(12);
            component.Initialize(18);
            component.Storage.AddInvRestriction(new Inventoryx3Multiply()); // Multiplicateur de stacks
            component.Storage.AddInvRestriction(new TagRestriction(new string[] // Les tags autorisées à être utilisées dans le stockage.
            {
                "Tool",
            }));
            this.ModsPostInitialize();
            }



        partial void ModsPreInitialize();

        partial void ModsPostInitialize();
    }

    [Serialized]
    [LocDisplayName("Support à Outils Mural")]
    [LocDescription("Gagnez de la place en rangeant vos outils avec style. Le désordre, c'est fini !")]
    [Ecopedia("Crafted Objects", "Storage", createAsSubPage: true)]
    [Weight(1000)]
    [MaxStackSize(10)]
    public partial class StockageOutilsItem : WorldObjectItem<StockageOutilsObject>
    {
        protected override OccupancyContext GetOccupancyContext => new SideAttachedContext(0 | DirectionAxisFlags.Backward, WorldObject.GetOccupancyInfo(this.WorldObjectType));

        [Serialized, SyncToView, NewTooltipChildren(CacheAs.Instance, flags: TTFlags.AllowNonControllerTypeForChildren)] public object PersistentData { get; set; }
    }


    [RequiresSkill(typeof(CarpentrySkill), 1)]
    public partial class StockageOutilsRecipe : RecipeFamily
    {
        public StockageOutilsRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "Support à Outils Mural",
                displayName: Localizer.DoStr("Support à Outils Mural"),

                ingredients: new List<IngredientElement>
                {
                    new IngredientElement("HewnLog", 5, typeof(CarpentrySkill), typeof(CarpentryLavishResourcesTalent)), // Recette personnalisable : "5" Hewnlog.
                    new IngredientElement(typeof(IronBarItem), 10, typeof(BlacksmithSkill)), // Recette personnalisable : "10" IronBar.
                },


                items: new List<CraftingElement>
                {
                    new CraftingElement<StockageOutilsItem>()
                });
            this.Recipes = new List<Recipe> { recipe };
            this.ExperienceOnCraft = 2;

            this.LaborInCalories = CreateLaborInCaloriesValue(250, typeof(CarpentrySkill)); // Quantité de calories "250" consommées pour la fabrication.

            this.CraftMinutes = CreateCraftTimeValue(beneficiary: typeof(PaletteRecipe), start: 2, skillType: typeof(CarpentrySkill), typeof(CarpentryFocusedSpeedTalent), typeof(CarpentryParallelSpeedTalent)); // Durée de fabrication de l'objet : 2 minutes.

            this.ModsPreInitialize();
            this.Initialize(displayText: Localizer.DoStr("Support à Outils Mural"), recipeType: typeof(StockageOutilsRecipe));
            this.ModsPostInitialize();

            CraftingComponent.AddRecipe(tableType: typeof(CarpentryTableObject), recipeFamily: this);
        }

        partial void ModsPreInitialize();

        partial void ModsPostInitialize();
    }
#endregion



    #region Petite Etagère Simple
    // Petite Etagère Simple


    [Serialized]
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(LinkComponent))]
    [RequireComponent(typeof(OccupancyRequirementComponent))]
    [RequireComponent(typeof(ForSaleComponent))]
    [RequireComponent(typeof(PublicStorageComponent))]
    [Tag("Usable")]
    [Ecopedia("Crafted Objects", "Storage", subPageName: "Petite Etagère Simple")]
    public partial class Petite_Simple_EtagereObject : WorldObject, IRepresentsItem
    {
        public virtual Type RepresentedItemType => typeof(Petite_Simple_EtagereItem);
        public override LocString DisplayName => Localizer.DoStr("Petite Etagère Simple");
        public override TableTextureMode TableTexture => TableTextureMode.Wood;
        static Petite_Simple_EtagereObject()

        {
            var BlockOccupancyList = new List<BlockOccupancy>
            {


            new BlockOccupancy(new Vector3i(0, 0, 0)),
            new BlockOccupancy(new Vector3i(0, 1, 0)),
            };

            AddOccupancy<Petite_Simple_EtagereObject>(BlockOccupancyList);




        }


        protected override void Initialize()
        {
        this.ModsPreInitialize();
        PublicStorageComponent component = base.GetComponent<PublicStorageComponent>(null);
        this.GetComponent<LinkComponent>().Initialize(12);
        component.Initialize(8); // Le nombre d'emplacement "8" autorisé dans le stockage.
        component.Storage.AddInvRestriction(new Inventoryx2Multiply()); // Multiplicateur de stacks
        component.Storage.AddInvRestriction(new NotCarriedRestriction());
        this.ModsPostInitialize();
        }


        partial void ModsPreInitialize();

        partial void ModsPostInitialize();
    }

    [Serialized]
    [LocDisplayName("Petite Etagère Simple")]
    [LocDescription("Besoin d’un rangement sans prise de tête ? La Petite Étagère Simple est là pour empiler vos fournitures.")]
    [Ecopedia("Crafted Objects", "Storage", createAsSubPage: true)]
    [Weight(1000)]
    [MaxStackSize(10)]
    public partial class Petite_Simple_EtagereItem : WorldObjectItem<Petite_Simple_EtagereObject>
    {
        protected override OccupancyContext GetOccupancyContext => new SideAttachedContext(0 | DirectionAxisFlags.Down, WorldObject.GetOccupancyInfo(this.WorldObjectType));

    }

    [RequiresSkill(typeof(MechanicsSkill), 1)]
    public partial class Petite_Simple_EtagereRecipe : RecipeFamily
    {
        public Petite_Simple_EtagereRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "Petite Etagère Simple",  //noloc
                displayName: Localizer.DoStr("Petite Etagère Simple"),

                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(NailItem), 5, typeof(MechanicsSkill), typeof(MechanicsLavishResourcesTalent)), // Recette personnalisable : "5" Clous.
                    new IngredientElement(typeof(IronBarItem), 5, typeof(MechanicsSkill), typeof(MechanicsLavishResourcesTalent)), // Recette personnalisable : "5" Lingot de fer.
                    new IngredientElement("WoodBoard", 10, typeof(MechanicsSkill), typeof(MechanicsLavishResourcesTalent)), // Recette personnalisable : "10" Planches en bois.
                },

                items: new List<CraftingElement>
                {
                    new CraftingElement<Petite_Simple_EtagereItem>()
                });
            this.Recipes = new List<Recipe> { recipe };
            this.ExperienceOnCraft = 25;

            this.LaborInCalories = CreateLaborInCaloriesValue(250, typeof(MechanicsSkill)); // Quantité de calories "250" consommées pour la fabrication.

            this.CraftMinutes = CreateCraftTimeValue(beneficiary: typeof(Petite_Simple_EtagereRecipe), start: 3, skillType: typeof(MechanicsSkill), typeof(MechanicsFocusedSpeedTalent), typeof(MechanicsParallelSpeedTalent)); // Durée de fabrication de l'objet : 3 minutes.

            this.ModsPreInitialize();
            this.Initialize(displayText: Localizer.DoStr("Petite Etagère Simple"), recipeType: typeof(Petite_Simple_EtagereRecipe));
            this.ModsPostInitialize();

            CraftingComponent.AddRecipe(tableType: typeof(MachinistTableObject), recipeFamily: this);
        }

        partial void ModsPreInitialize();

        partial void ModsPostInitialize();
    }
#endregion



    #region Petite Etagère Double
    // ________________________________  Petite Etagère Double _______________________________________________


    [Serialized]
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(LinkComponent))]
    [RequireComponent(typeof(OccupancyRequirementComponent))]
    [RequireComponent(typeof(ForSaleComponent))]
    [RequireComponent(typeof(PublicStorageComponent))]
    [Tag("Usable")]
    [Ecopedia("Crafted Objects", "Storage", subPageName: "Petite Etagère Double")]
    public partial class Petite_Double_EtagereObject : WorldObject, IRepresentsItem
    {
        public virtual Type RepresentedItemType => typeof(Petite_Double_EtagereItem);
        public override LocString DisplayName => Localizer.DoStr("Petite Etagère Double");
        public override TableTextureMode TableTexture => TableTextureMode.Wood;
        static Petite_Double_EtagereObject()

        {
            var BlockOccupancyList = new List<BlockOccupancy>
            {


            new BlockOccupancy(new Vector3i(0, 0, 0)),
            new BlockOccupancy(new Vector3i(0, 1, 0)),
            new BlockOccupancy(new Vector3i(1, 0, 0)),
            new BlockOccupancy(new Vector3i(1, 1, 0)),
            };

            AddOccupancy<Petite_Double_EtagereObject>(BlockOccupancyList);




        }



        protected override void Initialize()
        {
        this.ModsPreInitialize();
        PublicStorageComponent component = base.GetComponent<PublicStorageComponent>(null);
        this.GetComponent<LinkComponent>().Initialize(12);
        component.Initialize(16); // Le nombre d'emplacement "16" autorisé dans le stockage.
        component.Storage.AddInvRestriction(new Inventoryx2Multiply()); // Multiplicateur de stacks
        component.Storage.AddInvRestriction(new NotCarriedRestriction());
        this.ModsPostInitialize();
        }



        partial void ModsPreInitialize();

        partial void ModsPostInitialize();
    }

    [Serialized]
    [LocDisplayName("Petite Etagère Double")]
    [LocDescription("Deux fois plus d’étagères, deux fois plus de bazar bien rangé ! La Petite Étagère Double : parce que parfois, une seule ne suffit pas pour caser tout ce bric-à-brac.")]
    [Ecopedia("Crafted Objects", "Storage", createAsSubPage: true)]
    [Weight(1000)]
    [MaxStackSize(10)]
    public partial class Petite_Double_EtagereItem : WorldObjectItem<Petite_Double_EtagereObject>
    {
        protected override OccupancyContext GetOccupancyContext => new SideAttachedContext(0 | DirectionAxisFlags.Down, WorldObject.GetOccupancyInfo(this.WorldObjectType));

    }

    [RequiresSkill(typeof(MechanicsSkill), 2)]
    public partial class Petite_Double_EtagereRecipe : RecipeFamily
    {
        public Petite_Double_EtagereRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "Petite Etagère Double",  //noloc
                displayName: Localizer.DoStr("Petite Etagère Double"),

                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(NailItem), 10, typeof(MechanicsSkill), typeof(MechanicsLavishResourcesTalent)), // Recette personnalisable : "10" Clous.
                    new IngredientElement(typeof(IronBarItem), 10, typeof(MechanicsSkill), typeof(MechanicsLavishResourcesTalent)), // Recette personnalisable : "10" Lingot de fer.
                    new IngredientElement("WoodBoard", 20, typeof(MechanicsSkill), typeof(MechanicsLavishResourcesTalent)), // Recette personnalisable : "20" Planches en bois.
                },

                items: new List<CraftingElement>
                {
                    new CraftingElement<Petite_Double_EtagereItem>()
                });
            this.Recipes = new List<Recipe> { recipe };
            this.ExperienceOnCraft = 25;

            this.LaborInCalories = CreateLaborInCaloriesValue(350, typeof(MechanicsSkill)); // Quantité de calories "350" consommées pour la fabrication.

            this.CraftMinutes = CreateCraftTimeValue(beneficiary: typeof(Petite_Double_EtagereRecipe), start: 4, skillType: typeof(MechanicsSkill), typeof(MechanicsFocusedSpeedTalent), typeof(MechanicsParallelSpeedTalent)); // Durée de fabrication de l'objet : 4 minutes.

            this.ModsPreInitialize();
            this.Initialize(displayText: Localizer.DoStr("Petite Etagère Double"), recipeType: typeof(Petite_Double_EtagereRecipe));
            this.ModsPostInitialize();

            CraftingComponent.AddRecipe(tableType: typeof(MachinistTableObject), recipeFamily: this);
        }

        partial void ModsPreInitialize();

        partial void ModsPostInitialize();
    }
#endregion



    #region Grande Etagère Simple
    // ________________________________  Grande Etagère Simple _______________________________________________


    [Serialized]
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(LinkComponent))]
    [RequireComponent(typeof(OccupancyRequirementComponent))]
    [RequireComponent(typeof(ForSaleComponent))]
    [RequireComponent(typeof(PublicStorageComponent))]
    [Tag("Usable")]
    [Ecopedia("Crafted Objects", "Storage", subPageName: "Grande Etagère Simple")]
    public partial class Grande_Simple_EtagereObject : WorldObject, IRepresentsItem
    {
        public virtual Type RepresentedItemType => typeof(Grande_Simple_EtagereItem);
        public override LocString DisplayName => Localizer.DoStr("Grande Etagère Simple");
        public override TableTextureMode TableTexture => TableTextureMode.Wood;
        static Grande_Simple_EtagereObject()

        {
            var BlockOccupancyList = new List<BlockOccupancy>
            {


            new BlockOccupancy(new Vector3i(0, 0, 0)),
            new BlockOccupancy(new Vector3i(0, 0, 1)),
            new BlockOccupancy(new Vector3i(0, 1, 0)),
            new BlockOccupancy(new Vector3i(0, 1, 1)),
            new BlockOccupancy(new Vector3i(0, 2, 0)),
            new BlockOccupancy(new Vector3i(0, 2, 1)),
            new BlockOccupancy(new Vector3i(0, 3, 0)),
            new BlockOccupancy(new Vector3i(0, 3, 1)),
            new BlockOccupancy(new Vector3i(1, 0, 0)),
            new BlockOccupancy(new Vector3i(1, 0, 1)),
            new BlockOccupancy(new Vector3i(1, 1, 0)),
            new BlockOccupancy(new Vector3i(1, 1, 1)),
            new BlockOccupancy(new Vector3i(1, 2, 0)),
            new BlockOccupancy(new Vector3i(1, 2, 1)),
            new BlockOccupancy(new Vector3i(1, 3, 0)),
            new BlockOccupancy(new Vector3i(1, 3, 1)),
            };

            AddOccupancy<Grande_Simple_EtagereObject>(BlockOccupancyList);




        }




        protected override void Initialize()
        {
        this.ModsPreInitialize();
        PublicStorageComponent component = base.GetComponent<PublicStorageComponent>(null);
        this.GetComponent<LinkComponent>().Initialize(12);
        component.Initialize(32); // Le nombre d'emplacement "32" autorisé dans le stockage.
        component.Storage.AddInvRestriction(new Inventoryx2Multiply()); // Multiplicateur de stacks
        component.Storage.AddInvRestriction(new NotCarriedRestriction());
        this.ModsPostInitialize();
        }


        partial void ModsPreInitialize();

        partial void ModsPostInitialize();
    }

    [Serialized]
    [LocDisplayName("Grande Etagère Simple")]
    [LocDescription("Plus grande, plus robuste, mais toujours simple ! La Grande Étagère Simple est là pour gérer les grosses affaires. Parce que même les objets encombrants ont besoin d'un petit coin bien rangé.")]
    [Ecopedia("Crafted Objects", "Storage", createAsSubPage: true)]
    [Weight(1000)]
    [MaxStackSize(10)]
    public partial class Grande_Simple_EtagereItem : WorldObjectItem<Grande_Simple_EtagereObject>
    {
        protected override OccupancyContext GetOccupancyContext => new SideAttachedContext(0 | DirectionAxisFlags.Down, WorldObject.GetOccupancyInfo(this.WorldObjectType));

    }

    [RequiresSkill(typeof(MechanicsSkill), 3)]
    public partial class Grande_Simple_EtagereRecipe : RecipeFamily
    {
        public Grande_Simple_EtagereRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "Grande Etagère Simple",  //noloc
                displayName: Localizer.DoStr("Grande Etagère Simple"),

                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(NailItem), 30, typeof(MechanicsSkill), typeof(MechanicsLavishResourcesTalent)), // Recette personnalisable : "30" Clous.
                    new IngredientElement(typeof(IronBarItem), 30, typeof(MechanicsSkill), typeof(MechanicsLavishResourcesTalent)), // Recette personnalisable : "30" Lingot de fer.
                    new IngredientElement("WoodBoard", 50, typeof(MechanicsSkill), typeof(MechanicsLavishResourcesTalent)), // Recette personnalisable : "50" Planches en bois.
                },

                items: new List<CraftingElement>
                {
                    new CraftingElement<Grande_Simple_EtagereItem>()
                });
            this.Recipes = new List<Recipe> { recipe };
            this.ExperienceOnCraft = 25;

            this.LaborInCalories = CreateLaborInCaloriesValue(500, typeof(MechanicsSkill)); // Quantité de calories "500" consommées pour la fabrication.

            this.CraftMinutes = CreateCraftTimeValue(beneficiary: typeof(Grande_Simple_EtagereRecipe), start: 6, skillType: typeof(MechanicsSkill), typeof(MechanicsFocusedSpeedTalent), typeof(MechanicsParallelSpeedTalent)); // Durée de fabrication de l'objet : 6 minutes.

            this.ModsPreInitialize();
            this.Initialize(displayText: Localizer.DoStr("Grande Etagère Simple"), recipeType: typeof(Grande_Simple_EtagereRecipe));
            this.ModsPostInitialize();

            CraftingComponent.AddRecipe(tableType: typeof(MachinistTableObject), recipeFamily: this);
        }

        partial void ModsPreInitialize();

        partial void ModsPostInitialize();
    }
#endregion



    #region Grande Etagère Double
    // ________________________________  Grande Etagère Double _______________________________________________


    [Serialized]
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(LinkComponent))]
    [RequireComponent(typeof(OccupancyRequirementComponent))]
    [RequireComponent(typeof(ForSaleComponent))]
    [RequireComponent(typeof(PublicStorageComponent))]
    [Tag("Usable")]
    [Ecopedia("Crafted Objects", "Storage", subPageName: "Grande Etagère Double")]
    public partial class Grande_Double_EtagereObject : WorldObject, IRepresentsItem
    {
        public virtual Type RepresentedItemType => typeof(Grande_Double_EtagereItem);
        public override LocString DisplayName => Localizer.DoStr("Grande Etagère Double");
        public override TableTextureMode TableTexture => TableTextureMode.Wood;
        static Grande_Double_EtagereObject()

        {
            var BlockOccupancyList = new List<BlockOccupancy>
            {


            new BlockOccupancy(new Vector3i(0, 0, 0)),
            new BlockOccupancy(new Vector3i(0, 0, 1)),
            new BlockOccupancy(new Vector3i(0, 1, 0)),
            new BlockOccupancy(new Vector3i(0, 1, 1)),
            new BlockOccupancy(new Vector3i(0, 2, 0)),
            new BlockOccupancy(new Vector3i(0, 2, 1)),
            new BlockOccupancy(new Vector3i(0, 3, 0)),
            new BlockOccupancy(new Vector3i(0, 3, 1)),
            new BlockOccupancy(new Vector3i(1, 0, 0)),
            new BlockOccupancy(new Vector3i(1, 0, 1)),
            new BlockOccupancy(new Vector3i(1, 1, 0)),
            new BlockOccupancy(new Vector3i(1, 1, 1)),
            new BlockOccupancy(new Vector3i(1, 2, 0)),
            new BlockOccupancy(new Vector3i(1, 2, 1)),
            new BlockOccupancy(new Vector3i(1, 3, 0)),
            new BlockOccupancy(new Vector3i(1, 3, 1)),
            new BlockOccupancy(new Vector3i(2, 0, 0)),
            new BlockOccupancy(new Vector3i(2, 0, 1)),
            new BlockOccupancy(new Vector3i(2, 1, 0)),
            new BlockOccupancy(new Vector3i(2, 1, 1)),
            new BlockOccupancy(new Vector3i(2, 2, 0)),
            new BlockOccupancy(new Vector3i(2, 2, 1)),
            new BlockOccupancy(new Vector3i(2, 3, 0)),
            new BlockOccupancy(new Vector3i(2, 3, 1)),
            new BlockOccupancy(new Vector3i(3, 0, 0)),
            new BlockOccupancy(new Vector3i(3, 0, 1)),
            new BlockOccupancy(new Vector3i(3, 1, 0)),
            new BlockOccupancy(new Vector3i(3, 1, 1)),
            new BlockOccupancy(new Vector3i(3, 2, 0)),
            new BlockOccupancy(new Vector3i(3, 2, 1)),
            new BlockOccupancy(new Vector3i(3, 3, 0)),
            new BlockOccupancy(new Vector3i(3, 3, 1)),
            };

            AddOccupancy<Grande_Double_EtagereObject>(BlockOccupancyList);




        }



        protected override void Initialize()
        {
        this.ModsPreInitialize();
        PublicStorageComponent component = base.GetComponent<PublicStorageComponent>(null);
        this.GetComponent<LinkComponent>().Initialize(12);
        component.Initialize(64); // Le nombre d'emplacement "64" autorisé dans le stockage.
        component.Storage.AddInvRestriction(new Inventoryx2Multiply()); // Multiplicateur de stacks
        component.Storage.AddInvRestriction(new NotCarriedRestriction());
        this.ModsPostInitialize();
        }


        partial void ModsPreInitialize();

        partial void ModsPostInitialize();
    }

    [Serialized]
    [LocDisplayName("Grande Etagère Double")]
    [LocDescription("Quand une seule grande étagère ne suffit pas, optez pour la Grande Étagère Double ! Deux fois plus de place pour empiler joyeusement votre inventaire... ou votre chaos bien organisé.")]
    [Ecopedia("Crafted Objects", "Storage", createAsSubPage: true)]
    [Weight(1000)]
    [MaxStackSize(10)]
    public partial class Grande_Double_EtagereItem : WorldObjectItem<Grande_Double_EtagereObject>
    {
        protected override OccupancyContext GetOccupancyContext => new SideAttachedContext(0 | DirectionAxisFlags.Down, WorldObject.GetOccupancyInfo(this.WorldObjectType));

    }

    [RequiresSkill(typeof(MechanicsSkill), 4)]
    public partial class Grande_Double_EtagereRecipe : RecipeFamily
    {
        public Grande_Double_EtagereRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "Grande Etagère Double",  //noloc
                displayName: Localizer.DoStr("Grande Etagère Double"),

                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(NailItem), 60, typeof(MechanicsSkill), typeof(MechanicsLavishResourcesTalent)), // Recette personnalisable : "60" Clous.
                    new IngredientElement(typeof(IronBarItem), 60, typeof(MechanicsSkill), typeof(MechanicsLavishResourcesTalent)), // Recette personnalisable : "60" Lingot de fer.
                    new IngredientElement("WoodBoard", 100, typeof(MechanicsSkill), typeof(MechanicsLavishResourcesTalent)), // Recette personnalisable : "100" Planches en bois.
                },

                items: new List<CraftingElement>
                {
                    new CraftingElement<Grande_Double_EtagereItem>()
                });
            this.Recipes = new List<Recipe> { recipe };
            this.ExperienceOnCraft = 25;

            this.LaborInCalories = CreateLaborInCaloriesValue(800, typeof(MechanicsSkill)); // Quantité de calories "800" consommées pour la fabrication.

            this.CraftMinutes = CreateCraftTimeValue(beneficiary: typeof(Grande_Double_EtagereRecipe), start: 10, skillType: typeof(MechanicsSkill), typeof(MechanicsFocusedSpeedTalent), typeof(MechanicsParallelSpeedTalent)); // Durée de fabrication de l'objet : 10 minutes.

            this.ModsPreInitialize();
            this.Initialize(displayText: Localizer.DoStr("Grande Etagère Double"), recipeType: typeof(Grande_Double_EtagereRecipe));
            this.ModsPostInitialize();

            CraftingComponent.AddRecipe(tableType: typeof(MachinistTableObject), recipeFamily: this);
        }

        partial void ModsPreInitialize();

        partial void ModsPostInitialize();
    }
#endregion



    #region Container Bleu 5m
    // Shipping_01

    [Serialized]
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(LinkComponent))]
    [RequireComponent(typeof(CustomTextComponent))]
    [RequireComponent(typeof(PublicStorageComponent))]
    [RequireComponent(typeof(ForSaleComponent))]
    [Tag("Usable")]
    [Ecopedia("Crafted Objects", "Storage", subPageName: "Container Bleu 5m")]
    public partial class Shipping_01Object : WorldObject, IRepresentsItem
    {
        public virtual Type RepresentedItemType => typeof(Shipping_01Item);
        public override LocString DisplayName => Localizer.DoStr("Container Bleu 5m");
        public override TableTextureMode TableTexture => TableTextureMode.Metal;


        static Shipping_01Object()

        {
            var BlockOccupancyList = new List<BlockOccupancy>
            {
            new BlockOccupancy(new Vector3i(0, 0, 0)),
            new BlockOccupancy(new Vector3i(0, 0, 1)),
            new BlockOccupancy(new Vector3i(0, 0, 2)),
            new BlockOccupancy(new Vector3i(0, 0, 3)),
            new BlockOccupancy(new Vector3i(0, 0, 4)),
            new BlockOccupancy(new Vector3i(0, 1, 0)),
            new BlockOccupancy(new Vector3i(0, 1, 1)),
            new BlockOccupancy(new Vector3i(0, 1, 2)),
            new BlockOccupancy(new Vector3i(0, 1, 3)),
            new BlockOccupancy(new Vector3i(0, 1, 4)),
            new BlockOccupancy(new Vector3i(1, 0, 0)),
            new BlockOccupancy(new Vector3i(1, 0, 1)),
            new BlockOccupancy(new Vector3i(1, 0, 2)),
            new BlockOccupancy(new Vector3i(1, 0, 3)),
            new BlockOccupancy(new Vector3i(1, 0, 4)),
            new BlockOccupancy(new Vector3i(1, 1, 0)),
            new BlockOccupancy(new Vector3i(1, 1, 1)),
            new BlockOccupancy(new Vector3i(1, 1, 2)),
            new BlockOccupancy(new Vector3i(1, 1, 3)),
            new BlockOccupancy(new Vector3i(1, 1, 4)),
            };

            AddOccupancy<Shipping_01Object>(BlockOccupancyList);




        }



        protected override void Initialize()
        {
        this.ModsPreInitialize();
        this.GetComponent<CustomTextComponent>().Initialize(700);
        this.GetComponent<LinkComponent>().Initialize(12); // Maximum connection distance with nearby storage units: 12 meters.
        PublicStorageComponent component = base.GetComponent<PublicStorageComponent>(null);
        component.Initialize(32); // Le nombre d'emplacement "32" autorisé dans le stockage.
        component.Storage.AddInvRestriction(new Inventoryx3Multiply()); // Multiplicateur de stacks

        this.ModsPostInitialize();
        }


        partial void ModsPreInitialize();

        partial void ModsPostInitialize();
    }

    [Serialized]
    [LocDisplayName("Container Bleu 5m")]
    [LocDescription("Assez grand pour ranger tous vos objets... ou simplement tout le bazar que vous ne savez plus où mettre ! Résistant, spacieux et de couleur bleue pour ne jamais le perdre de vue.")]
    [Ecopedia("Crafted Objects", "Storage", createAsSubPage: true)]
    [Weight(20000)]
    [MaxStackSize(10)]
    public partial class Shipping_01Item : WorldObjectItem<Shipping_01Object>
    {

    }

    [Ecopedia("Crafted Objects", "Storage", subPageName: "Container Bleu 5m")]
    [RequiresSkill(typeof(MechanicsSkill), 5)]
    public partial class Shipping_01Recipe : RecipeFamily
    {
        public Shipping_01Recipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "Container Bleu 5m",  //noloc
                displayName: Localizer.DoStr("Container Bleu 5m"),

                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(IronPlateItem), 100, typeof(MechanicsSkill), typeof(MechanicsLavishResourcesTalent)), // Customizable recipe: "100" Iron Plates.
                    new IngredientElement(typeof(ScrewsItem), 50, typeof(MechanicsSkill), typeof(MechanicsLavishResourcesTalent)), // Customizable recipe: "50" Screws.
                    new IngredientElement(typeof(IronBarItem), 30, typeof(MechanicsSkill), typeof(MechanicsLavishResourcesTalent)), // Customizable recipe: "30" Iron Ingots.
                },

                items: new List<CraftingElement>
                {
                    new CraftingElement<Shipping_01Item>()
                });
            this.Recipes = new List<Recipe> { recipe };
            this.ExperienceOnCraft = 25;

            this.LaborInCalories = CreateLaborInCaloriesValue(400, typeof(MechanicsSkill)); // Amount of calories consumed for crafting: "250".

            this.CraftMinutes = CreateCraftTimeValue(beneficiary: typeof(Shipping_01Recipe), start: 10, skillType: typeof(MechanicsSkill), typeof(MechanicsFocusedSpeedTalent), typeof(MechanicsParallelSpeedTalent)); // Durée de fabrication de l'objet : 10 minutes.

            this.ModsPreInitialize();
            this.Initialize(displayText: Localizer.DoStr("Container Bleu 5m"), recipeType: typeof(Shipping_01Recipe));
            this.ModsPostInitialize();

            CraftingComponent.AddRecipe(tableType: typeof(MachinistTableObject), recipeFamily: this);
        }
        partial void ModsPreInitialize();
        partial void ModsPostInitialize();
    }
#endregion



    #region Container Vert 10m
    // Shipping_02 ________________________

    [Serialized]
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(LinkComponent))]
    [RequireComponent(typeof(CustomTextComponent))]
    [RequireComponent(typeof(PublicStorageComponent))]
    [RequireComponent(typeof(ForSaleComponent))]
    [Tag("Usable")]
    [Ecopedia("Crafted Objects", "Storage", subPageName: "Container Vert 10m")]
    public partial class Shipping_02Object : WorldObject, IRepresentsItem
    {
        public virtual Type RepresentedItemType => typeof(Shipping_02Item);
        public override LocString DisplayName => Localizer.DoStr("Container Vert 10m");
        public override TableTextureMode TableTexture => TableTextureMode.Metal;


        static Shipping_02Object()

        {
            var BlockOccupancyList = new List<BlockOccupancy>
            {
            new BlockOccupancy(new Vector3i(0, 0, 0)),
            new BlockOccupancy(new Vector3i(0, 0, 1)),
            new BlockOccupancy(new Vector3i(0, 0, 2)),
            new BlockOccupancy(new Vector3i(0, 0, 3)),
            new BlockOccupancy(new Vector3i(0, 0, 4)),
            new BlockOccupancy(new Vector3i(0, 0, 5)),
            new BlockOccupancy(new Vector3i(0, 0, 6)),
            new BlockOccupancy(new Vector3i(0, 0, 7)),
            new BlockOccupancy(new Vector3i(0, 0, 8)),
            new BlockOccupancy(new Vector3i(0, 0, 9)),
            new BlockOccupancy(new Vector3i(0, 1, 0)),
            new BlockOccupancy(new Vector3i(0, 1, 1)),
            new BlockOccupancy(new Vector3i(0, 1, 2)),
            new BlockOccupancy(new Vector3i(0, 1, 3)),
            new BlockOccupancy(new Vector3i(0, 1, 4)),
            new BlockOccupancy(new Vector3i(0, 1, 5)),
            new BlockOccupancy(new Vector3i(0, 1, 6)),
            new BlockOccupancy(new Vector3i(0, 1, 7)),
            new BlockOccupancy(new Vector3i(0, 1, 8)),
            new BlockOccupancy(new Vector3i(0, 1, 9)),
            new BlockOccupancy(new Vector3i(1, 0, 0)),
            new BlockOccupancy(new Vector3i(1, 0, 1)),
            new BlockOccupancy(new Vector3i(1, 0, 2)),
            new BlockOccupancy(new Vector3i(1, 0, 3)),
            new BlockOccupancy(new Vector3i(1, 0, 4)),
            new BlockOccupancy(new Vector3i(1, 0, 5)),
            new BlockOccupancy(new Vector3i(1, 0, 6)),
            new BlockOccupancy(new Vector3i(1, 0, 7)),
            new BlockOccupancy(new Vector3i(1, 0, 8)),
            new BlockOccupancy(new Vector3i(1, 0, 9)),
            new BlockOccupancy(new Vector3i(1, 1, 0)),
            new BlockOccupancy(new Vector3i(1, 1, 1)),
            new BlockOccupancy(new Vector3i(1, 1, 2)),
            new BlockOccupancy(new Vector3i(1, 1, 3)),
            new BlockOccupancy(new Vector3i(1, 1, 4)),
            new BlockOccupancy(new Vector3i(1, 1, 5)),
            new BlockOccupancy(new Vector3i(1, 1, 6)),
            new BlockOccupancy(new Vector3i(1, 1, 7)),
            new BlockOccupancy(new Vector3i(1, 1, 8)),
            new BlockOccupancy(new Vector3i(1, 1, 9)),
            };

            AddOccupancy<Shipping_02Object>(BlockOccupancyList);




        }


        protected override void Initialize()
        {
        this.ModsPreInitialize();
        this.GetComponent<CustomTextComponent>().Initialize(700);
        this.GetComponent<LinkComponent>().Initialize(12); // Maximum connection distance with nearby storage units: 12 meters.
        PublicStorageComponent component = base.GetComponent<PublicStorageComponent>(null);
        component.Initialize(64); // Le nombre d'emplacement "64" autorisé dans le stockage.
        component.Storage.AddInvRestriction(new Inventoryx3Multiply()); // Multiplicateur de stacks

            this.ModsPostInitialize();
        }


        partial void ModsPreInitialize();

        partial void ModsPostInitialize();
    }

    [Serialized]
    [LocDisplayName("Container Vert 10m")]
    [LocDescription("Assez grand pour cacher une forêt... ou tout simplement tout ce que vous ne savez plus où mettre. En vert, pour la touche écolo, bien sûr !")]
    [Ecopedia("Crafted Objects", "Storage", createAsSubPage: true)]
    [Weight(20000)]
    [MaxStackSize(10)]
    public partial class Shipping_02Item : WorldObjectItem<Shipping_02Object>
    {

    }

    [Ecopedia("Crafted Objects", "Storage", subPageName: "Container Vert 10m")]
    [RequiresSkill(typeof(MechanicsSkill), 6)]
    public partial class Shipping_02Recipe : RecipeFamily
    {
        public Shipping_02Recipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "Container Vert 10m",  //noloc
                displayName: Localizer.DoStr("Container Vert 10m"),

                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(IronPlateItem), 200, typeof(MechanicsSkill), typeof(MechanicsLavishResourcesTalent)), // Customizable recipe: "200" Iron Plates.
                    new IngredientElement(typeof(ScrewsItem), 100, typeof(MechanicsSkill), typeof(MechanicsLavishResourcesTalent)), // Customizable recipe: "100" Screws.
                    new IngredientElement(typeof(IronBarItem), 60, typeof(MechanicsSkill), typeof(MechanicsLavishResourcesTalent)), // Customizable recipe: "60" Iron Ingots.
                },

                items: new List<CraftingElement>
                {
                    new CraftingElement<Shipping_02Item>()
                });
            this.Recipes = new List<Recipe> { recipe };
            this.ExperienceOnCraft = 50;

            this.LaborInCalories = CreateLaborInCaloriesValue(500, typeof(MechanicsSkill)); // Amount of calories consumed for crafting: "500".

            this.CraftMinutes = CreateCraftTimeValue(beneficiary: typeof(Shipping_02Recipe), start: 15, skillType: typeof(MechanicsSkill), typeof(MechanicsFocusedSpeedTalent), typeof(MechanicsParallelSpeedTalent)); // Durée de fabrication de l'objet : 15 minutes.

            this.ModsPreInitialize();
            this.Initialize(displayText: Localizer.DoStr("Container Vert 10m"), recipeType: typeof(Shipping_02Recipe));
            this.ModsPostInitialize();

            CraftingComponent.AddRecipe(tableType: typeof(MachinistTableObject), recipeFamily: this);
        }
        partial void ModsPreInitialize();
        partial void ModsPostInitialize();
    }
#endregion



    #region Container Rouge 12m
    //Shipping_03____________________________________________


    [Serialized]
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(LinkComponent))]
    [RequireComponent(typeof(CustomTextComponent))]
    [RequireComponent(typeof(PublicStorageComponent))]
    [RequireComponent(typeof(ForSaleComponent))]
    [Tag("Usable")]
    [Ecopedia("Crafted Objects", "Storage", subPageName: "Container Rouge 12m")]
    public partial class Shipping_03Object : WorldObject, IRepresentsItem
    {
        public virtual Type RepresentedItemType => typeof(Shipping_03Item);
        public override LocString DisplayName => Localizer.DoStr("Container Rouge 12m");
        public override TableTextureMode TableTexture => TableTextureMode.Metal;


        static Shipping_03Object()

        {
            var BlockOccupancyList = new List<BlockOccupancy>
            {
            new BlockOccupancy(new Vector3i(0, 0, 0)),
            new BlockOccupancy(new Vector3i(0, 0, 1)),
            new BlockOccupancy(new Vector3i(0, 0, 2)),
            new BlockOccupancy(new Vector3i(0, 0, 3)),
            new BlockOccupancy(new Vector3i(0, 0, 4)),
            new BlockOccupancy(new Vector3i(0, 0, 5)),
            new BlockOccupancy(new Vector3i(0, 0, 6)),
            new BlockOccupancy(new Vector3i(0, 0, 7)),
            new BlockOccupancy(new Vector3i(0, 0, 8)),
            new BlockOccupancy(new Vector3i(0, 0, 9)),
            new BlockOccupancy(new Vector3i(0, 0, 10)),
            new BlockOccupancy(new Vector3i(0, 0, 11)),
            new BlockOccupancy(new Vector3i(0, 1, 0)),
            new BlockOccupancy(new Vector3i(0, 1, 1)),
            new BlockOccupancy(new Vector3i(0, 1, 2)),
            new BlockOccupancy(new Vector3i(0, 1, 3)),
            new BlockOccupancy(new Vector3i(0, 1, 4)),
            new BlockOccupancy(new Vector3i(0, 1, 5)),
            new BlockOccupancy(new Vector3i(0, 1, 6)),
            new BlockOccupancy(new Vector3i(0, 1, 7)),
            new BlockOccupancy(new Vector3i(0, 1, 8)),
            new BlockOccupancy(new Vector3i(0, 1, 9)),
            new BlockOccupancy(new Vector3i(0, 1, 10)),
            new BlockOccupancy(new Vector3i(0, 1, 11)),
            new BlockOccupancy(new Vector3i(0, 2, 0)),
            new BlockOccupancy(new Vector3i(0, 2, 1)),
            new BlockOccupancy(new Vector3i(0, 2, 2)),
            new BlockOccupancy(new Vector3i(0, 2, 3)),
            new BlockOccupancy(new Vector3i(0, 2, 4)),
            new BlockOccupancy(new Vector3i(0, 2, 5)),
            new BlockOccupancy(new Vector3i(0, 2, 6)),
            new BlockOccupancy(new Vector3i(0, 2, 7)),
            new BlockOccupancy(new Vector3i(0, 2, 8)),
            new BlockOccupancy(new Vector3i(0, 2, 9)),
            new BlockOccupancy(new Vector3i(0, 2, 10)),
            new BlockOccupancy(new Vector3i(0, 2, 11)),
            new BlockOccupancy(new Vector3i(1, 0, 0)),
            new BlockOccupancy(new Vector3i(1, 0, 1)),
            new BlockOccupancy(new Vector3i(1, 0, 2)),
            new BlockOccupancy(new Vector3i(1, 0, 3)),
            new BlockOccupancy(new Vector3i(1, 0, 4)),
            new BlockOccupancy(new Vector3i(1, 0, 5)),
            new BlockOccupancy(new Vector3i(1, 0, 6)),
            new BlockOccupancy(new Vector3i(1, 0, 7)),
            new BlockOccupancy(new Vector3i(1, 0, 8)),
            new BlockOccupancy(new Vector3i(1, 0, 9)),
            new BlockOccupancy(new Vector3i(1, 0, 10)),
            new BlockOccupancy(new Vector3i(1, 0, 11)),
            new BlockOccupancy(new Vector3i(1, 1, 0)),
            new BlockOccupancy(new Vector3i(1, 1, 1)),
            new BlockOccupancy(new Vector3i(1, 1, 2)),
            new BlockOccupancy(new Vector3i(1, 1, 3)),
            new BlockOccupancy(new Vector3i(1, 1, 4)),
            new BlockOccupancy(new Vector3i(1, 1, 5)),
            new BlockOccupancy(new Vector3i(1, 1, 6)),
            new BlockOccupancy(new Vector3i(1, 1, 7)),
            new BlockOccupancy(new Vector3i(1, 1, 8)),
            new BlockOccupancy(new Vector3i(1, 1, 9)),
            new BlockOccupancy(new Vector3i(1, 1, 10)),
            new BlockOccupancy(new Vector3i(1, 1, 11)),
            new BlockOccupancy(new Vector3i(1, 2, 0)),
            new BlockOccupancy(new Vector3i(1, 2, 1)),
            new BlockOccupancy(new Vector3i(1, 2, 2)),
            new BlockOccupancy(new Vector3i(1, 2, 3)),
            new BlockOccupancy(new Vector3i(1, 2, 4)),
            new BlockOccupancy(new Vector3i(1, 2, 5)),
            new BlockOccupancy(new Vector3i(1, 2, 6)),
            new BlockOccupancy(new Vector3i(1, 2, 7)),
            new BlockOccupancy(new Vector3i(1, 2, 8)),
            new BlockOccupancy(new Vector3i(1, 2, 9)),
            new BlockOccupancy(new Vector3i(1, 2, 10)),
            new BlockOccupancy(new Vector3i(1, 2, 11)),
            new BlockOccupancy(new Vector3i(2, 0, 0)),
            new BlockOccupancy(new Vector3i(2, 0, 1)),
            new BlockOccupancy(new Vector3i(2, 0, 2)),
            new BlockOccupancy(new Vector3i(2, 0, 3)),
            new BlockOccupancy(new Vector3i(2, 0, 4)),
            new BlockOccupancy(new Vector3i(2, 0, 5)),
            new BlockOccupancy(new Vector3i(2, 0, 6)),
            new BlockOccupancy(new Vector3i(2, 0, 7)),
            new BlockOccupancy(new Vector3i(2, 0, 8)),
            new BlockOccupancy(new Vector3i(2, 0, 9)),
            new BlockOccupancy(new Vector3i(2, 0, 10)),
            new BlockOccupancy(new Vector3i(2, 0, 11)),
            new BlockOccupancy(new Vector3i(2, 1, 0)),
            new BlockOccupancy(new Vector3i(2, 1, 1)),
            new BlockOccupancy(new Vector3i(2, 1, 2)),
            new BlockOccupancy(new Vector3i(2, 1, 3)),
            new BlockOccupancy(new Vector3i(2, 1, 4)),
            new BlockOccupancy(new Vector3i(2, 1, 5)),
            new BlockOccupancy(new Vector3i(2, 1, 6)),
            new BlockOccupancy(new Vector3i(2, 1, 7)),
            new BlockOccupancy(new Vector3i(2, 1, 8)),
            new BlockOccupancy(new Vector3i(2, 1, 9)),
            new BlockOccupancy(new Vector3i(2, 1, 10)),
            new BlockOccupancy(new Vector3i(2, 1, 11)),
            new BlockOccupancy(new Vector3i(2, 2, 0)),
            new BlockOccupancy(new Vector3i(2, 2, 1)),
            new BlockOccupancy(new Vector3i(2, 2, 2)),
            new BlockOccupancy(new Vector3i(2, 2, 3)),
            new BlockOccupancy(new Vector3i(2, 2, 4)),
            new BlockOccupancy(new Vector3i(2, 2, 5)),
            new BlockOccupancy(new Vector3i(2, 2, 6)),
            new BlockOccupancy(new Vector3i(2, 2, 7)),
            new BlockOccupancy(new Vector3i(2, 2, 8)),
            new BlockOccupancy(new Vector3i(2, 2, 9)),
            new BlockOccupancy(new Vector3i(2, 2, 10)),
            new BlockOccupancy(new Vector3i(2, 2, 11)),
            };

            AddOccupancy<Shipping_03Object>(BlockOccupancyList);




        }


        protected override void Initialize()
        {
        this.ModsPreInitialize();
        this.GetComponent<CustomTextComponent>().Initialize(700);
        this.GetComponent<LinkComponent>().Initialize(12); // Maximum connection distance with nearby storage units: 12 meters.
        PublicStorageComponent component = base.GetComponent<PublicStorageComponent>(null);
        component.Initialize(128); // Le nombre d'emplacement "128" autorisé dans le stockage.
        component.Storage.AddInvRestriction(new Inventoryx3Multiply()); // Multiplicateur de stacks

            this.ModsPostInitialize();
        }



        partial void ModsPreInitialize();

        partial void ModsPostInitialize();
    }

    [Serialized]
    [LocDisplayName("Container Rouge 12m")]
    [LocDescription("Tellement grand que même un mammouth s'y sentirait à l'aise ! Rouge vif pour rappeler que vous stockez du sérieux... ou juste beaucoup trop de trucs.")]
    [Ecopedia("Crafted Objects", "Storage", createAsSubPage: true)]
    [Weight(20000)]
    [MaxStackSize(10)]
    public partial class Shipping_03Item : WorldObjectItem<Shipping_03Object>
    {

    }

    [Ecopedia("Crafted Objects", "Storage", subPageName: "Container Rouge 12m")]
    [RequiresSkill(typeof(MechanicsSkill), 7)]
    public partial class Shipping_03Recipe : RecipeFamily
    {
        public Shipping_03Recipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "Container Rouge 12m",  //noloc
                displayName: Localizer.DoStr("Container Rouge 12m"),

                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(IronPlateItem), 300, typeof(MechanicsSkill), typeof(MechanicsLavishResourcesTalent)), // Recette personnalisable : "300" Plaques de fer.
                    new IngredientElement(typeof(ScrewsItem), 200, typeof(MechanicsSkill), typeof(MechanicsLavishResourcesTalent)), // Recette personnalisable : "200" Vis.
                    new IngredientElement(typeof(IronBarItem), 120, typeof(MechanicsSkill), typeof(MechanicsLavishResourcesTalent)), // Recette personnalisable : "120" Lingot de fer.
                },

                items: new List<CraftingElement>
                {
                    new CraftingElement<Shipping_03Item>()
                });
            this.Recipes = new List<Recipe> { recipe };
            this.ExperienceOnCraft = 75;

            this.LaborInCalories = CreateLaborInCaloriesValue(700, typeof(MechanicsSkill)); // Quantité de calories "300" consommées pour la fabrication.

            this.CraftMinutes = CreateCraftTimeValue(beneficiary: typeof(Shipping_03Recipe), start: 20, skillType: typeof(MechanicsSkill), typeof(MechanicsFocusedSpeedTalent), typeof(MechanicsParallelSpeedTalent)); // Durée de fabrication de l'objet : 20 minutes.

            this.ModsPreInitialize();
            this.Initialize(displayText: Localizer.DoStr("Container Rouge 12m"), recipeType: typeof(Shipping_03Recipe));
            this.ModsPostInitialize();

            CraftingComponent.AddRecipe(tableType: typeof(MachinistTableObject), recipeFamily: this);
        }
        partial void ModsPreInitialize();
        partial void ModsPostInitialize();
    }
    #endregion



    #region Petit Stockage à bois
    // Petit Stockage Wood

    [Serialized]
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(LinkComponent))]
    [RequireComponent(typeof(OccupancyRequirementComponent))]
    [RequireComponent(typeof(ForSaleComponent))]
    [RequireComponent(typeof(ModularStockpileComponent))]
    [RequireComponent(typeof(PublicStorageComponent))]
    [Tag("Usable")]
    [Ecopedia("Crafted Objects", "Storage", subPageName: "Petit Stockage à bois")]
    public partial class PetitStockageWoodObject : WorldObject, IRepresentsItem
    {
        public virtual Type RepresentedItemType => typeof(PetitStockageWoodItem);
        public override LocString DisplayName => Localizer.DoStr("Petit Stockage à bois");
        public override TableTextureMode TableTexture => TableTextureMode.Wood;
        static PetitStockageWoodObject()

        {
            var BlockOccupancyList = new List<BlockOccupancy>
            {


            new BlockOccupancy(new Vector3i(0, 0, 0)),
            new BlockOccupancy(new Vector3i(0, 0, 1)),
            new BlockOccupancy(new Vector3i(0, 0, 2)),
            new BlockOccupancy(new Vector3i(0, 1, 0)),
            new BlockOccupancy(new Vector3i(0, 1, 1)),
            new BlockOccupancy(new Vector3i(0, 1, 2)),
            new BlockOccupancy(new Vector3i(0, 2, 0)),
            new BlockOccupancy(new Vector3i(0, 2, 1)),
            new BlockOccupancy(new Vector3i(0, 2, 2)),
            new BlockOccupancy(new Vector3i(1, 0, 0)),
            new BlockOccupancy(new Vector3i(1, 0, 1)),
            new BlockOccupancy(new Vector3i(1, 0, 2)),
            new BlockOccupancy(new Vector3i(1, 1, 0)),
            new BlockOccupancy(new Vector3i(1, 1, 1)),
            new BlockOccupancy(new Vector3i(1, 1, 2)),
            new BlockOccupancy(new Vector3i(1, 2, 0)),
            new BlockOccupancy(new Vector3i(1, 2, 1)),
            new BlockOccupancy(new Vector3i(1, 2, 2)),
            new BlockOccupancy(new Vector3i(2, 0, 0)),
            new BlockOccupancy(new Vector3i(2, 0, 1)),
            new BlockOccupancy(new Vector3i(2, 0, 2)),
            new BlockOccupancy(new Vector3i(2, 1, 0)),
            new BlockOccupancy(new Vector3i(2, 1, 1)),
            new BlockOccupancy(new Vector3i(2, 1, 2)),
            new BlockOccupancy(new Vector3i(2, 2, 0)),
            new BlockOccupancy(new Vector3i(2, 2, 1)),
            new BlockOccupancy(new Vector3i(2, 2, 2)),
            new BlockOccupancy(new Vector3i(3, 0, 0)),
            new BlockOccupancy(new Vector3i(3, 0, 1)),
            new BlockOccupancy(new Vector3i(3, 0, 2)),
            new BlockOccupancy(new Vector3i(3, 1, 0)),
            new BlockOccupancy(new Vector3i(3, 1, 1)),
            new BlockOccupancy(new Vector3i(3, 1, 2)),
            new BlockOccupancy(new Vector3i(3, 2, 0)),
            new BlockOccupancy(new Vector3i(3, 2, 1)),
            new BlockOccupancy(new Vector3i(3, 2, 2)),
            new BlockOccupancy(new Vector3i(4, 0, 0)),
            new BlockOccupancy(new Vector3i(4, 0, 1)),
            new BlockOccupancy(new Vector3i(4, 0, 2)),
            new BlockOccupancy(new Vector3i(4, 1, 0)),
            new BlockOccupancy(new Vector3i(4, 1, 1)),
            new BlockOccupancy(new Vector3i(4, 1, 2)),
            new BlockOccupancy(new Vector3i(4, 2, 0)),
            new BlockOccupancy(new Vector3i(4, 2, 1)),
            new BlockOccupancy(new Vector3i(4, 2, 2)),
            };

            AddOccupancy<PetitStockageWoodObject>(BlockOccupancyList);




        }



        protected override void Initialize()
        {
        this.ModsPreInitialize();
        this.GetComponent<StockpileComponent>().Initialize(new Vector3i(4, 2, 2));
        this.GetComponent<LinkComponent>().Initialize(12); // Maximum connection distance with nearby storage units: 12 meters.
        PublicStorageComponent component = base.GetComponent<PublicStorageComponent>(null);
        component.Initialize(18); // Le nombre d'emplacement "18" autorisé dans le stockage.
        component.Storage.AddInvRestriction(new Inventoryx2Multiply()); // Multiplicateur de stacks
        component.Storage.AddInvRestriction(new TagRestriction(new string[] // Les tags autorisées "Wood" à être utilisées dans le stockage.
        {
                "Wood",
        }));
        this.ModsPostInitialize();
        }



        partial void ModsPreInitialize();

        partial void ModsPostInitialize();
    }




    [Serialized]
    [LocDisplayName("Petit Stockage à bois")]
    [LocDescription("Parce que même les bûches méritent un toit avant de finir en planches !")]
    [Ecopedia("Crafted Objects", "Storage", createAsSubPage: true)]
    [Weight(1000)]
    [MaxStackSize(10)]
    public partial class PetitStockageWoodItem : WorldObjectItem<PetitStockageWoodObject>
    {
        protected override OccupancyContext GetOccupancyContext => new SideAttachedContext(0 | DirectionAxisFlags.Down, WorldObject.GetOccupancyInfo(this.WorldObjectType));

    }


    [RequiresSkill(typeof(CarpentrySkill), 1)]
    public partial class PetitStockageWoodRecipe : RecipeFamily
    {
        public PetitStockageWoodRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "Petit Stockage à bois",
                displayName: Localizer.DoStr("Petit Stockage à bois"),

                ingredients: new List<IngredientElement>
                {
                    new IngredientElement("HewnLog", 15, typeof(CarpentrySkill), typeof(CarpentryLavishResourcesTalent)), // Recette personnalisable : "15" HewnLog.
                    new IngredientElement("WoodBoard", 10, typeof(CarpentrySkill), typeof(CarpentryLavishResourcesTalent)), // Recette personnalisable : "10" WoodBoard.
                    new IngredientElement("Wood", 10, typeof(CarpentrySkill), typeof(CarpentryLavishResourcesTalent)), // Recette personnalisable : "10" Wood.
                },


                items: new List<CraftingElement>
                {
                    new CraftingElement<PetitStockageWoodItem>()
                });
            this.Recipes = new List<Recipe> { recipe };
            this.ExperienceOnCraft = 2;

            this.LaborInCalories = CreateLaborInCaloriesValue(250, typeof(CarpentrySkill)); // Quantité de calories "250" consommées pour la fabrication.

            this.CraftMinutes = CreateCraftTimeValue(beneficiary: typeof(PetitStockageWoodRecipe), start: 2, skillType: typeof(CarpentrySkill), typeof(CarpentryFocusedSpeedTalent), typeof(CarpentryParallelSpeedTalent)); // Durée de fabrication de l'objet : 2 minutes.

            this.ModsPreInitialize();
            this.Initialize(displayText: Localizer.DoStr("Petit Stockage à bois"), recipeType: typeof(PetitStockageWoodRecipe));
            this.ModsPostInitialize();

            CraftingComponent.AddRecipe(tableType: typeof(CarpentryTableObject), recipeFamily: this);
        }

        partial void ModsPreInitialize();

        partial void ModsPostInitialize();
    }
    #endregion



    #region Moyen stockage à bois
    // MoyenStockage_____________________________________________________________________

    [Serialized]
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(LinkComponent))]
    [RequireComponent(typeof(OccupancyRequirementComponent))]
    [RequireComponent(typeof(ForSaleComponent))]
    [RequireComponent(typeof(ModularStockpileComponent))]
    [RequireComponent(typeof(PublicStorageComponent))]
    [Tag("Usable")]
    [Ecopedia("Crafted Objects", "Storage", subPageName: "Moyen stockage à bois")]
    public partial class MoyenStockageWoodObject : WorldObject, IRepresentsItem
    {
        public virtual Type RepresentedItemType => typeof(MoyenStockageWoodItem);
        public override LocString DisplayName => Localizer.DoStr("Moyen stockage à bois");
        public override TableTextureMode TableTexture => TableTextureMode.Wood;
        static MoyenStockageWoodObject()

        {
            var BlockOccupancyList = new List<BlockOccupancy>
            {
            new BlockOccupancy(new Vector3i(0, 0, 0)),
            new BlockOccupancy(new Vector3i(0, 0, 1)),
            new BlockOccupancy(new Vector3i(0, 0, 2)),
            new BlockOccupancy(new Vector3i(0, 1, 0)),
            new BlockOccupancy(new Vector3i(0, 1, 1)),
            new BlockOccupancy(new Vector3i(0, 1, 2)),
            new BlockOccupancy(new Vector3i(0, 2, 0)),
            new BlockOccupancy(new Vector3i(0, 2, 1)),
            new BlockOccupancy(new Vector3i(0, 2, 2)),
            new BlockOccupancy(new Vector3i(0, 3, 0)),
            new BlockOccupancy(new Vector3i(0, 3, 1)),
            new BlockOccupancy(new Vector3i(0, 3, 2)),
            new BlockOccupancy(new Vector3i(0, 4, 0)),
            new BlockOccupancy(new Vector3i(0, 4, 1)),
            new BlockOccupancy(new Vector3i(0, 4, 2)),
            new BlockOccupancy(new Vector3i(1, 0, 0)),
            new BlockOccupancy(new Vector3i(1, 0, 1)),
            new BlockOccupancy(new Vector3i(1, 0, 2)),
            new BlockOccupancy(new Vector3i(1, 1, 0)),
            new BlockOccupancy(new Vector3i(1, 1, 1)),
            new BlockOccupancy(new Vector3i(1, 1, 2)),
            new BlockOccupancy(new Vector3i(1, 2, 0)),
            new BlockOccupancy(new Vector3i(1, 2, 1)),
            new BlockOccupancy(new Vector3i(1, 2, 2)),
            new BlockOccupancy(new Vector3i(1, 3, 0)),
            new BlockOccupancy(new Vector3i(1, 3, 1)),
            new BlockOccupancy(new Vector3i(1, 3, 2)),
            new BlockOccupancy(new Vector3i(1, 4, 0)),
            new BlockOccupancy(new Vector3i(1, 4, 1)),
            new BlockOccupancy(new Vector3i(1, 4, 2)),
            new BlockOccupancy(new Vector3i(2, 0, 0)),
            new BlockOccupancy(new Vector3i(2, 0, 1)),
            new BlockOccupancy(new Vector3i(2, 0, 2)),
            new BlockOccupancy(new Vector3i(2, 1, 0)),
            new BlockOccupancy(new Vector3i(2, 1, 1)),
            new BlockOccupancy(new Vector3i(2, 1, 2)),
            new BlockOccupancy(new Vector3i(2, 2, 0)),
            new BlockOccupancy(new Vector3i(2, 2, 1)),
            new BlockOccupancy(new Vector3i(2, 2, 2)),
            new BlockOccupancy(new Vector3i(2, 3, 0)),
            new BlockOccupancy(new Vector3i(2, 3, 1)),
            new BlockOccupancy(new Vector3i(2, 3, 2)),
            new BlockOccupancy(new Vector3i(2, 4, 0)),
            new BlockOccupancy(new Vector3i(2, 4, 1)),
            new BlockOccupancy(new Vector3i(2, 4, 2)),
            new BlockOccupancy(new Vector3i(3, 0, 0)),
            new BlockOccupancy(new Vector3i(3, 0, 1)),
            new BlockOccupancy(new Vector3i(3, 0, 2)),
            new BlockOccupancy(new Vector3i(3, 1, 0)),
            new BlockOccupancy(new Vector3i(3, 1, 1)),
            new BlockOccupancy(new Vector3i(3, 1, 2)),
            new BlockOccupancy(new Vector3i(3, 2, 0)),
            new BlockOccupancy(new Vector3i(3, 2, 1)),
            new BlockOccupancy(new Vector3i(3, 2, 2)),
            new BlockOccupancy(new Vector3i(3, 3, 0)),
            new BlockOccupancy(new Vector3i(3, 3, 1)),
            new BlockOccupancy(new Vector3i(3, 3, 2)),
            new BlockOccupancy(new Vector3i(3, 4, 0)),
            new BlockOccupancy(new Vector3i(3, 4, 1)),
            new BlockOccupancy(new Vector3i(3, 4, 2)),
            new BlockOccupancy(new Vector3i(4, 0, 0)),
            new BlockOccupancy(new Vector3i(4, 0, 1)),
            new BlockOccupancy(new Vector3i(4, 0, 2)),
            new BlockOccupancy(new Vector3i(4, 1, 0)),
            new BlockOccupancy(new Vector3i(4, 1, 1)),
            new BlockOccupancy(new Vector3i(4, 1, 2)),
            new BlockOccupancy(new Vector3i(4, 2, 0)),
            new BlockOccupancy(new Vector3i(4, 2, 1)),
            new BlockOccupancy(new Vector3i(4, 2, 2)),
            new BlockOccupancy(new Vector3i(4, 3, 0)),
            new BlockOccupancy(new Vector3i(4, 3, 1)),
            new BlockOccupancy(new Vector3i(4, 3, 2)),
            new BlockOccupancy(new Vector3i(4, 4, 0)),
            new BlockOccupancy(new Vector3i(4, 4, 1)),
            new BlockOccupancy(new Vector3i(4, 4, 2)),
            new BlockOccupancy(new Vector3i(5, 0, 0)),
            new BlockOccupancy(new Vector3i(5, 0, 1)),
            new BlockOccupancy(new Vector3i(5, 0, 2)),
            new BlockOccupancy(new Vector3i(5, 1, 0)),
            new BlockOccupancy(new Vector3i(5, 1, 1)),
            new BlockOccupancy(new Vector3i(5, 1, 2)),
            new BlockOccupancy(new Vector3i(5, 2, 0)),
            new BlockOccupancy(new Vector3i(5, 2, 1)),
            new BlockOccupancy(new Vector3i(5, 2, 2)),
            new BlockOccupancy(new Vector3i(5, 3, 0)),
            new BlockOccupancy(new Vector3i(5, 3, 1)),
            new BlockOccupancy(new Vector3i(5, 3, 2)),
            new BlockOccupancy(new Vector3i(5, 4, 0)),
            new BlockOccupancy(new Vector3i(5, 4, 1)),
            new BlockOccupancy(new Vector3i(5, 4, 2)),
            };

            AddOccupancy<MoyenStockageWoodObject>(BlockOccupancyList);




        }

        protected override void Initialize()
        {
        this.ModsPreInitialize();
        this.GetComponent<StockpileComponent>().Initialize(new Vector3i(5, 4, 2));
        this.GetComponent<LinkComponent>().Initialize(12); // Maximum connection distance with nearby storage units: 12 meters.
        PublicStorageComponent component = base.GetComponent<PublicStorageComponent>(null);
        component.Initialize(24); // Le nombre d'emplacement "24" autorisé dans le stockage.
        component.Storage.AddInvRestriction(new Inventoryx3Multiply()); // Multiplicateur de stacks
        component.Storage.AddInvRestriction(new TagRestriction(new string[] // Les tags autorisées "Wood" à être utilisées dans le stockage.
        {
                "Wood",
        }));
        this.ModsPostInitialize();
        }



        partial void ModsPreInitialize();

        partial void ModsPostInitialize();
    }



    [Serialized]
    [LocDisplayName("Moyen stockage à bois")]
    [LocDescription("Besoin de ranger tout ce bois qui traîne ? La moyenne pergola est là pour sauver ta cabane du chaos ! Empile ton bois avec classe (et un soupçon d’organisation, promis).")]
    [Ecopedia("Crafted Objects", "Storage", createAsSubPage: true)]
    [Weight(1000)]
    [MaxStackSize(10)]
    public partial class MoyenStockageWoodItem : WorldObjectItem<MoyenStockageWoodObject>
    {
        protected override OccupancyContext GetOccupancyContext => new SideAttachedContext(0 | DirectionAxisFlags.Down, WorldObject.GetOccupancyInfo(this.WorldObjectType));

    }


    [RequiresSkill(typeof(CarpentrySkill), 2)]
    public partial class MoyenStockageWoodRecipe : RecipeFamily
    {
        public MoyenStockageWoodRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "Moyen stockage à bois",
                displayName: Localizer.DoStr("Moyen stockage à bois"),

                ingredients: new List<IngredientElement>
                {
                    new IngredientElement("HewnLog", 40, typeof(CarpentrySkill), typeof(CarpentryLavishResourcesTalent)), // Recette personnalisable : "30" HewnLog.
                    new IngredientElement("WoodBoard", 30, typeof(CarpentrySkill), typeof(CarpentryLavishResourcesTalent)), // Recette personnalisable : "20" WoodBoard.
                    new IngredientElement("Wood", 30, typeof(CarpentrySkill), typeof(CarpentryLavishResourcesTalent)), // Recette personnalisable : "20" Wood.
                },


                items: new List<CraftingElement>
                {
                    new CraftingElement<MoyenStockageWoodItem>()
                });
            this.Recipes = new List<Recipe> { recipe };
            this.ExperienceOnCraft = 2;

            this.LaborInCalories = CreateLaborInCaloriesValue(350, typeof(CarpentrySkill)); // Quantité de calories "350" consommées pour la fabrication.

            this.CraftMinutes = CreateCraftTimeValue(beneficiary: typeof(PetitStockageWoodRecipe), start: 5, skillType: typeof(CarpentrySkill), typeof(CarpentryFocusedSpeedTalent), typeof(CarpentryParallelSpeedTalent)); // Durée de fabrication de l'objet : 5 minutes.

            this.ModsPreInitialize();
            this.Initialize(displayText: Localizer.DoStr("Moyen stockage à bois"), recipeType: typeof(MoyenStockageWoodRecipe));
            this.ModsPostInitialize();

            CraftingComponent.AddRecipe(tableType: typeof(CarpentryTableObject), recipeFamily: this);
        }

        partial void ModsPreInitialize();

        partial void ModsPostInitialize();
    }
    #endregion



    #region Grand Stockage à bois
    // GrandStockage________________________________________________________________


    [Serialized]
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(LinkComponent))]
    [RequireComponent(typeof(OccupancyRequirementComponent))]
    [RequireComponent(typeof(ForSaleComponent))]
    [RequireComponent(typeof(ModularStockpileComponent))]
    [RequireComponent(typeof(PublicStorageComponent))]
    [Tag("Usable")]
    [Ecopedia("Crafted Objects", "Storage", subPageName: "Grand Stockage à bois")]
    public partial class GrandStockageWoodObject : WorldObject, IRepresentsItem
    {
        public virtual Type RepresentedItemType => typeof(GrandStockageWoodItem);
        public override LocString DisplayName => Localizer.DoStr("Grand Stockage à bois");
        public override TableTextureMode TableTexture => TableTextureMode.Wood;
        static GrandStockageWoodObject()

        {
            var BlockOccupancyList = new List<BlockOccupancy>
            {
            new BlockOccupancy(new Vector3i(0, 0, 0)),
            new BlockOccupancy(new Vector3i(0, 0, 1)),
            new BlockOccupancy(new Vector3i(0, 0, 2)),
            new BlockOccupancy(new Vector3i(0, 0, 3)),
            new BlockOccupancy(new Vector3i(0, 0, 4)),
            new BlockOccupancy(new Vector3i(0, 0, 5)),
            new BlockOccupancy(new Vector3i(0, 0, 6)),
            new BlockOccupancy(new Vector3i(0, 0, 7)),
            new BlockOccupancy(new Vector3i(0, 1, 0)),
            new BlockOccupancy(new Vector3i(0, 1, 1)),
            new BlockOccupancy(new Vector3i(0, 1, 2)),
            new BlockOccupancy(new Vector3i(0, 1, 3)),
            new BlockOccupancy(new Vector3i(0, 1, 4)),
            new BlockOccupancy(new Vector3i(0, 1, 5)),
            new BlockOccupancy(new Vector3i(0, 1, 6)),
            new BlockOccupancy(new Vector3i(0, 1, 7)),
            new BlockOccupancy(new Vector3i(0, 2, 0)),
            new BlockOccupancy(new Vector3i(0, 2, 1)),
            new BlockOccupancy(new Vector3i(0, 2, 2)),
            new BlockOccupancy(new Vector3i(0, 2, 3)),
            new BlockOccupancy(new Vector3i(0, 2, 4)),
            new BlockOccupancy(new Vector3i(0, 2, 5)),
            new BlockOccupancy(new Vector3i(0, 2, 6)),
            new BlockOccupancy(new Vector3i(0, 2, 7)),
            new BlockOccupancy(new Vector3i(0, 3, 0)),
            new BlockOccupancy(new Vector3i(0, 3, 1)),
            new BlockOccupancy(new Vector3i(0, 3, 2)),
            new BlockOccupancy(new Vector3i(0, 3, 3)),
            new BlockOccupancy(new Vector3i(0, 3, 4)),
            new BlockOccupancy(new Vector3i(0, 3, 5)),
            new BlockOccupancy(new Vector3i(0, 3, 6)),
            new BlockOccupancy(new Vector3i(0, 3, 7)),
            new BlockOccupancy(new Vector3i(0, 4, 0)),
            new BlockOccupancy(new Vector3i(0, 4, 1)),
            new BlockOccupancy(new Vector3i(0, 4, 2)),
            new BlockOccupancy(new Vector3i(0, 4, 3)),
            new BlockOccupancy(new Vector3i(0, 4, 4)),
            new BlockOccupancy(new Vector3i(0, 4, 5)),
            new BlockOccupancy(new Vector3i(0, 4, 6)),
            new BlockOccupancy(new Vector3i(0, 4, 7)),
            new BlockOccupancy(new Vector3i(0, 5, 0)),
            new BlockOccupancy(new Vector3i(0, 5, 1)),
            new BlockOccupancy(new Vector3i(0, 5, 2)),
            new BlockOccupancy(new Vector3i(0, 5, 3)),
            new BlockOccupancy(new Vector3i(0, 5, 4)),
            new BlockOccupancy(new Vector3i(0, 5, 5)),
            new BlockOccupancy(new Vector3i(0, 5, 6)),
            new BlockOccupancy(new Vector3i(0, 5, 7)),
            new BlockOccupancy(new Vector3i(0, 6, 0)),
            new BlockOccupancy(new Vector3i(0, 6, 1)),
            new BlockOccupancy(new Vector3i(0, 6, 2)),
            new BlockOccupancy(new Vector3i(0, 6, 3)),
            new BlockOccupancy(new Vector3i(0, 6, 4)),
            new BlockOccupancy(new Vector3i(0, 6, 5)),
            new BlockOccupancy(new Vector3i(0, 6, 6)),
            new BlockOccupancy(new Vector3i(0, 6, 7)),
            new BlockOccupancy(new Vector3i(1, 0, 0)),
            new BlockOccupancy(new Vector3i(1, 0, 1)),
            new BlockOccupancy(new Vector3i(1, 0, 2)),
            new BlockOccupancy(new Vector3i(1, 0, 3)),
            new BlockOccupancy(new Vector3i(1, 0, 4)),
            new BlockOccupancy(new Vector3i(1, 0, 5)),
            new BlockOccupancy(new Vector3i(1, 0, 6)),
            new BlockOccupancy(new Vector3i(1, 0, 7)),
            new BlockOccupancy(new Vector3i(1, 1, 0)),
            new BlockOccupancy(new Vector3i(1, 1, 1)),
            new BlockOccupancy(new Vector3i(1, 1, 2)),
            new BlockOccupancy(new Vector3i(1, 1, 3)),
            new BlockOccupancy(new Vector3i(1, 1, 4)),
            new BlockOccupancy(new Vector3i(1, 1, 5)),
            new BlockOccupancy(new Vector3i(1, 1, 6)),
            new BlockOccupancy(new Vector3i(1, 1, 7)),
            new BlockOccupancy(new Vector3i(1, 2, 0)),
            new BlockOccupancy(new Vector3i(1, 2, 1)),
            new BlockOccupancy(new Vector3i(1, 2, 2)),
            new BlockOccupancy(new Vector3i(1, 2, 3)),
            new BlockOccupancy(new Vector3i(1, 2, 4)),
            new BlockOccupancy(new Vector3i(1, 2, 5)),
            new BlockOccupancy(new Vector3i(1, 2, 6)),
            new BlockOccupancy(new Vector3i(1, 2, 7)),
            new BlockOccupancy(new Vector3i(1, 3, 0)),
            new BlockOccupancy(new Vector3i(1, 3, 1)),
            new BlockOccupancy(new Vector3i(1, 3, 2)),
            new BlockOccupancy(new Vector3i(1, 3, 3)),
            new BlockOccupancy(new Vector3i(1, 3, 4)),
            new BlockOccupancy(new Vector3i(1, 3, 5)),
            new BlockOccupancy(new Vector3i(1, 3, 6)),
            new BlockOccupancy(new Vector3i(1, 3, 7)),
            new BlockOccupancy(new Vector3i(1, 4, 0)),
            new BlockOccupancy(new Vector3i(1, 4, 1)),
            new BlockOccupancy(new Vector3i(1, 4, 2)),
            new BlockOccupancy(new Vector3i(1, 4, 3)),
            new BlockOccupancy(new Vector3i(1, 4, 4)),
            new BlockOccupancy(new Vector3i(1, 4, 5)),
            new BlockOccupancy(new Vector3i(1, 4, 6)),
            new BlockOccupancy(new Vector3i(1, 4, 7)),
            new BlockOccupancy(new Vector3i(1, 5, 0)),
            new BlockOccupancy(new Vector3i(1, 5, 1)),
            new BlockOccupancy(new Vector3i(1, 5, 2)),
            new BlockOccupancy(new Vector3i(1, 5, 3)),
            new BlockOccupancy(new Vector3i(1, 5, 4)),
            new BlockOccupancy(new Vector3i(1, 5, 5)),
            new BlockOccupancy(new Vector3i(1, 5, 6)),
            new BlockOccupancy(new Vector3i(1, 5, 7)),
            new BlockOccupancy(new Vector3i(1, 6, 0)),
            new BlockOccupancy(new Vector3i(1, 6, 1)),
            new BlockOccupancy(new Vector3i(1, 6, 2)),
            new BlockOccupancy(new Vector3i(1, 6, 3)),
            new BlockOccupancy(new Vector3i(1, 6, 4)),
            new BlockOccupancy(new Vector3i(1, 6, 5)),
            new BlockOccupancy(new Vector3i(1, 6, 6)),
            new BlockOccupancy(new Vector3i(1, 6, 7)),
            new BlockOccupancy(new Vector3i(2, 0, 0)),
            new BlockOccupancy(new Vector3i(2, 0, 1)),
            new BlockOccupancy(new Vector3i(2, 0, 2)),
            new BlockOccupancy(new Vector3i(2, 0, 3)),
            new BlockOccupancy(new Vector3i(2, 0, 4)),
            new BlockOccupancy(new Vector3i(2, 0, 5)),
            new BlockOccupancy(new Vector3i(2, 0, 6)),
            new BlockOccupancy(new Vector3i(2, 0, 7)),
            new BlockOccupancy(new Vector3i(2, 1, 0)),
            new BlockOccupancy(new Vector3i(2, 1, 1)),
            new BlockOccupancy(new Vector3i(2, 1, 2)),
            new BlockOccupancy(new Vector3i(2, 1, 3)),
            new BlockOccupancy(new Vector3i(2, 1, 4)),
            new BlockOccupancy(new Vector3i(2, 1, 5)),
            new BlockOccupancy(new Vector3i(2, 1, 6)),
            new BlockOccupancy(new Vector3i(2, 1, 7)),
            new BlockOccupancy(new Vector3i(2, 2, 0)),
            new BlockOccupancy(new Vector3i(2, 2, 1)),
            new BlockOccupancy(new Vector3i(2, 2, 2)),
            new BlockOccupancy(new Vector3i(2, 2, 3)),
            new BlockOccupancy(new Vector3i(2, 2, 4)),
            new BlockOccupancy(new Vector3i(2, 2, 5)),
            new BlockOccupancy(new Vector3i(2, 2, 6)),
            new BlockOccupancy(new Vector3i(2, 2, 7)),
            new BlockOccupancy(new Vector3i(2, 3, 0)),
            new BlockOccupancy(new Vector3i(2, 3, 1)),
            new BlockOccupancy(new Vector3i(2, 3, 2)),
            new BlockOccupancy(new Vector3i(2, 3, 3)),
            new BlockOccupancy(new Vector3i(2, 3, 4)),
            new BlockOccupancy(new Vector3i(2, 3, 5)),
            new BlockOccupancy(new Vector3i(2, 3, 6)),
            new BlockOccupancy(new Vector3i(2, 3, 7)),
            new BlockOccupancy(new Vector3i(2, 4, 0)),
            new BlockOccupancy(new Vector3i(2, 4, 1)),
            new BlockOccupancy(new Vector3i(2, 4, 2)),
            new BlockOccupancy(new Vector3i(2, 4, 3)),
            new BlockOccupancy(new Vector3i(2, 4, 4)),
            new BlockOccupancy(new Vector3i(2, 4, 5)),
            new BlockOccupancy(new Vector3i(2, 4, 6)),
            new BlockOccupancy(new Vector3i(2, 4, 7)),
            new BlockOccupancy(new Vector3i(2, 5, 0)),
            new BlockOccupancy(new Vector3i(2, 5, 1)),
            new BlockOccupancy(new Vector3i(2, 5, 2)),
            new BlockOccupancy(new Vector3i(2, 5, 3)),
            new BlockOccupancy(new Vector3i(2, 5, 4)),
            new BlockOccupancy(new Vector3i(2, 5, 5)),
            new BlockOccupancy(new Vector3i(2, 5, 6)),
            new BlockOccupancy(new Vector3i(2, 5, 7)),
            new BlockOccupancy(new Vector3i(2, 6, 0)),
            new BlockOccupancy(new Vector3i(2, 6, 1)),
            new BlockOccupancy(new Vector3i(2, 6, 2)),
            new BlockOccupancy(new Vector3i(2, 6, 3)),
            new BlockOccupancy(new Vector3i(2, 6, 4)),
            new BlockOccupancy(new Vector3i(2, 6, 5)),
            new BlockOccupancy(new Vector3i(2, 6, 6)),
            new BlockOccupancy(new Vector3i(2, 6, 7)),
            new BlockOccupancy(new Vector3i(3, 0, 0)),
            new BlockOccupancy(new Vector3i(3, 0, 1)),
            new BlockOccupancy(new Vector3i(3, 0, 2)),
            new BlockOccupancy(new Vector3i(3, 0, 3)),
            new BlockOccupancy(new Vector3i(3, 0, 4)),
            new BlockOccupancy(new Vector3i(3, 0, 5)),
            new BlockOccupancy(new Vector3i(3, 0, 6)),
            new BlockOccupancy(new Vector3i(3, 0, 7)),
            new BlockOccupancy(new Vector3i(3, 1, 0)),
            new BlockOccupancy(new Vector3i(3, 1, 1)),
            new BlockOccupancy(new Vector3i(3, 1, 2)),
            new BlockOccupancy(new Vector3i(3, 1, 3)),
            new BlockOccupancy(new Vector3i(3, 1, 4)),
            new BlockOccupancy(new Vector3i(3, 1, 5)),
            new BlockOccupancy(new Vector3i(3, 1, 6)),
            new BlockOccupancy(new Vector3i(3, 1, 7)),
            new BlockOccupancy(new Vector3i(3, 2, 0)),
            new BlockOccupancy(new Vector3i(3, 2, 1)),
            new BlockOccupancy(new Vector3i(3, 2, 2)),
            new BlockOccupancy(new Vector3i(3, 2, 3)),
            new BlockOccupancy(new Vector3i(3, 2, 4)),
            new BlockOccupancy(new Vector3i(3, 2, 5)),
            new BlockOccupancy(new Vector3i(3, 2, 6)),
            new BlockOccupancy(new Vector3i(3, 2, 7)),
            new BlockOccupancy(new Vector3i(3, 3, 0)),
            new BlockOccupancy(new Vector3i(3, 3, 1)),
            new BlockOccupancy(new Vector3i(3, 3, 2)),
            new BlockOccupancy(new Vector3i(3, 3, 3)),
            new BlockOccupancy(new Vector3i(3, 3, 4)),
            new BlockOccupancy(new Vector3i(3, 3, 5)),
            new BlockOccupancy(new Vector3i(3, 3, 6)),
            new BlockOccupancy(new Vector3i(3, 3, 7)),
            new BlockOccupancy(new Vector3i(3, 4, 0)),
            new BlockOccupancy(new Vector3i(3, 4, 1)),
            new BlockOccupancy(new Vector3i(3, 4, 2)),
            new BlockOccupancy(new Vector3i(3, 4, 3)),
            new BlockOccupancy(new Vector3i(3, 4, 4)),
            new BlockOccupancy(new Vector3i(3, 4, 5)),
            new BlockOccupancy(new Vector3i(3, 4, 6)),
            new BlockOccupancy(new Vector3i(3, 4, 7)),
            new BlockOccupancy(new Vector3i(3, 5, 0)),
            new BlockOccupancy(new Vector3i(3, 5, 1)),
            new BlockOccupancy(new Vector3i(3, 5, 2)),
            new BlockOccupancy(new Vector3i(3, 5, 3)),
            new BlockOccupancy(new Vector3i(3, 5, 4)),
            new BlockOccupancy(new Vector3i(3, 5, 5)),
            new BlockOccupancy(new Vector3i(3, 5, 6)),
            new BlockOccupancy(new Vector3i(3, 5, 7)),
            new BlockOccupancy(new Vector3i(3, 6, 0)),
            new BlockOccupancy(new Vector3i(3, 6, 1)),
            new BlockOccupancy(new Vector3i(3, 6, 2)),
            new BlockOccupancy(new Vector3i(3, 6, 3)),
            new BlockOccupancy(new Vector3i(3, 6, 4)),
            new BlockOccupancy(new Vector3i(3, 6, 5)),
            new BlockOccupancy(new Vector3i(3, 6, 6)),
            new BlockOccupancy(new Vector3i(3, 6, 7)),
            new BlockOccupancy(new Vector3i(4, 0, 0)),
            new BlockOccupancy(new Vector3i(4, 0, 1)),
            new BlockOccupancy(new Vector3i(4, 0, 2)),
            new BlockOccupancy(new Vector3i(4, 0, 3)),
            new BlockOccupancy(new Vector3i(4, 0, 4)),
            new BlockOccupancy(new Vector3i(4, 0, 5)),
            new BlockOccupancy(new Vector3i(4, 0, 6)),
            new BlockOccupancy(new Vector3i(4, 0, 7)),
            new BlockOccupancy(new Vector3i(4, 1, 0)),
            new BlockOccupancy(new Vector3i(4, 1, 1)),
            new BlockOccupancy(new Vector3i(4, 1, 2)),
            new BlockOccupancy(new Vector3i(4, 1, 3)),
            new BlockOccupancy(new Vector3i(4, 1, 4)),
            new BlockOccupancy(new Vector3i(4, 1, 5)),
            new BlockOccupancy(new Vector3i(4, 1, 6)),
            new BlockOccupancy(new Vector3i(4, 1, 7)),
            new BlockOccupancy(new Vector3i(4, 2, 0)),
            new BlockOccupancy(new Vector3i(4, 2, 1)),
            new BlockOccupancy(new Vector3i(4, 2, 2)),
            new BlockOccupancy(new Vector3i(4, 2, 3)),
            new BlockOccupancy(new Vector3i(4, 2, 4)),
            new BlockOccupancy(new Vector3i(4, 2, 5)),
            new BlockOccupancy(new Vector3i(4, 2, 6)),
            new BlockOccupancy(new Vector3i(4, 2, 7)),
            new BlockOccupancy(new Vector3i(4, 3, 0)),
            new BlockOccupancy(new Vector3i(4, 3, 1)),
            new BlockOccupancy(new Vector3i(4, 3, 2)),
            new BlockOccupancy(new Vector3i(4, 3, 3)),
            new BlockOccupancy(new Vector3i(4, 3, 4)),
            new BlockOccupancy(new Vector3i(4, 3, 5)),
            new BlockOccupancy(new Vector3i(4, 3, 6)),
            new BlockOccupancy(new Vector3i(4, 3, 7)),
            new BlockOccupancy(new Vector3i(4, 4, 0)),
            new BlockOccupancy(new Vector3i(4, 4, 1)),
            new BlockOccupancy(new Vector3i(4, 4, 2)),
            new BlockOccupancy(new Vector3i(4, 4, 3)),
            new BlockOccupancy(new Vector3i(4, 4, 4)),
            new BlockOccupancy(new Vector3i(4, 4, 5)),
            new BlockOccupancy(new Vector3i(4, 4, 6)),
            new BlockOccupancy(new Vector3i(4, 4, 7)),
            new BlockOccupancy(new Vector3i(4, 5, 0)),
            new BlockOccupancy(new Vector3i(4, 5, 1)),
            new BlockOccupancy(new Vector3i(4, 5, 2)),
            new BlockOccupancy(new Vector3i(4, 5, 3)),
            new BlockOccupancy(new Vector3i(4, 5, 4)),
            new BlockOccupancy(new Vector3i(4, 5, 5)),
            new BlockOccupancy(new Vector3i(4, 5, 6)),
            new BlockOccupancy(new Vector3i(4, 5, 7)),
            new BlockOccupancy(new Vector3i(4, 6, 0)),
            new BlockOccupancy(new Vector3i(4, 6, 1)),
            new BlockOccupancy(new Vector3i(4, 6, 2)),
            new BlockOccupancy(new Vector3i(4, 6, 3)),
            new BlockOccupancy(new Vector3i(4, 6, 4)),
            new BlockOccupancy(new Vector3i(4, 6, 5)),
            new BlockOccupancy(new Vector3i(4, 6, 6)),
            new BlockOccupancy(new Vector3i(4, 6, 7)),
            new BlockOccupancy(new Vector3i(5, 0, 0)),
            new BlockOccupancy(new Vector3i(5, 0, 1)),
            new BlockOccupancy(new Vector3i(5, 0, 2)),
            new BlockOccupancy(new Vector3i(5, 0, 3)),
            new BlockOccupancy(new Vector3i(5, 0, 4)),
            new BlockOccupancy(new Vector3i(5, 0, 5)),
            new BlockOccupancy(new Vector3i(5, 0, 6)),
            new BlockOccupancy(new Vector3i(5, 0, 7)),
            new BlockOccupancy(new Vector3i(5, 1, 0)),
            new BlockOccupancy(new Vector3i(5, 1, 1)),
            new BlockOccupancy(new Vector3i(5, 1, 2)),
            new BlockOccupancy(new Vector3i(5, 1, 3)),
            new BlockOccupancy(new Vector3i(5, 1, 4)),
            new BlockOccupancy(new Vector3i(5, 1, 5)),
            new BlockOccupancy(new Vector3i(5, 1, 6)),
            new BlockOccupancy(new Vector3i(5, 1, 7)),
            new BlockOccupancy(new Vector3i(5, 2, 0)),
            new BlockOccupancy(new Vector3i(5, 2, 1)),
            new BlockOccupancy(new Vector3i(5, 2, 2)),
            new BlockOccupancy(new Vector3i(5, 2, 3)),
            new BlockOccupancy(new Vector3i(5, 2, 4)),
            new BlockOccupancy(new Vector3i(5, 2, 5)),
            new BlockOccupancy(new Vector3i(5, 2, 6)),
            new BlockOccupancy(new Vector3i(5, 2, 7)),
            new BlockOccupancy(new Vector3i(5, 3, 0)),
            new BlockOccupancy(new Vector3i(5, 3, 1)),
            new BlockOccupancy(new Vector3i(5, 3, 2)),
            new BlockOccupancy(new Vector3i(5, 3, 3)),
            new BlockOccupancy(new Vector3i(5, 3, 4)),
            new BlockOccupancy(new Vector3i(5, 3, 5)),
            new BlockOccupancy(new Vector3i(5, 3, 6)),
            new BlockOccupancy(new Vector3i(5, 3, 7)),
            new BlockOccupancy(new Vector3i(5, 4, 0)),
            new BlockOccupancy(new Vector3i(5, 4, 1)),
            new BlockOccupancy(new Vector3i(5, 4, 2)),
            new BlockOccupancy(new Vector3i(5, 4, 3)),
            new BlockOccupancy(new Vector3i(5, 4, 4)),
            new BlockOccupancy(new Vector3i(5, 4, 5)),
            new BlockOccupancy(new Vector3i(5, 4, 6)),
            new BlockOccupancy(new Vector3i(5, 4, 7)),
            new BlockOccupancy(new Vector3i(5, 5, 0)),
            new BlockOccupancy(new Vector3i(5, 5, 1)),
            new BlockOccupancy(new Vector3i(5, 5, 2)),
            new BlockOccupancy(new Vector3i(5, 5, 3)),
            new BlockOccupancy(new Vector3i(5, 5, 4)),
            new BlockOccupancy(new Vector3i(5, 5, 5)),
            new BlockOccupancy(new Vector3i(5, 5, 6)),
            new BlockOccupancy(new Vector3i(5, 5, 7)),
            new BlockOccupancy(new Vector3i(5, 6, 0)),
            new BlockOccupancy(new Vector3i(5, 6, 1)),
            new BlockOccupancy(new Vector3i(5, 6, 2)),
            new BlockOccupancy(new Vector3i(5, 6, 3)),
            new BlockOccupancy(new Vector3i(5, 6, 4)),
            new BlockOccupancy(new Vector3i(5, 6, 5)),
            new BlockOccupancy(new Vector3i(5, 6, 6)),
            new BlockOccupancy(new Vector3i(5, 6, 7)),
            new BlockOccupancy(new Vector3i(6, 0, 0)),
            new BlockOccupancy(new Vector3i(6, 0, 1)),
            new BlockOccupancy(new Vector3i(6, 0, 2)),
            new BlockOccupancy(new Vector3i(6, 0, 3)),
            new BlockOccupancy(new Vector3i(6, 0, 4)),
            new BlockOccupancy(new Vector3i(6, 0, 5)),
            new BlockOccupancy(new Vector3i(6, 0, 6)),
            new BlockOccupancy(new Vector3i(6, 0, 7)),
            new BlockOccupancy(new Vector3i(6, 1, 0)),
            new BlockOccupancy(new Vector3i(6, 1, 1)),
            new BlockOccupancy(new Vector3i(6, 1, 2)),
            new BlockOccupancy(new Vector3i(6, 1, 3)),
            new BlockOccupancy(new Vector3i(6, 1, 4)),
            new BlockOccupancy(new Vector3i(6, 1, 5)),
            new BlockOccupancy(new Vector3i(6, 1, 6)),
            new BlockOccupancy(new Vector3i(6, 1, 7)),
            new BlockOccupancy(new Vector3i(6, 2, 0)),
            new BlockOccupancy(new Vector3i(6, 2, 1)),
            new BlockOccupancy(new Vector3i(6, 2, 2)),
            new BlockOccupancy(new Vector3i(6, 2, 3)),
            new BlockOccupancy(new Vector3i(6, 2, 4)),
            new BlockOccupancy(new Vector3i(6, 2, 5)),
            new BlockOccupancy(new Vector3i(6, 2, 6)),
            new BlockOccupancy(new Vector3i(6, 2, 7)),
            new BlockOccupancy(new Vector3i(6, 3, 0)),
            new BlockOccupancy(new Vector3i(6, 3, 1)),
            new BlockOccupancy(new Vector3i(6, 3, 2)),
            new BlockOccupancy(new Vector3i(6, 3, 3)),
            new BlockOccupancy(new Vector3i(6, 3, 4)),
            new BlockOccupancy(new Vector3i(6, 3, 5)),
            new BlockOccupancy(new Vector3i(6, 3, 6)),
            new BlockOccupancy(new Vector3i(6, 3, 7)),
            new BlockOccupancy(new Vector3i(6, 4, 0)),
            new BlockOccupancy(new Vector3i(6, 4, 1)),
            new BlockOccupancy(new Vector3i(6, 4, 2)),
            new BlockOccupancy(new Vector3i(6, 4, 3)),
            new BlockOccupancy(new Vector3i(6, 4, 4)),
            new BlockOccupancy(new Vector3i(6, 4, 5)),
            new BlockOccupancy(new Vector3i(6, 4, 6)),
            new BlockOccupancy(new Vector3i(6, 4, 7)),
            new BlockOccupancy(new Vector3i(6, 5, 0)),
            new BlockOccupancy(new Vector3i(6, 5, 1)),
            new BlockOccupancy(new Vector3i(6, 5, 2)),
            new BlockOccupancy(new Vector3i(6, 5, 3)),
            new BlockOccupancy(new Vector3i(6, 5, 4)),
            new BlockOccupancy(new Vector3i(6, 5, 5)),
            new BlockOccupancy(new Vector3i(6, 5, 6)),
            new BlockOccupancy(new Vector3i(6, 5, 7)),
            new BlockOccupancy(new Vector3i(6, 6, 0)),
            new BlockOccupancy(new Vector3i(6, 6, 1)),
            new BlockOccupancy(new Vector3i(6, 6, 2)),
            new BlockOccupancy(new Vector3i(6, 6, 3)),
            new BlockOccupancy(new Vector3i(6, 6, 4)),
            new BlockOccupancy(new Vector3i(6, 6, 5)),
            new BlockOccupancy(new Vector3i(6, 6, 6)),
            new BlockOccupancy(new Vector3i(6, 6, 7)),
            new BlockOccupancy(new Vector3i(7, 0, 0)),
            new BlockOccupancy(new Vector3i(7, 0, 1)),
            new BlockOccupancy(new Vector3i(7, 0, 2)),
            new BlockOccupancy(new Vector3i(7, 0, 3)),
            new BlockOccupancy(new Vector3i(7, 0, 4)),
            new BlockOccupancy(new Vector3i(7, 0, 5)),
            new BlockOccupancy(new Vector3i(7, 0, 6)),
            new BlockOccupancy(new Vector3i(7, 0, 7)),
            new BlockOccupancy(new Vector3i(7, 1, 0)),
            new BlockOccupancy(new Vector3i(7, 1, 1)),
            new BlockOccupancy(new Vector3i(7, 1, 2)),
            new BlockOccupancy(new Vector3i(7, 1, 3)),
            new BlockOccupancy(new Vector3i(7, 1, 4)),
            new BlockOccupancy(new Vector3i(7, 1, 5)),
            new BlockOccupancy(new Vector3i(7, 1, 6)),
            new BlockOccupancy(new Vector3i(7, 1, 7)),
            new BlockOccupancy(new Vector3i(7, 2, 0)),
            new BlockOccupancy(new Vector3i(7, 2, 1)),
            new BlockOccupancy(new Vector3i(7, 2, 2)),
            new BlockOccupancy(new Vector3i(7, 2, 3)),
            new BlockOccupancy(new Vector3i(7, 2, 4)),
            new BlockOccupancy(new Vector3i(7, 2, 5)),
            new BlockOccupancy(new Vector3i(7, 2, 6)),
            new BlockOccupancy(new Vector3i(7, 2, 7)),
            new BlockOccupancy(new Vector3i(7, 3, 0)),
            new BlockOccupancy(new Vector3i(7, 3, 1)),
            new BlockOccupancy(new Vector3i(7, 3, 2)),
            new BlockOccupancy(new Vector3i(7, 3, 3)),
            new BlockOccupancy(new Vector3i(7, 3, 4)),
            new BlockOccupancy(new Vector3i(7, 3, 5)),
            new BlockOccupancy(new Vector3i(7, 3, 6)),
            new BlockOccupancy(new Vector3i(7, 3, 7)),
            new BlockOccupancy(new Vector3i(7, 4, 0)),
            new BlockOccupancy(new Vector3i(7, 4, 1)),
            new BlockOccupancy(new Vector3i(7, 4, 2)),
            new BlockOccupancy(new Vector3i(7, 4, 3)),
            new BlockOccupancy(new Vector3i(7, 4, 4)),
            new BlockOccupancy(new Vector3i(7, 4, 5)),
            new BlockOccupancy(new Vector3i(7, 4, 6)),
            new BlockOccupancy(new Vector3i(7, 4, 7)),
            new BlockOccupancy(new Vector3i(7, 5, 0)),
            new BlockOccupancy(new Vector3i(7, 5, 1)),
            new BlockOccupancy(new Vector3i(7, 5, 2)),
            new BlockOccupancy(new Vector3i(7, 5, 3)),
            new BlockOccupancy(new Vector3i(7, 5, 4)),
            new BlockOccupancy(new Vector3i(7, 5, 5)),
            new BlockOccupancy(new Vector3i(7, 5, 6)),
            new BlockOccupancy(new Vector3i(7, 5, 7)),
            new BlockOccupancy(new Vector3i(7, 6, 0)),
            new BlockOccupancy(new Vector3i(7, 6, 1)),
            new BlockOccupancy(new Vector3i(7, 6, 2)),
            new BlockOccupancy(new Vector3i(7, 6, 3)),
            new BlockOccupancy(new Vector3i(7, 6, 4)),
            new BlockOccupancy(new Vector3i(7, 6, 5)),
            new BlockOccupancy(new Vector3i(7, 6, 6)),
            new BlockOccupancy(new Vector3i(7, 6, 7)),
            };

            AddOccupancy<GrandStockageWoodObject>(BlockOccupancyList);




        }

        protected override void Initialize()
        {
        this.ModsPreInitialize();
        this.GetComponent<StockpileComponent>().Initialize(new Vector3i(5, 5, 6));
        this.GetComponent<LinkComponent>().Initialize(12); // Maximum connection distance with nearby storage units: 12 meters.
        PublicStorageComponent component = base.GetComponent<PublicStorageComponent>(null);
        component.Initialize(32); // Le nombre d'emplacement "32" autorisé dans le stockage.
        component.Storage.AddInvRestriction(new Inventoryx4Multiply()); // Multiplicateur de stacks
        component.Storage.AddInvRestriction(new TagRestriction(new string[] // Les tags autorisées "Wood" à être utilisées dans le stockage.
        {
                "Wood",
        }));
        this.ModsPostInitialize();
        }



        partial void ModsPreInitialize();

        partial void ModsPostInitialize();
    }

    [Serialized]
    [LocDisplayName("Grand Stockage à bois")]
    [LocDescription("Parce que jeter des bûches partout, c'est bon pour les castors, mais pas pour toi. Range tes précieux bouts de forêt dans cette GrandStockageWood élégante, et impressionne tes amis bûcherons avec ta maîtrise de l'art du rangement !")]
    [Ecopedia("Crafted Objects", "Storage", createAsSubPage: true)]
    [Weight(1000)]
    [MaxStackSize(10)]
    public partial class GrandStockageWoodItem : WorldObjectItem<GrandStockageWoodObject>
    {
        protected override OccupancyContext GetOccupancyContext => new SideAttachedContext(0 | DirectionAxisFlags.Down, WorldObject.GetOccupancyInfo(this.WorldObjectType));

    }


    [RequiresSkill(typeof(CarpentrySkill), 3)]
    public partial class GrandStockageWoodRecipe : RecipeFamily
    {
        public GrandStockageWoodRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "Grand Stockage à bois",
                displayName: Localizer.DoStr("Grand Stockage à bois"),

                ingredients: new List<IngredientElement>
                {
                    new IngredientElement("Lumber", 60, typeof(CarpentrySkill), typeof(CarpentryLavishResourcesTalent)), // Recette personnalisable : "50" Lumber.
                    new IngredientElement("WoodBoard", 40, typeof(CarpentrySkill), typeof(CarpentryLavishResourcesTalent)), // Recette personnalisable : "20" WoodBoard.
                    new IngredientElement("Wood", 40, typeof(CarpentrySkill), typeof(CarpentryLavishResourcesTalent)), // Recette personnalisable : "20" Wood.
                },


                items: new List<CraftingElement>
                {
                    new CraftingElement<GrandStockageWoodItem>()
                });
            this.Recipes = new List<Recipe> { recipe };
            this.ExperienceOnCraft = 2;

            this.LaborInCalories = CreateLaborInCaloriesValue(450, typeof(CarpentrySkill)); // Quantité de calories "450" consommées pour la fabrication.

            this.CraftMinutes = CreateCraftTimeValue(beneficiary: typeof(PetitStockageWoodRecipe), start: 10, skillType: typeof(CarpentrySkill), typeof(CarpentryFocusedSpeedTalent), typeof(CarpentryParallelSpeedTalent)); // Durée de fabrication de l'objet : 10 minutes.

            this.ModsPreInitialize();
            this.Initialize(displayText: Localizer.DoStr("Grand Stockage à bois"), recipeType: typeof(GrandStockageWoodRecipe));
            this.ModsPostInitialize();

            CraftingComponent.AddRecipe(tableType: typeof(CarpentryTableObject), recipeFamily: this);
        }

        partial void ModsPreInitialize();

        partial void ModsPostInitialize();
    }
    #endregion



    #region Icon Tag CrushedRock
    // Icon Tag CrushedRock
    [Serialized]
        [Category("Blocks")]
        [LocDisplayName("CrushedRock")]
        public partial class CrushedRockItem : Item
        {
            public override LocString DisplayName => Localizer.DoStr("CrushedRock");

        }
    #endregion



    #region Icon Tag Silica
    // Icon Tag Silica

    [Serialized]
    [LocDisplayName("Silica")]
    public partial class SilicaItem : Item
    {
        public override LocString DisplayName => Localizer.DoStr("Silica");
    }
    #endregion



    #region Icon Tag Excavatable
    // Icon Tag Excavatable

    [Serialized]
    [Category("Blocks")]
    [LocDisplayName("Excavatable")]
    public partial class ExcavatableItem : Item
    {
        public override LocString DisplayName => Localizer.DoStr("Excavatable");
    }
    #endregion



    #region Icon Tag Constructable
    // Icon Tag Constructable

    [Serialized]
    [Category("Blocks")]
    [LocDisplayName("Constructable")]
    public partial class ConstructableItem : Item
    {
        public override LocString DisplayName => Localizer.DoStr("Constructable");
    }
    #endregion



    #region Silo raffinerie de pétrole
    // Silo raffinerie de pétrole


    [Serialized]
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(LinkComponent))]
    [RequireComponent(typeof(OccupancyRequirementComponent))]
    [RequireComponent(typeof(ForSaleComponent))]
    [RequireComponent(typeof(PublicStorageComponent))]
    [Tag("Usable")]
    [Ecopedia("Crafted Objects", "Storage", subPageName: "Silo raffinerie de pétrole")]
    public partial class SiloPetroleObject : WorldObject, IRepresentsItem
    {
        public virtual Type RepresentedItemType => typeof(SiloPetroleItem);
        public override LocString DisplayName => Localizer.DoStr("Silo raffinerie de pétrole");
        public override TableTextureMode TableTexture => TableTextureMode.Metal;
        static SiloPetroleObject()

        {
            var BlockOccupancyList = new List<BlockOccupancy>
            {
            new BlockOccupancy(new Vector3i(0, 0, 0)),
            new BlockOccupancy(new Vector3i(0, 0, 1)),
            new BlockOccupancy(new Vector3i(0, 0, 2)),
            new BlockOccupancy(new Vector3i(0, 0, 3)),
            new BlockOccupancy(new Vector3i(0, 0, 4)),
            new BlockOccupancy(new Vector3i(0, 1, 0)),
            new BlockOccupancy(new Vector3i(0, 1, 1)),
            new BlockOccupancy(new Vector3i(0, 1, 2)),
            new BlockOccupancy(new Vector3i(0, 1, 3)),
            new BlockOccupancy(new Vector3i(0, 1, 4)),
            new BlockOccupancy(new Vector3i(0, 2, 0)),
            new BlockOccupancy(new Vector3i(0, 2, 1)),
            new BlockOccupancy(new Vector3i(0, 2, 2)),
            new BlockOccupancy(new Vector3i(0, 2, 3)),
            new BlockOccupancy(new Vector3i(0, 2, 4)),
            new BlockOccupancy(new Vector3i(0, 3, 0)),
            new BlockOccupancy(new Vector3i(0, 3, 1)),
            new BlockOccupancy(new Vector3i(0, 3, 2)),
            new BlockOccupancy(new Vector3i(0, 3, 3)),
            new BlockOccupancy(new Vector3i(0, 3, 4)),
            new BlockOccupancy(new Vector3i(0, 4, 0)),
            new BlockOccupancy(new Vector3i(0, 4, 1)),
            new BlockOccupancy(new Vector3i(0, 4, 2)),
            new BlockOccupancy(new Vector3i(0, 4, 3)),
            new BlockOccupancy(new Vector3i(0, 4, 4)),
            new BlockOccupancy(new Vector3i(0, 5, 0)),
            new BlockOccupancy(new Vector3i(0, 5, 1)),
            new BlockOccupancy(new Vector3i(0, 5, 2)),
            new BlockOccupancy(new Vector3i(0, 5, 3)),
            new BlockOccupancy(new Vector3i(0, 5, 4)),
            new BlockOccupancy(new Vector3i(1, 0, 0)),
            new BlockOccupancy(new Vector3i(1, 0, 1)),
            new BlockOccupancy(new Vector3i(1, 0, 2)),
            new BlockOccupancy(new Vector3i(1, 0, 3)),
            new BlockOccupancy(new Vector3i(1, 0, 4)),
            new BlockOccupancy(new Vector3i(1, 1, 0)),
            new BlockOccupancy(new Vector3i(1, 1, 1)),
            new BlockOccupancy(new Vector3i(1, 1, 2)),
            new BlockOccupancy(new Vector3i(1, 1, 3)),
            new BlockOccupancy(new Vector3i(1, 1, 4)),
            new BlockOccupancy(new Vector3i(1, 2, 0)),
            new BlockOccupancy(new Vector3i(1, 2, 1)),
            new BlockOccupancy(new Vector3i(1, 2, 2)),
            new BlockOccupancy(new Vector3i(1, 2, 3)),
            new BlockOccupancy(new Vector3i(1, 2, 4)),
            new BlockOccupancy(new Vector3i(1, 3, 0)),
            new BlockOccupancy(new Vector3i(1, 3, 1)),
            new BlockOccupancy(new Vector3i(1, 3, 2)),
            new BlockOccupancy(new Vector3i(1, 3, 3)),
            new BlockOccupancy(new Vector3i(1, 3, 4)),
            new BlockOccupancy(new Vector3i(1, 4, 0)),
            new BlockOccupancy(new Vector3i(1, 4, 1)),
            new BlockOccupancy(new Vector3i(1, 4, 2)),
            new BlockOccupancy(new Vector3i(1, 4, 3)),
            new BlockOccupancy(new Vector3i(1, 4, 4)),
            new BlockOccupancy(new Vector3i(1, 5, 0)),
            new BlockOccupancy(new Vector3i(1, 5, 1)),
            new BlockOccupancy(new Vector3i(1, 5, 2)),
            new BlockOccupancy(new Vector3i(1, 5, 3)),
            new BlockOccupancy(new Vector3i(1, 5, 4)),
            new BlockOccupancy(new Vector3i(2, 0, 0)),
            new BlockOccupancy(new Vector3i(2, 0, 1)),
            new BlockOccupancy(new Vector3i(2, 0, 2)),
            new BlockOccupancy(new Vector3i(2, 0, 3)),
            new BlockOccupancy(new Vector3i(2, 0, 4)),
            new BlockOccupancy(new Vector3i(2, 1, 0)),
            new BlockOccupancy(new Vector3i(2, 1, 1)),
            new BlockOccupancy(new Vector3i(2, 1, 2)),
            new BlockOccupancy(new Vector3i(2, 1, 3)),
            new BlockOccupancy(new Vector3i(2, 1, 4)),
            new BlockOccupancy(new Vector3i(2, 2, 0)),
            new BlockOccupancy(new Vector3i(2, 2, 1)),
            new BlockOccupancy(new Vector3i(2, 2, 2)),
            new BlockOccupancy(new Vector3i(2, 2, 3)),
            new BlockOccupancy(new Vector3i(2, 2, 4)),
            new BlockOccupancy(new Vector3i(2, 3, 0)),
            new BlockOccupancy(new Vector3i(2, 3, 1)),
            new BlockOccupancy(new Vector3i(2, 3, 2)),
            new BlockOccupancy(new Vector3i(2, 3, 3)),
            new BlockOccupancy(new Vector3i(2, 3, 4)),
            new BlockOccupancy(new Vector3i(2, 4, 0)),
            new BlockOccupancy(new Vector3i(2, 4, 1)),
            new BlockOccupancy(new Vector3i(2, 4, 2)),
            new BlockOccupancy(new Vector3i(2, 4, 3)),
            new BlockOccupancy(new Vector3i(2, 4, 4)),
            new BlockOccupancy(new Vector3i(2, 5, 0)),
            new BlockOccupancy(new Vector3i(2, 5, 1)),
            new BlockOccupancy(new Vector3i(2, 5, 2)),
            new BlockOccupancy(new Vector3i(2, 5, 3)),
            new BlockOccupancy(new Vector3i(2, 5, 4)),
            new BlockOccupancy(new Vector3i(3, 0, 0)),
            new BlockOccupancy(new Vector3i(3, 0, 1)),
            new BlockOccupancy(new Vector3i(3, 0, 2)),
            new BlockOccupancy(new Vector3i(3, 0, 3)),
            new BlockOccupancy(new Vector3i(3, 0, 4)),
            new BlockOccupancy(new Vector3i(3, 1, 0)),
            new BlockOccupancy(new Vector3i(3, 1, 1)),
            new BlockOccupancy(new Vector3i(3, 1, 2)),
            new BlockOccupancy(new Vector3i(3, 1, 3)),
            new BlockOccupancy(new Vector3i(3, 1, 4)),
            new BlockOccupancy(new Vector3i(3, 2, 0)),
            new BlockOccupancy(new Vector3i(3, 2, 1)),
            new BlockOccupancy(new Vector3i(3, 2, 2)),
            new BlockOccupancy(new Vector3i(3, 2, 3)),
            new BlockOccupancy(new Vector3i(3, 2, 4)),
            new BlockOccupancy(new Vector3i(3, 3, 0)),
            new BlockOccupancy(new Vector3i(3, 3, 1)),
            new BlockOccupancy(new Vector3i(3, 3, 2)),
            new BlockOccupancy(new Vector3i(3, 3, 3)),
            new BlockOccupancy(new Vector3i(3, 3, 4)),
            new BlockOccupancy(new Vector3i(3, 4, 0)),
            new BlockOccupancy(new Vector3i(3, 4, 1)),
            new BlockOccupancy(new Vector3i(3, 4, 2)),
            new BlockOccupancy(new Vector3i(3, 4, 3)),
            new BlockOccupancy(new Vector3i(3, 4, 4)),
            new BlockOccupancy(new Vector3i(3, 5, 0)),
            new BlockOccupancy(new Vector3i(3, 5, 1)),
            new BlockOccupancy(new Vector3i(3, 5, 2)),
            new BlockOccupancy(new Vector3i(3, 5, 3)),
            new BlockOccupancy(new Vector3i(3, 5, 4)),
            new BlockOccupancy(new Vector3i(4, 0, 0)),
            new BlockOccupancy(new Vector3i(4, 0, 1)),
            new BlockOccupancy(new Vector3i(4, 0, 2)),
            new BlockOccupancy(new Vector3i(4, 0, 3)),
            new BlockOccupancy(new Vector3i(4, 0, 4)),
            new BlockOccupancy(new Vector3i(4, 1, 0)),
            new BlockOccupancy(new Vector3i(4, 1, 1)),
            new BlockOccupancy(new Vector3i(4, 1, 2)),
            new BlockOccupancy(new Vector3i(4, 1, 3)),
            new BlockOccupancy(new Vector3i(4, 1, 4)),
            new BlockOccupancy(new Vector3i(4, 2, 0)),
            new BlockOccupancy(new Vector3i(4, 2, 1)),
            new BlockOccupancy(new Vector3i(4, 2, 2)),
            new BlockOccupancy(new Vector3i(4, 2, 3)),
            new BlockOccupancy(new Vector3i(4, 2, 4)),
            new BlockOccupancy(new Vector3i(4, 3, 0)),
            new BlockOccupancy(new Vector3i(4, 3, 1)),
            new BlockOccupancy(new Vector3i(4, 3, 2)),
            new BlockOccupancy(new Vector3i(4, 3, 3)),
            new BlockOccupancy(new Vector3i(4, 3, 4)),
            new BlockOccupancy(new Vector3i(4, 4, 0)),
            new BlockOccupancy(new Vector3i(4, 4, 1)),
            new BlockOccupancy(new Vector3i(4, 4, 2)),
            new BlockOccupancy(new Vector3i(4, 4, 3)),
            new BlockOccupancy(new Vector3i(4, 4, 4)),
            new BlockOccupancy(new Vector3i(4, 5, 0)),
            new BlockOccupancy(new Vector3i(4, 5, 1)),
            new BlockOccupancy(new Vector3i(4, 5, 2)),
            new BlockOccupancy(new Vector3i(4, 5, 3)),
            new BlockOccupancy(new Vector3i(4, 5, 4)),
            };

            AddOccupancy<SiloPetroleObject>(BlockOccupancyList);




        }


        protected override void Initialize()
        {
            this.ModsPreInitialize();
            PublicStorageComponent component = base.GetComponent<PublicStorageComponent>(null);
            this.GetComponent<LinkComponent>().Initialize(12);
            component.Initialize(15); // Le nombre d'emplacement "15" autorisé dans le stockage.
            component.Storage.AddInvRestriction(new Inventoryx2Multiply()); // Multiplicateur de stacks
            component.Inventory.AddInvRestriction(new TagRestriction(new string[] // Les tags autorisées à être utilisées dans le stockage.
            {
                "Liquid Fuel",
            }));
            this.ModsPostInitialize();
        }


        partial void ModsPreInitialize();

        partial void ModsPostInitialize();
    }

    [Serialized]
    [LocDisplayName("Silo raffinerie de pétrole")]
    [LocDescription("Un espace de stockage sécurisé conçu pour accueillir efficacement les barils de pétrole.")]
    [Ecopedia("Crafted Objects", "Storage", createAsSubPage: true)]
    [Weight(1000)]
    [MaxStackSize(10)]
    public partial class SiloPetroleItem : WorldObjectItem<SiloPetroleObject>
    {
        protected override OccupancyContext GetOccupancyContext => new SideAttachedContext(0 | DirectionAxisFlags.Down, WorldObject.GetOccupancyInfo(this.WorldObjectType));

    }

    [RequiresSkill(typeof(MechanicsSkill), 1)]
    public partial class SiloPetroleRecipe : RecipeFamily
    {
        public SiloPetroleRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "Silo raffinerie de pétrole",  //noloc
                displayName: Localizer.DoStr("Silo raffinerie de pétrole"),

                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(IronPlateItem), 50, typeof(MechanicsSkill), typeof(MechanicsLavishResourcesTalent)), // Customizable recipe: "50" Iron Plates.
                    new IngredientElement(typeof(ScrewsItem), 25, typeof(MechanicsSkill), typeof(MechanicsLavishResourcesTalent)), // Customizable recipe: "25" Screws.
                    new IngredientElement(typeof(IronBarItem), 15, typeof(MechanicsSkill), typeof(MechanicsLavishResourcesTalent)), // Customizable recipe: "15" Iron Ingots.
                },

                items: new List<CraftingElement>
                {
                    new CraftingElement<SiloPetroleItem>()
                });
            this.Recipes = new List<Recipe> { recipe };
            this.ExperienceOnCraft = 25;

            this.LaborInCalories = CreateLaborInCaloriesValue(250, typeof(MechanicsSkill)); // Quantité de calories "250" consommées pour la fabrication.

            this.CraftMinutes = CreateCraftTimeValue(beneficiary: typeof(SiloPetroleRecipe), start: 10, skillType: typeof(MechanicsSkill), typeof(MechanicsFocusedSpeedTalent), typeof(MechanicsParallelSpeedTalent)); // Durée de fabrication de l'objet : 10 minutes.

            this.ModsPreInitialize();
            this.Initialize(displayText: Localizer.DoStr("Silo raffinerie de pétrole"), recipeType: typeof(SiloPetroleRecipe));
            this.ModsPostInitialize();

            CraftingComponent.AddRecipe(tableType: typeof(MachinistTableObject), recipeFamily: this);
        }

        partial void ModsPreInitialize();

        partial void ModsPostInitialize();
    }
    #endregion



    #region Poubelle Verte
    // Poubelle


    [Serialized]
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(LinkComponent))]
    [RequireComponent(typeof(OccupancyRequirementComponent))]
    [RequireComponent(typeof(ForSaleComponent))]
    [RequireComponent(typeof(PublicStorageComponent))]
    [Tag("Usable")]
    [Ecopedia("Crafted Objects", "Storage", subPageName: "Poubelle verte")]
    public partial class PoubelleObject : WorldObject, IRepresentsItem
    {
        public virtual Type RepresentedItemType => typeof(PoubelleItem);
        public override LocString DisplayName => Localizer.DoStr("Poubelle verte");
        public override TableTextureMode TableTexture => TableTextureMode.Wood;
        static PoubelleObject()

        {
            var BlockOccupancyList = new List<BlockOccupancy>
            {
            new BlockOccupancy(new Vector3i(0, 0, 0)),
            new BlockOccupancy(new Vector3i(0, 1, 0)),
            };

            AddOccupancy<PoubelleObject>(BlockOccupancyList);




        }


        protected override void Initialize()
        {
            this.ModsPreInitialize();
            PublicStorageComponent component = base.GetComponent<PublicStorageComponent>(null);
            this.GetComponent<LinkComponent>().Initialize(12);
            component.Initialize(6); // Le nombre d'emplacement "5" autorisé dans le stockage.
            component.Storage.AddInvRestriction(new NotCarriedRestriction());
            this.ModsPostInitialize();
        }


        partial void ModsPreInitialize();

        partial void ModsPostInitialize();
    }

    [Serialized]
    [LocDisplayName("Poubelle verte")]
    [LocDescription("Le dernier refuge des objets oubliés, un endroit écologique pour vos vieux items.")]
    [Ecopedia("Crafted Objects", "Storage", createAsSubPage: true)]
    [Weight(1000)]
    [MaxStackSize(10)]
    public partial class PoubelleItem : WorldObjectItem<PoubelleObject>
    {
        protected override OccupancyContext GetOccupancyContext => new SideAttachedContext(0 | DirectionAxisFlags.Down, WorldObject.GetOccupancyInfo(this.WorldObjectType));

    }

    public partial class PoubelleRecipe : RecipeFamily
    {
        public PoubelleRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "Poubelle verte",  
                displayName: Localizer.DoStr("Poubelle verte"),

                ingredients: new List<IngredientElement>
                {
                    new IngredientElement("Wood", 10,typeof(Skill)), 
                },

                items: new List<CraftingElement>
                {
                    new CraftingElement<PoubelleItem>()
                });
            this.Recipes = new List<Recipe> { recipe };

            this.LaborInCalories = CreateLaborInCaloriesValue(30);

            this.CraftMinutes = CreateCraftTimeValue(0.5f);

            this.ModsPreInitialize();
            this.Initialize(displayText: Localizer.DoStr("Poubelle verte"), recipeType: typeof(PoubelleRecipe));
            this.ModsPostInitialize();

            CraftingComponent.AddRecipe(tableType: typeof(WorkbenchObject), recipeFamily: this);
        }

        partial void ModsPreInitialize();
        partial void ModsPostInitialize();
    }

    #endregion



    #region Caisse a outils
    // Caisse à outils


    [Serialized]
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(LinkComponent))]
    [RequireComponent(typeof(OccupancyRequirementComponent))]
    [RequireComponent(typeof(ForSaleComponent))]
    [RequireComponent(typeof(PublicStorageComponent))]
    [Tag("Usable")]
    [Ecopedia("Crafted Objects", "Storage", subPageName: "Caisse à outils")]
    public partial class CaisseOutilsObject : WorldObject, IRepresentsItem
    {
        public virtual Type RepresentedItemType => typeof(CaisseOutilsItem);
        public override LocString DisplayName => Localizer.DoStr("Caisse à outils");
        public override TableTextureMode TableTexture => TableTextureMode.Wood;

        protected override void Initialize()
        {
            this.ModsPreInitialize();
            PublicStorageComponent component = base.GetComponent<PublicStorageComponent>(null);
            this.GetComponent<LinkComponent>().Initialize(12);
            component.Initialize(36); // Le nombre d'emplacement "36" autorisé dans le stockage.
            component.Storage.AddInvRestriction(new Inventoryx2Multiply()); // Multiplicateur de stacks
            component.Storage.AddInvRestriction(new TagRestriction(new string[] // Les tags autorisées à être utilisées dans le stockage.
            {
                "Tool",
            }));
            this.ModsPostInitialize();
        }


        partial void ModsPreInitialize();

        partial void ModsPostInitialize();
    }

    [Serialized]
    [LocDisplayName("Caisse à outils")]
    [LocDescription("Le coffre-fort des bricoleurs ! Rassemblez tous vos outils ici, car on sait tous que le marteau a tendance à disparaître au bon moment !")]
    [Ecopedia("Crafted Objects", "Storage", createAsSubPage: true)]
    [Weight(1000)]
    [MaxStackSize(10)]
    public partial class CaisseOutilsItem : WorldObjectItem<CaisseOutilsObject>
    {
        protected override OccupancyContext GetOccupancyContext => new SideAttachedContext(0 | DirectionAxisFlags.Down, WorldObject.GetOccupancyInfo(this.WorldObjectType));

    }

    [RequiresSkill(typeof(MechanicsSkill), 1)]
    public partial class CaisseOutilsRecipe : RecipeFamily
    {
        public CaisseOutilsRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "Caisse à outils",  //noloc
                displayName: Localizer.DoStr("Caisse à outils"),

                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(NailItem), 15, typeof(MechanicsSkill), typeof(MechanicsLavishResourcesTalent)), // Recette personnalisable : "15" Clous.
                    new IngredientElement(typeof(IronBarItem), 30, typeof(MechanicsSkill), typeof(MechanicsLavishResourcesTalent)), // Recette personnalisable : "30" Lingot de fer.
                },

                items: new List<CraftingElement>
                {
                    new CraftingElement<CaisseOutilsItem>()
                });
            this.Recipes = new List<Recipe> { recipe };
            this.ExperienceOnCraft = 25;

            this.LaborInCalories = CreateLaborInCaloriesValue(250, typeof(MechanicsSkill)); // Quantité de calories "250" consommées pour la fabrication.

            this.CraftMinutes = CreateCraftTimeValue(beneficiary: typeof(CaisseOutilsRecipe), start: 5, skillType: typeof(MechanicsSkill), typeof(MechanicsFocusedSpeedTalent), typeof(MechanicsParallelSpeedTalent)); // Durée de fabrication de l'objet : 5 minutes.

            this.ModsPreInitialize();
            this.Initialize(displayText: Localizer.DoStr("Caisse à outils"), recipeType: typeof(CaisseOutilsRecipe));
            this.ModsPostInitialize();

            CraftingComponent.AddRecipe(tableType: typeof(MachinistTableObject), recipeFamily: this);
        }

        partial void ModsPreInitialize();

        partial void ModsPostInitialize();
    }
    #endregion



    #region Benne à gravat
    // Benne à gravat

    [Serialized]
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(LinkComponent))]
    [RequireComponent(typeof(OccupancyRequirementComponent))]
    [RequireComponent(typeof(ForSaleComponent))]
    [RequireComponent(typeof(ModularStockpileComponent))]
    [RequireComponent(typeof(PublicStorageComponent))]
    [Tag("Usable")]
    [Ecopedia("Crafted Objects", "Storage", subPageName: "Benne à gravat")]
    public partial class BenneGravatObject : WorldObject, IRepresentsItem
    {
        public virtual Type RepresentedItemType => typeof(BenneGravatItem);
        public override LocString DisplayName => Localizer.DoStr("Benne à gravat");
        public override TableTextureMode TableTexture => TableTextureMode.Metal;
        static BenneGravatObject()

        {
            var BlockOccupancyList = new List<BlockOccupancy>
            {
            new BlockOccupancy(new Vector3i(0, 0, 0)),
            new BlockOccupancy(new Vector3i(0, 0, 1)),
            new BlockOccupancy(new Vector3i(0, 0, 2)),
            new BlockOccupancy(new Vector3i(0, 0, 3)),
            new BlockOccupancy(new Vector3i(0, 0, 4)),
            new BlockOccupancy(new Vector3i(0, 0, 5)),
            new BlockOccupancy(new Vector3i(0, 1, 0)),
            new BlockOccupancy(new Vector3i(0, 1, 1)),
            new BlockOccupancy(new Vector3i(0, 1, 2)),
            new BlockOccupancy(new Vector3i(0, 1, 3)),
            new BlockOccupancy(new Vector3i(0, 1, 4)),
            new BlockOccupancy(new Vector3i(0, 1, 5)),
            new BlockOccupancy(new Vector3i(0, 2, 0)),
            new BlockOccupancy(new Vector3i(0, 2, 1)),
            new BlockOccupancy(new Vector3i(0, 2, 2)),
            new BlockOccupancy(new Vector3i(0, 2, 3)),
            new BlockOccupancy(new Vector3i(0, 2, 4)),
            new BlockOccupancy(new Vector3i(0, 2, 5)),
            new BlockOccupancy(new Vector3i(0, 3, 0)),
            new BlockOccupancy(new Vector3i(0, 3, 1)),
            new BlockOccupancy(new Vector3i(0, 3, 2)),
            new BlockOccupancy(new Vector3i(0, 3, 3)),
            new BlockOccupancy(new Vector3i(0, 3, 4)),
            new BlockOccupancy(new Vector3i(0, 3, 5)),
            new BlockOccupancy(new Vector3i(1, 0, 0)),
            new BlockOccupancy(new Vector3i(1, 0, 1)),
            new BlockOccupancy(new Vector3i(1, 0, 2)),
            new BlockOccupancy(new Vector3i(1, 0, 3)),
            new BlockOccupancy(new Vector3i(1, 0, 4)),
            new BlockOccupancy(new Vector3i(1, 0, 5)),
            new BlockOccupancy(new Vector3i(1, 1, 0)),
            new BlockOccupancy(new Vector3i(1, 1, 1)),
            new BlockOccupancy(new Vector3i(1, 1, 2)),
            new BlockOccupancy(new Vector3i(1, 1, 3)),
            new BlockOccupancy(new Vector3i(1, 1, 4)),
            new BlockOccupancy(new Vector3i(1, 1, 5)),
            new BlockOccupancy(new Vector3i(1, 2, 0)),
            new BlockOccupancy(new Vector3i(1, 2, 1)),
            new BlockOccupancy(new Vector3i(1, 2, 2)),
            new BlockOccupancy(new Vector3i(1, 2, 3)),
            new BlockOccupancy(new Vector3i(1, 2, 4)),
            new BlockOccupancy(new Vector3i(1, 2, 5)),
            new BlockOccupancy(new Vector3i(1, 3, 0)),
            new BlockOccupancy(new Vector3i(1, 3, 1)),
            new BlockOccupancy(new Vector3i(1, 3, 2)),
            new BlockOccupancy(new Vector3i(1, 3, 3)),
            new BlockOccupancy(new Vector3i(1, 3, 4)),
            new BlockOccupancy(new Vector3i(1, 3, 5)),
            new BlockOccupancy(new Vector3i(2, 0, 0)),
            new BlockOccupancy(new Vector3i(2, 0, 1)),
            new BlockOccupancy(new Vector3i(2, 0, 2)),
            new BlockOccupancy(new Vector3i(2, 0, 3)),
            new BlockOccupancy(new Vector3i(2, 0, 4)),
            new BlockOccupancy(new Vector3i(2, 0, 5)),
            new BlockOccupancy(new Vector3i(2, 1, 0)),
            new BlockOccupancy(new Vector3i(2, 1, 1)),
            new BlockOccupancy(new Vector3i(2, 1, 2)),
            new BlockOccupancy(new Vector3i(2, 1, 3)),
            new BlockOccupancy(new Vector3i(2, 1, 4)),
            new BlockOccupancy(new Vector3i(2, 1, 5)),
            new BlockOccupancy(new Vector3i(2, 2, 0)),
            new BlockOccupancy(new Vector3i(2, 2, 1)),
            new BlockOccupancy(new Vector3i(2, 2, 2)),
            new BlockOccupancy(new Vector3i(2, 2, 3)),
            new BlockOccupancy(new Vector3i(2, 2, 4)),
            new BlockOccupancy(new Vector3i(2, 2, 5)),
            new BlockOccupancy(new Vector3i(2, 3, 0)),
            new BlockOccupancy(new Vector3i(2, 3, 1)),
            new BlockOccupancy(new Vector3i(2, 3, 2)),
            new BlockOccupancy(new Vector3i(2, 3, 3)),
            new BlockOccupancy(new Vector3i(2, 3, 4)),
            new BlockOccupancy(new Vector3i(2, 3, 5)),
            new BlockOccupancy(new Vector3i(3, 0, 0)),
            new BlockOccupancy(new Vector3i(3, 0, 1)),
            new BlockOccupancy(new Vector3i(3, 0, 2)),
            new BlockOccupancy(new Vector3i(3, 0, 3)),
            new BlockOccupancy(new Vector3i(3, 0, 4)),
            new BlockOccupancy(new Vector3i(3, 0, 5)),
            new BlockOccupancy(new Vector3i(3, 1, 0)),
            new BlockOccupancy(new Vector3i(3, 1, 1)),
            new BlockOccupancy(new Vector3i(3, 1, 2)),
            new BlockOccupancy(new Vector3i(3, 1, 3)),
            new BlockOccupancy(new Vector3i(3, 1, 4)),
            new BlockOccupancy(new Vector3i(3, 1, 5)),
            new BlockOccupancy(new Vector3i(3, 2, 0)),
            new BlockOccupancy(new Vector3i(3, 2, 1)),
            new BlockOccupancy(new Vector3i(3, 2, 2)),
            new BlockOccupancy(new Vector3i(3, 2, 3)),
            new BlockOccupancy(new Vector3i(3, 2, 4)),
            new BlockOccupancy(new Vector3i(3, 2, 5)),
            new BlockOccupancy(new Vector3i(3, 3, 0)),
            new BlockOccupancy(new Vector3i(3, 3, 1)),
            new BlockOccupancy(new Vector3i(3, 3, 2)),
            new BlockOccupancy(new Vector3i(3, 3, 3)),
            new BlockOccupancy(new Vector3i(3, 3, 4)),
            new BlockOccupancy(new Vector3i(3, 3, 5)),
            };

            AddOccupancy<BenneGravatObject>(BlockOccupancyList);




        }




        protected override void Initialize()
        {
            this.ModsPreInitialize();
            this.GetComponent<StockpileComponent>().Initialize(new Vector3i(3, 3, 4));
            PublicStorageComponent component = base.GetComponent<PublicStorageComponent>(null);
            this.GetComponent<LinkComponent>().Initialize(12);
            component.Initialize(16);
            component.Storage.AddInvRestriction(new Inventoryx4Multiply()); // Multiplicateur de stacks
            component.Storage.AddInvRestriction(new TagRestriction(new string[] // Les tags autorisées à être utilisées dans le stockage.
            {
                "Silica",
                "Excavatable",
                "CrushedRock",
            }));
            this.ModsPostInitialize();
        }


        partial void ModsPreInitialize();

        partial void ModsPostInitialize();
    }

    [Serialized]
    [LocDisplayName("Benne à gravat")]
    [LocDescription("Un conteneur robuste pour stocker efficacement pierres, sable et terre. Idéal pour garder votre espace bien organisé !")]
    [Ecopedia("Crafted Objects", "Storage", createAsSubPage: true)]
    [Weight(1000)]
    [MaxStackSize(10)]
    public partial class BenneGravatItem : WorldObjectItem<BenneGravatObject>
    {
        protected override OccupancyContext GetOccupancyContext => new SideAttachedContext(0 | DirectionAxisFlags.Down, WorldObject.GetOccupancyInfo(this.WorldObjectType));

    }

    [RequiresSkill(typeof(MechanicsSkill), 5)]
    public partial class BenneGravatRecipe : RecipeFamily
    {
        public BenneGravatRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "Benne à gravat",  //noloc
                displayName: Localizer.DoStr("Benne à gravat"),

                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(IronPlateItem), 50, typeof(MechanicsSkill), typeof(MechanicsLavishResourcesTalent)), // Customizable recipe: "50" Iron Plates.
                    new IngredientElement(typeof(ScrewsItem), 10, typeof(MechanicsSkill), typeof(MechanicsLavishResourcesTalent)), // Customizable recipe: "10" Screws.
                    new IngredientElement(typeof(IronBarItem), 20, typeof(MechanicsSkill), typeof(MechanicsLavishResourcesTalent)), // Customizable recipe: "20" Iron Ingots.
                },

                items: new List<CraftingElement>
                {
                    new CraftingElement<BenneGravatItem>()
                });
            this.Recipes = new List<Recipe> { recipe };
            this.ExperienceOnCraft = 25;

            this.LaborInCalories = CreateLaborInCaloriesValue(400, typeof(MechanicsSkill)); // Amount of calories consumed for crafting: "250".

            this.CraftMinutes = CreateCraftTimeValue(beneficiary: typeof(BenneGravatRecipe), start: 5, skillType: typeof(MechanicsSkill), typeof(MechanicsFocusedSpeedTalent), typeof(MechanicsParallelSpeedTalent)); // Durée de fabrication de l'objet : 5 minutes.

            this.ModsPreInitialize();
            this.Initialize(displayText: Localizer.DoStr("Benne à gravat"), recipeType: typeof(BenneGravatRecipe));
            this.ModsPostInitialize();

            CraftingComponent.AddRecipe(tableType: typeof(MachinistTableObject), recipeFamily: this);
        }
        partial void ModsPreInitialize();
        partial void ModsPostInitialize();
    }
    #endregion



    #region Dressing
    // Dressing


    [Serialized]
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(LinkComponent))]
    [RequireComponent(typeof(OccupancyRequirementComponent))]
    [RequireComponent(typeof(ForSaleComponent))]
    [RequireComponent(typeof(PublicStorageComponent))]
    [Tag("Usable")]
    [Ecopedia("Crafted Objects", "Storage", subPageName: "Dressing")]
    public partial class DressingObject : WorldObject, IRepresentsItem
    {
        public virtual Type RepresentedItemType => typeof(DressingItem);
        public override LocString DisplayName => Localizer.DoStr("Dressing");
        public override TableTextureMode TableTexture => TableTextureMode.Wood;


        static DressingObject()

        {
            var BlockOccupancyList = new List<BlockOccupancy>
            {
            new BlockOccupancy(new Vector3i(0, 0, 0)),
            new BlockOccupancy(new Vector3i(0, 1, 0)),
            new BlockOccupancy(new Vector3i(1, 0, 0)),
            new BlockOccupancy(new Vector3i(1, 1, 0)),
            };

            AddOccupancy<DressingObject>(BlockOccupancyList);




        }


        protected override void Initialize()
        {
            this.ModsPreInitialize();
            PublicStorageComponent component = base.GetComponent<PublicStorageComponent>(null);
            this.GetComponent<LinkComponent>().Initialize(12);
            component.Initialize(20); // Le nombre d'emplacement "20" autorisé dans le stockage.
            component.Storage.AddInvRestriction(new Inventoryx2Multiply()); // Multiplicateur de stacks
            component.Storage.AddInvRestriction(new TagRestriction(new string[] // Les tags autorisées à être utilisées dans le stockage.
            {
                "Clothes",
            }));
            this.ModsPostInitialize();
        }


        partial void ModsPreInitialize();

        partial void ModsPostInitialize();
    }

    [Serialized]
    [LocDisplayName("Dressing")]
    [LocDescription("Un espace de rangement pratique pour organiser tous vos vêtements du jeu. Gardez vos tenues prêtes pour chaque occasion !")]
    [Ecopedia("Crafted Objects", "Storage", createAsSubPage: true)]
    [Weight(1000)]
    [MaxStackSize(10)]
    public partial class DressingItem : WorldObjectItem<DressingObject>
    {
        protected override OccupancyContext GetOccupancyContext => new SideAttachedContext(0 | DirectionAxisFlags.Down, WorldObject.GetOccupancyInfo(this.WorldObjectType));

    }


    [RequiresSkill(typeof(CarpentrySkill), 1)]
    public partial class DressingRecipe : RecipeFamily
    {
        public DressingRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "Dressing",
                displayName: Localizer.DoStr("Dressing"),

                ingredients: new List<IngredientElement>
                {
                    new IngredientElement("WoodBoard", 40, typeof(CarpentrySkill), typeof(CarpentryLavishResourcesTalent)), // Recette personnalisable : "40" Planches.
                },


                items: new List<CraftingElement>
                {
                    new CraftingElement<DressingItem>()
                });
            this.Recipes = new List<Recipe> { recipe };
            this.ExperienceOnCraft = 2;

            this.LaborInCalories = CreateLaborInCaloriesValue(250, typeof(CarpentrySkill)); // Quantité de calories "250" consommées pour la fabrication.
            this.CraftMinutes = CreateCraftTimeValue(beneficiary: typeof(DressingRecipe), start: 3, skillType: typeof(CarpentrySkill), typeof(CarpentryFocusedSpeedTalent), typeof(CarpentryParallelSpeedTalent)); // Durée de fabrication de l'objet : 2 minutes.

            this.ModsPreInitialize();
            this.Initialize(displayText: Localizer.DoStr("Dressing"), recipeType: typeof(DressingRecipe));
            this.ModsPostInitialize();

            CraftingComponent.AddRecipe(tableType: typeof(CarpentryTableObject), recipeFamily: this);
        }

        partial void ModsPreInitialize();

        partial void ModsPostInitialize();
    }
    #endregion



    #region Barile de graine
    // Barile de graine
    [Serialized]
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(LinkComponent))]
    [RequireComponent(typeof(HousingComponent))]
    [RequireComponent(typeof(PublicStorageComponent))]
    [RequireComponent(typeof(OccupancyRequirementComponent))]
    [RequireComponent(typeof(ForSaleComponent))]
    [RequireComponent(typeof(RoomRequirementsComponent))]
    [RequireComponent(typeof(PaintableComponent))]
    [Tag("Usable")]
    [Ecopedia("Housing Objects", "Kitchen", subPageName: "Barile de graine")]
    public partial class SeedbarrelObject : WorldObject, IRepresentsItem
    {
        public virtual Type RepresentedItemType => typeof(SeedbarrelItem);
        public override LocString DisplayName => Localizer.DoStr("Barile de graine");
        public override TableTextureMode TableTexture => TableTextureMode.Wood;

        protected override void Initialize()
        {
            this.ModsPreInitialize();
            this.GetComponent<HousingComponent>().HomeValue = SeedbarrelItem.homeValue;
            var storage = this.GetComponent<PublicStorageComponent>();
            storage.Initialize(16);
            storage.Storage.AddInvRestriction(new StackLimitRestriction(200));
            storage.Storage.AddInvRestriction(new NotCarriedRestriction()); 
            storage.ShelfLifeMultiplier = 1.6f;
            storage.Inventory.AddInvRestriction(new TagRestriction(new string[] // Les tags autorisées à être utilisées dans le stockage.
            {
                "Seeds",
            }));
            this.ModsPostInitialize();
        }

        partial void ModsPreInitialize();
        partial void ModsPostInitialize();
    }

    [Serialized]
    [LocDisplayName("Barile de graine")]
    [LocDescription("Un conteneur pratique pour stocker et organiser toutes vos graines, prêt à soutenir vos futures récoltes !")]
    [Ecopedia("Housing Objects", "Kitchen", createAsSubPage: true)]
    [Tag("Housing")]
    [Weight(1000)]
    [Tag(nameof(SurfaceTags.CanBeOnSurface))]
    public partial class SeedbarrelItem : WorldObjectItem<SeedbarrelObject>
    {
        [NewTooltip(CacheAs.SubType, 50)] public static LocString UpdateTooltip() => Localizer.Do($"{Localizer.DoStr("Increases")} total shelf life by: {Text.InfoLight(Text.Percent(0.6f))}").Dash();
        protected override OccupancyContext GetOccupancyContext => new SideAttachedContext(0 | DirectionAxisFlags.Down, WorldObject.GetOccupancyInfo(this.WorldObjectType));
        public override HomeFurnishingValue HomeValue => homeValue;
        public static readonly HomeFurnishingValue homeValue = new HomeFurnishingValue()
        {
            ObjectName = typeof(SeedbarrelObject).UILink(),
            Category = HousingConfig.GetRoomCategory("Kitchen"),
            BaseValue = 3,
            TypeForRoomLimit = Localizer.DoStr("Food Storage"),
            DiminishingReturnMultiplier = 0.5f

        };

    }

    [RequiresSkill(typeof(CarpentrySkill), 1)]
    [Ecopedia("Housing Objects", "Kitchen", subPageName: "Barile de graine")]
    public partial class SeedbarrelRecipe : RecipeFamily
    {
        public SeedbarrelRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "Barile de graine",
                displayName: Localizer.DoStr("Barile de graine"),

                ingredients: new List<IngredientElement>
                {
                    new IngredientElement("HewnLog", 5, typeof(CarpentrySkill), typeof(CarpentryLavishResourcesTalent)),
                    new IngredientElement("WoodBoard", 10, typeof(CarpentrySkill), typeof(CarpentryLavishResourcesTalent)),
                },

                items: new List<CraftingElement>
                {
                    new CraftingElement<SeedbarrelItem>()
                });
            this.Recipes = new List<Recipe> { recipe };
            this.ExperienceOnCraft = 3;

            this.LaborInCalories = CreateLaborInCaloriesValue(60, typeof(CarpentrySkill));

            this.CraftMinutes = CreateCraftTimeValue(beneficiary: typeof(SeedbarrelRecipe), start: 2, skillType: typeof(CarpentrySkill), typeof(CarpentryFocusedSpeedTalent), typeof(CarpentryParallelSpeedTalent));

            this.ModsPreInitialize();
            this.Initialize(displayText: Localizer.DoStr("Barile de graine"), recipeType: typeof(SeedbarrelRecipe));
            this.ModsPostInitialize();

            CraftingComponent.AddRecipe(tableType: typeof(CarpentryTableObject), recipeFamily: this);
        }

        partial void ModsPreInitialize();
        partial void ModsPostInitialize();
    }
    #endregion



    #region Conteneur de déchets polluants
    // Shipping_dechet

    [Serialized]
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(LinkComponent))]
    [RequireComponent(typeof(PublicStorageComponent))]
    [RequireComponent(typeof(ForSaleComponent))]
    [Tag("Usable")]
    [Ecopedia("Crafted Objects", "Storage", subPageName: "Conteneur de déchets polluants")]
    public partial class Shipping_dechetObject : WorldObject, IRepresentsItem
    {
        public virtual Type RepresentedItemType => typeof(Shipping_dechetItem);
        public override LocString DisplayName => Localizer.DoStr("Conteneur de déchets polluants");
        public override TableTextureMode TableTexture => TableTextureMode.Metal;

        static Shipping_dechetObject()

        {
            var BlockOccupancyList = new List<BlockOccupancy>
            {
            new BlockOccupancy(new Vector3i(0, 0, 0)),
            new BlockOccupancy(new Vector3i(0, 0, 1)),
            new BlockOccupancy(new Vector3i(0, 0, 2)),
            new BlockOccupancy(new Vector3i(0, 0, 3)),
            new BlockOccupancy(new Vector3i(0, 0, 4)),
            new BlockOccupancy(new Vector3i(0, 0, 5)),
            new BlockOccupancy(new Vector3i(0, 0, 6)),
            new BlockOccupancy(new Vector3i(0, 0, 7)),
            new BlockOccupancy(new Vector3i(0, 0, 8)),
            new BlockOccupancy(new Vector3i(0, 0, 9)),
            new BlockOccupancy(new Vector3i(0, 0, 10)),
            new BlockOccupancy(new Vector3i(0, 0, 11)),
            new BlockOccupancy(new Vector3i(0, 0, 12)),
            new BlockOccupancy(new Vector3i(0, 0, 13)),
            new BlockOccupancy(new Vector3i(0, 0, 14)),
            new BlockOccupancy(new Vector3i(0, 0, 15)),
            new BlockOccupancy(new Vector3i(0, 0, 16)),
            new BlockOccupancy(new Vector3i(0, 0, 17)),
            new BlockOccupancy(new Vector3i(0, 0, 18)),
            new BlockOccupancy(new Vector3i(0, 0, 19)),
            new BlockOccupancy(new Vector3i(0, 0, 20)),
            new BlockOccupancy(new Vector3i(0, 0, 21)),
            new BlockOccupancy(new Vector3i(0, 1, 0)),
            new BlockOccupancy(new Vector3i(0, 1, 1)),
            new BlockOccupancy(new Vector3i(0, 1, 2)),
            new BlockOccupancy(new Vector3i(0, 1, 3)),
            new BlockOccupancy(new Vector3i(0, 1, 4)),
            new BlockOccupancy(new Vector3i(0, 1, 5)),
            new BlockOccupancy(new Vector3i(0, 1, 6)),
            new BlockOccupancy(new Vector3i(0, 1, 7)),
            new BlockOccupancy(new Vector3i(0, 1, 8)),
            new BlockOccupancy(new Vector3i(0, 1, 9)),
            new BlockOccupancy(new Vector3i(0, 1, 10)),
            new BlockOccupancy(new Vector3i(0, 1, 11)),
            new BlockOccupancy(new Vector3i(0, 1, 12)),
            new BlockOccupancy(new Vector3i(0, 1, 13)),
            new BlockOccupancy(new Vector3i(0, 1, 14)),
            new BlockOccupancy(new Vector3i(0, 1, 15)),
            new BlockOccupancy(new Vector3i(0, 1, 16)),
            new BlockOccupancy(new Vector3i(0, 1, 17)),
            new BlockOccupancy(new Vector3i(0, 1, 18)),
            new BlockOccupancy(new Vector3i(0, 1, 19)),
            new BlockOccupancy(new Vector3i(0, 1, 20)),
            new BlockOccupancy(new Vector3i(0, 1, 21)),
            new BlockOccupancy(new Vector3i(0, 2, 0)),
            new BlockOccupancy(new Vector3i(0, 2, 1)),
            new BlockOccupancy(new Vector3i(0, 2, 2)),
            new BlockOccupancy(new Vector3i(0, 2, 3)),
            new BlockOccupancy(new Vector3i(0, 2, 4)),
            new BlockOccupancy(new Vector3i(0, 2, 5)),
            new BlockOccupancy(new Vector3i(0, 2, 6)),
            new BlockOccupancy(new Vector3i(0, 2, 7)),
            new BlockOccupancy(new Vector3i(0, 2, 8)),
            new BlockOccupancy(new Vector3i(0, 2, 9)),
            new BlockOccupancy(new Vector3i(0, 2, 10)),
            new BlockOccupancy(new Vector3i(0, 2, 11)),
            new BlockOccupancy(new Vector3i(0, 2, 12)),
            new BlockOccupancy(new Vector3i(0, 2, 13)),
            new BlockOccupancy(new Vector3i(0, 2, 14)),
            new BlockOccupancy(new Vector3i(0, 2, 15)),
            new BlockOccupancy(new Vector3i(0, 2, 16)),
            new BlockOccupancy(new Vector3i(0, 2, 17)),
            new BlockOccupancy(new Vector3i(0, 2, 18)),
            new BlockOccupancy(new Vector3i(0, 2, 19)),
            new BlockOccupancy(new Vector3i(0, 2, 20)),
            new BlockOccupancy(new Vector3i(0, 2, 21)),
            new BlockOccupancy(new Vector3i(0, 3, 0)),
            new BlockOccupancy(new Vector3i(0, 3, 1)),
            new BlockOccupancy(new Vector3i(0, 3, 2)),
            new BlockOccupancy(new Vector3i(0, 3, 3)),
            new BlockOccupancy(new Vector3i(0, 3, 4)),
            new BlockOccupancy(new Vector3i(0, 3, 5)),
            new BlockOccupancy(new Vector3i(0, 3, 6)),
            new BlockOccupancy(new Vector3i(0, 3, 7)),
            new BlockOccupancy(new Vector3i(0, 3, 8)),
            new BlockOccupancy(new Vector3i(0, 3, 9)),
            new BlockOccupancy(new Vector3i(0, 3, 10)),
            new BlockOccupancy(new Vector3i(0, 3, 11)),
            new BlockOccupancy(new Vector3i(0, 3, 12)),
            new BlockOccupancy(new Vector3i(0, 3, 13)),
            new BlockOccupancy(new Vector3i(0, 3, 14)),
            new BlockOccupancy(new Vector3i(0, 3, 15)),
            new BlockOccupancy(new Vector3i(0, 3, 16)),
            new BlockOccupancy(new Vector3i(0, 3, 17)),
            new BlockOccupancy(new Vector3i(0, 3, 18)),
            new BlockOccupancy(new Vector3i(0, 3, 19)),
            new BlockOccupancy(new Vector3i(0, 3, 20)),
            new BlockOccupancy(new Vector3i(0, 3, 21)),
            new BlockOccupancy(new Vector3i(0, 4, 0)),
            new BlockOccupancy(new Vector3i(0, 4, 1)),
            new BlockOccupancy(new Vector3i(0, 4, 2)),
            new BlockOccupancy(new Vector3i(0, 4, 3)),
            new BlockOccupancy(new Vector3i(0, 4, 4)),
            new BlockOccupancy(new Vector3i(0, 4, 5)),
            new BlockOccupancy(new Vector3i(0, 4, 6)),
            new BlockOccupancy(new Vector3i(0, 4, 7)),
            new BlockOccupancy(new Vector3i(0, 4, 8)),
            new BlockOccupancy(new Vector3i(0, 4, 9)),
            new BlockOccupancy(new Vector3i(0, 4, 10)),
            new BlockOccupancy(new Vector3i(0, 4, 11)),
            new BlockOccupancy(new Vector3i(0, 4, 12)),
            new BlockOccupancy(new Vector3i(0, 4, 13)),
            new BlockOccupancy(new Vector3i(0, 4, 14)),
            new BlockOccupancy(new Vector3i(0, 4, 15)),
            new BlockOccupancy(new Vector3i(0, 4, 16)),
            new BlockOccupancy(new Vector3i(0, 4, 17)),
            new BlockOccupancy(new Vector3i(0, 4, 18)),
            new BlockOccupancy(new Vector3i(0, 4, 19)),
            new BlockOccupancy(new Vector3i(0, 4, 20)),
            new BlockOccupancy(new Vector3i(0, 4, 21)),
            new BlockOccupancy(new Vector3i(0, 5, 0)),
            new BlockOccupancy(new Vector3i(0, 5, 1)),
            new BlockOccupancy(new Vector3i(0, 5, 2)),
            new BlockOccupancy(new Vector3i(0, 5, 3)),
            new BlockOccupancy(new Vector3i(0, 5, 4)),
            new BlockOccupancy(new Vector3i(0, 5, 5)),
            new BlockOccupancy(new Vector3i(0, 5, 6)),
            new BlockOccupancy(new Vector3i(0, 5, 7)),
            new BlockOccupancy(new Vector3i(0, 5, 8)),
            new BlockOccupancy(new Vector3i(0, 5, 9)),
            new BlockOccupancy(new Vector3i(0, 5, 10)),
            new BlockOccupancy(new Vector3i(0, 5, 11)),
            new BlockOccupancy(new Vector3i(0, 5, 12)),
            new BlockOccupancy(new Vector3i(0, 5, 13)),
            new BlockOccupancy(new Vector3i(0, 5, 14)),
            new BlockOccupancy(new Vector3i(0, 5, 15)),
            new BlockOccupancy(new Vector3i(0, 5, 16)),
            new BlockOccupancy(new Vector3i(0, 5, 17)),
            new BlockOccupancy(new Vector3i(0, 5, 18)),
            new BlockOccupancy(new Vector3i(0, 5, 19)),
            new BlockOccupancy(new Vector3i(0, 5, 20)),
            new BlockOccupancy(new Vector3i(0, 5, 21)),
            new BlockOccupancy(new Vector3i(0, 6, 0)),
            new BlockOccupancy(new Vector3i(0, 6, 1)),
            new BlockOccupancy(new Vector3i(0, 6, 2)),
            new BlockOccupancy(new Vector3i(0, 6, 3)),
            new BlockOccupancy(new Vector3i(0, 6, 4)),
            new BlockOccupancy(new Vector3i(0, 6, 5)),
            new BlockOccupancy(new Vector3i(0, 6, 6)),
            new BlockOccupancy(new Vector3i(0, 6, 7)),
            new BlockOccupancy(new Vector3i(0, 6, 8)),
            new BlockOccupancy(new Vector3i(0, 6, 9)),
            new BlockOccupancy(new Vector3i(0, 6, 10)),
            new BlockOccupancy(new Vector3i(0, 6, 11)),
            new BlockOccupancy(new Vector3i(0, 6, 12)),
            new BlockOccupancy(new Vector3i(0, 6, 13)),
            new BlockOccupancy(new Vector3i(0, 6, 14)),
            new BlockOccupancy(new Vector3i(0, 6, 15)),
            new BlockOccupancy(new Vector3i(0, 6, 16)),
            new BlockOccupancy(new Vector3i(0, 6, 17)),
            new BlockOccupancy(new Vector3i(0, 6, 18)),
            new BlockOccupancy(new Vector3i(0, 6, 19)),
            new BlockOccupancy(new Vector3i(0, 6, 20)),
            new BlockOccupancy(new Vector3i(0, 6, 21)),
            new BlockOccupancy(new Vector3i(0, 7, 0)),
            new BlockOccupancy(new Vector3i(0, 7, 1)),
            new BlockOccupancy(new Vector3i(0, 7, 2)),
            new BlockOccupancy(new Vector3i(0, 7, 3)),
            new BlockOccupancy(new Vector3i(0, 7, 4)),
            new BlockOccupancy(new Vector3i(0, 7, 5)),
            new BlockOccupancy(new Vector3i(0, 7, 6)),
            new BlockOccupancy(new Vector3i(0, 7, 7)),
            new BlockOccupancy(new Vector3i(0, 7, 8)),
            new BlockOccupancy(new Vector3i(0, 7, 9)),
            new BlockOccupancy(new Vector3i(0, 7, 10)),
            new BlockOccupancy(new Vector3i(0, 7, 11)),
            new BlockOccupancy(new Vector3i(0, 7, 12)),
            new BlockOccupancy(new Vector3i(0, 7, 13)),
            new BlockOccupancy(new Vector3i(0, 7, 14)),
            new BlockOccupancy(new Vector3i(0, 7, 15)),
            new BlockOccupancy(new Vector3i(0, 7, 16)),
            new BlockOccupancy(new Vector3i(0, 7, 17)),
            new BlockOccupancy(new Vector3i(0, 7, 18)),
            new BlockOccupancy(new Vector3i(0, 7, 19)),
            new BlockOccupancy(new Vector3i(0, 7, 20)),
            new BlockOccupancy(new Vector3i(0, 7, 21)),
            new BlockOccupancy(new Vector3i(0, 8, 0)),
            new BlockOccupancy(new Vector3i(0, 8, 1)),
            new BlockOccupancy(new Vector3i(0, 8, 2)),
            new BlockOccupancy(new Vector3i(0, 8, 3)),
            new BlockOccupancy(new Vector3i(0, 8, 4)),
            new BlockOccupancy(new Vector3i(0, 8, 5)),
            new BlockOccupancy(new Vector3i(0, 8, 6)),
            new BlockOccupancy(new Vector3i(0, 8, 7)),
            new BlockOccupancy(new Vector3i(0, 8, 8)),
            new BlockOccupancy(new Vector3i(0, 8, 9)),
            new BlockOccupancy(new Vector3i(0, 8, 10)),
            new BlockOccupancy(new Vector3i(0, 8, 11)),
            new BlockOccupancy(new Vector3i(0, 8, 12)),
            new BlockOccupancy(new Vector3i(0, 8, 13)),
            new BlockOccupancy(new Vector3i(0, 8, 14)),
            new BlockOccupancy(new Vector3i(0, 8, 15)),
            new BlockOccupancy(new Vector3i(0, 8, 16)),
            new BlockOccupancy(new Vector3i(0, 8, 17)),
            new BlockOccupancy(new Vector3i(0, 8, 18)),
            new BlockOccupancy(new Vector3i(0, 8, 19)),
            new BlockOccupancy(new Vector3i(0, 8, 20)),
            new BlockOccupancy(new Vector3i(0, 8, 21)),
            new BlockOccupancy(new Vector3i(0, 9, 0)),
            new BlockOccupancy(new Vector3i(0, 9, 1)),
            new BlockOccupancy(new Vector3i(0, 9, 2)),
            new BlockOccupancy(new Vector3i(0, 9, 3)),
            new BlockOccupancy(new Vector3i(0, 9, 4)),
            new BlockOccupancy(new Vector3i(0, 9, 5)),
            new BlockOccupancy(new Vector3i(0, 9, 6)),
            new BlockOccupancy(new Vector3i(0, 9, 7)),
            new BlockOccupancy(new Vector3i(0, 9, 8)),
            new BlockOccupancy(new Vector3i(0, 9, 9)),
            new BlockOccupancy(new Vector3i(0, 9, 10)),
            new BlockOccupancy(new Vector3i(0, 9, 11)),
            new BlockOccupancy(new Vector3i(0, 9, 12)),
            new BlockOccupancy(new Vector3i(0, 9, 13)),
            new BlockOccupancy(new Vector3i(0, 9, 14)),
            new BlockOccupancy(new Vector3i(0, 9, 15)),
            new BlockOccupancy(new Vector3i(0, 9, 16)),
            new BlockOccupancy(new Vector3i(0, 9, 17)),
            new BlockOccupancy(new Vector3i(0, 9, 18)),
            new BlockOccupancy(new Vector3i(0, 9, 19)),
            new BlockOccupancy(new Vector3i(0, 9, 20)),
            new BlockOccupancy(new Vector3i(0, 9, 21)),
            new BlockOccupancy(new Vector3i(1, 0, 0)),
            new BlockOccupancy(new Vector3i(1, 0, 1)),
            new BlockOccupancy(new Vector3i(1, 0, 2)),
            new BlockOccupancy(new Vector3i(1, 0, 3)),
            new BlockOccupancy(new Vector3i(1, 0, 4)),
            new BlockOccupancy(new Vector3i(1, 0, 5)),
            new BlockOccupancy(new Vector3i(1, 0, 6)),
            new BlockOccupancy(new Vector3i(1, 0, 7)),
            new BlockOccupancy(new Vector3i(1, 0, 8)),
            new BlockOccupancy(new Vector3i(1, 0, 9)),
            new BlockOccupancy(new Vector3i(1, 0, 10)),
            new BlockOccupancy(new Vector3i(1, 0, 11)),
            new BlockOccupancy(new Vector3i(1, 0, 12)),
            new BlockOccupancy(new Vector3i(1, 0, 13)),
            new BlockOccupancy(new Vector3i(1, 0, 14)),
            new BlockOccupancy(new Vector3i(1, 0, 15)),
            new BlockOccupancy(new Vector3i(1, 0, 16)),
            new BlockOccupancy(new Vector3i(1, 0, 17)),
            new BlockOccupancy(new Vector3i(1, 0, 18)),
            new BlockOccupancy(new Vector3i(1, 0, 19)),
            new BlockOccupancy(new Vector3i(1, 0, 20)),
            new BlockOccupancy(new Vector3i(1, 0, 21)),
            new BlockOccupancy(new Vector3i(1, 1, 0)),
            new BlockOccupancy(new Vector3i(1, 1, 1)),
            new BlockOccupancy(new Vector3i(1, 1, 2)),
            new BlockOccupancy(new Vector3i(1, 1, 3)),
            new BlockOccupancy(new Vector3i(1, 1, 4)),
            new BlockOccupancy(new Vector3i(1, 1, 5)),
            new BlockOccupancy(new Vector3i(1, 1, 6)),
            new BlockOccupancy(new Vector3i(1, 1, 7)),
            new BlockOccupancy(new Vector3i(1, 1, 8)),
            new BlockOccupancy(new Vector3i(1, 1, 9)),
            new BlockOccupancy(new Vector3i(1, 1, 10)),
            new BlockOccupancy(new Vector3i(1, 1, 11)),
            new BlockOccupancy(new Vector3i(1, 1, 12)),
            new BlockOccupancy(new Vector3i(1, 1, 13)),
            new BlockOccupancy(new Vector3i(1, 1, 14)),
            new BlockOccupancy(new Vector3i(1, 1, 15)),
            new BlockOccupancy(new Vector3i(1, 1, 16)),
            new BlockOccupancy(new Vector3i(1, 1, 17)),
            new BlockOccupancy(new Vector3i(1, 1, 18)),
            new BlockOccupancy(new Vector3i(1, 1, 19)),
            new BlockOccupancy(new Vector3i(1, 1, 20)),
            new BlockOccupancy(new Vector3i(1, 1, 21)),
            new BlockOccupancy(new Vector3i(1, 2, 0)),
            new BlockOccupancy(new Vector3i(1, 2, 1)),
            new BlockOccupancy(new Vector3i(1, 2, 2)),
            new BlockOccupancy(new Vector3i(1, 2, 3)),
            new BlockOccupancy(new Vector3i(1, 2, 4)),
            new BlockOccupancy(new Vector3i(1, 2, 5)),
            new BlockOccupancy(new Vector3i(1, 2, 6)),
            new BlockOccupancy(new Vector3i(1, 2, 7)),
            new BlockOccupancy(new Vector3i(1, 2, 8)),
            new BlockOccupancy(new Vector3i(1, 2, 9)),
            new BlockOccupancy(new Vector3i(1, 2, 10)),
            new BlockOccupancy(new Vector3i(1, 2, 11)),
            new BlockOccupancy(new Vector3i(1, 2, 12)),
            new BlockOccupancy(new Vector3i(1, 2, 13)),
            new BlockOccupancy(new Vector3i(1, 2, 14)),
            new BlockOccupancy(new Vector3i(1, 2, 15)),
            new BlockOccupancy(new Vector3i(1, 2, 16)),
            new BlockOccupancy(new Vector3i(1, 2, 17)),
            new BlockOccupancy(new Vector3i(1, 2, 18)),
            new BlockOccupancy(new Vector3i(1, 2, 19)),
            new BlockOccupancy(new Vector3i(1, 2, 20)),
            new BlockOccupancy(new Vector3i(1, 2, 21)),
            new BlockOccupancy(new Vector3i(1, 3, 0)),
            new BlockOccupancy(new Vector3i(1, 3, 1)),
            new BlockOccupancy(new Vector3i(1, 3, 2)),
            new BlockOccupancy(new Vector3i(1, 3, 3)),
            new BlockOccupancy(new Vector3i(1, 3, 4)),
            new BlockOccupancy(new Vector3i(1, 3, 5)),
            new BlockOccupancy(new Vector3i(1, 3, 6)),
            new BlockOccupancy(new Vector3i(1, 3, 7)),
            new BlockOccupancy(new Vector3i(1, 3, 8)),
            new BlockOccupancy(new Vector3i(1, 3, 9)),
            new BlockOccupancy(new Vector3i(1, 3, 10)),
            new BlockOccupancy(new Vector3i(1, 3, 11)),
            new BlockOccupancy(new Vector3i(1, 3, 12)),
            new BlockOccupancy(new Vector3i(1, 3, 13)),
            new BlockOccupancy(new Vector3i(1, 3, 14)),
            new BlockOccupancy(new Vector3i(1, 3, 15)),
            new BlockOccupancy(new Vector3i(1, 3, 16)),
            new BlockOccupancy(new Vector3i(1, 3, 17)),
            new BlockOccupancy(new Vector3i(1, 3, 18)),
            new BlockOccupancy(new Vector3i(1, 3, 19)),
            new BlockOccupancy(new Vector3i(1, 3, 20)),
            new BlockOccupancy(new Vector3i(1, 3, 21)),
            new BlockOccupancy(new Vector3i(1, 4, 0)),
            new BlockOccupancy(new Vector3i(1, 4, 1)),
            new BlockOccupancy(new Vector3i(1, 4, 2)),
            new BlockOccupancy(new Vector3i(1, 4, 3)),
            new BlockOccupancy(new Vector3i(1, 4, 4)),
            new BlockOccupancy(new Vector3i(1, 4, 5)),
            new BlockOccupancy(new Vector3i(1, 4, 6)),
            new BlockOccupancy(new Vector3i(1, 4, 7)),
            new BlockOccupancy(new Vector3i(1, 4, 8)),
            new BlockOccupancy(new Vector3i(1, 4, 9)),
            new BlockOccupancy(new Vector3i(1, 4, 10)),
            new BlockOccupancy(new Vector3i(1, 4, 11)),
            new BlockOccupancy(new Vector3i(1, 4, 12)),
            new BlockOccupancy(new Vector3i(1, 4, 13)),
            new BlockOccupancy(new Vector3i(1, 4, 14)),
            new BlockOccupancy(new Vector3i(1, 4, 15)),
            new BlockOccupancy(new Vector3i(1, 4, 16)),
            new BlockOccupancy(new Vector3i(1, 4, 17)),
            new BlockOccupancy(new Vector3i(1, 4, 18)),
            new BlockOccupancy(new Vector3i(1, 4, 19)),
            new BlockOccupancy(new Vector3i(1, 4, 20)),
            new BlockOccupancy(new Vector3i(1, 4, 21)),
            new BlockOccupancy(new Vector3i(1, 5, 0)),
            new BlockOccupancy(new Vector3i(1, 5, 1)),
            new BlockOccupancy(new Vector3i(1, 5, 2)),
            new BlockOccupancy(new Vector3i(1, 5, 3)),
            new BlockOccupancy(new Vector3i(1, 5, 4)),
            new BlockOccupancy(new Vector3i(1, 5, 5)),
            new BlockOccupancy(new Vector3i(1, 5, 6)),
            new BlockOccupancy(new Vector3i(1, 5, 7)),
            new BlockOccupancy(new Vector3i(1, 5, 8)),
            new BlockOccupancy(new Vector3i(1, 5, 9)),
            new BlockOccupancy(new Vector3i(1, 5, 10)),
            new BlockOccupancy(new Vector3i(1, 5, 11)),
            new BlockOccupancy(new Vector3i(1, 5, 12)),
            new BlockOccupancy(new Vector3i(1, 5, 13)),
            new BlockOccupancy(new Vector3i(1, 5, 14)),
            new BlockOccupancy(new Vector3i(1, 5, 15)),
            new BlockOccupancy(new Vector3i(1, 5, 16)),
            new BlockOccupancy(new Vector3i(1, 5, 17)),
            new BlockOccupancy(new Vector3i(1, 5, 18)),
            new BlockOccupancy(new Vector3i(1, 5, 19)),
            new BlockOccupancy(new Vector3i(1, 5, 20)),
            new BlockOccupancy(new Vector3i(1, 5, 21)),
            new BlockOccupancy(new Vector3i(1, 6, 0)),
            new BlockOccupancy(new Vector3i(1, 6, 1)),
            new BlockOccupancy(new Vector3i(1, 6, 2)),
            new BlockOccupancy(new Vector3i(1, 6, 3)),
            new BlockOccupancy(new Vector3i(1, 6, 4)),
            new BlockOccupancy(new Vector3i(1, 6, 5)),
            new BlockOccupancy(new Vector3i(1, 6, 6)),
            new BlockOccupancy(new Vector3i(1, 6, 7)),
            new BlockOccupancy(new Vector3i(1, 6, 8)),
            new BlockOccupancy(new Vector3i(1, 6, 9)),
            new BlockOccupancy(new Vector3i(1, 6, 10)),
            new BlockOccupancy(new Vector3i(1, 6, 11)),
            new BlockOccupancy(new Vector3i(1, 6, 12)),
            new BlockOccupancy(new Vector3i(1, 6, 13)),
            new BlockOccupancy(new Vector3i(1, 6, 14)),
            new BlockOccupancy(new Vector3i(1, 6, 15)),
            new BlockOccupancy(new Vector3i(1, 6, 16)),
            new BlockOccupancy(new Vector3i(1, 6, 17)),
            new BlockOccupancy(new Vector3i(1, 6, 18)),
            new BlockOccupancy(new Vector3i(1, 6, 19)),
            new BlockOccupancy(new Vector3i(1, 6, 20)),
            new BlockOccupancy(new Vector3i(1, 6, 21)),
            new BlockOccupancy(new Vector3i(1, 7, 0)),
            new BlockOccupancy(new Vector3i(1, 7, 1)),
            new BlockOccupancy(new Vector3i(1, 7, 2)),
            new BlockOccupancy(new Vector3i(1, 7, 3)),
            new BlockOccupancy(new Vector3i(1, 7, 4)),
            new BlockOccupancy(new Vector3i(1, 7, 5)),
            new BlockOccupancy(new Vector3i(1, 7, 6)),
            new BlockOccupancy(new Vector3i(1, 7, 7)),
            new BlockOccupancy(new Vector3i(1, 7, 8)),
            new BlockOccupancy(new Vector3i(1, 7, 9)),
            new BlockOccupancy(new Vector3i(1, 7, 10)),
            new BlockOccupancy(new Vector3i(1, 7, 11)),
            new BlockOccupancy(new Vector3i(1, 7, 12)),
            new BlockOccupancy(new Vector3i(1, 7, 13)),
            new BlockOccupancy(new Vector3i(1, 7, 14)),
            new BlockOccupancy(new Vector3i(1, 7, 15)),
            new BlockOccupancy(new Vector3i(1, 7, 16)),
            new BlockOccupancy(new Vector3i(1, 7, 17)),
            new BlockOccupancy(new Vector3i(1, 7, 18)),
            new BlockOccupancy(new Vector3i(1, 7, 19)),
            new BlockOccupancy(new Vector3i(1, 7, 20)),
            new BlockOccupancy(new Vector3i(1, 7, 21)),
            new BlockOccupancy(new Vector3i(1, 8, 0)),
            new BlockOccupancy(new Vector3i(1, 8, 1)),
            new BlockOccupancy(new Vector3i(1, 8, 2)),
            new BlockOccupancy(new Vector3i(1, 8, 3)),
            new BlockOccupancy(new Vector3i(1, 8, 4)),
            new BlockOccupancy(new Vector3i(1, 8, 5)),
            new BlockOccupancy(new Vector3i(1, 8, 6)),
            new BlockOccupancy(new Vector3i(1, 8, 7)),
            new BlockOccupancy(new Vector3i(1, 8, 8)),
            new BlockOccupancy(new Vector3i(1, 8, 9)),
            new BlockOccupancy(new Vector3i(1, 8, 10)),
            new BlockOccupancy(new Vector3i(1, 8, 11)),
            new BlockOccupancy(new Vector3i(1, 8, 12)),
            new BlockOccupancy(new Vector3i(1, 8, 13)),
            new BlockOccupancy(new Vector3i(1, 8, 14)),
            new BlockOccupancy(new Vector3i(1, 8, 15)),
            new BlockOccupancy(new Vector3i(1, 8, 16)),
            new BlockOccupancy(new Vector3i(1, 8, 17)),
            new BlockOccupancy(new Vector3i(1, 8, 18)),
            new BlockOccupancy(new Vector3i(1, 8, 19)),
            new BlockOccupancy(new Vector3i(1, 8, 20)),
            new BlockOccupancy(new Vector3i(1, 8, 21)),
            new BlockOccupancy(new Vector3i(1, 9, 0)),
            new BlockOccupancy(new Vector3i(1, 9, 1)),
            new BlockOccupancy(new Vector3i(1, 9, 2)),
            new BlockOccupancy(new Vector3i(1, 9, 3)),
            new BlockOccupancy(new Vector3i(1, 9, 4)),
            new BlockOccupancy(new Vector3i(1, 9, 5)),
            new BlockOccupancy(new Vector3i(1, 9, 6)),
            new BlockOccupancy(new Vector3i(1, 9, 7)),
            new BlockOccupancy(new Vector3i(1, 9, 8)),
            new BlockOccupancy(new Vector3i(1, 9, 9)),
            new BlockOccupancy(new Vector3i(1, 9, 10)),
            new BlockOccupancy(new Vector3i(1, 9, 11)),
            new BlockOccupancy(new Vector3i(1, 9, 12)),
            new BlockOccupancy(new Vector3i(1, 9, 13)),
            new BlockOccupancy(new Vector3i(1, 9, 14)),
            new BlockOccupancy(new Vector3i(1, 9, 15)),
            new BlockOccupancy(new Vector3i(1, 9, 16)),
            new BlockOccupancy(new Vector3i(1, 9, 17)),
            new BlockOccupancy(new Vector3i(1, 9, 18)),
            new BlockOccupancy(new Vector3i(1, 9, 19)),
            new BlockOccupancy(new Vector3i(1, 9, 20)),
            new BlockOccupancy(new Vector3i(1, 9, 21)),
            new BlockOccupancy(new Vector3i(2, 0, 0)),
            new BlockOccupancy(new Vector3i(2, 0, 1)),
            new BlockOccupancy(new Vector3i(2, 0, 2)),
            new BlockOccupancy(new Vector3i(2, 0, 3)),
            new BlockOccupancy(new Vector3i(2, 0, 4)),
            new BlockOccupancy(new Vector3i(2, 0, 5)),
            new BlockOccupancy(new Vector3i(2, 0, 6)),
            new BlockOccupancy(new Vector3i(2, 0, 7)),
            new BlockOccupancy(new Vector3i(2, 0, 8)),
            new BlockOccupancy(new Vector3i(2, 0, 9)),
            new BlockOccupancy(new Vector3i(2, 0, 10)),
            new BlockOccupancy(new Vector3i(2, 0, 11)),
            new BlockOccupancy(new Vector3i(2, 0, 12)),
            new BlockOccupancy(new Vector3i(2, 0, 13)),
            new BlockOccupancy(new Vector3i(2, 0, 14)),
            new BlockOccupancy(new Vector3i(2, 0, 15)),
            new BlockOccupancy(new Vector3i(2, 0, 16)),
            new BlockOccupancy(new Vector3i(2, 0, 17)),
            new BlockOccupancy(new Vector3i(2, 0, 18)),
            new BlockOccupancy(new Vector3i(2, 0, 19)),
            new BlockOccupancy(new Vector3i(2, 0, 20)),
            new BlockOccupancy(new Vector3i(2, 0, 21)),
            new BlockOccupancy(new Vector3i(2, 1, 0)),
            new BlockOccupancy(new Vector3i(2, 1, 1)),
            new BlockOccupancy(new Vector3i(2, 1, 2)),
            new BlockOccupancy(new Vector3i(2, 1, 3)),
            new BlockOccupancy(new Vector3i(2, 1, 4)),
            new BlockOccupancy(new Vector3i(2, 1, 5)),
            new BlockOccupancy(new Vector3i(2, 1, 6)),
            new BlockOccupancy(new Vector3i(2, 1, 7)),
            new BlockOccupancy(new Vector3i(2, 1, 8)),
            new BlockOccupancy(new Vector3i(2, 1, 9)),
            new BlockOccupancy(new Vector3i(2, 1, 10)),
            new BlockOccupancy(new Vector3i(2, 1, 11)),
            new BlockOccupancy(new Vector3i(2, 1, 12)),
            new BlockOccupancy(new Vector3i(2, 1, 13)),
            new BlockOccupancy(new Vector3i(2, 1, 14)),
            new BlockOccupancy(new Vector3i(2, 1, 15)),
            new BlockOccupancy(new Vector3i(2, 1, 16)),
            new BlockOccupancy(new Vector3i(2, 1, 17)),
            new BlockOccupancy(new Vector3i(2, 1, 18)),
            new BlockOccupancy(new Vector3i(2, 1, 19)),
            new BlockOccupancy(new Vector3i(2, 1, 20)),
            new BlockOccupancy(new Vector3i(2, 1, 21)),
            new BlockOccupancy(new Vector3i(2, 2, 0)),
            new BlockOccupancy(new Vector3i(2, 2, 1)),
            new BlockOccupancy(new Vector3i(2, 2, 2)),
            new BlockOccupancy(new Vector3i(2, 2, 3)),
            new BlockOccupancy(new Vector3i(2, 2, 4)),
            new BlockOccupancy(new Vector3i(2, 2, 5)),
            new BlockOccupancy(new Vector3i(2, 2, 6)),
            new BlockOccupancy(new Vector3i(2, 2, 7)),
            new BlockOccupancy(new Vector3i(2, 2, 8)),
            new BlockOccupancy(new Vector3i(2, 2, 9)),
            new BlockOccupancy(new Vector3i(2, 2, 10)),
            new BlockOccupancy(new Vector3i(2, 2, 11)),
            new BlockOccupancy(new Vector3i(2, 2, 12)),
            new BlockOccupancy(new Vector3i(2, 2, 13)),
            new BlockOccupancy(new Vector3i(2, 2, 14)),
            new BlockOccupancy(new Vector3i(2, 2, 15)),
            new BlockOccupancy(new Vector3i(2, 2, 16)),
            new BlockOccupancy(new Vector3i(2, 2, 17)),
            new BlockOccupancy(new Vector3i(2, 2, 18)),
            new BlockOccupancy(new Vector3i(2, 2, 19)),
            new BlockOccupancy(new Vector3i(2, 2, 20)),
            new BlockOccupancy(new Vector3i(2, 2, 21)),
            new BlockOccupancy(new Vector3i(2, 3, 0)),
            new BlockOccupancy(new Vector3i(2, 3, 1)),
            new BlockOccupancy(new Vector3i(2, 3, 2)),
            new BlockOccupancy(new Vector3i(2, 3, 3)),
            new BlockOccupancy(new Vector3i(2, 3, 4)),
            new BlockOccupancy(new Vector3i(2, 3, 5)),
            new BlockOccupancy(new Vector3i(2, 3, 6)),
            new BlockOccupancy(new Vector3i(2, 3, 7)),
            new BlockOccupancy(new Vector3i(2, 3, 8)),
            new BlockOccupancy(new Vector3i(2, 3, 9)),
            new BlockOccupancy(new Vector3i(2, 3, 10)),
            new BlockOccupancy(new Vector3i(2, 3, 11)),
            new BlockOccupancy(new Vector3i(2, 3, 12)),
            new BlockOccupancy(new Vector3i(2, 3, 13)),
            new BlockOccupancy(new Vector3i(2, 3, 14)),
            new BlockOccupancy(new Vector3i(2, 3, 15)),
            new BlockOccupancy(new Vector3i(2, 3, 16)),
            new BlockOccupancy(new Vector3i(2, 3, 17)),
            new BlockOccupancy(new Vector3i(2, 3, 18)),
            new BlockOccupancy(new Vector3i(2, 3, 19)),
            new BlockOccupancy(new Vector3i(2, 3, 20)),
            new BlockOccupancy(new Vector3i(2, 3, 21)),
            new BlockOccupancy(new Vector3i(2, 4, 0)),
            new BlockOccupancy(new Vector3i(2, 4, 1)),
            new BlockOccupancy(new Vector3i(2, 4, 2)),
            new BlockOccupancy(new Vector3i(2, 4, 3)),
            new BlockOccupancy(new Vector3i(2, 4, 4)),
            new BlockOccupancy(new Vector3i(2, 4, 5)),
            new BlockOccupancy(new Vector3i(2, 4, 6)),
            new BlockOccupancy(new Vector3i(2, 4, 7)),
            new BlockOccupancy(new Vector3i(2, 4, 8)),
            new BlockOccupancy(new Vector3i(2, 4, 9)),
            new BlockOccupancy(new Vector3i(2, 4, 10)),
            new BlockOccupancy(new Vector3i(2, 4, 11)),
            new BlockOccupancy(new Vector3i(2, 4, 12)),
            new BlockOccupancy(new Vector3i(2, 4, 13)),
            new BlockOccupancy(new Vector3i(2, 4, 14)),
            new BlockOccupancy(new Vector3i(2, 4, 15)),
            new BlockOccupancy(new Vector3i(2, 4, 16)),
            new BlockOccupancy(new Vector3i(2, 4, 17)),
            new BlockOccupancy(new Vector3i(2, 4, 18)),
            new BlockOccupancy(new Vector3i(2, 4, 19)),
            new BlockOccupancy(new Vector3i(2, 4, 20)),
            new BlockOccupancy(new Vector3i(2, 4, 21)),
            new BlockOccupancy(new Vector3i(2, 5, 0)),
            new BlockOccupancy(new Vector3i(2, 5, 1)),
            new BlockOccupancy(new Vector3i(2, 5, 2)),
            new BlockOccupancy(new Vector3i(2, 5, 3)),
            new BlockOccupancy(new Vector3i(2, 5, 4)),
            new BlockOccupancy(new Vector3i(2, 5, 5)),
            new BlockOccupancy(new Vector3i(2, 5, 6)),
            new BlockOccupancy(new Vector3i(2, 5, 7)),
            new BlockOccupancy(new Vector3i(2, 5, 8)),
            new BlockOccupancy(new Vector3i(2, 5, 9)),
            new BlockOccupancy(new Vector3i(2, 5, 10)),
            new BlockOccupancy(new Vector3i(2, 5, 11)),
            new BlockOccupancy(new Vector3i(2, 5, 12)),
            new BlockOccupancy(new Vector3i(2, 5, 13)),
            new BlockOccupancy(new Vector3i(2, 5, 14)),
            new BlockOccupancy(new Vector3i(2, 5, 15)),
            new BlockOccupancy(new Vector3i(2, 5, 16)),
            new BlockOccupancy(new Vector3i(2, 5, 17)),
            new BlockOccupancy(new Vector3i(2, 5, 18)),
            new BlockOccupancy(new Vector3i(2, 5, 19)),
            new BlockOccupancy(new Vector3i(2, 5, 20)),
            new BlockOccupancy(new Vector3i(2, 5, 21)),
            new BlockOccupancy(new Vector3i(2, 6, 0)),
            new BlockOccupancy(new Vector3i(2, 6, 1)),
            new BlockOccupancy(new Vector3i(2, 6, 2)),
            new BlockOccupancy(new Vector3i(2, 6, 3)),
            new BlockOccupancy(new Vector3i(2, 6, 4)),
            new BlockOccupancy(new Vector3i(2, 6, 5)),
            new BlockOccupancy(new Vector3i(2, 6, 6)),
            new BlockOccupancy(new Vector3i(2, 6, 7)),
            new BlockOccupancy(new Vector3i(2, 6, 8)),
            new BlockOccupancy(new Vector3i(2, 6, 9)),
            new BlockOccupancy(new Vector3i(2, 6, 10)),
            new BlockOccupancy(new Vector3i(2, 6, 11)),
            new BlockOccupancy(new Vector3i(2, 6, 12)),
            new BlockOccupancy(new Vector3i(2, 6, 13)),
            new BlockOccupancy(new Vector3i(2, 6, 14)),
            new BlockOccupancy(new Vector3i(2, 6, 15)),
            new BlockOccupancy(new Vector3i(2, 6, 16)),
            new BlockOccupancy(new Vector3i(2, 6, 17)),
            new BlockOccupancy(new Vector3i(2, 6, 18)),
            new BlockOccupancy(new Vector3i(2, 6, 19)),
            new BlockOccupancy(new Vector3i(2, 6, 20)),
            new BlockOccupancy(new Vector3i(2, 6, 21)),
            new BlockOccupancy(new Vector3i(2, 7, 0)),
            new BlockOccupancy(new Vector3i(2, 7, 1)),
            new BlockOccupancy(new Vector3i(2, 7, 2)),
            new BlockOccupancy(new Vector3i(2, 7, 3)),
            new BlockOccupancy(new Vector3i(2, 7, 4)),
            new BlockOccupancy(new Vector3i(2, 7, 5)),
            new BlockOccupancy(new Vector3i(2, 7, 6)),
            new BlockOccupancy(new Vector3i(2, 7, 7)),
            new BlockOccupancy(new Vector3i(2, 7, 8)),
            new BlockOccupancy(new Vector3i(2, 7, 9)),
            new BlockOccupancy(new Vector3i(2, 7, 10)),
            new BlockOccupancy(new Vector3i(2, 7, 11)),
            new BlockOccupancy(new Vector3i(2, 7, 12)),
            new BlockOccupancy(new Vector3i(2, 7, 13)),
            new BlockOccupancy(new Vector3i(2, 7, 14)),
            new BlockOccupancy(new Vector3i(2, 7, 15)),
            new BlockOccupancy(new Vector3i(2, 7, 16)),
            new BlockOccupancy(new Vector3i(2, 7, 17)),
            new BlockOccupancy(new Vector3i(2, 7, 18)),
            new BlockOccupancy(new Vector3i(2, 7, 19)),
            new BlockOccupancy(new Vector3i(2, 7, 20)),
            new BlockOccupancy(new Vector3i(2, 7, 21)),
            new BlockOccupancy(new Vector3i(2, 8, 0)),
            new BlockOccupancy(new Vector3i(2, 8, 1)),
            new BlockOccupancy(new Vector3i(2, 8, 2)),
            new BlockOccupancy(new Vector3i(2, 8, 3)),
            new BlockOccupancy(new Vector3i(2, 8, 4)),
            new BlockOccupancy(new Vector3i(2, 8, 5)),
            new BlockOccupancy(new Vector3i(2, 8, 6)),
            new BlockOccupancy(new Vector3i(2, 8, 7)),
            new BlockOccupancy(new Vector3i(2, 8, 8)),
            new BlockOccupancy(new Vector3i(2, 8, 9)),
            new BlockOccupancy(new Vector3i(2, 8, 10)),
            new BlockOccupancy(new Vector3i(2, 8, 11)),
            new BlockOccupancy(new Vector3i(2, 8, 12)),
            new BlockOccupancy(new Vector3i(2, 8, 13)),
            new BlockOccupancy(new Vector3i(2, 8, 14)),
            new BlockOccupancy(new Vector3i(2, 8, 15)),
            new BlockOccupancy(new Vector3i(2, 8, 16)),
            new BlockOccupancy(new Vector3i(2, 8, 17)),
            new BlockOccupancy(new Vector3i(2, 8, 18)),
            new BlockOccupancy(new Vector3i(2, 8, 19)),
            new BlockOccupancy(new Vector3i(2, 8, 20)),
            new BlockOccupancy(new Vector3i(2, 8, 21)),
            new BlockOccupancy(new Vector3i(2, 9, 0)),
            new BlockOccupancy(new Vector3i(2, 9, 1)),
            new BlockOccupancy(new Vector3i(2, 9, 2)),
            new BlockOccupancy(new Vector3i(2, 9, 3)),
            new BlockOccupancy(new Vector3i(2, 9, 4)),
            new BlockOccupancy(new Vector3i(2, 9, 5)),
            new BlockOccupancy(new Vector3i(2, 9, 6)),
            new BlockOccupancy(new Vector3i(2, 9, 7)),
            new BlockOccupancy(new Vector3i(2, 9, 8)),
            new BlockOccupancy(new Vector3i(2, 9, 9)),
            new BlockOccupancy(new Vector3i(2, 9, 10)),
            new BlockOccupancy(new Vector3i(2, 9, 11)),
            new BlockOccupancy(new Vector3i(2, 9, 12)),
            new BlockOccupancy(new Vector3i(2, 9, 13)),
            new BlockOccupancy(new Vector3i(2, 9, 14)),
            new BlockOccupancy(new Vector3i(2, 9, 15)),
            new BlockOccupancy(new Vector3i(2, 9, 16)),
            new BlockOccupancy(new Vector3i(2, 9, 17)),
            new BlockOccupancy(new Vector3i(2, 9, 18)),
            new BlockOccupancy(new Vector3i(2, 9, 19)),
            new BlockOccupancy(new Vector3i(2, 9, 20)),
            new BlockOccupancy(new Vector3i(2, 9, 21)),
            new BlockOccupancy(new Vector3i(3, 0, 0)),
            new BlockOccupancy(new Vector3i(3, 0, 1)),
            new BlockOccupancy(new Vector3i(3, 0, 2)),
            new BlockOccupancy(new Vector3i(3, 0, 3)),
            new BlockOccupancy(new Vector3i(3, 0, 4)),
            new BlockOccupancy(new Vector3i(3, 0, 5)),
            new BlockOccupancy(new Vector3i(3, 0, 6)),
            new BlockOccupancy(new Vector3i(3, 0, 7)),
            new BlockOccupancy(new Vector3i(3, 0, 8)),
            new BlockOccupancy(new Vector3i(3, 0, 9)),
            new BlockOccupancy(new Vector3i(3, 0, 10)),
            new BlockOccupancy(new Vector3i(3, 0, 11)),
            new BlockOccupancy(new Vector3i(3, 0, 12)),
            new BlockOccupancy(new Vector3i(3, 0, 13)),
            new BlockOccupancy(new Vector3i(3, 0, 14)),
            new BlockOccupancy(new Vector3i(3, 0, 15)),
            new BlockOccupancy(new Vector3i(3, 0, 16)),
            new BlockOccupancy(new Vector3i(3, 0, 17)),
            new BlockOccupancy(new Vector3i(3, 0, 18)),
            new BlockOccupancy(new Vector3i(3, 0, 19)),
            new BlockOccupancy(new Vector3i(3, 0, 20)),
            new BlockOccupancy(new Vector3i(3, 0, 21)),
            new BlockOccupancy(new Vector3i(3, 1, 0)),
            new BlockOccupancy(new Vector3i(3, 1, 1)),
            new BlockOccupancy(new Vector3i(3, 1, 2)),
            new BlockOccupancy(new Vector3i(3, 1, 3)),
            new BlockOccupancy(new Vector3i(3, 1, 4)),
            new BlockOccupancy(new Vector3i(3, 1, 5)),
            new BlockOccupancy(new Vector3i(3, 1, 6)),
            new BlockOccupancy(new Vector3i(3, 1, 7)),
            new BlockOccupancy(new Vector3i(3, 1, 8)),
            new BlockOccupancy(new Vector3i(3, 1, 9)),
            new BlockOccupancy(new Vector3i(3, 1, 10)),
            new BlockOccupancy(new Vector3i(3, 1, 11)),
            new BlockOccupancy(new Vector3i(3, 1, 12)),
            new BlockOccupancy(new Vector3i(3, 1, 13)),
            new BlockOccupancy(new Vector3i(3, 1, 14)),
            new BlockOccupancy(new Vector3i(3, 1, 15)),
            new BlockOccupancy(new Vector3i(3, 1, 16)),
            new BlockOccupancy(new Vector3i(3, 1, 17)),
            new BlockOccupancy(new Vector3i(3, 1, 18)),
            new BlockOccupancy(new Vector3i(3, 1, 19)),
            new BlockOccupancy(new Vector3i(3, 1, 20)),
            new BlockOccupancy(new Vector3i(3, 1, 21)),
            new BlockOccupancy(new Vector3i(3, 2, 0)),
            new BlockOccupancy(new Vector3i(3, 2, 1)),
            new BlockOccupancy(new Vector3i(3, 2, 2)),
            new BlockOccupancy(new Vector3i(3, 2, 3)),
            new BlockOccupancy(new Vector3i(3, 2, 4)),
            new BlockOccupancy(new Vector3i(3, 2, 5)),
            new BlockOccupancy(new Vector3i(3, 2, 6)),
            new BlockOccupancy(new Vector3i(3, 2, 7)),
            new BlockOccupancy(new Vector3i(3, 2, 8)),
            new BlockOccupancy(new Vector3i(3, 2, 9)),
            new BlockOccupancy(new Vector3i(3, 2, 10)),
            new BlockOccupancy(new Vector3i(3, 2, 11)),
            new BlockOccupancy(new Vector3i(3, 2, 12)),
            new BlockOccupancy(new Vector3i(3, 2, 13)),
            new BlockOccupancy(new Vector3i(3, 2, 14)),
            new BlockOccupancy(new Vector3i(3, 2, 15)),
            new BlockOccupancy(new Vector3i(3, 2, 16)),
            new BlockOccupancy(new Vector3i(3, 2, 17)),
            new BlockOccupancy(new Vector3i(3, 2, 18)),
            new BlockOccupancy(new Vector3i(3, 2, 19)),
            new BlockOccupancy(new Vector3i(3, 2, 20)),
            new BlockOccupancy(new Vector3i(3, 2, 21)),
            new BlockOccupancy(new Vector3i(3, 3, 0)),
            new BlockOccupancy(new Vector3i(3, 3, 1)),
            new BlockOccupancy(new Vector3i(3, 3, 2)),
            new BlockOccupancy(new Vector3i(3, 3, 3)),
            new BlockOccupancy(new Vector3i(3, 3, 4)),
            new BlockOccupancy(new Vector3i(3, 3, 5)),
            new BlockOccupancy(new Vector3i(3, 3, 6)),
            new BlockOccupancy(new Vector3i(3, 3, 7)),
            new BlockOccupancy(new Vector3i(3, 3, 8)),
            new BlockOccupancy(new Vector3i(3, 3, 9)),
            new BlockOccupancy(new Vector3i(3, 3, 10)),
            new BlockOccupancy(new Vector3i(3, 3, 11)),
            new BlockOccupancy(new Vector3i(3, 3, 12)),
            new BlockOccupancy(new Vector3i(3, 3, 13)),
            new BlockOccupancy(new Vector3i(3, 3, 14)),
            new BlockOccupancy(new Vector3i(3, 3, 15)),
            new BlockOccupancy(new Vector3i(3, 3, 16)),
            new BlockOccupancy(new Vector3i(3, 3, 17)),
            new BlockOccupancy(new Vector3i(3, 3, 18)),
            new BlockOccupancy(new Vector3i(3, 3, 19)),
            new BlockOccupancy(new Vector3i(3, 3, 20)),
            new BlockOccupancy(new Vector3i(3, 3, 21)),
            new BlockOccupancy(new Vector3i(3, 4, 0)),
            new BlockOccupancy(new Vector3i(3, 4, 1)),
            new BlockOccupancy(new Vector3i(3, 4, 2)),
            new BlockOccupancy(new Vector3i(3, 4, 3)),
            new BlockOccupancy(new Vector3i(3, 4, 4)),
            new BlockOccupancy(new Vector3i(3, 4, 5)),
            new BlockOccupancy(new Vector3i(3, 4, 6)),
            new BlockOccupancy(new Vector3i(3, 4, 7)),
            new BlockOccupancy(new Vector3i(3, 4, 8)),
            new BlockOccupancy(new Vector3i(3, 4, 9)),
            new BlockOccupancy(new Vector3i(3, 4, 10)),
            new BlockOccupancy(new Vector3i(3, 4, 11)),
            new BlockOccupancy(new Vector3i(3, 4, 12)),
            new BlockOccupancy(new Vector3i(3, 4, 13)),
            new BlockOccupancy(new Vector3i(3, 4, 14)),
            new BlockOccupancy(new Vector3i(3, 4, 15)),
            new BlockOccupancy(new Vector3i(3, 4, 16)),
            new BlockOccupancy(new Vector3i(3, 4, 17)),
            new BlockOccupancy(new Vector3i(3, 4, 18)),
            new BlockOccupancy(new Vector3i(3, 4, 19)),
            new BlockOccupancy(new Vector3i(3, 4, 20)),
            new BlockOccupancy(new Vector3i(3, 4, 21)),
            new BlockOccupancy(new Vector3i(3, 5, 0)),
            new BlockOccupancy(new Vector3i(3, 5, 1)),
            new BlockOccupancy(new Vector3i(3, 5, 2)),
            new BlockOccupancy(new Vector3i(3, 5, 3)),
            new BlockOccupancy(new Vector3i(3, 5, 4)),
            new BlockOccupancy(new Vector3i(3, 5, 5)),
            new BlockOccupancy(new Vector3i(3, 5, 6)),
            new BlockOccupancy(new Vector3i(3, 5, 7)),
            new BlockOccupancy(new Vector3i(3, 5, 8)),
            new BlockOccupancy(new Vector3i(3, 5, 9)),
            new BlockOccupancy(new Vector3i(3, 5, 10)),
            new BlockOccupancy(new Vector3i(3, 5, 11)),
            new BlockOccupancy(new Vector3i(3, 5, 12)),
            new BlockOccupancy(new Vector3i(3, 5, 13)),
            new BlockOccupancy(new Vector3i(3, 5, 14)),
            new BlockOccupancy(new Vector3i(3, 5, 15)),
            new BlockOccupancy(new Vector3i(3, 5, 16)),
            new BlockOccupancy(new Vector3i(3, 5, 17)),
            new BlockOccupancy(new Vector3i(3, 5, 18)),
            new BlockOccupancy(new Vector3i(3, 5, 19)),
            new BlockOccupancy(new Vector3i(3, 5, 20)),
            new BlockOccupancy(new Vector3i(3, 5, 21)),
            new BlockOccupancy(new Vector3i(3, 6, 0)),
            new BlockOccupancy(new Vector3i(3, 6, 1)),
            new BlockOccupancy(new Vector3i(3, 6, 2)),
            new BlockOccupancy(new Vector3i(3, 6, 3)),
            new BlockOccupancy(new Vector3i(3, 6, 4)),
            new BlockOccupancy(new Vector3i(3, 6, 5)),
            new BlockOccupancy(new Vector3i(3, 6, 6)),
            new BlockOccupancy(new Vector3i(3, 6, 7)),
            new BlockOccupancy(new Vector3i(3, 6, 8)),
            new BlockOccupancy(new Vector3i(3, 6, 9)),
            new BlockOccupancy(new Vector3i(3, 6, 10)),
            new BlockOccupancy(new Vector3i(3, 6, 11)),
            new BlockOccupancy(new Vector3i(3, 6, 12)),
            new BlockOccupancy(new Vector3i(3, 6, 13)),
            new BlockOccupancy(new Vector3i(3, 6, 14)),
            new BlockOccupancy(new Vector3i(3, 6, 15)),
            new BlockOccupancy(new Vector3i(3, 6, 16)),
            new BlockOccupancy(new Vector3i(3, 6, 17)),
            new BlockOccupancy(new Vector3i(3, 6, 18)),
            new BlockOccupancy(new Vector3i(3, 6, 19)),
            new BlockOccupancy(new Vector3i(3, 6, 20)),
            new BlockOccupancy(new Vector3i(3, 6, 21)),
            new BlockOccupancy(new Vector3i(3, 7, 0)),
            new BlockOccupancy(new Vector3i(3, 7, 1)),
            new BlockOccupancy(new Vector3i(3, 7, 2)),
            new BlockOccupancy(new Vector3i(3, 7, 3)),
            new BlockOccupancy(new Vector3i(3, 7, 4)),
            new BlockOccupancy(new Vector3i(3, 7, 5)),
            new BlockOccupancy(new Vector3i(3, 7, 6)),
            new BlockOccupancy(new Vector3i(3, 7, 7)),
            new BlockOccupancy(new Vector3i(3, 7, 8)),
            new BlockOccupancy(new Vector3i(3, 7, 9)),
            new BlockOccupancy(new Vector3i(3, 7, 10)),
            new BlockOccupancy(new Vector3i(3, 7, 11)),
            new BlockOccupancy(new Vector3i(3, 7, 12)),
            new BlockOccupancy(new Vector3i(3, 7, 13)),
            new BlockOccupancy(new Vector3i(3, 7, 14)),
            new BlockOccupancy(new Vector3i(3, 7, 15)),
            new BlockOccupancy(new Vector3i(3, 7, 16)),
            new BlockOccupancy(new Vector3i(3, 7, 17)),
            new BlockOccupancy(new Vector3i(3, 7, 18)),
            new BlockOccupancy(new Vector3i(3, 7, 19)),
            new BlockOccupancy(new Vector3i(3, 7, 20)),
            new BlockOccupancy(new Vector3i(3, 7, 21)),
            new BlockOccupancy(new Vector3i(3, 8, 0)),
            new BlockOccupancy(new Vector3i(3, 8, 1)),
            new BlockOccupancy(new Vector3i(3, 8, 2)),
            new BlockOccupancy(new Vector3i(3, 8, 3)),
            new BlockOccupancy(new Vector3i(3, 8, 4)),
            new BlockOccupancy(new Vector3i(3, 8, 5)),
            new BlockOccupancy(new Vector3i(3, 8, 6)),
            new BlockOccupancy(new Vector3i(3, 8, 7)),
            new BlockOccupancy(new Vector3i(3, 8, 8)),
            new BlockOccupancy(new Vector3i(3, 8, 9)),
            new BlockOccupancy(new Vector3i(3, 8, 10)),
            new BlockOccupancy(new Vector3i(3, 8, 11)),
            new BlockOccupancy(new Vector3i(3, 8, 12)),
            new BlockOccupancy(new Vector3i(3, 8, 13)),
            new BlockOccupancy(new Vector3i(3, 8, 14)),
            new BlockOccupancy(new Vector3i(3, 8, 15)),
            new BlockOccupancy(new Vector3i(3, 8, 16)),
            new BlockOccupancy(new Vector3i(3, 8, 17)),
            new BlockOccupancy(new Vector3i(3, 8, 18)),
            new BlockOccupancy(new Vector3i(3, 8, 19)),
            new BlockOccupancy(new Vector3i(3, 8, 20)),
            new BlockOccupancy(new Vector3i(3, 8, 21)),
            new BlockOccupancy(new Vector3i(3, 9, 0)),
            new BlockOccupancy(new Vector3i(3, 9, 1)),
            new BlockOccupancy(new Vector3i(3, 9, 2)),
            new BlockOccupancy(new Vector3i(3, 9, 3)),
            new BlockOccupancy(new Vector3i(3, 9, 4)),
            new BlockOccupancy(new Vector3i(3, 9, 5)),
            new BlockOccupancy(new Vector3i(3, 9, 6)),
            new BlockOccupancy(new Vector3i(3, 9, 7)),
            new BlockOccupancy(new Vector3i(3, 9, 8)),
            new BlockOccupancy(new Vector3i(3, 9, 9)),
            new BlockOccupancy(new Vector3i(3, 9, 10)),
            new BlockOccupancy(new Vector3i(3, 9, 11)),
            new BlockOccupancy(new Vector3i(3, 9, 12)),
            new BlockOccupancy(new Vector3i(3, 9, 13)),
            new BlockOccupancy(new Vector3i(3, 9, 14)),
            new BlockOccupancy(new Vector3i(3, 9, 15)),
            new BlockOccupancy(new Vector3i(3, 9, 16)),
            new BlockOccupancy(new Vector3i(3, 9, 17)),
            new BlockOccupancy(new Vector3i(3, 9, 18)),
            new BlockOccupancy(new Vector3i(3, 9, 19)),
            new BlockOccupancy(new Vector3i(3, 9, 20)),
            new BlockOccupancy(new Vector3i(3, 9, 21)),
            new BlockOccupancy(new Vector3i(4, 0, 0)),
            new BlockOccupancy(new Vector3i(4, 0, 1)),
            new BlockOccupancy(new Vector3i(4, 0, 2)),
            new BlockOccupancy(new Vector3i(4, 0, 3)),
            new BlockOccupancy(new Vector3i(4, 0, 4)),
            new BlockOccupancy(new Vector3i(4, 0, 5)),
            new BlockOccupancy(new Vector3i(4, 0, 6)),
            new BlockOccupancy(new Vector3i(4, 0, 7)),
            new BlockOccupancy(new Vector3i(4, 0, 8)),
            new BlockOccupancy(new Vector3i(4, 0, 9)),
            new BlockOccupancy(new Vector3i(4, 0, 10)),
            new BlockOccupancy(new Vector3i(4, 0, 11)),
            new BlockOccupancy(new Vector3i(4, 0, 12)),
            new BlockOccupancy(new Vector3i(4, 0, 13)),
            new BlockOccupancy(new Vector3i(4, 0, 14)),
            new BlockOccupancy(new Vector3i(4, 0, 15)),
            new BlockOccupancy(new Vector3i(4, 0, 16)),
            new BlockOccupancy(new Vector3i(4, 0, 17)),
            new BlockOccupancy(new Vector3i(4, 0, 18)),
            new BlockOccupancy(new Vector3i(4, 0, 19)),
            new BlockOccupancy(new Vector3i(4, 0, 20)),
            new BlockOccupancy(new Vector3i(4, 0, 21)),
            new BlockOccupancy(new Vector3i(4, 1, 0)),
            new BlockOccupancy(new Vector3i(4, 1, 1)),
            new BlockOccupancy(new Vector3i(4, 1, 2)),
            new BlockOccupancy(new Vector3i(4, 1, 3)),
            new BlockOccupancy(new Vector3i(4, 1, 4)),
            new BlockOccupancy(new Vector3i(4, 1, 5)),
            new BlockOccupancy(new Vector3i(4, 1, 6)),
            new BlockOccupancy(new Vector3i(4, 1, 7)),
            new BlockOccupancy(new Vector3i(4, 1, 8)),
            new BlockOccupancy(new Vector3i(4, 1, 9)),
            new BlockOccupancy(new Vector3i(4, 1, 10)),
            new BlockOccupancy(new Vector3i(4, 1, 11)),
            new BlockOccupancy(new Vector3i(4, 1, 12)),
            new BlockOccupancy(new Vector3i(4, 1, 13)),
            new BlockOccupancy(new Vector3i(4, 1, 14)),
            new BlockOccupancy(new Vector3i(4, 1, 15)),
            new BlockOccupancy(new Vector3i(4, 1, 16)),
            new BlockOccupancy(new Vector3i(4, 1, 17)),
            new BlockOccupancy(new Vector3i(4, 1, 18)),
            new BlockOccupancy(new Vector3i(4, 1, 19)),
            new BlockOccupancy(new Vector3i(4, 1, 20)),
            new BlockOccupancy(new Vector3i(4, 1, 21)),
            new BlockOccupancy(new Vector3i(4, 2, 0)),
            new BlockOccupancy(new Vector3i(4, 2, 1)),
            new BlockOccupancy(new Vector3i(4, 2, 2)),
            new BlockOccupancy(new Vector3i(4, 2, 3)),
            new BlockOccupancy(new Vector3i(4, 2, 4)),
            new BlockOccupancy(new Vector3i(4, 2, 5)),
            new BlockOccupancy(new Vector3i(4, 2, 6)),
            new BlockOccupancy(new Vector3i(4, 2, 7)),
            new BlockOccupancy(new Vector3i(4, 2, 8)),
            new BlockOccupancy(new Vector3i(4, 2, 9)),
            new BlockOccupancy(new Vector3i(4, 2, 10)),
            new BlockOccupancy(new Vector3i(4, 2, 11)),
            new BlockOccupancy(new Vector3i(4, 2, 12)),
            new BlockOccupancy(new Vector3i(4, 2, 13)),
            new BlockOccupancy(new Vector3i(4, 2, 14)),
            new BlockOccupancy(new Vector3i(4, 2, 15)),
            new BlockOccupancy(new Vector3i(4, 2, 16)),
            new BlockOccupancy(new Vector3i(4, 2, 17)),
            new BlockOccupancy(new Vector3i(4, 2, 18)),
            new BlockOccupancy(new Vector3i(4, 2, 19)),
            new BlockOccupancy(new Vector3i(4, 2, 20)),
            new BlockOccupancy(new Vector3i(4, 2, 21)),
            new BlockOccupancy(new Vector3i(4, 3, 0)),
            new BlockOccupancy(new Vector3i(4, 3, 1)),
            new BlockOccupancy(new Vector3i(4, 3, 2)),
            new BlockOccupancy(new Vector3i(4, 3, 3)),
            new BlockOccupancy(new Vector3i(4, 3, 4)),
            new BlockOccupancy(new Vector3i(4, 3, 5)),
            new BlockOccupancy(new Vector3i(4, 3, 6)),
            new BlockOccupancy(new Vector3i(4, 3, 7)),
            new BlockOccupancy(new Vector3i(4, 3, 8)),
            new BlockOccupancy(new Vector3i(4, 3, 9)),
            new BlockOccupancy(new Vector3i(4, 3, 10)),
            new BlockOccupancy(new Vector3i(4, 3, 11)),
            new BlockOccupancy(new Vector3i(4, 3, 12)),
            new BlockOccupancy(new Vector3i(4, 3, 13)),
            new BlockOccupancy(new Vector3i(4, 3, 14)),
            new BlockOccupancy(new Vector3i(4, 3, 15)),
            new BlockOccupancy(new Vector3i(4, 3, 16)),
            new BlockOccupancy(new Vector3i(4, 3, 17)),
            new BlockOccupancy(new Vector3i(4, 3, 18)),
            new BlockOccupancy(new Vector3i(4, 3, 19)),
            new BlockOccupancy(new Vector3i(4, 3, 20)),
            new BlockOccupancy(new Vector3i(4, 3, 21)),
            new BlockOccupancy(new Vector3i(4, 4, 0)),
            new BlockOccupancy(new Vector3i(4, 4, 1)),
            new BlockOccupancy(new Vector3i(4, 4, 2)),
            new BlockOccupancy(new Vector3i(4, 4, 3)),
            new BlockOccupancy(new Vector3i(4, 4, 4)),
            new BlockOccupancy(new Vector3i(4, 4, 5)),
            new BlockOccupancy(new Vector3i(4, 4, 6)),
            new BlockOccupancy(new Vector3i(4, 4, 7)),
            new BlockOccupancy(new Vector3i(4, 4, 8)),
            new BlockOccupancy(new Vector3i(4, 4, 9)),
            new BlockOccupancy(new Vector3i(4, 4, 10)),
            new BlockOccupancy(new Vector3i(4, 4, 11)),
            new BlockOccupancy(new Vector3i(4, 4, 12)),
            new BlockOccupancy(new Vector3i(4, 4, 13)),
            new BlockOccupancy(new Vector3i(4, 4, 14)),
            new BlockOccupancy(new Vector3i(4, 4, 15)),
            new BlockOccupancy(new Vector3i(4, 4, 16)),
            new BlockOccupancy(new Vector3i(4, 4, 17)),
            new BlockOccupancy(new Vector3i(4, 4, 18)),
            new BlockOccupancy(new Vector3i(4, 4, 19)),
            new BlockOccupancy(new Vector3i(4, 4, 20)),
            new BlockOccupancy(new Vector3i(4, 4, 21)),
            new BlockOccupancy(new Vector3i(4, 5, 0)),
            new BlockOccupancy(new Vector3i(4, 5, 1)),
            new BlockOccupancy(new Vector3i(4, 5, 2)),
            new BlockOccupancy(new Vector3i(4, 5, 3)),
            new BlockOccupancy(new Vector3i(4, 5, 4)),
            new BlockOccupancy(new Vector3i(4, 5, 5)),
            new BlockOccupancy(new Vector3i(4, 5, 6)),
            new BlockOccupancy(new Vector3i(4, 5, 7)),
            new BlockOccupancy(new Vector3i(4, 5, 8)),
            new BlockOccupancy(new Vector3i(4, 5, 9)),
            new BlockOccupancy(new Vector3i(4, 5, 10)),
            new BlockOccupancy(new Vector3i(4, 5, 11)),
            new BlockOccupancy(new Vector3i(4, 5, 12)),
            new BlockOccupancy(new Vector3i(4, 5, 13)),
            new BlockOccupancy(new Vector3i(4, 5, 14)),
            new BlockOccupancy(new Vector3i(4, 5, 15)),
            new BlockOccupancy(new Vector3i(4, 5, 16)),
            new BlockOccupancy(new Vector3i(4, 5, 17)),
            new BlockOccupancy(new Vector3i(4, 5, 18)),
            new BlockOccupancy(new Vector3i(4, 5, 19)),
            new BlockOccupancy(new Vector3i(4, 5, 20)),
            new BlockOccupancy(new Vector3i(4, 5, 21)),
            new BlockOccupancy(new Vector3i(4, 6, 0)),
            new BlockOccupancy(new Vector3i(4, 6, 1)),
            new BlockOccupancy(new Vector3i(4, 6, 2)),
            new BlockOccupancy(new Vector3i(4, 6, 3)),
            new BlockOccupancy(new Vector3i(4, 6, 4)),
            new BlockOccupancy(new Vector3i(4, 6, 5)),
            new BlockOccupancy(new Vector3i(4, 6, 6)),
            new BlockOccupancy(new Vector3i(4, 6, 7)),
            new BlockOccupancy(new Vector3i(4, 6, 8)),
            new BlockOccupancy(new Vector3i(4, 6, 9)),
            new BlockOccupancy(new Vector3i(4, 6, 10)),
            new BlockOccupancy(new Vector3i(4, 6, 11)),
            new BlockOccupancy(new Vector3i(4, 6, 12)),
            new BlockOccupancy(new Vector3i(4, 6, 13)),
            new BlockOccupancy(new Vector3i(4, 6, 14)),
            new BlockOccupancy(new Vector3i(4, 6, 15)),
            new BlockOccupancy(new Vector3i(4, 6, 16)),
            new BlockOccupancy(new Vector3i(4, 6, 17)),
            new BlockOccupancy(new Vector3i(4, 6, 18)),
            new BlockOccupancy(new Vector3i(4, 6, 19)),
            new BlockOccupancy(new Vector3i(4, 6, 20)),
            new BlockOccupancy(new Vector3i(4, 6, 21)),
            new BlockOccupancy(new Vector3i(4, 7, 0)),
            new BlockOccupancy(new Vector3i(4, 7, 1)),
            new BlockOccupancy(new Vector3i(4, 7, 2)),
            new BlockOccupancy(new Vector3i(4, 7, 3)),
            new BlockOccupancy(new Vector3i(4, 7, 4)),
            new BlockOccupancy(new Vector3i(4, 7, 5)),
            new BlockOccupancy(new Vector3i(4, 7, 6)),
            new BlockOccupancy(new Vector3i(4, 7, 7)),
            new BlockOccupancy(new Vector3i(4, 7, 8)),
            new BlockOccupancy(new Vector3i(4, 7, 9)),
            new BlockOccupancy(new Vector3i(4, 7, 10)),
            new BlockOccupancy(new Vector3i(4, 7, 11)),
            new BlockOccupancy(new Vector3i(4, 7, 12)),
            new BlockOccupancy(new Vector3i(4, 7, 13)),
            new BlockOccupancy(new Vector3i(4, 7, 14)),
            new BlockOccupancy(new Vector3i(4, 7, 15)),
            new BlockOccupancy(new Vector3i(4, 7, 16)),
            new BlockOccupancy(new Vector3i(4, 7, 17)),
            new BlockOccupancy(new Vector3i(4, 7, 18)),
            new BlockOccupancy(new Vector3i(4, 7, 19)),
            new BlockOccupancy(new Vector3i(4, 7, 20)),
            new BlockOccupancy(new Vector3i(4, 7, 21)),
            new BlockOccupancy(new Vector3i(4, 8, 0)),
            new BlockOccupancy(new Vector3i(4, 8, 1)),
            new BlockOccupancy(new Vector3i(4, 8, 2)),
            new BlockOccupancy(new Vector3i(4, 8, 3)),
            new BlockOccupancy(new Vector3i(4, 8, 4)),
            new BlockOccupancy(new Vector3i(4, 8, 5)),
            new BlockOccupancy(new Vector3i(4, 8, 6)),
            new BlockOccupancy(new Vector3i(4, 8, 7)),
            new BlockOccupancy(new Vector3i(4, 8, 8)),
            new BlockOccupancy(new Vector3i(4, 8, 9)),
            new BlockOccupancy(new Vector3i(4, 8, 10)),
            new BlockOccupancy(new Vector3i(4, 8, 11)),
            new BlockOccupancy(new Vector3i(4, 8, 12)),
            new BlockOccupancy(new Vector3i(4, 8, 13)),
            new BlockOccupancy(new Vector3i(4, 8, 14)),
            new BlockOccupancy(new Vector3i(4, 8, 15)),
            new BlockOccupancy(new Vector3i(4, 8, 16)),
            new BlockOccupancy(new Vector3i(4, 8, 17)),
            new BlockOccupancy(new Vector3i(4, 8, 18)),
            new BlockOccupancy(new Vector3i(4, 8, 19)),
            new BlockOccupancy(new Vector3i(4, 8, 20)),
            new BlockOccupancy(new Vector3i(4, 8, 21)),
            new BlockOccupancy(new Vector3i(4, 9, 0)),
            new BlockOccupancy(new Vector3i(4, 9, 1)),
            new BlockOccupancy(new Vector3i(4, 9, 2)),
            new BlockOccupancy(new Vector3i(4, 9, 3)),
            new BlockOccupancy(new Vector3i(4, 9, 4)),
            new BlockOccupancy(new Vector3i(4, 9, 5)),
            new BlockOccupancy(new Vector3i(4, 9, 6)),
            new BlockOccupancy(new Vector3i(4, 9, 7)),
            new BlockOccupancy(new Vector3i(4, 9, 8)),
            new BlockOccupancy(new Vector3i(4, 9, 9)),
            new BlockOccupancy(new Vector3i(4, 9, 10)),
            new BlockOccupancy(new Vector3i(4, 9, 11)),
            new BlockOccupancy(new Vector3i(4, 9, 12)),
            new BlockOccupancy(new Vector3i(4, 9, 13)),
            new BlockOccupancy(new Vector3i(4, 9, 14)),
            new BlockOccupancy(new Vector3i(4, 9, 15)),
            new BlockOccupancy(new Vector3i(4, 9, 16)),
            new BlockOccupancy(new Vector3i(4, 9, 17)),
            new BlockOccupancy(new Vector3i(4, 9, 18)),
            new BlockOccupancy(new Vector3i(4, 9, 19)),
            new BlockOccupancy(new Vector3i(4, 9, 20)),
            new BlockOccupancy(new Vector3i(4, 9, 21)),
            new BlockOccupancy(new Vector3i(5, 0, 0)),
            new BlockOccupancy(new Vector3i(5, 0, 1)),
            new BlockOccupancy(new Vector3i(5, 0, 2)),
            new BlockOccupancy(new Vector3i(5, 0, 3)),
            new BlockOccupancy(new Vector3i(5, 0, 4)),
            new BlockOccupancy(new Vector3i(5, 0, 5)),
            new BlockOccupancy(new Vector3i(5, 0, 6)),
            new BlockOccupancy(new Vector3i(5, 0, 7)),
            new BlockOccupancy(new Vector3i(5, 0, 8)),
            new BlockOccupancy(new Vector3i(5, 0, 9)),
            new BlockOccupancy(new Vector3i(5, 0, 10)),
            new BlockOccupancy(new Vector3i(5, 0, 11)),
            new BlockOccupancy(new Vector3i(5, 0, 12)),
            new BlockOccupancy(new Vector3i(5, 0, 13)),
            new BlockOccupancy(new Vector3i(5, 0, 14)),
            new BlockOccupancy(new Vector3i(5, 0, 15)),
            new BlockOccupancy(new Vector3i(5, 0, 16)),
            new BlockOccupancy(new Vector3i(5, 0, 17)),
            new BlockOccupancy(new Vector3i(5, 0, 18)),
            new BlockOccupancy(new Vector3i(5, 0, 19)),
            new BlockOccupancy(new Vector3i(5, 0, 20)),
            new BlockOccupancy(new Vector3i(5, 0, 21)),
            new BlockOccupancy(new Vector3i(5, 1, 0)),
            new BlockOccupancy(new Vector3i(5, 1, 1)),
            new BlockOccupancy(new Vector3i(5, 1, 2)),
            new BlockOccupancy(new Vector3i(5, 1, 3)),
            new BlockOccupancy(new Vector3i(5, 1, 4)),
            new BlockOccupancy(new Vector3i(5, 1, 5)),
            new BlockOccupancy(new Vector3i(5, 1, 6)),
            new BlockOccupancy(new Vector3i(5, 1, 7)),
            new BlockOccupancy(new Vector3i(5, 1, 8)),
            new BlockOccupancy(new Vector3i(5, 1, 9)),
            new BlockOccupancy(new Vector3i(5, 1, 10)),
            new BlockOccupancy(new Vector3i(5, 1, 11)),
            new BlockOccupancy(new Vector3i(5, 1, 12)),
            new BlockOccupancy(new Vector3i(5, 1, 13)),
            new BlockOccupancy(new Vector3i(5, 1, 14)),
            new BlockOccupancy(new Vector3i(5, 1, 15)),
            new BlockOccupancy(new Vector3i(5, 1, 16)),
            new BlockOccupancy(new Vector3i(5, 1, 17)),
            new BlockOccupancy(new Vector3i(5, 1, 18)),
            new BlockOccupancy(new Vector3i(5, 1, 19)),
            new BlockOccupancy(new Vector3i(5, 1, 20)),
            new BlockOccupancy(new Vector3i(5, 1, 21)),
            new BlockOccupancy(new Vector3i(5, 2, 0)),
            new BlockOccupancy(new Vector3i(5, 2, 1)),
            new BlockOccupancy(new Vector3i(5, 2, 2)),
            new BlockOccupancy(new Vector3i(5, 2, 3)),
            new BlockOccupancy(new Vector3i(5, 2, 4)),
            new BlockOccupancy(new Vector3i(5, 2, 5)),
            new BlockOccupancy(new Vector3i(5, 2, 6)),
            new BlockOccupancy(new Vector3i(5, 2, 7)),
            new BlockOccupancy(new Vector3i(5, 2, 8)),
            new BlockOccupancy(new Vector3i(5, 2, 9)),
            new BlockOccupancy(new Vector3i(5, 2, 10)),
            new BlockOccupancy(new Vector3i(5, 2, 11)),
            new BlockOccupancy(new Vector3i(5, 2, 12)),
            new BlockOccupancy(new Vector3i(5, 2, 13)),
            new BlockOccupancy(new Vector3i(5, 2, 14)),
            new BlockOccupancy(new Vector3i(5, 2, 15)),
            new BlockOccupancy(new Vector3i(5, 2, 16)),
            new BlockOccupancy(new Vector3i(5, 2, 17)),
            new BlockOccupancy(new Vector3i(5, 2, 18)),
            new BlockOccupancy(new Vector3i(5, 2, 19)),
            new BlockOccupancy(new Vector3i(5, 2, 20)),
            new BlockOccupancy(new Vector3i(5, 2, 21)),
            new BlockOccupancy(new Vector3i(5, 3, 0)),
            new BlockOccupancy(new Vector3i(5, 3, 1)),
            new BlockOccupancy(new Vector3i(5, 3, 2)),
            new BlockOccupancy(new Vector3i(5, 3, 3)),
            new BlockOccupancy(new Vector3i(5, 3, 4)),
            new BlockOccupancy(new Vector3i(5, 3, 5)),
            new BlockOccupancy(new Vector3i(5, 3, 6)),
            new BlockOccupancy(new Vector3i(5, 3, 7)),
            new BlockOccupancy(new Vector3i(5, 3, 8)),
            new BlockOccupancy(new Vector3i(5, 3, 9)),
            new BlockOccupancy(new Vector3i(5, 3, 10)),
            new BlockOccupancy(new Vector3i(5, 3, 11)),
            new BlockOccupancy(new Vector3i(5, 3, 12)),
            new BlockOccupancy(new Vector3i(5, 3, 13)),
            new BlockOccupancy(new Vector3i(5, 3, 14)),
            new BlockOccupancy(new Vector3i(5, 3, 15)),
            new BlockOccupancy(new Vector3i(5, 3, 16)),
            new BlockOccupancy(new Vector3i(5, 3, 17)),
            new BlockOccupancy(new Vector3i(5, 3, 18)),
            new BlockOccupancy(new Vector3i(5, 3, 19)),
            new BlockOccupancy(new Vector3i(5, 3, 20)),
            new BlockOccupancy(new Vector3i(5, 3, 21)),
            new BlockOccupancy(new Vector3i(5, 4, 0)),
            new BlockOccupancy(new Vector3i(5, 4, 1)),
            new BlockOccupancy(new Vector3i(5, 4, 2)),
            new BlockOccupancy(new Vector3i(5, 4, 3)),
            new BlockOccupancy(new Vector3i(5, 4, 4)),
            new BlockOccupancy(new Vector3i(5, 4, 5)),
            new BlockOccupancy(new Vector3i(5, 4, 6)),
            new BlockOccupancy(new Vector3i(5, 4, 7)),
            new BlockOccupancy(new Vector3i(5, 4, 8)),
            new BlockOccupancy(new Vector3i(5, 4, 9)),
            new BlockOccupancy(new Vector3i(5, 4, 10)),
            new BlockOccupancy(new Vector3i(5, 4, 11)),
            new BlockOccupancy(new Vector3i(5, 4, 12)),
            new BlockOccupancy(new Vector3i(5, 4, 13)),
            new BlockOccupancy(new Vector3i(5, 4, 14)),
            new BlockOccupancy(new Vector3i(5, 4, 15)),
            new BlockOccupancy(new Vector3i(5, 4, 16)),
            new BlockOccupancy(new Vector3i(5, 4, 17)),
            new BlockOccupancy(new Vector3i(5, 4, 18)),
            new BlockOccupancy(new Vector3i(5, 4, 19)),
            new BlockOccupancy(new Vector3i(5, 4, 20)),
            new BlockOccupancy(new Vector3i(5, 4, 21)),
            new BlockOccupancy(new Vector3i(5, 5, 0)),
            new BlockOccupancy(new Vector3i(5, 5, 1)),
            new BlockOccupancy(new Vector3i(5, 5, 2)),
            new BlockOccupancy(new Vector3i(5, 5, 3)),
            new BlockOccupancy(new Vector3i(5, 5, 4)),
            new BlockOccupancy(new Vector3i(5, 5, 5)),
            new BlockOccupancy(new Vector3i(5, 5, 6)),
            new BlockOccupancy(new Vector3i(5, 5, 7)),
            new BlockOccupancy(new Vector3i(5, 5, 8)),
            new BlockOccupancy(new Vector3i(5, 5, 9)),
            new BlockOccupancy(new Vector3i(5, 5, 10)),
            new BlockOccupancy(new Vector3i(5, 5, 11)),
            new BlockOccupancy(new Vector3i(5, 5, 12)),
            new BlockOccupancy(new Vector3i(5, 5, 13)),
            new BlockOccupancy(new Vector3i(5, 5, 14)),
            new BlockOccupancy(new Vector3i(5, 5, 15)),
            new BlockOccupancy(new Vector3i(5, 5, 16)),
            new BlockOccupancy(new Vector3i(5, 5, 17)),
            new BlockOccupancy(new Vector3i(5, 5, 18)),
            new BlockOccupancy(new Vector3i(5, 5, 19)),
            new BlockOccupancy(new Vector3i(5, 5, 20)),
            new BlockOccupancy(new Vector3i(5, 5, 21)),
            new BlockOccupancy(new Vector3i(5, 6, 0)),
            new BlockOccupancy(new Vector3i(5, 6, 1)),
            new BlockOccupancy(new Vector3i(5, 6, 2)),
            new BlockOccupancy(new Vector3i(5, 6, 3)),
            new BlockOccupancy(new Vector3i(5, 6, 4)),
            new BlockOccupancy(new Vector3i(5, 6, 5)),
            new BlockOccupancy(new Vector3i(5, 6, 6)),
            new BlockOccupancy(new Vector3i(5, 6, 7)),
            new BlockOccupancy(new Vector3i(5, 6, 8)),
            new BlockOccupancy(new Vector3i(5, 6, 9)),
            new BlockOccupancy(new Vector3i(5, 6, 10)),
            new BlockOccupancy(new Vector3i(5, 6, 11)),
            new BlockOccupancy(new Vector3i(5, 6, 12)),
            new BlockOccupancy(new Vector3i(5, 6, 13)),
            new BlockOccupancy(new Vector3i(5, 6, 14)),
            new BlockOccupancy(new Vector3i(5, 6, 15)),
            new BlockOccupancy(new Vector3i(5, 6, 16)),
            new BlockOccupancy(new Vector3i(5, 6, 17)),
            new BlockOccupancy(new Vector3i(5, 6, 18)),
            new BlockOccupancy(new Vector3i(5, 6, 19)),
            new BlockOccupancy(new Vector3i(5, 6, 20)),
            new BlockOccupancy(new Vector3i(5, 6, 21)),
            new BlockOccupancy(new Vector3i(5, 7, 0)),
            new BlockOccupancy(new Vector3i(5, 7, 1)),
            new BlockOccupancy(new Vector3i(5, 7, 2)),
            new BlockOccupancy(new Vector3i(5, 7, 3)),
            new BlockOccupancy(new Vector3i(5, 7, 4)),
            new BlockOccupancy(new Vector3i(5, 7, 5)),
            new BlockOccupancy(new Vector3i(5, 7, 6)),
            new BlockOccupancy(new Vector3i(5, 7, 7)),
            new BlockOccupancy(new Vector3i(5, 7, 8)),
            new BlockOccupancy(new Vector3i(5, 7, 9)),
            new BlockOccupancy(new Vector3i(5, 7, 10)),
            new BlockOccupancy(new Vector3i(5, 7, 11)),
            new BlockOccupancy(new Vector3i(5, 7, 12)),
            new BlockOccupancy(new Vector3i(5, 7, 13)),
            new BlockOccupancy(new Vector3i(5, 7, 14)),
            new BlockOccupancy(new Vector3i(5, 7, 15)),
            new BlockOccupancy(new Vector3i(5, 7, 16)),
            new BlockOccupancy(new Vector3i(5, 7, 17)),
            new BlockOccupancy(new Vector3i(5, 7, 18)),
            new BlockOccupancy(new Vector3i(5, 7, 19)),
            new BlockOccupancy(new Vector3i(5, 7, 20)),
            new BlockOccupancy(new Vector3i(5, 7, 21)),
            new BlockOccupancy(new Vector3i(5, 8, 0)),
            new BlockOccupancy(new Vector3i(5, 8, 1)),
            new BlockOccupancy(new Vector3i(5, 8, 2)),
            new BlockOccupancy(new Vector3i(5, 8, 3)),
            new BlockOccupancy(new Vector3i(5, 8, 4)),
            new BlockOccupancy(new Vector3i(5, 8, 5)),
            new BlockOccupancy(new Vector3i(5, 8, 6)),
            new BlockOccupancy(new Vector3i(5, 8, 7)),
            new BlockOccupancy(new Vector3i(5, 8, 8)),
            new BlockOccupancy(new Vector3i(5, 8, 9)),
            new BlockOccupancy(new Vector3i(5, 8, 10)),
            new BlockOccupancy(new Vector3i(5, 8, 11)),
            new BlockOccupancy(new Vector3i(5, 8, 12)),
            new BlockOccupancy(new Vector3i(5, 8, 13)),
            new BlockOccupancy(new Vector3i(5, 8, 14)),
            new BlockOccupancy(new Vector3i(5, 8, 15)),
            new BlockOccupancy(new Vector3i(5, 8, 16)),
            new BlockOccupancy(new Vector3i(5, 8, 17)),
            new BlockOccupancy(new Vector3i(5, 8, 18)),
            new BlockOccupancy(new Vector3i(5, 8, 19)),
            new BlockOccupancy(new Vector3i(5, 8, 20)),
            new BlockOccupancy(new Vector3i(5, 8, 21)),
            new BlockOccupancy(new Vector3i(5, 9, 0)),
            new BlockOccupancy(new Vector3i(5, 9, 1)),
            new BlockOccupancy(new Vector3i(5, 9, 2)),
            new BlockOccupancy(new Vector3i(5, 9, 3)),
            new BlockOccupancy(new Vector3i(5, 9, 4)),
            new BlockOccupancy(new Vector3i(5, 9, 5)),
            new BlockOccupancy(new Vector3i(5, 9, 6)),
            new BlockOccupancy(new Vector3i(5, 9, 7)),
            new BlockOccupancy(new Vector3i(5, 9, 8)),
            new BlockOccupancy(new Vector3i(5, 9, 9)),
            new BlockOccupancy(new Vector3i(5, 9, 10)),
            new BlockOccupancy(new Vector3i(5, 9, 11)),
            new BlockOccupancy(new Vector3i(5, 9, 12)),
            new BlockOccupancy(new Vector3i(5, 9, 13)),
            new BlockOccupancy(new Vector3i(5, 9, 14)),
            new BlockOccupancy(new Vector3i(5, 9, 15)),
            new BlockOccupancy(new Vector3i(5, 9, 16)),
            new BlockOccupancy(new Vector3i(5, 9, 17)),
            new BlockOccupancy(new Vector3i(5, 9, 18)),
            new BlockOccupancy(new Vector3i(5, 9, 19)),
            new BlockOccupancy(new Vector3i(5, 9, 20)),
            new BlockOccupancy(new Vector3i(5, 9, 21)),
            new BlockOccupancy(new Vector3i(6, 0, 0)),
            new BlockOccupancy(new Vector3i(6, 0, 1)),
            new BlockOccupancy(new Vector3i(6, 0, 2)),
            new BlockOccupancy(new Vector3i(6, 0, 3)),
            new BlockOccupancy(new Vector3i(6, 0, 4)),
            new BlockOccupancy(new Vector3i(6, 0, 5)),
            new BlockOccupancy(new Vector3i(6, 0, 6)),
            new BlockOccupancy(new Vector3i(6, 0, 7)),
            new BlockOccupancy(new Vector3i(6, 0, 8)),
            new BlockOccupancy(new Vector3i(6, 0, 9)),
            new BlockOccupancy(new Vector3i(6, 0, 10)),
            new BlockOccupancy(new Vector3i(6, 0, 11)),
            new BlockOccupancy(new Vector3i(6, 0, 12)),
            new BlockOccupancy(new Vector3i(6, 0, 13)),
            new BlockOccupancy(new Vector3i(6, 0, 14)),
            new BlockOccupancy(new Vector3i(6, 0, 15)),
            new BlockOccupancy(new Vector3i(6, 0, 16)),
            new BlockOccupancy(new Vector3i(6, 0, 17)),
            new BlockOccupancy(new Vector3i(6, 0, 18)),
            new BlockOccupancy(new Vector3i(6, 0, 19)),
            new BlockOccupancy(new Vector3i(6, 0, 20)),
            new BlockOccupancy(new Vector3i(6, 0, 21)),
            new BlockOccupancy(new Vector3i(6, 1, 0)),
            new BlockOccupancy(new Vector3i(6, 1, 1)),
            new BlockOccupancy(new Vector3i(6, 1, 2)),
            new BlockOccupancy(new Vector3i(6, 1, 3)),
            new BlockOccupancy(new Vector3i(6, 1, 4)),
            new BlockOccupancy(new Vector3i(6, 1, 5)),
            new BlockOccupancy(new Vector3i(6, 1, 6)),
            new BlockOccupancy(new Vector3i(6, 1, 7)),
            new BlockOccupancy(new Vector3i(6, 1, 8)),
            new BlockOccupancy(new Vector3i(6, 1, 9)),
            new BlockOccupancy(new Vector3i(6, 1, 10)),
            new BlockOccupancy(new Vector3i(6, 1, 11)),
            new BlockOccupancy(new Vector3i(6, 1, 12)),
            new BlockOccupancy(new Vector3i(6, 1, 13)),
            new BlockOccupancy(new Vector3i(6, 1, 14)),
            new BlockOccupancy(new Vector3i(6, 1, 15)),
            new BlockOccupancy(new Vector3i(6, 1, 16)),
            new BlockOccupancy(new Vector3i(6, 1, 17)),
            new BlockOccupancy(new Vector3i(6, 1, 18)),
            new BlockOccupancy(new Vector3i(6, 1, 19)),
            new BlockOccupancy(new Vector3i(6, 1, 20)),
            new BlockOccupancy(new Vector3i(6, 1, 21)),
            new BlockOccupancy(new Vector3i(6, 2, 0)),
            new BlockOccupancy(new Vector3i(6, 2, 1)),
            new BlockOccupancy(new Vector3i(6, 2, 2)),
            new BlockOccupancy(new Vector3i(6, 2, 3)),
            new BlockOccupancy(new Vector3i(6, 2, 4)),
            new BlockOccupancy(new Vector3i(6, 2, 5)),
            new BlockOccupancy(new Vector3i(6, 2, 6)),
            new BlockOccupancy(new Vector3i(6, 2, 7)),
            new BlockOccupancy(new Vector3i(6, 2, 8)),
            new BlockOccupancy(new Vector3i(6, 2, 9)),
            new BlockOccupancy(new Vector3i(6, 2, 10)),
            new BlockOccupancy(new Vector3i(6, 2, 11)),
            new BlockOccupancy(new Vector3i(6, 2, 12)),
            new BlockOccupancy(new Vector3i(6, 2, 13)),
            new BlockOccupancy(new Vector3i(6, 2, 14)),
            new BlockOccupancy(new Vector3i(6, 2, 15)),
            new BlockOccupancy(new Vector3i(6, 2, 16)),
            new BlockOccupancy(new Vector3i(6, 2, 17)),
            new BlockOccupancy(new Vector3i(6, 2, 18)),
            new BlockOccupancy(new Vector3i(6, 2, 19)),
            new BlockOccupancy(new Vector3i(6, 2, 20)),
            new BlockOccupancy(new Vector3i(6, 2, 21)),
            new BlockOccupancy(new Vector3i(6, 3, 0)),
            new BlockOccupancy(new Vector3i(6, 3, 1)),
            new BlockOccupancy(new Vector3i(6, 3, 2)),
            new BlockOccupancy(new Vector3i(6, 3, 3)),
            new BlockOccupancy(new Vector3i(6, 3, 4)),
            new BlockOccupancy(new Vector3i(6, 3, 5)),
            new BlockOccupancy(new Vector3i(6, 3, 6)),
            new BlockOccupancy(new Vector3i(6, 3, 7)),
            new BlockOccupancy(new Vector3i(6, 3, 8)),
            new BlockOccupancy(new Vector3i(6, 3, 9)),
            new BlockOccupancy(new Vector3i(6, 3, 10)),
            new BlockOccupancy(new Vector3i(6, 3, 11)),
            new BlockOccupancy(new Vector3i(6, 3, 12)),
            new BlockOccupancy(new Vector3i(6, 3, 13)),
            new BlockOccupancy(new Vector3i(6, 3, 14)),
            new BlockOccupancy(new Vector3i(6, 3, 15)),
            new BlockOccupancy(new Vector3i(6, 3, 16)),
            new BlockOccupancy(new Vector3i(6, 3, 17)),
            new BlockOccupancy(new Vector3i(6, 3, 18)),
            new BlockOccupancy(new Vector3i(6, 3, 19)),
            new BlockOccupancy(new Vector3i(6, 3, 20)),
            new BlockOccupancy(new Vector3i(6, 3, 21)),
            new BlockOccupancy(new Vector3i(6, 4, 0)),
            new BlockOccupancy(new Vector3i(6, 4, 1)),
            new BlockOccupancy(new Vector3i(6, 4, 2)),
            new BlockOccupancy(new Vector3i(6, 4, 3)),
            new BlockOccupancy(new Vector3i(6, 4, 4)),
            new BlockOccupancy(new Vector3i(6, 4, 5)),
            new BlockOccupancy(new Vector3i(6, 4, 6)),
            new BlockOccupancy(new Vector3i(6, 4, 7)),
            new BlockOccupancy(new Vector3i(6, 4, 8)),
            new BlockOccupancy(new Vector3i(6, 4, 9)),
            new BlockOccupancy(new Vector3i(6, 4, 10)),
            new BlockOccupancy(new Vector3i(6, 4, 11)),
            new BlockOccupancy(new Vector3i(6, 4, 12)),
            new BlockOccupancy(new Vector3i(6, 4, 13)),
            new BlockOccupancy(new Vector3i(6, 4, 14)),
            new BlockOccupancy(new Vector3i(6, 4, 15)),
            new BlockOccupancy(new Vector3i(6, 4, 16)),
            new BlockOccupancy(new Vector3i(6, 4, 17)),
            new BlockOccupancy(new Vector3i(6, 4, 18)),
            new BlockOccupancy(new Vector3i(6, 4, 19)),
            new BlockOccupancy(new Vector3i(6, 4, 20)),
            new BlockOccupancy(new Vector3i(6, 4, 21)),
            new BlockOccupancy(new Vector3i(6, 5, 0)),
            new BlockOccupancy(new Vector3i(6, 5, 1)),
            new BlockOccupancy(new Vector3i(6, 5, 2)),
            new BlockOccupancy(new Vector3i(6, 5, 3)),
            new BlockOccupancy(new Vector3i(6, 5, 4)),
            new BlockOccupancy(new Vector3i(6, 5, 5)),
            new BlockOccupancy(new Vector3i(6, 5, 6)),
            new BlockOccupancy(new Vector3i(6, 5, 7)),
            new BlockOccupancy(new Vector3i(6, 5, 8)),
            new BlockOccupancy(new Vector3i(6, 5, 9)),
            new BlockOccupancy(new Vector3i(6, 5, 10)),
            new BlockOccupancy(new Vector3i(6, 5, 11)),
            new BlockOccupancy(new Vector3i(6, 5, 12)),
            new BlockOccupancy(new Vector3i(6, 5, 13)),
            new BlockOccupancy(new Vector3i(6, 5, 14)),
            new BlockOccupancy(new Vector3i(6, 5, 15)),
            new BlockOccupancy(new Vector3i(6, 5, 16)),
            new BlockOccupancy(new Vector3i(6, 5, 17)),
            new BlockOccupancy(new Vector3i(6, 5, 18)),
            new BlockOccupancy(new Vector3i(6, 5, 19)),
            new BlockOccupancy(new Vector3i(6, 5, 20)),
            new BlockOccupancy(new Vector3i(6, 5, 21)),
            new BlockOccupancy(new Vector3i(6, 6, 0)),
            new BlockOccupancy(new Vector3i(6, 6, 1)),
            new BlockOccupancy(new Vector3i(6, 6, 2)),
            new BlockOccupancy(new Vector3i(6, 6, 3)),
            new BlockOccupancy(new Vector3i(6, 6, 4)),
            new BlockOccupancy(new Vector3i(6, 6, 5)),
            new BlockOccupancy(new Vector3i(6, 6, 6)),
            new BlockOccupancy(new Vector3i(6, 6, 7)),
            new BlockOccupancy(new Vector3i(6, 6, 8)),
            new BlockOccupancy(new Vector3i(6, 6, 9)),
            new BlockOccupancy(new Vector3i(6, 6, 10)),
            new BlockOccupancy(new Vector3i(6, 6, 11)),
            new BlockOccupancy(new Vector3i(6, 6, 12)),
            new BlockOccupancy(new Vector3i(6, 6, 13)),
            new BlockOccupancy(new Vector3i(6, 6, 14)),
            new BlockOccupancy(new Vector3i(6, 6, 15)),
            new BlockOccupancy(new Vector3i(6, 6, 16)),
            new BlockOccupancy(new Vector3i(6, 6, 17)),
            new BlockOccupancy(new Vector3i(6, 6, 18)),
            new BlockOccupancy(new Vector3i(6, 6, 19)),
            new BlockOccupancy(new Vector3i(6, 6, 20)),
            new BlockOccupancy(new Vector3i(6, 6, 21)),
            new BlockOccupancy(new Vector3i(6, 7, 0)),
            new BlockOccupancy(new Vector3i(6, 7, 1)),
            new BlockOccupancy(new Vector3i(6, 7, 2)),
            new BlockOccupancy(new Vector3i(6, 7, 3)),
            new BlockOccupancy(new Vector3i(6, 7, 4)),
            new BlockOccupancy(new Vector3i(6, 7, 5)),
            new BlockOccupancy(new Vector3i(6, 7, 6)),
            new BlockOccupancy(new Vector3i(6, 7, 7)),
            new BlockOccupancy(new Vector3i(6, 7, 8)),
            new BlockOccupancy(new Vector3i(6, 7, 9)),
            new BlockOccupancy(new Vector3i(6, 7, 10)),
            new BlockOccupancy(new Vector3i(6, 7, 11)),
            new BlockOccupancy(new Vector3i(6, 7, 12)),
            new BlockOccupancy(new Vector3i(6, 7, 13)),
            new BlockOccupancy(new Vector3i(6, 7, 14)),
            new BlockOccupancy(new Vector3i(6, 7, 15)),
            new BlockOccupancy(new Vector3i(6, 7, 16)),
            new BlockOccupancy(new Vector3i(6, 7, 17)),
            new BlockOccupancy(new Vector3i(6, 7, 18)),
            new BlockOccupancy(new Vector3i(6, 7, 19)),
            new BlockOccupancy(new Vector3i(6, 7, 20)),
            new BlockOccupancy(new Vector3i(6, 7, 21)),
            new BlockOccupancy(new Vector3i(6, 8, 0)),
            new BlockOccupancy(new Vector3i(6, 8, 1)),
            new BlockOccupancy(new Vector3i(6, 8, 2)),
            new BlockOccupancy(new Vector3i(6, 8, 3)),
            new BlockOccupancy(new Vector3i(6, 8, 4)),
            new BlockOccupancy(new Vector3i(6, 8, 5)),
            new BlockOccupancy(new Vector3i(6, 8, 6)),
            new BlockOccupancy(new Vector3i(6, 8, 7)),
            new BlockOccupancy(new Vector3i(6, 8, 8)),
            new BlockOccupancy(new Vector3i(6, 8, 9)),
            new BlockOccupancy(new Vector3i(6, 8, 10)),
            new BlockOccupancy(new Vector3i(6, 8, 11)),
            new BlockOccupancy(new Vector3i(6, 8, 12)),
            new BlockOccupancy(new Vector3i(6, 8, 13)),
            new BlockOccupancy(new Vector3i(6, 8, 14)),
            new BlockOccupancy(new Vector3i(6, 8, 15)),
            new BlockOccupancy(new Vector3i(6, 8, 16)),
            new BlockOccupancy(new Vector3i(6, 8, 17)),
            new BlockOccupancy(new Vector3i(6, 8, 18)),
            new BlockOccupancy(new Vector3i(6, 8, 19)),
            new BlockOccupancy(new Vector3i(6, 8, 20)),
            new BlockOccupancy(new Vector3i(6, 8, 21)),
            new BlockOccupancy(new Vector3i(6, 9, 0)),
            new BlockOccupancy(new Vector3i(6, 9, 1)),
            new BlockOccupancy(new Vector3i(6, 9, 2)),
            new BlockOccupancy(new Vector3i(6, 9, 3)),
            new BlockOccupancy(new Vector3i(6, 9, 4)),
            new BlockOccupancy(new Vector3i(6, 9, 5)),
            new BlockOccupancy(new Vector3i(6, 9, 6)),
            new BlockOccupancy(new Vector3i(6, 9, 7)),
            new BlockOccupancy(new Vector3i(6, 9, 8)),
            new BlockOccupancy(new Vector3i(6, 9, 9)),
            new BlockOccupancy(new Vector3i(6, 9, 10)),
            new BlockOccupancy(new Vector3i(6, 9, 11)),
            new BlockOccupancy(new Vector3i(6, 9, 12)),
            new BlockOccupancy(new Vector3i(6, 9, 13)),
            new BlockOccupancy(new Vector3i(6, 9, 14)),
            new BlockOccupancy(new Vector3i(6, 9, 15)),
            new BlockOccupancy(new Vector3i(6, 9, 16)),
            new BlockOccupancy(new Vector3i(6, 9, 17)),
            new BlockOccupancy(new Vector3i(6, 9, 18)),
            new BlockOccupancy(new Vector3i(6, 9, 19)),
            new BlockOccupancy(new Vector3i(6, 9, 20)),
            new BlockOccupancy(new Vector3i(6, 9, 21)),
            new BlockOccupancy(new Vector3i(7, 0, 0)),
            new BlockOccupancy(new Vector3i(7, 0, 1)),
            new BlockOccupancy(new Vector3i(7, 0, 2)),
            new BlockOccupancy(new Vector3i(7, 0, 3)),
            new BlockOccupancy(new Vector3i(7, 0, 4)),
            new BlockOccupancy(new Vector3i(7, 0, 5)),
            new BlockOccupancy(new Vector3i(7, 0, 6)),
            new BlockOccupancy(new Vector3i(7, 0, 7)),
            new BlockOccupancy(new Vector3i(7, 0, 8)),
            new BlockOccupancy(new Vector3i(7, 0, 9)),
            new BlockOccupancy(new Vector3i(7, 0, 10)),
            new BlockOccupancy(new Vector3i(7, 0, 11)),
            new BlockOccupancy(new Vector3i(7, 0, 12)),
            new BlockOccupancy(new Vector3i(7, 0, 13)),
            new BlockOccupancy(new Vector3i(7, 0, 14)),
            new BlockOccupancy(new Vector3i(7, 0, 15)),
            new BlockOccupancy(new Vector3i(7, 0, 16)),
            new BlockOccupancy(new Vector3i(7, 0, 17)),
            new BlockOccupancy(new Vector3i(7, 0, 18)),
            new BlockOccupancy(new Vector3i(7, 0, 19)),
            new BlockOccupancy(new Vector3i(7, 0, 20)),
            new BlockOccupancy(new Vector3i(7, 0, 21)),
            new BlockOccupancy(new Vector3i(7, 1, 0)),
            new BlockOccupancy(new Vector3i(7, 1, 1)),
            new BlockOccupancy(new Vector3i(7, 1, 2)),
            new BlockOccupancy(new Vector3i(7, 1, 3)),
            new BlockOccupancy(new Vector3i(7, 1, 4)),
            new BlockOccupancy(new Vector3i(7, 1, 5)),
            new BlockOccupancy(new Vector3i(7, 1, 6)),
            new BlockOccupancy(new Vector3i(7, 1, 7)),
            new BlockOccupancy(new Vector3i(7, 1, 8)),
            new BlockOccupancy(new Vector3i(7, 1, 9)),
            new BlockOccupancy(new Vector3i(7, 1, 10)),
            new BlockOccupancy(new Vector3i(7, 1, 11)),
            new BlockOccupancy(new Vector3i(7, 1, 12)),
            new BlockOccupancy(new Vector3i(7, 1, 13)),
            new BlockOccupancy(new Vector3i(7, 1, 14)),
            new BlockOccupancy(new Vector3i(7, 1, 15)),
            new BlockOccupancy(new Vector3i(7, 1, 16)),
            new BlockOccupancy(new Vector3i(7, 1, 17)),
            new BlockOccupancy(new Vector3i(7, 1, 18)),
            new BlockOccupancy(new Vector3i(7, 1, 19)),
            new BlockOccupancy(new Vector3i(7, 1, 20)),
            new BlockOccupancy(new Vector3i(7, 1, 21)),
            new BlockOccupancy(new Vector3i(7, 2, 0)),
            new BlockOccupancy(new Vector3i(7, 2, 1)),
            new BlockOccupancy(new Vector3i(7, 2, 2)),
            new BlockOccupancy(new Vector3i(7, 2, 3)),
            new BlockOccupancy(new Vector3i(7, 2, 4)),
            new BlockOccupancy(new Vector3i(7, 2, 5)),
            new BlockOccupancy(new Vector3i(7, 2, 6)),
            new BlockOccupancy(new Vector3i(7, 2, 7)),
            new BlockOccupancy(new Vector3i(7, 2, 8)),
            new BlockOccupancy(new Vector3i(7, 2, 9)),
            new BlockOccupancy(new Vector3i(7, 2, 10)),
            new BlockOccupancy(new Vector3i(7, 2, 11)),
            new BlockOccupancy(new Vector3i(7, 2, 12)),
            new BlockOccupancy(new Vector3i(7, 2, 13)),
            new BlockOccupancy(new Vector3i(7, 2, 14)),
            new BlockOccupancy(new Vector3i(7, 2, 15)),
            new BlockOccupancy(new Vector3i(7, 2, 16)),
            new BlockOccupancy(new Vector3i(7, 2, 17)),
            new BlockOccupancy(new Vector3i(7, 2, 18)),
            new BlockOccupancy(new Vector3i(7, 2, 19)),
            new BlockOccupancy(new Vector3i(7, 2, 20)),
            new BlockOccupancy(new Vector3i(7, 2, 21)),
            new BlockOccupancy(new Vector3i(7, 3, 0)),
            new BlockOccupancy(new Vector3i(7, 3, 1)),
            new BlockOccupancy(new Vector3i(7, 3, 2)),
            new BlockOccupancy(new Vector3i(7, 3, 3)),
            new BlockOccupancy(new Vector3i(7, 3, 4)),
            new BlockOccupancy(new Vector3i(7, 3, 5)),
            new BlockOccupancy(new Vector3i(7, 3, 6)),
            new BlockOccupancy(new Vector3i(7, 3, 7)),
            new BlockOccupancy(new Vector3i(7, 3, 8)),
            new BlockOccupancy(new Vector3i(7, 3, 9)),
            new BlockOccupancy(new Vector3i(7, 3, 10)),
            new BlockOccupancy(new Vector3i(7, 3, 11)),
            new BlockOccupancy(new Vector3i(7, 3, 12)),
            new BlockOccupancy(new Vector3i(7, 3, 13)),
            new BlockOccupancy(new Vector3i(7, 3, 14)),
            new BlockOccupancy(new Vector3i(7, 3, 15)),
            new BlockOccupancy(new Vector3i(7, 3, 16)),
            new BlockOccupancy(new Vector3i(7, 3, 17)),
            new BlockOccupancy(new Vector3i(7, 3, 18)),
            new BlockOccupancy(new Vector3i(7, 3, 19)),
            new BlockOccupancy(new Vector3i(7, 3, 20)),
            new BlockOccupancy(new Vector3i(7, 3, 21)),
            new BlockOccupancy(new Vector3i(7, 4, 0)),
            new BlockOccupancy(new Vector3i(7, 4, 1)),
            new BlockOccupancy(new Vector3i(7, 4, 2)),
            new BlockOccupancy(new Vector3i(7, 4, 3)),
            new BlockOccupancy(new Vector3i(7, 4, 4)),
            new BlockOccupancy(new Vector3i(7, 4, 5)),
            new BlockOccupancy(new Vector3i(7, 4, 6)),
            new BlockOccupancy(new Vector3i(7, 4, 7)),
            new BlockOccupancy(new Vector3i(7, 4, 8)),
            new BlockOccupancy(new Vector3i(7, 4, 9)),
            new BlockOccupancy(new Vector3i(7, 4, 10)),
            new BlockOccupancy(new Vector3i(7, 4, 11)),
            new BlockOccupancy(new Vector3i(7, 4, 12)),
            new BlockOccupancy(new Vector3i(7, 4, 13)),
            new BlockOccupancy(new Vector3i(7, 4, 14)),
            new BlockOccupancy(new Vector3i(7, 4, 15)),
            new BlockOccupancy(new Vector3i(7, 4, 16)),
            new BlockOccupancy(new Vector3i(7, 4, 17)),
            new BlockOccupancy(new Vector3i(7, 4, 18)),
            new BlockOccupancy(new Vector3i(7, 4, 19)),
            new BlockOccupancy(new Vector3i(7, 4, 20)),
            new BlockOccupancy(new Vector3i(7, 4, 21)),
            new BlockOccupancy(new Vector3i(7, 5, 0)),
            new BlockOccupancy(new Vector3i(7, 5, 1)),
            new BlockOccupancy(new Vector3i(7, 5, 2)),
            new BlockOccupancy(new Vector3i(7, 5, 3)),
            new BlockOccupancy(new Vector3i(7, 5, 4)),
            new BlockOccupancy(new Vector3i(7, 5, 5)),
            new BlockOccupancy(new Vector3i(7, 5, 6)),
            new BlockOccupancy(new Vector3i(7, 5, 7)),
            new BlockOccupancy(new Vector3i(7, 5, 8)),
            new BlockOccupancy(new Vector3i(7, 5, 9)),
            new BlockOccupancy(new Vector3i(7, 5, 10)),
            new BlockOccupancy(new Vector3i(7, 5, 11)),
            new BlockOccupancy(new Vector3i(7, 5, 12)),
            new BlockOccupancy(new Vector3i(7, 5, 13)),
            new BlockOccupancy(new Vector3i(7, 5, 14)),
            new BlockOccupancy(new Vector3i(7, 5, 15)),
            new BlockOccupancy(new Vector3i(7, 5, 16)),
            new BlockOccupancy(new Vector3i(7, 5, 17)),
            new BlockOccupancy(new Vector3i(7, 5, 18)),
            new BlockOccupancy(new Vector3i(7, 5, 19)),
            new BlockOccupancy(new Vector3i(7, 5, 20)),
            new BlockOccupancy(new Vector3i(7, 5, 21)),
            new BlockOccupancy(new Vector3i(7, 6, 0)),
            new BlockOccupancy(new Vector3i(7, 6, 1)),
            new BlockOccupancy(new Vector3i(7, 6, 2)),
            new BlockOccupancy(new Vector3i(7, 6, 3)),
            new BlockOccupancy(new Vector3i(7, 6, 4)),
            new BlockOccupancy(new Vector3i(7, 6, 5)),
            new BlockOccupancy(new Vector3i(7, 6, 6)),
            new BlockOccupancy(new Vector3i(7, 6, 7)),
            new BlockOccupancy(new Vector3i(7, 6, 8)),
            new BlockOccupancy(new Vector3i(7, 6, 9)),
            new BlockOccupancy(new Vector3i(7, 6, 10)),
            new BlockOccupancy(new Vector3i(7, 6, 11)),
            new BlockOccupancy(new Vector3i(7, 6, 12)),
            new BlockOccupancy(new Vector3i(7, 6, 13)),
            new BlockOccupancy(new Vector3i(7, 6, 14)),
            new BlockOccupancy(new Vector3i(7, 6, 15)),
            new BlockOccupancy(new Vector3i(7, 6, 16)),
            new BlockOccupancy(new Vector3i(7, 6, 17)),
            new BlockOccupancy(new Vector3i(7, 6, 18)),
            new BlockOccupancy(new Vector3i(7, 6, 19)),
            new BlockOccupancy(new Vector3i(7, 6, 20)),
            new BlockOccupancy(new Vector3i(7, 6, 21)),
            new BlockOccupancy(new Vector3i(7, 7, 0)),
            new BlockOccupancy(new Vector3i(7, 7, 1)),
            new BlockOccupancy(new Vector3i(7, 7, 2)),
            new BlockOccupancy(new Vector3i(7, 7, 3)),
            new BlockOccupancy(new Vector3i(7, 7, 4)),
            new BlockOccupancy(new Vector3i(7, 7, 5)),
            new BlockOccupancy(new Vector3i(7, 7, 6)),
            new BlockOccupancy(new Vector3i(7, 7, 7)),
            new BlockOccupancy(new Vector3i(7, 7, 8)),
            new BlockOccupancy(new Vector3i(7, 7, 9)),
            new BlockOccupancy(new Vector3i(7, 7, 10)),
            new BlockOccupancy(new Vector3i(7, 7, 11)),
            new BlockOccupancy(new Vector3i(7, 7, 12)),
            new BlockOccupancy(new Vector3i(7, 7, 13)),
            new BlockOccupancy(new Vector3i(7, 7, 14)),
            new BlockOccupancy(new Vector3i(7, 7, 15)),
            new BlockOccupancy(new Vector3i(7, 7, 16)),
            new BlockOccupancy(new Vector3i(7, 7, 17)),
            new BlockOccupancy(new Vector3i(7, 7, 18)),
            new BlockOccupancy(new Vector3i(7, 7, 19)),
            new BlockOccupancy(new Vector3i(7, 7, 20)),
            new BlockOccupancy(new Vector3i(7, 7, 21)),
            new BlockOccupancy(new Vector3i(7, 8, 0)),
            new BlockOccupancy(new Vector3i(7, 8, 1)),
            new BlockOccupancy(new Vector3i(7, 8, 2)),
            new BlockOccupancy(new Vector3i(7, 8, 3)),
            new BlockOccupancy(new Vector3i(7, 8, 4)),
            new BlockOccupancy(new Vector3i(7, 8, 5)),
            new BlockOccupancy(new Vector3i(7, 8, 6)),
            new BlockOccupancy(new Vector3i(7, 8, 7)),
            new BlockOccupancy(new Vector3i(7, 8, 8)),
            new BlockOccupancy(new Vector3i(7, 8, 9)),
            new BlockOccupancy(new Vector3i(7, 8, 10)),
            new BlockOccupancy(new Vector3i(7, 8, 11)),
            new BlockOccupancy(new Vector3i(7, 8, 12)),
            new BlockOccupancy(new Vector3i(7, 8, 13)),
            new BlockOccupancy(new Vector3i(7, 8, 14)),
            new BlockOccupancy(new Vector3i(7, 8, 15)),
            new BlockOccupancy(new Vector3i(7, 8, 16)),
            new BlockOccupancy(new Vector3i(7, 8, 17)),
            new BlockOccupancy(new Vector3i(7, 8, 18)),
            new BlockOccupancy(new Vector3i(7, 8, 19)),
            new BlockOccupancy(new Vector3i(7, 8, 20)),
            new BlockOccupancy(new Vector3i(7, 8, 21)),
            new BlockOccupancy(new Vector3i(7, 9, 0)),
            new BlockOccupancy(new Vector3i(7, 9, 1)),
            new BlockOccupancy(new Vector3i(7, 9, 2)),
            new BlockOccupancy(new Vector3i(7, 9, 3)),
            new BlockOccupancy(new Vector3i(7, 9, 4)),
            new BlockOccupancy(new Vector3i(7, 9, 5)),
            new BlockOccupancy(new Vector3i(7, 9, 6)),
            new BlockOccupancy(new Vector3i(7, 9, 7)),
            new BlockOccupancy(new Vector3i(7, 9, 8)),
            new BlockOccupancy(new Vector3i(7, 9, 9)),
            new BlockOccupancy(new Vector3i(7, 9, 10)),
            new BlockOccupancy(new Vector3i(7, 9, 11)),
            new BlockOccupancy(new Vector3i(7, 9, 12)),
            new BlockOccupancy(new Vector3i(7, 9, 13)),
            new BlockOccupancy(new Vector3i(7, 9, 14)),
            new BlockOccupancy(new Vector3i(7, 9, 15)),
            new BlockOccupancy(new Vector3i(7, 9, 16)),
            new BlockOccupancy(new Vector3i(7, 9, 17)),
            new BlockOccupancy(new Vector3i(7, 9, 18)),
            new BlockOccupancy(new Vector3i(7, 9, 19)),
            new BlockOccupancy(new Vector3i(7, 9, 20)),
            new BlockOccupancy(new Vector3i(7, 9, 21)),
            new BlockOccupancy(new Vector3i(8, 0, 0)),
            new BlockOccupancy(new Vector3i(8, 0, 1)),
            new BlockOccupancy(new Vector3i(8, 0, 2)),
            new BlockOccupancy(new Vector3i(8, 0, 3)),
            new BlockOccupancy(new Vector3i(8, 0, 4)),
            new BlockOccupancy(new Vector3i(8, 0, 5)),
            new BlockOccupancy(new Vector3i(8, 0, 6)),
            new BlockOccupancy(new Vector3i(8, 0, 7)),
            new BlockOccupancy(new Vector3i(8, 0, 8)),
            new BlockOccupancy(new Vector3i(8, 0, 9)),
            new BlockOccupancy(new Vector3i(8, 0, 10)),
            new BlockOccupancy(new Vector3i(8, 0, 11)),
            new BlockOccupancy(new Vector3i(8, 0, 12)),
            new BlockOccupancy(new Vector3i(8, 0, 13)),
            new BlockOccupancy(new Vector3i(8, 0, 14)),
            new BlockOccupancy(new Vector3i(8, 0, 15)),
            new BlockOccupancy(new Vector3i(8, 0, 16)),
            new BlockOccupancy(new Vector3i(8, 0, 17)),
            new BlockOccupancy(new Vector3i(8, 0, 18)),
            new BlockOccupancy(new Vector3i(8, 0, 19)),
            new BlockOccupancy(new Vector3i(8, 0, 20)),
            new BlockOccupancy(new Vector3i(8, 0, 21)),
            new BlockOccupancy(new Vector3i(8, 1, 0)),
            new BlockOccupancy(new Vector3i(8, 1, 1)),
            new BlockOccupancy(new Vector3i(8, 1, 2)),
            new BlockOccupancy(new Vector3i(8, 1, 3)),
            new BlockOccupancy(new Vector3i(8, 1, 4)),
            new BlockOccupancy(new Vector3i(8, 1, 5)),
            new BlockOccupancy(new Vector3i(8, 1, 6)),
            new BlockOccupancy(new Vector3i(8, 1, 7)),
            new BlockOccupancy(new Vector3i(8, 1, 8)),
            new BlockOccupancy(new Vector3i(8, 1, 9)),
            new BlockOccupancy(new Vector3i(8, 1, 10)),
            new BlockOccupancy(new Vector3i(8, 1, 11)),
            new BlockOccupancy(new Vector3i(8, 1, 12)),
            new BlockOccupancy(new Vector3i(8, 1, 13)),
            new BlockOccupancy(new Vector3i(8, 1, 14)),
            new BlockOccupancy(new Vector3i(8, 1, 15)),
            new BlockOccupancy(new Vector3i(8, 1, 16)),
            new BlockOccupancy(new Vector3i(8, 1, 17)),
            new BlockOccupancy(new Vector3i(8, 1, 18)),
            new BlockOccupancy(new Vector3i(8, 1, 19)),
            new BlockOccupancy(new Vector3i(8, 1, 20)),
            new BlockOccupancy(new Vector3i(8, 1, 21)),
            new BlockOccupancy(new Vector3i(8, 2, 0)),
            new BlockOccupancy(new Vector3i(8, 2, 1)),
            new BlockOccupancy(new Vector3i(8, 2, 2)),
            new BlockOccupancy(new Vector3i(8, 2, 3)),
            new BlockOccupancy(new Vector3i(8, 2, 4)),
            new BlockOccupancy(new Vector3i(8, 2, 5)),
            new BlockOccupancy(new Vector3i(8, 2, 6)),
            new BlockOccupancy(new Vector3i(8, 2, 7)),
            new BlockOccupancy(new Vector3i(8, 2, 8)),
            new BlockOccupancy(new Vector3i(8, 2, 9)),
            new BlockOccupancy(new Vector3i(8, 2, 10)),
            new BlockOccupancy(new Vector3i(8, 2, 11)),
            new BlockOccupancy(new Vector3i(8, 2, 12)),
            new BlockOccupancy(new Vector3i(8, 2, 13)),
            new BlockOccupancy(new Vector3i(8, 2, 14)),
            new BlockOccupancy(new Vector3i(8, 2, 15)),
            new BlockOccupancy(new Vector3i(8, 2, 16)),
            new BlockOccupancy(new Vector3i(8, 2, 17)),
            new BlockOccupancy(new Vector3i(8, 2, 18)),
            new BlockOccupancy(new Vector3i(8, 2, 19)),
            new BlockOccupancy(new Vector3i(8, 2, 20)),
            new BlockOccupancy(new Vector3i(8, 2, 21)),
            new BlockOccupancy(new Vector3i(8, 3, 0)),
            new BlockOccupancy(new Vector3i(8, 3, 1)),
            new BlockOccupancy(new Vector3i(8, 3, 2)),
            new BlockOccupancy(new Vector3i(8, 3, 3)),
            new BlockOccupancy(new Vector3i(8, 3, 4)),
            new BlockOccupancy(new Vector3i(8, 3, 5)),
            new BlockOccupancy(new Vector3i(8, 3, 6)),
            new BlockOccupancy(new Vector3i(8, 3, 7)),
            new BlockOccupancy(new Vector3i(8, 3, 8)),
            new BlockOccupancy(new Vector3i(8, 3, 9)),
            new BlockOccupancy(new Vector3i(8, 3, 10)),
            new BlockOccupancy(new Vector3i(8, 3, 11)),
            new BlockOccupancy(new Vector3i(8, 3, 12)),
            new BlockOccupancy(new Vector3i(8, 3, 13)),
            new BlockOccupancy(new Vector3i(8, 3, 14)),
            new BlockOccupancy(new Vector3i(8, 3, 15)),
            new BlockOccupancy(new Vector3i(8, 3, 16)),
            new BlockOccupancy(new Vector3i(8, 3, 17)),
            new BlockOccupancy(new Vector3i(8, 3, 18)),
            new BlockOccupancy(new Vector3i(8, 3, 19)),
            new BlockOccupancy(new Vector3i(8, 3, 20)),
            new BlockOccupancy(new Vector3i(8, 3, 21)),
            new BlockOccupancy(new Vector3i(8, 4, 0)),
            new BlockOccupancy(new Vector3i(8, 4, 1)),
            new BlockOccupancy(new Vector3i(8, 4, 2)),
            new BlockOccupancy(new Vector3i(8, 4, 3)),
            new BlockOccupancy(new Vector3i(8, 4, 4)),
            new BlockOccupancy(new Vector3i(8, 4, 5)),
            new BlockOccupancy(new Vector3i(8, 4, 6)),
            new BlockOccupancy(new Vector3i(8, 4, 7)),
            new BlockOccupancy(new Vector3i(8, 4, 8)),
            new BlockOccupancy(new Vector3i(8, 4, 9)),
            new BlockOccupancy(new Vector3i(8, 4, 10)),
            new BlockOccupancy(new Vector3i(8, 4, 11)),
            new BlockOccupancy(new Vector3i(8, 4, 12)),
            new BlockOccupancy(new Vector3i(8, 4, 13)),
            new BlockOccupancy(new Vector3i(8, 4, 14)),
            new BlockOccupancy(new Vector3i(8, 4, 15)),
            new BlockOccupancy(new Vector3i(8, 4, 16)),
            new BlockOccupancy(new Vector3i(8, 4, 17)),
            new BlockOccupancy(new Vector3i(8, 4, 18)),
            new BlockOccupancy(new Vector3i(8, 4, 19)),
            new BlockOccupancy(new Vector3i(8, 4, 20)),
            new BlockOccupancy(new Vector3i(8, 4, 21)),
            new BlockOccupancy(new Vector3i(8, 5, 0)),
            new BlockOccupancy(new Vector3i(8, 5, 1)),
            new BlockOccupancy(new Vector3i(8, 5, 2)),
            new BlockOccupancy(new Vector3i(8, 5, 3)),
            new BlockOccupancy(new Vector3i(8, 5, 4)),
            new BlockOccupancy(new Vector3i(8, 5, 5)),
            new BlockOccupancy(new Vector3i(8, 5, 6)),
            new BlockOccupancy(new Vector3i(8, 5, 7)),
            new BlockOccupancy(new Vector3i(8, 5, 8)),
            new BlockOccupancy(new Vector3i(8, 5, 9)),
            new BlockOccupancy(new Vector3i(8, 5, 10)),
            new BlockOccupancy(new Vector3i(8, 5, 11)),
            new BlockOccupancy(new Vector3i(8, 5, 12)),
            new BlockOccupancy(new Vector3i(8, 5, 13)),
            new BlockOccupancy(new Vector3i(8, 5, 14)),
            new BlockOccupancy(new Vector3i(8, 5, 15)),
            new BlockOccupancy(new Vector3i(8, 5, 16)),
            new BlockOccupancy(new Vector3i(8, 5, 17)),
            new BlockOccupancy(new Vector3i(8, 5, 18)),
            new BlockOccupancy(new Vector3i(8, 5, 19)),
            new BlockOccupancy(new Vector3i(8, 5, 20)),
            new BlockOccupancy(new Vector3i(8, 5, 21)),
            new BlockOccupancy(new Vector3i(8, 6, 0)),
            new BlockOccupancy(new Vector3i(8, 6, 1)),
            new BlockOccupancy(new Vector3i(8, 6, 2)),
            new BlockOccupancy(new Vector3i(8, 6, 3)),
            new BlockOccupancy(new Vector3i(8, 6, 4)),
            new BlockOccupancy(new Vector3i(8, 6, 5)),
            new BlockOccupancy(new Vector3i(8, 6, 6)),
            new BlockOccupancy(new Vector3i(8, 6, 7)),
            new BlockOccupancy(new Vector3i(8, 6, 8)),
            new BlockOccupancy(new Vector3i(8, 6, 9)),
            new BlockOccupancy(new Vector3i(8, 6, 10)),
            new BlockOccupancy(new Vector3i(8, 6, 11)),
            new BlockOccupancy(new Vector3i(8, 6, 12)),
            new BlockOccupancy(new Vector3i(8, 6, 13)),
            new BlockOccupancy(new Vector3i(8, 6, 14)),
            new BlockOccupancy(new Vector3i(8, 6, 15)),
            new BlockOccupancy(new Vector3i(8, 6, 16)),
            new BlockOccupancy(new Vector3i(8, 6, 17)),
            new BlockOccupancy(new Vector3i(8, 6, 18)),
            new BlockOccupancy(new Vector3i(8, 6, 19)),
            new BlockOccupancy(new Vector3i(8, 6, 20)),
            new BlockOccupancy(new Vector3i(8, 6, 21)),
            new BlockOccupancy(new Vector3i(8, 7, 0)),
            new BlockOccupancy(new Vector3i(8, 7, 1)),
            new BlockOccupancy(new Vector3i(8, 7, 2)),
            new BlockOccupancy(new Vector3i(8, 7, 3)),
            new BlockOccupancy(new Vector3i(8, 7, 4)),
            new BlockOccupancy(new Vector3i(8, 7, 5)),
            new BlockOccupancy(new Vector3i(8, 7, 6)),
            new BlockOccupancy(new Vector3i(8, 7, 7)),
            new BlockOccupancy(new Vector3i(8, 7, 8)),
            new BlockOccupancy(new Vector3i(8, 7, 9)),
            new BlockOccupancy(new Vector3i(8, 7, 10)),
            new BlockOccupancy(new Vector3i(8, 7, 11)),
            new BlockOccupancy(new Vector3i(8, 7, 12)),
            new BlockOccupancy(new Vector3i(8, 7, 13)),
            new BlockOccupancy(new Vector3i(8, 7, 14)),
            new BlockOccupancy(new Vector3i(8, 7, 15)),
            new BlockOccupancy(new Vector3i(8, 7, 16)),
            new BlockOccupancy(new Vector3i(8, 7, 17)),
            new BlockOccupancy(new Vector3i(8, 7, 18)),
            new BlockOccupancy(new Vector3i(8, 7, 19)),
            new BlockOccupancy(new Vector3i(8, 7, 20)),
            new BlockOccupancy(new Vector3i(8, 7, 21)),
            new BlockOccupancy(new Vector3i(8, 8, 0)),
            new BlockOccupancy(new Vector3i(8, 8, 1)),
            new BlockOccupancy(new Vector3i(8, 8, 2)),
            new BlockOccupancy(new Vector3i(8, 8, 3)),
            new BlockOccupancy(new Vector3i(8, 8, 4)),
            new BlockOccupancy(new Vector3i(8, 8, 5)),
            new BlockOccupancy(new Vector3i(8, 8, 6)),
            new BlockOccupancy(new Vector3i(8, 8, 7)),
            new BlockOccupancy(new Vector3i(8, 8, 8)),
            new BlockOccupancy(new Vector3i(8, 8, 9)),
            new BlockOccupancy(new Vector3i(8, 8, 10)),
            new BlockOccupancy(new Vector3i(8, 8, 11)),
            new BlockOccupancy(new Vector3i(8, 8, 12)),
            new BlockOccupancy(new Vector3i(8, 8, 13)),
            new BlockOccupancy(new Vector3i(8, 8, 14)),
            new BlockOccupancy(new Vector3i(8, 8, 15)),
            new BlockOccupancy(new Vector3i(8, 8, 16)),
            new BlockOccupancy(new Vector3i(8, 8, 17)),
            new BlockOccupancy(new Vector3i(8, 8, 18)),
            new BlockOccupancy(new Vector3i(8, 8, 19)),
            new BlockOccupancy(new Vector3i(8, 8, 20)),
            new BlockOccupancy(new Vector3i(8, 8, 21)),
            new BlockOccupancy(new Vector3i(8, 9, 0)),
            new BlockOccupancy(new Vector3i(8, 9, 1)),
            new BlockOccupancy(new Vector3i(8, 9, 2)),
            new BlockOccupancy(new Vector3i(8, 9, 3)),
            new BlockOccupancy(new Vector3i(8, 9, 4)),
            new BlockOccupancy(new Vector3i(8, 9, 5)),
            new BlockOccupancy(new Vector3i(8, 9, 6)),
            new BlockOccupancy(new Vector3i(8, 9, 7)),
            new BlockOccupancy(new Vector3i(8, 9, 8)),
            new BlockOccupancy(new Vector3i(8, 9, 9)),
            new BlockOccupancy(new Vector3i(8, 9, 10)),
            new BlockOccupancy(new Vector3i(8, 9, 11)),
            new BlockOccupancy(new Vector3i(8, 9, 12)),
            new BlockOccupancy(new Vector3i(8, 9, 13)),
            new BlockOccupancy(new Vector3i(8, 9, 14)),
            new BlockOccupancy(new Vector3i(8, 9, 15)),
            new BlockOccupancy(new Vector3i(8, 9, 16)),
            new BlockOccupancy(new Vector3i(8, 9, 17)),
            new BlockOccupancy(new Vector3i(8, 9, 18)),
            new BlockOccupancy(new Vector3i(8, 9, 19)),
            new BlockOccupancy(new Vector3i(8, 9, 20)),
            new BlockOccupancy(new Vector3i(8, 9, 21)),
            new BlockOccupancy(new Vector3i(9, 0, 0)),
            new BlockOccupancy(new Vector3i(9, 0, 1)),
            new BlockOccupancy(new Vector3i(9, 0, 2)),
            new BlockOccupancy(new Vector3i(9, 0, 3)),
            new BlockOccupancy(new Vector3i(9, 0, 4)),
            new BlockOccupancy(new Vector3i(9, 0, 5)),
            new BlockOccupancy(new Vector3i(9, 0, 6)),
            new BlockOccupancy(new Vector3i(9, 0, 7)),
            new BlockOccupancy(new Vector3i(9, 0, 8)),
            new BlockOccupancy(new Vector3i(9, 0, 9)),
            new BlockOccupancy(new Vector3i(9, 0, 10)),
            new BlockOccupancy(new Vector3i(9, 0, 11)),
            new BlockOccupancy(new Vector3i(9, 0, 12)),
            new BlockOccupancy(new Vector3i(9, 0, 13)),
            new BlockOccupancy(new Vector3i(9, 0, 14)),
            new BlockOccupancy(new Vector3i(9, 0, 15)),
            new BlockOccupancy(new Vector3i(9, 0, 16)),
            new BlockOccupancy(new Vector3i(9, 0, 17)),
            new BlockOccupancy(new Vector3i(9, 0, 18)),
            new BlockOccupancy(new Vector3i(9, 0, 19)),
            new BlockOccupancy(new Vector3i(9, 0, 20)),
            new BlockOccupancy(new Vector3i(9, 0, 21)),
            new BlockOccupancy(new Vector3i(9, 1, 0)),
            new BlockOccupancy(new Vector3i(9, 1, 1)),
            new BlockOccupancy(new Vector3i(9, 1, 2)),
            new BlockOccupancy(new Vector3i(9, 1, 3)),
            new BlockOccupancy(new Vector3i(9, 1, 4)),
            new BlockOccupancy(new Vector3i(9, 1, 5)),
            new BlockOccupancy(new Vector3i(9, 1, 6)),
            new BlockOccupancy(new Vector3i(9, 1, 7)),
            new BlockOccupancy(new Vector3i(9, 1, 8)),
            new BlockOccupancy(new Vector3i(9, 1, 9)),
            new BlockOccupancy(new Vector3i(9, 1, 10)),
            new BlockOccupancy(new Vector3i(9, 1, 11)),
            new BlockOccupancy(new Vector3i(9, 1, 12)),
            new BlockOccupancy(new Vector3i(9, 1, 13)),
            new BlockOccupancy(new Vector3i(9, 1, 14)),
            new BlockOccupancy(new Vector3i(9, 1, 15)),
            new BlockOccupancy(new Vector3i(9, 1, 16)),
            new BlockOccupancy(new Vector3i(9, 1, 17)),
            new BlockOccupancy(new Vector3i(9, 1, 18)),
            new BlockOccupancy(new Vector3i(9, 1, 19)),
            new BlockOccupancy(new Vector3i(9, 1, 20)),
            new BlockOccupancy(new Vector3i(9, 1, 21)),
            new BlockOccupancy(new Vector3i(9, 2, 0)),
            new BlockOccupancy(new Vector3i(9, 2, 1)),
            new BlockOccupancy(new Vector3i(9, 2, 2)),
            new BlockOccupancy(new Vector3i(9, 2, 3)),
            new BlockOccupancy(new Vector3i(9, 2, 4)),
            new BlockOccupancy(new Vector3i(9, 2, 5)),
            new BlockOccupancy(new Vector3i(9, 2, 6)),
            new BlockOccupancy(new Vector3i(9, 2, 7)),
            new BlockOccupancy(new Vector3i(9, 2, 8)),
            new BlockOccupancy(new Vector3i(9, 2, 9)),
            new BlockOccupancy(new Vector3i(9, 2, 10)),
            new BlockOccupancy(new Vector3i(9, 2, 11)),
            new BlockOccupancy(new Vector3i(9, 2, 12)),
            new BlockOccupancy(new Vector3i(9, 2, 13)),
            new BlockOccupancy(new Vector3i(9, 2, 14)),
            new BlockOccupancy(new Vector3i(9, 2, 15)),
            new BlockOccupancy(new Vector3i(9, 2, 16)),
            new BlockOccupancy(new Vector3i(9, 2, 17)),
            new BlockOccupancy(new Vector3i(9, 2, 18)),
            new BlockOccupancy(new Vector3i(9, 2, 19)),
            new BlockOccupancy(new Vector3i(9, 2, 20)),
            new BlockOccupancy(new Vector3i(9, 2, 21)),
            new BlockOccupancy(new Vector3i(9, 3, 0)),
            new BlockOccupancy(new Vector3i(9, 3, 1)),
            new BlockOccupancy(new Vector3i(9, 3, 2)),
            new BlockOccupancy(new Vector3i(9, 3, 3)),
            new BlockOccupancy(new Vector3i(9, 3, 4)),
            new BlockOccupancy(new Vector3i(9, 3, 5)),
            new BlockOccupancy(new Vector3i(9, 3, 6)),
            new BlockOccupancy(new Vector3i(9, 3, 7)),
            new BlockOccupancy(new Vector3i(9, 3, 8)),
            new BlockOccupancy(new Vector3i(9, 3, 9)),
            new BlockOccupancy(new Vector3i(9, 3, 10)),
            new BlockOccupancy(new Vector3i(9, 3, 11)),
            new BlockOccupancy(new Vector3i(9, 3, 12)),
            new BlockOccupancy(new Vector3i(9, 3, 13)),
            new BlockOccupancy(new Vector3i(9, 3, 14)),
            new BlockOccupancy(new Vector3i(9, 3, 15)),
            new BlockOccupancy(new Vector3i(9, 3, 16)),
            new BlockOccupancy(new Vector3i(9, 3, 17)),
            new BlockOccupancy(new Vector3i(9, 3, 18)),
            new BlockOccupancy(new Vector3i(9, 3, 19)),
            new BlockOccupancy(new Vector3i(9, 3, 20)),
            new BlockOccupancy(new Vector3i(9, 3, 21)),
            new BlockOccupancy(new Vector3i(9, 4, 0)),
            new BlockOccupancy(new Vector3i(9, 4, 1)),
            new BlockOccupancy(new Vector3i(9, 4, 2)),
            new BlockOccupancy(new Vector3i(9, 4, 3)),
            new BlockOccupancy(new Vector3i(9, 4, 4)),
            new BlockOccupancy(new Vector3i(9, 4, 5)),
            new BlockOccupancy(new Vector3i(9, 4, 6)),
            new BlockOccupancy(new Vector3i(9, 4, 7)),
            new BlockOccupancy(new Vector3i(9, 4, 8)),
            new BlockOccupancy(new Vector3i(9, 4, 9)),
            new BlockOccupancy(new Vector3i(9, 4, 10)),
            new BlockOccupancy(new Vector3i(9, 4, 11)),
            new BlockOccupancy(new Vector3i(9, 4, 12)),
            new BlockOccupancy(new Vector3i(9, 4, 13)),
            new BlockOccupancy(new Vector3i(9, 4, 14)),
            new BlockOccupancy(new Vector3i(9, 4, 15)),
            new BlockOccupancy(new Vector3i(9, 4, 16)),
            new BlockOccupancy(new Vector3i(9, 4, 17)),
            new BlockOccupancy(new Vector3i(9, 4, 18)),
            new BlockOccupancy(new Vector3i(9, 4, 19)),
            new BlockOccupancy(new Vector3i(9, 4, 20)),
            new BlockOccupancy(new Vector3i(9, 4, 21)),
            new BlockOccupancy(new Vector3i(9, 5, 0)),
            new BlockOccupancy(new Vector3i(9, 5, 1)),
            new BlockOccupancy(new Vector3i(9, 5, 2)),
            new BlockOccupancy(new Vector3i(9, 5, 3)),
            new BlockOccupancy(new Vector3i(9, 5, 4)),
            new BlockOccupancy(new Vector3i(9, 5, 5)),
            new BlockOccupancy(new Vector3i(9, 5, 6)),
            new BlockOccupancy(new Vector3i(9, 5, 7)),
            new BlockOccupancy(new Vector3i(9, 5, 8)),
            new BlockOccupancy(new Vector3i(9, 5, 9)),
            new BlockOccupancy(new Vector3i(9, 5, 10)),
            new BlockOccupancy(new Vector3i(9, 5, 11)),
            new BlockOccupancy(new Vector3i(9, 5, 12)),
            new BlockOccupancy(new Vector3i(9, 5, 13)),
            new BlockOccupancy(new Vector3i(9, 5, 14)),
            new BlockOccupancy(new Vector3i(9, 5, 15)),
            new BlockOccupancy(new Vector3i(9, 5, 16)),
            new BlockOccupancy(new Vector3i(9, 5, 17)),
            new BlockOccupancy(new Vector3i(9, 5, 18)),
            new BlockOccupancy(new Vector3i(9, 5, 19)),
            new BlockOccupancy(new Vector3i(9, 5, 20)),
            new BlockOccupancy(new Vector3i(9, 5, 21)),
            new BlockOccupancy(new Vector3i(9, 6, 0)),
            new BlockOccupancy(new Vector3i(9, 6, 1)),
            new BlockOccupancy(new Vector3i(9, 6, 2)),
            new BlockOccupancy(new Vector3i(9, 6, 3)),
            new BlockOccupancy(new Vector3i(9, 6, 4)),
            new BlockOccupancy(new Vector3i(9, 6, 5)),
            new BlockOccupancy(new Vector3i(9, 6, 6)),
            new BlockOccupancy(new Vector3i(9, 6, 7)),
            new BlockOccupancy(new Vector3i(9, 6, 8)),
            new BlockOccupancy(new Vector3i(9, 6, 9)),
            new BlockOccupancy(new Vector3i(9, 6, 10)),
            new BlockOccupancy(new Vector3i(9, 6, 11)),
            new BlockOccupancy(new Vector3i(9, 6, 12)),
            new BlockOccupancy(new Vector3i(9, 6, 13)),
            new BlockOccupancy(new Vector3i(9, 6, 14)),
            new BlockOccupancy(new Vector3i(9, 6, 15)),
            new BlockOccupancy(new Vector3i(9, 6, 16)),
            new BlockOccupancy(new Vector3i(9, 6, 17)),
            new BlockOccupancy(new Vector3i(9, 6, 18)),
            new BlockOccupancy(new Vector3i(9, 6, 19)),
            new BlockOccupancy(new Vector3i(9, 6, 20)),
            new BlockOccupancy(new Vector3i(9, 6, 21)),
            new BlockOccupancy(new Vector3i(9, 7, 0)),
            new BlockOccupancy(new Vector3i(9, 7, 1)),
            new BlockOccupancy(new Vector3i(9, 7, 2)),
            new BlockOccupancy(new Vector3i(9, 7, 3)),
            new BlockOccupancy(new Vector3i(9, 7, 4)),
            new BlockOccupancy(new Vector3i(9, 7, 5)),
            new BlockOccupancy(new Vector3i(9, 7, 6)),
            new BlockOccupancy(new Vector3i(9, 7, 7)),
            new BlockOccupancy(new Vector3i(9, 7, 8)),
            new BlockOccupancy(new Vector3i(9, 7, 9)),
            new BlockOccupancy(new Vector3i(9, 7, 10)),
            new BlockOccupancy(new Vector3i(9, 7, 11)),
            new BlockOccupancy(new Vector3i(9, 7, 12)),
            new BlockOccupancy(new Vector3i(9, 7, 13)),
            new BlockOccupancy(new Vector3i(9, 7, 14)),
            new BlockOccupancy(new Vector3i(9, 7, 15)),
            new BlockOccupancy(new Vector3i(9, 7, 16)),
            new BlockOccupancy(new Vector3i(9, 7, 17)),
            new BlockOccupancy(new Vector3i(9, 7, 18)),
            new BlockOccupancy(new Vector3i(9, 7, 19)),
            new BlockOccupancy(new Vector3i(9, 7, 20)),
            new BlockOccupancy(new Vector3i(9, 7, 21)),
            new BlockOccupancy(new Vector3i(9, 8, 0)),
            new BlockOccupancy(new Vector3i(9, 8, 1)),
            new BlockOccupancy(new Vector3i(9, 8, 2)),
            new BlockOccupancy(new Vector3i(9, 8, 3)),
            new BlockOccupancy(new Vector3i(9, 8, 4)),
            new BlockOccupancy(new Vector3i(9, 8, 5)),
            new BlockOccupancy(new Vector3i(9, 8, 6)),
            new BlockOccupancy(new Vector3i(9, 8, 7)),
            new BlockOccupancy(new Vector3i(9, 8, 8)),
            new BlockOccupancy(new Vector3i(9, 8, 9)),
            new BlockOccupancy(new Vector3i(9, 8, 10)),
            new BlockOccupancy(new Vector3i(9, 8, 11)),
            new BlockOccupancy(new Vector3i(9, 8, 12)),
            new BlockOccupancy(new Vector3i(9, 8, 13)),
            new BlockOccupancy(new Vector3i(9, 8, 14)),
            new BlockOccupancy(new Vector3i(9, 8, 15)),
            new BlockOccupancy(new Vector3i(9, 8, 16)),
            new BlockOccupancy(new Vector3i(9, 8, 17)),
            new BlockOccupancy(new Vector3i(9, 8, 18)),
            new BlockOccupancy(new Vector3i(9, 8, 19)),
            new BlockOccupancy(new Vector3i(9, 8, 20)),
            new BlockOccupancy(new Vector3i(9, 8, 21)),
            new BlockOccupancy(new Vector3i(9, 9, 0)),
            new BlockOccupancy(new Vector3i(9, 9, 1)),
            new BlockOccupancy(new Vector3i(9, 9, 2)),
            new BlockOccupancy(new Vector3i(9, 9, 3)),
            new BlockOccupancy(new Vector3i(9, 9, 4)),
            new BlockOccupancy(new Vector3i(9, 9, 5)),
            new BlockOccupancy(new Vector3i(9, 9, 6)),
            new BlockOccupancy(new Vector3i(9, 9, 7)),
            new BlockOccupancy(new Vector3i(9, 9, 8)),
            new BlockOccupancy(new Vector3i(9, 9, 9)),
            new BlockOccupancy(new Vector3i(9, 9, 10)),
            new BlockOccupancy(new Vector3i(9, 9, 11)),
            new BlockOccupancy(new Vector3i(9, 9, 12)),
            new BlockOccupancy(new Vector3i(9, 9, 13)),
            new BlockOccupancy(new Vector3i(9, 9, 14)),
            new BlockOccupancy(new Vector3i(9, 9, 15)),
            new BlockOccupancy(new Vector3i(9, 9, 16)),
            new BlockOccupancy(new Vector3i(9, 9, 17)),
            new BlockOccupancy(new Vector3i(9, 9, 18)),
            new BlockOccupancy(new Vector3i(9, 9, 19)),
            new BlockOccupancy(new Vector3i(9, 9, 20)),
            new BlockOccupancy(new Vector3i(9, 9, 21)),
            };

            AddOccupancy<Shipping_dechetObject>(BlockOccupancyList);




        }

        protected override void Initialize()
        {
            this.ModsPreInitialize();
            this.GetComponent<LinkComponent>().Initialize(12); // Maximum connection distance with nearby storage units: 12 meters.
            PublicStorageComponent component = base.GetComponent<PublicStorageComponent>(null);
            component.Initialize(64); // Le nombre d'emplacement "32" autorisé dans le stockage.
            component.Storage.AddInvRestriction(new Inventoryx6Multiply()); // Multiplicateur de stacks
            component.Storage.AddInvRestriction(new TagRestriction(new string[] // Les tags autorisées à être utilisées dans le stockage.
            {
                "Waste Product",
            }));
            this.ModsPostInitialize();
        }


        partial void ModsPreInitialize();

        partial void ModsPostInitialize();
    }

    [Serialized]
    [LocDisplayName("Conteneur de déchets polluants")]
    [LocDescription("Un espace sûr pour entreposer les matériaux polluants, afin de prévenir toute contamination dans votre environnement.")]
    [Ecopedia("Crafted Objects", "Storage", createAsSubPage: true)]
    [Weight(20000)]
    [MaxStackSize(10)]
    public partial class Shipping_dechetItem : WorldObjectItem<Shipping_dechetObject>
    {

    }

    [Ecopedia("Crafted Objects", "Storage", subPageName: "Conteneur de déchets polluants")]
    [RequiresSkill(typeof(MechanicsSkill), 5)]
    public partial class Shipping_dechetRecipe : RecipeFamily
    {
        public Shipping_dechetRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "Conteneur de déchets polluants",  //noloc
                displayName: Localizer.DoStr("Conteneur de déchets polluants"),

                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(IronPlateItem), 400, typeof(MechanicsSkill), typeof(MechanicsLavishResourcesTalent)), // Recette personnalisable : "400" Plaques de fer.
                    new IngredientElement(typeof(ScrewsItem), 300, typeof(MechanicsSkill), typeof(MechanicsLavishResourcesTalent)), // Recette personnalisable : "300" Vis.
                    new IngredientElement(typeof(IronBarItem), 220, typeof(MechanicsSkill), typeof(MechanicsLavishResourcesTalent)), // Recette personnalisable : "220" Lingot de fer.
                },

                items: new List<CraftingElement>
                {
                    new CraftingElement<Shipping_dechetItem>()
                });
            this.Recipes = new List<Recipe> { recipe };
            this.ExperienceOnCraft = 25;

            this.LaborInCalories = CreateLaborInCaloriesValue(800, typeof(MechanicsSkill)); // Amount of calories consumed for crafting: "250".

            this.CraftMinutes = CreateCraftTimeValue(beneficiary: typeof(Shipping_dechetRecipe), start: 20, skillType: typeof(MechanicsSkill), typeof(MechanicsFocusedSpeedTalent), typeof(MechanicsParallelSpeedTalent)); // Durée de fabrication de l'objet : 10 minutes.

            this.ModsPreInitialize();
            this.Initialize(displayText: Localizer.DoStr("Conteneur de déchets polluants"), recipeType: typeof(Shipping_dechetRecipe));
            this.ModsPostInitialize();

            CraftingComponent.AddRecipe(tableType: typeof(MachinistTableObject), recipeFamily: this);
        }
        partial void ModsPreInitialize();
        partial void ModsPostInitialize();
    }
#endregion




}
