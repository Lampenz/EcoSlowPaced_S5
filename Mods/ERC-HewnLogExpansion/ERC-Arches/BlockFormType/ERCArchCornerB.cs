namespace Eco.Mods.TechTree
{
    using System;
    using Eco.Gameplay.Blocks;
    using Eco.Shared.Localization;
    using Eco.World.Blocks;

    /// <summary>
    /// <para>Server side definition for the "ERCArchCornerB" form type. </para>
    /// <para>More information about FormType objects can be found at https://docs.play.eco/api/server/eco.gameplay/Eco.Gameplay.Blocks.FormType.html</para>
    /// </summary>
    /// <remarks>
    /// This is an auto-generated class. Don't modify it! All your changes will be wiped with next update! Use Mods* partial methods instead for customization. 
    /// If you wish to modify this class, please create a new partial class or follow the instructions in the "UserCode" folder to override the entire file.
    /// </remarks>
    public partial class ERCArchCornerBFormType : FormType
    {
        /// <summary>Basic name of the block form type.</summary>
        public override string Name => "ERCArchCornerB";
        /// <summary>The plural localization name for the block form type.</summary>
        public override LocString DisplayName => Localizer.DoStr("Arch Lower Corner");
        /// <summary>The tooltip description for the block form type.</summary>
        public override LocString DisplayDescription => Localizer.DoStr("Arch Lower Corner");
        /// <summary>The block FormGroup this form type belongs to</summary>
        public override Type GroupType => typeof(ArchesFormGroup);
        /// <summary>Defines the sort order used by this block form type</summary>
        public override int SortOrder => 13;
        /// <summary>Defines the minimum hammer tier for this block form type</summary>
        public override int MinTier => 1;
    }
}
