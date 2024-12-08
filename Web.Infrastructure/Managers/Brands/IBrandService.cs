

namespace Web.Infrastructure.Managers.Brands
{
    public interface IBrandService : IManager
    {
        Task<IResult<BrandResponse>> Create(CreateBrandRequest request);

        Task<IResult> Delete(BrandResponse request);
        Task<IResult<BrandResponseList>> GetAll();
        Task<IResult<UpdateBrandRequest>> GetToUpdateById(Guid BrandId);
        Task<IResult<BrandResponse>> Update(UpdateBrandRequest request);
        Task<IResult<FileResult>> Export(ExportFileType fileType, List<BrandResponse> query);

    }
    public class BrandService : IBrandService
    {
        IHttpClientService http;

        public BrandService(IHttpClientService http)
        {
            this.http = http;
        }

        public async Task<IResult<BrandResponse>> Update(UpdateBrandRequest request)
        {
            var result = await http.PostAsJsonAsync(StaticClass.Brand.EndPoint.Update, request);
            return await result.ToResult<BrandResponse>();
        }

        public async Task<IResult<BrandResponse>> Create(CreateBrandRequest request)
        {
            var result = await http.PostAsJsonAsync(StaticClass.Brand.EndPoint.Create, request);
            return await result.ToResult<BrandResponse>();
        }



        public async Task<IResult<BrandResponseList>> GetAll()
        {
            var result = await http.PostAsJsonAsync(StaticClass.Brand.EndPoint.GetAll, new BrandGetAll());
            return await result.ToResult<BrandResponseList>();
        }

        public async Task<IResult<UpdateBrandRequest>> GetToUpdateById(Guid Id)
        {

            var result = await http.PostAsJsonAsync(StaticClass.Brand.EndPoint.GetById, new BrandId(Id));
            return await result.ToResult<UpdateBrandRequest>();
        }

        public async Task<IResult> Delete(BrandResponse request)
        {
            var result = await http.PostAsJsonAsync(StaticClass.Brand.EndPoint.Delete, request);
            return await result.ToResult();
        }

        public async Task<IResult<FileResult>> Export(ExportFileType fileType, List<BrandResponse> query)
        {

            var result = await http.PostAsJsonAsync(StaticClass.Brand.EndPoint.Export, new BrandGetAllExport(fileType, query));
            return await result.ToResult<FileResult>();
        }
    }
}
