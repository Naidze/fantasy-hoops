﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using fantasy_hoops.Database;

namespace fantasy_hoops.Migrations
{
    [DbContext(typeof(GameContext))]
    [Migration("20190226143318_AddGLeagueStatus")]
    partial class AddGLeagueStatus
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.2-servicing-10034")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

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
                        .HasName("RoleNameIndex");

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

            modelBuilder.Entity("fantasy_hoops.Models.FriendRequest", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.Property<string>("ReceiverID");

                    b.Property<string>("SenderID");

                    b.Property<int>("Status");

                    b.HasKey("ID");

                    b.HasIndex("ReceiverID");

                    b.HasIndex("SenderID");

                    b.ToTable("FriendRequests");
                });

            modelBuilder.Entity("fantasy_hoops.Models.Injuries", b =>
                {
                    b.Property<int>("InjuryID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.Property<string>("Description");

                    b.Property<string>("Injury");

                    b.Property<string>("Link");

                    b.Property<int>("PlayerID");

                    b.Property<string>("Status");

                    b.Property<string>("Title");

                    b.HasKey("InjuryID");

                    b.HasIndex("PlayerID");

                    b.ToTable("Injuries");
                });

            modelBuilder.Entity("fantasy_hoops.Models.Lineup", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Calculated");

                    b.Property<DateTime>("Date");

                    b.Property<double>("FP");

                    b.Property<int>("PlayerID");

                    b.Property<string>("Position");

                    b.Property<string>("UserID");

                    b.HasKey("ID");

                    b.HasIndex("PlayerID");

                    b.HasIndex("UserID");

                    b.ToTable("Lineups");
                });

            modelBuilder.Entity("fantasy_hoops.Models.News", b =>
                {
                    b.Property<int>("NewsID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.Property<string>("Title");

                    b.Property<int>("hTeamID");

                    b.Property<int>("vTeamID");

                    b.HasKey("NewsID");

                    b.ToTable("News");
                });

            modelBuilder.Entity("fantasy_hoops.Models.Notification", b =>
                {
                    b.Property<int>("NotificationID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateCreated");

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<bool>("ReadStatus");

                    b.Property<string>("UserID");

                    b.HasKey("NotificationID");

                    b.HasIndex("UserID");

                    b.ToTable("Notifications");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Notification");
                });

            modelBuilder.Entity("fantasy_hoops.Models.Paragraph", b =>
                {
                    b.Property<int>("ParagraphID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content");

                    b.Property<int>("NewsID");

                    b.Property<int>("ParagraphNumber");

                    b.HasKey("ParagraphID");

                    b.HasIndex("NewsID");

                    b.ToTable("Paragraphs");
                });

            modelBuilder.Entity("fantasy_hoops.Models.Player", b =>
                {
                    b.Property<int>("PlayerID")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("AST");

                    b.Property<string>("AbbrName");

                    b.Property<double>("BLK");

                    b.Property<double>("FPPG");

                    b.Property<string>("FirstName");

                    b.Property<string>("FullName");

                    b.Property<int>("GP");

                    b.Property<bool>("IsInGLeague");

                    b.Property<bool>("IsPlaying");

                    b.Property<string>("LastName");

                    b.Property<int>("NbaID");

                    b.Property<int>("Number");

                    b.Property<double>("PTS");

                    b.Property<string>("Position");

                    b.Property<int>("Price");

                    b.Property<double>("REB");

                    b.Property<double>("STL");

                    b.Property<string>("Status")
                        .IsRequired();

                    b.Property<DateTime?>("StatusDate");

                    b.Property<double>("TOV");

                    b.Property<int>("TeamID");

                    b.HasKey("PlayerID");

                    b.HasIndex("TeamID");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("fantasy_hoops.Models.Stats", b =>
                {
                    b.Property<int>("StatsID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AST");

                    b.Property<int>("BLK");

                    b.Property<int>("DREB");

                    b.Property<DateTime>("Date");

                    b.Property<int>("FGA");

                    b.Property<int>("FGM");

                    b.Property<double>("FGP");

                    b.Property<int>("FLS");

                    b.Property<double>("FP");

                    b.Property<int>("FTA");

                    b.Property<int>("FTM");

                    b.Property<double>("FTP");

                    b.Property<double>("GS");

                    b.Property<string>("MIN");

                    b.Property<int>("OREB");

                    b.Property<int>("OppID");

                    b.Property<int>("PTS");

                    b.Property<int>("PlayerID");

                    b.Property<int>("Price");

                    b.Property<int>("STL");

                    b.Property<string>("Score");

                    b.Property<int>("TOV");

                    b.Property<int>("TPA");

                    b.Property<int>("TPM");

                    b.Property<double>("TPP");

                    b.Property<int>("TREB");

                    b.HasKey("StatsID");

                    b.HasIndex("PlayerID");

                    b.ToTable("Stats");
                });

            modelBuilder.Entity("fantasy_hoops.Models.Team", b =>
                {
                    b.Property<int>("TeamID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Abbreviation");

                    b.Property<string>("City");

                    b.Property<string>("Color");

                    b.Property<string>("Name");

                    b.Property<int>("NbaID");

                    b.HasKey("TeamID");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("fantasy_hoops.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Description");

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<int>("FavoriteTeamId");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<int>("Streak");

                    b.Property<int?>("TeamID");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.HasIndex("TeamID");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("fantasy_hoops.Models.GameScoreNotification", b =>
                {
                    b.HasBaseType("fantasy_hoops.Models.Notification");

                    b.Property<double>("Score");

                    b.HasDiscriminator().HasValue("GameScoreNotification");
                });

            modelBuilder.Entity("fantasy_hoops.Models.Notifications.FriendRequestNotification", b =>
                {
                    b.HasBaseType("fantasy_hoops.Models.Notification");

                    b.Property<string>("FriendID");

                    b.Property<string>("RequestMessage");

                    b.HasIndex("FriendID");

                    b.HasDiscriminator().HasValue("FriendRequestNotification");
                });

            modelBuilder.Entity("fantasy_hoops.Models.Notifications.InjuryNotification", b =>
                {
                    b.HasBaseType("fantasy_hoops.Models.Notification");

                    b.Property<string>("InjuryDescription");

                    b.Property<string>("InjuryStatus");

                    b.Property<int>("PlayerID");

                    b.HasIndex("PlayerID");

                    b.HasDiscriminator().HasValue("InjuryNotification");
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
                    b.HasOne("fantasy_hoops.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("fantasy_hoops.Models.User")
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

                    b.HasOne("fantasy_hoops.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("fantasy_hoops.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("fantasy_hoops.Models.FriendRequest", b =>
                {
                    b.HasOne("fantasy_hoops.Models.User", "Receiver")
                        .WithMany()
                        .HasForeignKey("ReceiverID");

                    b.HasOne("fantasy_hoops.Models.User", "Sender")
                        .WithMany()
                        .HasForeignKey("SenderID");
                });

            modelBuilder.Entity("fantasy_hoops.Models.Injuries", b =>
                {
                    b.HasOne("fantasy_hoops.Models.Player", "Player")
                        .WithMany()
                        .HasForeignKey("PlayerID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("fantasy_hoops.Models.Lineup", b =>
                {
                    b.HasOne("fantasy_hoops.Models.Player", "Player")
                        .WithMany("Lineups")
                        .HasForeignKey("PlayerID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("fantasy_hoops.Models.User", "User")
                        .WithMany("Lineups")
                        .HasForeignKey("UserID");
                });

            modelBuilder.Entity("fantasy_hoops.Models.Notification", b =>
                {
                    b.HasOne("fantasy_hoops.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserID");
                });

            modelBuilder.Entity("fantasy_hoops.Models.Paragraph", b =>
                {
                    b.HasOne("fantasy_hoops.Models.News", "News")
                        .WithMany("Paragraphs")
                        .HasForeignKey("NewsID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("fantasy_hoops.Models.Player", b =>
                {
                    b.HasOne("fantasy_hoops.Models.Team", "Team")
                        .WithMany("Players")
                        .HasForeignKey("TeamID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("fantasy_hoops.Models.Stats", b =>
                {
                    b.HasOne("fantasy_hoops.Models.Player", "Player")
                        .WithMany("Stats")
                        .HasForeignKey("PlayerID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("fantasy_hoops.Models.User", b =>
                {
                    b.HasOne("fantasy_hoops.Models.Team", "Team")
                        .WithMany("Users")
                        .HasForeignKey("TeamID");
                });

            modelBuilder.Entity("fantasy_hoops.Models.Notifications.FriendRequestNotification", b =>
                {
                    b.HasOne("fantasy_hoops.Models.User", "Friend")
                        .WithMany()
                        .HasForeignKey("FriendID");
                });

            modelBuilder.Entity("fantasy_hoops.Models.Notifications.InjuryNotification", b =>
                {
                    b.HasOne("fantasy_hoops.Models.Player", "Player")
                        .WithMany()
                        .HasForeignKey("PlayerID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
