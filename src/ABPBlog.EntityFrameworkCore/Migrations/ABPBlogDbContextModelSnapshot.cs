﻿// <auto-generated />
using System;
using ABPBlog.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ABPBlog.Migrations
{
    [DbContext(typeof(ABPBlogDbContext))]
    partial class ABPBlogDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity("ABPBlog.Entity.Topic", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content");

                    b.Property<DateTime>("CreateOn");

                    b.Property<string>("Email");

                    b.Property<DateTime>("LastReplyTime");

                    b.Property<string>("LastReplyUserId");

                    b.Property<int?>("LastReplyUserId1");

                    b.Property<int>("NodeId");

                    b.Property<int>("ReplyCount");

                    b.Property<string>("Title");

                    b.Property<int>("Top");

                    b.Property<int>("Type");

                    b.Property<string>("UserId");

                    b.Property<int?>("UserId1");

                    b.Property<int>("ViewCount");

                    b.HasKey("Id");

                    b.HasIndex("LastReplyUserId1");

                    b.HasIndex("NodeId");

                    b.HasIndex("UserId1");

                    b.ToTable("Topic");
                });

            modelBuilder.Entity("ABPBlog.Entity.TopicNode", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateOn");

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.Property<string>("NodeName");

                    b.Property<int>("Order");

                    b.Property<int>("ParentId");

                    b.HasKey("Id");

                    b.ToTable("TopicNode");
                });

            modelBuilder.Entity("ABPBlog.Entity.TopicReply", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateOn");

                    b.Property<string>("ReplyContent");

                    b.Property<string>("ReplyEmail");

                    b.Property<string>("ReplyUserId");

                    b.Property<int?>("ReplyUserId1");

                    b.Property<int>("TopicId");

                    b.HasKey("Id");

                    b.HasIndex("ReplyUserId1");

                    b.HasIndex("TopicId");

                    b.ToTable("TopicReply");
                });

            modelBuilder.Entity("ABPBlog.Entity.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Avatar");

                    b.Property<DateTime>("CreateOn");

                    b.Property<string>("Email");

                    b.Property<string>("GitHub");

                    b.Property<DateTime>("LastTime");

                    b.Property<string>("PassWord");

                    b.Property<string>("Profile");

                    b.Property<int>("Score");

                    b.Property<int>("TopicCount");

                    b.Property<int>("TopicReplyCount");

                    b.Property<string>("Url");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("ABPBlog.Entity.UserCollection", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateOn");

                    b.Property<int>("State");

                    b.Property<int>("TopicId");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("TopicId");

                    b.ToTable("UserCollection");
                });

            modelBuilder.Entity("ABPBlog.Entity.UserMessage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content");

                    b.Property<DateTime>("CreateOn");

                    b.Property<string>("ReceiveUserId");

                    b.Property<int?>("ReceiveUserId1");

                    b.Property<string>("SendUserId");

                    b.Property<int?>("SendUserId1");

                    b.Property<int>("State");

                    b.Property<string>("Type");

                    b.HasKey("Id");

                    b.HasIndex("ReceiveUserId1");

                    b.HasIndex("SendUserId1");

                    b.ToTable("UserMessage");
                });

            modelBuilder.Entity("ABPBlog.Entity.Topic", b =>
                {
                    b.HasOne("ABPBlog.Entity.User", "LastReplyUser")
                        .WithMany()
                        .HasForeignKey("LastReplyUserId1");

                    b.HasOne("ABPBlog.Entity.TopicNode", "Node")
                        .WithMany()
                        .HasForeignKey("NodeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ABPBlog.Entity.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId1");
                });

            modelBuilder.Entity("ABPBlog.Entity.TopicReply", b =>
                {
                    b.HasOne("ABPBlog.Entity.User", "ReplyUser")
                        .WithMany()
                        .HasForeignKey("ReplyUserId1");

                    b.HasOne("ABPBlog.Entity.Topic")
                        .WithMany("Replys")
                        .HasForeignKey("TopicId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ABPBlog.Entity.UserCollection", b =>
                {
                    b.HasOne("ABPBlog.Entity.Topic", "Topic")
                        .WithMany()
                        .HasForeignKey("TopicId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ABPBlog.Entity.UserMessage", b =>
                {
                    b.HasOne("ABPBlog.Entity.User", "ReceiveUser")
                        .WithMany()
                        .HasForeignKey("ReceiveUserId1");

                    b.HasOne("ABPBlog.Entity.User", "SendUser")
                        .WithMany()
                        .HasForeignKey("SendUserId1");
                });
#pragma warning restore 612, 618
        }
    }
}
