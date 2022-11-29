﻿using System.Reflection;

namespace Starlight
{
    public static class AssemblyResolver
    {
        public static Assembly? Resolve(ResolveEventArgs args, ServerConfiguration context)
        {
            string fileName = args.Name.Split(',')[0];
            string path = Path.Combine(context.PluginPath, fileName + ".dll");

            if (File.Exists(path))
            {
                if (!context.LoadedAssemblies.TryGetValue(fileName, out var assembly))
                {
                    assembly = Assembly.Load(File.ReadAllBytes(path));
                    context.LoadedAssemblies.Add(fileName, assembly);
                }
                return assembly;
            }
            return null;
        }

        public static void ResolvePlugins(ServerConfiguration context)
        {
            if (!Directory.Exists(context.PluginPath))
                Directory.CreateDirectory(context.PluginPath);

            var files = new DirectoryInfo(context.PluginPath)
                .GetFiles("*.dll")
                .ToList();

            foreach (var item in files)
            {
                var name = Path.GetFileNameWithoutExtension(item.Name);

                if (!context.LoadedAssemblies.TryGetValue(name, out var assembly))
                {
                    byte[]? pe = null;

                    try
                    {
                        var pdb = Path.ChangeExtension(item.FullName, ".pdb");
                        var symbols = File.Exists(pdb)
                            ? File.ReadAllBytes(pdb)
                            : null;

                        assembly = Assembly.Load(pe = File.ReadAllBytes(item.FullName), symbols);
                    }
                    catch (BadImageFormatException)
                    {
                        continue;
                    }
                    catch (FileLoadException)
                    {
                        throw;
                    }

                    context.LoadedAssemblies.Add(name, assembly);
                }
            }
        }
    }
}
