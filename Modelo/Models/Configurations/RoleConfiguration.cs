﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modelo.Models;
using System;
using System.Collections.Generic;

namespace Modelo.Models.Configurations
{
    public partial class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> entity)
        {
            entity.Property(e => e.Id).HasColumnName("id");

            entity.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("description");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<Role> entity);
    }
}
