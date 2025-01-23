using Shared.Models.FileResults.Generics.Reponses;

namespace Shared.Models.BaseResponses
{
    public class BaseResponse : IResponse
    {
        public string Name { get; set; } = string.Empty;
        public Guid Id { get; set; }
        public bool IsNodeOpen { get; set; }
        protected static bool EqualOperator(BaseResponse left, BaseResponse right)
        {
            if (ReferenceEquals(left, null) ^ ReferenceEquals(right, null))
            {
                return false;
            }
            return ReferenceEquals(left, right) || left!.Equals(right!);
        }

        protected static bool NotEqualOperator(BaseResponse left, BaseResponse right)
        {
            return !EqualOperator(left, right);
        }

        protected virtual IEnumerable<object> GetEqualityComponents()
        {
            yield return Id;

        }

        public override bool Equals(object? obj)
        {
            if (obj == null || obj.GetType() != GetType())
            {
                return false;
            }

            var other = (BaseResponse)obj;

            return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
        }

        public override int GetHashCode()
        {
            return GetEqualityComponents()
                .Select(x => x != null ? x.GetHashCode() : 0)
                .Aggregate((x, y) => x ^ y);
        }


    }
}
