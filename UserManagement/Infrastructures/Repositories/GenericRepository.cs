﻿using Application.Interfaces;
using Application.Interfaces.IRepositories;
using Application.Interfaces.IServices;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructures.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly UserManagementDbContext _context;
        protected DbSet<T> _dbSet;
        private readonly ICurrentTime _timeService;
        private readonly IClaimsService _claimsService;

        public GenericRepository(UserManagementDbContext context, ICurrentTime timeService, IClaimsService claimsService)
        {
            _context = context;
            _dbSet = context.Set<T>();
            _timeService = timeService;
            _claimsService = claimsService;
        }
        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, string? includeProperties = null)
        {
            IQueryable<T> query = _dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' },
                             StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return await query.ToListAsync();
        }

        public async Task<T?> GetAsync(Expression<Func<T, bool>>? filter = null, string? includeProperties = null)
        {
            IQueryable<T> query = _dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' },
                             StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return await query.FirstOrDefaultAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            var result = await _dbSet.FirstOrDefaultAsync(x => x.Id == id);
            // todo should throw excepted when not found
            return result;
        }
        public async Task AddAsync(T entity)
        {
            entity.CreatedDate = _timeService.GetCurrentTime();
            entity.CreatedBy = _claimsService.GetCurrentUserName;
            await _dbSet.AddAsync(entity);
        }

        public void SoftRemove(T entity)
        {
            entity.IsDeleted = true;
            entity.DeletedBy = _claimsService.GetCurrentUserName;
            entity.DeletedDate = _timeService.GetCurrentTime();
            _dbSet.Update(entity);
        }

        public void Update(T entity)
        {
            entity.ModifiedDate = _timeService.GetCurrentTime();
            entity.ModifiedBy = _claimsService.GetCurrentUserName;
            _dbSet.Update(entity);
        }

        public async Task AddRangeAsync(List<T> entities)
        {
            foreach (var entity in entities)
            {
                entity.CreatedDate = _timeService.GetCurrentTime();
                entity.CreatedBy = _claimsService.GetCurrentUserName;
            }
            await _dbSet.AddRangeAsync(entities);
        }

        public void SoftRemoveRange(List<T> entities)
        {
            foreach (var entity in entities)
            {
                entity.IsDeleted = true;
                entity.DeletedDate = _timeService.GetCurrentTime();
                entity.DeletedBy = _claimsService.GetCurrentUserName;
            }
            _dbSet.UpdateRange(entities);
        }

        public Task DeleteAsync(T entity)
        {
            _context.Remove(entity);
            return Task.CompletedTask;
        }

        public Task DeleteRangeAsync(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
            return Task.CompletedTask;
        }

        public void UpdateRange(List<T> entities)
        {
            foreach (var entity in entities)
            {
                entity.CreatedDate = _timeService.GetCurrentTime();
                entity.CreatedBy = _claimsService.GetCurrentUserName;
            }
            _dbSet.UpdateRange(entities);
        }
    }
}
