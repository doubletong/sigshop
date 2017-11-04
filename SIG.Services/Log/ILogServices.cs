using System;
using System.Collections.Generic;
using System.Text;
using SIG.Repository.PagedList;

namespace SIG.Services.Log
{
    public interface ILogServices
    {
        IPagedList<Data.Entity.Log> SearchLogs(int pageIndex, int pageSize, DateTime? startDate, DateTime? expireDate,
            string level, out int count);
        void RemoveAll();
        void Delete(int id);
    }
}
