﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using reqMan.Data;

namespace reqMan.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20181208231042_R14")]
    partial class R14
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.3-rtm-32065")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("Relational:Sequence:shared.RequestActionSequence", "'RequestActionSequence', 'shared', '100000', '1', '', '', 'Int32', 'False'")
                .HasAnnotation("Relational:Sequence:shared.RequestSequence", "'RequestSequence', 'shared', '100000', '1', '', '', 'Int32', 'False'")
                .HasAnnotation("Relational:Sequence:shared.RequestTypeSequence", "'RequestTypeSequence', 'shared', '100000', '1', '', '', 'Int32', 'False'")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("reqMan.Models.Request", b =>
                {
                    b.Property<string>("RequestId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AttachmentPath");

                    b.Property<float>("CurrentAmount");

                    b.Property<float>("CurrentPensionPerecentage");

                    b.Property<string>("CyclePartnerList");

                    b.Property<string>("CycleSchemeRequest");

                    b.Property<DateTime>("DateModified")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<DateTime>("DateRequested")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("Description");

                    b.Property<bool>("OptOut");

                    b.Property<string>("RequestTypeId");

                    b.Property<float>("RevisedAmount");

                    b.Property<float>("RevisedPensionPerecentage");

                    b.Property<DateTime>("StartDate");

                    b.Property<string>("State");

                    b.Property<string>("TasteCardLink");

                    b.Property<string>("Username");

                    b.Property<string>("ZoneFrom");

                    b.Property<string>("ZoneTo");

                    b.HasKey("RequestId");

                    b.HasIndex("RequestTypeId");

                    b.HasIndex("Username");

                    b.ToTable("Requests");
                });

            modelBuilder.Entity("reqMan.Models.RequestAction", b =>
                {
                    b.Property<int>("RequestActionSeq")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("NEXT VALUE FOR shared.RequestActionSequence");

                    b.Property<string>("Action");

                    b.Property<string>("Comment");

                    b.Property<DateTime>("Date")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("RequestId");

                    b.Property<string>("Username");

                    b.HasKey("RequestActionSeq");

                    b.HasIndex("RequestId");

                    b.HasIndex("Username");

                    b.ToTable("RequestActions");
                });

            modelBuilder.Entity("reqMan.Models.RequestType", b =>
                {
                    b.Property<string>("RequestTypeId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("FormPath");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int>("RequestTypeSeq")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("NEXT VALUE FOR shared.RequestTypeSequence");

                    b.HasKey("RequestTypeId");

                    b.ToTable("RequestType");
                });

            modelBuilder.Entity("reqMan.Models.User", b =>
                {
                    b.Property<string>("Username")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<string>("Password");

                    b.Property<string>("UserType");

                    b.HasKey("Username");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("reqMan.Models.Request", b =>
                {
                    b.HasOne("reqMan.Models.RequestType", "RequestType")
                        .WithMany()
                        .HasForeignKey("RequestTypeId");

                    b.HasOne("reqMan.Models.User", "User")
                        .WithMany("Requests")
                        .HasForeignKey("Username");
                });

            modelBuilder.Entity("reqMan.Models.RequestAction", b =>
                {
                    b.HasOne("reqMan.Models.Request", "Request")
                        .WithMany("RequestActions")
                        .HasForeignKey("RequestId");

                    b.HasOne("reqMan.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("Username");
                });
#pragma warning restore 612, 618
        }
    }
}
