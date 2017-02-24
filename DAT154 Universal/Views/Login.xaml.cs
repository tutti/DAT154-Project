using System;
using System.IO;
using Windows.Storage;
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
            CheckServer(UsernameTextBox.Text, PasswordTextBox.Password);
        }

        private async void CheckServer(string username, string password) {


            HttpClient httpClient = new HttpClient();
            try {
                DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(User));
                var variables = new Dictionary<string, string> {
                    {"email",username },
                    {"password",password }
                };  
                var content = new FormUrlEncodedContent(variables);
                var result = await httpClient.PostAsync("http://localhost:1893/api/login", content);
                MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(result.Content.ToString()));

                try {
                    User response = (User)js.ReadObject(ms);

                    writeToFile(response.type);
                } catch {
                    ErrorMessage.Text = "User not found";
                }
                
            } catch {
                ErrorMessage.Text = "Could not get connection";
            }
        }

        async void writeToFile(int type) {
            StorageFolder tempFolder = ApplicationData.Current.TemporaryFolder;
            StorageFile userType;
            try {
                userType = await tempFolder.CreateFileAsync("user.txt");
            } catch {
                userType = await tempFolder.GetFileAsync("user.txt");
            }
            await FileIO.WriteTextAsync(userType, type.ToString());
            Frame.Navigate(typeof(JobListing));
        }
        
    }


    [DataContract]
    public class User {
        [DataMember]
        int id;
        [DataMember]
        public int type;
        [DataMember]
        String name;
        [DataMember]
        public String email;
        [DataMember]
        string password;
        public User(string _username, string _password) {
            email = _username;
            password = _password;
        }
    }
}
