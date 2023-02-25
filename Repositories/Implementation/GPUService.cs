using GPUStoreMVC.Models.Data;
using GPUStoreMVC.Models.Other;
using GPUStoreMVC.Repositories.Abstract;

namespace GPUStoreMvc.Repositories.Implementation
{
    public class GPUService : IGPUService
    {
        private readonly DatabaseContext ctx;
        public GPUService(DatabaseContext ctx)
        {
            this.ctx = ctx;
        }
        public bool Add(GPU model)
        {
            try
            {
                ctx.GPUs.Add(model);
                ctx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Delete(int GPUID)
        {
            try
            {
                var data = this.GetById(GPUID);
                if (data == null)
                    return false;
                ctx.GPUs.Remove(data);
                ctx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public GPU GetById(int GPUID)
        {
            return ctx.GPUs.Find(GPUID);
        }

        public GPUListVM List(string term = "", bool paging = false, int currentPage = 0)
        {
            var data = new GPUListVM();

            var list = ctx.GPUs.ToList();

            if (!string.IsNullOrEmpty(term))
            {
                term = term.ToLower();
                list = list.Where(a => a.Name.ToLower().StartsWith(term)).ToList();
            }

            if (paging)
            {
                // here we will apply paging
                int pageSize = 5;
                int count = list.Count;
                int TotalPages = (int)Math.Ceiling(count / (double)pageSize);
                list = list.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
                data.PageSize = pageSize;
                data.CurrentPage = currentPage;
                data.TotalPages = TotalPages;
            }
            data.GPUList = list.AsQueryable();
            return data;
        }

        public bool Edit(GPU model)
        {
            try
            {
                ctx.GPUs.Update(model);
                ctx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}