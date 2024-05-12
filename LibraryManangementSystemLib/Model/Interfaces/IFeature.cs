namespace HotelManangementSystemLibrary
{
    public interface IFeature : IHotelModel<IFeature>
    {
        string Description { get; }
        string FeatureID { get; }
        string FeatureName { get; }
        decimal Price { get; }
        void ChangePrice(decimal newprice);
        void ChangeDescription(string desc);
        void ChangeName(string newname);
    }
}