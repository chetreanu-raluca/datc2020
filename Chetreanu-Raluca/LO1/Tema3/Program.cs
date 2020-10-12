using System;
using Google.Apis.Drive.v3;
using Google.Apis.Auth.OAuth2;
using System.Threading;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;

namespace Tema3
{
    class Program
    {

        private static DriveService _service;
        private static string _token;
        static void Main(string[] args)
        {
            init();
            GetMyFiles();
        }

        static void init() {
            string[] scopes = new string[] {
                DriveService.Scope.Drive,
                DriveService.Scope.DriveFile
            };

            var cliendId = "253168991475-tgsi5655dh95kmrostj73f41d7cuhl7g.apps.googleusercontent.com";
            var clientSecret = "V3CBWUCzv8RCnEqaMGtAGqK2";

            var credential= GoogleWebAuthorizationBroker.AuthorizeAsync(

                new ClientSecrets
                {
                    ClientId=cliendId,
                    ClientSecret=clientSecret
                },

                scopes,
                Environment.UserName,
                CancellationToken.None,

                null
            ).Result;

            _service = new DriveService(new Google.Apis.Services.BaseClientService.Initializer()
            {
                HttpClientInitializer = credential
            });

            _token = credential.Token.AccessToken;

            Console.WriteLine("Token:  " +_token);
        }

        static void GetMyFiles()
        {
            var request = (HttpWebRequest)WebRequest.Create("htttps://googleapis.com/drive/v3/files?q='root'%20in%20parents");
            request.Headers.Add(HttpRequestHeader.Authorization,"Bearer" + _token);

            using (var response = request.GetResponse())
            {
                using (Stream data = response.GetResponseStream())
                using (var reader = new StreamReader(data))
                {
                    string text = reader.ReadToEnd();
                    var myData = JObject.Parse(text);
                    foreach(var file in myData["files"])
                    {
                        if (file["mimeType"].ToString() !="application/vnd.google-apps.folder")
                        {
                            Console.WriteLine("File name: "+ file["name"]);
                        }
                    }
                }
            }
        }
    }
}
