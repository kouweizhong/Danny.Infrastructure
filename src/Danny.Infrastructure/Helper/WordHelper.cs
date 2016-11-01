using System;
using Microsoft.Office.Interop.Word;

namespace Danny.Infrastructure.Helper.Office
{
    public class WordHelper
    {
        public static void WordToHtml(string sourceFileName, string targetFileName)
        {
            Microsoft.Office.Interop.Word.ApplicationClass WordApp;
            Microsoft.Office.Interop.Word.Document WordDoc;
            Object oMissing = System.Reflection.Missing.Value;
            WordApp = new Microsoft.Office.Interop.Word.ApplicationClass();
            object fileName = sourceFileName;

            WordDoc = WordApp.Documents.Open(ref fileName,
               ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
               ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
               ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing);
            try
            {
                
                Type wordType = WordApp.GetType();
                // 打开文件
                Type docsType = WordApp.Documents.GetType();
                // 转换格式，另存为
                Type docType = WordDoc.GetType();
                object saveFileName = targetFileName;
                docType.InvokeMember("SaveAs", System.Reflection.BindingFlags.InvokeMethod, null, WordDoc, new object[] { saveFileName, Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatHTML });
            }
            finally 
            {
                WordDoc.Close(ref oMissing, ref oMissing, ref oMissing);
                WordApp.Quit(ref oMissing, ref oMissing, ref oMissing);
            }
          
        }


        public static void HtmlToWord(string sourceFileName, string targetFileName )
        {
            object missing = System.Reflection.Missing.Value;
            object readOnly = false;
            object isVisible = false;
            object file1 = sourceFileName;
            object html1 = targetFileName;
            ApplicationClass oWordApp = null;
            Document oWordDoc = null;
            try
            {
                object format = WdSaveFormat.wdFormatDocument;
                oWordApp = new ApplicationClass();
                oWordApp.Visible = false;
                oWordDoc = oWordApp.Documents.Open(ref   file1, ref   isVisible, ref   readOnly, ref   missing, ref   missing, ref   missing, ref   missing, ref   missing, ref   missing, ref   format, ref   missing, ref   isVisible, ref   missing, ref   missing, ref   missing, ref missing);
                oWordDoc.SaveAs(ref   html1, ref   format, ref   missing, ref   missing, ref   missing, ref   missing, ref   missing, ref   missing, ref   missing, ref   missing, ref   missing, ref   missing, ref   missing, ref   missing, ref   missing, ref   missing);
            }
            finally
            {
                if (oWordDoc != null)
                {
                    oWordDoc.Close(ref     missing, ref     missing, ref     missing);
                }
                if (oWordApp != null)
                {
                    oWordApp.Application.Quit(ref   missing, ref   missing, ref   missing);
                }
            }
        }
    }
}
