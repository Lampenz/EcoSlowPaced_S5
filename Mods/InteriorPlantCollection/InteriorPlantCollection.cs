 using Eco.Core.Plugins.Interfaces;
    
    public class InteriorPlantCollectionMod : IModInit
    {
        public static ModRegistration Register() => new() 
        { 
            ModName = "InteriorPlantCollection",
            ModDescription = "Add new plants to your interior!.",
            ModDisplayName = "Interior Plant Collection",
        };
    }