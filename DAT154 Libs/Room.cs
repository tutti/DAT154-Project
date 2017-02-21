using System.Data.Linq.Mapping;

namespace DAT154_Libs {
    [Table(Name = "room")]
    public class Room {

        [Column(IsPrimaryKey = true, CanBeNull = false)]
        public int id { get; set; }

        [Column(CanBeNull = false)]
        public int room_number { get; set; }

        [Column(CanBeNull = false)]
        public int beds { get; set; }

        [Column(CanBeNull = false)]
        public int room_size { get; set; }

        [Column(CanBeNull = false)]
        public int quality { get; set; }
    }
}