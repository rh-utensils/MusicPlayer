using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Uno.Foundation;

namespace MusicPlayer.Shared.Helpers
{
    public static class JavaScript
    {
        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = Encoding.Default.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }

        public static string Base64Decode(string base64EncodedData)
        {
            var pattern = new Regex(@"^([A-Za-z0-9+/]{4})*([A-Za-z0-9+/]{3}=|[A-Za-z0-9+/]{2}==)?$");

            if (string.IsNullOrEmpty(base64EncodedData) || !pattern.IsMatch(base64EncodedData)) return "";

            var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
            return Encoding.Default.GetString(base64EncodedBytes);
        }

        public static string RunScript(string name, IEnumerable<(string parameter, string value)> replace = null)
        {
            var function = new File().Read($"{name}.js");

            function = "(() => {\n" + function + "\n})();";

            if (replace == null) return Invoke(function);

            foreach (var (parameter, value) in replace) function = function.Replace(parameter, value);

            return Invoke(function);
        }

        public static async Task<string> RunScriptAsync(string name,
            IEnumerable<(string parameter, string value)> replace = null)
        {
            var function = new File().Read($"{name}.js");

            function = "(async () => {\n" + function + "\n})();";

            if (replace == null) return await InvokeAsync(function);

            foreach (var (parameter, value) in replace) function = function.Replace(parameter, value);

            return await InvokeAsync(function);
        }

        public static string Invoke(string code)
        {
            return WebAssemblyRuntime.InvokeJS(code);
        }

        public static async Task<string> InvokeAsync(string code)
        {
            return await WebAssemblyRuntime.InvokeAsync(code);
        }

        public class Control
        {
            public static void SetStyle(UIElement control, string property, string value)
            {
                var htmlId = control.GetHtmlId();

                Invoke($"document.getElementById('{htmlId}').style.{property} = '{value}';");
            }

            public static void AddClasses(UIElement control, string[] classes)
            {
                var htmlId = control.GetHtmlId();

                foreach (var className in classes)
                    Invoke($"document.getElementById('{htmlId}').classList.add('{className}');");
            }

            public static void RemoveClasses(UIElement control, string[] classes)
            {
                var htmlId = control.GetHtmlId();

                foreach (var className in classes)
                    Invoke($"document.getElementById('{htmlId}').classList.remove('{className}');");
            }
        }
    }
}