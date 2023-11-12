# Copyright (c) 2016 Microsoft Corporation. All Rights Reserved.
# Licensed under the MIT License (MIT)

<#============================================================================
  File:     powershellSamples.ps1
  Summary:  Demonstrates examples to upload/download/delete an item in RS. 
===========================================================================#>

$user = 'extRSAuth'
$pass = 'passphrase'
$pair = "$($user):$($pass)"
$encodedCreds = [System.Convert]::ToBase64String([System.Text.Encoding]::ASCII.GetBytes($pair))
$basicAuthValue = "Basic $encodedCreds"

$Headers = @{
    Authorization = $basicAuthValue
}

$ReportPortalUri = 'https://localhost/reports'

# Upload
Write-Host "Upload an item..."
$uploadItemPath = 'C:\source\test.rdl'
$catalogItemsUri = $ReportPortalUri + "/api/v2.0/CatalogItems"
$bytes = [System.IO.File]::ReadAllBytes($uploadItemPath)
$payload = @{
    "@odata.type" = "#Model.Report";
    "Content" = [System.Convert]::ToBase64String($bytes);
    "ContentType"="";
    "Name" = 'test';
    "Path" = '/test';
    } | ConvertTo-Json
Invoke-WebRequest -Uri $catalogItemsUri -Method Post -Body $payload -ContentType "application/json" -Headers $Headers | Out-Null

# Download
Write-Host "Download an item..."
$downloadPath = 'C:\source\downloaded\test.rdl'
$catalogItemsUri = $ReportPortalUri + "/api/v2.0/CatalogItems(Path='/test')/Content/`$value"
$response = Invoke-WebRequest -Uri $catalogItemsUri -Method Get -Headers $Headers
[System.IO.File]::WriteAllBytes($downloadPath, $response.Content)

# Delete
Write-Host "Delete an item..."
$url = $ReportPortalUri + "/api/v2.0/CatalogItems(Path='/test')"
Invoke-WebRequest -Uri $url -Method Delete -UseDefaultCredentials -Headers $Headers | Out-Null
