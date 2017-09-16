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


Imports System.IO
Imports System.Threading
Imports Bytescout.PDFExtractor

Module Module1

	<MTAThread>
	Sub Main()

		Const inputFile = "sample.pdf"
		Const resultFile = "result.pdf"

		Dim pageCount As Integer

		' Get document page count
		Using infoExtractor = New InfoExtractor()
			infoExtractor.LoadDocumentFromFile(inputFile)
			pageCount = infoExtractor.GetPageCount()
		End Using

		' Process the document by 10-page pieces
		Dim numberOfThreads As Integer
		numberOfThreads = pageCount \ 10
		If (pageCount - numberOfThreads * 10 > 0) Then numberOfThreads = numberOfThreads + 1

		Dim doneEvents(numberOfThreads - 1) As WaitHandle
		Dim stopwatch As Stopwatch = Stopwatch.StartNew()
		Dim startPage, endPage As Integer
		Dim pieces(numberOfThreads - 1) As String

		' Run threads
		For i As Integer = 0 To numberOfThreads - 1

			doneEvents(i) = New ManualResetEvent(False)
			startPage = i * 10
			endPage = Math.Min(pageCount - 1, (i + 1) * 10 - 1)
			
			If numberOfThreads = 1 Then
				endPage = endPage - 1
			End If
						
			pieces(i) = String.Format("temp-{0}-{1}.pdf", startPage, endPage)
			ThreadPool.QueueUserWorkItem(New WaitCallback(AddressOf ThreadProc), New Object() {i, doneEvents(i), inputFile, pieces(i), startPage, endPage})

		Next

		' Wait for all threads
		WaitHandle.WaitAll(doneEvents)

		' Merge pieces 
		Using merger = New DocumentMerger
			merger.Merge(pieces, resultFile)
		End Using

		' Delete temp files
		For Each tempFile As String In pieces
			File.Delete(tempFile)
		Next
		
		Console.WriteLine("All done in {0}.", stopwatch.Elapsed)
		Console.WriteLine()

		Console.WriteLine("Press any key to exit...")
		Console.ReadKey()

	End Sub

	Sub ThreadProc(ByVal stateInfo As Object)

		Dim threadIndex As Integer = stateInfo(0)
		Dim waitEvent As ManualResetEvent = stateInfo(1)
		Dim inputFile As String = stateInfo(2)
		Dim outputFile As String = stateInfo(3)
		Dim startPage As Integer = stateInfo(4)
		Dim endPage As Integer = stateInfo(5)

		Console.WriteLine("Thread #{0} started with the page range from {1} to {2}.", threadIndex, startPage, endPage)

		Dim stopwatch As Stopwatch = Stopwatch.StartNew()

		' Extract a piece of document
		Dim chunk As String = String.Format("temp-{0}-{1}", startPage, endPage)
		Using splitter = New DocumentSplitter
			splitter.ExtractPageRange(inputFile, chunk, startPage + 1, endPage + 1)
		End Using

		' Process the piece
		Using searchablePdfMaker As New SearchablePDFMaker("demo", "demo")

			searchablePdfMaker.OCRDetectPageRotation = True
			searchablePdfMaker.OCRLanguageDataFolder = "C:\Program Files\Bytescout PDF Extractor SDK\net4.00\tessdata"
			searchablePdfMaker.LoadDocumentFromFile(chunk)

			' 300 DPI resolution is recommended. 
			' Using of higher values will slow down the processing but does not guarantee the higher quality.
			searchablePdfMaker.OCRResolution = 300

			searchablePdfMaker.MakePDFSearchable(outputFile)

		End Using

		File.Delete(chunk)

		Console.WriteLine("Thread #{0} finished in {1}.", threadIndex, stopwatch.Elapsed)

		waitEvent.Set()

	End Sub

End Module
