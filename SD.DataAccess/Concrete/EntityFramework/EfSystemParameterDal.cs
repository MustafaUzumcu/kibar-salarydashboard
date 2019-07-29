using Microsoft.EntityFrameworkCore;
using SD.Core.DataAccess.EntityFramework;
using SD.DataAccess.Abstract;
using SD.Entities;
using SD.Entities.Concrete;

namespace SD.DataAccess.Concrete.EntityFramework
{
    public class EfSystemParameterDal : EfEntityRepositoryBase<SystemParameter>, ISystemParameterDal
    {
        public EfSystemParameterDal(SDContext sdContext) : base(sdContext)
        {
        }


    }
}
