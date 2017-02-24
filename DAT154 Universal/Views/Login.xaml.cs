using System;
using System.IO;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;

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
            int i = CheckServer(UsernameTextBox.Text, PasswordTextBox.Password);
            if (i>-1){
                writeToFile(i);
            }else if(i==-1){
                ErrorMessage.Text = "User not found";
            }else {
                ErrorMessage.Text = "Could not connect to server";
            }
            
        }

        private async int CheckServer(string username, string password) {


            HttpClient httpClient = new HttpClient();
            try {
                User u = new User(username,password);
                DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(User));
                MemoryStream ms = new MemoryStream();
                js.WriteObject(ms, u);
                ms.Position = 0;
                StreamReader sr = new StreamReader(ms);
                var content = new StringContent(sr.ReadToEnd(), Encoding.UTF8, "application/json");
                var result = await httpClient.PostAsync("localhost:1893", content);
                

            } catch {
                return -2;
            }
            if (username == "Magnar" && password == "Gya") {
                return 1;
            }
            return -1;
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
        int type;
        [DataMember]
        String name;
        [DataMember]
        public String email;
        [DataMember]
        string Password;
        public User(string _username, string _password) {
            email = _username;
            Password = _password;
        }
    }
}
