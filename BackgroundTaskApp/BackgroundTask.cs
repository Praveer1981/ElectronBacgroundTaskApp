using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Notifications;
using Windows.Data.Xml.Dom;
//using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using Windows.Storage;
using Windows.Storage.Pickers;








namespace BackgroundTaskApp
{
    [DataContract]
    public sealed class ConfigFileDetail
    {
        [DataMember]
        public string TestingUnreleasedFeatures { get; set; }                
    }

    public sealed class BackgroundTask :IBackgroundTask
    {
        public async void Run(IBackgroundTaskInstance taskInstance)
        {
            //throw new NotImplementedException();
            //String filepath = @"E:\eUWP\UnReleasedFeatureTest.txt";
            // Windows.Storage.StorageFolder installedLocation = Windows.ApplicationModel.Package.Current.InstalledLocation;
            // StorageFile file = StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///file.txt"));
            // Windows.Storage.StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;            
            // ShowToast(storageFolder.Path);
            // Windows.Storage.StorageFile filepath = await storageFolder.GetFileAsync("UnReleasedFeatureTest.txt");
            // StorageFolder storageFolder = KnownFolders.DocumentsLibrary.GetFileAsync();
            // StorageFile filepath = await KnownFolders.DocumentsLibrary.GetFileAsync("UnReleasedFeatureTest.txt");
            // StorageFile filepath = await StorageFile.GetFileFromPathAsync(filepath1);
            //var localFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            // Create a file if it does not exist
            try
            {
                // TODO:   I want to create file in C:\\ABC\\ and read from there
                Windows.Storage.StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
                string filepath = storageFolder.Path + "\\" + "UnReleasedFeatureTest.txt";
                //   Windows.Storage.StorageFolder storagefilePath = await StorageFolder.GetFolder
                // If file does not exist then create it first
                if (!File.Exists(filepath))
                {
                    // Create UnReleasedFeatureTest.txt file; replace if incase it exist
                    Windows.Storage.StorageFile featureFlagConfigFile = await storageFolder.CreateFileAsync("UnReleasedFeatureTest.txt");
                    // Write data into the file
                    string data = "{\"TestingUnReleasedFeatures\" : false}";

                    await Windows.Storage.FileIO.WriteTextAsync(featureFlagConfigFile, data);
                }
                // read from the file 
                using (StreamReader sr = new StreamReader(new FileStream(filepath, FileMode.Open)))
                {
                    var jsonData = sr.ReadToEnd();
                    var myDetails = JObject.Parse(jsonData);

                    var data =  myDetails["TestingUnReleasedFeatures"].ToString();
                    if (data.ToUpper() == "TRUE") {
                        ShowToast("Hi Praveer, I'm Electron's UWP App " + data);
                        UpdateTile("Hi Praveer see result, I'm Electron's UWP App ");
                    }
                }
            }
            catch (Exception e)
            {
                //ShowToast(e.ToString());
                Console.WriteLine(e.ToString());
            }
        }
        /// <summary>
        /// Show toast notification
        /// </summary>
        private void ShowToast(string msg)
        {
            ToastTemplateType toastTemplate = ToastTemplateType.ToastText02;
            XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(toastTemplate);

            XmlNodeList toastTextElements = toastXml.GetElementsByTagName("text");
            toastTextElements[0].AppendChild(toastXml.CreateTextNode(msg));
            toastTextElements[1].AppendChild(toastXml.CreateTextNode(DateTime.Now.ToString()));

            ToastNotification toast = new ToastNotification(toastXml);
            ToastNotificationManager.CreateToastNotifier().Show(toast);
        }

        /// <summary>
        /// Update the live tile
        /// </summary>
        private void UpdateTile(string msg)
        {
            XmlDocument tileXml = TileUpdateManager.GetTemplateContent(TileTemplateType.TileSquare150x150Text01);

            XmlNodeList textNodes = tileXml.GetElementsByTagName("text");
            textNodes[0].InnerText = msg;
            textNodes[1].InnerText = DateTime.Now.ToString("HH:mm:ss");

            TileNotification tileNotification = new TileNotification(tileXml);
            TileUpdateManager.CreateTileUpdaterForApplication().Update(tileNotification);
        }
    }
}
