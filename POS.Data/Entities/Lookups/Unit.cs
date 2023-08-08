using System;

namespace POS.Data
{
    public class Unit : BaseEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
