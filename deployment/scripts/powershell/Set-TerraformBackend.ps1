# this will create Azure resource group and storage account
call az group create --location ukwest --name $(terraformstoragerg)

call az storage account create --name $(terraformstorageaccount) --resource-group $(terraformstoragerg) --location ukwest --sku Standard_LRS

call az storage container create --name terraform --account-name $(terraformstorageaccount)

call az storage account keys list -g $(terraformstoragerg) -n $(terraformstorageaccount)