""" Cloud API asynchronous "PDF To Text" job example.
    Allows to avoid timeout errors when processing huge or scanned PDF documents.
"""
import os
import requests # pip install requests
import time
import datetime

# Please NOTE: In this sample we're assuming Cloud Api Server is hosted at "https://localhost". 
# If it's not then please replace this with with your hosting url.

# Base URL for PDF.co Web API requests
BASE_URL = "https://localhost"

# Source PDF file
SourceFile = ".\\sample.pdf"

# Comma-separated list of page indices (or ranges) to process. Leave empty for all pages. Example: '0,2-5,7-'.
Pages = ""

# PDF document password. Leave empty for unprotected documents.
Password = ""

# Search string. 
SearchString = "\d{1,}\.\d\d" # Regular expression to find numbers like '100.00'
                              # Note: do not use `+` char in regex, but use `{1,}` instead.
                              # `+` char is valid for URL and will not be escaped, and it will become a space char on the server side.

# Enable regular expressions (Regex) 
RegexSearch = True

# (!) Make asynchronous job
Async = True


def main(args = None):
    uploadedFileUrl = uploadFile(SourceFile)
    if (uploadedFileUrl != None):
        searchTextInPDF(uploadedFileUrl)


def searchTextInPDF(uploadedFileUrl):
    """Search Text using PDF.co Web API"""

    # Prepare URL for 'PDF Text Search' API request
    url = "{}/pdf/find?async={}&password={}&pages={}&url={}&searchString={}&regexSearch={}".format(
        BASE_URL,
        Async,
        Password,
        Pages,
        uploadedFileUrl,
        SearchString,
        RegexSearch
    )

    # Execute request and get response as JSON
    response = requests.get(url, headers={  "content-type": "application/octet-stream" })
    if (response.status_code == 200):
        json = response.json()

        if json["error"] == False:
            # Asynchronous job ID
            jobId = json["jobId"]
            #  URL of the result file
            resultFileUrl = json["url"]
            
            # Check the job status in a loop. 
            # If you don't want to pause the main thread you can rework the code 
            # to use a separate thread for the status checking and completion.
            while True:
                status = checkJobStatus(jobId) # Possible statuses: "working", "failed", "aborted", "success".
                
                # Display timestamp and status (for demo purposes)
                print(datetime.datetime.now().strftime("%H:%M.%S") + ": " + status)
                
                if status == "success":
                    resp_res = requests.get(resultFileUrl);
                    json_resp_res = resp_res.json()

                    # Display found information
                    for item in json_resp_res:
                        print(f"Found text {item['text']} at coordinates {item['left']}, {item['top']}")

                elif status == "working":
                    # Pause for a few seconds
                    time.sleep(3)
                else:
                    print(status)
                    break
        else:
            # Show service reported error
            print(json["message"])
    else:
        print(f"Request error: {response.status_code} {response.reason}")


def checkJobStatus(jobId):
    """Checks server job status"""

    url = f"{BASE_URL}/job/check?jobid={jobId}"
    
    response = requests.get(url)
    if (response.status_code == 200):
        json = response.json()
        return json["status"]
    else:
        print(f"Request error: {response.status_code} {response.reason}")

    return None


def uploadFile(fileName):
    """Uploads file to the cloud"""
    
    # 1. RETRIEVE PRESIGNED URL TO UPLOAD FILE.

    # Prepare URL for 'Get Presigned URL' API request
    url = "{}/file/upload/get-presigned-url?contenttype=application/octet-stream&name={}".format(
        BASE_URL, os.path.basename(fileName))
    
    # Execute request and get response as JSON
    response = requests.get(url)
    if (response.status_code == 200):
        json = response.json()
        
        if json["error"] == False:
            # URL to use for file upload
            uploadUrl = json["presignedUrl"]
            # URL for future reference
            uploadedFileUrl = json["url"]

            # 2. UPLOAD FILE TO CLOUD.
            with open(fileName, 'rb') as file:
                requests.put(uploadUrl, data=file, headers={  "content-type": "application/octet-stream" })

            return uploadedFileUrl
        else:
            # Show service reported error
            print(json["message"])    
    else:
        print(f"Request error: {response.status_code} {response.reason}")

    return None


if __name__ == '__main__':
    main()