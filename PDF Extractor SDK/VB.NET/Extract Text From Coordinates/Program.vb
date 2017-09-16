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


Imports System.Drawing
Imports System.IO
Imports Bytescout.PDFExtractor



Class Program
	Friend Shared Sub Main(args As String())

		' Create Bytescout.PDFExtractor.TextExtractor instance
		Dim extractor As New TextExtractor()
		extractor.RegistrationName = "demo"
		extractor.RegistrationKey = "demo"

		' Load sample PDF document
		extractor.LoadDocumentFromFile("sample2.pdf")

		' define a rectangle location to get text from it from pdf at 0,0 with width and height as 200x200 accordingly
            	Dim location as RectangleF  = new RectangleF(0, 0, 200, 200)
                
            	' set text extractor extraction area to this rectangle
            	extractor.SetExtractionArea(location)

            	' now we can get text from this pdf rectangle from page #0
        Dim extractedString As String = extractor.GetTextFromPage(0)
                
            	' write text from pdf rectangle to the console
        Console.WriteLine("Extracted from page #0" + ":\r\n" + extractedString)

	End Sub
End Class
