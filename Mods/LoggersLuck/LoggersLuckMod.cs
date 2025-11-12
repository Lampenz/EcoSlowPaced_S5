 using Eco.Core.Plugins.Interfaces;
    
    public class LoggersLuckMod : IModInit
    {
        public static ModRegistration Register() => new() 
        { 
            ModName = "LoggersLuck",
            ModDescription = "Gives loggers the high-level talents they deserve!",
            ModDisplayName = "Loggers Luck",
        };
    }