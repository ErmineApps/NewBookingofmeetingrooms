using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RoomService
{
    public class ServiceRoomEntry
    {
        private CancellationTokenSource _cancellation;

        public void StartService()
        {
            _cancellation = new CancellationTokenSource();
            Start(_cancellation.Token);
        }

        public Task Start(CancellationToken token)
        {
            try
            {
                return Task.Run(() => {
                    while (!token.IsCancellationRequested)
                    {
                        Console.WriteLine("Started CheckingAvailabilityOfRooms()");
                        CheckingAvailabilityOfRooms().Wait();
                        Task.Delay(60000, token).Wait();
                    }
                });
            }
            catch (Exception e)
            {
                var exc = e.GetBaseException();
                throw exc;
            }

        }

        public void StopService()
        {
            Console.WriteLine("CLOSE EWW");
            _cancellation.Cancel();
        }

        public async Task CheckingAvailabilityOfRooms()
        {
            Console.WriteLine("CheckingAvailabilityOfRooms SSTART EWW");
            try
            {
                using (var db = new BookingOfMeetingRoomsDBEntities())
                {
                    var meetingRooms = await db.MeetingRooms.ToListAsync();
                    var timeNow = DateTime.Now;
                    var timeNowPlus = DateTime.Now.AddDays(1);

                    foreach (var meetingRoom in meetingRooms)
                    {
                        var reservations = db.Reservations.Where(p => (DateTime)p.DateStart > timeNow && (DateTime)p.DateStart < timeNowPlus && (int)p.MeetingRoom_Id == meetingRoom.Id)
                            .ToList();

                        // свободна!
                        if (!reservations.Any())
                        {
                            meetingRoom.DateReserv = null;
                            meetingRoom.FreedomStatus = true;

                            db.Entry(meetingRoom).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                        else
                        {
                            foreach (var reservation in reservations)
                            {
                                if (reservation.DateStart < timeNow &&
                                    timeNow < reservation.DateFinish)
                                {
                                    meetingRoom.DateReserv = reservation.DateStart;
                                    meetingRoom.FreedomStatus = false;

                                    db.Entry(meetingRoom).State = EntityState.Modified;
                                    db.SaveChanges();
                                    break;
                                }
                                // забранированна на ..
                                else
                                {
                                    meetingRoom.DateReserv = reservation.DateStart;
                                    meetingRoom.FreedomStatus = true;

                                    db.Entry(meetingRoom).State = EntityState.Modified;
                                    db.SaveChanges();
                                    break;
                                }
                            }
                        }
                    }
                }
                Console.WriteLine("___CLOSE 1");
            }
            catch (Exception e)
            {
                Console.WriteLine("___CLOSE 3");
                Console.WriteLine(e);
                throw;
            }
            Console.WriteLine("___CLOSE 2");
        }
    }
}
