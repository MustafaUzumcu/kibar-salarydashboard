using SD.Business.Abstract;
using SD.DataAccess.Abstract;
using SD.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace SD.Business.Concrete
{
    public class SystemParameterService : ISystemParameterService
    {
        ISystemParameterDal _systemParameterDal;

        public SystemParameterService(ISystemParameterDal systemParameterDal)
        {
            _systemParameterDal = systemParameterDal;
        }

        public SystemParameter Add(SystemParameter systemParameter)
        {
            return _systemParameterDal.Add(systemParameter);
        }

        public void Delete(SystemParameter systemParameter)
        {
            _systemParameterDal.Delete(systemParameter);
        }

        public SystemParameter Get(Expression<Func<SystemParameter, bool>> filter)
        {
            return _systemParameterDal.Get(filter);
        }

        public List<SystemParameter> GetList()
        {
            return _systemParameterDal.GetList();
        }

        public SystemParameter Update(SystemParameter systemParameter)
        {
            return _systemParameterDal.Update(systemParameter);
        }
    }
}
