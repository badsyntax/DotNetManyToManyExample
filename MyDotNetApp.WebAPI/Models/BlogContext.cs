using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MyDotNetApp.Models
{
    public class BlogContext : DbContext
    {
        public DbSet<Post> Posts { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public string DbPath { get; private set; }

        public BlogContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = $"{path}{System.IO.Path.DirectorySeparatorChar}blog.db";
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new TagEntityTypeConfiguration());
            builder.ApplyConfiguration(new PostEntityTypeConfiguration());
        }
    }

    public class TagEntityTypeConfiguration : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> entity)
        {
            entity.HasIndex(e => e.Name)
               .IsUnique();
        }
    }

    public class PostEntityTypeConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> entity)
        {
            entity
                .HasMany(p => p.Tags)
                .WithMany(p => p.Posts)
                .UsingEntity<PostTag>(
                    j => j
                        .HasOne(pt => pt.Tag)
                        .WithMany(t => t.PostTags)
                        .HasForeignKey(pt => pt.TagId),
                    j => j
                        .HasOne(pt => pt.Post)
                        .WithMany(p => p.PostTags)
                        .HasForeignKey(pt => pt.PostId),
                    j => j
                        .HasKey(t => new { t.TagId, t.PostId })
                );
        }
    }
}
