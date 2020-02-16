﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using fantasy_hoops.Database;

namespace fantasy_hoops.Migrations
{
    [DbContext(typeof(GameContext))]
    partial class GameContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasColumnType("nvarchar(256)")
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
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("fantasy_hoops.Models.Achievements.Achievement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CompletedMessage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("GoalBase")
                        .HasColumnType("int");

                    b.Property<string>("Icon")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Achievements");
                });

            modelBuilder.Entity("fantasy_hoops.Models.Achievements.UserAchievement", b =>
                {
                    b.Property<string>("UserID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AchievementID")
                        .HasColumnType("int");

                    b.Property<bool>("IsAchieved")
                        .HasColumnType("bit");

                    b.Property<int>("Level")
                        .HasColumnType("int");

                    b.Property<int>("LevelUpGoal")
                        .HasColumnType("int");

                    b.Property<double>("Progress")
                        .HasColumnType("float");

                    b.HasKey("UserID", "AchievementID");

                    b.HasIndex("AchievementID");

                    b.ToTable("UserAchievements");
                });

            modelBuilder.Entity("fantasy_hoops.Models.FriendRequest", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("ReceiverID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("SenderID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("ReceiverID");

                    b.HasIndex("SenderID");

                    b.ToTable("FriendRequests");
                });

            modelBuilder.Entity("fantasy_hoops.Models.Game", b =>
                {
                    b.Property<int>("GameID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AwayScore")
                        .HasColumnType("int");

                    b.Property<int>("AwayTeamID")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Date")
                        .HasColumnType("Date");

                    b.Property<int>("HomeScore")
                        .HasColumnType("int");

                    b.Property<int>("HomeTeamID")
                        .HasColumnType("int");

                    b.HasKey("GameID");

                    b.HasIndex("AwayTeamID");

                    b.HasIndex("HomeTeamID");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("fantasy_hoops.Models.Injury", b =>
                {
                    b.Property<int>("InjuryID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InjuryTitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Link")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PlayerID")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("InjuryID");

                    b.HasIndex("PlayerID")
                        .IsUnique();

                    b.ToTable("Injuries");
                });

            modelBuilder.Entity("fantasy_hoops.Models.News", b =>
                {
                    b.Property<int>("NewsID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("hTeamID")
                        .HasColumnType("int");

                    b.Property<int>("vTeamID")
                        .HasColumnType("int");

                    b.HasKey("NewsID");

                    b.HasIndex("hTeamID");

                    b.HasIndex("vTeamID");

                    b.ToTable("News");
                });

            modelBuilder.Entity("fantasy_hoops.Models.Notification", b =>
                {
                    b.Property<int>("NotificationID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("ReadStatus")
                        .HasColumnType("bit");

                    b.Property<string>("UserID")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("NotificationID");

                    b.HasIndex("UserID");

                    b.ToTable("Notifications");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Notification");
                });

            modelBuilder.Entity("fantasy_hoops.Models.Paragraph", b =>
                {
                    b.Property<int>("ParagraphID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NewsID")
                        .HasColumnType("int");

                    b.Property<int>("ParagraphNumber")
                        .HasColumnType("int");

                    b.HasKey("ParagraphID");

                    b.HasIndex("NewsID");

                    b.ToTable("Paragraphs");
                });

            modelBuilder.Entity("fantasy_hoops.Models.Player", b =>
                {
                    b.Property<int>("PlayerID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("AST")
                        .HasColumnType("float");

                    b.Property<string>("AbbrName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("BLK")
                        .HasColumnType("float");

                    b.Property<double>("FPPG")
                        .HasColumnType("float");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("GP")
                        .HasColumnType("int");

                    b.Property<int>("InjuryID")
                        .HasColumnType("int");

                    b.Property<bool>("IsInGLeague")
                        .HasColumnType("bit");

                    b.Property<bool>("IsPlaying")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NbaID")
                        .HasColumnType("int");

                    b.Property<string>("Number")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("PTS")
                        .HasColumnType("float");

                    b.Property<string>("Position")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<double>("REB")
                        .HasColumnType("float");

                    b.Property<double>("STL")
                        .HasColumnType("float");

                    b.Property<double>("TOV")
                        .HasColumnType("float");

                    b.Property<int>("TeamID")
                        .HasColumnType("int");

                    b.HasKey("PlayerID");

                    b.HasIndex("TeamID");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("fantasy_hoops.Models.Post", b =>
                {
                    b.Property<int>("PostID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AuthorID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Body")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PostID");

                    b.HasIndex("AuthorID");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("fantasy_hoops.Models.PushSubscription", b =>
                {
                    b.Property<string>("P256Dh")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Auth")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Endpoint")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("ExpirationTime")
                        .HasColumnType("float");

                    b.Property<string>("UserID")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("P256Dh");

                    b.HasIndex("UserID");

                    b.ToTable("PushSubscriptions");
                });

            modelBuilder.Entity("fantasy_hoops.Models.Stats", b =>
                {
                    b.Property<int>("StatsID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AST")
                        .HasColumnType("int");

                    b.Property<int>("BLK")
                        .HasColumnType("int");

                    b.Property<int>("DREB")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("Date");

                    b.Property<int>("FGA")
                        .HasColumnType("int");

                    b.Property<int>("FGM")
                        .HasColumnType("int");

                    b.Property<double>("FGP")
                        .HasColumnType("float");

                    b.Property<int>("FLS")
                        .HasColumnType("int");

                    b.Property<double>("FP")
                        .HasColumnType("float");

                    b.Property<int>("FTA")
                        .HasColumnType("int");

                    b.Property<int>("FTM")
                        .HasColumnType("int");

                    b.Property<double>("FTP")
                        .HasColumnType("float");

                    b.Property<double>("GS")
                        .HasColumnType("float");

                    b.Property<int?>("GameID")
                        .HasColumnType("int");

                    b.Property<string>("MIN")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OREB")
                        .HasColumnType("int");

                    b.Property<int>("OppID")
                        .HasColumnType("int");

                    b.Property<int>("PTS")
                        .HasColumnType("int");

                    b.Property<int>("PlayerID")
                        .HasColumnType("int");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<int>("STL")
                        .HasColumnType("int");

                    b.Property<string>("Score")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TOV")
                        .HasColumnType("int");

                    b.Property<int>("TPA")
                        .HasColumnType("int");

                    b.Property<int>("TPM")
                        .HasColumnType("int");

                    b.Property<double>("TPP")
                        .HasColumnType("float");

                    b.Property<int>("TREB")
                        .HasColumnType("int");

                    b.HasKey("StatsID");

                    b.HasIndex("GameID");

                    b.HasIndex("PlayerID");

                    b.ToTable("Stats");
                });

            modelBuilder.Entity("fantasy_hoops.Models.Team", b =>
                {
                    b.Property<int>("TeamID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Abbreviation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Color")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NbaID")
                        .HasColumnType("int");

                    b.Property<string>("NextOppFormatted")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("NextOpponentID")
                        .HasColumnType("int");

                    b.HasKey("TeamID");

                    b.HasIndex("NextOpponentID");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("fantasy_hoops.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("AvatarURL")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<int>("FavoriteTeamId")
                        .HasColumnType("int");

                    b.Property<bool>("IsSocialAccount")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Streak")
                        .HasColumnType("int");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("FavoriteTeamId");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("fantasy_hoops.Models.UserLineup", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CID")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("Date");

                    b.Property<double>("FP")
                        .HasColumnType("float");

                    b.Property<bool>("IsCalculated")
                        .HasColumnType("bit");

                    b.Property<int>("PfID")
                        .HasColumnType("int");

                    b.Property<int>("PgID")
                        .HasColumnType("int");

                    b.Property<int>("SfID")
                        .HasColumnType("int");

                    b.Property<int>("SgID")
                        .HasColumnType("int");

                    b.Property<string>("UserID")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ID");

                    b.HasIndex("CID");

                    b.HasIndex("PfID");

                    b.HasIndex("PgID");

                    b.HasIndex("SfID");

                    b.HasIndex("SgID");

                    b.HasIndex("UserID");

                    b.ToTable("UserLineups");
                });

            modelBuilder.Entity("fantasy_hoops.Models.GameScoreNotification", b =>
                {
                    b.HasBaseType("fantasy_hoops.Models.Notification");

                    b.Property<double>("Score")
                        .HasColumnType("float");

                    b.HasDiscriminator().HasValue("GameScoreNotification");
                });

            modelBuilder.Entity("fantasy_hoops.Models.Notifications.FriendRequestNotification", b =>
                {
                    b.HasBaseType("fantasy_hoops.Models.Notification");

                    b.Property<string>("FriendID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RequestMessage")
                        .HasColumnType("nvarchar(max)");

                    b.HasIndex("FriendID");

                    b.HasDiscriminator().HasValue("FriendRequestNotification");
                });

            modelBuilder.Entity("fantasy_hoops.Models.Notifications.InjuryNotification", b =>
                {
                    b.HasBaseType("fantasy_hoops.Models.Notification");

                    b.Property<string>("InjuryDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InjuryStatus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PlayerID")
                        .HasColumnType("int");

                    b.HasIndex("PlayerID");

                    b.HasDiscriminator().HasValue("InjuryNotification");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("fantasy_hoops.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("fantasy_hoops.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("fantasy_hoops.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("fantasy_hoops.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("fantasy_hoops.Models.Achievements.UserAchievement", b =>
                {
                    b.HasOne("fantasy_hoops.Models.Achievements.Achievement", "Achievement")
                        .WithMany()
                        .HasForeignKey("AchievementID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("fantasy_hoops.Models.User", "User")
                        .WithMany("Achievements")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
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

            modelBuilder.Entity("fantasy_hoops.Models.Game", b =>
                {
                    b.HasOne("fantasy_hoops.Models.Team", "AwayTeam")
                        .WithMany()
                        .HasForeignKey("AwayTeamID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("fantasy_hoops.Models.Team", "HomeTeam")
                        .WithMany()
                        .HasForeignKey("HomeTeamID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("fantasy_hoops.Models.Injury", b =>
                {
                    b.HasOne("fantasy_hoops.Models.Player", "Player")
                        .WithOne("Injury")
                        .HasForeignKey("fantasy_hoops.Models.Injury", "PlayerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("fantasy_hoops.Models.News", b =>
                {
                    b.HasOne("fantasy_hoops.Models.Team", "hTeam")
                        .WithMany()
                        .HasForeignKey("hTeamID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("fantasy_hoops.Models.Team", "vTeam")
                        .WithMany()
                        .HasForeignKey("vTeamID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
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
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("fantasy_hoops.Models.Player", b =>
                {
                    b.HasOne("fantasy_hoops.Models.Team", "Team")
                        .WithMany("Players")
                        .HasForeignKey("TeamID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("fantasy_hoops.Models.Post", b =>
                {
                    b.HasOne("fantasy_hoops.Models.User", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorID");
                });

            modelBuilder.Entity("fantasy_hoops.Models.PushSubscription", b =>
                {
                    b.HasOne("fantasy_hoops.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserID");
                });

            modelBuilder.Entity("fantasy_hoops.Models.Stats", b =>
                {
                    b.HasOne("fantasy_hoops.Models.Game", "Game")
                        .WithMany()
                        .HasForeignKey("GameID");

                    b.HasOne("fantasy_hoops.Models.Player", "Player")
                        .WithMany("Stats")
                        .HasForeignKey("PlayerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("fantasy_hoops.Models.Team", b =>
                {
                    b.HasOne("fantasy_hoops.Models.Team", "NextOpponent")
                        .WithMany()
                        .HasForeignKey("NextOpponentID");
                });

            modelBuilder.Entity("fantasy_hoops.Models.User", b =>
                {
                    b.HasOne("fantasy_hoops.Models.Team", "FavoriteTeam")
                        .WithMany()
                        .HasForeignKey("FavoriteTeamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("fantasy_hoops.Models.UserLineup", b =>
                {
                    b.HasOne("fantasy_hoops.Models.Player", "C")
                        .WithMany()
                        .HasForeignKey("CID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("fantasy_hoops.Models.Player", "Pf")
                        .WithMany()
                        .HasForeignKey("PfID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("fantasy_hoops.Models.Player", "Pg")
                        .WithMany()
                        .HasForeignKey("PgID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("fantasy_hoops.Models.Player", "Sf")
                        .WithMany()
                        .HasForeignKey("SfID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("fantasy_hoops.Models.Player", "Sg")
                        .WithMany()
                        .HasForeignKey("SgID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("fantasy_hoops.Models.User", "User")
                        .WithMany("UserLineups")
                        .HasForeignKey("UserID");
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
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
