using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataInvoice.SOLUTIONS.MANAGEMENT.ORGANIZEDREPOSITORY
{
    public class OrganizedRepositoryManager
    {


        public string localRootDirectory = @"C:\Users\alexlet\SharePoint\Nuegy - Documents\";
        public void CopyFile(OrganizedRepository repo, System.IO.FileInfo fi, Dictionary<string, string> customFieldResutls = null, string userRenameFile = null)
        {
            if (repo.RepositoryMode != ENUMS.RepositoryModeEnum.LOCALDIRECTORY) throw new Exception("Mode non disponible");

            string directorytocopy = GenerateCustomName(repo.FormatedDirectoryName, customFieldResutls);
            string filetocopy = "";

            if (!string.IsNullOrWhiteSpace(repo.FormatedFileName))
                filetocopy = GenerateCustomName(repo.FormatedFileName, customFieldResutls);
            else if (!string.IsNullOrWhiteSpace(userRenameFile))
                filetocopy = userRenameFile;
            else filetocopy = fi.Name;

            string fullpath = NGLib.COMPONENTS.FILE.FileTools.SlashEnd(localRootDirectory) + NGLib.COMPONENTS.FILE.FileTools.SlashEnd(directorytocopy) + filetocopy;


            // COPIE
            System.IO.FileInfo fifullpath = new System.IO.FileInfo(fullpath);
            if (!fifullpath.Directory.Exists && repo.CreateDirectoryIfNotExist)
                fifullpath.Directory.Create();
            else if (!fifullpath.Directory.Exists) throw new Exception("Output Directory not found");

            System.IO.File.Copy(fi.FullName, fullpath, false);


        }




        public static string GenerateCustomName(string CompleteName, Dictionary<string, string> customFieldResutls)
        {
            if (customFieldResutls == null)
                customFieldResutls = new Dictionary<string, string>();
            string FinalName = CompleteName;
            foreach (string itemkey in customFieldResutls.Keys)
            {
                if (customFieldResutls[itemkey] == null) continue;
                string textoreplace = "{" + itemkey + "}";
                FinalName = FinalName.Replace(textoreplace, customFieldResutls[itemkey]);
            }
            return FinalName;
        }



    }
}
