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
using System.Text.RegularExpressions;

namespace DAT154_Desktop {
    /// <summary>
    /// Interaction logic for Service.xaml
    /// </summary>
    public partial class Service : Page {

        ObservableCollection<TaskContainer> tasks;
        ObservableCollection<Room> rooms;
        ObservableCollection<string> categories;

        public Service() {
            InitializeComponent();
            
            taskList.MouseDoubleClick += taskClicked;

            tasks = new ObservableCollection<TaskContainer>();
            foreach (DAT154_Libs.Task task in DataAccess.getTasks()) {
                tasks.Add(new TaskContainer(task));
            }
            taskList.ItemsSource = tasks;

            rooms = new ObservableCollection<Room>();
            categories = new ObservableCollection<string>();

            categories.Add("Cleaning");
            categories.Add("Service");
            categories.Add("Maintenance");
            categories.Add("Conciergence");
            categories.Add("Legal");
            categories.Add("Exorcism");

            foreach (Room room in DataAccess.getRooms()) {
                rooms.Add(room);
            }
            newroom.ItemsSource = rooms;
            newcategory.ItemsSource = categories;

            // ----------- Events ----------------
            //New task popup
            newPopup.Opened += clearNewPopup;

        }

        private void clearNewPopup(object sender, EventArgs e) {
            newcategory.SelectedIndex = 0;
            newroom.SelectedIndex = 0;
            newtext.Text = "";
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
            TaskContainer selectedTask = ((TaskContainer)taskList.SelectedItem);
            viewroom.Content = selectedTask.room_number;
            viewcategory.Content = selectedTask.categories;
            viewtext.Content = selectedTask.myTask.notes;
        }

        public void refreshTaskList(List<DAT154_Libs.Task> _tasks) {
            tasks = new ObservableCollection<TaskContainer>();

            foreach (DAT154_Libs.Task _task in _tasks) {
                tasks.Add(new TaskContainer(_task));
            }
            taskList.ItemsSource = tasks;
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

        
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e) {
        Regex regex = new Regex("[^0-9]+");
        e.Handled = regex.IsMatch(e.Text);
        }
    }

    public class TaskContainer {
        public DAT154_Libs.Task myTask { get; set; }

        public TaskContainer(DAT154_Libs.Task _myTask) {
            myTask = _myTask;
            room_number = DataAccess.getRoomById(myTask.room_id).room_number;
        }

        public string categories {
            get {
                string cts = "";
                for (int i = 1; i < 64; i *= 2) {
                    if (myTask.isCategory(i)) {
                        if (cts.Length == 0) {
                            cts = cts + getCategoryString(i);
                        } else {
                            cts = cts + ", " + getCategoryString(i);
                        }
                    }
                }

                return cts;
            }
            set { categories = value; }
        }

        public int room_number { get; set; }

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
