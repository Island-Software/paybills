using System;
using System.IO;

namespace Paybills.API.Infrastructure.Helpers
{
    public static class DotEnv
    {
        public static void Load(string filePath) {
            if (!File.Exists(filePath)) {
                return;
            }
            
            var lines = File.ReadAllLines(filePath);
            
            foreach (var line in lines) {
                var parts = line.Split("=");
                
                if (parts.Length == 2) {
                    Environment.SetEnvironmentVariable(parts[0], parts[1]);
                }
            }
        }
    }
}