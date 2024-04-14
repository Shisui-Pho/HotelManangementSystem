using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HotelManangementSystemLibrary
{
    internal class PersonelRepository : GeneralCollection<IServicePersonel> , IPersonelRepository
    {
        public PersonelRepository() : base() { }
        public PersonelRepository(IServicePersonel[] personels) : base(personels) { }
        public PersonelRepository(List<IServicePersonel> personels) : base(personels) { }
        public IEnumerator<Ticket?> GetPersonelTickets(string personID)
        {
            yield return null;
        }
        public IServicePersonel GetPersonel(string personelID)
        {
            return base._collection.FirstOrDefault(p => p.UserID == personelID);
        }//GetPersonel
    }//class
}//namespace
