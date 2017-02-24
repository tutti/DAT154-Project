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
            }catch {
                job_type = 1;
            }
            switch (job_type) {
                case 1: job_type_index = 0;
                    break;
                case 2: job_type_index = 1;
                    break;
                case 4: job_type_index = 2;
                    break;
                case 8: job_type_index = 3;
                    break;
                case 16: job_type_index = 4;
                    break;
                case 32: job_type_index = 5;
                    break;
            }
            job_type_list.SelectedIndex = job_type_index;
            a = new List<TaskView>();
            checkServer(job_type);
        }

        async void checkServer(int job_type) {
            HttpClient httpClient = new HttpClient();
            try {
                var variables = new Dictionary<string, string> {
                    {"category",job_type.ToString() }
                };
                var content = new FormUrlEncodedContent(variables);
                var result = await httpClient.PostAsync("http://localhost:1893/api/searchTask", content);
                parseJSON(await result.Content.ReadAsStringAsync());
            } catch {

            }
                
                TaskList.ItemsSource = a;
        }

        public void refreshButton_clicked(object  sender, RoutedEventArgs args) {
            TaskList.ItemsSource = null;
            TaskList.Items.Clear();
            checkServer(job_type);
        }

        public void JobTypeChange(object sender, SelectionChangedEventArgs args) {
            job_type_index = job_type_list.SelectedIndex;
            switch (job_type_index) {
                case 0: job_type = 1;
                    break;
                case 1: job_type = 2;
                    break;
                case 2: job_type = 4;
                    break;
                case 3: job_type = 8;
                    break;
                case 4: job_type = 16;
                    break;
                case 5: job_type = 32;
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
            string newNote =textBox.Text;
            TaskView tv = (TaskView)textBox.DataContext;
            if(tv != null) {
                tv.task.notes = newNote;
            }
        }

        public void parseJSON(string JSON) {
            testName.Text = JSON;
        }
    }

    public class TaskView {
        public MockTasks task { get; set; }
        public Room room { get; set; }
        public TaskView(int _id, int _room_id, int _status, int _category, string _notes) {
            task = new MockTasks(_id, _room_id, _status, _category, _notes);
            room = new Room(_room_id);
        }

     
    }

    public class Room {
        public int room_id { get; set; }
        public int room_number { get; set; }
        public Room(int _room_id) {
            room_id = _room_id;
            room_number = _room_id;
        }
    }
    public class MockTasks {
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

        public MockTasks(int _id, int _room_id, int _status, int _category, string _notes) {
            id = _id;
            room_id = _room_id;
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
