using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using NewBookingofmeetingrooms;

namespace NewBookingofmeetingrooms.ControllersApi
{
    public class ServicesController : ApiController
    {
        private BookingOfMeetingRoomsDBEntities db = new BookingOfMeetingRoomsDBEntities();

     
        // GET: api/Services/GetServicesID/1
        public IQueryable<Services> GetServicesID(int id)
        {
            return  db.Services.Where(p => p.IdUser == id);
        }

        // GET: api/Services/GetServicesDeleteID/1
        public IQueryable<Services> GetServicesDeleteID(int id)
        {
            var services = db.Services.Where(p => p.IdUser == id);

            foreach (var service in services)
            {
                db.Services.Remove(service);
            }

            db.SaveChanges();

            return services;
        }

       

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ServicesExists(int id)
        {
            return db.Services.Count(e => e.Id == id) > 0;
        }
    }
}