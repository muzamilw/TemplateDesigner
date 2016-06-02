using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Data;

using System.IO;

namespace TemplateDesigner.Models
{
    [DataContract]
    public class UserImages
    {
        [DataMember]
        public string ImageName { get; set; }
        [DataMember]
        public string ImageRelativePath { get; set; }
        [DataMember]
        public string ImageAbsolutePath { get; set; }
        [DataMember]
        public int ImageWidth { get; set; }
        [DataMember]
        public int ImageHeight { get; set; }

        public UserImages()
        {

        }

        private string username;
        private string ipath;
        private string imgname;

        public UserImages(string uname, string imgpath)
        {
            username = uname;
            ipath = imgpath;
            imgname = "";
        }
        public DataTable getAllImages()
        {
            try
            {
                if (Directory.Exists(ipath + "/" + username) == false)
                {
                    Directory.CreateDirectory(ipath + "/" + username);
                }
                if (Directory.Exists(ipath + "/" + username + "/" + "images") == false)
                {
                    Directory.CreateDirectory(ipath + "/" + username + "/" + "images");
                }
                DataTable dt = new DataTable();
                DataColumn col = new DataColumn("imgNm");
                dt.Columns.Add(col);
                DataColumn col2 = new DataColumn("imgPath");
                dt.Columns.Add(col2);
                foreach (string fName in Directory.GetFiles(ipath + "/" + username + "/" + "images", "*.jpg"))
                {
                    FileAttributes attributes = File.GetAttributes(fName);
                    if ((attributes & FileAttributes.Hidden) != FileAttributes.Hidden)
                    {
                        DataRow row = dt.NewRow();
                        row["imgNm"] = Path.GetFileName(fName);
                        row["imgPath"] = "AppData/UserData/" + username + "/" + "images/" + Path.GetFileName(fName);
                        dt.Rows.Add(row);
                    }
                }
                foreach (string fName in Directory.GetFiles(ipath + "/" + username + "/" + "images", "*.gif"))
                {
                    FileAttributes attributes = File.GetAttributes(fName);
                    if ((attributes & FileAttributes.Hidden) != FileAttributes.Hidden)
                    {

                        DataRow row = dt.NewRow();
                        row["imgNm"] = Path.GetFileName(fName);
                        row["imgPath"] = "AppData/UserData/" + username + "/" + "images/" + Path.GetFileName(fName);
                        dt.Rows.Add(row);
                    }
                }
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("getAllImages", ex);
            }
        }
        private bool fileExist(string nm)
        {
            try
            {
                return File.Exists(ipath + "/" + username + "/" + "images/" + nm);
            }
            catch (Exception ex)
            {
                throw new Exception("fileExist", ex);
            }

        }
        public static string getFileName(string FileName)
        {
            string[] strArr;

            if (FileName.Equals(""))
                return "";
            else
            {
                strArr = FileName.Split('\\');
                return strArr[strArr.Length - 1];
            }
        }
        public bool saveImages(FileUpload imgFile)
        {
            try
            {
                bool chk = false;
                if (imgFile.PostedFile != null)
                {
                    if (imgFile.PostedFile.FileName.Length > 0 && imgFile.PostedFile.ContentLength > 0)
                    {
                        //if (imgFile.PostedFile.FileName.Length > 0
                        int i = 1;
                        string imgName = imgFile.FileName.Trim().Replace(' ', '_');
                        string[] arr = imgName.Split('.');
                        if (arr[arr.Length - 1].ToLower() == "jpg" || arr[arr.Length - 1].ToLower() == "gif")
                        {
                            while (fileExist(imgName) == true)
                            {
                                imgName = "copy_" + i.ToString() + "_" + imgFile.FileName;
                                i++;
                            }
                            imgFile.PostedFile.SaveAs(ipath + "/" + username + "/" + "images/" + imgName);
                            imgname = "AppData/UserData/" + username + "/" + "images/" + imgName;
                            chk = true;
                        }
                    }
                }
                return chk;
            }
            catch (Exception ex)
            {
                throw new Exception("saveImages", ex);
            }
        }


        


        public string getImgName
        {
            get { return this.imgname; }
        }
    }
}
