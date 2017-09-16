//****************************************************************************//
//                                                                            //
// Download evaluation version: https://bytescout.com/download/web-installer  //
//                                                                            //
// Signup Cloud API free trial: https://secure.bytescout.com/users/sign_up    //
//                                                                            //
// Copyright © 2017 ByteScout Inc. All rights reserved.                       //
// http://www.bytescout.com                                                   //
//                                                                            //
//****************************************************************************//


using System;
using Bytescout.PDFExtractor;

namespace ExtractInfo
{
	class Program
	{
		static void Main(string[] args)
		{
			// Create Bytescout.PDFExtractor.AttachmentExtractor instance
			AttachmentExtractor extractor = new AttachmentExtractor();
			extractor.RegistrationName = "demo";
			extractor.RegistrationKey = "demo";

			// Load sample PDF document
			extractor.LoadDocumentFromFile("Beispielrechnung_ZUGFeRD_RC_COMFORT_neu.pdf");

			// extracting XML invoice which is stored as an attachment
			for (int i = 0; i < extractor.Count; i++)
			{
				Console.WriteLine("Saving XML invoice attachment:\t" + extractor.GetFileName(i));
				
				// save file into the current folder
				extractor.Save(i, extractor.GetFileName(i));

				Console.WriteLine("Done.");
			}

			
			Console.WriteLine();
			Console.WriteLine("Press any key to open the XML invoice extracted...");
			Console.ReadLine();


		        // Open the invoice in default XML viewer
		        System.Diagnostics.Process.Start("ZUGFeRD-invoice.xml");



		}
	}
}
