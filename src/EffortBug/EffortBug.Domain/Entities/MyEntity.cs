using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using NodaTime;

namespace EffortBug.Domain.Entities
{
    public class MyEntity
    {
        [Column("Date", TypeName = "Date")]
        private DateTime _date { get; set; }

        public int Id { get; set; }
        public string Name { get; set; }

        [NotMapped]
        public LocalDate Date
        {
            get => LocalDate.FromDateTime(_date);
            set => _date = value.ToDateTimeUnspecified();
        }

        public class Configuration : EntityTypeConfiguration<MyEntity>
        {
            public Configuration()
            {
                Property(p => p._date);
            }
        }
    }
}
