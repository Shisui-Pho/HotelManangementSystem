namespace HotelManangementSystemLibrary
{
    //Event delegates
    public delegate void delOnRemovedEvent(object sender, HotelEventArgs args);
    public delegate void delOnUpdatedEvent(object old, object @new, HotelEventArgs args);
    public delegate void delOnAddedEvent(HotelEventArgs args);
    public delegate void delOnPropertyChanged(string id, string field, string newVal);
    public delegate void delOnPriceChanged(IRoom room);
    internal delegate void delOnFeatureRemoved(string roomNumber, string FeatureID);
    public delegate void delOnFeaturesModified(IFeature feature, bool isAdded, FeatureEventArgs args);
    public delegate void delOnTrasnaction(TransactionArgs transaction);
    public delegate void delOnBalanceChanged(decimal newBalance, decimal newAmountOwing);
    public delegate void delBalanceChanged(BalanceChangedEventArgs args);
    public delegate void delOnBookingFeesChanged(BookingFeesChangedEventArgs args);
    public delegate void delOnServiceLog(ServiceLogEventArgs args);
    public delegate void delOnTicketAdded(Ticket ticket,string serviceid);
    public delegate bool delUserExceptionEvent(AlertUserEvent args);
}//namespace