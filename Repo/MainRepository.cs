using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using ReturnToSport.Data;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ReturnToSport.Repo
{
    public class MainRepository<T> : IMainRepository<T> where T : BaseEntity
    {
        private readonly MainContext _context;
        private readonly DbSet<T> entities;

        public MainRepository(MainContext context)
        {
            _context = context;
            entities = context.Set<T>();
        }

        #region sync Action Implementations
        public long Ekle(T entity)
        {
            _context.Add(entity);
            return entity.Id;
        }

        public void TopluEkle(IEnumerable<T> entities)
        {
            _context.AddRange(entities);
        }

        public T Getir(long id)
        {
            return entities.SingleOrDefault(s => s.Id == id);
        }
        public T Getir(long id, string include)
        {
            return entities.Include(include).SingleOrDefault(s => s.Id == id);
        }
        public T Getir(long id, params Expression<Func<T, object>>[] includeExpressions)
        {
            IQueryable<T> set = entities;
            foreach (var property in includeExpressions)
            {
                set = set.Include(property);
            }
            return set.SingleOrDefault(s => s.Id == id);
        }


        public IQueryable<T> TumunuGetir()
        {
            return entities.AsQueryable();
        }
        public IQueryable<T> TumunuGetir(string include)
        {
            return entities.Include(include).AsQueryable();
        }
        public IQueryable<T> TumunuGetir(string[] include )
        {
            foreach (var property in include)
            {
                entities.Include(property);
            }
            return entities.AsQueryable().AsNoTracking();
        }

        public IQueryable<T> TumunuOkunanGetir()
        {
            return entities.AsQueryable().AsNoTracking();
        }
        public IQueryable<T> TumunuOkunanGetir(string include)
        {
            return entities.Include(include).AsQueryable().AsNoTracking();
        }
        public IQueryable<T> TumunuOkunanGetir(params Expression<Func<T, object>>[] includeExpressions)
        {
            IQueryable<T> set = entities;
            foreach (var property in includeExpressions)
            {
                set = set.Include(property);
            }
            return set.AsQueryable();
        }

        public void Guncelle(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Update(entity);
        }

        public void GuncelleVeAktifHaleGetir(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Update(entity);
        }

        public void KaliciSil(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
        }

        public void GeciciSil(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Update(entity);
        }

        public void TopluSil(IEnumerable<T> entities)
        {
            _context.RemoveRange(entities);
        }

        public void DegisiklikleriKaydet()
        {
            _context.SaveChanges();
        }

        public void Initialize(IQueryable<T> entity)
        {
            _context.Database.EnsureCreated();

            // Tablo datasi varsa yeni veri ekleme
            if (entities.AsEnumerable().Any())
            {
                return;
            }
            foreach (T e in entity)
            {
                _context.Add(e);
                _context.SaveChanges();
            }

        }
#endregion

        #region async Action Implementations
        public async Task<long> EkleAsync(T entity)
        {
            await _context.AddAsync(entity);
            return entity.Id;
        }

        public async Task TopluEkleAsync(IEnumerable<T> entities)
        {
            await _context.AddRangeAsync(entities);
        }

        public async Task<T> GetirAsync(long id)
        {
            return await entities.SingleOrDefaultAsync(s => s.Id == id);
        }
        public async Task<T> GetirAsync(long id, string include)
        {
            return await entities.Include(include).SingleOrDefaultAsync(s => s.Id == id);
        }
        public async Task<T> GetirAsync(long id, params Expression<Func<T, object>>[] includeExpressions)
        {
            IQueryable<T> set = entities;
            foreach (var property in includeExpressions)
            {
                set = set.Include(property);
            }
            return await set.SingleOrDefaultAsync(s => s.Id == id);
        }

        public async Task<IQueryable<T>> TumunuGetirAsync()
        {
            return await Task.Run(() => entities.AsQueryable());
        }
        public async Task<IQueryable<T>> TumunuGetirAsync(string include)
        {
            return await Task.Run(() => entities.Include(include).AsQueryable());
        }
        public async Task<IQueryable<T>> TumunuGetirAsync(string[] include)
        {
            foreach (var property in include)
            {
                entities.Include(property);
            }
            return await Task.Run(() => entities.AsQueryable().AsNoTracking());
        }

        public async Task<IQueryable<T>> TumunuOkunanGetirAsync()
        {
            return await Task.Run(() => entities.AsQueryable().AsNoTracking());
        }
        public async Task<IQueryable<T>> TumunuOkunanGetirAsync(string include)
        {
            return await Task.Run(() => entities.Include(include).AsQueryable().AsNoTracking());
        }
        public async Task<IQueryable<T>> TumunuOkunanGetirAsync(params Expression<Func<T, object>>[] includeExpressions)
        {
            IQueryable<T> set = entities;
            foreach (var property in includeExpressions)
            {
                set = set.Include(property);
            }
            return await Task.Run(() => set.AsQueryable());
        }

        public async Task GuncelleAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            await Task.Run(() => entities.Update(entity));
        }

        public async Task GuncelleVeAktifHaleGetirAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            await Task.Run(() => entities.Update(entity));
        }

        public async Task KaliciSilAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            await Task.Run(() => entities.Remove(entity));
        }

        public async Task GeciciSilAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            await Task.Run(() => entities.Update(entity));
        }

        public async Task TopluSilAsync(IEnumerable<T> entities)
        {
            await Task.Run(() => _context.RemoveRange(entities));
        }

        public async Task DegisiklikleriKaydetAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task InitializeAsync(IQueryable<T> entity)
        {
            _context.Database.EnsureCreated();

            // Tablo datasi varsa yeni veri ekleme
            if (entities.AsEnumerable().Any())
            {
                return;
            }
            foreach (T e in entity)
            {
                await _context.AddAsync(e);
                await _context.SaveChangesAsync();
            }
        }
        #endregion
    }
}
