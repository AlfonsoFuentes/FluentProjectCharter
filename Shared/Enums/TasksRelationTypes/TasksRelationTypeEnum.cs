namespace Shared.Enums.TasksRelationTypes
{
    public class TasksRelationTypeEnum : ValueObject
    {
        public override string ToString()
        {
            return Id == -1 ? Name : $"{Name}";
        }

        public static TasksRelationTypeEnum Create(int id, string name) => new TasksRelationTypeEnum() { Id = id, Name = name };
        public static TasksRelationTypeEnum None = Create(-1, "None");
        public static TasksRelationTypeEnum StartStart = Create(0, "Start Start");
        public static TasksRelationTypeEnum StartFinish = Create(1, "Start End");
        public static TasksRelationTypeEnum FinishStart = Create(2, "End Start");
        public static TasksRelationTypeEnum FinishFinish = Create(3, "End End");


        public static List<TasksRelationTypeEnum> List = new List<TasksRelationTypeEnum>()
        {
            None, StartStart, StartFinish, FinishStart, FinishFinish,

        };
        public static string GetName(int id) => List.Exists(x => x.Id == id) ? List.FirstOrDefault(x => x.Id == id)!.Name : string.Empty;

        public static TasksRelationTypeEnum GetType(string type) => List.Exists(x => x.Name.Equals(type)) ?
            List.FirstOrDefault(x => x.Name.Equals(type))! : None;

        public static TasksRelationTypeEnum GetType(int id) => List.Exists(x => x.Id == id) ? List.FirstOrDefault(x => x.Id == id)! : None;

    }
}