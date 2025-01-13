namespace Shared.Enums.ConnectionTypes
{
    public class ConnectionTypeEnum : ValueObject
    {
        public override string ToString()
        {
            return Id == -1 ? Name : $"{Name}";
        }

        public static ConnectionTypeEnum Create(int id, string name) => new ConnectionTypeEnum() { Id = id, Name = name };
        public static ConnectionTypeEnum None = Create(-1, "None");
        public static ConnectionTypeEnum Triclamp = Create(0, "Clamp");
        public static ConnectionTypeEnum Flange = Create(1, "Flange");
        public static ConnectionTypeEnum Threaded = Create(2, "Threaded");
        public static ConnectionTypeEnum BeavelSeat = Create(3, "Beavel Seat");
        public static ConnectionTypeEnum NPT = Create(4, "NPT");
        public static ConnectionTypeEnum Wafer = Create(5, "Wafer");
        public static ConnectionTypeEnum Lug = Create(5, "Lug");

        public static List<ConnectionTypeEnum> List = new List<ConnectionTypeEnum>()
        {
            None, Triclamp, Flange, Threaded, BeavelSeat, NPT, Wafer,Lug

        };
        public static string GetName(int id) => List.Exists(x => x.Id == id) ? List.FirstOrDefault(x => x.Id == id)!.Name : string.Empty;

        public static ConnectionTypeEnum GetType(string type) => List.Exists(x => x.Name == type) ? List.FirstOrDefault(x => x.Name == type)! : None;

        public static ConnectionTypeEnum GetType(int id) => List.Exists(x => x.Id == id) ? List.FirstOrDefault(x => x.Id == id)! : None;

    }
}