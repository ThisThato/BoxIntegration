using System.ComponentModel;

namespace DAL.Models
{
  public class Config
  {

    [DisplayName("ClientID")]
    public string ClientID { get; set; }

    [DisplayName("ClientSecret")]
    public string ClientSecret { get; set; }

    [DisplayName("EnterpriseID")]
    public string EnterpriseId { get; set; }

    [DisplayName("JWTPrivateKeyPassword")]
    public string PrivateKeyPassword { get; set; }

    [DisplayName("JWTPublicKey")]
    public string PublicKey { get; set; }

    [DisplayName("JWTPrivateKey")]
    public string PrivateKey { get; set; }

    [DisplayName("UseRootFolder")]
    public string UseRootFolder { get; set; }

    [DisplayName("BoxFolderId")]
    public string FolderId { get; set; }

    [DisplayName("ItemsCount")]
    public string ItemsCount { get; set; }
    [DisplayName("Path")]
    public string Path { get; set; }

  }
}
