using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using WIA;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace DataInvoice.AppDocumentAcquisition.ACQUIRE
{
    public class AcquireManager
    {


        private const int WIA_PROPERTIES_WIA_RESERVED_FOR_NEW_PROPS = 1024;
        private const int WIA_PROPERTIES_WIA_DIP_FIRST = 2;

        private const int WIA_PROPERTIES_WIA_DPA_FIRST = WIA_PROPERTIES_WIA_DIP_FIRST + WIA_PROPERTIES_WIA_RESERVED_FOR_NEW_PROPS;
        private const int WIA_PROPERTIES_WIA_DPC_FIRST = WIA_PROPERTIES_WIA_DPA_FIRST + WIA_PROPERTIES_WIA_RESERVED_FOR_NEW_PROPS;
        private const int WIA_PROPERTIES_WIA_DPS_FIRST = WIA_PROPERTIES_WIA_DPC_FIRST + WIA_PROPERTIES_WIA_RESERVED_FOR_NEW_PROPS;

        private const int WIA_PROPERTIES_WIA_DPS_DOCUMENT_HANDLING_STATUS = WIA_PROPERTIES_WIA_DPS_FIRST + 13;
        private const int WIA_PROPERTIES_WIA_DPS_DOCUMENT_HANDLING_SELECT = WIA_PROPERTIES_WIA_DPS_FIRST + 14;
        private const int WIA_IPS_CUR_INTENT = 6146;
        private const int WIA_IPS_XRES = 6147;
        private const int WIA_IPS_YRES = 6148;
        private const int FEED_READY = 0x01;



        //WIA.CommonDialog wiaDialog = null;
        WIA.CommonDialog wiaDialog = null;
        WIA.Device defaultDevice = null;
        WIA.DeviceManager deviceManager = null;


        NGLib.COMPONENTS.FILE.DIRECTORY.WorkDirectory tempDirectory = null;


        public AcquireManager()
        {
            this.wiaDialog = new WIA.CommonDialog();
            deviceManager = new DeviceManager();
   
        }


        public void PrepareScan()
        {


            if (tempDirectory == null)
            {
                this.tempDirectory = new NGLib.COMPONENTS.FILE.DIRECTORY.WorkDirectory();
                this.tempDirectory.Open();
            }
                

            //if (defaultDevice==null)
            //    this.defaultDevice = this.wiaDialog.ShowSelectDevice(WIA.WiaDeviceType.ScannerDeviceType, true, false);
            //Console.WriteLine("DEVICE " + defaultDevice.DeviceID);
        
        }




       public string Scan(System.IO.FileInfo fichierScanOut=null)
        {
            List<Bitmap> imagespages = new List<Bitmap>();

            //foreach (Item item in this.defaultDevice.Items)
            //{

            ImageFormat imageFormat = ImageFormat.Tiff;
            ImageFile imagef = null;
            //imagef = deviceItem.Transfer();
            //WIA.Properties  prop= new WIA.Properties();


            imagef = this.wiaDialog.ShowAcquireImage(WiaDeviceType.ScannerDeviceType, WiaImageIntent.ColorIntent, WiaImageBias.MaximizeQuality, "{B96B3CB1-0728-11D3-9D7B-0000F81EF32E}", false, true, false);
            Bitmap img = WorkRenderImage(imagef);

                imagespages.Add(img);
            //}

            Console.WriteLine("PAGES " + imagespages.Count);

            if (fichierScanOut==null)
                fichierScanOut = new FileInfo(this.tempDirectory.WorkPath + DateTime.Now.ToString("yyyyMMddHHmmss") + NGLib.DATA.FORMAT.StringUtilities.GenerateString(6) + ".pdf");

            int i = 0;
            foreach (Bitmap imagespage in imagespages)
            {
                i++;

                 byte[] imgb = ImageToByte(imagespage);
                 ConvertToPDF(imgb, fichierScanOut.FullName);
                 break;
                //imagespage.Save(fichierScanOut.FullName + "." + i.ToString());
            }



           
            
            //image.Save(fichierScanOut.FullName+".");

            //imagef = (ImageFile)this.wiaDialog.ShowTransfer(defaultDevice.Items[1]);

            //imagef = wiaDialog.ShowAcquireImage(
            //    DeviceType: WiaDeviceType.ScannerDeviceType,
            //    Intent: WiaImageIntent.ColorIntent,
            //    Bias: WiaImageBias.MinimizeSize,
            //    FormatID: imageFormat.Guid.ToString("B"),
            //    AlwaysSelectDevice: false,
            //    UseCommonUI: true,
            //    CancelError: false);



            //image.SaveFile(fichier.FullName);
            //Bitmap image = WorkRenderImage(imagef);
            //image.Save(fichierScanOut.FullName);

            return fichierScanOut.FullName;
       }




       //public Bitmap ScanOne()
       //{

       //    ImageFormat imageFormat = ImageFormat.Tiff;
       //    ImageFile imagef = null;
       //    //imagef = deviceItem.Transfer();
       //    //WIA.Properties  prop= new WIA.Properties();
  

       //    imagef = this.wiaDialog.ShowAcquireImage(WiaDeviceType.ScannerDeviceType, WiaImageIntent.ColorIntent, WiaImageBias.MaximizeQuality, "{B96B3CB1-0728-11D3-9D7B-0000F81EF32E}", false, true, false);
       //    Bitmap image = WorkRenderImage(imagef);
       // //image.Save(fichierScanOut.FullName+".");
       //    return image;
       //}



       private Bitmap WorkRenderImage(ImageFile imagef, bool minimize= true)
       {
           Vector vector = imagef.FileData;
           Bitmap img = null;
           if (vector != null)
           {
               byte[] bytes = vector.get_BinaryData() as byte[];
               if (bytes != null)
               {
                   using (var ms = new MemoryStream(bytes))
                   {
                       if (minimize)   img = Wo(ms);
                       else img = new Bitmap(ms);
                   }
               }
           }
           return img;

       }
       public static byte[] ImageToByte(System.Drawing.Image img)
       {
           ImageConverter converter = new ImageConverter();
           return (byte[])converter.ConvertTo(img, typeof(byte[]));
       }


        public void ConvertToPDF(byte[] imgb, string outputfile)
       {
           iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(imgb);
           using (FileStream fs = new FileStream(outputfile, FileMode.Create, FileAccess.Write, FileShare.None))
           {
               using (Document doc = new Document(image))
               {
                   using (PdfWriter writer = PdfWriter.GetInstance(doc, fs))
                   {
                       doc.Open();
                       image.SetAbsolutePosition(0, 0);
                       writer.DirectContent.AddImage(image);
                       doc.Close();
                   }
               }
           }
       }




       public Bitmap Wo(Stream bitmapstream)
       {
           Bitmap bitmaptemp = new Bitmap(bitmapstream);
           return Wo(bitmaptemp);
       }
       public Bitmap Wo(Bitmap bitmap)
       {
           Bitmap retour = null;

           ImageCodecInfo codecInfo = ImageCodecInfo.GetImageDecoders().First(codec => codec.FormatID == ImageFormat.Jpeg.Guid);
           System.Drawing.Imaging.Encoder encoder = System.Drawing.Imaging.Encoder.Quality;
           EncoderParameters parameters = new EncoderParameters(1);
           EncoderParameter qualityParameter = new EncoderParameter(encoder, 50L); // qualité 80% 
           parameters.Param[0] = qualityParameter;

           Stream ms = new MemoryStream();
           bitmap.Save(ms, codecInfo, parameters);
           retour = new Bitmap(ms);
           return retour;
       }






       //public List<Image> Scan(string scannerId)
       //{
       //    List<Image> images = new List<Image>();
       //    List<String> tmp_imageList = new List<String>();

       //    bool hasMorePages = true;
       //    bool useAdf = true;
       //    bool duplex = false;

       //    int pages = 0;

       //    string fileName = null;
       //    string fileName_duplex = null;

       //    WIA.DeviceManager manager = null;
       //    WIA.Device device = null;
       //    WIA.DeviceInfo device_infoHolder = null;
       //    WIA.Item item = null;
       //    WIA.ICommonDialog wiaCommonDialog = null;

       //    manager = new WIA.DeviceManager();

       //    // select the correct scanner using the provided scannerId parameter
       //    foreach (WIA.DeviceInfo info in manager.DeviceInfos)
       //    {
       //        if (info.DeviceID == scannerId)
       //        {
       //            // Find scanner to connect to
       //            device_infoHolder = info;
       //            break;
       //        }
       //    }

       //    while (hasMorePages)
       //    {
       //        wiaCommonDialog = new WIA.CommonDialog();

       //        // Connect to scanner
       //        device = device_infoHolder.Connect();

       //        if (device.Items[1] != null)
       //        {
       //            item = device.Items[1] as WIA.Item;

       //            try
       //            {
       //                if ((useAdf) || (duplex))
       //                    SetupADF(device, duplex); //Sets the right properties in WIA

       //                WIA.ImageFile image = null;
       //                WIA.ImageFile image_duplex = null;

       //                // scan image                
       //                image = (WIA.ImageFile)wiaCommonDialog.ShowTransfer(item, wiaFormatTIFF, false);

       //                if (duplex)
       //                {
       //                    image_duplex = (ImageFile)wiaCommonDialog.ShowTransfer(item, wiaFormatPNG, false);
       //                }

       //                // save (front) image to temp file
       //                fileName = Path.GetTempFileName();
       //                tmp_imageList.Add(fileName);
       //                File.Delete(fileName);
       //                image.SaveFile(fileName);
       //                image = null;

       //                // add file to images list
       //                images.Add(Image.FromFile(fileName));

       //                if (duplex)
       //                {
       //                    fileName_duplex = Path.GetTempFileName();
       //                    tmp_imageList.Add(fileName_duplex);
       //                    File.Delete(fileName_duplex);
       //                    image_duplex.SaveFile(fileName_duplex);
       //                    image_duplex = null;

       //                    // add file_duplex to images list
       //                    images.Add(Image.FromFile(fileName_duplex));
       //                }

       //                if (useAdf || duplex)
       //                {
       //                    hasMorePages = HasMorePages(device); //Returns true if the feeder has more pages
       //                    pages++;
       //                }
       //            }
       //            catch (Exception exc)
       //            {
       //                throw exc;
       //            }
       //            finally
       //            {
       //                wiaCommonDialog = null;
       //                manager = null;
       //                item = null;
       //                device = null;
       //            }
       //        }
       //    }
       //    device = null;
       //    return images;
       //}






    }
}
