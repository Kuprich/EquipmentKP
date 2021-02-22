using Equipment.Database.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Equipment.Database.Context
{
    public class EquipContext : DbContext
    {
        public DbSet<EquipSubType> EquipSubTypes { get; set; }
        public DbSet<EquipType> EquipTypes { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Equip> Equips { get; set; }
        public EquipContext(DbContextOptions<EquipContext> options) : base(options)
        {

        }
    }
}
