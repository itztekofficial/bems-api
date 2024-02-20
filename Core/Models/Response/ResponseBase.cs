using System.Collections.Generic;

namespace Core.Models.Response
{
    public class ResponseBase
    {
        public int Id { get; set; }
    }

    public class CountryResponse : ResponseBase
    {
        public int Code { get; set; }
        public string Name { get; set; }
    }

    public class StateResponse : ResponseBase
    {
        public string CountryId { get; set; }
        public int Code { get; set; }
        public string Name { get; set; }
    }

    public class CommonDropdown : ResponseBase
    {
        public string Name { get; set; }
    }

    public class DocumentDropdown : ResponseBase
    {
        public int EntityTypeId { get; set; }
        public int CustomerTypeId { get; set; }
        public string Name { get; set; }
    }

    public class MasterResponse
    {
        public IEnumerable<CommonDropdown> CountryList { get; set; }

        //public IEnumerable<CommonDropdown> StateList { get; set; }
        public IEnumerable<CommonDropdown> EntityList { get; set; }

        public IEnumerable<CommonDropdown> EntityTypeList { get; set; }
        public IEnumerable<CommonDropdown> CustomerTypeList { get; set; }
        public IEnumerable<CommonDropdown> AgreementList { get; set; }
        public IEnumerable<CommonDropdown> PaymentTermList { get; set; }

        //====For Legal===
        public IEnumerable<CommonDropdown> AgreementSubCategoryList { get; set; }

        public IEnumerable<CommonDropdown> LegalUserList { get; set; }
        public IEnumerable<DocumentDropdown> DocumentList { get; set; }
        public IEnumerable<CommonDropdown> ProductList { get; set; }
        public IEnumerable<CommonDropdown> TermValidityList { get; set; }
        public IEnumerable<CommonDropdown> DocShareModeList { get; set; }
        public IEnumerable<CommonDropdown> RoleTypeList { get; set; }
        public IEnumerable<CommonDropdown> ConfirmationOfHardCopy { get; set; }
    }
}