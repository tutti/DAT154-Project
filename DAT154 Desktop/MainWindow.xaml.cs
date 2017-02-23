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
            booking.bookButton.Click += bookButtonClick;
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
            doSubmitNewTask();
            service.newPopup.IsOpen = false;
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
            doBook();
            booking.newbookingPopup.IsOpen = false;
        }
        public void filterBookingList(object sender, TextChangedEventArgs e) {
            //Get all users matching the name
            List<DAT154_Libs.User> users = DAT154_Libs.Data.getUsersByName((string)booking.nameFilter.Text);
            List<DAT154_Libs.Booking> results = new List<DAT154_Libs.Booking>();

            //For each user, find their bookings, and for each of their bookings add it to the list
            foreach (DAT154_Libs.User user in users) {
                List<DAT154_Libs.Booking> subresults = DAT154_Libs.Data.getBookings(user);
                foreach (DAT154_Libs.Booking subresult in subresults) {
                    results.Add(subresult);
                }
            } 

            booking.refreshBookingList(results);
        }
        public void filterTaskList(object sender, TextChangedEventArgs e) {
            //fetch new list filtered by room
            int roomnumber = Convert.ToInt32(service.roomFilter.Text);
            DAT154_Libs.Room room = DAT154_Libs.Data.getRoomByRoomNumber(roomnumber);
            List<DAT154_Libs.Task> tasks = DAT154_Libs.Data.getTasks(room, null, null);

            service.refreshTaskList(tasks);
        }
        public void searchAvailableRooms() {;
            int roomsize = booking.getRoomSizeInt();
            int roomquality = booking.getRoomQualityInt();
            int bednumber = (int) booking.bednumber.SelectedItem;
            DateTime startdate = booking.startdate.SelectedDate.Value;
            DateTime enddate = booking.startdate.SelectedDate.Value;

            booking.refreshRoomList(DAT154_Libs.Data.getRooms(bednumber, -1, roomsize, -1, roomquality, -1, startdate, enddate));
        }

        public void goBooking() {
            _mainFrame.Navigate(booking);
        }

        public void goService() {
            _mainFrame.Navigate(service);
        }

        public Boolean dologin() {
            string username = login.usernameInput.Text;
            string password = login.passwordInput.Password;

            //DAT154_Libs.Data.login(username, password);
            //The above should return a boolean or a user object

            //Temp code to prevent errors
            return true;
        }

        public void doSubmitNewTask() {
            DAT154_Libs.Task task = new DAT154_Libs.Task();
            task.category = service.getCategoryInt();
            task.notes = service.newtext.Text;
            task.room_id = ((DAT154_Libs.Room)service.newroom.SelectedItem).id;

            DAT154_Libs.Data.insert(task);
        }

        public void doLogout() {
            goLogin();
        }

        public void doBook() {
            DAT154_Libs.User user = DAT154_Libs.Data.getUserByEmail(booking.email.Text);
            DateTime startdate = booking.startdate.SelectedDate.Value;
            DateTime enddate = booking.startdate.SelectedDate.Value;
            DAT154_Libs.Room room = (DAT154_Libs.Room)booking.roomList.SelectedItem;

            DAT154_Libs.Data.bookRoom(user,room,startdate,enddate);
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
