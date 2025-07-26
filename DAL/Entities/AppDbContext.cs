using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DAL.Entities;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Appointment> Appointments { get; set; }

    public virtual DbSet<Arv> Arvs { get; set; }

    public virtual DbSet<ArvProtocol> ArvProtocols { get; set; }

    public virtual DbSet<ArvProtocolDetail> ArvProtocolDetails { get; set; }

    public virtual DbSet<Blog> Blogs { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<CustomizedArvProtocol> CustomizedArvProtocols { get; set; }

    public virtual DbSet<CustomizedArvProtocolDetail> CustomizedArvProtocolDetails { get; set; }

    public virtual DbSet<DoctorInfo> DoctorInfos { get; set; }

    public virtual DbSet<EducationalResource> EducationalResources { get; set; }

    public virtual DbSet<Examination> Examinations { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<Schedule> Schedules { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("server=PCOFVTER\\SQLEXPRESS; database=HIV; uid=sa; pwd=290604; TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.ToTable("Account");
        });

        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.ToTable("Appointment");

            entity.HasIndex(e => e.DoctorId, "IX_Appointment_DoctorId");

            entity.HasIndex(e => e.PatientId, "IX_Appointment_PatientId");

            entity.HasIndex(e => e.ScheduleId, "IX_Appointment_ScheduleId");

            entity.HasOne(d => d.Doctor).WithMany(p => p.AppointmentDoctors)
                .HasForeignKey(d => d.DoctorId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Patient).WithMany(p => p.AppointmentPatients)
                .HasForeignKey(d => d.PatientId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Schedule).WithMany(p => p.Appointments).HasForeignKey(d => d.ScheduleId);
        });

        modelBuilder.Entity<Arv>(entity =>
        {
            entity.ToTable("ARV");
        });

        modelBuilder.Entity<ArvProtocol>(entity =>
        {
            entity.HasKey(e => e.ProtocolId);

            entity.ToTable("ARV_Protocol");
        });

        modelBuilder.Entity<ArvProtocolDetail>(entity =>
        {
            entity.ToTable("ARV_Protocol_Detail");

            entity.HasIndex(e => e.ArvId, "IX_ARV_Protocol_Detail_ArvId");

            entity.HasIndex(e => e.ProtocolId, "IX_ARV_Protocol_Detail_ProtocolId");

            entity.HasOne(d => d.Arv).WithMany(p => p.ArvProtocolDetails).HasForeignKey(d => d.ArvId);

            entity.HasOne(d => d.Protocol).WithMany(p => p.ArvProtocolDetails).HasForeignKey(d => d.ProtocolId);
        });

        modelBuilder.Entity<Blog>(entity =>
        {
            entity.ToTable("Blog");

            entity.HasIndex(e => e.AuthorId, "IX_Blog_AuthorId");

            entity.HasOne(d => d.Author).WithMany(p => p.Blogs).HasForeignKey(d => d.AuthorId);
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.ToTable("Comment");

            entity.HasIndex(e => e.BlogId, "IX_Comment_BlogId");

            entity.HasIndex(e => e.UserId, "IX_Comment_UserId");

            entity.HasOne(d => d.Blog).WithMany(p => p.Comments).HasForeignKey(d => d.BlogId);

            entity.HasOne(d => d.User).WithMany(p => p.Comments)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<CustomizedArvProtocol>(entity =>
        {
            entity.HasKey(e => e.CustomProtocolId);

            entity.ToTable("CustomizedARV_Protocol");

            entity.HasIndex(e => e.BaseProtocolId, "IX_CustomizedARV_Protocol_BaseProtocolId");

            entity.HasIndex(e => e.DoctorId, "IX_CustomizedARV_Protocol_DoctorId");

            entity.HasIndex(e => e.PatientId, "IX_CustomizedARV_Protocol_PatientId");

            entity.HasOne(d => d.BaseProtocol).WithMany(p => p.CustomizedArvProtocols).HasForeignKey(d => d.BaseProtocolId);

            entity.HasOne(d => d.Doctor).WithMany(p => p.CustomizedArvProtocolDoctors).HasForeignKey(d => d.DoctorId);

            entity.HasOne(d => d.Patient).WithMany(p => p.CustomizedArvProtocolPatients).HasForeignKey(d => d.PatientId);
        });

        modelBuilder.Entity<CustomizedArvProtocolDetail>(entity =>
        {
            entity.ToTable("CustomizedARV_Protocol_Detail");

            entity.HasIndex(e => e.ArvId, "IX_CustomizedARV_Protocol_Detail_ArvId");

            entity.HasIndex(e => e.CustomProtocolId, "IX_CustomizedARV_Protocol_Detail_CustomProtocolId");

            entity.HasOne(d => d.Arv).WithMany(p => p.CustomizedArvProtocolDetails).HasForeignKey(d => d.ArvId);

            entity.HasOne(d => d.CustomProtocol).WithMany(p => p.CustomizedArvProtocolDetails).HasForeignKey(d => d.CustomProtocolId);
        });

        modelBuilder.Entity<DoctorInfo>(entity =>
        {
            entity.HasKey(e => e.DoctorId);

            entity.ToTable("DoctorInfo");

            entity.Property(e => e.DoctorId).ValueGeneratedNever();

            entity.HasOne(d => d.Doctor).WithOne(p => p.DoctorInfo).HasForeignKey<DoctorInfo>(d => d.DoctorId);
        });

        modelBuilder.Entity<EducationalResource>(entity =>
        {
            entity.HasKey(e => e.ResourceId);

            entity.HasIndex(e => e.CreatedBy, "IX_EducationalResources_CreatedBy");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.EducationalResources).HasForeignKey(d => d.CreatedBy);
        });

        modelBuilder.Entity<Examination>(entity =>
        {
            entity.HasKey(e => e.ExamId);

            entity.ToTable("Examination");

            entity.HasIndex(e => e.DoctorId, "IX_Examination_DoctorId");

            entity.HasIndex(e => e.PatientId, "IX_Examination_PatientId");

            entity.HasOne(d => d.Doctor).WithMany(p => p.ExaminationDoctors)
                .HasForeignKey(d => d.DoctorId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Patient).WithMany(p => p.ExaminationPatients)
                .HasForeignKey(d => d.PatientId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.ToTable("Notification");

            entity.HasIndex(e => e.AppointmentId, "IX_Notification_AppointmentId");

            entity.HasIndex(e => e.ExaminationId, "IX_Notification_ExaminationId");

            entity.HasIndex(e => e.ProtocolId, "IX_Notification_ProtocolId");

            entity.HasIndex(e => e.UserId, "IX_Notification_UserId");

            entity.HasOne(d => d.Appointment).WithMany(p => p.Notifications).HasForeignKey(d => d.AppointmentId);

            entity.HasOne(d => d.Examination).WithMany(p => p.Notifications).HasForeignKey(d => d.ExaminationId);

            entity.HasOne(d => d.Protocol).WithMany(p => p.Notifications).HasForeignKey(d => d.ProtocolId);

            entity.HasOne(d => d.User).WithMany(p => p.Notifications).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<Schedule>(entity =>
        {
            entity.ToTable("Schedule");

            entity.HasIndex(e => e.DoctorId, "IX_Schedule_DoctorId");

            entity.HasOne(d => d.Doctor).WithMany(p => p.Schedules)
                .HasForeignKey(d => d.DoctorId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.HasIndex(e => e.AccountId, "IX_User_AccountId").IsUnique();

            entity.HasOne(d => d.Account).WithOne(p => p.User).HasForeignKey<User>(d => d.AccountId);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
