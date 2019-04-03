using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace HospitalProject.Migrations
{
    public partial class @new : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    AdminID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Email = table.Column<string>(nullable: false),
                    UserID = table.Column<string>(nullable: true),
                    UserStatus = table.Column<bool>(maxLength: 255, nullable: false),
                    UserType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.AdminID);
                });

            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    client_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    appointment_details = table.Column<string>(maxLength: 255, nullable: false),
                    client_doctor_id = table.Column<string>(maxLength: 255, nullable: false),
                    client_emailadd = table.Column<string>(maxLength: 255, nullable: false),
                    client_fname = table.Column<string>(maxLength: 255, nullable: false),
                    client_lname = table.Column<string>(maxLength: 255, nullable: false),
                    client_phone = table.Column<string>(maxLength: 255, nullable: false),
                    date_time = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.client_id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clinics",
                columns: table => new
                {
                    clinic_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    clinic_description = table.Column<string>(maxLength: 255, nullable: false),
                    clinic_location = table.Column<string>(maxLength: 255, nullable: false),
                    clinic_name = table.Column<string>(maxLength: 255, nullable: false),
                    clinic_phone = table.Column<string>(nullable: true),
                    clinic_services = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clinics", x => x.clinic_id);
                });

            migrationBuilder.CreateTable(
                name: "Doctors",
                columns: table => new
                {
                    DoctorID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DoctorName = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctors", x => x.DoctorID);
                });

            migrationBuilder.CreateTable(
                name: "DonationForms",
                columns: table => new
                {
                    donationFormID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    charityName = table.Column<string>(maxLength: 255, nullable: false),
                    donationCause = table.Column<string>(maxLength: 255, nullable: false),
                    donationGoal = table.Column<int>(nullable: false),
                    formDescription = table.Column<string>(maxLength: 2147483647, nullable: true),
                    presetAmounts = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonationForms", x => x.donationFormID);
                });

            migrationBuilder.CreateTable(
                name: "EmergencyWaitTimes",
                columns: table => new
                {
                    EmergencyWaitTimeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ServiceName = table.Column<string>(maxLength: 255, nullable: false),
                    WaitTime = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmergencyWaitTimes", x => x.EmergencyWaitTimeID);
                });

            migrationBuilder.CreateTable(
                name: "JobPostings",
                columns: table => new
                {
                    jobPostingID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    jobDescription = table.Column<string>(maxLength: 2147483647, nullable: true),
                    jobExpiryDate = table.Column<DateTime>(nullable: false),
                    jobPostingDate = table.Column<DateTime>(nullable: false),
                    jobQualifications = table.Column<string>(maxLength: 2147483647, nullable: true),
                    jobSkills = table.Column<string>(maxLength: 2147483647, nullable: true),
                    jobTitle = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobPostings", x => x.jobPostingID);
                });

            migrationBuilder.CreateTable(
                name: "Medicalservices",
                columns: table => new
                {
                    medical_services_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    medical_service_type = table.Column<string>(maxLength: 255, nullable: false),
                    medical_services_description = table.Column<string>(maxLength: 255, nullable: false),
                    medical_services_name = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medicalservices", x => x.medical_services_id);
                });

            migrationBuilder.CreateTable(
                name: "Navigations",
                columns: table => new
                {
                    navigationID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    navigationName = table.Column<string>(maxLength: 255, nullable: false),
                    navigationPosition = table.Column<int>(nullable: false),
                    navigationURL = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Navigations", x => x.navigationID);
                });

            migrationBuilder.CreateTable(
                name: "ParkingServices",
                columns: table => new
                {
                    ParkingServiceID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ParkingNumber = table.Column<int>(nullable: false),
                    ParkingServiceUserID = table.Column<string>(nullable: true),
                    Rate = table.Column<string>(maxLength: 255, nullable: false),
                    Status = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParkingServices", x => x.ParkingServiceID);
                });

            migrationBuilder.CreateTable(
                name: "PlanYourStays",
                columns: table => new
                {
                    PlanYourStayID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PlanYourStayUserID = table.Column<string>(nullable: true),
                    RatePerDay = table.Column<string>(maxLength: 255, nullable: false),
                    RoomNumber = table.Column<string>(maxLength: 255, nullable: false),
                    ServiceName = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanYourStays", x => x.PlanYourStayID);
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    QuestionID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AnswerList = table.Column<string>(maxLength: 255, nullable: false),
                    QuestionList = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.QuestionID);
                });

            migrationBuilder.CreateTable(
                name: "ReferAPatients",
                columns: table => new
                {
                    ReferAPatientID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CurrPrimDiag = table.Column<string>(maxLength: 2147483647, nullable: false),
                    DOB = table.Column<DateTime>(nullable: false),
                    MedHist = table.Column<string>(maxLength: 2147483647, nullable: false),
                    OHIP = table.Column<string>(maxLength: 255, nullable: false),
                    PatAddress = table.Column<string>(maxLength: 2147483647, nullable: false),
                    PatEmail = table.Column<string>(maxLength: 255, nullable: false),
                    PatName = table.Column<string>(maxLength: 255, nullable: false),
                    PatPhone = table.Column<string>(maxLength: 255, nullable: false),
                    ProgReq = table.Column<string>(maxLength: 255, nullable: false),
                    ReferFac = table.Column<string>(maxLength: 255, nullable: false),
                    ReferPhysEmail = table.Column<string>(maxLength: 255, nullable: false),
                    ReferPhysName = table.Column<string>(maxLength: 255, nullable: false),
                    ReferPhysPhone = table.Column<string>(maxLength: 255, nullable: false),
                    ReferalDate = table.Column<DateTime>(nullable: false),
                    ServiceReq = table.Column<string>(maxLength: 2147483647, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReferAPatients", x => x.ReferAPatientID);
                });

            migrationBuilder.CreateTable(
                name: "VolunteerPosts",
                columns: table => new
                {
                    VolunteerPostID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Department = table.Column<string>(maxLength: 255, nullable: false),
                    Details = table.Column<string>(maxLength: 2147483647, nullable: false),
                    Position = table.Column<string>(maxLength: 255, nullable: false),
                    PostDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VolunteerPosts", x => x.VolunteerPostID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    DepartmentID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DepartmentName = table.Column<string>(maxLength: 255, nullable: false),
                    DoctorID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.DepartmentID);
                    table.ForeignKey(
                        name: "FK_Departments_Doctors_DoctorID",
                        column: x => x.DoctorID,
                        principalTable: "Doctors",
                        principalColumn: "DoctorID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ratings",
                columns: table => new
                {
                    RatingID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DoctorID = table.Column<int>(nullable: true),
                    Feedback = table.Column<string>(maxLength: 2147483647, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ratings", x => x.RatingID);
                    table.ForeignKey(
                        name: "FK_Ratings_Doctors_DoctorID",
                        column: x => x.DoctorID,
                        principalTable: "Doctors",
                        principalColumn: "DoctorID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Donations",
                columns: table => new
                {
                    donationID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    donationFormID = table.Column<int>(nullable: false),
                    donorEmail = table.Column<string>(maxLength: 255, nullable: false),
                    donorName = table.Column<string>(maxLength: 255, nullable: true),
                    isRecurring = table.Column<int>(nullable: false),
                    paymentAmount = table.Column<int>(nullable: false),
                    paymentMethod = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Donations", x => x.donationID);
                    table.ForeignKey(
                        name: "FK_Donations_DonationForms_donationFormID",
                        column: x => x.donationFormID,
                        principalTable: "DonationForms",
                        principalColumn: "donationFormID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobApplications",
                columns: table => new
                {
                    jobApplicationID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    applicantEmail = table.Column<string>(maxLength: 2147483647, nullable: true),
                    applicantName = table.Column<string>(maxLength: 255, nullable: false),
                    applicationDate = table.Column<DateTime>(nullable: false),
                    jobPostingID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobApplications", x => x.jobApplicationID);
                    table.ForeignKey(
                        name: "FK_JobApplications_JobPostings_jobPostingID",
                        column: x => x.jobPostingID,
                        principalTable: "JobPostings",
                        principalColumn: "jobPostingID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pages",
                columns: table => new
                {
                    pageID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    dateCreated = table.Column<DateTime>(nullable: false),
                    jobSkills = table.Column<string>(maxLength: 2147483647, nullable: true),
                    lastModified = table.Column<DateTime>(nullable: false),
                    navigationID = table.Column<int>(nullable: false),
                    pageAuthor = table.Column<string>(maxLength: 255, nullable: false),
                    pageContent = table.Column<string>(maxLength: 2147483647, nullable: true),
                    pageOrder = table.Column<int>(nullable: false),
                    pageTitle = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pages", x => x.pageID);
                    table.ForeignKey(
                        name: "FK_Pages_Navigations_navigationID",
                        column: x => x.navigationID,
                        principalTable: "Navigations",
                        principalColumn: "navigationID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    AdminID = table.Column<int>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    ParkingServiceID = table.Column<int>(nullable: true),
                    PasswordHash = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    PlanYourStayID = table.Column<int>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Admins_AdminID",
                        column: x => x.AdminID,
                        principalTable: "Admins",
                        principalColumn: "AdminID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_ParkingServices_ParkingServiceID",
                        column: x => x.ParkingServiceID,
                        principalTable: "ParkingServices",
                        principalColumn: "ParkingServiceID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_PlanYourStays_PlanYourStayID",
                        column: x => x.PlanYourStayID,
                        principalTable: "PlanYourStays",
                        principalColumn: "PlanYourStayID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VolunteerApplications",
                columns: table => new
                {
                    VolunteerAppID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Age = table.Column<int>(maxLength: 3, nullable: false),
                    AppDate = table.Column<DateTime>(nullable: false),
                    AppFName = table.Column<string>(maxLength: 255, nullable: false),
                    AppLName = table.Column<string>(maxLength: 255, nullable: false),
                    Descriptions = table.Column<string>(maxLength: 2147483647, nullable: false),
                    Email = table.Column<string>(maxLength: 255, nullable: false),
                    Phone = table.Column<string>(maxLength: 255, nullable: false),
                    VolunteerPostID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VolunteerApplications", x => x.VolunteerAppID);
                    table.ForeignKey(
                        name: "FK_VolunteerApplications_VolunteerPosts_VolunteerPostID",
                        column: x => x.VolunteerPostID,
                        principalTable: "VolunteerPosts",
                        principalColumn: "VolunteerPostID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_AdminID",
                table: "AspNetUsers",
                column: "AdminID",
                unique: true,
                filter: "[AdminID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ParkingServiceID",
                table: "AspNetUsers",
                column: "ParkingServiceID",
                unique: true,
                filter: "[ParkingServiceID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_PlanYourStayID",
                table: "AspNetUsers",
                column: "PlanYourStayID",
                unique: true,
                filter: "[PlanYourStayID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_DoctorID",
                table: "Departments",
                column: "DoctorID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Donations_donationFormID",
                table: "Donations",
                column: "donationFormID");

            migrationBuilder.CreateIndex(
                name: "IX_JobApplications_jobPostingID",
                table: "JobApplications",
                column: "jobPostingID");

            migrationBuilder.CreateIndex(
                name: "IX_Pages_navigationID",
                table: "Pages",
                column: "navigationID");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_DoctorID",
                table: "Ratings",
                column: "DoctorID");

            migrationBuilder.CreateIndex(
                name: "IX_VolunteerApplications_VolunteerPostID",
                table: "VolunteerApplications",
                column: "VolunteerPostID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Clinics");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "Donations");

            migrationBuilder.DropTable(
                name: "EmergencyWaitTimes");

            migrationBuilder.DropTable(
                name: "JobApplications");

            migrationBuilder.DropTable(
                name: "Medicalservices");

            migrationBuilder.DropTable(
                name: "Pages");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "Ratings");

            migrationBuilder.DropTable(
                name: "ReferAPatients");

            migrationBuilder.DropTable(
                name: "VolunteerApplications");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "DonationForms");

            migrationBuilder.DropTable(
                name: "JobPostings");

            migrationBuilder.DropTable(
                name: "Navigations");

            migrationBuilder.DropTable(
                name: "Doctors");

            migrationBuilder.DropTable(
                name: "VolunteerPosts");

            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "ParkingServices");

            migrationBuilder.DropTable(
                name: "PlanYourStays");
        }
    }
}
