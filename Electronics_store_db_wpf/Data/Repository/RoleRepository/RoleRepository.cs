using Electronics_store_db_wpf.Data.DatabaseModel;
using Electronics_store_db_wpf.Data.Repository.EFcommonRepository;
using System;

namespace Electronics_store_db_wpf.Data.Repository.RoleRepository
{
    internal class RoleRepository : EFcommonRepository<Role>, IRoleRepository
    {
        public RoleRepository(IServiceProvider _serviceProvider) : base(_serviceProvider)
        {
        }
    }
}
