using System;
using System.IO;

namespace MusicPlayer.Shared.Helpers
{
    public class File
    {
        public string Read(string file)
        {
            var stream = GetStreamFromResource(file, GetType());
            var reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }

        private static Stream GetStreamFromResource(string resourceName, Type typeCalling)
        {
            try
            {
                var assembly = typeCalling?.Assembly;
                var resources = assembly?.GetManifestResourceNames();
                foreach (var sResourceName in resources)
                    if (sResourceName.ToUpperInvariant()
                        .EndsWith(resourceName.ToUpperInvariant(), StringComparison.InvariantCulture))
                        return assembly?.GetManifestResourceStream(sResourceName);
            }
            catch
            {
                // Ignore
            }

            throw new Exception("Unable to find resource file: " + resourceName);
        }
    }
}