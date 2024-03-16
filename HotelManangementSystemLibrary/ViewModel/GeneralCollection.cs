using System;
using System.Collections.Generic;
using System.Text;

namespace HotelManangementSystemLibrary
{
    internal abstract class GeneralCollection<T> : IGeneralCollection<T>
    {
        //private data members
        protected List<T> _collection;
        private bool isSorted = false;

        //indexers
        public T this[int index] 
        {
            get
            {
                if (index >= Count)
                    throw new IndexOutOfRangeException();
                return _collection[index];
            }//
        }//end getter

        public int Count => _collection.Count;

        public event delOnRemovedEvent ItemRemovedEvent;
        public event delOnAddedEvent ItemAddedEvent;
        public event delOnUpdatedEvent UpdatedEvent;
        public GeneralCollection()
        {
            _collection = new List<T>();
        }//GeneralCollection
        public GeneralCollection(List<T> lstdata)
            => _collection = lstdata;
        public GeneralCollection(T[] data)
        {
            _collection = new List<T>();
            _collection.AddRange(data);
        }//end ctor
        public void Add(T item)
        {
            ItemAddedEvent?.Invoke(new HotelEventArgs("", "") { IsHandled = false });
            _collection.Add(item);
        }//Add

        public void BatchSort()
        {
            if (!isSorted)
            {
                isSorted = true;
                _collection.Sort();
            }//end if  
        }//BatchSort

        public IEnumerator<T> GetEnumerator()
        {
            foreach (T item in _collection)
            {
                yield return item;
            }//end foreach
        }//GetEnumerator
        public void Remove(T item)
        {
            _collection.Remove(item);
            ItemRemovedEvent?.Invoke(item, new HotelEventArgs("","") { IsHandled = false });
        }//Remove

        public void Update(T old, T _new)
        {
            int i = _collection.IndexOf(old);
            if (i < 0)
                throw new ArgumentException("Item was not found.");
            _collection[i] = _new;
            UpdatedEvent?.Invoke(old, _new, new HotelEventArgs("", "") { IsHandled = false });
        }//Update
    }////class
}//namespace