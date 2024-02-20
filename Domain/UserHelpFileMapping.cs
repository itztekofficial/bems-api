using Microsoft.AspNetCore.Http;

namespace Core.DataModel
{
    public class UserHelpFileMapping : BaseEntity
    {
        public int RoleTypeId { get; set; }
        public IFormFile? File { get; set; }
        public string? FileExt { get; set; }
        public int Sequence { get; set; }

        public string? FileName { get; set; } //For UI
        public string? RoleType { get; set; } //For UI
    }
}