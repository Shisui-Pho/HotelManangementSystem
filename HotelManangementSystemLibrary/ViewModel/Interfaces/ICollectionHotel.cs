using System;
using System.Collections.Generic;
using System.Text;

namespace HotelManangementSystemLibrary
{
    public interface ICollectionHotel<T>
    {
        int Count { get; }
        void Add(T item);
        void Remove(T item);
        void Update(T old, T _new);
        IEnumerator<T> GetEnumerator();
    }//interface
}//namespace
