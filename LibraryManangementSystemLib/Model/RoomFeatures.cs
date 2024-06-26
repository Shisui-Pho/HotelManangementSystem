﻿using HotelManangementSystemLibrary.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HotelManangementSystemLibrary
{
    public class RoomFeatures : IRoomFeatures
    {
        private List<IFeature> features;
        public event delOnFeaturesModified OnFeaturesModified;
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
            {
                ExceptionLog.Exception("The feature has already been implemented","Feature Creation Error");
                return;
            }
            features.Add(feature);
            OnFeaturesModified?.Invoke(feature, true, new FeatureEventArgs()); ;
        }
        public void RemoveFeature(string featureid)
        {
            IFeature temp = features.FirstOrDefault(f => f.FeatureID == featureid);
            if (temp == null)
            {
                ExceptionLog.Exception($"The feature Id \"{featureid}\" does not exist.","Feature not found");
                return;
            }
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
            if(s.Length > 1)
                s = s.Remove(s.Length - 1);
            s += "}";
            return s;
        }//ToString
    }//class
}//namespace
