using System.Reflection;
using static Shared.StaticClasses.StaticClass;

namespace Shared.StaticClasses
{
    public static class StaticClass
    {
        public static class Actions
        {
            public static string CreateUpdate = "CreateUpdate";
            public static string Create = "Create";
            public static string Update = "Update";
            public static string Approve = "Approve";
            public static string Close = "Close";
            public static string GetAll = "GetAll";
            public static string ToSearch = "ToSearch";
            public static string Delete = "Delete";
            public static string DeleteGroup = "DeleteGroup";
            public static string GetById = "GetById";
            public static string Export = "Export";
            public static string Validate = $"Validate";

        }
        public static class ResponseMessages
        {
            public static string ReponseSuccesfullyMessage(string rowName, string tablename, string ResponseType) =>
               $"{rowName} was {ResponseType} succesfully in table: {tablename}";
            public static string ReponseFailMessage(string rowName, string tablename, string ResponseType) =>
               $"{rowName} was not {ResponseType} succesfully in table: {tablename}";

            public static string ReponseSuccesfullyMessageCreated(string rowName, string tablename) =>
                $"{rowName} was {ResponseType.Created} succesfully in table: {tablename}";
            public static string ReponseSuccesfullyMessageUpdated(string rowName, string tablename) =>
               $"{rowName} was {ResponseType.Updated} succesfully in table: {tablename}";
            public static string ReponseSuccesfullyMessageDelete(string rowName, string tablename) =>
               $"{rowName} was {ResponseType.Delete} succesfully in table: {tablename}";
            public static string ReponseFailMessageCreated(string rowName, string tablename) =>
                $"{rowName} was not {ResponseType.Created} succesfully in table: {tablename}";
            public static string ReponseFailMessageUpdate(string rowName, string tablename) =>
                $"{rowName} was not {ResponseType.Updated} succesfully in table: {tablename}";
            public static string ReponseFailMessageDelete(string rowName, string tablename) =>
                $"{rowName} was not {ResponseType.Delete} succesfully in table: {tablename}";
            public static string ReponseNotFound(string tablename) =>
               $"row was not {ResponseType.NotFound} succesfully in table: {tablename}";

        }
        public static class ResponseType
        {
            public static string Created = "created";
            public static string Updated = "updated";
            public static string Delete = "delete";
            public static string NotFound = "found";
            public static string UnApprove = "un approved";
            public static string Approve = "approved";
            public static string Reopen = "re opened";
            public static string Received = "received";
            public static string ChangeStatus = "changed status";
        }


        public static class EngineeringFluidCodes
        {
            public static string ClassLegend = "Engineering Fluid Code";
            public static string ClassName => MethodBase.GetCurrentMethod()!.DeclaringType!.Name;
            public static class EndPoint
            {
                public static string CreateUpdate = $"{ClassName}/{Actions.CreateUpdate}";

                public static string GetAll = $"{ClassName}/{Actions.GetAll}";
                public static string GetById = $"{ClassName}/{Actions.GetById}";
                public static string Delete = $"{ClassName}/{Actions.Delete}";
                public static string Export = $"{ClassName}/{Actions.Export}";
                public static string Validate = $"{ClassName}/{Actions.Validate}";
                public static string DeleteGroup = $"{ClassName}/{Actions.DeleteGroup}";
            }
            public static class Cache
            {

                public static string[] Key(Guid BrandId) => new[] { GetAll, GetById(BrandId) };
                public static string GetAll => $"GetAll-{ClassName}";
                public static string GetById(Guid BrandId) => $"GetById-{ClassName}-{BrandId}";
            }
            public static class PageName
            {
                public static string Create = $"Create{ClassName}";
                public static string Update = $"Update{ClassName}";
                public static string GetAll = $"GetAll{ClassName}";

            }


        }
        public static class Brands
        {
            public static string ClassLegend = "Brand";
            public static string ClassName => MethodBase.GetCurrentMethod()!.DeclaringType!.Name;
            public static class EndPoint
            {
                public static string CreateUpdate = $"{ClassName}/{Actions.CreateUpdate}";
                public static string GetAll = $"{ClassName}/{Actions.GetAll}";
                public static string GetById = $"{ClassName}/{Actions.GetById}";
                public static string Delete = $"{ClassName}/{Actions.Delete}";
                public static string DeleteGroup = $"{ClassName}/{Actions.DeleteGroup}";
                public static string Export = $"{ClassName}/{Actions.Export}";
                public static string Validate = $"{ClassName}/{Actions.Validate}";
            }
            public static class Cache
            {

                public static string[] Key(Guid BrandId) => new[] { GetAll, GetById(BrandId) };
                public static string GetAll => $"GetAll-{ClassName}";
                public static string GetById(Guid BrandId) => $"GetById-{ClassName}-{BrandId}";
            }
            public static class PageName
            {
                public static string Create = $"Create{ClassName}";
                public static string Update = $"Update{ClassName}";
                public static string GetAll = $"GetAll{ClassName}";

            }


        }
        public static class StakeHolders
        {
            public static string ClassLegend = "Stakeholder";
            public static string ClassName => MethodBase.GetCurrentMethod()!.DeclaringType!.Name;
            public static class EndPoint
            {
                public static string CreateUpdate = $"{ClassName}/{Actions.CreateUpdate}";
                public static string DeleteGroup = $"{ClassName}/{Actions.DeleteGroup}";



                public static string GetAll = $"{ClassName}/{Actions.GetAll}";
                public static string GetById = $"{ClassName}/{Actions.GetById}";

                public static string Delete = $"{ClassName}/{Actions.Delete}";

                public static string Export = $"{ClassName}/{Actions.Export}";
                public static string Validate = $"{ClassName}/{Actions.Validate}";
            }
            public static class Cache
            {

                public static string[] Key(Guid Id) => new[] { GetAll, GetById(Id) };
                public static string GetAll => $"GetAll-{ClassName}";
                public static string GetById(Guid Id) => $"GetById-{ClassName}-{Id}";
            }
            public static class PageName
            {
                public static string Create = $"Create{ClassName}";
                public static string Update = $"Update{ClassName}";
                public static string GetAll = $"GetAll{ClassName}";

            }


        }
        public static class Suppliers
        {
            public static string ClassLegend = "Supplier";
            public static string ClassName => MethodBase.GetCurrentMethod()!.DeclaringType!.Name;
            public static class EndPoint
            {
                public static string CreateUpdate = $"{ClassName}/{Actions.CreateUpdate}";
                public static string GetAll = $"{ClassName}/{Actions.GetAll}";
                public static string GetById = $"{ClassName}/{Actions.GetById}";
                public static string Delete = $"{ClassName}/{Actions.Delete}";
                public static string DeleteGroup = $"{ClassName}/{Actions.DeleteGroup}";
                public static string Export = $"{ClassName}/{Actions.Export}";
                public static string ValidateName = $"{ClassName}/{Actions.Validate}Name";
                public static string ValidateVendorCode = $"{ClassName}/{Actions.Validate}VendorCode";
                public static string ValidateNickName = $"{ClassName}/{Actions.Validate}NickName";
            }
            public static class Cache
            {

                public static string[] Key(Guid BrandId) => new[] { GetAll, GetById(BrandId) };
                public static string GetAll => $"GetAll-{ClassName}";
                public static string GetById(Guid BrandId) => $"GetById-{ClassName}-{BrandId}";
            }
            public static class PageName
            {
                public static string Create = $"Create{ClassName}";
                public static string Update = $"Update{ClassName}";
                public static string GetAll = $"GetAll{ClassName}";

            }


        }
        public static class EquipmentTemplates
        {
            public static string ClassLegend = "Equipment Template";
            public static string ClassName => MethodBase.GetCurrentMethod()!.DeclaringType!.Name;
            public static class EndPoint
            {
                public static string Create = $"{ClassName}/{Actions.Create}";

                public static string Update = $"{ClassName}/{Actions.Update}";
                public static string GetAll = $"{ClassName}/{Actions.GetAll}";
                public static string GetById = $"{ClassName}/{Actions.GetById}";
                public static string Delete = $"{ClassName}/{Actions.Delete}";
                public static string DeleteGroup = $"{ClassName}/{Actions.DeleteGroup}";
                public static string Export = $"{ClassName}/{Actions.Export}";
                public static string Validate = $"{ClassName}/{Actions.Validate}";
            }
            public static class Cache
            {

                public static string[] Key(Guid Id) => new[] { GetAll, GetById(Id) };
                public static string GetAll => $"GetAll-{ClassName}";
                public static string GetById(Guid Id) => $"GetById-{ClassName}-{Id}";
            }
            public static class PageName
            {
                public static string Create = $"Create{ClassName}";
                public static string Update = $"Update{ClassName}";
                public static string GetAll = $"GetAll{ClassName}";

            }


        }
        public static class Nozzles
        {
            public static string ClassLegend = "Nozzle";
            public static string ClassName => MethodBase.GetCurrentMethod()!.DeclaringType!.Name;
            public static class EndPoint
            {
                public static string Create = $"{ClassName}/{Actions.Create}";

                public static string Update = $"{ClassName}/{Actions.Update}";
                public static string GetAll = $"{ClassName}/{Actions.GetAll}";
                public static string GetById = $"{ClassName}/{Actions.GetById}";
                public static string Delete = $"{ClassName}/{Actions.Delete}";
                public static string Export = $"{ClassName}/{Actions.Export}";
                public static string Validate = $"{ClassName}/{Actions.Validate}";
            }
            public static class Cache
            {

                public static string[] Key(Guid Id) => new[] { GetAll, GetById(Id) };
                public static string GetAll => $"GetAll-{ClassName}";
                public static string GetById(Guid Id) => $"GetById-{ClassName}-{Id}";
            }
            public static class PageName
            {
                public static string Create = $"Create{ClassName}";
                public static string Update = $"Update{ClassName}";
                public static string GetAll = $"GetAll{ClassName}";

            }


        }
        public static class ValveTemplates
        {
            public static string ClassLegend = "Valve Template";
            public static string ClassName => MethodBase.GetCurrentMethod()!.DeclaringType!.Name;
            public static class EndPoint
            {
                public static string Create = $"{ClassName}/{Actions.Create}";
                public static string DeleteGroup = $"{ClassName}/{Actions.DeleteGroup}";
                public static string Update = $"{ClassName}/{Actions.Update}";
                public static string GetAll = $"{ClassName}/{Actions.GetAll}";
                public static string GetById = $"{ClassName}/{Actions.GetById}";
                public static string Delete = $"{ClassName}/{Actions.Delete}";
                public static string Export = $"{ClassName}/{Actions.Export}";
                public static string Validate = $"{ClassName}/{Actions.Validate}";
            }
            public static class Cache
            {

                public static string[] Key(Guid Id) => new[] { GetAll, GetById(Id) };
                public static string GetAll => $"GetAll-{ClassName}";
                public static string GetById(Guid Id) => $"GetById-{ClassName}-{Id}";
            }
            public static class PageName
            {
                public static string Create = $"Create{ClassName}";
                public static string Update = $"Update{ClassName}";
                public static string GetAll = $"GetAll{ClassName}";

            }


        }
        public static class InstrumentTemplates
        {
            public static string ClassLegend = "Instrument Template";
            public static string ClassName => MethodBase.GetCurrentMethod()!.DeclaringType!.Name;
            public static class EndPoint
            {
                public static string Create = $"{ClassName}/{Actions.Create}";
                public static string DeleteGroup = $"{ClassName}/{Actions.DeleteGroup}";
                public static string Update = $"{ClassName}/{Actions.Update}";
                public static string GetAll = $"{ClassName}/{Actions.GetAll}";
                public static string GetById = $"{ClassName}/{Actions.GetById}";
                public static string Delete = $"{ClassName}/{Actions.Delete}";
                public static string Export = $"{ClassName}/{Actions.Export}";
                public static string Validate = $"{ClassName}/{Actions.Validate}";
            }
            public static class Cache
            {

                public static string[] Key(Guid Id) => new[] { GetAll, GetById(Id) };
                public static string GetAll => $"GetAll-{ClassName}";
                public static string GetById(Guid Id) => $"GetById-{ClassName}-{Id}";
            }
            public static class PageName
            {
                public static string Create = $"Create{ClassName}";
                public static string Update = $"Update{ClassName}";
                public static string GetAll = $"GetAll{ClassName}";

            }


        }
        public static class PipeTemplates
        {
            public static string ClassLegend = "Pipe Template";
            public static string ClassName => MethodBase.GetCurrentMethod()!.DeclaringType!.Name;
            public static class EndPoint
            {
                public static string Create = $"{ClassName}/{Actions.Create}";
                public static string DeleteGroup = $"{ClassName}/{Actions.DeleteGroup}";
                public static string Update = $"{ClassName}/{Actions.Update}";
                public static string GetAll = $"{ClassName}/{Actions.GetAll}";
                public static string GetById = $"{ClassName}/{Actions.GetById}";
                public static string Delete = $"{ClassName}/{Actions.Delete}";
                public static string Export = $"{ClassName}/{Actions.Export}";
                public static string Validate = $"{ClassName}/{Actions.Validate}";
            }
            public static class Cache
            {

                public static string[] Key(Guid Id) => new[] { GetAll, GetById(Id) };
                public static string GetAll => $"GetAll-{ClassName}";
                public static string GetById(Guid Id) => $"GetById-{ClassName}-{Id}";
            }
            public static class PageName
            {
                public static string Create = $"Create{ClassName}";
                public static string Update = $"Update{ClassName}";
                public static string GetAll = $"GetAll{ClassName}";

            }


        }
        public static class Projects
        {
            public static string ClassLegend = "Project";
            public static string ClassName => MethodBase.GetCurrentMethod()!.DeclaringType!.Name;
            public static class EndPoint
            {
                public static string Approve = $"{ClassName}/{Actions.Approve}";
                public static string UnApprove = $"{ClassName}/{Actions.Approve}Un";
                public static string CreateUpdate = $"{ClassName}/{Actions.CreateUpdate}";
                public static string UpdateState = $"{ClassName}/{Actions.Update}State";


                public static string GetAll = $"{ClassName}/{Actions.GetAll}";

                public static string GetById = $"{ClassName}/{Actions.GetById}";

                public static string Delete = $"{ClassName}/{Actions.Delete}";
                public static string ProjectCharter = $"{ClassName}/{Actions.Export}Charter";
                public static string ProjectPlann = $"{ClassName}/{Actions.Export}Plann";

                public static string Validate = $"{ClassName}/{Actions.Validate}";
                public static string ValidateProjectNumber = $"{ClassName}/{Actions.Validate}ProjectNumber";
                public static string DeleteGroup = $"{ClassName}/{Actions.DeleteGroup}";
            }
            public static class Cache
            {

                public static string[] Key(Guid Id) => new[] { GetAll, GetById(Id), GetCompleteById(Id) };
                public static string GetAll => $"GetAll-{ClassName}";
                public static string GetById(Guid Id) => $"GetById-{ClassName}-{Id}";
                public static string GetCompleteById(Guid Id) => $"GetCompleteById-{ClassName}-{Id}";

                public static string GetValidateById(Guid Id) => $"GetValidateById-{ClassName}-{Id}";
            }
            public static class PageName
            {
                public static string Create = $"Create{ClassName}";
                public static string Update = $"Update{ClassName}";
                public static string GetAll = $"GetAll{ClassName}";
                public static string Complete = $"Complete{ClassName}";
            }


        }
        public static class Apps
        {
            public static string ClassLegend = "App";
            public static string ClassName => MethodBase.GetCurrentMethod()!.DeclaringType!.Name;
            public static class EndPoint
            {
                public static string CreateUpdate = $"{ClassName}/{Actions.CreateUpdate}";

                public static string GetById = $"{ClassName}/{Actions.GetById}";



            }
            public static class Cache
            {

                public static string[] Key(Guid Id) => new[] { GetAll, GetById(Id), GetCompleteById(Id) };
                public static string GetAll => $"GetAll-{ClassName}";
                public static string GetById(Guid Id) => $"GetById-{ClassName}-{Id}";
                public static string GetCompleteById(Guid Id) => $"GetCompleteById-{ClassName}-{Id}";
            }
            public static class PageName
            {
                public static string Create = $"Create{ClassName}";
                public static string Update = $"Update{ClassName}";
                public static string GetAll = $"GetAll{ClassName}";
                public static string Complete = $"Complete{ClassName}";
            }


        }
        public static class Objectives
        {
            public static string ClassLegend = "Objectives";
            public static string ClassName => MethodBase.GetCurrentMethod()!.DeclaringType!.Name;
            public static class EndPoint
            {
                public static string DeleteGroup = $"{ClassName}/{Actions.DeleteGroup}";
                public static string CreateUpdate = $"{ClassName}/{Actions.CreateUpdate}";
                public static string UpdateUp = $"{ClassName}/{Actions.Update}Up";
                public static string UpdateDown = $"{ClassName}/{Actions.Update}Down";
                public static string GetAll = $"{ClassName}/{Actions.GetAll}";
                public static string GetById = $"{ClassName}/{Actions.GetById}";
                public static string Delete = $"{ClassName}/{Actions.Delete}";
                public static string Export = $"{ClassName}/{Actions.Export}";
                public static string Validate = $"{ClassName}/{Actions.Validate}";
            }
            public static class Cache
            {
                public static string[] Key(Guid Id, Guid ProjectId) => new[] { GetAll(ProjectId), GetById(Id) };
                public static string GetAll(Guid ProjectId) => $"GetAll-{ProjectId}{ClassName}";
                public static string GetById(Guid Id) => $"GetById-{ClassName}-{Id}";
            }
            public static class PageName
            {
                public static string Create = $"Create{ClassName}";
                public static string Update = $"Update{ClassName}";
                public static string GetAll = $"GetAll{ClassName}";

            }


        }
        public static class Assumptions
        {
            public static string ClassLegend = "Assumption";
            public static string ClassName => MethodBase.GetCurrentMethod()!.DeclaringType!.Name;
            public static class EndPoint
            {
                public static string DeleteGroup = $"{ClassName}/{Actions.DeleteGroup}";
                public static string CreateUpdate = $"{ClassName}/{Actions.CreateUpdate}";
                public static string UpdateUp = $"{ClassName}/{Actions.Update}Up";
                public static string UpdateDown = $"{ClassName}/{Actions.Update}Down";
                public static string GetAll = $"{ClassName}/{Actions.GetAll}";
                public static string GetById = $"{ClassName}/{Actions.GetById}";
                public static string Delete = $"{ClassName}/{Actions.Delete}";
                public static string Export = $"{ClassName}/{Actions.Export}";
                public static string Validate = $"{ClassName}/{Actions.Validate}";
            }
            public static class Cache
            {
                public static string[] Key(Guid Id, Guid ProjectId) => new[] { GetAll(ProjectId), GetById(Id) };
                public static string GetAll(Guid ProjectId) => $"GetAll-{ProjectId}{ClassName}";
                public static string GetById(Guid Id) => $"GetById-{ClassName}-{Id}";
            }
            public static class PageName
            {
                public static string Create = $"Create{ClassName}";
                public static string Update = $"Update{ClassName}";
                public static string GetAll = $"GetAll{ClassName}";

            }


        }
        public static class BackGrounds
        {
            public static string ClassLegend = "Background";
            public static string ClassName => MethodBase.GetCurrentMethod()!.DeclaringType!.Name;
            public static class EndPoint
            {
                public static string DeleteGroup = $"{ClassName}/{Actions.DeleteGroup}";
                public static string CreateUpdate = $"{ClassName}/{Actions.CreateUpdate}";
                public static string UpdateUp = $"{ClassName}/{Actions.Update}Up";
                public static string UpdateDown = $"{ClassName}/{Actions.Update}Down";
                public static string GetAll = $"{ClassName}/{Actions.GetAll}";
                public static string GetById = $"{ClassName}/{Actions.GetById}";
                public static string Delete = $"{ClassName}/{Actions.Delete}";
                public static string Export = $"{ClassName}/{Actions.Export}";
                public static string Validate = $"{ClassName}/{Actions.Validate}";
            }
            public static class Cache
            {
                public static string[] Key(Guid Id, Guid ProjectId) => new[] { GetAll(ProjectId), GetById(Id) };
                public static string GetAll(Guid ProjectId) => $"GetAll-{ProjectId}{ClassName}";
                public static string GetById(Guid Id) => $"GetById-{ClassName}-{Id}";
            }
            public static class PageName
            {
                public static string Create = $"Create{ClassName}";
                public static string Update = $"Update{ClassName}";
                public static string GetAll = $"GetAll{ClassName}";

            }


        }
        public static class Bennefits
        {
            public static string ClassLegend = "Bennefit";
            public static string ClassName => MethodBase.GetCurrentMethod()!.DeclaringType!.Name;
            public static class EndPoint
            {
                public static string DeleteGroup = $"{ClassName}/{Actions.DeleteGroup}";
                public static string CreateUpdate = $"{ClassName}/{Actions.CreateUpdate}";
                public static string UpdateUp = $"{ClassName}/{Actions.Update}Up";
                public static string UpdateDown = $"{ClassName}/{Actions.Update}Down";
                public static string GetAll = $"{ClassName}/{Actions.GetAll}";
                public static string GetById = $"{ClassName}/{Actions.GetById}";
                public static string Delete = $"{ClassName}/{Actions.Delete}";
                public static string Export = $"{ClassName}/{Actions.Export}";
                public static string Validate = $"{ClassName}/{Actions.Validate}";
            }
            public static class Cache
            {

                public static string[] Key(Guid Id, Guid ProjectId) => new[] { GetAll(ProjectId), GetById(Id) };
                public static string GetAll(Guid ProjectId) => $"GetAll-{ProjectId}{ClassName}";
                public static string GetById(Guid Id) => $"GetById-{ClassName}-{Id}";
            }
            public static class PageName
            {
                public static string Create = $"Create{ClassName}";
                public static string Update = $"Update{ClassName}";
                public static string GetAll = $"GetAll{ClassName}";

            }


        }
        public static class Deliverables
        {
            public static string ClassLegend = "Deliverables";
            public static string ClassName => MethodBase.GetCurrentMethod()!.DeclaringType!.Name;
            public static class EndPoint
            {
                public static string DeleteGroup = $"{ClassName}/{Actions.DeleteGroup}";
                public static string CreateUpdate = $"{ClassName}/{Actions.CreateUpdate}";
                public static string UpdateUp = $"{ClassName}/{Actions.Update}Up";
                public static string UpdateDown = $"{ClassName}/{Actions.Update}Down";

                public static string UpdateExpand = $"{ClassName}/{Actions.Update}Expand";

                public static string GetAll = $"{ClassName}/{Actions.GetAll}";
                public static string GetById = $"{ClassName}/{Actions.GetById}";
                public static string Delete = $"{ClassName}/{Actions.Delete}";
                public static string Export = $"{ClassName}/{Actions.Export}";
                public static string Validate = $"{ClassName}/{Actions.Validate}";
            }
            public static class Cache
            {

                public static string[] Key(Guid Id, Guid ProjectId) => new[] {
                    GetAll(ProjectId), GetById(Id),
                    GanttTasks.Cache.GetAll(ProjectId) };

                public static string GetAll(Guid ProjectId) => $"GetAll-{ProjectId}{ClassName}";
                public static string GetById(Guid Id) => $"GetById-{ClassName}-{Id}";
            }
            public static class PageName
            {
                public static string Create = $"Create{ClassName}";
                public static string Update = $"Update{ClassName}";
                public static string GetAll = $"GetAll{ClassName}";

            }


        }
        public static class Constrainsts
        {
            public static string ClassLegend = "Constraints";
            public static string ClassName => MethodBase.GetCurrentMethod()!.DeclaringType!.Name;
            public static class EndPoint
            {
                public static string DeleteGroup = $"{ClassName}/{Actions.DeleteGroup}";
                public static string CreateUpdate = $"{ClassName}/{Actions.CreateUpdate}";
                public static string UpdateUp = $"{ClassName}/{Actions.Update}Up";
                public static string UpdateDown = $"{ClassName}/{Actions.Update}Down";
                public static string GetAll = $"{ClassName}/{Actions.GetAll}";
                public static string GetById = $"{ClassName}/{Actions.GetById}";
                public static string Delete = $"{ClassName}/{Actions.Delete}";
                public static string Export = $"{ClassName}/{Actions.Export}";
                public static string Validate = $"{ClassName}/{Actions.Validate}";
            }
            public static class Cache
            {

                public static string[] Key(Guid Id, Guid ProjectId) => new[] { GetAll(ProjectId), GetById(Id) };
                public static string GetAll(Guid ProjectId) => $"GetAll-{ProjectId}{ClassName}";
                public static string GetById(Guid Id) => $"GetById-{ClassName}-{Id}";
            }
            public static class PageName
            {
                public static string Create = $"Create{ClassName}";
                public static string Update = $"Update{ClassName}";
                public static string GetAll = $"GetAll{ClassName}";

            }


        }
        public static class GanttTasks
        {
            public static string ClassLegend = "Gantt Task";
            public static string ClassName => MethodBase.GetCurrentMethod()!.DeclaringType!.Name;
            public static class EndPoint
            {
                public static string Create = $"{ClassName}/{Actions.Create}";
                public static string UpdateState = $"{ClassName}/{Actions.Update}State";
                public static string UpdateExpand = $"{ClassName}/{Actions.Update}Expand";
                public static string UpdateUp = $"{ClassName}/{Actions.Update}Up";
                public static string UpdateDown = $"{ClassName}/{Actions.Update}Down";
                public static string UpdateEDT = $"{ClassName}/{Actions.Update}EDT";
                public static string UpdateRight = $"{ClassName}/{Actions.Update}Right";
                public static string UpdateLeft = $"{ClassName}/{Actions.Update}Left";
                public static string Update = $"{ClassName}/{Actions.Update}";
                public static string GetAll = $"{ClassName}/{Actions.GetAll}";
                public static string GetAllProject = $"{ClassName}/{Actions.GetAll}Project";
                public static string GetById = $"{ClassName}/{Actions.GetById}";
                public static string Delete = $"{ClassName}/{Actions.Delete}";
                public static string Export = $"{ClassName}/{Actions.Export}";
                public static string Validate = $"{ClassName}/{Actions.Validate}";
            }
            public static class Cache
            {

                public static string[] Key(Guid Id, Guid projectid) => new[] { GetAll(projectid), GetById(Id) };
                public static string GetAll(Guid projectid) => $"GetAll-{ClassName}-{projectid}";
                public static string GetById(Guid Id) => $"GetById-{ClassName}-{Id}";
            }
            public static class PageName
            {
                public static string Create = $"Create{ClassName}";
                public static string Update = $"Update{ClassName}";
                public static string GetAll = $"GetAll{ClassName}";

            }


        }
        public static class KnownRisks
        {
            public static string ClassLegend = "Known Risk";
            public static string ClassName => MethodBase.GetCurrentMethod()!.DeclaringType!.Name;
            public static class EndPoint
            {
                public static string DeleteGroup = $"{ClassName}/{Actions.DeleteGroup}";
                public static string CreateUpdate = $"{ClassName}/{Actions.CreateUpdate}";
                public static string UpdateUp = $"{ClassName}/{Actions.Update}Up";
                public static string UpdateDown = $"{ClassName}/{Actions.Update}Down";
                public static string GetAll = $"{ClassName}/{Actions.GetAll}";
                public static string GetById = $"{ClassName}/{Actions.GetById}";
                public static string Delete = $"{ClassName}/{Actions.Delete}";
                public static string Export = $"{ClassName}/{Actions.Export}";
                public static string Validate = $"{ClassName}/{Actions.Validate}";
            }
            public static class Cache
            {
                public static string[] Key(Guid Id, Guid ProjectId) => new[] { GetAll(ProjectId), GetById(Id) };
                public static string GetAll(Guid ProjectId) => $"GetAll-{ProjectId}{ClassName}";
                public static string GetById(Guid Id) => $"GetById-{ClassName}-{Id}";
            }
            public static class PageName
            {
                public static string Create = $"Create{ClassName}";
                public static string Update = $"Update{ClassName}";
                public static string GetAll = $"GetAll{ClassName}";

            }


        }
        public static class LearnedLessons
        {
            public static string ClassLegend = "Learned Lesson";
            public static string ClassName => MethodBase.GetCurrentMethod()!.DeclaringType!.Name;
            public static class EndPoint
            {
                public static string DeleteGroup = $"{ClassName}/{Actions.DeleteGroup}";
                public static string CreateUpdate = $"{ClassName}/{Actions.CreateUpdate}";
                public static string UpdateUp = $"{ClassName}/{Actions.Update}Up";
                public static string UpdateDown = $"{ClassName}/{Actions.Update}Down";
                public static string GetAll = $"{ClassName}/{Actions.GetAll}";
                public static string GetById = $"{ClassName}/{Actions.GetById}";
                public static string Delete = $"{ClassName}/{Actions.Delete}";
                public static string Export = $"{ClassName}/{Actions.Export}";
                public static string Validate = $"{ClassName}/{Actions.Validate}";
            }
            public static class Cache
            {
                public static string[] Key(Guid Id, Guid ProjectId) => new[] { GetAll(ProjectId), GetById(Id) };
                public static string GetAll(Guid ProjectId) => $"GetAll-{ProjectId}{ClassName}";
                public static string GetById(Guid Id) => $"GetById-{ClassName}-{Id}";
            }
            public static class PageName
            {
                public static string Create = $"Create{ClassName}";
                public static string Update = $"Update{ClassName}";
                public static string GetAll = $"GetAll{ClassName}";

            }


        }
        public static class Qualitys
        {
            public static string ClassLegend = "Quality";
            public static string ClassName => MethodBase.GetCurrentMethod()!.DeclaringType!.Name;
            public static class EndPoint
            {
                public static string DeleteGroup = $"{ClassName}/{Actions.DeleteGroup}";
                public static string CreateUpdate = $"{ClassName}/{Actions.CreateUpdate}";
                public static string UpdateUp = $"{ClassName}/{Actions.Update}Up";
                public static string UpdateDown = $"{ClassName}/{Actions.Update}Down";
                public static string GetAll = $"{ClassName}/{Actions.GetAll}";
                public static string GetById = $"{ClassName}/{Actions.GetById}";
                public static string Delete = $"{ClassName}/{Actions.Delete}";
                public static string Export = $"{ClassName}/{Actions.Export}";
                public static string Validate = $"{ClassName}/{Actions.Validate}";
            }
            public static class Cache
            {
                public static string[] Key(Guid Id, Guid ProjectId) => new[] { GetAll(ProjectId), GetById(Id) };
                public static string GetAll(Guid ProjectId) => $"GetAll-{ProjectId}{ClassName}";
                public static string GetById(Guid Id) => $"GetById-{ClassName}-{Id}";
            }
            public static class PageName
            {
                public static string Create = $"Create{ClassName}";
                public static string Update = $"Update{ClassName}";
                public static string GetAll = $"GetAll{ClassName}";

            }


        }
        public static class Acquisitions
        {
            public static string ClassLegend = "Acquisition";
            public static string ClassName => MethodBase.GetCurrentMethod()!.DeclaringType!.Name;
            public static class EndPoint
            {
                public static string DeleteGroup = $"{ClassName}/{Actions.DeleteGroup}";
                public static string CreateUpdate = $"{ClassName}/{Actions.CreateUpdate}";
                public static string UpdateUp = $"{ClassName}/{Actions.Update}Up";
                public static string UpdateDown = $"{ClassName}/{Actions.Update}Down";
                public static string GetAll = $"{ClassName}/{Actions.GetAll}";
                public static string GetById = $"{ClassName}/{Actions.GetById}";
                public static string Delete = $"{ClassName}/{Actions.Delete}";
                public static string Export = $"{ClassName}/{Actions.Export}";
                public static string Validate = $"{ClassName}/{Actions.Validate}";
            }
            public static class Cache
            {
                public static string[] Key(Guid Id, Guid ProjectId) => new[] { GetAll(ProjectId), GetById(Id) };
                public static string GetAll(Guid ProjectId) => $"GetAll-{ProjectId}{ClassName}";
                public static string GetById(Guid Id) => $"GetById-{ClassName}-{Id}";
            }
            public static class PageName
            {
                public static string Create = $"Create{ClassName}";
                public static string Update = $"Update{ClassName}";
                public static string GetAll = $"GetAll{ClassName}";

            }


        }
        public static class Communications
        {
            public static string ClassLegend = "Communication";
            public static string ClassName => MethodBase.GetCurrentMethod()!.DeclaringType!.Name;
            public static class EndPoint
            {
                public static string DeleteGroup = $"{ClassName}/{Actions.DeleteGroup}";
                public static string CreateUpdate = $"{ClassName}/{Actions.CreateUpdate}";
                public static string UpdateUp = $"{ClassName}/{Actions.Update}Up";
                public static string UpdateDown = $"{ClassName}/{Actions.Update}Down";
                public static string GetAll = $"{ClassName}/{Actions.GetAll}";
                public static string GetById = $"{ClassName}/{Actions.GetById}";
                public static string Delete = $"{ClassName}/{Actions.Delete}";
                public static string Export = $"{ClassName}/{Actions.Export}";
                public static string Validate = $"{ClassName}/{Actions.Validate}";
            }
            public static class Cache
            {
                public static string[] Key(Guid Id, Guid ProjectId) => new[] { GetAll(ProjectId), GetById(Id) };
                public static string GetAll(Guid ProjectId) => $"GetAll-{ProjectId}{ClassName}";
                public static string GetById(Guid Id) => $"GetById-{ClassName}-{Id}";
            }
            public static class PageName
            {
                public static string Create = $"Create{ClassName}";
                public static string Update = $"Update{ClassName}";
                public static string GetAll = $"GetAll{ClassName}";

            }


        }
        public static class Resources
        {
            public static string ClassLegend = "Resource";
            public static string ClassName => MethodBase.GetCurrentMethod()!.DeclaringType!.Name;
            public static class EndPoint
            {
                public static string DeleteGroup = $"{ClassName}/{Actions.DeleteGroup}";
                public static string CreateUpdate = $"{ClassName}/{Actions.CreateUpdate}";
                public static string UpdateUp = $"{ClassName}/{Actions.Update}Up";
                public static string UpdateDown = $"{ClassName}/{Actions.Update}Down";
                public static string GetAll = $"{ClassName}/{Actions.GetAll}";
                public static string GetById = $"{ClassName}/{Actions.GetById}";
                public static string Delete = $"{ClassName}/{Actions.Delete}";
                public static string Export = $"{ClassName}/{Actions.Export}";
                public static string Validate = $"{ClassName}/{Actions.Validate}";
            }
            public static class Cache
            {
                public static string[] Key(Guid Id, Guid ProjectId) => new[] { GetAll(ProjectId), GetById(Id) };
                public static string GetAll(Guid ProjectId) => $"GetAll-{ProjectId}{ClassName}";
                public static string GetById(Guid Id) => $"GetById-{ClassName}-{Id}";
            }
            public static class PageName
            {
                public static string Create = $"Create{ClassName}";
                public static string Update = $"Update{ClassName}";
                public static string GetAll = $"GetAll{ClassName}";

            }


        }
        public static class Meetings
        {
            public static string ClassLegend = "Meeting";
            public static string ClassName => MethodBase.GetCurrentMethod()!.DeclaringType!.Name;
            public static class EndPoint
            {
                public static string DeleteGroup = $"{ClassName}/{Actions.DeleteGroup}";
                public static string UpdateUp = $"{ClassName}/{Actions.Update}Up";
                public static string UpdateDown = $"{ClassName}/{Actions.Update}Down";
                public static string Create = $"{ClassName}/{Actions.Create}";
                public static string UpdateState = $"{ClassName}/{Actions.Update}State";
                public static string Update = $"{ClassName}/{Actions.Update}";
                public static string GetAll = $"{ClassName}/{Actions.GetAll}";
                public static string GetById = $"{ClassName}/{Actions.GetById}";
                public static string Delete = $"{ClassName}/{Actions.Delete}";
                public static string Export = $"{ClassName}/{Actions.Export}";
                public static string Validate = $"{ClassName}/{Actions.Validate}";
            }
            public static class Cache
            {

                public static string[] Key(Guid Id) => new[] { GetAll, GetById(Id) };
                public static string GetAll => $"GetAll-{ClassName}";
                public static string GetById(Guid Id) => $"GetById-{ClassName}-{Id}";
            }
            public static class PageName
            {
                public static string Create = $"Create{ClassName}";
                public static string Update = $"Update{ClassName}";
                public static string GetAll = $"GetAll{ClassName}";

            }


        }
        public static class MeetingAttendants
        {
            public static string ClassLegend = "Meeting Attendant";
            public static string ClassName => MethodBase.GetCurrentMethod()!.DeclaringType!.Name;
            public static class EndPoint
            {
                public static string DeleteGroup = $"{ClassName}/{Actions.DeleteGroup}";
                public static string Create = $"{ClassName}/{Actions.Create}";

                public static string Update = $"{ClassName}/{Actions.Update}";
                public static string GetAll = $"{ClassName}/{Actions.GetAll}";
                public static string GetById = $"{ClassName}/{Actions.GetById}";
                public static string Delete = $"{ClassName}/{Actions.Delete}";
                public static string Export = $"{ClassName}/{Actions.Export}";
                public static string Validate = $"{ClassName}/{Actions.Validate}";
            }
            public static class Cache
            {

                public static string[] Key(Guid Id) => new[] { GetAll, GetById(Id) };
                public static string GetAll => $"GetAll-{ClassName}";
                public static string GetById(Guid Id) => $"GetById-{ClassName}-{Id}";
            }
            public static class PageName
            {
                public static string Create = $"Create{ClassName}";
                public static string Update = $"Update{ClassName}";
                public static string GetAll = $"GetAll{ClassName}";

            }


        }
        public static class MeetingAgreements
        {
            public static string ClassLegend = "Meeting Attendant Agreement";
            public static string ClassName => MethodBase.GetCurrentMethod()!.DeclaringType!.Name;
            public static class EndPoint
            {
                public static string Create = $"{ClassName}/{Actions.Create}";
                public static string DeleteGroup = $"{ClassName}/{Actions.DeleteGroup}";
                public static string Update = $"{ClassName}/{Actions.Update}";
                public static string GetAll = $"{ClassName}/{Actions.GetAll}";
                public static string GetById = $"{ClassName}/{Actions.GetById}";
                public static string Delete = $"{ClassName}/{Actions.Delete}";
                public static string Export = $"{ClassName}/{Actions.Export}";
                public static string Validate = $"{ClassName}/{Actions.Validate}";
            }
            public static class Cache
            {

                public static string[] Key(Guid Id) => new[] { GetAll, GetById(Id) };
                public static string GetAll => $"GetAll-{ClassName}";
                public static string GetById(Guid Id) => $"GetById-{ClassName}-{Id}";
            }
            public static class PageName
            {
                public static string Create = $"Create{ClassName}";
                public static string Update = $"Update{ClassName}";
                public static string GetAll = $"GetAll{ClassName}";

            }


        }
        public static class ExpertJudgements
        {
            public static string ClassLegend = "Experts Judgement";
            public static string ClassName => MethodBase.GetCurrentMethod()!.DeclaringType!.Name;
            public static class EndPoint
            {
                public static string DeleteGroup = $"{ClassName}/{Actions.DeleteGroup}";
                public static string CreateUpdate = $"{ClassName}/{Actions.CreateUpdate}";
                public static string UpdateUp = $"{ClassName}/{Actions.Update}Up";
                public static string UpdateDown = $"{ClassName}/{Actions.Update}Down";
                public static string GetAll = $"{ClassName}/{Actions.GetAll}";
                public static string GetById = $"{ClassName}/{Actions.GetById}";
                public static string Delete = $"{ClassName}/{Actions.Delete}";
                public static string Export = $"{ClassName}/{Actions.Export}";
                public static string Validate = $"{ClassName}/{Actions.Validate}";
            }
            public static class Cache
            {
                public static string[] Key(Guid Id, Guid ProjectId) => new[] { GetAll(ProjectId), GetById(Id) };
                public static string GetAll(Guid ProjectId) => $"GetAll-{ProjectId}{ClassName}";
                public static string GetById(Guid Id) => $"GetById-{ClassName}-{Id}";
            }
            public static class PageName
            {
                public static string Create = $"Create{ClassName}";
                public static string Update = $"Update{ClassName}";
                public static string GetAll = $"GetAll{ClassName}";

            }


        }
        public static class AcceptanceCriterias
        {
            public static string ClassLegend = "Acceptance Criterias";
            public static string ClassName => MethodBase.GetCurrentMethod()!.DeclaringType!.Name;
            public static class EndPoint
            {
                public static string CreateUpdate = $"{ClassName}/{Actions.CreateUpdate}";
                public static string UpdateUp = $"{ClassName}/{Actions.Update}Up";
                public static string UpdateDown = $"{ClassName}/{Actions.Update}Down";

                public static string GetAll = $"{ClassName}/{Actions.GetAll}";
                public static string GetById = $"{ClassName}/{Actions.GetById}";
                public static string Delete = $"{ClassName}/{Actions.Delete}";
                public static string DeleteGroup = $"{ClassName}/{Actions.DeleteGroup}";
                public static string Export = $"{ClassName}/{Actions.Export}";
                public static string Validate = $"{ClassName}/{Actions.Validate}";
            }
            public static class Cache
            {
                public static string[] Key(Guid Id, Guid ProjectId) => new[] { GetAll(ProjectId), GetById(Id) };
                public static string GetAll(Guid ProjectId) => $"GetAll-{ProjectId}{ClassName}";
                public static string GetById(Guid Id) => $"GetById-{ClassName}-{Id}";
            }
            public static class PageName
            {
                public static string Create = $"Create{ClassName}";
                public static string Update = $"Update{ClassName}";
                public static string GetAll = $"GetAll{ClassName}";

            }


        }
        public static class Requirements
        {
            public static string ClassLegend = "Requirement";
            public static string ClassName => MethodBase.GetCurrentMethod()!.DeclaringType!.Name;
            public static class EndPoint
            {
                public static string DeleteGroup = $"{ClassName}/{Actions.DeleteGroup}";
                public static string CreateUpdate = $"{ClassName}/{Actions.CreateUpdate}";
                public static string UpdateUp = $"{ClassName}/{Actions.Update}Up";
                public static string UpdateDown = $"{ClassName}/{Actions.Update}Down";
                public static string GetAll = $"{ClassName}/{Actions.GetAll}";
                public static string GetById = $"{ClassName}/{Actions.GetById}";
                public static string Delete = $"{ClassName}/{Actions.Delete}";
                public static string Export = $"{ClassName}/{Actions.Export}";
                public static string Validate = $"{ClassName}/{Actions.Validate}";
            }
            public static class Cache
            {
                public static string[] Key(Guid Id, Guid ProjectId) => new[] { GetAll(ProjectId), GetById(Id) };
                public static string GetAll(Guid ProjectId) => $"GetAll-{ProjectId}{ClassName}";
                public static string GetById(Guid Id) => $"GetById-{ClassName}-{Id}";
            }
            public static class PageName
            {
                public static string Create = $"Create{ClassName}";
                public static string Update = $"Update{ClassName}";
                public static string GetAll = $"GetAll{ClassName}";

            }


        }
        public static class Scopes
        {
            public static string ClassLegend = "Scope";
            public static string ClassName => MethodBase.GetCurrentMethod()!.DeclaringType!.Name;
            public static class EndPoint
            {
                public static string DeleteGroup = $"{ClassName}/{Actions.DeleteGroup}";
                public static string CreateUpdate = $"{ClassName}/{Actions.CreateUpdate}";
                public static string UpdateUp = $"{ClassName}/{Actions.Update}Up";
                public static string UpdateDown = $"{ClassName}/{Actions.Update}Down";
                public static string GetAll = $"{ClassName}/{Actions.GetAll}";
                public static string GetById = $"{ClassName}/{Actions.GetById}";
                public static string Delete = $"{ClassName}/{Actions.Delete}";
                public static string Export = $"{ClassName}/{Actions.Export}";
                public static string Validate = $"{ClassName}/{Actions.Validate}";
            }
            public static class Cache
            {

                public static string[] Key(Guid Id, Guid ProjectId) => new[] { GetAll(ProjectId), GetById(Id) };
                public static string GetAll(Guid ProjectId) => $"GetAll-{ProjectId}{ClassName}";
                public static string GetById(Guid Id) => $"GetById-{ClassName}-{Id}";
            }
            public static class PageName
            {
                public static string Create = $"Create{ClassName}";
                public static string Update = $"Update{ClassName}";
                public static string GetAll = $"GetAll{ClassName}";

            }


        }

        public static class StakeHolderInsideProjects
        {
            public static string ClassLegend = "Stakeholder Inside Project";
            public static string ClassName => "StakeProject";
            public static class EndPoint
            {
                public static string DeleteGroup = $"{ClassName}/{Actions.DeleteGroup}";
                public static string Create = $"{ClassName}/{Actions.Create}";
                public static string Update = $"{ClassName}/{Actions.Update}";
                public static string UpdateUp = $"{ClassName}/{Actions.Update}Up";
                public static string UpdateDown = $"{ClassName}/{Actions.Update}Down";

                public static string GetAll = $"{ClassName}/{Actions.GetAll}";
                public static string GetById = $"{ClassName}/{Actions.GetById}";
                public static string Delete = $"{ClassName}/{Actions.Delete}";
                public static string Export = $"{ClassName}/{Actions.Export}";
                public static string Validate = $"{ClassName}/{Actions.Validate}";
            }
            public static class Cache
            {
                public static string[] Key(Guid Id, Guid ProjectId) => new[] { GetAll(ProjectId), GetById(Id) };
                public static string GetAll(Guid ProjectId) => $"GetAll-{ProjectId}{ClassName}";
                public static string GetById(Guid Id) => $"GetById-{ClassName}-{Id}";
            }
            public static class PageName
            {
                public static string Create = $"Create{ClassName}";
                public static string Update = $"Update{ClassName}";
                public static string GetAll = $"GetAll{ClassName}";

            }


        }
        public static class BudgetItems
        {
            public static string ClassLegend = "Budget Items";
            public static string ClassName => MethodBase.GetCurrentMethod()!.DeclaringType!.Name;
            public static class EndPoint
            {
                public static string Create = $"{ClassName}/{Actions.Create}";

                public static string Update = $"{ClassName}/{Actions.Update}";
                public static string GetAll = $"{ClassName}/{Actions.GetAll}";
                public static string GetAllWithPurchaseorder = $"{ClassName}/{Actions.GetAll}WithPurchaseorder";
                public static string GetAllByDeliverable = $"{ClassName}/{Actions.GetAll}ByDeliverable";
                public static string GetById = $"{ClassName}/{Actions.GetById}";
                public static string GetWithPurchaseorderById = $"{ClassName}/{Actions.GetById}WithPurchaseorder";
                public static string Delete = $"{ClassName}/{Actions.Delete}";
                public static string DeleteGroup = $"{ClassName}/{Actions.DeleteGroup}";
                public static string MoveGroup = $"{ClassName}/{Actions.Delete}MoveGroup";
                public static string CopyGroup = $"{ClassName}/{Actions.Delete}CopyGroup";
                public static string Export = $"{ClassName}/{Actions.Export}";
                public static string Validate = $"{ClassName}/{Actions.Validate}";
            }
            public static class Cache
            {

                public static string[] Key(Guid Id, Guid projectId, Guid? deliverableId)
                {
                    var deliverablecache = deliverableId.HasValue ? GetAllByDeliverable(deliverableId.Value) : string.Empty;
                    var result = new[] {
                        GetAll(projectId),
                        GetById(Id),
                        deliverablecache,
                        GetAllWithPurchaseOrder(projectId),
                        GetByIdWithPurchaseOrder(Id),
                        StaticClass.Projects.Cache.GetById(projectId),
                    };
                    return result;
                }
                public static string GetAll(Guid projectId) => $"GetAll-{ClassName}-{projectId}";
                public static string GetAllWithPurchaseOrder(Guid projectId) => $"GetAllWithPurchaseOrder-{ClassName}-{projectId}";
                public static string GetAllByDeliverable(Guid deliverableid) => $"GetAllByDeliverable-{ClassName}-{deliverableid}";
                public static string GetById(Guid Id) => $"GetById-{ClassName}-{Id}";
                public static string GetByIdWithPurchaseOrder(Guid Id) => $"GetByIdWithPurchaseOrder-{ClassName}-{Id}";
            }
            public static class PageName
            {
                public static string Create = $"Create{ClassName}";
                public static string Update = $"Update{ClassName}";
                public static string GetAll = $"GetAll{ClassName}";

            }


        }
        public static class Alterations
        {
            public static string ClassLegend = "Alteration";
            public static string ClassName => MethodBase.GetCurrentMethod()!.DeclaringType!.Name;
            public static class EndPoint
            {
                public static string CreateUpdate = $"{ClassName}/{Actions.CreateUpdate}";

                public static string DeleteGroup = $"{ClassName}/{Actions.DeleteGroup}";

                public static string GetAll = $"{ClassName}/{Actions.GetAll}";
                public static string GetById = $"{ClassName}/{Actions.GetById}";
                public static string Delete = $"{ClassName}/{Actions.Delete}";
                public static string Export = $"{ClassName}/{Actions.Export}";
                public static string Validate = $"{ClassName}/{Actions.Validate}";
            }
            public static class Cache
            {
                public static string[] Key(Guid Id, Guid ProjectId) => new[] { GetAll(ProjectId), GetById(Id) };
                public static string GetAll(Guid ProjectId) => $"GetAll-{ClassName}-{ProjectId}";
                public static string GetById(Guid Id) => $"GetById-{ClassName}-{Id}";
            }
            public static class PageName
            {
                public static string Create = $"Create{ClassName}";
                public static string Update = $"Update{ClassName}";
                public static string GetAll = $"GetAll{ClassName}";

            }


        }
        public static class Foundations
        {
            public static string ClassLegend = "Foundation";
            public static string ClassName => MethodBase.GetCurrentMethod()!.DeclaringType!.Name;
            public static class EndPoint
            {
                public static string CreateUpdate = $"{ClassName}/{Actions.CreateUpdate}";
                public static string DeleteGroup = $"{ClassName}/{Actions.DeleteGroup}";

                public static string GetAll = $"{ClassName}/{Actions.GetAll}";
                public static string GetById = $"{ClassName}/{Actions.GetById}";
                public static string Delete = $"{ClassName}/{Actions.Delete}";
                public static string Export = $"{ClassName}/{Actions.Export}";
                public static string Validate = $"{ClassName}/{Actions.Validate}";
            }
            public static class Cache
            {
                public static string[] Key(Guid Id, Guid ProjectId) => new[] { GetAll(ProjectId), GetById(Id) };
                public static string GetAll(Guid ProjectId) => $"GetAll-{ClassName}-{ProjectId}";
                public static string GetById(Guid Id) => $"GetById-{ClassName}-{Id}";
            }
            public static class PageName
            {
                public static string Create = $"Create{ClassName}";
                public static string Update = $"Update{ClassName}";
                public static string GetAll = $"GetAll{ClassName}";

            }


        }
        public static class Structurals
        {
            public static string ClassLegend = "Structurals";
            public static string ClassName => MethodBase.GetCurrentMethod()!.DeclaringType!.Name;
            public static class EndPoint
            {
                public static string CreateUpdate = $"{ClassName}/{Actions.CreateUpdate}";
                public static string DeleteGroup = $"{ClassName}/{Actions.DeleteGroup}";

                public static string GetAll = $"{ClassName}/{Actions.GetAll}";
                public static string GetById = $"{ClassName}/{Actions.GetById}";
                public static string Delete = $"{ClassName}/{Actions.Delete}";
                public static string Export = $"{ClassName}/{Actions.Export}";
                public static string Validate = $"{ClassName}/{Actions.Validate}";
            }
            public static class Cache
            {
                public static string[] Key(Guid Id, Guid ProjectId) => new[] { GetAll(ProjectId), GetById(Id) };
                public static string GetAll(Guid ProjectId) => $"GetAll-{ClassName}-{ProjectId}";
                public static string GetById(Guid Id) => $"GetById-{ClassName}-{Id}";
            }
            public static class PageName
            {
                public static string Create = $"Create{ClassName}";
                public static string Update = $"Update{ClassName}";
                public static string GetAll = $"GetAll{ClassName}";

            }


        }
        public static class Equipments
        {
            public static string ClassLegend = "Equipments";
            public static string ClassName => MethodBase.GetCurrentMethod()!.DeclaringType!.Name;
            public static class EndPoint
            {
                public static string CreateUpdate = $"{ClassName}/{Actions.CreateUpdate}";
                public static string DeleteGroup = $"{ClassName}/{Actions.DeleteGroup}";

                public static string GetAll = $"{ClassName}/{Actions.GetAll}";
                public static string GetById = $"{ClassName}/{Actions.GetById}";
                public static string Delete = $"{ClassName}/{Actions.Delete}";
                public static string Export = $"{ClassName}/{Actions.Export}";
                public static string Validate = $"{ClassName}/{Actions.Validate}";
                public static string ValidateTag = $"{ClassName}/{Actions.Validate}Tag";
            }
            public static class Cache
            {

                public static string[] Key(Guid Id, Guid ProjectId) => new[] { GetAll(ProjectId), GetById(Id) };
                public static string GetAll(Guid ProjectId) => $"GetAll-{ClassName}-{ProjectId}";
                public static string GetById(Guid Id) => $"GetById-{ClassName}-{Id}";
            }
            public static class PageName
            {
                public static string Create = $"Create{ClassName}";
                public static string Update = $"Update{ClassName}";
                public static string GetAll = $"GetAll{ClassName}";

            }


        }
        public static class Valves
        {
            public static string ClassLegend = "Valve";
            public static string ClassName => MethodBase.GetCurrentMethod()!.DeclaringType!.Name;
            public static class EndPoint
            {
                public static string CreateUpdate = $"{ClassName}/{Actions.CreateUpdate}";
                public static string DeleteGroup = $"{ClassName}/{Actions.DeleteGroup}";

                public static string GetAll = $"{ClassName}/{Actions.GetAll}";
                public static string GetById = $"{ClassName}/{Actions.GetById}";
                public static string Delete = $"{ClassName}/{Actions.Delete}";
                public static string Export = $"{ClassName}/{Actions.Export}";
                public static string Validate = $"{ClassName}/{Actions.Validate}";
                public static string ValidateTag = $"{ClassName}/{Actions.Validate}Tag";
            }
            public static class Cache
            {
                public static string[] Key(Guid Id, Guid ProjectId) => new[] { GetAll(ProjectId), GetById(Id) };
                public static string GetAll(Guid ProjectId) => $"GetAll-{ClassName}-{ProjectId}";
                public static string GetById(Guid Id) => $"GetById-{ClassName}-{Id}";
            }
            public static class PageName
            {
                public static string Create = $"Create{ClassName}";
                public static string Update = $"Update{ClassName}";
                public static string GetAll = $"GetAll{ClassName}";

            }


        }
        public static class Electricals
        {
            public static string ClassLegend = "Electricals";
            public static string ClassName => MethodBase.GetCurrentMethod()!.DeclaringType!.Name;
            public static class EndPoint
            {
                public static string CreateUpdate = $"{ClassName}/{Actions.CreateUpdate}";
                public static string DeleteGroup = $"{ClassName}/{Actions.DeleteGroup}";

                public static string GetAll = $"{ClassName}/{Actions.GetAll}";
                public static string GetById = $"{ClassName}/{Actions.GetById}";
                public static string Delete = $"{ClassName}/{Actions.Delete}";
                public static string Export = $"{ClassName}/{Actions.Export}";
                public static string Validate = $"{ClassName}/{Actions.Validate}";
            }
            public static class Cache
            {

                public static string[] Key(Guid Id, Guid ProjectId) => new[] { GetAll(ProjectId), GetById(Id) };
                public static string GetAll(Guid ProjectId) => $"GetAll-{ClassName}-{ProjectId}";
                public static string GetById(Guid Id) => $"GetById-{ClassName}-{Id}";
            }
            public static class PageName
            {
                public static string Create = $"Create{ClassName}";
                public static string Update = $"Update{ClassName}";
                public static string GetAll = $"GetAll{ClassName}";

            }


        }
        public static class Pipes
        {
            public static string ClassLegend = "Pipes";
            public static string ClassName => MethodBase.GetCurrentMethod()!.DeclaringType!.Name;
            public static class EndPoint
            {
                public static string CreateUpdate = $"{ClassName}/{Actions.CreateUpdate}";
                public static string DeleteGroup = $"{ClassName}/{Actions.DeleteGroup}";

                public static string GetAll = $"{ClassName}/{Actions.GetAll}";
                public static string GetById = $"{ClassName}/{Actions.GetById}";
                public static string Delete = $"{ClassName}/{Actions.Delete}";
                public static string Export = $"{ClassName}/{Actions.Export}";
                public static string Validate = $"{ClassName}/{Actions.Validate}";
                public static string ValidateTag = $"{ClassName}/{Actions.Validate}Tag";
            }
            public static class Cache
            {
                public static string[] Key(Guid Id, Guid ProjectId) => new[] { GetAll(ProjectId), GetById(Id) };
                public static string GetAll(Guid ProjectId) => $"GetAll-{ClassName}-{ProjectId}";
                public static string GetById(Guid Id) => $"GetById-{ClassName}-{Id}";
            }
            public static class PageName
            {
                public static string Create = $"Create{ClassName}";
                public static string Update = $"Update{ClassName}";
                public static string GetAll = $"GetAll{ClassName}";

            }


        }
        public static class Instruments
        {
            public static string ClassLegend = "Instrument";
            public static string ClassName => MethodBase.GetCurrentMethod()!.DeclaringType!.Name;
            public static class EndPoint
            {
                public static string CreateUpdate = $"{ClassName}/{Actions.CreateUpdate}";
                public static string DeleteGroup = $"{ClassName}/{Actions.DeleteGroup}";

                public static string GetAll = $"{ClassName}/{Actions.GetAll}";
                public static string GetById = $"{ClassName}/{Actions.GetById}";
                public static string Delete = $"{ClassName}/{Actions.Delete}";
                public static string Export = $"{ClassName}/{Actions.Export}";
                public static string Validate = $"{ClassName}/{Actions.Validate}";
                public static string ValidateTag = $"{ClassName}/{Actions.Validate}Tag";
            }
            public static class Cache
            {
                public static string[] Key(Guid Id, Guid ProjectId) => new[] { GetAll(ProjectId), GetById(Id) };
                public static string GetAll(Guid ProjectId) => $"GetAll-{ClassName}-{ProjectId}";
                public static string GetById(Guid Id) => $"GetById-{ClassName}-{Id}";
            }
            public static class PageName
            {
                public static string Create = $"Create{ClassName}";
                public static string Update = $"Update{ClassName}";
                public static string GetAll = $"GetAll{ClassName}";

            }


        }
        public static class EHSs
        {
            public static string ClassLegend = "EHS";
            public static string ClassName => MethodBase.GetCurrentMethod()!.DeclaringType!.Name;
            public static class EndPoint
            {
                public static string CreateUpdate = $"{ClassName}/{Actions.CreateUpdate}";
                public static string DeleteGroup = $"{ClassName}/{Actions.DeleteGroup}";

                public static string GetAll = $"{ClassName}/{Actions.GetAll}";
                public static string GetById = $"{ClassName}/{Actions.GetById}";
                public static string Delete = $"{ClassName}/{Actions.Delete}";
                public static string Export = $"{ClassName}/{Actions.Export}";
                public static string Validate = $"{ClassName}/{Actions.Validate}";
            }
            public static class Cache
            {
                public static string[] Key(Guid Id, Guid ProjectId) => new[] { GetAll(ProjectId), GetById(Id) };
                public static string GetAll(Guid ProjectId) => $"GetAll-{ClassName}-{ProjectId}";
                public static string GetById(Guid Id) => $"GetById-{ClassName}-{Id}";
            }
            public static class PageName
            {
                public static string Create = $"Create{ClassName}";
                public static string Update = $"Update{ClassName}";
                public static string GetAll = $"GetAll{ClassName}";

            }


        }
        public static class Paintings
        {
            public static string ClassLegend = "Paintings";
            public static string ClassName => MethodBase.GetCurrentMethod()!.DeclaringType!.Name;
            public static class EndPoint
            {
                public static string CreateUpdate = $"{ClassName}/{Actions.CreateUpdate}";
                public static string DeleteGroup = $"{ClassName}/{Actions.DeleteGroup}";

                public static string GetAll = $"{ClassName}/{Actions.GetAll}";
                public static string GetById = $"{ClassName}/{Actions.GetById}";
                public static string Delete = $"{ClassName}/{Actions.Delete}";
                public static string Export = $"{ClassName}/{Actions.Export}";
                public static string Validate = $"{ClassName}/{Actions.Validate}";
            }
            public static class Cache
            {
                public static string[] Key(Guid Id, Guid ProjectId) => new[] { GetAll(ProjectId), GetById(Id) };
                public static string GetAll(Guid ProjectId) => $"GetAll-{ClassName}-{ProjectId}";
                public static string GetById(Guid Id) => $"GetById-{ClassName}-{Id}";
            }
            public static class PageName
            {
                public static string Create = $"Create{ClassName}";
                public static string Update = $"Update{ClassName}";
                public static string GetAll = $"GetAll{ClassName}";

            }


        }
        public static class Taxs
        {
            public static string ClassLegend = "Taxes";
            public static string ClassName => MethodBase.GetCurrentMethod()!.DeclaringType!.Name;
            public static class EndPoint
            {
                public static string CreateUpdate = $"{ClassName}/{Actions.CreateUpdate}";
                public static string DeleteGroup = $"{ClassName}/{Actions.DeleteGroup}";

                public static string GetBudgetItemsToApplyTaxById = $"{ClassName}/GetBudgetItemsToApplyTaxById";

                public static string GetAll = $"{ClassName}/{Actions.GetAll}";
                public static string GetById = $"{ClassName}/{Actions.GetById}";
                public static string Delete = $"{ClassName}/{Actions.Delete}";
                public static string Export = $"{ClassName}/{Actions.Export}";
                public static string Validate = $"{ClassName}/{Actions.Validate}";
            }
            public static class Cache
            {
                public static string[] Key(Guid Id, Guid ProjectId) => new[] { GetAll(ProjectId), GetById(Id) };
                public static string GetAll(Guid ProjectId) => $"GetAll-{ClassName}-{ProjectId}";
                public static string GetById(Guid Id) => $"GetById-{ClassName}-{Id}";
            }
            public static class PageName
            {
                public static string Create = $"Create{ClassName}";
                public static string Update = $"Update{ClassName}";
                public static string GetAll = $"GetAll{ClassName}";

            }


        }
        public static class Testings
        {
            public static string ClassLegend = "Testing";
            public static string ClassName => MethodBase.GetCurrentMethod()!.DeclaringType!.Name;
            public static class EndPoint
            {
                public static string CreateUpdate = $"{ClassName}/{Actions.CreateUpdate}";
                public static string DeleteGroup = $"{ClassName}/{Actions.DeleteGroup}";

                public static string GetAll = $"{ClassName}/{Actions.GetAll}";
                public static string GetById = $"{ClassName}/{Actions.GetById}";
                public static string Delete = $"{ClassName}/{Actions.Delete}";
                public static string Export = $"{ClassName}/{Actions.Export}";
                public static string Validate = $"{ClassName}/{Actions.Validate}";
            }
            public static class Cache
            {
                public static string[] Key(Guid Id, Guid ProjectId) => new[] { GetAll(ProjectId), GetById(Id) };
                public static string GetAll(Guid ProjectId) => $"GetAll-{ClassName}-{ProjectId}";
                public static string GetById(Guid Id) => $"GetById-{ClassName}-{Id}";
            }
            public static class PageName
            {
                public static string Create = $"Create{ClassName}";
                public static string Update = $"Update{ClassName}";
                public static string GetAll = $"GetAll{ClassName}";

            }


        }
        public static class EngineeringDesigns
        {
            public static string ClassLegend = "Engineering Design";
            public static string ClassName => MethodBase.GetCurrentMethod()!.DeclaringType!.Name;
            public static class EndPoint
            {
                public static string CreateUpdate = $"{ClassName}/{Actions.CreateUpdate}";
                public static string DeleteGroup = $"{ClassName}/{Actions.DeleteGroup}";

                public static string GetAll = $"{ClassName}/{Actions.GetAll}";
                public static string GetById = $"{ClassName}/{Actions.GetById}";
                public static string Delete = $"{ClassName}/{Actions.Delete}";
                public static string Export = $"{ClassName}/{Actions.Export}";
                public static string Validate = $"{ClassName}/{Actions.Validate}";
            }
            public static class Cache
            {
                public static string[] Key(Guid Id, Guid ProjectId) => new[] { GetAll(ProjectId), GetById(Id) };
                public static string GetAll(Guid ProjectId) => $"GetAll-{ClassName}-{ProjectId}";
                public static string GetById(Guid Id) => $"GetById-{ClassName}-{Id}";
            }
            public static class PageName
            {
                public static string Create = $"Create{ClassName}";
                public static string Update = $"Update{ClassName}";
                public static string GetAll = $"GetAll{ClassName}";

            }


        }
        public static class Contingencys
        {
            public static string ClassLegend = "Contingency";
            public static string ClassName => MethodBase.GetCurrentMethod()!.DeclaringType!.Name;
            public static class EndPoint
            {
                public static string CreateUpdate = $"{ClassName}/{Actions.CreateUpdate}";
                public static string DeleteGroup = $"{ClassName}/{Actions.DeleteGroup}";

                public static string GetAll = $"{ClassName}/{Actions.GetAll}";
                public static string GetById = $"{ClassName}/{Actions.GetById}";
                public static string Delete = $"{ClassName}/{Actions.Delete}";
                public static string Export = $"{ClassName}/{Actions.Export}";
                public static string Validate = $"{ClassName}/{Actions.Validate}";
            }
            public static class Cache
            {
                public static string[] Key(Guid Id, Guid ProjectId) => new[] { GetAll(ProjectId), GetById(Id) };
                public static string GetAll(Guid ProjectId) => $"GetAll-{ClassName}-{ProjectId}";
                public static string GetById(Guid Id) => $"GetById-{ClassName}-{Id}";
            }
            public static class PageName
            {
                public static string Create = $"Create{ClassName}";
                public static string Update = $"Update{ClassName}";
                public static string GetAll = $"GetAll{ClassName}";

            }


        }

        public static class PurchaseOrders
        {
            public static string ClassLegend = "Purchase Orders";
            public static string ClassName => MethodBase.GetCurrentMethod()!.DeclaringType!.Name;
            public static class EndPoint
            {
                public static string Create = $"{ClassName}/{Actions.Create}";
                public static string EditCreated = $"{ClassName}/{Actions.Create}Edit";

                public static string Approve = $"{ClassName}/{Actions.Approve}";
                public static string EditApproved = $"{ClassName}/{Actions.Approve}Edit";
                public static string Close = $"{ClassName}/{Actions.Close}";
                public static string GetAll = $"{ClassName}/{Actions.GetAll}";

                public static string GetById = $"{ClassName}/{Actions.GetById}";
                public static string Delete = $"{ClassName}/{Actions.Delete}";
                public static string Export = $"{ClassName}/{Actions.Export}";
                public static string DeleteGroup = $"{ClassName}/{Actions.DeleteGroup}";
                public static string ValidateNumber = $"{ClassName}/{Actions.Validate}Number";
                public static string ValidateName = $"{ClassName}/{Actions.Validate}Name";
                public static string ValidatePR = $"{ClassName}/{Actions.Validate}PR";
            }
            public static class Cache
            {
                public static string[] Key(Guid Id, Guid ProjectId) => new[] {GetAllClosed,GetAllApproved, GetAllCreated,GetAll, GetById(Id),
                    BudgetItems.Cache.GetAllWithPurchaseOrder(ProjectId) };
                public static string[] KeyCreated(Guid Id, Guid ProjectId) => new[] { GetAllCreated,GetAll, GetById(Id),
                    BudgetItems.Cache.GetAllWithPurchaseOrder(ProjectId) };
                public static string[] KeyApproved(Guid Id, Guid ProjectId) => new[] { GetAllApproved,GetAllCreated, GetAll,GetById(Id),
                    BudgetItems.Cache.GetAllWithPurchaseOrder(ProjectId) };
                public static string[] KeyEditApproved(Guid Id, Guid ProjectId) => new[] { GetAllApproved,GetAll, GetById(Id),
                    BudgetItems.Cache.GetAllWithPurchaseOrder(ProjectId) };
                public static string[] KeyClosed(Guid Id, Guid ProjectId) => new[] { GetAllApproved,GetAllClosed, GetAll,GetById(Id),
                    BudgetItems.Cache.GetAllWithPurchaseOrder(ProjectId) };

                public static string GetAllApproved => $"GetAll-{ClassName}-Approved";
                public static string GetAllCreated => $"GetAll-{ClassName}-Created";
                public static string GetAllClosed => $"GetAll-{ClassName}-Closed";
                public static string GetAll => $"GetAll-{ClassName}";

                public static string GetById(Guid Id) => $"GetById-{ClassName}-{Id}";
            }
            public static class PageName
            {
                public static string Create = $"Create{ClassName}";
                public static string Close = $"Close{ClassName}";
                public static string Approve = $"Approve{ClassName}";
                public static string GetAll = $"GetAll{ClassName}";

            }


        }
    }
}
