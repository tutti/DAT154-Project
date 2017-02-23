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
using DAT154_Libs;

namespace DAT154_Desktop {
    /// <summary>
    /// Interaction logic for Service.xaml
    /// </summary>
    public partial class Service : Page {

        ObservableCollection<DAT154_Libs.Task> tasks;
        ObservableCollection<DAT154_Libs.Room> rooms;
        ObservableCollection<string> categories;

        public Service() {
            InitializeComponent();
            
            taskList.MouseDoubleClick += taskClicked;

            tasks = new ObservableCollection<DAT154_Libs.Task>();
            foreach (DAT154_Libs.Task task in FakeData.getTasks()) {
                tasks.Add(task);
            }
            taskList.ItemsSource = tasks;

            rooms = new ObservableCollection<DAT154_Libs.Room>();
            categories = new ObservableCollection<string>();

            categories.Add("Cleaning");
            categories.Add("Service");
            categories.Add("Maintenance");
            categories.Add("Conciergence");
            categories.Add("Legal");
            categories.Add("Exorcism");

            foreach (DAT154_Libs.Room room in FakeData.getRooms()) {
                rooms.Add(room);
            }
            newroom.ItemsSource = rooms;
            newcategory.ItemsSource = categories;
        }

        private void addButtonClick(object sender, RoutedEventArgs e) {
            newPopup.IsOpen = true;
        }

        private void closeNew(object sender, MouseButtonEventArgs e) {
            newPopup.IsOpen = false;
        }
        private void newmouseoverX(object sender, MouseEventArgs e) {
            newpopupexitButton.Background = new SolidColorBrush() { Color = Colors.Red };
        }
        private void newmouseleaveX(object sender, MouseEventArgs e) {
            newpopupexitButton.Background = new SolidColorBrush() { Color = Colors.AliceBlue };
        }

        private void closeView(object sender, MouseButtonEventArgs e) {
            viewPopup.IsOpen = false;
        }
        private void viewmouseoverX(object sender, MouseEventArgs e) {
            viewpopupexitButton.Background = new SolidColorBrush() { Color = Colors.Red };
        }
        private void viewmouseleaveX(object sender, MouseEventArgs e) {
            viewpopupexitButton.Background = new SolidColorBrush() { Color = Colors.AliceBlue };
        }

        private void taskClicked(object sender, MouseButtonEventArgs e) {
            Console.WriteLine("clicked");
            viewPopup.IsOpen = true;
            DAT154_Libs.Task selectedTask = (DAT154_Libs.Task)taskList.SelectedItem;
            viewroom.Content = selectedTask.room_id;
            viewcategory.Content = selectedTask.category;
            viewtext.Content = selectedTask.notes;
        }

        public void refreshTaskList(List<DAT154_Libs.Task> _tasks) {
            tasks = new ObservableCollection<DAT154_Libs.Task>();

            foreach (DAT154_Libs.Task _task in _tasks) {
                tasks.Add(_task);
            }
        }

        public int getCategoryInt() {
            switch((string)newcategory.SelectedItem) {
                case "Cleaning": return DAT154_Libs.Task.CATEGORY.CLEANING;
                case "Service": return DAT154_Libs.Task.CATEGORY.SERVICE;
                case "Maintenance": return DAT154_Libs.Task.CATEGORY.MAINTENANCE;
                case "Conciergence": return DAT154_Libs.Task.CATEGORY.CONCIERGENCE;
                case "Legal": return DAT154_Libs.Task.CATEGORY.LEGAL;
                case "Exorcism": return DAT154_Libs.Task.CATEGORY.EXORCISM;

                default: return -1;
            }
        }

        public static string getCategoryString(int categoryInt) {
            switch (categoryInt) {
                case 1: return "Cleaning";
                case 2: return "Service";
                case 4: return "Maintenance";
                case 8: return "Conciergence";
                case 16: return "Legal";
                case 32: return "Exorcism";

                default: return "invalid category";
            }

        }
    }
}
