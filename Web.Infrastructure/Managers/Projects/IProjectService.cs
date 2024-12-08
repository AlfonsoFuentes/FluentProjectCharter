using Shared.Models.Projects.Exports;
using Shared.Models.Projects.Records;
using Shared.Models.Projects.Reponses;

namespace Web.Infrastructure.Managers.Projects
{
    public interface IProjectService : IManager
    {
        Task<IResult<ProjectResponse>> GetById(Guid Id);
        Task<IResult<ProjectResponseList>> GetAll();
    
        Task<IResult<FileResult>> Export(ExportFileType fileType, List<ProjectResponse> query);
    
    }
    public class ProjectService : IProjectService
    {
        IHttpClientService http;

        public ProjectService(IHttpClientService http)
        {
            this.http = http;
        }



        public async Task<IResult<ProjectResponseList>> GetAll()
        {
            var result = await http.PostAsJsonAsync(StaticClass.Projects.EndPoint.GetAll, new ProjectGetAll());
            return await result.ToResult<ProjectResponseList>();
        }

        public async Task<IResult<FileResult>> Export(ExportFileType fileType, List<ProjectResponse> query)
        {

            var result = await http.PostAsJsonAsync(StaticClass.Projects.EndPoint.Export, new ProjectGetAllExport(fileType, query));
            return await result.ToResult<FileResult>();
        }
       
        public async Task<IResult<ProjectResponse>> GetById(Guid Id)
        {
            GetProjectByIdRequest request = new()
            {
                Id = Id
            };
            var result = await http.PostAsJsonAsync(StaticClass.Projects.EndPoint.GetById, request);
            return await result.ToResult<ProjectResponse>();
        }
    }
  
}
