﻿// <auto-generated />
using FormBuilderDemo.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FormBuilderDemo.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("FormBuilderDemo.Enities.Form", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.ToTable("Form_tb");
                });

            modelBuilder.Entity("FormBuilderDemo.Enities.FormData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("FormId")
                        .HasColumnType("int");

                    b.Property<string>("FormValue")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("FormId");

                    b.ToTable("FormData_tb");
                });

            modelBuilder.Entity("FormBuilderDemo.Enities.FormField", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("FieldType")
                        .HasColumnType("int");

                    b.Property<int>("FormId")
                        .HasColumnType("int");

                    b.Property<bool>("IsRequired")
                        .HasColumnType("bit");

                    b.Property<string>("MaxLength")
                        .HasMaxLength(4)
                        .HasColumnType("nvarchar(4)");

                    b.Property<string>("Options")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("UniqueName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("FormId");

                    b.ToTable("FormField_tb");
                });

            modelBuilder.Entity("FormBuilderDemo.Enities.FormData", b =>
                {
                    b.HasOne("FormBuilderDemo.Enities.Form", "Form")
                        .WithMany("FormDatas")
                        .HasForeignKey("FormId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Form");
                });

            modelBuilder.Entity("FormBuilderDemo.Enities.FormField", b =>
                {
                    b.HasOne("FormBuilderDemo.Enities.Form", "Form")
                        .WithMany("FormFields")
                        .HasForeignKey("FormId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Form");
                });

            modelBuilder.Entity("FormBuilderDemo.Enities.Form", b =>
                {
                    b.Navigation("FormDatas");

                    b.Navigation("FormFields");
                });
#pragma warning restore 612, 618
        }
    }
}
