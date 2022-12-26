using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.LibrarySys.Core.Models
{
    public class BookImagesConfig : IEntityTypeConfiguration<BookImages>
    {
        public void Configure(EntityTypeBuilder<BookImages> builder)
        {
            //builder.ToTable("BookImage");
            builder.HasKey(i => i.ID);
            builder.Property(i => i.ID).ValueGeneratedOnAdd();
            builder.Property(i => i.BookID).IsRequired();
            builder.Property(i => i.Path).IsRequired().HasMaxLength(200);
        }
    }
}
