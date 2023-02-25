using GPUStoreMVC.Models.Data;

namespace GPUStoreMVC.Models.Other
{
    public class GPUListVM
    {
        public IQueryable<GPU>? GPUList { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public string? Term { get; set; }
    }
}