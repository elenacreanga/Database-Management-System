using System.IO;

namespace DbManagementSystem.Core
{
    public static class Constants
    {
        private static readonly string ProjectFolderPath = Directory
            .GetParent(Directory.GetCurrentDirectory())
            .Parent
            .Parent
            .FullName;
        public static readonly string ServerLocation = ProjectFolderPath + @"\DatabaseWorkspace";
    }
}
