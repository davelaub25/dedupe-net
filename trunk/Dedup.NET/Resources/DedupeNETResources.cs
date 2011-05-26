using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DedupeNET.Resources.Data;
using System.Reflection;
using System.IO;
using DedupeNET.Configuration;

namespace DedupeNET.Resources
{
    public static partial class DedupeNETResources
    {
        public static string GetStringResource(string qualifiedResourceName)
        {
            string dataProviderClassName = DedupeNETSettings.IDFSettings.DefaultProvider.DataProvider;
            dataProviderClassName = dataProviderClassName.Substring(dataProviderClassName.LastIndexOf('.') + 1);

            char[] separator = { '|' };
            string[] parts = qualifiedResourceName.Split(separator, StringSplitOptions.RemoveEmptyEntries);

            string resourceCategory = parts[0];
            string resorceName = parts[1];

            Assembly assembly = Assembly.GetExecutingAssembly();
            StreamReader streamReader;

            try
            {
                streamReader = new StreamReader(assembly.GetManifestResourceStream(
               string.Format("DedupeNET.Resources.{0}.{1}.{2}", resourceCategory, "Common", resorceName)));
            }
            catch (ArgumentNullException)
            {
                streamReader = new StreamReader(assembly.GetManifestResourceStream(
                    string.Format("DedupeNET.Resoasdurces.{0}.{1}.{2}", resourceCategory, dataProviderClassName, resorceName)));
            }

            return streamReader.ReadToEnd();
        }
    }
}
