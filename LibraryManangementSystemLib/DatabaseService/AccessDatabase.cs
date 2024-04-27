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
            //LoadEntireDatabaseAsync();
        }//AccessDatabase
        public async void LoadEntireDatabaseAsync()
        {
            if (users != null)
                return;
            await Task.Run(() =>
            {
                users = DBFactory.CreateAndLoadDBUsers(this.connectionString);
                rooms = DBFactory.CreateAndLoadDBRooms(this.connectionString);
                guests = DBFactory.CreateAndLoadDBGuests(this.connectionString, users);
            });//
            bookings = await DBFactory.CreateAndLoadDBRoomBookings(this.connectionString, guests, rooms, default);
        }//LoadEntireDatabaseAsync

        #region Loading
        public IGuests LoadGuests()
        {
            guests = DBFactory.CreateAndLoadDBGuests(this.connectionString, users);
            return guests;
        }//LoadGuests

        public IRooms LoadRooms()
        {
            rooms = DBFactory.CreateAndLoadDBRooms(this.connectionString);
            return rooms;
        }//LoadRooms
        //private 

        public IUsers LoadUsers()
        {
            users = DBFactory.CreateAndLoadDBUsers(this.connectionString);
            return users;
        }//LoadUsers
        public async Task<IRoomBookings> LoadBookingsAsync(IUser user)
        {
            bookings = await DBFactory.CreateAndLoadDBRoomBookings(this.connectionString, guests, rooms,user);
            return bookings;
        }//LoadBookings
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

        public IRoomBookings LoadBookings()
        {
            return bookings;
        }
        #endregion Saving
    }//class
}//namespace