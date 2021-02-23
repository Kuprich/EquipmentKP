using Equipment.Database.Context;
using Equipment.Database.Entities.Base;
using Equipment.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Equipment.Database.Repositories.Base
{
    class DbRepositoryBase<T> : IRepository<T> where T: Entity, new()
    {
        private readonly EquipmentContext context;
        private readonly DbSet<T> dbSet;
        public bool autoSaveChanges { get; set; } = true;

        public DbRepositoryBase(EquipmentContext context)
        {
            this.context = context;
            dbSet = context.Set<T>();
        }

        public virtual IQueryable<T> Items => dbSet;

        public T Add(T item)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));
            context.Entry(item).State = EntityState.Added;
            if (autoSaveChanges)
                context.SaveChanges();
            return item;
        }
        public async Task<T> AddAsync(T item, CancellationToken cancel = default)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));
            context.Entry(item).State = EntityState.Added;
            if (autoSaveChanges)
                await context.SaveChangesAsync(cancel).ConfigureAwait(false);
            return item;
        }

        public T Get(int id) => Items.SingleOrDefault(i => i.Id == id);
        public async Task<T> GetAsync(int id, CancellationToken cancel = default) => await Items
            .SingleOrDefaultAsync(i => i.Id == id)
            .ConfigureAwait(false);

        public void Remove(T item)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));
            context.Remove(item);
            if (autoSaveChanges)
                context.SaveChanges();
        }
        public async Task RemoveAsync(T item, CancellationToken cancel = default)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));
            context.Remove(item);
            if (autoSaveChanges)
                await context.SaveChangesAsync(cancel).ConfigureAwait(false);
        }

        public void Update(T item)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));
            context.Entry(item).State = EntityState.Modified;
            if (autoSaveChanges)
                context.SaveChanges();
        }

        public async Task UpdateAsync(T item, CancellationToken cancel = default)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));
            context.Entry(item).State = EntityState.Modified;
            if (autoSaveChanges)
                await context.SaveChangesAsync(cancel);
        }
    }
}
