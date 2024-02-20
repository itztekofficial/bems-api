using System.Collections.Generic;

namespace Core.Models.Response;
public class WorkFlowCombinedDataResponse
{
    public IEnumerable<CommonDropdown> EntityList { get; set; }
    public IEnumerable<CommonDropdown> StatusTypeList { get; set; }
    public IEnumerable<CommonDropdown> RoleTypeList { get; set; }
}