
namespace DineConnect.OrderManagementService.Domain.Common
{
    public abstract class ValueObject : IEquatable<ValueObject>
    {
        /// <summary>
        /// Provide the components (or properties) of the value object that are relevant for equality comparisons.
        /// </summary>
        public abstract IEnumerable<object> GetEqualityComponents();

        public override bool Equals(object? obj)
        {
            if (obj is null || obj.GetType() != GetType())
            {
                return false;
            }

            var valueObject = (ValueObject)obj;
            return GetEqualityComponents()
                    .SequenceEqual(valueObject.GetEqualityComponents());
        }

        public static bool operator ==(ValueObject left, ValueObject right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(ValueObject left, ValueObject right)
        {
            return !Equals(left, right);
        }

        public override int GetHashCode()
        {
            return GetEqualityComponents()
                    .Select(x => x?.GetHashCode() ?? 0)
                    .Aggregate((x, y) => x ^ y);
        }

        public bool Equals(ValueObject? other)
        {
            return Equals((object?)other);
        }
    }
}