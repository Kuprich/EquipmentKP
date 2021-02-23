using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Equipment.Interfaces
{
    public interface IRepository<T> where T: class, IEntity, new()
    {
        /// <summary> данные из репозитория</summary>
        IQueryable<T> Items { get; }

        /// <summary>Получить объект по его Id</summary>
        T Get(int id);
        /// <summary>Асинхронная операция получения объекта по его Id</summary>
        Task<T> GetAsync(int id, CancellationToken cancel = default);

        /// <summary>Добавить объект в репозиторий</summary>
        T Add(T item);
        /// <summary>Асинхронное добавление объекта в репозиторий</summary>
        Task<T> AddAsync(T item, CancellationToken cancel = default);

        /// <summary>Удалить объект из репозитория</summary>
        void Remove(T item);
        /// <summary>Асинхронное удаление объекта из репозитория</summary>
        Task RemoveAsync(T item, CancellationToken cancel = default);

        /// <summary>Обновить объект в репозитории</summary>
        void Update(T item);
        /// <summary>Асинхронно обнолвение объекта в репозитории</summary>
        Task UpdateAsync(T item, CancellationToken cancel = default);
    }
}
