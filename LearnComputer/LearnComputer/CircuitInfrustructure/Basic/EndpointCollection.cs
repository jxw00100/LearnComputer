using System;
using System.Collections;
using System.Collections.Generic;

namespace LearnComputer.CircuitInfrustructure
{
    public class EndpointCollection<T>: IEndpointCollection<T> where T: IEndpoint, new()
    {
        private T[] _endpoints;
        private InnerEnumerator _enumerator;

        public EndpointCollection(Int32 width)
        {
            if(width <= 0) throw new ArgumentException("The items count of collection must large than 0.");

            _endpoints = new T[width];
            _enumerator = new InnerEnumerator(ref _endpoints);

            for (var i = 0; i < width; i++)
            {
                _endpoints[i] = new T();
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _enumerator;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public Int32 Count
        {
            get { return _endpoints.Length; }
        }

        public void Connect<TE>(IEndpointCollection<TE> connectToPoints) where TE : IEndpoint
        {
            if(connectToPoints == null) throw new ArgumentException("The connectToPoints collection is null.");

            for (var i = 0; i < _endpoints.Length && i < connectToPoints.Count; i++)
            {
                _endpoints[i].ConnectTo(connectToPoints[i]);
            }
        }

        public void ConnectAt(Int32 index, IEndpoint endpoint)
        {
            _endpoints[index].ConnectTo(endpoint);
        }

        public void DisconnectAll()
        {
            foreach(var endpoint in _endpoints) endpoint.DisconnectEndpoint();
        }

        public void DisconnectAt(Int32 index)
        {
            _endpoints[index].DisconnectEndpoint();
        }

        public T this[Int32 index]
        {
            get { return _endpoints[index]; }
            private set { _endpoints[index] = value; }
        }

        private struct InnerEnumerator: IEnumerator<T>
        {
            private T[] _array;
            private Int32 _index;

            public T Current { get; private set; }

            public InnerEnumerator(ref T[] array) : this()
            {
                _array = array;
                _index = 0;
                Current = _array[0];
            }

            public void Dispose()
            {
            }

            public Boolean MoveNext()
            {
                if (_index < _array.Length) 
                {
                    Current = _array[_index];
                    _index++;
                    return true;
                }
                else
                {
                    return false;
                }
            }

            public void Reset()
            {
                _index = 0;
            }

            Object IEnumerator.Current
            {
                get { return Current; }
            }
        }
    }
}
