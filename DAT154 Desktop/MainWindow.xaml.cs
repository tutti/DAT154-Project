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
            booking.searchButton.Click += new RoutedEventHandler(searchButtonClick);
            booking.bookButton.Click += new RoutedEventHandler(bookButtonClick);
            booking.nameFilter.TextChanged += new TextChangedEventHandler(filterBookingList);
            booking.cancelButton.Click += new RoutedEventHandler(cancelButtonClick);
            booking.openButton.Click += new RoutedEventHandler(openButtonClick);
            booking.checkinButton.Click += new RoutedEventHandler(checkinButtonClick);
            booking.checkoutButton.Click += new RoutedEventHandler(checkoutButtonClick);

            //Service menu
            service.mainmenuButton.Click += new RoutedEventHandler(mainmenuButtonClick);
            service.newButton.Click += new RoutedEventHandler(submitButtonClick);
            service.roomFilter.TextChanged += new TextChangedEventHandler(filterTaskList);
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
        public void submitButtonClick(object sender, EventArgs e) {
            if (doSubmitNewTask()) {
                service.newPopup.IsOpen = false;
            } else {
                service.newerror.Content = "Failed to submit task";
            }
        }
        public void bookingButtonClick(object sender, EventArgs e) {
            goBooking();
        }
        public void cancelButtonClick(object sender, EventArgs e) {
            doCancel();
        }
        public void openButtonClick(object sender, EventArgs e) {
            doOpen();
        }
        public void checkinButtonClick(object sender, EventArgs e) {
            doCheckin();
        }
        public void checkoutButtonClick(object sender, EventArgs e) {
            doCheckout();
        }
        public void searchButtonClick(object sender, EventArgs e) {
            searchAvailableRooms();
        }
        public void serviceButtonClick(object sender, EventArgs e) {
            goService();
        }

        public void logoutButtonClick(object sender, EventArgs e) {
            doLogout();
            goLogin();
        }


        public void bookButtonClick(object sender, EventArgs e) {
            if (doBook()) {
                booking.newbookingPopup.IsOpen = false;
            } else {
                booking.error.Content = "No rooms meet the requirement";
            }
        }
        public void filterBookingList(object sender, TextChangedEventArgs e) {
            //fetch new list filtered by name
        }
        public void filterTaskList(object sender, TextChangedEventArgs e) {
            //fetch new list filtered by room
        }
        public void searchAvailableRooms() {
            //fetch list of rooms matching search parameters
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

        public Boolean doSubmitNewTask() {
            return false;
        }

        public void doLogout() {
            goLogin();
        }

        public Boolean doBook() {
            return false;
        }

        public void doCancel() {

        }
        public void doOpen() {

        }
        public void doCheckin() {

        }
        public void doCheckout() {
        }
    }
}
