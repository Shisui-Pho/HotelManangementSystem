using System;
using System.Collections.Generic;
using System.Linq;

namespace HotelManangementSystemLibrary
{
    public interface IRoomFeatures
    {
        event delOnFeaturesModified OnFeaturesModified;
        public RoomFeatures() => features = new List<IFeature>();
        public IEnumerable<Feature> GetRoomFeatures()
        {
            foreach (Feature item in features)
            {
                yield return item;
            }
        }
        public void AddFeature(IFeature feature)
        {
            if (features.IndexOf(feature) >= 0)
                throw new ArgumentException("The feature has already been implemented");
            features.Add(feature);
            OnFeaturesModified?.Invoke(feature, true, new FeatureEventArgs()); ;
        }
        public void RemoveFeature(string featureid)
        {
            IFeature temp = features.FirstOrDefault(f => f.FeatureID == featureid);
            if (temp == null)
                throw new ArgumentException("The feature ID was not found in this room");
            features.Remove(temp);
            OnFeaturesModified?.Invoke(temp, false, new FeatureEventArgs());
        }//
        public override string ToString()
        {
            string s = "{";
            foreach (var item in features)
            {
                s += item.FeatureID + " ";
            }
            if (s.Length > 1)
                s = s.Remove(s.Length - 1);
            s += "}";
            return s;
        }//ToString
    }//class
}//namespace
