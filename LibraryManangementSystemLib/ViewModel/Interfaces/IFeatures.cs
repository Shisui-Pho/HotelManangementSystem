using System;
using System.Collections.Generic;
using System.Text;

namespace HotelManangementSystemLibrary
{
    public interface IFeatures : IGeneralCollection<IFeature>, IDisposable
    {
        IFeature GetFeature(string id);
    }
}
