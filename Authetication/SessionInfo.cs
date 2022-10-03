using Box.V2;
using Box.V2.Config;
using Box.V2.JWTAuth;
using System;
using System.Threading.Tasks;
using DAL.Models;
using System.Threading;

namespace Authetication
{

  public class SessionInfo
  {

    public  static string AccessToken { get; set; }
    public static BoxClient Client {get; set; }


    public async Task<BoxClient> Authenticate()
    {
      BoxJWTAuth boxJWT = BuildConfig();
      //Authenticate
      AccessToken = await boxJWT.AdminTokenAsync(); //valid for 60 minutes so should be cached and re-used
      Client = boxJWT.AdminClient(AccessToken);
      return Client;
    }

    private  BoxJWTAuth BuildConfig()
    {
      //Build Config
      Config config = ConfigHelper.GetConfig();
      var boxConfig = new BoxConfigBuilder(config.ClientID, config.ClientSecret, config.EnterpriseId, config.PrivateKey, config.PrivateKeyPassword, config.PublicKey).Build();
      return new BoxJWTAuth(boxConfig);

    }

  }
}
