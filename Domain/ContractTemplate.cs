using Microsoft.AspNetCore.Http;

namespace Core.DataModel
{
    public class ContractTemplate : BaseEntity
    {
        public IFormFile? File { get; set; }
        public string? FileExt { get; set; }
        public string? FileName { get; set; } //For UI
        public int Sequence { get; set; }
    }
}
