using System.Collections;

namespace Domain.Common
{
    public class EntityEnumerator<T> : IEnumerator<T> where T : Entity
    {
        private readonly T[] _entities;
        private int _position = -1;

        public EntityEnumerator(T[] entities)
        {
            _entities = entities;
        }

        public T Current
        {
            get
            {
                try
                {
                    return _entities[_position];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }

        private object Current1 => Current;

        object IEnumerator.Current => Current1;

        public bool MoveNext()
        {
            _position++;
            return _position < _entities.Length;
        }

        public void Reset()
        {
            _position = -1;
        }

        void IDisposable.Dispose() { }
    }
}
