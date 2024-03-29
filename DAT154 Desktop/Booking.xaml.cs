﻿using System;
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
using DAT154_Libs;

namespace DAT154_Desktop {
    /// <summary>
    /// Interaction logic for Booking.xaml
    /// </summary>
    public partial class Booking : Page {
        ObservableCollection<BookingContainer> bookings;
        ObservableCollection<Room> rooms;
        ObservableCollection<string> roomsizes;
        ObservableCollection<string> roomqualities;
        ObservableCollection<int> bednumbers;

        public Booking() {
            InitializeComponent();

            //Bookings list
            bookings = new ObservableCollection<BookingContainer>();

            foreach (DAT154_Libs.Booking booking in DataAccess.getBookings()) {
                bookings.Add(new BookingContainer(booking));
            }

            bookingList.ItemsSource = bookings;

            //New booking popup menu
            rooms = new ObservableCollection<Room>();
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

            //Popup reset
            newbookingPopup.Opened += clearNewPopup;
        }

        private void dateSanityCheck() {
            if (enddate.SelectedDate <= startdate.SelectedDate) {
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

        private void clearNewPopup(object sender, EventArgs e) {
            email.Text = "";
            roomquality.SelectedIndex = 0;
            roomsize.SelectedIndex = 0;
            bednumber.SelectedIndex = 0;
            startdate.SelectedDate = DateTime.Today;
            enddate.SelectedDate = startdate.SelectedDate.Value.AddDays(1);

            rooms = new ObservableCollection<Room>();
            roomList.ItemsSource = rooms;
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

        public void refreshRoomList(List<Room> _rooms) {
            rooms = new ObservableCollection<Room>();

            foreach (Room _room in _rooms) {
                rooms.Add(_room);
            }

            roomList.ItemsSource = rooms;
        }

        public void refreshBookingList(List<DAT154_Libs.Booking> _bookings) {
            bookings = new ObservableCollection<BookingContainer>();

            foreach (DAT154_Libs.Booking _booking in _bookings) {
                bookings.Add(new BookingContainer(_booking));
            }

            bookingList.ItemsSource = bookings;
        }
    }

    public class BookingContainer {
        public DAT154_Libs.Booking myBooking { get; set;}
        public string name { get; set; }
        public int room { get; set; }
        public string status { get {return statusString(myBooking.booking_status); } set {;} }

        public BookingContainer(DAT154_Libs.Booking _myBooking) {
            myBooking = _myBooking;
            room = DataAccess.getRoomById(myBooking.room_id).room_number;
            name = DataAccess.getUserById(myBooking.user_id).name;
        }

        private static string statusString(int statusInt) {
            switch(statusInt) {
                case -1: return "Canceled";
                case 0: return "Open";
                case 2: return "Checked in";
                case 3: return "Completed";

                default: return "invalid status";
            }
        }
    }
}
