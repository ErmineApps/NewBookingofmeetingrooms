using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NewBookingofmeetingrooms;

namespace NewBookingofmeetingrooms.Controllers
{
    public class BookingСonfirmationController : Controller
    {
        private BookingOfMeetingRoomsDBEntities db = new BookingOfMeetingRoomsDBEntities();

        // GET: BookingСonfirmation
        public ActionResult Index()
        {
            var reservation = db.Reservations.Include(r => r.MeetingRooms).Where(p => p.Status == null);
            return View(reservation.ToList());
        }

        // POST: BookingСonfirmation/Delete/5
        //[HttpPost, ActionName("SetStatusYes")]
        [ActionName("SetStatusYes")]
        public ActionResult SetStatusYes(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservations reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            //ViewBag.MeetingRoom = new SelectList(db.MeetingRooms, "Id", "Name", reservation.MeetingRooms);
            //ViewBag.User = new SelectList(db.Users, "Id", "UserName", reservation.User_Id);

            if (ModelState.IsValid)
            {
                reservation.Status = true;
                db.Entry(reservation).State = EntityState.Modified;

                Services service = new Services();
                service.Reservations = reservation;
                service.IdUser = reservation.User_Id;
                //User user = reservation.User   

                db.Services.Add(service);
                db.SaveChanges();
                //return RedirectToAction("Index");
                var reservation11 = db.Reservations.Include(r => r.MeetingRooms).Where(p => p.Status == null);
                return View("Index", reservation11.ToList());
            }
            var reservation1 = db.Reservations.Include(r => r.MeetingRooms).Where(p => p.Status == null);
            return View("Index", reservation1.ToList());
        }

        // POST: BookingСonfirmation/Delete/5
        //        [HttpPost, ActionName("SetStatusNo")]
        [ActionName("SetStatusNo")]
        public ActionResult SetStatusNo(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservations reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
//            ViewBag.MeetingRoom = new SelectList(db.MeetingRooms, "Id", "Name", reservation.MeetingRooms);
//            ViewBag.User = new SelectList(db.Users, "Id", "UserName", reservation.User_Id);

            if (ModelState.IsValid)
            {
                reservation.Status = false;
                db.Entry(reservation).State = EntityState.Modified;

                Services service = new Services();
                service.Reservations = reservation;
                service.IdUser = reservation.User_Id;
                //User user = reservation.User   

                db.Services.Add(service);
                db.SaveChanges();
                //return RedirectToAction("Index");
                var reservation11 = db.Reservations
                    .Include(r => r.MeetingRooms)
                    .Where(p => p.Status == null);

                return View("Index", reservation11.ToList());
            }
            var reservation1 = db.Reservations.Include(r => r.MeetingRooms).Where(p => p.Status == null);
            return View("Index", reservation1.ToList());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }

}
