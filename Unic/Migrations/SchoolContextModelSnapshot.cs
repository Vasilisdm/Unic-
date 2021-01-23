﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Unic.Data;

namespace Unic.Migrations
{
    [DbContext(typeof(SchoolContext))]
    partial class SchoolContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("Unic.Models.Course", b =>
                {
                    b.Property<int>("CourseID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Credits")
                        .HasColumnType("INTEGER");

                    b.Property<int>("DepartmentID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("CourseID");

                    b.HasIndex("DepartmentID");

                    b.ToTable("Course");
                });

            modelBuilder.Entity("Unic.Models.CourseAssignment", b =>
                {
                    b.Property<int>("CourseID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("InstructorID")
                        .HasColumnType("INTEGER");

                    b.HasKey("CourseID", "InstructorID");

                    b.HasIndex("InstructorID");

                    b.ToTable("CourseAssignment");
                });

            modelBuilder.Entity("Unic.Models.Department", b =>
                {
                    b.Property<int>("DepartmentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Budget")
                        .HasColumnType("money");

                    b.Property<int?>("InstructorID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("BLOB");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("TEXT");

                    b.HasKey("DepartmentID");

                    b.HasIndex("InstructorID");

                    b.ToTable("Department");
                });

            modelBuilder.Entity("Unic.Models.Enrollment", b =>
                {
                    b.Property<int>("CourseID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("StudentID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("EnrollmentID")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Grade")
                        .HasColumnType("INTEGER");

                    b.HasKey("CourseID", "StudentID", "EnrollmentID");

                    b.HasIndex("StudentID");

                    b.ToTable("Enrollment");
                });

            modelBuilder.Entity("Unic.Models.Instructor", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("FirstMidName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT")
                        .HasColumnName("FirstName");

                    b.Property<DateTime>("HireDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.ToTable("Instructor");
                });

            modelBuilder.Entity("Unic.Models.OfficeAssignment", b =>
                {
                    b.Property<int>("InstructorID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Location")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("InstructorID");

                    b.ToTable("OfficeAssignment");
                });

            modelBuilder.Entity("Unic.Models.Student", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("EnrollmentDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstMidName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT")
                        .HasColumnName("FirstName");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.ToTable("Student");
                });

            modelBuilder.Entity("Unic.Models.Course", b =>
                {
                    b.HasOne("Unic.Models.Department", "Department")
                        .WithMany("Courses")
                        .HasForeignKey("DepartmentID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");
                });

            modelBuilder.Entity("Unic.Models.CourseAssignment", b =>
                {
                    b.HasOne("Unic.Models.Course", "Course")
                        .WithMany("CourseAssignments")
                        .HasForeignKey("CourseID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Unic.Models.Instructor", "Instructor")
                        .WithMany("CourseAssignments")
                        .HasForeignKey("InstructorID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("Instructor");
                });

            modelBuilder.Entity("Unic.Models.Department", b =>
                {
                    b.HasOne("Unic.Models.Instructor", "Administrator")
                        .WithMany()
                        .HasForeignKey("InstructorID");

                    b.Navigation("Administrator");
                });

            modelBuilder.Entity("Unic.Models.Enrollment", b =>
                {
                    b.HasOne("Unic.Models.Course", "Course")
                        .WithMany("Enrollments")
                        .HasForeignKey("CourseID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Unic.Models.Student", "Student")
                        .WithMany("Enrollments")
                        .HasForeignKey("StudentID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("Unic.Models.OfficeAssignment", b =>
                {
                    b.HasOne("Unic.Models.Instructor", "Instructor")
                        .WithOne("OfficeAssignment")
                        .HasForeignKey("Unic.Models.OfficeAssignment", "InstructorID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Instructor");
                });

            modelBuilder.Entity("Unic.Models.Course", b =>
                {
                    b.Navigation("CourseAssignments");

                    b.Navigation("Enrollments");
                });

            modelBuilder.Entity("Unic.Models.Department", b =>
                {
                    b.Navigation("Courses");
                });

            modelBuilder.Entity("Unic.Models.Instructor", b =>
                {
                    b.Navigation("CourseAssignments");

                    b.Navigation("OfficeAssignment");
                });

            modelBuilder.Entity("Unic.Models.Student", b =>
                {
                    b.Navigation("Enrollments");
                });
#pragma warning restore 612, 618
        }
    }
}
