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
    public class ReservationsController : ApiController
    {
        private BookingOfMeetingRoomsDBEntities db = new BookingOfMeetingRoomsDBEntities();

        // GET: api/Reservations/GetReservationsForMeetingRoom/5
        [ResponseType(typeof(Reservations))]
        public IQueryable<Reservations> GetReservationsForMeetingRoom(int id)
        {
            var timeNow = DateTime.Now;
            return db.Reservations.Where(p=>p.MeetingRoom_Id==id && p.Status == true && p.DateFinish > timeNow);
        }


        /// <returns></returns>
        // POST: api/Reservations/AddReservations
        [ResponseType(typeof(Reservations))]
        public IHttpActionResult AddReservations(Reservations reservations)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //reservations.MeetingRooms = db.MeetingRooms.Find(reservations.MeetingRoom_Id);
            //reservations.Users = db.Users.Find(reservations.User_Id);

            db.Reservations.Add(reservations);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = reservations.Id }, reservations);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ReservationsExists(int id)
        {
            return db.Reservations.Count(e => e.Id == id) > 0;
        }
    }
}