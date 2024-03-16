using System;
using System.Collections.Generic;
using System.Text;

namespace HotelManangementSystemLibrary
{
    public interface IGeneralCollection<T>
    {
        event delOnRemovedEvent ItemRemovedEvent;
        event delOnAddedEvent ItemAddedEvent;
        event delOnUpdatedEvent UpdatedEvent;
        int Count { get; }
        T this[int index] { get; }//An indexer for the 
        void Add(T item);
        void Remove(T item);
        void Update(T old, T _new);
        void BatchSort();
        IEnumerator<T> GetEnumerator();
    }//interface
}//namespace
