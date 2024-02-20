namespace Core.Enums
{
    public enum EnumExportType
    {
        Template = 1,
    }

    public enum EnmUseQueue
    {
        No = 0,
        Yes = 1
    }

    public enum UserType
    {
        Admin = 1,
        Other = 2
    }

    public enum EnumRequestStatus
    {
        Requested = 3,
        ApprovedByDeptHOD = 4,
        ApprovedByLegalHOD = 5,
        Drafts = 6,
        Negotiation = 7,
        Execution = 8,
        Executed = 9,
        SharedWithCustomer = 10,
        SendBack = 11,
        Hold = 12,
        Reject = 13,
        Release = 19,
        ShareWithLegalDepartment = 22,
        Acknowlege = 23,
        Customer = 30,
        Legal = 31,
        SubmissionOfHardCopy = 32,
        ConfirmationOfHardCopy = 33,
        SubmissionOfCustomerSignedCopy = 34,
        ConfirmationOfSignedCopy = 35
    }

    public enum EnumRoleType
    {
        Administrator = 14,
        DepartmentUser = 15,
        DepartmentHOD = 16,
        LegalHOD = 17,
        LegalUser = 18,
    }
}