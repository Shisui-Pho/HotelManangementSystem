using System;
using System.Collections.Generic;
using System.Linq;

namespace HotelManangementSystemLibrary
{
    public interface IRoomFeatures
    {
        event delOnFeaturesModified OnFeaturesModified;
        IEnumerable<Feature> GetRoomFeatures();
        void AddFeature(IFeature feature);
        void RemoveFeature(string featureid);
    }//class
}//namespace
