
namespace DAL.Models
{
  public class State
  {
    public string AuthToken { get; }
    public string SshKey { get; }
    public static bool  RefreshToken{get;set;}

  }
}
