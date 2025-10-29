using Eco.Core.Utils;
using Eco.Gameplay.Civics.GameValues;
using Eco.Shared.Localization;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Civics
{
    public static class FailNullSafeExtension
    {
        public static bool GetValueOrFailNullSafe<T, U>(this GameValue<T> gameValue, Eval<U> eval, string paramName, out U value, out Eval<T> failure, T returnValue = default)
        {
            value = eval.Val;
            if (value != null)
            {
                failure = null;
                return true;
            }
            if (eval != null)
            {
                failure = Eval.Make($"Invalid {Localizer.DoStr(paramName)} specified on {gameValue.GetType().GetLocDisplayName()}: {eval.Message}", returnValue);
                return false;
            }
            failure = Eval.Make($"{Localizer.DoStr(paramName)} not set on {gameValue.GetType().GetLocDisplayName()}.", returnValue);
            return false;
        }
    }
}
