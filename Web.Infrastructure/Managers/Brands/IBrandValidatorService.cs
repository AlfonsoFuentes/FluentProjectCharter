using Shared.Models.Brands.Validators;

namespace Web.Infrastructure.Managers.Brands
{
    public interface IBrandValidatorService : IManager
    {
        Task<bool> ValidateName(ValidateBrandName request);



    }
    public class BrandValidatorService : IBrandValidatorService
    {
        IHttpClientService Http;


        public BrandValidatorService(IHttpClientService http)
        {
            Http = http;
        }
        public async Task<bool> ValidateName(ValidateBrandName request)
        {
            var httpresult = await Http.PostAsJsonAsync(StaticClass.Brand.EndPoint.Validate, request);

            return await httpresult.ToObject<bool>();
        }


    }

}
