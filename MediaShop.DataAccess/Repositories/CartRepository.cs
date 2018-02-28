namespace MediaShop.DataAccess.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using Base;
    using Common.Enums;
    using MediaShop.Common.Interfaces.Repositories;
    using MediaShop.Common.Models;
    using MediaShop.DataAccess.Context;
    using MediaShop.Common.Exceptions.CartExceptions;
    using MediaShop.DataAccess.Properties;

    /// <summary>
    /// Class for work with repository
    /// </summary>
    public class CartRepository : Repository<ContentCart>, ICartRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CartRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public CartRepository(MediaContext context)
            : base(context)
        {
        }

        /// <summary>
        /// Method for add object type ContentCart
        /// </summary>
        /// <param name="model">updating object</param>
        /// <returns>rezalt operation</returns>
        public override ContentCart Add(ContentCart model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var result = this.DbSet.Add(model);
            this.Context.SaveChanges();
            return result;
        }

        /// <summary>
        /// Override async add model to repository
        /// </summary>
        /// <param name="model">model user</param>
        /// <returns>db entry</returns>
        /// <exception cref="ArgumentNullException">if model = null</exception>
        public override async Task<ContentCart> AddAsync(ContentCart model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var result = this.DbSet.Add(model);
            await this.Context.SaveChangesAsync().ConfigureAwait(false);
            return result;
        }

        /// <summary>
        /// Method for update object type ContentCart
        /// </summary>
        /// <param name="model">updating object</param>
        /// <returns>rezalt operation</returns>
        public override ContentCart Update(ContentCart model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var entity = this.Get(model.Id);
            entity.ModifiedDate = DateTime.Now;
            entity.ModifierId = model.CreatorId;
            this.Context.Entry(entity).State = EntityState.Modified;
            this.Context.SaveChanges();
            return entity;
        }

        /// <summary>
        /// Override async method update for type ContentCart
        /// </summary>
        /// <param name="model">Model to update</param>
        /// <returns>Updated model</returns>
        public override async Task<ContentCart> UpdateAsync(ContentCart model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var entity = await this.GetAsync(model.Id);
            entity = Mapper.Map(model, entity);
            entity.ModifiedDate = DateTime.Now;
            entity.ModifierId = model.CreatorId;
            this.Context.Entry(entity).State = EntityState.Modified;
            await this.Context.SaveChangesAsync().ConfigureAwait(false);
            return entity;
        }

        /// <summary>
        /// Method for delete object type ContentCart
        /// </summary>
        /// <param name="model">object for delete</param>
        /// <returns>rezalt operation</returns>
        public override ContentCart Delete(ContentCart model)
        {
            this.Context.Entry(model).State = EntityState.Deleted;
            this.Context.SaveChanges();
            return model;
        }

        /// <summary>
        /// Method for delete object type ContentCart
        /// </summary>
        /// <param name="model">object for delete</param>
        /// <returns>rezalt operation</returns>
        public async Task<int> DeleteAsync(ContentCart model)
        {
            this.Context.Entry(model).State = EntityState.Deleted;
            var result = await this.Context.SaveChangesAsync().ConfigureAwait(false);
            return result;
        }

        /// <summary>
        /// Method for delete object type ContentCart
        /// </summary>
        /// <param name="id">id for delete</param>
        /// <returns>rezalt operation</returns>
        public override ContentCart Delete(long id)
        {
            var contentCart = this.DbSet.SingleOrDefault(x => x.Id == id
                && x.StateContent == Common.Enums.CartEnums.StateCartContent.InCart);
            var result = this.DbSet.Remove(contentCart);
            this.Context.SaveChanges();
            return result;
        }

        /// <summary>
        /// Method for delete object type ContentCart
        /// </summary>
        /// <param name="id">id for delete</param>
        /// <returns>rezalt operation</returns>
        public async Task<int> DeleteAsync(long id)
        {
            var contentCart = this.DbSet.SingleOrDefault(x => x.Id == id
                && x.StateContent == Common.Enums.CartEnums.StateCartContent.InCart);
            if (contentCart == null)
            {
                throw new DeleteContentInCartExceptions(Resources.InvalidId);
            }

            var model = this.DbSet.Remove(contentCart);
            var result = await this.Context.SaveChangesAsync().ConfigureAwait(false);
            return result;
        }

        /// <summary>
        /// Method for getting object type ContentCart
        /// by Id
        /// </summary>
        /// <param name="id">identificator</param>
        /// <returns>rezalt operation</returns>
        public override ContentCart Get(long id)
        {
            var result = this.DbSet.AsNoTracking().SingleOrDefault(x => x.Id == id);
            return result;
        }

        /// <summary>
        /// Method for getting object type ContentCart
        /// by Id
        /// </summary>
        /// <param name="id">identificator</param>
        /// <returns>rezalt operation</returns>
        public async Task<ContentCart> GetAsync(long id)
        {
            var result = await this.DbSet.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id).ConfigureAwait(false);
            return result;
        }

        /// <summary>
        /// Method for getting collection objects type ContentCart in state InCart
        /// by user identificator
        /// </summary>
        /// <param name="userId">identificator user</param>
        /// <returns>rezalt operation</returns>
        public IEnumerable<ContentCart> GetById(long userId)
        {
            var result = this.DbSet.AsNoTracking().Where(x => x.CreatorId == userId && x.StateContent == CartEnums.StateCartContent.InCart).ToList();
            return result;
        }

                /// <summary>
        /// Method for getting collection objects type ContentCart in state InCart
        /// by user identificator
        /// </summary>
        /// <param name="userId">identificator user</param>
        /// <returns>rezalt operation</returns>
        public async Task<IEnumerable<ContentCart>> GetByIdAsync(long userId)
        {
            var result = await this.DbSet.AsNoTracking()
                .Where(x => x.CreatorId == userId && x.StateContent == CartEnums.StateCartContent.InCart).ToListAsync()
                .ConfigureAwait(false);
            return result;
        }

        /// <summary>
        /// Method for getting collection objects type ContentCart
        /// by user identificator
        /// </summary>
        /// <param name="userId">identificator user</param>
        /// <returns>rezalt operation</returns>
        public IEnumerable<ContentCart> GetAll(long userId)
        {
            var result = this.DbSet.AsNoTracking().Where(x => x.CreatorId == userId).ToList();
            return result;
        }
    }
}
