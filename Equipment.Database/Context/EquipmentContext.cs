using Equipment.Database.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Equipment.Database.Context
{
    public class EquipmentContext : DbContext
    {

        public DbSet<SubEquipmentType> SubEquipmentTypes { get; set; }
        public DbSet<MainEquipmentType> MainEquipmentTypes { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<MainEquipment> MainEquipments { get; set; }
        public DbSet<SubEquipment> SubEquipments { get; set; }
        public EquipmentContext(DbContextOptions<EquipmentContext> options) : base(options)
        {

        }
    }
}
