﻿using Entities.Concretes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.EntityConfigurations;

public class AccountConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.ToTable("Accounts").HasKey(a => a.Id);

        builder.Property(a => a.Id).HasColumnName("Id").IsRequired();
        builder.Property(a => a.UserId).HasColumnName("UserId").IsRequired();
        builder.Property(a => a.PhoneNumber).HasColumnName("PhoneNumber");
        builder.Property(a => a.NationalId).HasColumnName("NationalId");
        builder.Property(a => a.Description).HasColumnName("Description");
        builder.Property(a => a.BirthDate).HasColumnName("BirthDate");
        builder.Property(a => a.ProfilePhotoPath).HasColumnName("ProfilePhotoPath");

        builder.HasIndex(indexExpression: a => a.Id, name: "UK_Id").IsUnique();
        builder.HasIndex(indexExpression: a => a.UserId, name: "UK_UserId").IsUnique();


        builder.HasOne(a => a.User);
        builder.HasMany(a => a.WorkHours);

        builder.HasQueryFilter(a => !a.DeletedDate.HasValue);
    }
}