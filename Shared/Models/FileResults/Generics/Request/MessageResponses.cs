using Shared.StaticClasses;

namespace Shared.Models.FileResults.Generics.Request
{
    public abstract class ValidateMessageResponse
    {
        public abstract string Legend { get; }
        public abstract string ClassName { get; }
        public string Succesfully => StaticClass.ResponseMessages.ReponseSuccesfullyMessageCreated(Legend, ClassName);
        public string Fail => StaticClass.ResponseMessages.ReponseFailMessageCreated(Legend, ClassName);


    }
    public abstract class CreateMessageResponse
    {
        public abstract string Legend { get; }
        public abstract string ClassName { get; }
        public string Succesfully => StaticClass.ResponseMessages.ReponseSuccesfullyMessageCreated(Legend, ClassName);
        public string Fail => StaticClass.ResponseMessages.ReponseFailMessageCreated(Legend, ClassName);


    }
    public abstract class UpdateMessageResponse
    {
        public abstract string Legend { get; }
        public abstract string ClassName { get; }
        public string Succesfully => StaticClass.ResponseMessages.ReponseSuccesfullyMessageUpdated(Legend, ClassName);
        public string Fail => StaticClass.ResponseMessages.ReponseFailMessageUpdate(Legend, ClassName);
        public string NotFound => StaticClass.ResponseMessages.ReponseNotFound(ClassName);

    }
    public abstract class DeleteMessageResponse
    {
        public abstract string Legend { get; }
        public abstract string ClassName { get; }
        public string Succesfully => StaticClass.ResponseMessages.ReponseSuccesfullyMessageDelete(Legend, ClassName);
        public string Fail => StaticClass.ResponseMessages.ReponseFailMessageDelete(Legend, ClassName);
        public string NotFound => StaticClass.ResponseMessages.ReponseNotFound(ClassName);

    }
    public abstract class GetByIdMessageResponse
    {
        public abstract string ClassName { get; }
        public string NotFound => StaticClass.ResponseMessages.ReponseNotFound(ClassName);

    }
}
