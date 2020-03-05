using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace w3botLauncher.Utils
{
    public static class RegistryUtils
    {
        public const string REGISTRY_SUBKEY = "w3bot";
        public const string REGISTRY_VALUE_TYPE = "Path";

        public static bool IsRegistrySubKeyAvailable()
        {
            using (var subKey = Registry.CurrentUser.OpenSubKey(REGISTRY_SUBKEY, true))
            {
                if (subKey == null)
                    return false;
            }

            return true;
        }

        public static bool IsRegistryEntryAvailable()
        {
            if (!IsRegistrySubKeyAvailable())
                return false;

            using (var subKey = Registry.CurrentUser.OpenSubKey(REGISTRY_SUBKEY, true))
            {
                var valueType = subKey.GetValue(REGISTRY_VALUE_TYPE);
                if (valueType == null)
                    return false;
            }

            return true;
        }

        public static void CreateRegistryEntry(string installPath)
        {
            if (!IsRegistrySubKeyAvailable())
                Registry.CurrentUser.CreateSubKey(REGISTRY_SUBKEY);

            if (!IsRegistryEntryAvailable())
            {
                var key = Registry.CurrentUser.OpenSubKey(REGISTRY_SUBKEY, true);
                key.SetValue(REGISTRY_VALUE_TYPE, installPath);
            }
        }
    }
}
