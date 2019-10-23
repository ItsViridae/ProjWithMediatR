using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vinformatix.Api.Data;

namespace ProjectZ.Data.Entities
{
    public class UserAssociation : IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int AssociationId { get; set; }
        public Association Association { get; set; }
    }

    public class UserAssociationConfiguration : IEntityTypeConfiguration<UserAssociation>
    {
        public void Configure(EntityTypeBuilder<UserAssociation> builder)
        {
            builder.HasOne(x => x.User)
                .WithMany(x => x.UserAssociations);

            builder.HasOne(x => x.Association)
                .WithMany(x => x.UserAssociations);
        }
    }
}
