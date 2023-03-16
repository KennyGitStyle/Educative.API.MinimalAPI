using Educative.Core.Entity;
using Educative.Infrastructure.Data.Context;
using Educative.Infrastructure.Interface;

namespace Educative.Infrastructure.Repository
{
    public class
    AddressRepository
    : GenericRepository<Address>, IAddressRepository
    {

        public AddressRepository(EducativeContext context) :
            base(context)
        {
            
        }
 
    }
}
