// Scope of Resource Group
targetScope='subscription'

// Parameter Declaration
param pLocation string = 'eastus'
param pPrefix string='predemo-'

// Concatenation Operation
param pResourceGroupName string = '${pPrefix}biceprgeus'

// Resource Group
resource resourceGroup 'Microsoft.Resources/resourceGroups@2021-04-01' = {
  name: pResourceGroupName
  location: pLocation
}

