using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MessagingToolkit.QRCode.Codec;
using MessagingToolkit.QRCode;
using MessagingToolkit.QRCode.Codec.Data;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;

namespace ShipOnline.UtilityService
{
    public class QRCodeServices 
    {
        /// <summary>
        /// Creat QR code to image
        /// </summary>
        /// <param name="qrcode"></param>
        /// <returns></returns>
        public static string creatQR(string qrcode)
        {
            string result = null;
            QRCodeEncoder enc = new QRCodeEncoder();
            string savePos = "QRcode";
            string posText = HttpContext.Current.Server.MapPath(savePos);

            try
            {
                if (!string.IsNullOrEmpty(qrcode))
                {
                    string filePath = "~/" + savePos;
                    string fileName = qrcode + ".png";
                    enc.QRCodeScale = 5;
                    enc.QRCodeVersion = 1;
                    if (!Directory.Exists(HttpContext.Current.Server.MapPath(filePath)))
                    {
                        Directory.CreateDirectory(HttpContext.Current.Server.MapPath(filePath));
                    }
                    Bitmap img = enc.Encode(qrcode, System.Text.Encoding.UTF8);
                    string path = Path.Combine(HttpContext.Current.Server.MapPath(filePath), fileName);

                    if (System.IO.File.Exists(path))
                    {
                        System.IO.File.Delete(path);
                    }
                    img.Save(path, ImageFormat.Png);

                    result = filePath +  "/" + fileName;
                }
            }
            catch (Exception)
            {
                result = null;
            }
            return result;
        }

    }
}