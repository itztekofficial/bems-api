namespace Core.DataModel
{
    public class Menu : BaseEntity
    {
        #region "private veriables"

        private string? _expIcon;
        private string? _collapsIcon;

        #endregion "private veriables"

        public int ParentId { get; set; }
        public string? Label { get; set; }
        public string? Icon { get; set; }
        public string? RouterLink { get; set; }
        public int Order { get; set; }
        public bool IsParent { get; set; }

        public string? ExpandedIcon { get => _expIcon ?? "pi pi-folder-open"; set => _expIcon = value; }
        public string? CollapsedIcon { get => _collapsIcon ?? "pi pi-folder"; set => _collapsIcon = value; }
    }
}