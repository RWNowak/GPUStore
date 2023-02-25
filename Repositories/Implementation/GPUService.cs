using GPUStoreMVC.Models.Data;
using GPUStoreMVC.Models.Other;
using GPUStoreMVC.Repositories.Abstract;
using System.Security.Cryptography.X509Certificates;

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

        public bool Delete(int id)
        {
            try
            {
                var data = this.GetById(id);
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

        public GPU GetById(int id)
        {
            return ctx.GPUs.Find(id);
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

        public bool Update(GPU model)
        {
            try
            {
                ctx.GPUs.Update(model);
                // we have to add these genre ids in movieGenre table
                ctx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public List<GPU> GetAll()
        {
            return ctx.GPUs.ToList();
        }
    }
}