using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EcommerceApp.Data.Entities;

namespace EcommerceApp.Data.Repositories.Contracts
{
    public interface ICategoryRepository : IRepository<Category>
    {
        public void Update(Category category);
    }
}