using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GroceryWalletCalculator.Keys
{
    public class Keys
    {
        public string MicrosoftVisionApiKey { get; set; }

        private static Keys _instance;
        public static Keys GetKeys()
        {
            if (_instance != null) return _instance;

            var assembly = typeof(Keys).GetTypeInfo().Assembly;
            var stream = assembly.GetManifestResourceStream("GroceryWalletCalculator.Keys.keys.json");
            using (var reader = new StreamReader(stream))
            {
                _instance = JsonConvert.DeserializeObject<Keys>(reader.ReadToEnd());
            }
            return _instance;
        }
    }
}
