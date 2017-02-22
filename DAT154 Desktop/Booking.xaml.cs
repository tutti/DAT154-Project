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
        public Booking() {
            InitializeComponent();

            bookings = new ObservableCollection<DAT154_Libs.Booking>();
            rooms = new ObservableCollection<DAT154_Libs.Room>();
            foreach (DAT154_Libs.Booking booking in FakeData.getBookings()) {
                bookings.Add(booking);
            }

            bookingList.ItemsSource = bookings;
            roomList.ItemsSource = rooms;
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
    }
}
