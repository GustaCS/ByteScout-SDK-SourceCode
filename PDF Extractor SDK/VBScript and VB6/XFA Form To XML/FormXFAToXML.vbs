'****************************************************************************'
'                                                                            '
' Download evaluation version: https://bytescout.com/download/web-installer  '
'                                                                            '
' Signup Cloud API free trial: https://secure.bytescout.com/users/sign_up    '
'                                                                            '
' Copyright © 2017 ByteScout Inc. All rights reserved.                       '
' http://www.bytescout.com                                                   '
'                                                                            '
'****************************************************************************'


' Create Bytescout.PDFExtractor.XMLExtractor object
Set extractor = CreateObject("Bytescout.PDFExtractor.XFAFormExtractor")

extractor.RegistrationName = "demo"
extractor.RegistrationKey = "demo"

' Load sample PDF document
extractor.LoadDocumentFromFile "..\..\samplexfa.pdf"


for i=0 to 12 ' iterate through 12 content types (see XFAFormContentType enum in the documentation, there are 13 types)

count = extractor.GetCount(i) ' get count of the given type
 For j=0 to count-1
   extractor.SaveToFile i, j, CStr(i) & "-" & CStr(j) & ".xml"
 Next

Next


MsgBox "XFA xml data has been extracted"

