using System.Collections;

namespace Domain.Common
{
    public class EntityEnumerable<T> : IEnumerable<T> where T : Entity
    {
        private readonly T[] _entities;

        public EntityEnumerable(params T[] entities)
        {
            _entities = entities;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new EntityEnumerator<T>(_entities);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
