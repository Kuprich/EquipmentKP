using Equipment.Database.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Equipment.Database.Context
{
    public class EquipmentContext : DbContext
    {
        public DbSet<Location> Locations { get; set; }
        public DbSet<EquipmentCategory> EquipmentCategories { get; set; }
        public DbSet<EquipmentType> EquipmentTypes { get; set; }
        public DbSet<RequestStatus> ApplicationStatuses { get; set; }
        public DbSet<Request> Applications { get; set; }
        public DbSet<Entities.Equipment> Equipments { get; set; }
        public DbSet<EquipmentsKit> EquipmentsKits { get; set; }

        public EquipmentContext(DbContextOptions<EquipmentContext> options) : base(options)
        {

        }
    }
}
