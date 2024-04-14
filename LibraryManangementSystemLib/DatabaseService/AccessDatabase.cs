using System.Threading.Tasks;

namespace HotelManangementSystemLibrary.DatabaseService
{
    public class AccessDatabase : IDatabaseService//, IDisposable
    {
        //Datamembers
        private IUsers users;
        private IGuests guests;
        private IRoomBookings bookings;
        private IRooms rooms;

        //Properties
        public IRoomBookings Bookings => bookings;

        public IRooms Rooms => rooms;

        public IUsers Users => users;

        public IGuests Guests => guests;

        private readonly string connectionString;
        //ctor 
        public AccessDatabase(string connectionString)
        {
            this.connectionString = connectionString;
            LoadEntireDatabaseAsync();
        }//AccessDatabase
        public async void LoadEntireDatabaseAsync()
        {
            if (users != null)
                return;
            await Task.Run(() =>
            {
                users = DBFactory.CreateAndLoadDBUsers(this.connectionString);
                rooms = DBFactory.CreateAndLoadDBRooms(this.connectionString);
                guests = DBFactory.CreateAndLoadDBGuests(this.connectionString,users);
                bookings = DBFactory.CreateAndLoadDBRoomBookings(this.connectionString,guests,rooms);
            });//
        }//LoadEntireDatabaseAsync

        #region Loading
        public IRoomBookings LoadBookings()
        {
            return bookings;
        }//LoadBookings
        public IGuests LoadGuests()
        {
            return guests;
        }//LoadGuests

        public IRooms LoadRooms()
        {
            return rooms;
        }//LoadRooms
        //private 

        public IUsers LoadUsers()
        {
            return users;
        }//LoadUsers
        #endregion Loading

        #region Saving not implemented since the changes will be push to the database autometically
        public void SaveBookings()
        {
           //Do nothing since all processes/changes(updates delations, insertion) will be pushed to the database autometically
        }//SaveBookings

        public void SaveGuets()
        {
            //Do nothing since all processes/changes(updates delations, insertion) will be pushed to the database autometically
        }//SaveGuets

        public void SaveRooms()
        {
            //Do nothing since all processes/changes(updates delations, insertion) will be pushed to the database autometically
        }//SaveRooms

        public void SaveUsers()
        {
            //Do nothing since all processes/changes(updates delations, insertion) will be pushed to the database autometically
        }//SaveUser
        #endregion Saving
    }//class
}//namespace