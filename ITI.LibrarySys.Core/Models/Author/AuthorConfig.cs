using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.LibrarySys.Core.Models
{
    public class AuthorConfig : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            //builder.ToTable("Author");
            builder.HasKey(i => i.ID);
            builder.Property(i => i.ID).IsRequired().ValueGeneratedOnAdd();
            builder.Property(i => i.Name).IsRequired().HasMaxLength(200);
        }
    }
}
