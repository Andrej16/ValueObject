namespace Domain.Common
{
    public abstract class ValueObject : 
        EqualityComparer<ValueObject>, 
        IComparable, 
        IComparable<ValueObject>
    {
        private int? _cachedHashCode;

        protected abstract IEnumerable<object> GetEqualityComponents();

        protected static bool EqualOperator(ValueObject? left, ValueObject? right)
        {
            if (left is null && right is null)
                return true;
            else if (left is null || right is null)
                return false;

            bool isEquals = left.GetEqualityComponents().SequenceEqual(right.GetEqualityComponents());

            return isEquals;
        }

        protected static bool NotEqualOperator(ValueObject? left, ValueObject? right)
        {
            return !EqualOperator(left, right);
        }

        public override bool Equals(ValueObject? left, ValueObject? right)
        {
            return EqualOperator(left, right);
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || obj.GetType() != GetType())
            {
                return false;
            }

            ValueObject other = (ValueObject)obj;

            return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
        }

        public override int GetHashCode(ValueObject obj)
        {
            return obj.GetHashCode();
        }

        public override int GetHashCode()
        {
            if (!_cachedHashCode.HasValue)
            {
                _cachedHashCode = GetEqualityComponents()
                    .Select(x => x != null ? x.GetHashCode() : 0)
                    .Aggregate((x, y) => x ^ y);
            }

            return _cachedHashCode.Value;
        }

        public ValueObject? GetCopy()
        {
            return MemberwiseClone() as ValueObject;
        }

        public static bool operator ==(ValueObject one, ValueObject two)
        {
            return EqualOperator(one, two);
        }

        public static bool operator !=(ValueObject one, ValueObject two)
        {
            return NotEqualOperator(one, two);
        }

        public virtual int CompareTo(object? obj)
        {
            if (obj is null)
            {
                throw new ArgumentNullException(nameof(obj));
            }

            ValueObject obj2 = (ValueObject)obj;
            object[] array = GetEqualityComponents().ToArray();
            object[] array2 = obj2.GetEqualityComponents().ToArray();
            for (int i = 0; i < array.Length; i++)
            {
                int num = CompareComponents(array[i], array2[i]);
                if (num != 0)
                {
                    return num;
                }
            }

            return 0;
        }

        public virtual int CompareTo(ValueObject? other)
        {
            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            return CompareTo((object)other);
        }

        private int CompareComponents(object? object1, object? object2)
        {
            if (object1 == null && object2 == null)
            {
                return 0;
            }

            if (object1 == null)
            {
                return -1;
            }

            if (object2 == null)
            {
                return 1;
            }

            IComparable? comparable = object1 as IComparable;
            if (comparable != null)
            {
                IComparable? comparable2 = object2 as IComparable;
                if (comparable2 != null)
                {
                    return comparable.CompareTo(comparable2);
                }
            }

            if (!object1.Equals(object2))
            {
                return -1;
            }

            return 0;
        }
    }
}
