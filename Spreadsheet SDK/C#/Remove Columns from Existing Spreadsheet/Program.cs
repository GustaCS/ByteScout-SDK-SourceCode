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
using Bytescout.Spreadsheet;
using System.IO;
using System.Diagnostics;

namespace Removing_columns_from_existing_XLS
{
    class Program
    {
        static void Main(string[] args)
        {
            // Open Spreadsheet
            Spreadsheet document = new Spreadsheet();
	document.LoadFromFile("AdvancedReport.xls");

            // Get Worksheet
            Worksheet worksheet = document.Workbook.Worksheets[0];

            // Remove Column
            worksheet.Columns.Delete(1, 1);

            // delete output file if exists already
            if (File.Exists("Output.xls")){
                File.Delete("Output.xls");
            }

            // Save document
            document.SaveAs("Output.xls");

            // Close Spreadsheet
            document.Close();

            // open generated XLS document in default program
            Process.Start("Output.xls");
        }
    }
}
