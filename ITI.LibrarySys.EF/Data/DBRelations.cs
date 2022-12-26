using ITI.LibrarySys.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.LibrarySys.EF
{
    public static class DBRelations
    {
        public static void MapRelations(this ModelBuilder builder)
        {
            //To make (1-m) relation between Book and Author Tables
            builder.Entity<Book>()
                .HasOne(b => b.Author)
                .WithMany(b => b.Books)
                .HasForeignKey(b => b.AuthorID)
                .IsRequired().OnDelete(DeleteBehavior.Cascade);

            //To make (1-m) relation between Book and BookImages Tables
            builder.Entity<BookImages>()
                .HasOne(bi => bi.Book)
                .WithMany(bi => bi.BookImages)
                .HasForeignKey(bi => bi.BookID)
                .IsRequired().OnDelete(DeleteBehavior.Cascade);
        }
    }
}
