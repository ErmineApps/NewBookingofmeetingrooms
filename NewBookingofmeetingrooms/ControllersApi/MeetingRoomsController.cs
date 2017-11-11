using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using NewBookingofmeetingrooms;

namespace NewBookingofmeetingrooms.ControllersApi
{
    public class MeetingRoomsController : ApiController
    {
        private BookingOfMeetingRoomsDBEntities db = new BookingOfMeetingRoomsDBEntities();

        // GET: api/MeetingRooms/GetMeetingRooms
        public IQueryable<MeetingRooms> GetMeetingRooms()
        {
            return db.MeetingRooms;
        }

       

/*        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MeetingRoomsExists(int id)
        {
            return db.MeetingRooms.Count(e => e.Id == id) > 0;
        }*/
    }
}