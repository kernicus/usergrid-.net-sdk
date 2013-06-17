using System;
using System.Configuration;
using Usergrid.Sdk.Model;

namespace Usergrid.Sdk.IntegrationTests
{
	public class BaseTest
	{
		private static Random random = new Random(DateTime.Now.Millisecond);

	    private readonly Configuration _config;
	    public BaseTest()
	    {
            var configMap = new ExeConfigurationFileMap {ExeConfigFilename = "MySettings.config"};
			Configuration config = ConfigurationManager.OpenMappedExeConfiguration(configMap, ConfigurationUserLevel.None);
	        if (config.HasFile && config.AppSettings.Settings.Count > 0)
	            _config = config;
	    }

	    protected string Organization
		{
			get{ return GetAppSetting("organization");}
		}

		protected string Application
		{
			get{ return GetAppSetting("application");}
		}

		protected string ClientId
		{
			get { return GetAppSetting("clientId");}
		}

		protected string ClientSecret
		{
			get{ return GetAppSetting("clientSecret");}
		}

		protected string UserId
		{
			get { return GetAppSetting("userId");}
		}

		protected string UserSecret
		{
			get{ return GetAppSetting("userSecret");}
		}

        protected string P12CertificatePath
		{
            get { return GetAppSetting("p12CertificatePath"); }
		}

        protected string GoogleApiKey
		{
            get { return GetAppSetting("googleApiKey"); }
		}

        private string GetAppSetting(string key)
        {
            return _config == null ? ConfigurationManager.AppSettings[key] : _config.AppSettings.Settings[key].Value;
        }

        protected IClient InitializeClientAndLogin(AuthType authType)
        {
            var client = new Client(Organization, Application);
            if (authType == AuthType.Application || authType == AuthType.ClientId)
                client.Login(ClientId, ClientSecret, authType);
            else if (authType == AuthType.User)
                client.Login(UserId, UserSecret, authType);

            return client;
        }

		protected static int GetRandomInteger(int minValue, int maxValue)
		{
			return random.Next (minValue, maxValue);
		}

	    protected void DeleteEntityIfExists<TEntity>(IClient client, string collectionName, string entityIdentifier)
	    {
	        var customer = client.GetEntity<TEntity>(collectionName, entityIdentifier);

	        if (customer != null)
	            client.DeleteEntity(collectionName, entityIdentifier);
	    }
	}
}

