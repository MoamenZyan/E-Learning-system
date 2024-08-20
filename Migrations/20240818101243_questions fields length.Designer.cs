﻿// <auto-generated />
using System;
using E_Learning_Platform_API.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace E_Learning_Platform_API.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20240818101243_questions fields length")]
    partial class questionsfieldslength
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("E_Learning_Platform_API.Domain.Entities.Certification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<DateTime>("IssueDate")
                        .HasColumnType("DateTime");

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.HasIndex("StudentId");

                    b.ToTable("Certifications", (string)null);
                });

            modelBuilder.Entity("E_Learning_Platform_API.Domain.Entities.Course", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("VARCHAR");

                    b.Property<int>("InstructorId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("VARCHAR");

                    b.HasKey("Id");

                    b.HasIndex("InstructorId");

                    b.ToTable("Courses", (string)null);
                });

            modelBuilder.Entity("E_Learning_Platform_API.Domain.Entities.Exam", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<TimeSpan>("Duration")
                        .HasColumnType("TIME");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("VARCHAR");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.ToTable("Exams", (string)null);
                });

            modelBuilder.Entity("E_Learning_Platform_API.Domain.Entities.ExamQuestion", b =>
                {
                    b.Property<int>("ExamId")
                        .HasColumnType("int");

                    b.Property<int>("QuestionId")
                        .HasColumnType("int");

                    b.HasKey("ExamId", "QuestionId");

                    b.ToTable("ExamQuestions", (string)null);
                });

            modelBuilder.Entity("E_Learning_Platform_API.Domain.Entities.Instructor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(144)
                        .HasColumnType("VARCHAR");

                    b.Property<string>("Fname")
                        .IsRequired()
                        .HasMaxLength(144)
                        .HasColumnType("VARCHAR");

                    b.Property<string>("Lname")
                        .IsRequired()
                        .HasMaxLength(144)
                        .HasColumnType("VARCHAR");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("VARCHAR");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("VARCHAR");

                    b.HasKey("Id");

                    b.ToTable("Instructors", (string)null);
                });

            modelBuilder.Entity("E_Learning_Platform_API.Domain.Entities.Lecture", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<string>("VideoPath")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("VARCHAR");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.ToTable("Lectures", (string)null);
                });

            modelBuilder.Entity("E_Learning_Platform_API.Domain.Entities.Question", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Answer")
                        .IsRequired()
                        .HasMaxLength(1024)
                        .HasColumnType("VARCHAR");

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1024)
                        .HasColumnType("VARCHAR");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.ToTable("Questions", (string)null);
                });

            modelBuilder.Entity("E_Learning_Platform_API.Domain.Entities.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(144)
                        .HasColumnType("VARCHAR");

                    b.Property<string>("Fname")
                        .IsRequired()
                        .HasMaxLength(144)
                        .HasColumnType("VARCHAR");

                    b.Property<string>("Lname")
                        .IsRequired()
                        .HasMaxLength(144)
                        .HasColumnType("VARCHAR");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("VARCHAR");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("VARCHAR");

                    b.HasKey("Id");

                    b.ToTable("Students", (string)null);
                });

            modelBuilder.Entity("E_Learning_Platform_API.Domain.Entities.StudentCourse", b =>
                {
                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<DateTime>("EntrollDate")
                        .HasColumnType("DateTime");

                    b.Property<int>("Progress")
                        .HasColumnType("INTEGER");

                    b.HasKey("StudentId", "CourseId");

                    b.HasIndex("CourseId");

                    b.ToTable("StudentCourses", (string)null);
                });

            modelBuilder.Entity("E_Learning_Platform_API.Domain.Entities.Certification", b =>
                {
                    b.HasOne("E_Learning_Platform_API.Domain.Entities.Course", "Course")
                        .WithMany("Certifications")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("E_Learning_Platform_API.Domain.Entities.Student", "Student")
                        .WithMany("OwnedCertifications")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("E_Learning_Platform_API.Domain.Entities.Course", b =>
                {
                    b.HasOne("E_Learning_Platform_API.Domain.Entities.Instructor", "Instructor")
                        .WithMany("OwnedCourses")
                        .HasForeignKey("InstructorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Instructor");
                });

            modelBuilder.Entity("E_Learning_Platform_API.Domain.Entities.Exam", b =>
                {
                    b.HasOne("E_Learning_Platform_API.Domain.Entities.Course", "Course")
                        .WithMany("CourseExams")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");
                });

            modelBuilder.Entity("E_Learning_Platform_API.Domain.Entities.ExamQuestion", b =>
                {
                    b.HasOne("E_Learning_Platform_API.Domain.Entities.Exam", "Exam")
                        .WithMany("Questions")
                        .HasForeignKey("ExamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("E_Learning_Platform_API.Domain.Entities.Question", "Question")
                        .WithMany("Exams")
                        .HasForeignKey("ExamId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Exam");

                    b.Navigation("Question");
                });

            modelBuilder.Entity("E_Learning_Platform_API.Domain.Entities.Lecture", b =>
                {
                    b.HasOne("E_Learning_Platform_API.Domain.Entities.Course", "Course")
                        .WithMany("CourseLectures")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");
                });

            modelBuilder.Entity("E_Learning_Platform_API.Domain.Entities.Question", b =>
                {
                    b.HasOne("E_Learning_Platform_API.Domain.Entities.Course", "Course")
                        .WithMany("CourseQuestions")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");
                });

            modelBuilder.Entity("E_Learning_Platform_API.Domain.Entities.StudentCourse", b =>
                {
                    b.HasOne("E_Learning_Platform_API.Domain.Entities.Course", "Course")
                        .WithMany("EnrolledStudents")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("E_Learning_Platform_API.Domain.Entities.Student", "Student")
                        .WithMany("EnrolledCourses")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("E_Learning_Platform_API.Domain.Entities.Course", b =>
                {
                    b.Navigation("Certifications");

                    b.Navigation("CourseExams");

                    b.Navigation("CourseLectures");

                    b.Navigation("CourseQuestions");

                    b.Navigation("EnrolledStudents");
                });

            modelBuilder.Entity("E_Learning_Platform_API.Domain.Entities.Exam", b =>
                {
                    b.Navigation("Questions");
                });

            modelBuilder.Entity("E_Learning_Platform_API.Domain.Entities.Instructor", b =>
                {
                    b.Navigation("OwnedCourses");
                });

            modelBuilder.Entity("E_Learning_Platform_API.Domain.Entities.Question", b =>
                {
                    b.Navigation("Exams");
                });

            modelBuilder.Entity("E_Learning_Platform_API.Domain.Entities.Student", b =>
                {
                    b.Navigation("EnrolledCourses");

                    b.Navigation("OwnedCertifications");
                });
#pragma warning restore 612, 618
        }
    }
}
