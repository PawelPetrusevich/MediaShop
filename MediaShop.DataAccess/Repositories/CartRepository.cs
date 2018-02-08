namespace MediaShop.DataAccess.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using AutoMapper;
    using Base;
    using Common.Enums;
    using MediaShop.Common.Interfaces.Repositories;
    using MediaShop.Common.Models;
    using MediaShop.DataAccess.Context;

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
        /// Method for update object type ContentCart
        /// </summary>
        /// <param name="model">updating object</param>
        /// <returns>rezalt operation</returns>
        public override ContentCart Update(ContentCart model)
        {
            model.ModifiedDate = DateTime.Now;
            model.ModifierId = model.CreatorId; // get ModifaerId from token
            this.Context.Entry(model).State = EntityState.Modified;
            this.Context.SaveChanges();
            return model;
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
        /// Method for getting collection objects type ContentCart in state InCart
        /// by user identificator
        /// </summary>
        /// <param name="userId">identificator user</param>
        /// <returns>rezalt operation</returns>
        public IEnumerable<ContentCart> GetById(long userId)
        {
            var result = this.DbSet.AsNoTracking().Where(x => x.CreatorId == userId && x.StateContent == CartEnums.StateCartContent.InCart);
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
