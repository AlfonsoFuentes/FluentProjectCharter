using System.Reflection;

namespace Shared.StaticClasses
{
    public static class StaticClass
    {
        public static class Actions
        {

            public static string Create = "Create";
            public static string Update = "Update";
            public static string GetAll = "GetAll";
            public static string ToSearch = "ToSearch";
            public static string Delete = "Delete";
            public static string GetById = "GetById";
            public static string Export = "Export";
            public static string Validate = $"Validate";

        }
        public static class ResponseMessages
        {
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
        public static class Temporarys
        {
            public static string ClassLegend = "Temporarys";
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
            public static string ClassLegend = "Equipment Template";
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
        public static class PipingTemplates
        {
            public static string ClassLegend = "Piping Template";
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
        public static class Projects
        {
            public static string ClassLegend = "Project";
            public static string ClassName => MethodBase.GetCurrentMethod()!.DeclaringType!.Name;
            public static class EndPoint
            {
                public static string Create = $"{ClassName}/{Actions.Create}";
                public static string UpdateState = $"{ClassName}/{Actions.Update}State";
                public static string Update = $"{ClassName}/{Actions.Update}";
                public static string GetAll = $"{ClassName}/{Actions.GetAll}";
                public static string GetById = $"{ClassName}/{Actions.GetById}";
                public static string Delete = $"{ClassName}/{Actions.Delete}";
                public static string Export = $"{ClassName}/{Actions.Export}";
                //public static string Export = "ExportProject";
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
                public static string Complete = $"Complete{ClassName}";
            }


        }
        public static class AppStates
        {
            public static string ClassLegend = "AppState";
            public static string ClassName => MethodBase.GetCurrentMethod()!.DeclaringType!.Name;
            public static class EndPoint
            {
                public static string Update = $"{ClassName}/{Actions.Update}";
                public static string UpdateActiveProject = $"{ClassName}/{Actions.Update}ActiveProject";
                public static string UpdateProjectTab = $"{ClassName}/{Actions.Update}ProjectTab";
                public static string UpdateProjectCase = $"{ClassName}/{Actions.Update}ProjectCase";
                public static string UpdateProjectMeeting = $"{ClassName}/{Actions.Update}ProjectMeeting";
                public static string UpdateCaseTab = $"{ClassName}/{Actions.Update}CaseTab";
                public static string UpdateMeetingTab = $"{ClassName}/{Actions.Update}MeetingTab";
                public static string UpdateCaseScope = $"{ClassName}/{Actions.Update}CaseScope";
                public static string UpdateScopeTab = $"{ClassName}/{Actions.Update}ScopeTab";
                public static string UpdateScopeDeliverable = $"{ClassName}/{Actions.Update}ScopeDeliverable";
                public static string UpdateDeliverableTab = $"{ClassName}/{Actions.Update}DeliverableTab";
                public static string Create = $"{ClassName}/{Actions.Create}";
                public static string GetAll = $"{ClassName}/{Actions.GetAll}";
                public static string Delete = $"{ClassName}/{Actions.Delete}";
                public static string Export = $"{ClassName}/{Actions.Export}";
                public static string Validate = $"{ClassName}/{Actions.Validate}";
                public static string GetById = $"{ClassName}/{Actions.GetById}";
                public static string GetByIdInitial = $"{ClassName}/{Actions.GetById}Initial";

            }
            public static class Cache
            {

                public static List<string> Key(Guid Id) => new() {
                    GetAll,
                    GetById(Id)};
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
        public static class Cases
        {
            public static string ClassLegend = "Business Case";
            public static string ClassName => MethodBase.GetCurrentMethod()!.DeclaringType!.Name;
            public static class EndPoint
            {
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
        public static class HighLevelRequirements
        {
            public static string ClassLegend = "High Level Requirment";
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

                public static List<string> Key(Guid Id) => new() {
                    GetAll,
                    GetById(Id)};
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
        public static class Assumptions
        {
            public static string ClassLegend = "Assumption";
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

        public static class BackGrounds
        {
            public static string ClassLegend = "BackGround";
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
        public static class Bennefits
        {
            public static string ClassLegend = "Bennefit";
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
        public static class Constrainsts
        {
            public static string ClassLegend = "Constraints";
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
        public static class DecissionCriterias
        {
            public static string ClassLegend = "Decission Criteria";
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
        public static class Deliverables
        {
            public static string ClassLegend = "Deliverable";
            public static string ClassName => MethodBase.GetCurrentMethod()!.DeclaringType!.Name;
            public static class EndPoint
            {
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
        public static class SubDeliverables
        {
            public static string ClassLegend = "Sub Deliverables";
            public static string ClassName => MethodBase.GetCurrentMethod()!.DeclaringType!.Name;
            public static class EndPoint
            {
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
        public static class DeliverableRisks
        {
            public static string ClassLegend = "Deliverable Risk";
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
        public static class KnownRisks
        {
            public static string ClassLegend = "Known Risk";
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
        public static class Meetings
        {
            public static string ClassLegend = "Meeting";
            public static string ClassName => MethodBase.GetCurrentMethod()!.DeclaringType!.Name;
            public static class EndPoint
            {
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
            public static string ClassLegend = "Meeting Attendant Suggestion";
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
        public static class ExpertJudgements
        {
            public static string ClassLegend = "Experts Judgement";
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
        public static class OrganizationStrategys
        {
            public static string ClassLegend = "Organtization Strategy";
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
        public static class AcceptanceCriterias
        {
            public static string ClassLegend = "Acceptance Criterias";
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
        public static class Requirements
        {
            public static string ClassLegend = "Requirement";
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
        public static class Scopes
        {
            public static string ClassLegend = "Scope";
            public static string ClassName => MethodBase.GetCurrentMethod()!.DeclaringType!.Name;
            public static class EndPoint
            {
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
        public static class StakeHolders
        {
            public static string ClassLegend = "Stakeholder";
            public static string ClassName => MethodBase.GetCurrentMethod()!.DeclaringType!.Name;
            public static class EndPoint
            {
                public static string Create = $"{ClassName}/{Actions.Create}";
                public static string CreateInsideProject = $"{ClassName}/{Actions.Create}InsideProject";

                public static string Update = $"{ClassName}/{Actions.Update}";
                public static string UpdateInsideProject = $"{ClassName}/{Actions.Update}InsideProject";
                public static string GetAll = $"{ClassName}/{Actions.GetAll}";
                public static string GetById = $"{ClassName}/{Actions.GetById}";
                public static string GetByIdInsideProject = $"{ClassName}/{Actions.GetById}InsideProject";

                public static string Delete = $"{ClassName}/{Actions.Delete}";
                public static string DeleteInsideProject = $"{ClassName}/{Actions.Delete}InsideProject";
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
        public static class SucessfullFactors
        {
            public static string ClassLegend = "Succesfull factor";
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
        public static class BudgetItems
        {
            public static string ClassLegend = "Electricals";
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
        public static class Alterations
        {
            public static string ClassLegend = "Electricals";
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
        public static class Foundations
        {
            public static string ClassLegend = "Foundation";
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
        public static class Structurals
        {
            public static string ClassLegend = "Structurals";
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
        public static class Equipments
        {
            public static string ClassLegend = "Equipments";
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
                public static string ValidateTag = $"{ClassName}/{Actions.Validate}Tag";
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
        public static class Valves
        {
            public static string ClassLegend = "Valve";
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
                public static string ValidateTag = $"{ClassName}/{Actions.Validate}Tag";
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
        public static class Electricals
        {
            public static string ClassLegend = "Electricals";
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
        public static class Pipings
        {
            public static string ClassLegend = "Piping";
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
        public static class Instruments
        {
            public static string ClassLegend = "Instrument";
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
        public static class EHSs
        {
            public static string ClassLegend = "EHS";
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
        public static class Paintings
        {
            public static string ClassLegend = "Paintings";
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
        public static class Taxs
        {
            public static string ClassLegend = "Taxes";
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
        public static class Testings
        {
            public static string ClassLegend = "Testing";
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
        public static class EngineeringDesigns
        {
            public static string ClassLegend = "Engineering";
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
        public static class Contingencys
        {
            public static string ClassLegend = "Contingency";
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
      
    }
}
