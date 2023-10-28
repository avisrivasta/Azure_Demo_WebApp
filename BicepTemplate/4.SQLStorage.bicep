// Param Declaration
param pLocation string = resourceGroup().location
param pPrefix string='predemo'
param pStorageAccount string='${pPrefix}bicepsaccount2'

//Storage Accounts
resource storageaccount 'Microsoft.Storage/storageAccounts@2021-02-01' = {
  name: pStorageAccount
  location: pLocation
  kind: 'StorageV2'
  sku: {
    name: 'Premium_LRS'
  }
}
