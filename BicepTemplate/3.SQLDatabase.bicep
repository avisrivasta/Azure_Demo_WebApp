// Param Declaration
param pLocation string = resourceGroup().location
param pPrefix string='predemo-'
param sqlServerName string= '${pPrefix}bicepsqlservercbtechmeetup'
param sqlDatabaseName string = '${pPrefix}bicepsqldatabase'

// SQL Server
resource sqlServer 'Microsoft.Sql/servers@2014-04-01' ={
  name: sqlServerName
  location: pLocation
  properties: {
    administratorLogin:'sqladmin'
    administratorLoginPassword:'Test@12345678'
  }
}

// SQL Database
resource sqlServerDatabase 'Microsoft.Sql/servers/databases@2014-04-01' = {
  // Nesting relation
  parent: sqlServer
  name: sqlDatabaseName
  location: pLocation
  properties: {
    collation: 'SQL_Latin1_General_CP1_CI_AS'
    edition: 'Basic'
    maxSizeBytes: '2147483648'
    requestedServiceObjectiveName: 'Basic'
  }
}
