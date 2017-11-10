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

        // GET: api/Reservations
        public IQueryable<Reservations> GetReservations()
        {
            return db.Reservations;
        }

        // GET: api/Reservations/5
        [ResponseType(typeof(Reservations))]
        public IHttpActionResult GetReservations(int id)
        {
            Reservations reservations = db.Reservations.Find(id);
            if (reservations == null)
            {
                return NotFound();
            }

            return Ok(reservations);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reservations"></param>
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




        // PUT: api/Reservations/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutReservations(int id, Reservations reservations)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != reservations.Id)
            {
                return BadRequest();
            }

            db.Entry(reservations).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReservationsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        

        // DELETE: api/Reservations/5
        [ResponseType(typeof(Reservations))]
        public IHttpActionResult DeleteReservations(int id)
        {
            Reservations reservations = db.Reservations.Find(id);
            if (reservations == null)
            {
                return NotFound();
            }

            db.Reservations.Remove(reservations);
            db.SaveChanges();

            return Ok(reservations);
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