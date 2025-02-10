namespace Shared.Enums.MilestoneRelationTypes
{
    public class MilestoneRelationTypeEnum : ValueObject
    {
        public override string ToString()
        {
            return Id == -1 ? Name : $"{Name}";
        }

        public static MilestoneRelationTypeEnum Create(int id, string name) => new MilestoneRelationTypeEnum() { Id = id, Name = name };
        public static MilestoneRelationTypeEnum None = Create(-1, "None");
        public static MilestoneRelationTypeEnum StartStart = Create(0, "Start Start");
        public static MilestoneRelationTypeEnum StartFinish = Create(1, "Start Finish");
        public static MilestoneRelationTypeEnum FinishStart = Create(2, "Finish Start");
        public static MilestoneRelationTypeEnum FinishFinish = Create(3, "Finish Finish");


        public static List<MilestoneRelationTypeEnum> List = new List<MilestoneRelationTypeEnum>()
        {
            None, StartStart, StartFinish, FinishStart, FinishFinish,

        };
        public static string GetName(int id) => List.Exists(x => x.Id == id) ? List.FirstOrDefault(x => x.Id == id)!.Name : string.Empty;

        public static MilestoneRelationTypeEnum GetType(string type) => List.Exists(x => x.Name.Equals(type)) ? List.FirstOrDefault(x => x.Name.Equals(type))! : None;

        public static MilestoneRelationTypeEnum GetType(int id) => List.Exists(x => x.Id == id) ? List.FirstOrDefault(x => x.Id == id)! : None;

    }
}