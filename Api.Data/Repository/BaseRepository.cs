using Api.Data.Context;
using Api.Domain.Entities;
using Api.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Data.Repository
{
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly DataContext _context;
        private DbSet<T> _dataset;
        public BaseRepository(DataContext context)
        {
            _context = context;
            _dataset = _context.Set<T>();
        }

        public async Task<bool> ExistAsync(Guid id)
        {
            try
            {
                return await _dataset.AnyAsync(x => x.Id.Equals(id));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                var entityResult = await _dataset.SingleOrDefaultAsync(x => x.Id.Equals(id));
                if (entityResult == null)
                {
                    return false;
                }

                _dataset.Remove(entityResult);

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<T> InsertAsync(T entity)
        {
            try
            {
                if (entity.Id == Guid.Empty)
                {
                    entity.Id = Guid.NewGuid();
                }

                entity.CreateAt = DateTime.UtcNow;
                _dataset.Add(entity);

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }


            return entity;
        }

        public async Task<T> SelectAsync(Guid id)
        {
            try
            {
                return await _dataset.SingleOrDefaultAsync(x => x.Id.Equals(id));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<T>> SelectAsync()
        {
            try
            {
                return await _dataset.ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<T> UpdateAsync(T entity)
        {
            try
            {
                var entityResult = await _dataset.SingleOrDefaultAsync(x => x.Id == entity.Id);

                if (entityResult == null)
                {
                    return null;
                }

                entity.UpdateAt = DateTime.UtcNow;
                entity.CreateAt = entityResult.CreateAt;

                _context.Entry(entityResult).CurrentValues.SetValues(entity);

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return entity;

        }
    }
}
