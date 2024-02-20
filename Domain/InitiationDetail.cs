using Core.DataModel;
using Microsoft.AspNetCore.Http;

namespace Domain;

public class InitiationDetail : BaseEntity
{
    public int DepartmentId { get; set; }
    public int RoleTypeId { get; set; }
    public int ReqRaisedRoleTypeId { get; set; }
    public string? RefId { get; set; } //RefId
    public string? CustomerName { get; set; } //NAME OF THE CUSTOMER
    public int EntityTypeId { get; set; } //TYPE OF ENTITY/CONSTITUTION OF FIRM
    public string? OtherCustomerType { get; set; }
    public string? EntityId { get; set; } //NAME OF OUR ENTITY
    public int CustomerTypeId { get; set; } //MANUFACTURING/ MARKETING/ANY
    public int AgreementId { get; set; } //TYPE OF AGREEMENT
    public string? AgreementOthers { get; set; } //COMMENT (IF ANY)
    public int PaymentTermId { get; set; } //PAYMENT TERM
    public string? PaymentTermOthers { get; set; } //COMMENT (IF ANY)
    public string? OfficeAddress { get; set; }
    public string? OfficeAddress1 { get; set; }
    public int OfficeCountryId { get; set; }
    public string? OfficeOtherCountry { get; set; }
    public int OfficeStateId { get; set; }
    public string? OfficeOtherState { get; set; }
    public int OfficeCityId { get; set; }
    public string? OfficeOtherCity { get; set; }
    public string? OfficePinNo { get; set; }
    public string? DLNumber { get; set; } //Drug Licence No

    public bool IsOfficeDLAddressSame { get; set; }

    public string? DLAddress { get; set; }
    public string? DLAddress1 { get; set; }
    public int DLCountryId { get; set; }
    public string? DLOtherCountry { get; set; }
    public int DLStateId { get; set; }
    public string? DLOtherState { get; set; }
    public int DLCityId { get; set; }
    public string? DLOtherCity { get; set; }
    public string? DLPinNo { get; set; }
    public string? PANNumber { get; set; }
    public string? GSTNumber { get; set; }

    public bool IsDLBillingAddressSame { get; set; }

    public string? BillingAddress { get; set; }
    public string? BillingAddress1 { get; set; }
    public int BillingCountryId { get; set; }
    public string? BillingOtherCountry { get; set; }
    public int BillingStateId { get; set; }
    public string? BillingOtherState { get; set; }
    public int BillingCityId { get; set; }
    public string? BillingOtherCity { get; set; }
    public string? BillingPinNo { get; set; }

    public bool IsBillingDeliveryAddressSame { get; set; }

    public string? DeliveryAddress { get; set; }
    public string? DeliveryAddress1 { get; set; }
    public int DeliveryCountryId { get; set; }
    public string? DeliveryOtherCountry { get; set; }
    public int DeliveryStateId { get; set; }
    public string? DeliveryOtherState { get; set; }
    public int DeliveryCityId { get; set; }
    public string? DeliveryOtherCity { get; set; }
    public string? DeliveryPinNo { get; set; }

    public int MarketedById { get; set; }
    public string? MarketedAddress { get; set; }

    public string? AuthorisedSignatoryName { get; set; }
    public string? ContactPersonName { get; set; }
    public string? ContactPersonMobile { get; set; }
    public string? ContactPersonEmail { get; set; }
    public string? Comment { get; set; }
    public List<IFormFile>? InitiationAttachments { get; set; }
    public IFormFile? PartyProfileSheet { get; set; }
    public IFormFile? DraftAgreement { get; set; }
    public int RequestStatusId { get; set; }
    public int SubStatusId { get; set; }
    public int SendBackFrom { get; set; }
    public int SendBackTo { get; set; }
    public int ApproverRoleType { get; set; }

    public string? PartyProfileSheet_FileName { get; set; } //For view only
    public string? DraftAgreement_FileName { get; set; } //For view only

    public string? ReplySendBackRemark { get; set; }

    //=====Legal HOD===
    public int TypeOfAgreementId { get; set; }

    public string? CommentAgreementType { get; set; }
    public int SubCategoryAgreementId { get; set; }
    public string? CommentSubCategoryAgreement { get; set; }
    public int AssignLegalUserId { get; set; }
    public string? CommentAssignLegalUser { get; set; }

    //====LegalUser====
    public List<IFormFile>? AttachmentLegalUser { get; set; }
    public List<IFormFile>? ReplyAttachment { get; set; }

    public string? CommentLegalUser { get; set; }
    public string? PendingDocIds { get; set; }
    public string? RemarksLegalUser { get; set; }
    public string? ReplyRemarks { get; set; }

    //====Pre Execution====
    public int PreExecutionStepId { get; set; }
    public string? PreExecutionProcessName { get; set; }
    public List<IFormFile>? PreExecutionAttachments { get; set; }
    public string? PreExecutionDate { get; set; }
    public string? PreExecutionRemarks { get; set; }

    //====Execution====
    public string? AgreementDate { get; set; }
    public string? EffectiveDate { get; set; }
    public int? TermValidityId { get; set; }
    public int? TermValidityNo { get; set; }
    public string? RenewalDueDate { get; set; }
    public int? YearNotice { get; set; }
    public int? MonthNotice { get; set; }
    public int? DayNotice { get; set; }
    public int? CountryId { get; set; }
    public int? StateId { get; set; }
    public string? OtherExecutionState { get; set; }
    public int? CityId { get; set; }
    public string? OtherExecutionCity { get; set; }
    public string? CommercialApprovedBy { get; set; }
    public string? LegalApprovedBy { get; set; }
    public string? ProductIds { get; set; }
    public string? CommentExecution { get; set; }

    public string? OtherRemarks { get; set; }
    public List<IFormFile>? AttachmentExecution { get; set; }
    public IFormFile? FinalAgreementFile { get; set; }
}

//public class DocumentDetail
//{
//    public string? DocumentName { get; set; }
//    public string? ContentType { get; set; }
//    public string? DocumentContent { get; set; }
//}

public class InitiationAttachment : BaseEntity
{
    public int InitiationId { get; set; }
    public int DocumentId { get; set; }
    public string? AttachmentName { get; set; }
    public string? FileName { get; set; }
    public string? Extension { get; set; }
    public int Sequence { get; set; }
}

public class InitiationUpdateStatus
{
    public int Id { get; set; }
    public int RoleTypeId { get; set; }
    public int RequestStatusId { get; set; }
    public int SendBackFrom { get; set; }
    public int SendBackTo { get; set; }
    public string? Remarks { get; set; }
    public int UpdatedById { get; set; }
}

public class BeforeExecutionModel
{
    public int Id { get; set; }
    public int RequestStatusId { get; set; }
    public int UpdatedById { get; set; }
}

public class UpdateLegaluserModel
{
    public int Id { get; set; }
    public int LegalUserId { get; set; }
    public string? Remarks { get; set; }
    public int UpdatedById { get; set; }
}

public class SharedWithCustomerModel
{
    public int Id { get; set; }
    public int RequestStatusId { get; set; }
    public int SharedDocumentModeId { get; set; }
    public int UpdatedById { get; set; }
}

public class SharedWithLegalAcknowlegeModel
{
    public int Id { get; set; }
    public int RequestStatusId { get; set; }
    public int UpdatedById { get; set; }
}

public class InitiationLegalHead : BaseEntity
{
    public int TypeOfAgreementId { get; set; }
    public string? CommentAgreementType { get; set; }
    public int SubCategoryAgreementId { get; set; }
    public string? CommentSubCategoryAgreement { get; set; }
    public int AssignLegalUserId { get; set; }
    public string? CommentAssignLegalUser { get; set; }
}

public class InitiationLegalUser : BaseEntity
{
    public string? FileName { get; set; }
    public string? Comment { get; set; }
    public string? PendingDocIds { get; set; }
    public string? Remarks { get; set; }
    public string? ReplyFileName { get; set; }
    public bool IsApproved { get; set; }
    public string? ReplyRemarks { get; set; }
}

public class InitiationPreExecution : BaseEntity
{
    public string? PreExecutionStep { get; set; }
    public string? PreExecutionProcessName { get; set; }
    public string? ActionDateStr { get; set; }
    public string? FileName { get; set; }
    public string? Remarks { get; set; }
}

public class InitiationExecution : BaseEntity
{
    public string? CommercialApprovedBy { get; set; }
    public string? LegalApprovedBy { get; set; }
    public DateTime? AgreementDate { get; set; }
    public DateTime? EffectiveDate { get; set; }
    public int? TermValidityId { get; set; }
    public int? TermValidityNo { get; set; }
    public DateTime? RenewalDueDate { get; set; }
    public int? YearNotice { get; set; }
    public int? MonthNotice { get; set; }
    public int? DayNotice { get; set; }
    public int? CountryId { get; set; }
    public int? StateId { get; set; }
    public string? OtherExecutionState { get; set; }
    public int? CityId { get; set; }
    public string? OtherExecutionCity { get; set; }
    public string? ProductIds { get; set; }
    public string? Comment { get; set; }
    public string? OtherRemarks { get; set; }
    public string? AttachmentFileName { get; set; }
    public string? FinalAgreementFileName { get; set; }

    public string? AgreementDateStr { get; set; }
    public string? EffectiveDateStr { get; set; }
    public string? RenewalDueDateStr { get; set; }
}