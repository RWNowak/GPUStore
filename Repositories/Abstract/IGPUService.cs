using GPUStoreMVC.Models.Data;

namespace GPUStoreMVC.Repositories.Abstract
{
    public interface IGPUService
    {
        bool Add(GPU model);
        bool Update(GPU model);
        GPU GetById(int id);
        bool Delete(int id);

    }
}
