using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DAT154_Desktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Login login = new Login();
        MainMenu mainMenu = new MainMenu();
        Booking booking = new Booking();
        Service service = new Service();

        public MainWindow()
        {
            InitializeButtons();
            InitializeComponent();
            goLogin();
        }

        public void InitializeButtons() {
            //Login menu
            login.loginButton.Click += new RoutedEventHandler(loginButtonClick);
            
            //Main menu
            mainMenu.bookingButton.Click += new RoutedEventHandler(bookingButtonClick);
            mainMenu.serviceButton.Click += new RoutedEventHandler(serviceButtonClick);
            mainMenu.logoutButton.Click += new RoutedEventHandler(logoutButtonClick);

            //Booking menu
            booking.mainmenuButton.Click += new RoutedEventHandler(mainmenuButtonClick);
            booking.addBookingButton.Click += new RoutedEventHandler(addbookingButtonClick);

            booking.bookButton.Click += new RoutedEventHandler(bookButtonClick);

            //Service menu
            service.mainmenuButton.Click += new RoutedEventHandler(mainmenuButtonClick);
        }

        public void goLogin() {
            _mainFrame.Navigate(login);
        }

        public void goMainMenu() {
            _mainFrame.Navigate(mainMenu);
        }

        public void loginButtonClick(object sender, EventArgs e) {
            if (dologin()) {
                mainmenuButtonClick(sender, e);
            }
        }

        public void mainmenuButtonClick(object sender, EventArgs e) {
            goMainMenu();
        }

        public void bookingButtonClick(object sender, EventArgs e) {
            goBooking();
        }

        public void serviceButtonClick(object sender, EventArgs e) {
            goService();
        }

        public void logoutButtonClick(object sender, EventArgs e) {
            doLogout();
            goLogin();
        }

        public void addbookingButtonClick(object sender, EventArgs e) {
            booking.newbookingPopup.IsOpen = true;
        }

        public void bookButtonClick(object sender, EventArgs e) {
            if (doBook()) {
                booking.newbookingPopup.IsOpen = false;
            } else {
                booking.error.Content = "No rooms meet the requirement";
            }
        }

        public void goBooking() {
            _mainFrame.Navigate(booking);
        }

        public void goService() {
            _mainFrame.Navigate(service);
        }

        public Boolean dologin() {
            return true;
        }

        public void doLogout() {
            goLogin();
        }

        public Boolean doBook() {
            return false;
        }
    }
}
