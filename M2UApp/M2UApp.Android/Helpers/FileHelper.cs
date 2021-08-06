using System.IO;
using M2UApp.Droid.Helpers;
using M2UApp.Helpers;
using System;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileHelper))]
namespace M2UApp.Droid.Helpers
{
    public class FileHelper : IFileHelper
    {
        public string GetLocalFilePath(string filename)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            return Path.Combine(path, filename);
        
        }
    }
}