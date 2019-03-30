using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace HospitalProject.Migrations
{
    public partial class Imzan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Users_UserID",
                table: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "Admins");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Admins",
                table: "Admins",
                column: "UserID");

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

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Admins_UserID",
                table: "AspNetUsers",
                column: "UserID",
                principalTable: "Admins",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Admins_UserID",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Donations");

            migrationBuilder.DropTable(
                name: "JobApplications");

            migrationBuilder.DropTable(
                name: "Pages");

            migrationBuilder.DropTable(
                name: "DonationForms");

            migrationBuilder.DropTable(
                name: "JobPostings");

            migrationBuilder.DropTable(
                name: "Navigations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Admins",
                table: "Admins");

            migrationBuilder.RenameTable(
                name: "Admins",
                newName: "Users");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Users_UserID",
                table: "AspNetUsers",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
