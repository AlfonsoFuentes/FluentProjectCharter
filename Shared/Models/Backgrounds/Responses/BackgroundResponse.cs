using Shared.Models.FileResults.Generics.Reponses;

namespace Shared.Models.Backgrounds.Responses
{
    public class BackGroundResponse : BaseResponse,IResponseAll
    {
        public Guid ProjectId { get; set; }
   
      
    }
}
