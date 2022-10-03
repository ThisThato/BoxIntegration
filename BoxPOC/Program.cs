using System.Threading.Tasks;
using System;
using Authetication;
using DAL;
using Logic;
using Box.V2;

namespace BoxPOC
{
 
  class Program
  {

    static async Task Main(string[] args)
    {

      SessionInfo session = new SessionInfo();

      //Will there be a need to use the client in the frontend?
      BoxClient client = await session.Authenticate();

      //Instantiate a provider to communicate with box folder
      Provider provider = new Provider();

      //Download the files from Box
      await provider.DowloadFiles();

      //Dump the files to a folder
      provider.SendFiles();

      //TODO: UploadFles to Box
      provider.UploadFile();

      Console.WriteLine("Files successfuly downloaded...");
      Console.WriteLine("Files");


      Console.ReadLine();

    }
  }
}
