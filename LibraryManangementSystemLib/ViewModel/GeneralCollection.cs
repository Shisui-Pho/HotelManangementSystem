using HotelManangementSystemLibrary.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelManangementSystemLibrary
{
    public abstract class GeneralCollection<T> : IGeneralCollection<T>
        where T : IComparable
    {
        //Protected variable made available to the uderlying classes
        protected readonly List<T> _collection;
        //private data members
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
        public virtual void Add(T item)
        {
            ItemAddedEvent?.Invoke(new HotelEventArgs("", "") { IsHandled = false });
            _collection.Add(item);
        }//Add

        public void Sort()
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
        public virtual void Remove(T item)
        {
            _collection.Remove(item);
            ItemRemovedEvent?.Invoke(item, new HotelEventArgs("","") { IsHandled = false });
        }//Remove

        public virtual void Update(T old, T _new)
        {
            int i = IndexOf(old);
            if (i < 0)
            {
                ExceptionLog.Exception("Item was not found.","Item not found");
                return;
            }
            _collection[i] = _new;
            UpdatedEvent?.Invoke(old, _new, new HotelEventArgs("", "") { IsHandled = false });
        }//Update
        public void ClearAllData()
        {
            //_collection = new List<T>();
        }//ClearAllData
        protected int IndexOf(T item)
        {
            for (int i = 0; i < _collection.Count; i++)
                if (_collection[i].Equals(item))
                    return i;
            return -1;
        }
    }////class
}//namespace