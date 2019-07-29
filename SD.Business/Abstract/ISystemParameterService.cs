using SD.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SD.Business.Abstract
{
    public interface ISystemParameterService
    {
        List<SystemParameter> GetList();

        SystemParameter Get(Expression<Func<SystemParameter, bool>> filter);

        SystemParameter Add(SystemParameter entity);

        SystemParameter Update(SystemParameter entity);

        void Delete(SystemParameter entity);
    }
}
