using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace ClinicDatabaseModel.Database.Context;

internal partial class ClinicdbContext : DbContext
{
    private readonly string _connectionString;

    public string ConnectionString
    {
        get { return _connectionString; }
    }

    public ClinicdbContext(string connectionString)
    {
        _connectionString = connectionString;
    }

    public ClinicdbContext(string connectionString, DbContextOptions<ClinicdbContext> options)
        : base(options)
    {
        _connectionString = connectionString;
    }

    public virtual DbSet<Attachment> Attachments { get; set; }

    public virtual DbSet<Branch> Branches { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Patient> Patients { get; set; }

    public virtual DbSet<Recipe> Recipes { get; set; }

    public virtual DbSet<Shedule> Shedules { get; set; }

    public virtual DbSet<Speciality> Specialities { get; set; }

    public virtual DbSet<Visit> Visits { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql(_connectionString);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("pgcrypto");

        modelBuilder.Entity<Attachment>(entity =>
        {
            entity.HasKey(e => new { e.PatientId, e.SpecilityId }).HasName("attachments_pkey");

            entity.ToTable("attachments", tb => tb.HasComment("РџСЂРёРєСЂРµРїР»РµРЅРёСЏ"));

            entity.HasIndex(e => e.BranchId, "attachments_branch_id_key").IsUnique();

            entity.Property(e => e.PatientId).HasColumnName("patient_id");
            entity.Property(e => e.SpecilityId).HasColumnName("specility_id");
            entity.Property(e => e.BranchId).HasColumnName("branch_id");
            entity.Property(e => e.Since)
                .HasComment("Р”Р°С‚Р° РїСЂРёРєСЂРµРїР»РµРЅРёСЏ")
                .HasColumnName("since");

            entity.HasOne(d => d.Branch).WithOne(p => p.Attachment)
                .HasForeignKey<Attachment>(d => d.BranchId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("attachments_branch_id_fkey");

            entity.HasOne(d => d.Patient).WithMany(p => p.Attachments)
                .HasForeignKey(d => d.PatientId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("attachments_patient_id_fkey");

            entity.HasOne(d => d.Specility).WithMany(p => p.Attachments)
                .HasForeignKey(d => d.SpecilityId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("attachments_specility_id_fkey");
        });

        modelBuilder.Entity<Branch>(entity =>
        {
            entity.HasKey(e => e.BranchId).HasName("branch_pkey");

            entity.ToTable("branch", tb => tb.HasComment("Р¤РёР»РёР°Р»"));

            entity.HasIndex(e => e.Name, "branch_name_key").IsUnique();

            entity.Property(e => e.BranchId).HasColumnName("branch_id");
            entity.Property(e => e.Address)
                .HasComment("РђРґСЂРµСЃ С„РёР»РёР°Р»Р°")
                .HasColumnName("address");
            entity.Property(e => e.Email)
                .HasMaxLength(256)
                .HasComment("email")
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasComment("РќР°Р·РІР°РЅРёРµ РѕС‚РґРµР»РµРЅРёСЏ")
                .HasColumnName("name");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .HasComment("РўРµР»РµС„РѕРЅ")
                .HasColumnName("phone");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("employee_pkey");

            entity.ToTable("employee", tb => tb.HasComment("РЎРѕС‚СЂСѓРґРЅРёРєРё"));

            entity.HasIndex(e => e.EmployeeId, "employee_employee_id_idx");

            entity.HasIndex(e => e.Inn, "employee_inn_key").IsUnique();

            entity.HasIndex(e => e.Login, "employee_login_idx");

            entity.HasIndex(e => e.Login, "employee_login_key").IsUnique();

            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
            entity.Property(e => e.Award)
                .HasComment("РџСЂРµРјРёСЏ")
                .HasColumnName("award");
            entity.Property(e => e.Firstname)
                .HasMaxLength(128)
                .HasColumnName("firstname");
            entity.Property(e => e.Inn)
                .HasPrecision(12)
                .HasColumnName("inn");
            entity.Property(e => e.Lastname)
                .HasMaxLength(128)
                .HasComment("Р¤Р°РјРёР»РёСЏ")
                .HasColumnName("lastname");
            entity.Property(e => e.Login)
                .HasMaxLength(256)
                .HasComment("Р›РѕРіРёРЅ РґР»СЏ РІС…РѕРґР° РІ СЃРёСЃС‚РµРјСѓ")
                .HasColumnName("login");
            entity.Property(e => e.Password)
                .HasComment("РџР°СЂРѕР»СЊ")
                .HasColumnName("password");
            entity.Property(e => e.Patronymic)
                .HasMaxLength(128)
                .HasComment("РћС‚С‡РµСЃС‚РІРѕ")
                .HasColumnName("patronymic");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .HasComment("РљРѕРЅС‚Р°РєС‚РЅС‹Р№ РЅРѕРјРµСЂ С‚РµР»РµС„РѕРЅР° СЃРѕС‚СЂСѓРґРЅРёРєР°")
                .HasColumnName("phone");
            entity.Property(e => e.Salary).HasColumnName("salary");
            entity.Property(e => e.SpecilityId).HasColumnName("specility_id");
            entity.Property(e => e.Title)
                .HasMaxLength(128)
                .HasColumnName("title");
            entity.Property(e => e.VacationCount)
                .HasComment("РљРѕР»РёС‡РµСЃС‚РІРѕ РѕС‚РїСѓСЃРєРЅС‹С… РґРЅРµР№")
                .HasColumnName("vacation_count");

            entity.HasOne(d => d.Specility).WithMany(p => p.Employees)
                .HasForeignKey(d => d.SpecilityId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("employee_specility_id_fkey");
        });

        modelBuilder.Entity<Patient>(entity =>
        {
            entity.HasKey(e => e.PatientId).HasName("patient_pkey");

            entity.ToTable("patient", tb => tb.HasComment("РџР°С†РёРµРЅС‚С‹"));

            entity.HasIndex(e => e.Login, "patient_login_idx");

            entity.HasIndex(e => e.Login, "patient_login_key").IsUnique();

            entity.HasIndex(e => e.Oms, "patient_oms_key").IsUnique();

            entity.HasIndex(e => e.PatientId, "patient_patient_id_idx");

            entity.Property(e => e.PatientId).HasColumnName("patient_id");
            entity.Property(e => e.BirthDate)
                .HasComment("Р”Р°С‚Р° СЂРѕР¶РґРµРЅРёСЏ")
                .HasColumnName("birth_date");
            entity.Property(e => e.Firstname)
                .HasMaxLength(128)
                .HasColumnName("firstname");
            entity.Property(e => e.Gender)
                .HasComment("РџРѕР» : true - male")
                .HasColumnName("gender");
            entity.Property(e => e.Lastname)
                .HasMaxLength(128)
                .HasComment("Р¤Р°РјРёР»РёСЏ")
                .HasColumnName("lastname");
            entity.Property(e => e.Login)
                .HasMaxLength(256)
                .HasComment("login")
                .HasColumnName("login");
            entity.Property(e => e.Oms)
                .HasPrecision(16)
                .HasComment("РџРѕР»РёСЃ OMC")
                .HasColumnName("oms");
            entity.Property(e => e.Password)
                .HasComment("password")
                .HasColumnName("password");
            entity.Property(e => e.Patronymic)
                .HasMaxLength(128)
                .HasComment("РћС‚С‡РµСЃС‚РІРѕ")
                .HasColumnName("patronymic");
            entity.Property(e => e.Snils)
                .HasPrecision(11)
                .HasColumnName("snils");
        });

        modelBuilder.Entity<Recipe>(entity =>
        {
            entity.HasKey(e => e.RecipeId).HasName("recipes_pkey");

            entity.ToTable("recipes", tb => tb.HasComment("Р РµС†РµРїС‚С‹"));

            entity.Property(e => e.RecipeId).HasColumnName("recipe_id");
            entity.Property(e => e.ActiveSince)
                .HasComment("РќР°С‡Р°Р»Рѕ СЃСЂРѕРєР° СЂРµС†РµРїС‚Р°")
                .HasColumnName("active_since");
            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
            entity.Property(e => e.PatientId).HasColumnName("patient_id");
            entity.Property(e => e.Period)
                .HasComment("РЎСЂРѕРє СЂР°Р±РѕС‚С‹ СЂРµС†РµРїС‚Р°")
                .HasColumnName("period");
            entity.Property(e => e.Recipe1)
                .HasComment("Р РµС†РµРїС‚")
                .HasColumnName("recipe");

            entity.HasOne(d => d.Employee).WithMany(p => p.Recipes)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("recipes_employee_id_fkey");

            entity.HasOne(d => d.Patient).WithMany(p => p.Recipes)
                .HasForeignKey(d => d.PatientId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("recipes_patient_id_fkey");
        });

        modelBuilder.Entity<Shedule>(entity =>
        {
            entity.HasKey(e => e.WdId).HasName("shedule_pkey");

            entity.ToTable("shedule", tb => tb.HasComment("Р Р°СЃРїРёСЃР°РЅРёРµ"));

            entity.Property(e => e.WdId).HasColumnName("wd_id");
            entity.Property(e => e.BranchId).HasColumnName("branch_id");
            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
            entity.Property(e => e.StartWorking)
                .HasComment("РќР°С‡Р°Р»Рѕ СЂР°Р±РѕС‚С‹")
                .HasColumnName("start_working");
            entity.Property(e => e.Weekday)
                .HasPrecision(1)
                .HasComment("Р”РµРЅСЊ РЅРµРґРµР»Рё : Р§РёСЃР»Рѕ РѕС‚ 0 РґРѕ 6, РіРґРµ: \r\n0 - РІРѕСЃРєСЂРµСЃРµРЅРёРµ, 6 - СЃСѓР±Р±РѕС‚Р°")
                .HasColumnName("weekday");

            entity.HasOne(d => d.Branch).WithMany(p => p.Shedules)
                .HasForeignKey(d => d.BranchId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("shedule_branch_id_fkey");

            entity.HasOne(d => d.Employee).WithMany(p => p.Shedules)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("shedule_employee_id_fkey");
        });

        modelBuilder.Entity<Speciality>(entity =>
        {
            entity.HasKey(e => e.SpecilityId).HasName("speciality_pkey");

            entity.ToTable("speciality", tb => tb.HasComment("РЎРїРµС†РёР°Р»СЊРЅРѕСЃС‚СЊ"));

            entity.HasIndex(e => e.SpecialityName, "speciality_speciality_name_key").IsUnique();

            entity.Property(e => e.SpecilityId).HasColumnName("specility_id");
            entity.Property(e => e.SpecialityName)
                .HasComment("РЎРїРµС†РёР°Р»СЊРЅРѕСЃС‚СЊ")
                .HasColumnName("speciality_name");
        });

        modelBuilder.Entity<Visit>(entity =>
        {
            entity.HasKey(e => new { e.VisitDate, e.SheduledVisitTime }).HasName("visit_pkey");

            entity.ToTable("visit", tb => tb.HasComment("РџСЂРёРµРјС‹"));

            entity.Property(e => e.VisitDate)
                .HasComment("Р”Р°С‚Р° РїСЂРёРµРјР°")
                .HasColumnName("visit_date");
            entity.Property(e => e.SheduledVisitTime)
                .HasComment("Р’СЂРµРјСЏ РїСЂРёРµРјР° (РїРѕ СЂР°СЃРїРёСЃР°РЅРёСЋ)")
                .HasColumnName("sheduled_visit_time");
            entity.Property(e => e.Complaint)
                .HasComment("Р–Р°Р»РѕР±Р°")
                .HasColumnName("complaint");
            entity.Property(e => e.PatientId).HasColumnName("patient_id");
            entity.Property(e => e.WdId).HasColumnName("wd_id");

            entity.HasOne(d => d.Patient).WithMany(p => p.Visits)
                .HasForeignKey(d => d.PatientId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("visit_patient_id_fkey");

            entity.HasOne(d => d.Wd).WithMany(p => p.Visits)
                .HasForeignKey(d => d.WdId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("visit_wd_id_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
