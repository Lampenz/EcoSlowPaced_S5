//Taken from https://github.com/thomasfn/EcoCompaniesMod
namespace Civics
{
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Eco.Core.Controller;
    using Eco.Core.Plugins.Interfaces;
    using Eco.Core.Utils;
    using Eco.Gameplay.Aliases;
    using Eco.Gameplay.Civics.GameValues;
    using Eco.Gameplay.Players;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Shared.Localization;
    using Eco.Shared.Networking;
    using Eco.Shared.Serialization;
    using Eco.Shared.Utils;
    using Eco.Shared.Logging;

    [Localized, LocDisplayName(nameof(LandownersPlugin)), Priority(PriorityAttribute.High)]
    public class LandownersPlugin : Singleton<LandownersPlugin>, IModKitPlugin, IInitializablePlugin
    {
        private static readonly Dictionary<Type, GameValueType> gameValueTypeCache = new Dictionary<Type, GameValueType>();

        public void Initialize(TimedTask timer)
        {
            InstallGameValueHack();
        }

        private void InstallGameValueHack()
        {
            var attr = typeof(GameValue).GetCustomAttribute<CustomRPCSetterAttribute>();
            attr.ContainerType = GetType();
            attr.MethodName = nameof(DynamicSetGameValue);
            var idToMethod = typeof(RPCManager).GetField("IdToMethod", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null) as Dictionary<int, RPCMethod>;
            if (idToMethod == null)
            {
                Log.WriteErrorLineLocStr($"Failed to install game value hack: Couldn't retrieve RPCManager.IdToMethod");
            }
            var rpcMethodFuncProperty = typeof(RPCMethod).GetProperty(nameof(RPCMethod.Func), BindingFlags.Public | BindingFlags.Instance);
            if (rpcMethodFuncProperty == null)
            {
                Log.WriteErrorLineLocStr($"Failed to install game value hack: Couldn't find RPCMethod.Func property");
                return;
            }
            var backingField = GetBackingField(rpcMethodFuncProperty);
            if (backingField == null)
            {
                Log.WriteErrorLineLocStr($"Failed to install game value hack: Couldn't find RPCMethod.Func backing field");
                return;
            }
            var relevantRpcMethods = idToMethod.Values
                .Where(x => x.IsCustomSetter && x.PropertyInfo != null)
                .Where(x => x.PropertyInfo.PropertyType.IsAssignableTo(typeof(GameValue<IAlias>)) || x.PropertyInfo.PropertyType.IsAssignableTo(typeof(GameValue<User>)));
            foreach (var rpcMethod in relevantRpcMethods)
            {
                Func<object, object[], object> overrideFunc = (target, args) => { DynamicSetGameValue(target, rpcMethod.PropertyInfo, args[0]); return null; };
                backingField.SetValue(rpcMethod, overrideFunc);
            }
        }

        private static FieldInfo GetBackingField(PropertyInfo pi)
        {
            if (!pi.CanRead || !pi.GetGetMethod(nonPublic: true).IsDefined(typeof(CompilerGeneratedAttribute), inherit: true))
                return null;
            var backingField = pi.DeclaringType.GetField($"<{pi.Name}>k__BackingField", BindingFlags.Instance | BindingFlags.NonPublic);
            if (backingField == null)
                return null;
            if (!backingField.IsDefined(typeof(CompilerGeneratedAttribute), inherit: true))
                return null;
            return backingField;
        }

        public static void DynamicSetGameValue(object parent, PropertyInfo prop, object newValue)
        {
            if (newValue is BSONObject obj) { newValue = BsonManipulator.FromBson(obj, typeof(IController)); }
            if (newValue is GameValueType gvt)
            {
                if (gvt.Type == typeof(Landowner) && prop.PropertyType == typeof(GameValue<IAlias>))
                {
                    // The property wants an IAlias and the client sent a Landowner, so remap it to Landowners
                    newValue = GetGameValueType<Landowners>();
                }
                else if (gvt.Type == typeof(Landowners) && prop.PropertyType == typeof(GameValue<User>))
                {
                    // The property wants a User and the client sent an Landowners, so remap it to Landowner
                    // This shouldn't really be possible as Landowners is marked as NonSelectable but do it anyway to be safe
                    newValue = GetGameValueType<Landowner>();
                }
            }
            GameValueManager.DynamicSetGameValue(parent, prop, newValue);
        }

        private static GameValueType GetGameValueType<T>() where T : GameValue
            => gameValueTypeCache.GetOrAdd(typeof(T), () => new GameValueType()
            {
                Type = typeof(T),
                ChoosesType = typeof(T).GetStaticPropertyValue<Type>("ChoosesType"), // note: ignores Derived attribute
                ContextRequirements = typeof(T).Attribute<RequiredContextAttribute>()?.RequiredTypes,
                Name = typeof(T).Name,
                Description = typeof(T).GetLocDescription(),
                Category = typeof(T).Attribute<LocCategoryAttribute>()?.Category,
                MarkedUpName = typeof(T).UILink(),
            });

        public string GetCategory() => Localizer.DoStr("Civics");
        public string GetStatus() => string.Empty;
    }
}