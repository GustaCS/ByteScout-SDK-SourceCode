//*******************************************************************************************//
//                                                                                           //
// Download Free Evaluation Version From: https://bytescout.com/download/web-installer       //
//                                                                                           //
// Also available as Web API! Get Your Free API Key: https://app.pdf.co/signup               //
//                                                                                           //
// Copyright © 2017-2020 ByteScout, Inc. All rights reserved.                                //
// https://www.bytescout.com                                                                 //
// https://pdf.co                                                                            //
//                                                                                           //
//*******************************************************************************************//


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CreateBarCode
{
    class QrCodeEmailTemplate
    {
        #region Constructors

        public QrCodeEmailTemplate() { }
        public QrCodeEmailTemplate(string Email, string Subject, string Message)
        {
            this.Email = Email;
            this.Subject = Subject;
            this.Message = Message;
        }

        #endregion

        #region Properties

        public string Email { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }

        #endregion

        #region Overloaded Methods

        public override string ToString()
        {
            if (string.IsNullOrEmpty(Email) && string.IsNullOrEmpty(Subject) && string.IsNullOrEmpty(Message))
                return base.ToString();

            return $@"MATMSG:TO:

{Email};

SUB:

{Subject}

BODY:

{Message}
;;";
        }

        #endregion
    }
}
