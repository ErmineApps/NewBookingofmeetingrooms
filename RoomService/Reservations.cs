//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RoomService
{
    using System;
    using System.Collections.Generic;
    
    public partial class Reservations
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Reservations()
        {
            this.Services = new HashSet<Services>();
        }
    
        public int Id { get; set; }
        public Nullable<int> User_Id { get; set; }
        public Nullable<System.DateTime> DateStart { get; set; }
        public Nullable<System.DateTime> DateFinish { get; set; }
        public Nullable<bool> Status { get; set; }
        public Nullable<int> MeetingRoom_Id { get; set; }
    
        public virtual MeetingRooms MeetingRooms { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Services> Services { get; set; }
    }
}