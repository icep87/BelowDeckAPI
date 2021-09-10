# BelowDeckAPI
Backend for my React CMS Client 

You need to setup configure appsettings.json with the following information: 

{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  },
  "ConnectionStrings": {
    "CMSDatabase": "Data Source = localhost; Initial Catalog = DATABASE; User Id = USERNAME; Password = PASSWORD;"
  },
  "ClientSecrets": {
    "Value": "LONGSTRING",
    "Issuer": "BelowDeckAPI",
    "Audience": "BelowDeckAPI"
  },

  "AllowedHosts": "*"
}
