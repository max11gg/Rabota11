using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ApiHotels.Models
{
    public partial class HotelContext : DbContext
    {
        public HotelContext()
        {
        }

        public HotelContext(DbContextOptions<HotelContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Booking> Bookings { get; set; } 
        public virtual DbSet<CheckIn> CheckIns { get; set; } 
        public virtual DbSet<CheckOut> CheckOuts { get; set; } 
        public virtual DbSet<Hotel> Hotels { get; set; } 
        public virtual DbSet<Role> Roles { get; set; } 
        public virtual DbSet<Room> Rooms { get; set; } 
        public virtual DbSet<Service> Services { get; set; } 
        public virtual DbSet<Status> Statuses { get; set; } 
        public virtual DbSet<Token> Tokens { get; set; } 
        public virtual DbSet<User> Users { get; set; } 

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-EDQFE2B\\SQL;Initial Catalog=Hotel;Persist Security Info=True;User ID=sa;Password=123");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Booking>(entity =>
            {
                entity.HasKey(e => e.IdBooking);

                entity.ToTable("Booking");

                entity.Property(e => e.IdBooking).HasColumnName("ID_Booking");

                entity.Property(e => e.ArrivalDate)
                    .HasColumnType("date")
                    .HasColumnName("Arrival_date");

                entity.Property(e => e.DepartureDate)
                    .HasColumnType("date")
                    .HasColumnName("Departure_date");
                entity.Property(e => e.IsBooking).HasColumnName("Is_Booking");

                entity.Property(e => e.RoomId).HasColumnName("Room_ID");

                entity.Property(e => e.ServiceId).HasColumnName("Service_ID");

                entity.Property(e => e.UserId).HasColumnName("User_ID");

              
            });

            modelBuilder.Entity<CheckIn>(entity =>
            {
                entity.HasKey(e => e.IdCheckIn);

                entity.ToTable("Check_in");

                entity.Property(e => e.IdCheckIn).HasColumnName("ID_Check_in");

                entity.Property(e => e.BookingId).HasColumnName("Booking_ID");

                entity.Property(e => e.StatusCheckIn)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("Status_Check_in");

                entity.Property(e => e.UserId).HasColumnName("User_ID");

            });

            modelBuilder.Entity<CheckOut>(entity =>
            {
                entity.HasKey(e => e.IdCheckOut);

                entity.ToTable("Check_out");

                entity.Property(e => e.IdCheckOut).HasColumnName("ID_Check_out");

                entity.Property(e => e.CheckInId).HasColumnName("Check_in_ID");

                entity.Property(e => e.PaymentDate)
                    .HasColumnType("date")
                    .HasColumnName("Payment_date");

                entity.Property(e => e.TotalCost).HasColumnName("Total_cost");

                entity.Property(e => e.UserId).HasColumnName("User_ID");


            });

            modelBuilder.Entity<Hotel>(entity =>
            {
                entity.HasKey(e => e.IdHotels);

                entity.Property(e => e.IdHotels).HasColumnName("ID_Hotels");

                entity.Property(e => e.AdressHotel)
                    .IsUnicode(false)
                    .HasColumnName("Adress_hotel");

                entity.Property(e => e.EmailHotel)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("Email_hotel");

                entity.Property(e => e.NumberPhoneHotel)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("Number_Phone_hotel");

                entity.Property(e => e.RateHotel).HasColumnName("Rate_hotel");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.IdRole);

                entity.ToTable("Role");

                entity.HasIndex(e => e.NameRole, "UQ_Name_Role")
                    .IsUnique();

                entity.Property(e => e.IdRole).HasColumnName("ID_Role");

                entity.Property(e => e.NameRole)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Name_Role");
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.HasKey(e => e.IdRoom);

                entity.ToTable("Room");

                entity.Property(e => e.IdRoom).HasColumnName("ID_Room");

                entity.Property(e => e.CountRoom).HasColumnName("Count_room");

                entity.Property(e => e.HotelsId).HasColumnName("Hotels_ID");

                entity.Property(e => e.NumberRoom).HasColumnName("Number_room");

                entity.Property(e => e.StatusId).HasColumnName("Status_ID");

                entity.Property(e => e.TypeRoom)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("Type_room");

            });

            modelBuilder.Entity<Service>(entity =>
            {
                entity.HasKey(e => e.IdService);

                entity.ToTable("Service");

                entity.Property(e => e.IdService).HasColumnName("ID_Service");

                entity.Property(e => e.DescriptionServices)
                    .IsUnicode(false)
                    .HasColumnName("Description_Services");

                entity.Property(e => e.NameServices)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Name_Services");

                entity.Property(e => e.PriceServices).HasColumnName("Price_Services");
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.HasKey(e => e.IdStatus);

                entity.ToTable("Status");

                entity.HasIndex(e => e.Availability, "UQ_Name_Status")
                    .IsUnique();

                entity.Property(e => e.IdStatus).HasColumnName("ID_Status");

                entity.Property(e => e.Availability)
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Token>(entity =>
            {
                entity.Property(e => e.TokenId).HasColumnName("token_id");

                entity.Property(e => e.Token1)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("token");

                entity.Property(e => e.TokenDatetime)
                    .HasColumnName("token_datetime")
                    .HasDefaultValueSql("(sysdatetime())");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.IdUser);

                entity.ToTable("User");

                entity.Property(e => e.IdUser).HasColumnName("ID_User");

                entity.Property(e => e.EmailUser)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("Email_User");

                entity.Property(e => e.FirstNameUser)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("First_Name_User");

                entity.Property(e => e.LastNameUser)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("Last_Name_User");

                entity.Property(e => e.LoginUser)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Login_User");

                entity.Property(e => e.MiddleNameUser)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("Middle_name_User");

                entity.Property(e => e.NumberPhone)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("Number_Phone");

                entity.Property(e => e.PasportNumber)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("Pasport_number");

                entity.Property(e => e.PasportSeries)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("Pasport_series");

                entity.Property(e => e.PasswordUser)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Password_User");

                entity.Property(e => e.RoleId).HasColumnName("Role_ID");

                entity.Property(e => e.Salt)
                    .HasMaxLength(256)
                    .IsUnicode(false);

            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
