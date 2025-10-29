// Eco Russian Community

namespace Eco.Mods.TechTree
{
    using System;
    using Eco.Gameplay.Blocks;
    using Eco.Shared.Localization;

    public partial class HedgeCubeFormType : FormType
    {
        public override string Name => "Cube";
        public override LocString DisplayName => Localizer.DoStr("Cube");
        public override LocString DisplayDescription => Localizer.DoStr("Cube");
        public override Type GroupType => typeof(BasicFormGroup);
        public override int SortOrder => 40;
        public override int MinTier => 1;
    }

    public partial class HedgeCylinderFormType : FormType
    {
        public override string Name => "Cylinder";
        public override LocString DisplayName => Localizer.DoStr("Cylinder");
        public override LocString DisplayDescription => Localizer.DoStr("Cylinder");
        public override Type GroupType => typeof(SupportsFormGroup);
        public override int SortOrder => 60;
        public override int MinTier => 1;
    }

    public partial class HedgeHalfCylinderGFormType : FormType
    {
        public override string Name => "HalfCylinderG";
        public override LocString DisplayName => Localizer.DoStr("Half Cylinder Horizontal");
        public override LocString DisplayDescription => Localizer.DoStr("Half Cylinder Horizontal");
        public override Type GroupType => typeof(RoofsFormGroup);
        public override int SortOrder => 10;
        public override int MinTier => 1;
    }

    public partial class HedgeHalfCylinderTFormType : FormType
    {
        public override string Name => "HalfCylinderT";
        public override LocString DisplayName => Localizer.DoStr("Half Cylinder T-junction");
        public override LocString DisplayDescription => Localizer.DoStr("Half Cylinder T-junction");
        public override Type GroupType => typeof(RoofsFormGroup);
        public override int SortOrder => 40;
        public override int MinTier => 1;
    }

    public partial class HedgeHalfCylinderVFormType : FormType
    {
        public override string Name => "HalfCylinderV";
        public override LocString DisplayName => Localizer.DoStr("Half Cylinder Vertical");
        public override LocString DisplayDescription => Localizer.DoStr("Half Cylinder Vertical");
        public override Type GroupType => typeof(SupportsFormGroup);
        public override int SortOrder => 60;
        public override int MinTier => 1;
    }

    public partial class HedgeHalfCylinderXFormType : FormType
    {
        public override string Name => "HalfCylinderX";
        public override LocString DisplayName => Localizer.DoStr("Half Cylinder Сrosshair");
        public override LocString DisplayDescription => Localizer.DoStr("Half Cylinder Сrosshair");
        public override Type GroupType => typeof(RoofsFormGroup);
        public override int SortOrder => 50;
        public override int MinTier => 1;
    }

    public partial class HedgeTopAngleCylinderFormType : FormType
    {
        public override string Name => "TopAngleCylinder";
        public override LocString DisplayName => Localizer.DoStr("Half Cylinder Top Angle");
        public override LocString DisplayDescription => Localizer.DoStr("Half Cylinder Top Angle");
        public override Type GroupType => typeof(RoofsFormGroup);
        public override int SortOrder => 60;
        public override int MinTier => 1;
    }

    public partial class HedgeBottomAngleCylinderFormType : FormType
    {
        public override string Name => "BottomAngleCylinder";
        public override LocString DisplayName => Localizer.DoStr("Half Cylinder Bottom Angle");
        public override LocString DisplayDescription => Localizer.DoStr("Half Cylinder Bottom Angle");
        public override Type GroupType => typeof(RoofsFormGroup);
        public override int SortOrder => 60;
        public override int MinTier => 1;
    }

    public partial class HedgeHemiSphereFormType : FormType
    {
        public override string Name => "HemiSphere";
        public override LocString DisplayName => Localizer.DoStr("HemiSphere");
        public override LocString DisplayDescription => Localizer.DoStr("HemiSphere");
        public override Type GroupType => typeof(RoofsFormGroup);
        public override int SortOrder => 70;
        public override int MinTier => 1;
    }

    public partial class HedgeQuarterSphereFormType : FormType
    {
        public override string Name => "QuarterSphere";
        public override LocString DisplayName => Localizer.DoStr("Quarter Sphere");
        public override LocString DisplayDescription => Localizer.DoStr("Quarter Sphere");
        public override Type GroupType => typeof(RoofsFormGroup);
        public override int SortOrder => 80;
        public override int MinTier => 1;
    }

    public partial class HedgeTriangleFormType : FormType
    {
        public override string Name => "Triangle";
        public override LocString DisplayName => Localizer.DoStr("Triangle");
        public override LocString DisplayDescription => Localizer.DoStr("Triangle");
        public override Type GroupType => typeof(BasicFormGroup);
        public override int SortOrder => 60;
        public override int MinTier => 1;
    }

    public partial class HedgeHalfCylinderTurnFormType : FormType
    {
        public override string Name => "HalfCylinderTurn";
        public override LocString DisplayName => Localizer.DoStr("Half Cylinder Turn");
        public override LocString DisplayDescription => Localizer.DoStr("Half Cylinder Turn");
        public override Type GroupType => typeof(RoofsFormGroup);
        public override int SortOrder => 30;
        public override int MinTier => 1;
    }

    public partial class HedgeHalfCylinderGRFormType : FormType
    {
        public override string Name => "HalfCylinderGR";
        public override LocString DisplayName => Localizer.DoStr("Under Half Cylinder Horizontal");
        public override LocString DisplayDescription => Localizer.DoStr("Under Half Cylinder Horizontal");
        public override Type GroupType => typeof(RoofsFormGroup);
        public override int SortOrder => 20;
        public override int MinTier => 1;
    }
}
