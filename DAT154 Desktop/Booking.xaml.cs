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
using System.Collections.ObjectModel;

namespace DAT154_Desktop {
    /// <summary>
    /// Interaction logic for Booking.xaml
    /// </summary>
    public partial class Booking : Page {
        ObservableCollection<DAT154_Libs.Booking> bookings;
        ObservableCollection<DAT154_Libs.Room> rooms;
        ObservableCollection<string> roomsizes;
        ObservableCollection<string> roomqualities;
        ObservableCollection<int> bednumbers;

        public Booking() {
            InitializeComponent();

            //Bookings list
            bookings = new ObservableCollection<DAT154_Libs.Booking>();

            foreach (DAT154_Libs.Booking booking in FakeData.getBookings()) {
                bookings.Add(booking);
            }

            bookingList.ItemsSource = bookings;

            //New booking popup menu
            rooms = new ObservableCollection<DAT154_Libs.Room>();
            roomsizes = new ObservableCollection<string>();
            roomqualities = new ObservableCollection<string>();
            bednumbers = new ObservableCollection<int>();

            roomsizes.Add("Single");
            roomsizes.Add("Double");
            roomsizes.Add("Suite");

            roomqualities.Add("Standard");
            roomqualities.Add("Superior");
            roomqualities.Add("Deluxe");

            for (int i = 1; i <= 8; i++) {
                bednumbers.Add(i);
            }

            roomList.ItemsSource = rooms;
            roomsize.ItemsSource = roomsizes;
            roomquality.ItemsSource = roomqualities;
            bednumber.ItemsSource = bednumbers;

            startdate.SelectedDate = DateTime.Today;
            enddate.SelectedDate = startdate.SelectedDate.Value.AddDays(1);

            // ----------- Events --------------

            //Date selection
            startdate.SelectedDateChanged += dateChanged;
            enddate.SelectedDateChanged += dateChanged;
        }

        private void dateSanityCheck() {
            if (enddate.SelectedDate < startdate.SelectedDate) {
                enddate.SelectedDate = startdate.SelectedDate.Value.AddDays(1);
            }
        }

        private void dateChanged(object sender, SelectionChangedEventArgs e) {
            dateSanityCheck();
        }

        private void addButtonClick(object sender, RoutedEventArgs e) {
            newbookingPopup.IsOpen = true;
        }
        private void closeBooking(object sender, MouseButtonEventArgs e) {
            newbookingPopup.IsOpen = false;
        }

        private void mouseoverX(object sender, MouseEventArgs e) {
            popupexitButton.Background = new SolidColorBrush() { Color = Colors.Red };
        }

        private void mouseleaveX(object sender, MouseEventArgs e) {
            popupexitButton.Background = new SolidColorBrush() { Color = Colors.AliceBlue };
        }

        public int getRoomSizeInt() {
            switch ((string)roomsize.SelectedItem) {
                case "Single":  return 0;
                case "Double":  return 1;
                case "Suite":   return 2;

                default : return -1;
            }
        }

        public int getRoomQualityInt() {
            switch ((string)roomquality.SelectedItem) {
                case "Standard":    return 0;
                case "Superior":    return 1;
                case "Deluxe":      return 2;

                default: return -1;
            }
        }

        public void refreshRoomList(List<DAT154_Libs.Room> _rooms) {
            rooms = new ObservableCollection<DAT154_Libs.Room>();

            foreach (DAT154_Libs.Room _room in _rooms) {
                rooms.Add(_room);
            }
        }

        public void refreshBookingList(List<DAT154_Libs.Booking> _bookings) {
            bookings = new ObservableCollection<DAT154_Libs.Booking>();

            foreach (DAT154_Libs.Booking _booking in _bookings) {
                bookings.Add(_booking);
            }
        }
    }
}
