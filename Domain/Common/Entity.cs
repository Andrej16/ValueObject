﻿namespace Domain.Common
{
    public abstract class Entity : EqualityComparer<Entity>
    {
        public virtual int Id { get; protected set; }

        protected static bool EqualOperator(Entity? left, Entity? right)
        {
            if (left is null && right is null)
                return true;
            else if (left is null || right is null)
                return false;

            return left.Equals(right);
        }

        public override bool Equals(object? obj)
        {
            if (obj is not Entity other)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            if (GetType() != other.GetType())
                return false;

            if (Id == 0 || other.Id == 0)
                return false;

            return Id == other.Id;
        }

        public override bool Equals(Entity? left, Entity? right)
        {
            return EqualOperator(left, right);
        }

        public static bool operator ==(Entity? a, Entity? b)
        {
            if (a is null && b is null)
                return true;

            if (a is null || b is null)
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(Entity? a, Entity? b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return GetHashSource(this).GetHashCode();
        }

        public override int GetHashCode(Entity entity)
        {
            return GetHashSource(entity).GetHashCode();
        }

        private string GetHashSource(Entity entity)
        {
            return entity.GetType().ToString() + Id;
        }
    }
}
