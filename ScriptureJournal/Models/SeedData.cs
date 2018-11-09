using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace ScriptureJournal.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ScriptureJournalContext(
                serviceProvider.GetRequiredService<DbContextOptions<ScriptureJournalContext>>()))
            {
                // Look for any notes
                if (context.Scripture.Any())
                {
                    return; // Don't seed DB if it has content.
                }
                context.Scripture.AddRange(
                    new Scripture
                    {
                        Book = "1 Nephi",
                        Chapter = 3,
                        Verse = 7,
                        Note = "If we trust in the Lord, he will make a way to keep his commandments.",
                        DateAdded = DateTime.Parse("2018-5-7")
                    },
                    new Scripture
                    {
                        Book = "1 Nephi",
                        Chapter = 11,
                        Verse = 1,
                        Note = "Nephi had a desire and believed the Lord would answer his prayers.",
                        DateAdded = DateTime.Parse("2018-5-7")
                    },
                    new Scripture
                    {
                        Book = "2 Nephi",
                        Chapter = 4,
                        Verse = 29,
                        Note = "Nephi needed to forgive his enemies so they would not be able to destroy his peace.",
                        DateAdded = DateTime.Parse("2018-6-15")
                    },
                    new Scripture
                    {
                        Book = "Alma",
                        Chapter = 9,
                        Verse = 6,
                        Note = "The Lord knows people's hearts and prepares us to teach them and gives us what we need.",
                        DateAdded = DateTime.Parse("2018-9-26")
                    }
                    );
                context.SaveChanges();
            }
        }
    }
}
