using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Linq;

namespace OfxApplication.Library
{
    public class Lib
    {
        public String[] ReadFile(string filePath)
        {
            if (!System.IO.File.Exists(filePath))
            {
                throw new FileNotFoundException();
            }
            String[] data = System.IO.File.ReadAllLines(filePath);
            return data;
        }

        public List<STMTTRN> LoadData(String[] data)
        {
            List<STMTTRN> result = new List<STMTTRN>();
            STMTTRN stmttrn = null;

            XElement root = null;
            Guid guid;
            string type = "";
            string dtPosted = "";
            string trnamt = "";
            string fitid = "";
            string memo = "";
            foreach (string line in data)
            {
                //verify if the data of an transaction started
                if (line.IndexOf("<STMTTRN>") > -1)
                {
                    root = new XElement("STMTTRN");
                    stmttrn = new STMTTRN();
                    continue;
                }
                //verify if the data not finished yet and not reached the end tag '/STMTTRN'
                if (root != null && !line.StartsWith("/"))
                {
                    var tagName = getTagName(line);
                    if (tagName == "TRNTYPE") type = getTagValue(line);
                    if (tagName == "DTPOSTED") dtPosted = getTagValue(line);
                    if (tagName == "TRNAMT") trnamt = getTagValue(line);
                    if (tagName == "FITID") fitid = getTagValue(line);
                    if (tagName == "MEMO") memo = getTagValue(line);

                    stmttrn.type = type;
                    stmttrn.dtPosted = dtPosted;
                    stmttrn.trnamt = trnamt;
                    stmttrn.fitid = fitid;
                    stmttrn.memo = memo;
                }
                //verify if the data of an transaction ended
                if (line.IndexOf("</STMTTRN>") > -1)
                {
                    //verify if the hash is already include on the list (
                    string hash = stmttrn.GetHashCode();
                    if (result.Find(x => x.GetHashCode() == hash) == null)
                    {
                        guid = Guid.NewGuid();
                        stmttrn.id = guid;
                        result.Add(stmttrn);
                    }
                    root = null;
                }
            }
            return result;
        }

        //Get the name of the tag
        private static string getTagName(string line)
        {
            int tagOpen = line.IndexOf("<") + 1;
            int tagClose = line.IndexOf(">");
            tagClose = tagClose - tagOpen;
            return line.Substring(tagOpen, tagClose);
        }

        //Get the value from the end of the tag
        private static string getTagValue(string line)
        {
            int tagClose = line.IndexOf(">") + 1;
            string retValue = line.Substring(tagClose).Trim();
            if (retValue.IndexOf("[") != -1)
            {
                retValue = retValue.Substring(0, 9);
            }
            return retValue;
        }
    }
}
