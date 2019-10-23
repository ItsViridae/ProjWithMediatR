using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vinformatix.Api.Data;

namespace ProjectZ.Data.Entities
{
    public class Association : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<UserAssociation> UserAssociations { get; set; }
    }

    public class AssociationConfiguration : IEntityTypeConfiguration<Association>
    {
        public void Configure(EntityTypeBuilder<Association> builder)
        {
            builder.Property(x => x.Name).HasMaxLength(256);
        }
    }
}
