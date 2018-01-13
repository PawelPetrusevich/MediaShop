namespace MediaShop.DataAccess.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;
    using AutoMapper;
    using MediaShop.Common.Interfaces.Repositories;
    using MediaShop.Common.Models;

    /// <summary>
    /// Class for work with repository
    /// </summary>
    public class CartRepository : ICartRepository<ContentCartDto>, IDisposable
    {
        protected readonly DbContext DbContext;

        protected readonly DbSet<ContentCart> DbSet;

        private bool disposed;

        public CartRepository(DbContext context)
        {
            this.DbContext = context;
            this.DbSet = this.DbContext.Set<ContentCart>();
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="CartRepository"/> class.
        /// </summary>
        ~CartRepository()
        {
            this.Dispose(false);
        }

        /// <summary>
        /// Method for add object type ContentCartDto
        /// </summary>
        /// <param name="modelDto">updating object</param>
        /// <returns>rezalt operation</returns>
        public ContentCartDto Add(ContentCartDto modelDto)
        {
            if (modelDto == null)
            {
                throw new ArgumentNullException(nameof(modelDto));
            }

            var model = Mapper.Map<ContentCart>(modelDto);
            model.CreatedDate = DateTime.Now;
            model.CreatorId = modelDto.CreatorId; // temporally
            var result = this.DbSet.Add(model);
            this.DbContext.SaveChanges();
            return Mapper.Map<ContentCartDto>(result);
        }

        /// <summary>
        /// Method for update object type ContentCartDto
        /// </summary>
        /// <param name="modelDto">updating object</param>
        /// <returns>rezalt operation</returns>
        public ContentCartDto Update(ContentCartDto modelDto)
        {
            var model = Mapper.Map<ContentCart>(modelDto);
            model.ModifiedDate = DateTime.Now;
            model.ModifierId = modelDto.CreatorId; // temporally
            this.DbContext.Entry(model).State = EntityState.Modified;
            this.DbContext.SaveChanges();
            return Mapper.Map<ContentCartDto>(model);
        }

        /// <summary>
        /// Method for delete object type ContentCartDto
        /// </summary>
        /// <param name="modelDto">object for delete</param>
        /// <returns>rezalt operation</returns>
        public ContentCartDto Delete(ContentCartDto modelDto)
        {
            var model = Mapper.Map<ContentCart>(modelDto);
            var result = this.DbSet.Remove(model);
            this.DbContext.SaveChanges();
            return Mapper.Map<ContentCartDto>(result);
        }

        /// <summary>
        /// Method for delete object type ContentCartDto
        /// </summary>
        /// <param name="id">id for delete</param>
        /// <returns>rezalt operation</returns>
        public ContentCartDto Delete(long id)
        {
            var contentCart = this.DbSet.Where(x => x.Id == id
                && x.StateContent == Common.Enums.CartEnums.StateCartContent.InCart).SingleOrDefault();
            var result = this.DbSet.Remove(contentCart);
            this.DbContext.SaveChanges();
            return Mapper.Map<ContentCartDto>(result);
        }

        /// <summary>
        /// Method for find collection of object type ContentCartDto
        /// by predicate
        /// </summary>
        /// <param name="filter">predicate</param>
        /// <returns>collection objects</returns>
        public IEnumerable<ContentCartDto> Find(Expression<Func<ContentCartDto, bool>> filter)
        {
            // var result = this.DbSet.Where(filter);
            // return result;
            // For compilation solution!!!
            List<ContentCartDto> forCompillerList = new List<ContentCartDto>();
            return forCompillerList;
        }

        /// <summary>
        /// Method for getting object type ContentCartDto
        /// by identificator
        /// </summary>
        /// <param name="id">identificator</param>
        /// <returns>rezalt operation</returns>
        public ContentCartDto Get(long id)
        {
            var result = this.DbSet.Where(x => x.Id == id).SingleOrDefault();
            return Mapper.Map<ContentCartDto>(result);
        }

        /// <summary>
        /// Method for getting collection objects type ContentCartDto
        /// by user identificator
        /// </summary>
        /// <param name="userId">identificator user</param>
        /// <returns>rezalt operation</returns>
        public IEnumerable<ContentCartDto> GetAll(long userId)
        {
            var result = this.DbSet.Where(x => x.CreatorId == userId);
            return Mapper.Map<IEnumerable<ContentCart>, IEnumerable<ContentCartDto>>(result);
        }

        /// <summary>
        /// Releases resources
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
        }

        /// <summary>
        /// Releases resources
        /// </summary>
        /// <param name="flag">flag from where the method was called releases resources</param>
        protected virtual void Dispose(bool flag)
        {
            if (!this.disposed)
            {
                this.DbContext.Dispose();
                this.disposed = true;
                if (flag)
                {
                    GC.SuppressFinalize(this); // tell Garbage collector not to call the finalizer for the current object
                }
            }
        }
    }
}
