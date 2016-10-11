using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Linq;

namespace OfxApplication.Library
{
    public class Lib
    {
        public DirectoryInfo ReadDirectory(String dirPath)
        {
            DirectoryInfo di = new DirectoryInfo(dirPath);
            return di;
        }

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
            int id = 1;
            string type = "";
            string dtPosted = "";
            string trnamt = "";
            string fitid = "";
            string memo = "";
            foreach (string l in data)
            {
                if (l.IndexOf("<STMTTRN>") > -1)
                {
                    root = new XElement("STMTTRN");
                    stmttrn = new STMTTRN();
                    id++;
                    continue;
                }
                if (root != null && !l.StartsWith("/"))
                {
                    var tagName = getTagName(l);
                    if (tagName == "TRNTYPE") type = getTagValue(l);
                    if (tagName == "DTPOSTED") dtPosted = getTagValue(l);
                    if (tagName == "TRNAMT") trnamt = getTagValue(l);
                    if (tagName == "FITID") fitid = getTagValue(l);
                    if (tagName == "MEMO") memo = getTagValue(l);
                    stmttrn.id = id;
                    stmttrn.type = type;
                    stmttrn.dtPosted = dtPosted;
                    stmttrn.trnamt = trnamt;
                    stmttrn.fitid = fitid;
                    stmttrn.memo = memo;
                    result.Add(stmttrn);
                }
                if (l.IndexOf("</STMTTRN>") > -1)
                {
                    root = null;
                    continue;
                }
            }
            return result;
        }

        private static string getTagName(string line)
        {
            int pos_init = line.IndexOf("<") + 1;
            int pos_end = line.IndexOf(">");
            pos_end = pos_end - pos_init;
            return line.Substring(pos_init, pos_end);
        }

        private static string getTagValue(string line)
        {
            int pos_init = line.IndexOf(">") + 1;
            string retValue = line.Substring(pos_init).Trim();
            if (retValue.IndexOf("[") != -1)
            {
                retValue = retValue.Substring(0, 8);
            }
            return retValue;
        }

        public String printData(List<STMTTRN> result)
        {
            String output = "";
            foreach (STMTTRN item in result)
            {
                output += item.id + item.type + item.dtPosted + item.trnamt + "<br/>";
            }
            return output;
        }
    }
}
