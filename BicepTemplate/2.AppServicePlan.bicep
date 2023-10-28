// Param Declaration

//Function Usage
param pLocation string= resourceGroup().location
param pPrefix string='predemo-'
param pAppServicePlan string = '${pPrefix}bicepaspeuspredemo'
param pWebApplication string = '${pPrefix}bicepwaeus'
param pAppInsightsComponents string = '${pPrefix}bciepappinsights'

// App Service Plan
resource appServicePlan 'Microsoft.Web/serverfarms@2020-12-01' = {
  name: pAppServicePlan
  location: pLocation
  sku: {
    name: 'F1'
    capacity: 1
  }
}

// Web Application
resource webApplication 'Microsoft.Web/sites@2021-01-15' = {
  name: pWebApplication
  location: pLocation
  //Dependency
  properties: {
    serverFarmId: appServicePlan.id
  }
}

// Web Application Config
resource webApplicationConfig 'Microsoft.Web/sites/config@2021-01-15' = {
  name: 'web'
  parent:webApplication
  properties:{
    appSettings:[
      {
        name: 'APPINSIGHTS_INSTRUMENTATIONKEY'
        // Dependency 
        value:appInsightsComponents.properties.InstrumentationKey
      }
    ]
  }
  // dependsOn:[
  //   appInsightsComponents
  // ]
}

// Application Insights
resource appInsightsComponents 'Microsoft.Insights/components@2020-02-02' = {
  name: pAppInsightsComponents
  location: pLocation
  kind: 'web'
  properties: {
    Application_Type: 'web'
  }
}
