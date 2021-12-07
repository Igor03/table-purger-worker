using System;

namespace JIigor.Projects.TablePurger.Database.Entities
{
    public partial class PurgeableRecord
    {
        public PurgeableRecord(int id, string name, DateTime creationDate)
        {
            Id = id;
            Name = name;
            CreationDate = creationDate;
        }

        public PurgeableRecord()
        {
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime CreationDate { get; set; }
    }
}
