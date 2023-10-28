using Microsoft.Azure.KeyVault;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace CBITechMeetUpCloudSite.Controllers
{
    public class GetKeyVaultValue
    {
        string clientId = "";
        string clientSecret = "";
        string _keyVault = "";
        public void Main()
        {
            GetSecretValue("TechMeetup");
        }
        public virtual string GetSecretValue(string secretName)
        {
           var KeyVaultClient = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(GetAccessToken));
            string Vault = "https://" + _keyVault + ".vault.azure.net";
            var secret = KeyVaultClient.GetSecretAsync(Vault, secretName).GetAwaiter().GetResult();
            return secret.Value.ToString();

        }
        public async Task<string> GetAccessToken(string authority, string resource, string scope)
        {
            ClientCredential clientCredential = new ClientCredential(clientId, clientSecret);
            var context = new AuthenticationContext(authority, TokenCache.DefaultShared);
            var result = await context.AcquireTokenAsync(resource, clientCredential);

            return result.AccessToken;
        }
    }
}