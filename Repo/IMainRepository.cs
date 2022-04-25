using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ReturnToSport.Repo
{
    /// <summary>
    /// Entity operasyonlari icin Generic repository interface
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IMainRepository<T>
    {
        IQueryable<T> TumunuGetir();
        IQueryable<T> TumunuGetir(string include);
        IQueryable<T> TumunuGetir(string[] include);
        IQueryable<T> TumunuOkunanGetir();
        IQueryable<T> TumunuOkunanGetir(string include);
        IQueryable<T> TumunuOkunanGetir(params Expression<Func<T, object>>[] includeExpressions);

        T Getir(long id);
        T Getir(long id, string include);
        T Getir(long id, params Expression<Func<T, object>>[] includeExpressions);
        void Initialize(IQueryable<T> entity);
        long Ekle(T entity);
        void TopluEkle(IEnumerable<T> entities);
        void Guncelle(T entity);
        void GuncelleVeAktifHaleGetir(T entity);
        void KaliciSil(T entity);
        void GeciciSil(T entity);
        void TopluSil(IEnumerable<T> entities);
        void DegisiklikleriKaydet();

        //Async actions
        Task<IQueryable<T>> TumunuGetirAsync();
        Task<IQueryable<T>> TumunuGetirAsync(string include);
        Task<IQueryable<T>> TumunuGetirAsync(string[] include);
        Task<IQueryable<T>> TumunuOkunanGetirAsync();
        Task<IQueryable<T>> TumunuOkunanGetirAsync(string include);
        Task<IQueryable<T>> TumunuOkunanGetirAsync(params Expression<Func<T, object>>[] includeExpressions);

        Task<T> GetirAsync(long id);
        Task<T> GetirAsync(long id, string include);
        Task<T> GetirAsync(long id, params Expression<Func<T, object>>[] includeExpressions);
        Task InitializeAsync(IQueryable<T> entity);
        Task<long> EkleAsync(T entity);
        Task TopluEkleAsync(IEnumerable<T> entities);
        Task GuncelleAsync(T entity);
        Task GuncelleVeAktifHaleGetirAsync(T entity);
        Task KaliciSilAsync(T entity);
        Task GeciciSilAsync(T entity);
        Task TopluSilAsync(IEnumerable<T> entities);
        Task DegisiklikleriKaydetAsync();
    }
}
