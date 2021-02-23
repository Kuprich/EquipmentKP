using Equipment.Database.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Equipment.Database.Context
{
    public class EquipContext : DbContext
    {
        public DbSet<MainEquipmentType> EquipmentTypes { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<SubEquipmentType> SubEquipmentTypes { get; set; }
        public DbSet<Entities.MainEquipment> Equipment { get; set; }
        
        public EquipContext(DbContextOptions<EquipContext> options) : base(options)
        {

        }
    }
}
