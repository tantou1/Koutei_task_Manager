using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

namespace Koutei_task_Manager.UserControl
{
    public partial class UC02Label : System.Web.UI.UserControl
    {
        public event EventHandler EndKoutei;
        public void SetFusenJouhou(DataRow drFusen,bool fgazou)
        {
            //工程情報を表示の為
            lblKouteiId.Text = drFusen["cBUNRUI"].ToString();　　//工程コード
            lblKouteiName.Text = drFusen["sBUNRUI"].ToString();　　//工程名           
            lbcSHIJISYO.Text = drFusen["cSHIJISYO"].ToString();　　//指示書コード
            string sshijisyo= drFusen["sSHIJISYO"].ToString();
            lblsSHIJISYO.Text = sshijisyo.Trim().Replace("\r\n", "CRLFu000Du000A").Replace("\r", "").Replace("\n", "").Replace("CRLFu000Du000A", "\r\n");　　//指示書名
            lblSHIJISYO_Hyouji.Text = "#" + drFusen["cSHIJISYO"].ToString();　　//指示書コード表示
            lblLabelOrder.Text = drFusen["nJUMBAN"].ToString();　　//指示書分類順番
            string stokuisaki = drFusen["sTOKUISAKI"].ToString();
            lblsTokuisaki.Text =stokuisaki.Trim().Replace("\r\n", "CRLFu000Du000A").Replace("\r", "").Replace("\n", "").Replace("CRLFu000Du000A", "\r\n");　　//得意先名
            lbldkanryouyotei.Text = drFusen["dKANRYOUYOTEI"].ToString();　　//指示書完了予定日
            lbleigyou.Text = drFusen["sTANTOUSHA"].ToString(); //営業担当者
            lblmail.Text = drFusen["SMAIL"].ToString(); //営業担当メール

            #region 画像表示     
            if (fgazou)
            {
                string file_path = "";
                //file_path = @"C:\Users\cnsa018\Documents\photo_sign\2022\05\09\0000001\0000000526\CMPhotoLib\cmyk.jpg";
                file_path = drFusen["img"].ToString();

                if (Session["f_old_ver"].ToString() == "true" && file_path!="")
                {
                    
                    int i = file_path.IndexOf("\\指示書");
                    file_path = file_path.Substring(0, i) + "ShijisyoPrint";
                    string fileName= drFusen["img_filename"].ToString();
                    string[] strs = System.IO.Directory.GetFiles(file_path);
                    if (strs.Length > 0)
                    {
                        foreach (string file in strs)
                        {
                            string name = System.IO.Path.GetFileName(file);
                            //20141011 zww add
                            if (fileName.ToLower().EndsWith(".bmp") || fileName.ToLower().EndsWith(".png") || fileName.ToLower().EndsWith(".gif") || fileName.ToLower().EndsWith(".jpeg"))
                            {
                                int num = fileName.IndexOf(".");
                                string fileStyle = fileName.Replace(fileName.Substring(0, num), "");

                                if (name.ToLower().StartsWith(lbcSHIJISYO.Text) && name.ToLower().EndsWith(fileStyle.ToLower()))
                                {
                                    file_path = file;
                                    break;
                                }
                            }
                            else
                            {
                                if (name.StartsWith(lbcSHIJISYO.Text) && name.ToLower().EndsWith(".jpg"))
                                {
                                    file_path = file;
                                    break;
                                }
                            }
                            //.........end........                                        
                        }

                    }
                }
                if (File.Exists(file_path))
                {
                    string ext = Path.GetExtension(file_path);
                    if (ext.ToLower().Contains("gif") || ext.ToLower().Contains("jpg")
                        || ext.ToLower().Contains("jpeg") || ext.ToLower().Contains("png") 
                        || ext.ToLower().Contains("jfif"))
                    {
                        if (File.Exists(file_path))
                        {
                            System.Drawing.Image originalImage;
                            FileInfo fi = new FileInfo(file_path);
                            FileStream fs = new FileStream(file_path, FileMode.Open, FileAccess.Read);
                            originalImage = new Bitmap(fs);
                            byte[] imageBytes = System.IO.File.ReadAllBytes(file_path);
                            if (fi.Length > 23552)//about 23KB
                            {
                                imageBytes = ResizeImageFile(imageBytes, 760);
                            }
                            EncoderParameters myEncoderParameters = new EncoderParameters(1);
                            EncoderParameter myEncoderParameter = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 75L);
                            myEncoderParameters.Param[0] = myEncoderParameter;
                            string base64string = Convert.ToBase64String(imageBytes, 0, imageBytes.Length);

                            string imgurl = "data:image/png;base64," + base64string;

                            Image.ImageUrl = imgurl;

                            divFusenJouhou.Attributes.Add("style", "height:167px");
                        }
                    }
                    else
                    {
                        Image.Visible = false;
                    }                    

                }
                else
                {
                    Image.Visible = false;
                }
            }
            else
            {
                Image.Visible = false;
            }
            //if (drFusen["sPATH_SERVER_SOURCE"].ToString() != "")
            //{
            //    string file_path = drFusen["sPATH_SERVER_SOURCE"].ToString();
            //    if (File.Exists(file_path))
            //    {
            //        FileStream fs = new FileStream(file_path, FileMode.Open, FileAccess.Read);
            //        BinaryReader br = new BinaryReader(fs);
            //        Byte[] bt = br.ReadBytes((Int32)fs.Length);
            //        br.Close();
            //        fs.Close();
            //        string base64string = Convert.ToBase64String(bt, 0, bt.Length);

            //        string imgurl = "data:image/png;base64," + base64string;

            //        Image.ImageUrl = imgurl;

            //        divFusenJouhou.Attributes.Add("style", "height:186px");

            //    }
            //    else
            //    {
            //        Image.Visible = false;
            //    }
            //}
            //else
            //{
            //    Image.Visible = false;
            //}
            
            #endregion
            if(!Image.Visible)
            {
                div_img.Style["display"] = "none";

            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            EndKoutei(sender, e);
        }


        #region ResizeImageFile()
        public static byte[] ResizeImageFile(byte[] imageFile, int targetSize) // Set targetSize to 760
        {
            using (System.Drawing.Image oldImage = System.Drawing.Image.FromStream(new MemoryStream(imageFile)))
            {
                Size newSize = CalculateDimensions(oldImage.Size, targetSize);
                using (Bitmap newImage = new Bitmap(newSize.Width, newSize.Height, PixelFormat.Format24bppRgb))
                {
                    using (Graphics canvas = Graphics.FromImage(newImage))
                    {
                        canvas.SmoothingMode = SmoothingMode.AntiAlias;
                        canvas.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        canvas.PixelOffsetMode = PixelOffsetMode.HighQuality;
                        canvas.DrawImage(oldImage, new Rectangle(new Point(0, 0), newSize));
                        MemoryStream m = new MemoryStream();
                        newImage.Save(m, ImageFormat.Jpeg);
                        return m.GetBuffer();
                    }
                }
            }
        }
        #endregion

        #region CalculateDimensions()
        public static Size CalculateDimensions(Size oldSize, int targetSize)
        {
            Size newSize = new Size();
            if (oldSize.Height > oldSize.Width)
            {
                newSize.Width = (int)(oldSize.Width * ((float)targetSize / (float)oldSize.Height));
                newSize.Height = targetSize;
            }
            else
            {
                newSize.Width = targetSize;
                newSize.Height = (int)(oldSize.Height * ((float)targetSize / (float)oldSize.Width));
            }
            return newSize;
        }
        #endregion

    }
}