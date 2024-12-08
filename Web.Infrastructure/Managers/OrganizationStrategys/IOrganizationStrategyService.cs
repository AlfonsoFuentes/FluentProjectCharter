using Shared.Models.OrganizationStrategies.Exports;
using Shared.Models.OrganizationStrategies.Records;
using Shared.Models.OrganizationStrategies.Requests;
using Shared.Models.OrganizationStrategies.Responses;
using Shared.Models.Projects.Records;

namespace Web.Infrastructure.Managers.OrganizationStrategys
{
    public interface IOrganizationStrategyService : IManager
    {
        Task<IResult<OrganizationStrategyResponse>> Create(CreateOrganizationStrategyRequest request);
        Task<IResult<OrganizationStrategyResponse>> GetById(Guid Id);
        Task<IResult<OrganizationStrategyResponseList>> GetAll();

        Task<IResult<FileResult>> Export(ExportFileType fileType, List<OrganizationStrategyResponse> query);

    }
    public class OrganizationStrategyService : IOrganizationStrategyService
    {
        IHttpClientService http;

        public OrganizationStrategyService(IHttpClientService http)
        {
            this.http = http;
        }



        public async Task<IResult<OrganizationStrategyResponseList>> GetAll()
        {
            var result = await http.PostAsJsonAsync(StaticClass.OrganizationStrategys.EndPoint.GetAll, new OrganizationStrategyGetAll());
            return await result.ToResult<OrganizationStrategyResponseList>();
        }

        public async Task<IResult<FileResult>> Export(ExportFileType fileType, List<OrganizationStrategyResponse> query)
        {

            var result = await http.PostAsJsonAsync(StaticClass.OrganizationStrategys.EndPoint.Export, new OrganizationStrategyGetAllExport(fileType, query));
            return await result.ToResult<FileResult>();
        }

        public async Task<IResult<OrganizationStrategyResponse>> GetById(Guid Id)
        {
            GetProjectByIdRequest request = new()
            {
                Id = Id
            };
            var result = await http.PostAsJsonAsync(StaticClass.OrganizationStrategys.EndPoint.GetById, request);
            return await result.ToResult<OrganizationStrategyResponse>();
        }

        public async Task<IResult<OrganizationStrategyResponse>> Create(CreateOrganizationStrategyRequest request)
        {
            var result = await http.PostAsJsonAsync(StaticClass.OrganizationStrategys.EndPoint.Create, request);
            return await result.ToResult<OrganizationStrategyResponse>();
        }
    }
}
