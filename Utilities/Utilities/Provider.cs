using System;
using System.Collections.Generic;
using System.IO;
using Authetication;
using DAL.Models;
using DAL.Core;
using DAL.Enums;
using System.Threading.Tasks;
using System.Linq;
using System.Text.RegularExpressions;
using Box.V2.Models;

namespace Logic
{

  public class Provider
  {

    private Config Config { get; set; }

    //<FileName, File> 
    Dictionary<string, byte[]> Files = new Dictionary<string, byte[]>();

    public Provider()
    {
      this.Config = ConfigHelper.GetConfig();
    }


    public async Task<bool> DowloadFiles()
    {

      if (Config != null)
      {
        try
        {

          BoxCollection<BoxItem> boxFolderItems = await SessionInfo.Client.FoldersManager.GetFolderItemsAsync(Config.FolderId, int.Parse(Config.ItemsCount));

          if (boxFolderItems.TotalCount > 0)
          {

            foreach (BoxItem item in boxFolderItems.Entries)
            {

              //TODO: Filter the files by DateCreated 
              DateTime res = DateTime.Now.AddMonths(-1); //File must be created within the last month
              if (item.Type.ToLower() != "folder")
              {

                //DownloadFiles
                using Stream stream = await SessionInfo.Client.FilesManager.DownloadAsync(item.Id);
                using MemoryStream memoryStream = new MemoryStream();
                stream.CopyTo(memoryStream);
                byte[] file = memoryStream.ToArray();

                Files.Add(item.Name, file);

              }
            }

            return true;
          }

        }
        catch (Exception ex)
        {
          Logging.WriteLog($"Provider Download File \nException: {ex.Message}", "DownloadFile", LogSeverity.Debug);
          Files.Clear();
        }
      }

      return false;

    }


    public async void UploadFile()
    {
      try
      {

        string path = "C:\\Users\\Thato.Motaung\\Downloads\\ExtensionSummary Report - Updated.xlsx";
        string dateTime = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");

        using (FileStream stream = new FileStream(path, FileMode.Open))
        {
          //Create a Parent Entity
          BoxRequestEntity entity = new BoxRequestEntity() { Id = dateTime, Type = BoxType.file };

          BoxFileRequest request = new BoxFileRequest()
          {
            Name = "ExtensionSummary Report - Updated.xlsx",
            Parent = new BoxRequestEntity() { Id = dateTime, Type = BoxType.file }
          };
          BoxFile uploadedFile = await SessionInfo.Client.FilesManager.UploadAsync(request, stream);
        }

      }
      catch (Exception ex)
      {
        Logging.WriteLog($"Provider File \nException: {ex.Message}", "UploadFile", LogSeverity.Debug);
      }

     
    }

    public void SendFiles()
    {

      try
      {
        if (Files.Count > 0)
        {
          foreach (var file in Files)
          {
            File.WriteAllBytes($"{file.Key}", file.Value);
       
          }
        }

        Console.WriteLine("Files successfuly downloaded...");
      }

      catch (Exception ex)
      {
        Logging.WriteLog($"Provider File \nException: {ex.Message}", "SendFiles", LogSeverity.Debug);
      }

    }

  }

}
