using System.Collections.Generic;

namespace twmvc26.Services
{
    public interface IService<T> where T : class
    {
        List<T> GetAll();
    }
}