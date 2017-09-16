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


Imports Bytescout.PDFExtractor

Class Program
	Friend Shared Sub Main(args As String())

        ' Create Bytescout.PDFExtractor.AttachmentExtractor instance
        Dim extractor As New AttachmentExtractor()
		extractor.RegistrationName = "demo"
		extractor.RegistrationKey = "demo"

		' Load sample PDF document
        extractor.LoadDocumentFromFile("Beispielrechnung_ZUGFeRD_RC_COMFORT_neu.pdf")

        Dim i As Integer

	' extract the XML invoice which is stored as an attachment
        For i = 0 To extractor.Count - 1
            Console.WriteLine("Saving XML attachment:       " + extractor.GetFileName(i))
            extractor.Save(i, extractor.GetFileName(i)) ' save into the current folder
            Console.WriteLine("Done")
        Next


        Console.WriteLine("Press any key to continue and open XML...")
        Console.ReadLine()

        ' Open the invoice in default XML viewer        
	System.Diagnostics.Process.Start("ZUGFeRD-invoice.xml")

	End Sub
End Class
