using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vinformatix.Api.Data;

namespace ProjectZ.Data.Entities
{
    public class Image : IEntity
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public byte[] ImageBytes { get; set; }
    }

    public class ImageConfiguration : IEntityTypeConfiguration<Image>
    {
        public void Configure(EntityTypeBuilder<Image> builder)
        {
        }
    }
}
