using System.Reflection;
using HarmonyLib;
using Sandbox.Game.World;

namespace ClientPlugin.Extensions
{
    public static class MySessionExtensions
    {
        private static readonly FieldInfo UpdateAllowedFieldInfo = AccessTools.Field(typeof(MySession), "m_updateAllowed");
        public static bool IsUpdateAllowed(this MySession self)
        {
            return (bool)UpdateAllowedFieldInfo.GetValue(self);
        }
        public static void SetUpdateAllowed(this MySession self, bool value)
        {
            UpdateAllowedFieldInfo.SetValue(self, value);
        }
    }
}