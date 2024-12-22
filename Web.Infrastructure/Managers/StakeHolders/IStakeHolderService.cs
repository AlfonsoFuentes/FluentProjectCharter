using Shared.Models.Projects.Records;
using Shared.Models.StakeHolders.Exports;
using Shared.Models.StakeHolders.Records;
using Shared.Models.StakeHolders.Requests;
using Shared.Models.StakeHolders.Responses;

namespace Web.Infrastructure.Managers.StakeHolders
{
    public interface IStakeHolderService : IManager
    {
        Task<IResult<StakeHolderResponse>> Create(CreateStakeHolderRequest request);
        Task<IResult<StakeHolderResponse>> GetById(Guid Id);

        Task<IResult<StakeHolderResponseList>> GetAll();
        Task<IResult<FileResult>> Export(ExportFileType fileType, List<StakeHolderResponse> query);

    }
    public class StakeHolderService : IStakeHolderService
    {
        IHttpClientService http;

        public StakeHolderService(IHttpClientService http)
        {
            this.http = http;
        }




        public async Task<IResult<StakeHolderResponseList>> GetAll()
        {
            var result = await http.PostAsJsonAsync(StaticClass.StakeHolders.EndPoint.GetAll, new StakeHolderGetAll());
            return await result.ToResult<StakeHolderResponseList>();
        }

        public async Task<IResult<FileResult>> Export(ExportFileType fileType, List<StakeHolderResponse> query)
        {

            var result = await http.PostAsJsonAsync(StaticClass.StakeHolders.EndPoint.Export, new StakeHolderGetAllExport(fileType, query));
            return await result.ToResult<FileResult>();
        }

        public async Task<IResult<StakeHolderResponse>> GetById(Guid Id)
        {
            GetProjectByIdRequest request = new()
            {
                Id = Id
            };
            var result = await http.PostAsJsonAsync(StaticClass.StakeHolders.EndPoint.GetById, request);
            return await result.ToResult<StakeHolderResponse>();
        }

        public async Task<IResult<StakeHolderResponse>> Create(CreateStakeHolderRequest request)
        {
            var result = await http.PostAsJsonAsync(StaticClass.StakeHolders.EndPoint.Create, request);
            return await result.ToResult<StakeHolderResponse>();
        }
    }
}
