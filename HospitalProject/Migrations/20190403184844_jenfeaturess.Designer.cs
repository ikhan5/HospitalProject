﻿// <auto-generated />
using HospitalProject.Data;
using HospitalProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace HospitalProject.Migrations
{
    [DbContext(typeof(HospitalCMSContext))]
    [Migration("20190403184844_jenfeaturess")]
    partial class jenfeaturess
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.3-rtm-10026")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("HospitalProject.Models.Admin", b =>
                {
                    b.Property<int>("AdminID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("UserID");

                    b.Property<bool>("UserStatus")
                        .HasMaxLength(255);

                    b.Property<int>("UserType");

                    b.HasKey("AdminID");

                    b.ToTable("Admins");
                });

            modelBuilder.Entity("HospitalProject.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<int?>("AdminID");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<int?>("ParkingServiceID");

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<int?>("PlanYourStayID");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("AdminID")
                        .IsUnique()
                        .HasFilter("[AdminID] IS NOT NULL");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.HasIndex("ParkingServiceID")
                        .IsUnique()
                        .HasFilter("[ParkingServiceID] IS NOT NULL");

                    b.HasIndex("PlanYourStayID")
                        .IsUnique()
                        .HasFilter("[PlanYourStayID] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("HospitalProject.Models.Donation", b =>
                {
                    b.Property<int>("donationID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("donationFormID");

                    b.Property<string>("donorEmail")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<string>("donorName")
                        .HasMaxLength(255);

                    b.Property<int>("isRecurring");

                    b.Property<int>("paymentAmount");

                    b.Property<int>("paymentMethod");

                    b.HasKey("donationID");

                    b.HasIndex("donationFormID");

                    b.ToTable("Donations");
                });

            modelBuilder.Entity("HospitalProject.Models.DonationForm", b =>
                {
                    b.Property<int>("donationFormID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("charityName")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<string>("donationCause")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<int>("donationGoal");

                    b.Property<string>("formDescription")
                        .HasMaxLength(2147483647);

                    b.Property<string>("presetAmounts")
                        .HasMaxLength(255);

                    b.HasKey("donationFormID");

                    b.ToTable("DonationForms");
                });

            modelBuilder.Entity("HospitalProject.Models.EmergencyWaitTime", b =>
                {
                    b.Property<int>("EmergencyWaitTimeID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ServiceName")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<string>("WaitTime")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.HasKey("EmergencyWaitTimeID");

                    b.ToTable("EmergencyWaitTimes");
                });

            modelBuilder.Entity("HospitalProject.Models.JobApplication", b =>
                {
                    b.Property<int>("jobApplicationID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("applicantEmail")
                        .HasMaxLength(2147483647);

                    b.Property<string>("applicantName")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<DateTime>("applicationDate");

                    b.Property<int>("jobPostingID");

                    b.HasKey("jobApplicationID");

                    b.HasIndex("jobPostingID");

                    b.ToTable("JobApplications");
                });

            modelBuilder.Entity("HospitalProject.Models.JobPosting", b =>
                {
                    b.Property<int>("jobPostingID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("jobDescription")
                        .HasMaxLength(2147483647);

                    b.Property<DateTime>("jobExpiryDate");

                    b.Property<DateTime>("jobPostingDate");

                    b.Property<string>("jobQualifications")
                        .HasMaxLength(2147483647);

                    b.Property<string>("jobSkills")
                        .HasMaxLength(2147483647);

                    b.Property<string>("jobTitle")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.HasKey("jobPostingID");

                    b.ToTable("JobPostings");
                });

            modelBuilder.Entity("HospitalProject.Models.Navigation", b =>
                {
                    b.Property<int>("navigationID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("navigationName")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<int>("navigationPosition");

                    b.Property<string>("navigationURL")
                        .HasMaxLength(255);

                    b.HasKey("navigationID");

                    b.ToTable("Navigations");
                });

            modelBuilder.Entity("HospitalProject.Models.Page", b =>
                {
                    b.Property<int>("pageID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("dateCreated");

                    b.Property<string>("jobSkills")
                        .HasMaxLength(2147483647);

                    b.Property<DateTime>("lastModified");

                    b.Property<int>("navigationID");

                    b.Property<string>("pageAuthor")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<string>("pageContent")
                        .HasMaxLength(2147483647);

                    b.Property<int>("pageOrder");

                    b.Property<string>("pageTitle")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.HasKey("pageID");

                    b.HasIndex("navigationID");

                    b.ToTable("Pages");
                });

            modelBuilder.Entity("HospitalProject.Models.ParkingService", b =>
                {
                    b.Property<int>("ParkingServiceID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ParkingNumber");

                    b.Property<string>("ParkingServiceUserID");

                    b.Property<string>("Rate")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<bool>("Status");

                    b.HasKey("ParkingServiceID");

                    b.ToTable("ParkingServices");
                });

            modelBuilder.Entity("HospitalProject.Models.PlanYourStay", b =>
                {
                    b.Property<int>("PlanYourStayID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("PlanYourStayUserID");

                    b.Property<string>("RatePerDay")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<string>("RoomNumber")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<string>("ServiceName")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.HasKey("PlanYourStayID");

                    b.ToTable("PlanYourStays");
                });

            modelBuilder.Entity("HospitalProject.Models.ReferAPatient", b =>
                {
                    b.Property<int>("ReferAPatientID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CurrPrimDiag")
                        .IsRequired()
                        .HasMaxLength(2147483647);

                    b.Property<DateTime>("DOB");

                    b.Property<string>("MedHist")
                        .IsRequired()
                        .HasMaxLength(2147483647);

                    b.Property<string>("OHIP")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<string>("PatAddress")
                        .IsRequired()
                        .HasMaxLength(2147483647);

                    b.Property<string>("PatEmail")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<string>("PatName")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<string>("PatPhone")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<string>("ProgReq")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<string>("ReferFac")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<string>("ReferPhysEmail")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<string>("ReferPhysName")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<string>("ReferPhysPhone")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<DateTime>("ReferalDate");

                    b.Property<string>("ServiceReq")
                        .IsRequired()
                        .HasMaxLength(2147483647);

                    b.HasKey("ReferAPatientID");

                    b.ToTable("ReferAPatients");
                });

            modelBuilder.Entity("HospitalProject.Models.VolunteerApplication", b =>
                {
                    b.Property<int>("VolunteerAppID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Age")
                        .HasMaxLength(3);

                    b.Property<DateTime>("AppDate");

                    b.Property<string>("AppFName")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<string>("AppLName")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<string>("Descriptions")
                        .IsRequired()
                        .HasMaxLength(2147483647);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<int>("VolunteerPostID");

                    b.HasKey("VolunteerAppID");

                    b.HasIndex("VolunteerPostID");

                    b.ToTable("VolunteerApplications");
                });

            modelBuilder.Entity("HospitalProject.Models.VolunteerPost", b =>
                {
                    b.Property<int>("VolunteerPostID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Department")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<string>("Details")
                        .IsRequired()
                        .HasMaxLength(2147483647);

                    b.Property<string>("Position")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<DateTime>("PostDate");

                    b.HasKey("VolunteerPostID");

                    b.ToTable("VolunteerPosts");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("HospitalProject.Models.ApplicationUser", b =>
                {
                    b.HasOne("HospitalProject.Models.Admin", "admin")
                        .WithOne("admin")
                        .HasForeignKey("HospitalProject.Models.ApplicationUser", "AdminID");

                    b.HasOne("HospitalProject.Models.ParkingService")
                        .WithOne("user")
                        .HasForeignKey("HospitalProject.Models.ApplicationUser", "ParkingServiceID");

                    b.HasOne("HospitalProject.Models.PlanYourStay")
                        .WithOne("user")
                        .HasForeignKey("HospitalProject.Models.ApplicationUser", "PlanYourStayID");
                });

            modelBuilder.Entity("HospitalProject.Models.Donation", b =>
                {
                    b.HasOne("HospitalProject.Models.DonationForm", "DonationForm")
                        .WithMany("Donations")
                        .HasForeignKey("donationFormID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("HospitalProject.Models.JobApplication", b =>
                {
                    b.HasOne("HospitalProject.Models.JobPosting", "JobPosting")
                        .WithMany("JobApplication")
                        .HasForeignKey("jobPostingID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("HospitalProject.Models.Page", b =>
                {
                    b.HasOne("HospitalProject.Models.Navigation", "Navigation")
                        .WithMany("Pages")
                        .HasForeignKey("navigationID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("HospitalProject.Models.VolunteerApplication", b =>
                {
                    b.HasOne("HospitalProject.Models.VolunteerPost", "VolunteerPosts")
                        .WithMany("VolunteerApplications")
                        .HasForeignKey("VolunteerPostID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("HospitalProject.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("HospitalProject.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("HospitalProject.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("HospitalProject.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
