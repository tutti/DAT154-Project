using System;
using System.IO;
using Windows.Storage;
using Windows.Data.Json;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System.Net.Http;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Collections.Generic;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace DAT154_Universal.Views {
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Login : Page {
        public Login() {
            this.InitializeComponent();
        }

        private void SignInButton_Click(object sender, RoutedEventArgs e) {
            SignInButton.IsEnabled = false;
            CheckServer(UsernameTextBox.Text, PasswordTextBox.Password);
        }

        private async void CheckServer(string username, string password) {
            HttpClient httpClient = new HttpClient();
            try {
                var variables = new Dictionary<string, string> {
                    {"email",username },
                    {"password",password }
                };  
                var content = new FormUrlEncodedContent(variables);
                var result = await httpClient.PostAsync("http://localhost:1893/api/login", content);

                try {
                    String data = await result.Content.ReadAsStringAsync();
                    if (data.Contains("user")){ 
                        string newdata = data.Substring(data.IndexOf("type\":"));
                        string seconddata = newdata.Substring(newdata.IndexOf(':')+1);
                        string lastdata = seconddata.Substring(0,seconddata.IndexOf(','));
                        writeToFile(Convert.ToInt32(lastdata));
                    } else {
                        ErrorMessage.Text = "User not found";
                    }
                    
                } catch {
                    ErrorMessage.Text = "User not found";
                }
                
            } catch {
                ErrorMessage.Text = "Could not connect to server";
            }
            SignInButton.IsEnabled = true;
        }

        async void writeToFile(int type) {
            if(type != 0) {
                StorageFolder tempFolder = ApplicationData.Current.TemporaryFolder;
                StorageFile userType;
                try {
                    userType = await tempFolder.CreateFileAsync("user.txt");
                } catch {
                    userType = await tempFolder.GetFileAsync("user.txt");
                }
                await FileIO.WriteTextAsync(userType, type.ToString());
                Frame.Navigate(typeof(JobListing));
            }else {
                ErrorMessage.Text = "User is not a worker";
            }
            SignInButton.IsEnabled = true;
        }
        
    }


    
}
