using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

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
            
            if (UsernameTextBox.Text == "Magnar"){
                writeToFile("16");
            }else {
                ErrorMessage.Text = "User not found";
            }
            
        }

        async void writeToFile(string type) {
            StorageFolder tempFolder = ApplicationData.Current.TemporaryFolder;
            StorageFile userType;
            try {
                userType = await tempFolder.CreateFileAsync("user.txt");
            } catch {
                userType = await tempFolder.GetFileAsync("user.txt");
            }
            await FileIO.WriteTextAsync(userType, type);
            Frame.Navigate(typeof(JobListing));
        }
        
    }
}
