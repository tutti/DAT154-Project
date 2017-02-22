using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace DAT154_Universal.Views {
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class JobListing : Page {
        List<MockTasks> a;
        public JobListing() {
            this.InitializeComponent();
            a = new List<MockTasks>();
            a.Add(new MockTasks(1, 1, 0, 1, ""));
            a.Add(new MockTasks(2, 404, -1, 16, ""));
            TaskList.ItemsSource = a;
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
            public static readonly int MAINTENANCE = 3;
            public static readonly int CONCIERGENCE = 4;
            public static readonly int LEGAL = 8;
            public static readonly int EXORCISM = 16;
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
