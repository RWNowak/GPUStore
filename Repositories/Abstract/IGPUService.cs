using GPUStoreMVC.Models.Data;
using GPUStoreMVC.Models.Other;

namespace GPUStoreMVC.Repositories.Abstract
{
    public interface IGPUService
    {
        bool Add(GPU model);
        bool Edit(GPU model);
        GPU GetById(int id);
        bool Delete(int id);
        GPUListVM List(string term = "", bool paging = false, int currentPage = 0);

    }
}
