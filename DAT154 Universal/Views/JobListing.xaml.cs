using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.Storage;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Net.Http;
using System.Threading.Tasks;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace DAT154_Universal.Views {
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class JobListing : Page {
        List<TaskView> a;
        int job_type;
        int job_type_index;
        public JobListing() {
            this.InitializeComponent();
            getJobFile();
        }
        public async void getJobFile() {
            StorageFolder tempFolder = ApplicationData.Current.TemporaryFolder;
            try {
                StorageFile file = await tempFolder.GetFileAsync("user.txt");
                String user = await FileIO.ReadTextAsync(file);
                job_type = Int32.Parse(user);
            } catch {
                job_type = 1;
            }
            switch (job_type) {
                case 1:
                    job_type_index = 0;
                    break;
                case 2:
                    job_type_index = 1;
                    break;
                case 4:
                    job_type_index = 2;
                    break;
                case 8:
                    job_type_index = 3;
                    break;
                case 16:
                    job_type_index = 4;
                    break;
                case 32:
                    job_type_index = 5;
                    break;
                default:
                    job_type_index = 0;
                    break;
            }
            job_type_list.SelectedIndex = job_type_index;
            a = new List<TaskView>();
            System.Threading.Tasks.Task.Delay(10).Wait();
            checkServer(job_type);
        }

        async void checkServer(int job_type) {
            HttpClient httpClient = new HttpClient();
            try {
                var variables = new Dictionary<string, string> {
                    
                    {"category",job_type.ToString() },
                    {"status", "0" }
                };
                var content = new FormUrlEncodedContent(variables);
                var result = await httpClient.PostAsync("http://localhost:1893/api/searchTasks", content);
                parseJSON(await result.Content.ReadAsStringAsync());
            } catch {

            }
            System.Threading.Tasks.Task.Delay(600).Wait();
            TaskList.ItemsSource = a;
        }
        

        public void refreshButton_clicked(object sender, RoutedEventArgs args) {
            TaskList.ItemsSource = null;
            TaskList.Items.Clear();
            checkServer(job_type);
        }

        public void JobTypeChange(object sender, SelectionChangedEventArgs args) {
            job_type_index = job_type_list.SelectedIndex;
            switch (job_type_index) {
                case 0:
                    job_type = 1;
                    break;
                case 1:
                    job_type = 2;
                    break;
                case 2:
                    job_type = 4;
                    break;
                case 3:
                    job_type = 8;
                    break;
                case 4:
                    job_type = 16;
                    break;
                case 5:
                    job_type = 32;
                    break;
            }

            TaskList.ItemsSource = null;
            TaskList.Items.Clear();
            checkServer(job_type);
        }
        public void UpdateStatus(object sender, SelectionChangedEventArgs args) {
            var comboBox = (ComboBox)sender;
            int newStatus = comboBox.SelectedIndex;
            TaskView tv = (TaskView)comboBox.DataContext;
            if (tv != null) {
                tv.task.status = newStatus;
            }

        }

        public void UpdateNote(object sender, TextChangedEventArgs args) {
            var textBox = (TextBox)sender;
            string newNote = textBox.Text;
            TaskView tv = (TaskView)textBox.DataContext;
            if (tv != null) {
                tv.task.notes = newNote;
            }
        }
        public async void updateServer(object sender, RoutedEventArgs args) {
            var button = (Button)sender;
            TaskView taskview = (TaskView)button.DataContext;

            HttpClient httpClient = new HttpClient();
            try {
                var variables = new Dictionary<string, string> {
                    {"task_id",taskview.task.id.ToString() },
                    {"room_id",taskview.room.room_id.ToString() },
                    {"status", taskview.task.status.ToString() },
                    {"category", taskview.task.category.ToString() },
                    {"notes", taskview.task.notes }
                };
                var content = new FormUrlEncodedContent(variables);
                var result = await httpClient.PostAsync("http://localhost:1893/api/saveTask", content);
            } catch {

            }
        }

        public void parseJSON(string JSON) {

            string list = JSON.Substring(JSON.IndexOf("[") + 1);
            a = new List<TaskView>();
            while (list.Contains(":")) {
                string id = list.Substring(list.IndexOf(":") + 1);
                id = id.Substring(0, id.IndexOf(","));
                list = list.Substring(list.IndexOf(",") + 1);

                string roomid = list.Substring(list.IndexOf(":") + 1);
                roomid = roomid.Substring(0, roomid.IndexOf(","));
                list = list.Substring(list.IndexOf(",") + 1);

                string status = list.Substring(list.IndexOf(":") + 1);
                status = status.Substring(0, status.IndexOf(","));
                list = list.Substring(list.IndexOf(",") + 1);

                string category = list.Substring(list.IndexOf(":") + 1);
                category = category.Substring(0, category.IndexOf(","));
                list = list.Substring(list.IndexOf(",") + 1);

                string notes = list.Substring(list.IndexOf(":") + 1);
                notes = notes.Substring(notes.IndexOf("\"")+1);
                notes = notes.Substring(0, notes.IndexOf("\""));
                if (list.Contains(",")) {
                    list = list.Substring(list.IndexOf(",") + 1);
                } else {
                    list = "";
                }
                a.Add(new TaskView(Convert.ToInt32(id), Convert.ToInt32(roomid), Convert.ToInt32(status), Convert.ToInt32(category), notes));

            }
            
        }
    }

    public class TaskView {
        public Task task { get; set; }
        public Room room { get; set; }
        public TaskView(int _id, int _room_id, int _status, int _category, string _notes) {
            task = new Task(_id, _room_id, _status, _category, _notes);
            room = new Room(_room_id);
        }


    }

    public class Room {
        public int room_id { get; set; }
        public int room_number { get; set; }
        public Room(int _room_id) {
            room_id = _room_id;
            getRoomNumber(_room_id);
        }

        async void getRoomNumber(int _room_id) {
            HttpClient httpClient = new HttpClient();
            try {
                var variables = new Dictionary<string, string> {
                    {"room_id",_room_id.ToString() }
                };
                var content = new FormUrlEncodedContent(variables);
                var result = await httpClient.PostAsync("http://localhost:1893/api/getRoom", content);
                parseRoom(await result.Content.ReadAsStringAsync());
            } catch {

            }
        }
        public void parseRoom(string JSON) {
            string list = JSON.Substring(JSON.IndexOf("room_number"));
            list = list.Substring(list.IndexOf(":") + 1);
            list = list.Substring(0, list.IndexOf(","));
            room_number = Convert.ToInt32(list);
        }

    }

    public class Task {
        public class STATUS {
            public static readonly int CANCELED = -1;
            public static readonly int NEW = 0;
            public static readonly int ASSIGNED = 1;
            public static readonly int COMPLETED = 2;
        }

        public class CATEGORY {
            public static readonly int CLEANING = 1;
            public static readonly int SERVICE = 2;
            public static readonly int MAINTENANCE = 4;
            public static readonly int CONCIERGENCE = 8;
            public static readonly int LEGAL = 16;
            public static readonly int EXORCISM = 32;
        }

        public Task(int _id, int _room_id, int _status, int _category, string _notes) {
            id = _id;
            room_id = _room_id;
            switch (_status) {
                case 1: status = -1;
                    break;
                case 2: status = 0;
                    break;
                case 4: status = 1;
                    break;
                case 8: status = 2;
                    break;
            }
            status = _status;
            category = _category;
            notes = _notes;
        }
        public int id { get; set; }

        public int room_id { get; set; }

        public int status { get; set; }

        public int category { get; set; }

        public string notes { get; set; }

        public bool isCategory(int category) {
            return (this.category & category) == category;
        }
    }
}

