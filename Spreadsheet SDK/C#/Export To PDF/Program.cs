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


using Bytescout.Spreadsheet;
using System.IO;

namespace XLS2PDF
{
    class Program
    {
        static void Main(string[] args)
        {
            Spreadsheet document = new Spreadsheet();

            // load table from existing XLS file        
            document.LoadFromFile("SimpleReport.xls");

            // add image
            document.Workbook.Worksheets[0].Pictures.Add(10, 2, "image.jpg");

            // save as PDF
            document.SaveAsPDF("Output.pdf", false);

            // close the document 
            document.Close();
        }
    }
}
