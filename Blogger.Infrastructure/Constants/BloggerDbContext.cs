using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Blogger.Domain.Models;

namespace Blogger.Infrastructure.Constants;

public partial class BloggerDbContext : DbContext
{
    public BloggerDbContext()
    {
    }

    public BloggerDbContext(DbContextOptions<BloggerDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Blog> Blogs { get; set; }

    public virtual DbSet<BlogReaction> BlogReactions { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<CommentReaction> CommentReactions { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Blog>(entity =>
        {
            entity.HasKey(e => e.BlogId).HasName("PK_Blog_BlogId");

            entity.ToTable("Blog");

            entity.HasIndex(e => e.BlogTitle, "IX_Blog_BlogTitle");

            entity.HasIndex(e => e.BlogTitle, "UNI_Blog_BlogTitle").IsUnique();

            entity.Property(e => e.BlogTitle).HasMaxLength(100);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Status).HasDefaultValue(1);
            entity.Property(e => e.Username)
                .HasMaxLength(25)
                .IsUnicode(false);

            entity.HasOne(d => d.Category).WithMany(p => p.Blogs)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK_Blog_CategoryId");

            entity.HasOne(d => d.UsernameNavigation).WithMany(p => p.Blogs)
                .HasForeignKey(d => d.Username)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Blog_Username");
        });

        modelBuilder.Entity<BlogReaction>(entity =>
        {
            entity.HasKey(e => new { e.BlogId, e.Username }).HasName("PK_BlogReaction_BlogId_Username");

            entity.ToTable("BlogReaction");

            entity.Property(e => e.Username)
                .HasMaxLength(25)
                .IsUnicode(false);
            entity.Property(e => e.ReactedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Blog).WithMany(p => p.BlogReactions)
                .HasForeignKey(d => d.BlogId)
                .HasConstraintName("FK_BlogReaction_BlogId");

            entity.HasOne(d => d.UsernameNavigation).WithMany(p => p.BlogReactions)
                .HasForeignKey(d => d.Username)
                .HasConstraintName("FK_BlogReaction_Username");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK_Category_CategoryId");

            entity.ToTable("Category");

            entity.HasIndex(e => e.CategoryName, "UNI_Category_CategoryName").IsUnique();

            entity.Property(e => e.CategoryName).HasMaxLength(100);
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.CommentId).HasName("PK_Comment_CommentId");

            entity.ToTable("Comment", tb => tb.HasTrigger("TR_DeleteChildComments"));

            entity.Property(e => e.CommentedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Username)
                .HasMaxLength(25)
                .IsUnicode(false);
            entity.Property(e => e.Value).IsUnicode(false);

            entity.HasOne(d => d.Blog).WithMany(p => p.Comments)
                .HasForeignKey(d => d.BlogId)
                .HasConstraintName("FK_Comment_BlogId");

            entity.HasOne(d => d.Parent).WithMany(p => p.InverseParent)
                .HasForeignKey(d => d.ParentId)
                .HasConstraintName("FK_Comment_ParentId");

            entity.HasOne(d => d.UsernameNavigation).WithMany(p => p.Comments)
                .HasForeignKey(d => d.Username)
                .HasConstraintName("FK_Comment_Username");
        });

        modelBuilder.Entity<CommentReaction>(entity =>
        {
            entity.HasKey(e => new { e.CommentId, e.Username }).HasName("PK_CommentReaction_CommentId_Username");

            entity.ToTable("CommentReaction");

            entity.Property(e => e.Username)
                .HasMaxLength(25)
                .IsUnicode(false);
            entity.Property(e => e.ReactedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Comment).WithMany(p => p.CommentReactions)
                .HasForeignKey(d => d.CommentId)
                .HasConstraintName("FK_CommentReaction_CommentId");

            entity.HasOne(d => d.UsernameNavigation).WithMany(p => p.CommentReactions)
                .HasForeignKey(d => d.Username)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CommentReaction_Username");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Username).HasName("PK_User_Username");

            entity.ToTable("User");

            entity.Property(e => e.Username)
                .HasMaxLength(25)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(25)
                .IsUnicode(false);
            entity.Property(e => e.Status).HasDefaultValue(1);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
