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
using System.IO;
using System.Text;
using Bytescout.PDFExtractor;
using System.Drawing;
using System.Diagnostics;

namespace Example
{
    class Program
    {
        static void Main(string[] args)
        {

            TextExtractor extractor = new TextExtractor("demo", "demo");

            // load the document
            extractor.LoadDocumentFromFile("sample2.pdf");

            string searchString = "what";
            
            // get page count
            int pageCount = extractor.GetPageCount();
            int count = 0;

            // iterate through pages
            for (int i = 0; i < pageCount; i++)
            {
                // search for text string
                if (extractor.Find(i, searchString, false))
                {
                    do
                    {
                        count++;

                        // output search results
                        Console.WriteLine("Found on page " + i + " at location " + extractor.FoundText.Bounds.ToString());

                        // now we are getting the found text
			string extractedString = extractor.FoundText.Text;
                        Console.WriteLine("Extracted string: " + extractedString);

                    }
                    while (extractor.FindNext()); // search next occurance of the search string
                }
            }



            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();

        }
    }
}
