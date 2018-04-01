using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using PrintPDV.Controllers.Base;
using PrintPDV.Models;
using PrintPDV.Utility;
using PrintPDV.Utility.Models;

namespace PrintPDV.Controllers
{
    public class AppConfigController : GenericController<AppConfig>, IAppConfigController
    {
        public static IAppConfigController Instance
        {
            get { return SingletonUtility<AppConfigController>.Instance; }
        }

        public void SetAppConfig()
        {
            var dic = new Dictionary<string,string>();

            GetList().ToList().ForEach(item => dic.Add(item.Key, item.Value));

            new AppConfigUtility(dic);
        }

        public static bool IsDatabaseCreated
        {
            get
            {
                var dbFile = AppConfigUtility.AppDirectory + AppConfigUtility.DatabaseName;

                return File.Exists(dbFile);
            }
        }

        public static bool CreateDatabase()
        {
            try
            {
                var dbPath = AppConfigUtility.AppDirectory + AppConfigUtility.DatabaseName;
                var password = AppConfigUtility.DatabasePassword;
                var connString = AppConfigUtility.DatabaseConnString;
                var conn = new SQLiteConnection(connString);
                var sqlDir = AppConfigUtility.AppDirectory + @"Resources\database\Sql";
                var sqlFiles = Directory.GetFiles(sqlDir, "*.sql");

                if (File.Exists(dbPath))
                    File.Delete(dbPath);

                SQLiteConnection.CreateFile(dbPath);

                conn.SetPassword(password);

                conn.Open();

                foreach (var file in sqlFiles)
                {
                    var fileContent = File.ReadAllText(file);
                    var command = new SQLiteCommand(fileContent, conn);
                    command.ExecuteNonQuery();
                }

                conn.Close();

                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Log(LogUtility.LogType.SystemError, MethodBase.GetCurrentMethod().Name, ex.Message);
                throw;
            }
        }

        public static bool InsertAppConfig(string defaultLanguage)
        {
            try
            {
                Instance.Insert(new AppConfig { Key = "defaultLanguage", Value = defaultLanguage });
                Instance.Insert(new AppConfig { Key = "printDocumentName", Value = "PrintPDV" });
                Instance.Insert(new AppConfig { Key = "websiteUrl", Value = "printpdv.com.br" });
                Instance.Insert(new AppConfig { Key = "syncStatisticUrl", Value = "http://www.printpdv.com/rest/statistic/sync" });
                Instance.Insert(new AppConfig { Key = "validateLicenseUrl", Value = "http://www.printpdv.com/rest/license/validate" });

                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Log(LogUtility.LogType.SystemError, MethodBase.GetCurrentMethod().Name, ex.Message);
                throw;
            }
        }

        public static bool InserPrintersModels()
        {
            try
            {
                var printerList = new List<PrinterModel>
                {
                    new PrinterModel {Manufacturer = "Bematech", Name = "MP-4200 TH", Model = "7"},
                    new PrinterModel {Manufacturer = "Bematech", Name = "MP-4000 TH", Model = "5"},
                    new PrinterModel {Manufacturer = "Bematech", Name = "MP-2500 TH", Model = "8"},
                    new PrinterModel {Manufacturer = "Bematech", Name = "MP-2100 TH", Model = "0"},
                    new PrinterModel {Manufacturer = "Bematech", Name = "MP-2000 TH", Model = "0"},
                    new PrinterModel {Manufacturer = "Bematech", Name = "MP-2000 CI", Model = "0"},
                    new PrinterModel {Manufacturer = "Bematech", Name = "MP-20 TH", Model = "0"},
                    new PrinterModel {Manufacturer = "Bematech", Name = "MP-20 MI", Model = "1"},
                    new PrinterModel {Manufacturer = "Bematech", Name = "MP-20 CI", Model = "1"},
                    new PrinterModel {Manufacturer = "Epson", Name = "TM-T81+", Model = "TM-T81+"},
                    new PrinterModel {Manufacturer = "Epson", Name = "TM-T88 IV", Model = "TM-T88 IV"},
                    new PrinterModel {Manufacturer = "Epson", Name = "TM-T88 V", Model = "TM-T88 V"},
                    new PrinterModel {Manufacturer = "Epson", Name = "TM-T20", Model = "TM-T20"},
                    new PrinterModel {Manufacturer = "Epson", Name = "TM-T20 II", Model = "TM-T20 II"},
                    new PrinterModel {Manufacturer = "Epson", Name = "TM-T70", Model = "TM-T70"},
                    new PrinterModel {Manufacturer = "Epson", Name = "TM-T70II", Model = "TM-T70II"},
                    new PrinterModel {Manufacturer = "Epson", Name = "TM-P60II", Model = "TM-P60II"},
                    new PrinterModel {Manufacturer = "Epson", Name = "TM-P80", Model = "TM-P80"},
                    new PrinterModel {Manufacturer = "Epson", Name = "TM-P20", Model = "TM-P20"},
                    new PrinterModel {Manufacturer = "Epson", Name = "TM-H6000 III", Model = "TM-H6000 III"},
                    new PrinterModel {Manufacturer = "Epson", Name = "TM-H6000 IV", Model = "TM-H6000 IV"}
                };

                printerList.ForEach(item => PrinterModelController.Instance.Insert(item));

                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Log(LogUtility.LogType.SystemError, MethodBase.GetCurrentMethod().Name, ex.Message);
                throw;
            }
        }

        public static bool InsertCliparts()
        {
            try
            {
                var imageDir = AppConfigUtility.AppDirectory + @"Resources\database\Cliparts";

                var imageFiles = Directory.GetFiles(imageDir, "*.*", SearchOption.AllDirectories)
                    .Where(s => s.EndsWith(".png") || s.EndsWith(".bmp"));

                foreach (var file in imageFiles)
                {
                    var directoryName = Path.GetDirectoryName(file);

                    if (string.IsNullOrEmpty(directoryName)) continue;

                    try
                    {
                        var directory = new DirectoryInfo(directoryName).Name;

                        var galleryType = (Enumerations.GalleryType)Enum.Parse(typeof(Enumerations.GalleryType), directory);

                        var image = ImageUtility.ByteToImage(File.ReadAllBytes(file));
                        image = ImageUtility.ConvertToBlackWhite(image);

                        if (image.Width > 216)
                            image = ImageUtility.ResizeImageFixedWidth(image, 216);

                        var imageName = Path.GetFileNameWithoutExtension(file);
                        var imageBytes = ImageUtility.ImageToByte(image, ImageFormat.Bmp);
                        var clipartEntity = new GalleryClipart
                        {
                            Name = imageName,
                            GalleryType = galleryType,
                            Image = imageBytes
                        };

                        GalleryClipartController.Instance.Insert(clipartEntity);
                    }
                    catch (Exception)
                    {
                        // ignored
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Log(LogUtility.LogType.SystemError, MethodBase.GetCurrentMethod().Name, ex.Message);
                throw;
            }
        }
    }
}
