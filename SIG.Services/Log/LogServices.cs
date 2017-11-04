using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using SIG.Basic.Extensions;
using SIG.Repository;
using SIG.Repository.PagedList;

namespace SIG.Services.Log
{
    public class LogServices : ILogServices
    {
        private readonly IUnitOfWork _unitOfWork;
        public LogServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IPagedList<Data.Entity.Log> SearchLogs(int pageIndex, int pageSize, DateTime? startDate, DateTime? expireDate,
            string level, out int count)
        {
            var logs = _unitOfWork.GetRepository<Data.Entity.Log>();

            Expression<Func<Data.Entity.Log, bool>> expression = d=>d.Id>0;

            if (startDate != null)
            {
                expression = (d => d.Logged >= startDate);
            }

            if (expireDate != null)
            {
                expression = expression.AndAlso(l => l.Logged <= expireDate);
            }

            if (!string.IsNullOrEmpty(level))
                expression = expression.AndAlso(l => l.Level == level);

            count = _unitOfWork.GetRepository<Data.Entity.Log>().Count(expression);

            var result = _unitOfWork.GetRepository<Data.Entity.Log>().GetPagedList(predicate: expression, orderBy: d=>d.OrderBy(l=>l.Logged), pageIndex:pageIndex, pageSize:pageSize);
             
             //   .Skip(pageIndex * pageSize).Take(pageSize).AsEnumerable();

            return result;
        }

        public void RemoveAll()
        {
             _unitOfWork.GetRepository<Data.Entity.Log>().Delete(_unitOfWork.GetRepository<Data.Entity.Log>().GetAll());
            _unitOfWork.SaveChanges();
        }
        public void Delete(int id)
        {
            _unitOfWork.GetRepository<Data.Entity.Log>().Delete(id);
            _unitOfWork.SaveChanges();
        }
    }
}
