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
using System.Collections.Generic;
using System.Text;
using Bytescout.PDFExtractor;
using System.Diagnostics;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create Bytescout.PDFExtractor.XFDFExtractor instance
            XFDFExtractor extractor = new XFDFExtractor();
            extractor.RegistrationName = "demo";
            extractor.RegistrationKey = "demo";

            // Load sample PDF document
            extractor.LoadDocumentFromFile("sample3.pdf");

            extractor.SaveXFDFToFile("output.xml");

            Console.WriteLine();
            Console.WriteLine("Data has been extracted to 'output.xml' file.");
            Console.WriteLine();
            Console.WriteLine("Press any key to continue and open file in default associated application...");
            Console.ReadKey();
            Process.Start("output.xml");
        }
    }
}
