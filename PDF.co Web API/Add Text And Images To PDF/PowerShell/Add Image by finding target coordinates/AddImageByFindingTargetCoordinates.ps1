# The authentication key (API Key).
# Get your own by registering at https://app.pdf.co/documentation/api
$API_KEY = "***********************************"

# Direct URL of source PDF file.
$SourceFileUrl = "https://bytescout-com.s3.amazonaws.com/files/demo-files/cloud-api/pdf-edit/sample.pdf"

# Search string.
$SearchString = 'Your Company Name'

# Prepare URL for PDF text search API call.
# See documentation: https://app.pdf.co/documentation/api/1.0/pdf/find.html
$queryFindText = "https://api.pdf.co/v1/pdf/find?url=$($SourceFileURL)&searchString=$($SearchString)"
$queryFindText = [System.Uri]::EscapeUriString($queryFindText)

try {
    # Execute request
    $jsonResponseFindText = Invoke-RestMethod -Method Get -Headers @{ "x-api-key" = $API_KEY } -Uri $queryFindText

    if ($jsonResponseFindText.error -eq $false) {

        # Display search information
        $itemFindText = $jsonResponseFindText.body[0];

        Write-Host "Found text at coordinates $($itemFindText.left), $($itemFindText.top)"


        #Comma-separated list of page indices (or ranges) to process. Leave empty for all pages. Example: '0,2-5,7-'.
        $Pages = ""

        # PDF document password. Leave empty for unprotected documents.
        $Password = ""

        # Destination PDF file name
        $DestinationFile = "./result.pdf"

        # Image params
        $Type = "image"
        $X = 450
        $Y = $itemFindText.top
        $Width = 119
        $Height = 32
        $ImageUrl = "https://bytescout-com.s3.amazonaws.com/files/demo-files/cloud-api/pdf-edit/logo.png"

        $resultFileName = [System.IO.Path]::GetFileName($DestinationFile)

        # * Add image *
        # Prepare request to `PDF Edit` API endpoint
        $query = "https://api.pdf.co/v1/pdf/edit/add?name=$($resultFileName)&password=$($Password)&pages=$($Pages)&url=$($SourceFileUrl)&type=$($Type)&x=$($X)&y=$($Y)&width=$($Width)&height=$($Height)&urlimage=$($ImageUrl)";
        $query = [System.Uri]::EscapeUriString($query)

        try {
            # Execute request
            $jsonResponse = Invoke-RestMethod -Method Get -Headers @{ "x-api-key" = $API_KEY } -Uri $query

            if ($jsonResponse.error -eq $false) {
                # Get URL of generated barcode image file
                $resultFileUrl = $jsonResponse.url
                
                # Download output file
                Invoke-WebRequest -Uri $resultFileUrl -OutFile $DestinationFile

                Write-Host "Generated PDF saved to '$($DestinationFile)' file."
            }
            else {
                # Display service reported error
                Write-Host $jsonResponse.message
            }
        }
        catch {
            # Display request error
            Write-Host $_.Exception
        }
    }
    else {
        # Display service reported error
        Write-Host $jsonResponse.message
    }
}
catch {
    # Display request error
    Write-Host $_.Exception
}