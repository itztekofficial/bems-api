CREATE TABLE [Country](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Code] [varchar](5) NULL, 
	[Name] [varchar](150) NULL,
	[Sequence] [int] NULL,
    [IsActive] [bit]  NULL,
	[CreatedById] [int] NULL,
	[CreateDate] [datetime] NULL,
	[ModifiedById] [int] NULL,
	[ModifyDate] [datetime] NULL,
 CONSTRAINT [PK_Country] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [State](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CountryId] [int]  NULL,
	[Code] [varchar](5) NULL, 
	[Name] [varchar](150) NULL,
	[Sequence] [int] NULL,
    [IsActive] [bit]  NULL,
	[CreatedById] [int] NULL,
	[CreateDate] [datetime] NULL,
	[ModifiedById] [int] NULL,
	[ModifyDate] [datetime] NULL,
 CONSTRAINT [PK_State] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [City](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StateId] [int]  NULL,
	[Code] [varchar](5) NULL, 
	[Name] [varchar](150) NULL,
	[Sequence] [int] NULL,
    [IsActive] [bit]  NULL,
	[CreatedById] [int] NULL,
	[CreateDate] [datetime] NULL,
	[ModifiedById] [int] NULL,
	[ModifyDate] [datetime] NULL,
 CONSTRAINT [PK_City] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [Company](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](150) NULL,
	[Code] [varchar](25) NULL, 
	[ContactPerson] [varchar](100) NULL,
	[Emailid] [varchar](100) NULL,
	[RegMobile] [varchar](15) NULL,
	[Address] [varchar](250) NULL,
	[GSTNNo] [varchar](25) NULL,
	[CountryId] [int] NULL,
	[StateId] [int] NULL,
	[CityId] [int] NULL,
	[PinCode] [varchar](10) NULL,
	[IsActive] [bit]  NULL,
	[CreatedById] [int] NULL,
	[CreateDate] [datetime] NULL,
	[ModifiedById] [int] NULL,
	[ModifyDate] [datetime] NULL,
 CONSTRAINT [PK_Company] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [LookUpType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CompanyId] [int] NULL,
	[Name] [varchar](50) NOT NULL,
	[CreatedById] [int] NULL,
	[CreateDate] [datetime] NULL,
	[ModifiedById] [int] NULL,
	[ModifyDate] [datetime] NULL,
	[IsActive] [int] NULL,
 CONSTRAINT [PK_LookUpType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [Lookup](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CompanyId] [int] NULL,
	[LookTypeId] [int] NULL,
	[Name] [varchar](50) NULL,
	[Description] [varchar](50) NULL,
	[Value] [varchar](10) NULL,
	[Sequence] [int] NULL,
	[IsActive] [bit] NULL,
	[CreatedById] [int] NULL,
	[CreateDate] [datetime] NULL,
	[ModifiedById] [int] NULL,
	[ModifyDate] [datetime] NULL,
 CONSTRAINT [PK_LookUp] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [Menu](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CompanyId] [int] NULL,
	[ParentId] [int] NULL,
	[Label] [varchar](50) NULL,
	[Icon] [varchar](25) NULL,
	[RouterLink] [varchar](250) NULL,
	[Order] [int] NULL,
	[IsParent] [bit]  NULL,
	[ExpandedIcon] [varchar](25) NULL,
	[CollapsedIcon] [varchar](25) NULL,
	[IsActive] [bit]  NULL,
	[CreatedById] [int] NULL,
	[CreateDate] [datetime] NULL,
	[ModifiedById] [int] NULL,
	[ModifyDate] [datetime] NULL,
 CONSTRAINT [PK_Menu] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [RoleMenu](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CompanyId] [int] NULL,
	[Name] [varchar](100) NULL,
	[MenuIds] [varchar](100) NULL,
	[IsAdmin] [bit]  NULL,
	[IsPredefined] [bit]  NULL,
	[IsActive] [bit]  NULL,
	[CreatedById] [int] NULL,
	[CreateDate] [datetime] NULL,
	[ModifiedById] [int] NULL,
	[ModifyDate] [datetime] NULL,
 CONSTRAINT [PK_RoleMenu] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [User](
	[Id] [int] IDENTITY(1,1) NOT NULL,	
	[CompanyId] [int] NULL,
	[Name] [varchar](100) NULL,
	[EmailId] [varchar](100) NULL,
	[MobileNo] [varchar](15) NULL,
	[UserPwd] [varchar](250) NULL,
	[UserType] [int] NULL,
	[IsAdmin] [bit] NULL,
	[IsPredefined] [bit] NULL,
	[IsLoggedIn] [bit] NULL,
	[LogoutDateTime] [datetime] NULL,
	[IsActive] [bit] NULL,
	[CreatedById] [int] NULL,
	[CreateDate] [datetime] NULL,
	[ModifiedById] [int] NULL,
	[ModifyDate] [datetime] NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [UserMapping](
	[Id] [int] IDENTITY(1,1) NOT NULL,	
	[CompanyId] [int] NULL,
	[RefUserId] [int] NULL,		
	[EntityIds] [varchar](100) NULL,	
	[DepartmentIds] [varchar](100) NULL,
	[RoleIds] [varchar](100) NULL,
	[IsExtraMenu] [bit] NULL,
	[MenuIds] [varchar](100) NULL,
	[RoleTypeId] [int] NULL,		
	[IsActive] [bit] NULL,
	[CreatedById] [int] NULL,
	[CreateDate] [datetime] NULL,
	[ModifiedById] [int] NULL,
	[ModifyDate] [datetime] NULL,
 CONSTRAINT [PK_UserMapping] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [Department](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CompanyId] [int] NULL,
	[Name] [varchar](100) NOT NULL,
	[Sequence] [int] NOT NULL,
	[CreatedById] [int] NULL,
	[CreateDate] [datetime] NULL,
	[ModifiedById] [int] NULL,
	[ModifyDate] [datetime] NULL,
	[IsActive] [int] NULL,
 CONSTRAINT [PK_WorkFlow] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [Entity](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CompanyId] [int] NULL,
	[Code] [varchar](15) NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[Sequence] [int] NOT NULL,
	[IsActive] [int] NULL,
	[CreatedById] [int] NULL,
	[CreateDate] [datetime] NULL,
	[ModifiedById] [int] NULL,
	[ModifyDate] [datetime] NULL,
 CONSTRAINT [PK_Entity] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [EntityType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CompanyId] [int] NULL,
	[Name] [varchar](100) NOT NULL,
	[Sequence] [int] NOT NULL,
	[IsActive] [int] NULL,
	[CreatedById] [int] NULL,
	[CreateDate] [datetime] NULL,
	[ModifiedById] [int] NULL,
	[ModifyDate] [datetime] NULL,
 CONSTRAINT [PK_EntityType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [Agreement](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CompanyId] [int] NULL,
	[Name] [varchar](100) NOT NULL,
	[Sequence] [int] NOT NULL,
	[IsActive] [int] NULL,
	[CreatedById] [int] NULL,
	[CreateDate] [datetime] NULL,
	[ModifiedById] [int] NULL,
	[ModifyDate] [datetime] NULL,
 CONSTRAINT [PK_Agreement] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [SubAgreement](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CompanyId] [int] NULL,
	[Name] [varchar](100) NOT NULL,
	[Sequence] [int] NOT NULL,
	[IsActive] [int] NULL,
	[CreatedById] [int] NULL,
	[CreateDate] [datetime] NULL,
	[ModifiedById] [int] NULL,
	[ModifyDate] [datetime] NULL,
 CONSTRAINT [PK_SubAgreement] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [PaymentTerm](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CompanyId] [int] NULL,
	[Name] [varchar](100) NOT NULL,
	[Sequence] [int] NOT NULL,
	[IsActive] [int] NULL,
	[CreatedById] [int] NULL,
	[CreateDate] [datetime] NULL,
	[ModifiedById] [int] NULL,
	[ModifyDate] [datetime] NULL,
 CONSTRAINT [PK_PaymentTerm] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [Document](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CompanyId] [int] NULL,
	[EntityTypeId] [int] NULL,
	[Name] [varchar](100) NOT NULL,
	[Sequence] [int] NOT NULL,
	[IsActive] [int] NULL,
	[CreatedById] [int] NULL,
	[CreateDate] [datetime] NULL,
	[ModifiedById] [int] NULL,
	[ModifyDate] [datetime] NULL,
 CONSTRAINT [PK_Document] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [CustomerType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CompanyId] [int] NULL,
	[Name] [varchar](100) NOT NULL,
	[Sequence] [int] NOT NULL,
	[IsActive] [int] NULL,
	[CreatedById] [int] NULL,
	[CreateDate] [datetime] NULL,
	[ModifiedById] [int] NULL,
	[ModifyDate] [datetime] NULL,
 CONSTRAINT [PK_CustomerType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
CREATE TABLE [Product](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CompanyId] [int] NULL,
	[Code] [varchar](50) NULL,
	[Name] [varchar](250) NOT NULL,
	[Sequence] [int] NOT NULL,
	[IsActive] [int] NULL,
	[CreatedById] [int] NULL,
	[CreateDate] [datetime] NULL,
	[ModifiedById] [int] NULL,
	[ModifyDate] [datetime] NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [TermValidity](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CompanyId] [int] NULL,
	[Name] [varchar](250) NOT NULL,
	[Sequence] [int] NOT NULL,
	[IsActive] [int] NULL,
	[CreatedById] [int] NULL,
	[CreateDate] [datetime] NULL,
	[ModifiedById] [int] NULL,
	[ModifyDate] [datetime] NULL,
 CONSTRAINT [PK_TermValidity] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [ActivityLog](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CompanyId] [int] NULL,
	[Module] [varchar](100) NOT NULL,
	[Action] [varchar](100) NOT NULL,
	[Message] [varchar](250) NOT NULL,
	[IsActive] [int] NULL,
	[CreatedById] [int] NULL,
	[CreateDate] [datetime] NULL,
	[ModifiedById] [int] NULL,
	[ModifyDate] [datetime] NULL,
 CONSTRAINT [PK_ActivityLog] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
--========= Field Mapping ======
CREATE TABLE [FieldMapping](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CompanyId] [int] NULL,
	[Name] [varchar](100) NOT NULL,
	[IsVisible] [bit] NULL,
	[IsRequired] [bit] NULL,
	[Sequence] [int] NOT NULL,
	[IsActive] [int] NULL,
	[CreatedById] [int] NULL,
	[CreateDate] [datetime] NULL,
	[ModifiedById] [int] NULL,
	[ModifyDate] [datetime] NULL,
 CONSTRAINT [PK_FieldMapping] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
CREATE TABLE [InitiationDetail](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RefId] [varchar](50) NULL,
	[DepartmentId] [int] NOT NULL,
	[RoleTypeId] [int] NOT NULL,
	[CustomerName] [varchar](150) NOT NULL,
	[EntityTypeId] [int] NOT NULL,
	[OtherCustomerType] [varchar](100) NULL,
	[EntityId] [int] NOT NULL,
	[CustomerTypeId] [int] NOT NULL,
	[AgreementId] [int] NOT NULL,
	[AgreementOthers] [varchar](100) NULL,
	[PaymentTermId] [int] NOT NULL,
	[PaymentTermOthers] [varchar](100) NULL,
	[OfficeAddress] [varchar](100) NULL,
	[OfficeAddress1] [varchar](100) NULL,
	[OfficeCountryId] [int] NOT NULL,
	[OfficeOtherCountry] [varchar](100) NULL,
	[OfficeStateId] [int] NOT NULL,
	[OfficeOtherState] [varchar](100) NULL,
	[OfficeCityId] [int] NOT NULL,
	[OfficeOtherCity] [varchar](100) NULL,
	[OfficePinNo] [varchar](10) NULL,
	[DLNumber] [varchar](25) NULL,
	[IsOfficeDLAddressSame] [bit] NULL,
	[DLAddress] [varchar](100) NULL,
	[DLAddress1] [varchar](100) NULL,
	[DLCountryId] [int] NULL,
	[DLOtherCountry] [varchar](100) NULL,
	[DLStateId] [int] NULL,
	[DLOtherState] [varchar](100) NULL,
	[DLCityId] [int] NULL,
	[DLOtherCity] [varchar](100) NULL,
	[DLPinNo] [varchar](10) NULL,
	[PANNumber] [varchar](20) NULL,
	[GSTNumber] [varchar](20) NULL,
	[IsDLBillingAddressSame] [bit] NULL,
	[BillingAddress] [varchar](100) NULL,
	[BillingAddress1] [varchar](100) NULL,
	[BillingCountryId] [int] NULL,
	[BillingOtherCountry] [varchar](100) NULL,
	[BillingStateId] [int] NULL,
	[BillingOtherState] [varchar](100) NULL,
	[BillingCityId] [int] NULL,
	[BillingOtherCity] [varchar](100) NULL,
	[BillingPinNo] [varchar](10) NULL,
	[DeliveryAddress] [varchar](100) NULL,
	[DeliveryAddress1] [varchar](100) NULL,
	[DeliveryCountryId] [int] NULL,
	[DeliveryOtherCountry] [varchar](100) NULL,
	[DeliveryStateId] [int] NULL,
	[DeliveryOtherState] [varchar](100) NULL,
	[DeliveryCityId] [int] NULL,
	[DeliveryOtherCity] [varchar](100) NULL,
	[DeliveryPinNo] [varchar](10) NULL,
	[AuthorisedSignatoryName] [varchar](100) NULL,
	[ContactPersonName] [varchar](100) NULL,
	[ContactPersonMobile] [varchar](15) NULL,
	[ContactPersonEmail] [varchar](100) NULL,
	[PartyProfileSheet_FileName] [varchar](250) NOT NULL,
	[PartyProfileSheet_Extension] [varchar](10) NOT NULL,
	[DraftAgreement_FileName] [varchar](250) NOT NULL,
	[DraftAgreement_Extension] [varchar](10) NOT NULL,
	[Comment] [varchar](250) NULL,
	[Remarks] [varchar](250) NULL,
	[RequestStatusId] [int] NULL,
	[SubStatusId] [int] NULL,
	[SharedDocumentModeId] [int] NULL,
	[IsActive] [bit] NULL,
	[CreatedById] [int] NULL,
	[CreateDate] [datetime] NULL,
	[ModifiedById] [int] NULL,
	[ModifyDate] [datetime] NULL,
 CONSTRAINT [PK_InitiationDetail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


--========== InitiationDetail Log ==========
CREATE TABLE [InitiationDetail_Log](
	[Log_Id] [int] IDENTITY(1,1) NOT NULL,
	[Id] [int] NOT NULL,
	[RefId] [varchar](50) NULL,
	[RoleTypeId] [int] NOT NULL,
	[DepartmentId] [int] NOT NULL,
	[CustomerName] [varchar](150) NOT NULL,
	[EntityTypeId] [int] NOT NULL,
	[EntityId] [int] NOT NULL,
	[CustomerTypeId] [int] NOT NULL,
	[AgreementId] [int] NOT NULL,
	[AgreementOthers] [varchar](100) NULL,
	[PaymentTermId] [int] NOT NULL,
	[PaymentTermOthers] [varchar](100) NULL,
	[OfficeAddress] [varchar](100) NULL,
	[OfficeAddress1] [varchar](100) NULL,
	[OfficeCountryId] [int] NOT NULL,
	[OfficeOtherCountry] [varchar](100) NULL,
	[OfficeStateId] [int] NOT NULL,
	[OfficeOtherState] [varchar](100) NULL,
	[OfficeCityId] [int] NOT NULL,
	[OfficeOtherCity] [varchar](100) NULL,
	[DLNumber] [varchar](25) NULL,
	[IsOfficeDLAddressSame] [bit] NULL,
	[DLAddress] [varchar](100) NULL,
	[DLAddress1] [varchar](100) NULL,
	[DLCountryId] [int] NOT NULL,
	[DLOtherCountry] [varchar](100) NULL,
	[DLStateId] [int] NOT NULL,
	[DLOtherState] [varchar](100) NULL,
	[DLCityId] [int] NOT NULL,
	[DLOtherCity] [varchar](100) NULL,
	[PANNumber] [varchar](20) NULL,
	[GSTNumber] [varchar](20) NULL,
	[IsDLBillingAddressSame] [bit] NULL,
	[BillingAddress] [varchar](100) NULL,
	[BillingAddress1] [varchar](100) NULL,
	[BillingCountryId] [int] NOT NULL,
	[BillingOtherCountry] [varchar](100) NULL,
	[BillingStateId] [int] NOT NULL,
	[BillingOtherState] [varchar](100) NULL,
	[BillingCityId] [int] NOT NULL,
	[BillingOtherCity] [varchar](100) NULL,
	[AuthorisedSignatoryName] [varchar](100) NULL,
	[ContactPersonName] [varchar](100) NULL,
	[ContactPersonMobile] [varchar](15) NULL,
	[ContactPersonEmail] [varchar](100) NULL,
	[PartyProfileSheet_FileName] [varchar](250) NOT NULL,
	[PartyProfileSheet_Extension] [varchar](10) NOT NULL,
	[DraftAgreement_FileName] [varchar](250) NOT NULL,
	[DraftAgreement_Extension] [varchar](10) NOT NULL,
	[Comment] [varchar](250) NULL,
	[RequestStatusId] [int] NULL,
	[IsActive] [bit] NULL,
	[CreatedById] [int] NULL,
	[CreateDate] [datetime] NULL,
	[ModifiedById] [int] NULL,
	[ModifyDate] [datetime] NULL,
	[LogId] [int] NULL,
	[LogDate] [datetime] NULL,
	[Data] [varchar](50) NULL,
	[Action] [varchar](50) NULL,
 CONSTRAINT [PK_InitiationDetail_Log] PRIMARY KEY CLUSTERED 
(
	[Log_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [InitiationDetail_Log] ADD  CONSTRAINT [DF_InitiationDetail_Log_LogDate]  DEFAULT (getdate()) FOR [LogDate]
GO

--===============InitiationAttachment========
CREATE TABLE [InitiationAttachment](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[InitiationId] [varchar](50) NULL,
	[DocumentId] [int] NULL,
	[AttachmentName] [varchar](100) NOT NULL,
	[FileName] [varchar](250) NOT NULL,
	[Extension] [varchar](10) NOT NULL,
	[Sequence] [int] NOT NULL,
	[VersionNo] [varchar](10) NULL,
	[IsApproved] [bit] NULL,
	[IsActive] [bit] NULL,
	[CreatedById] [int] NULL,
	[CreateDate] [datetime] NULL,
	[ModifiedById] [int] NULL,
	[ModifyDate] [datetime] NULL,
 CONSTRAINT [PK_InitiationAttachment] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

--===============InitiationAttachment History========
CREATE TABLE [dbo].[InitiationAttachmentHistory](
	[Log_Id] [bigint] IDENTITY(1,1) NOT NULL,
	[InitiationId] [varchar](50) NULL,
	[DocumentId] [int] NULL,
	[AttachmentName] [varchar](100) NOT NULL,
	[FileName] [varchar](250) NOT NULL,
	[Extension] [varchar](10) NOT NULL,
	[Sequence] [int] NOT NULL,
	[VersionNo] [varchar](10) NULL,
	[IsApproved] [bit] NULL,
	[IsActive] [bit] NULL,
	[CreatedById] [int] NULL,
	[CreateDate] [datetime] NULL,
	[ModifiedById] [int] NULL,
	[ModifyDate] [datetime] NULL,
 CONSTRAINT [PK_InitiationAttachment_log] PRIMARY KEY CLUSTERED 
(
	[Log_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


CREATE TABLE [dbo].[InitiationRequestHistory](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[InitiationId] [varchar](50) NULL,
	[RequestStatusId] [int] NULL,
	[Comment] [varchar](250) NULL,
	[Remarks] [varchar](500) NULL,
	[CreatedById] [int] NULL,
	[CreateDate] [datetime] NULL,
 CONSTRAINT [PK_InitiationRequestHistory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [LegalHead](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[InitiationId] [varchar](50) NOT NULL,
	[TypeOfAgreementId] [int] NULL,
	[CommentAgreementType] [varchar](250) NULL,
	[SubCategoryAgreementId] [int] NULL,
	[CommentSubCategoryAgreement] [varchar](250) NULL,
	[AssignLegalUserId] [int] NULL,
	[CommentAssignLegalUser] [varchar](250) NULL,
	[IsActive] [bit] NULL,
	[CreatedById] [int] NULL,
	[CreateDate] [datetime] NULL,
	[ModifiedById] [int] NULL,
	[ModifyDate] [datetime] NULL,
 CONSTRAINT [PK_LegalHead] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
CREATE TABLE [LegalUser](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[InitiationId] [varchar](50) NOT NULL,
	[FileName] [varchar](250) NOT NULL,
	[Extension] [varchar](10) NOT NULL,
	[Comment] [varchar](250) NULL,
	[PendingDocIds] [varchar](50) NULL,
	[Remarks] [varchar](250) NULL,
	[IsActive] [bit] NULL,
	[CreatedById] [int] NULL,
	[CreateDate] [datetime] NULL,
	[ModifiedById] [int] NULL,
	[ModifyDate] [datetime] NULL,
 CONSTRAINT [PK_LegalUser] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
--============= Execution Details ===========
CREATE TABLE [ExecutionDetails](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[InitiationId] [varchar](50) NOT NULL,
	[AgreementDate] [datetime] NULL,
	[EffectiveDate] [datetime] NULL,
	[TermValidityId] [int] NULL,
	[TermValidityNo] [int] NULL,
	[RenewalDueDate] [datetime] NULL,
	[YearNotice] [int] NULL,
	[MonthNotice] [int] NULL,
	[DayNotice] [int] NULL,
	[CountryId] [int] NULL,
	[StateId] [int] NULL,
	[CityId] [int] NULL,
	[OtherExecutionState] [varchar](100) NULL,
	[OtherExecutionCity] [varchar](100) NULL,
	[CommercialApprovedBy] [varchar](250) NULL,
	[LegalApprovedBy] [varchar](250) NULL,
	[ProductIds] [varchar](100) NULL,
	[Comment] [varchar](250) NULL,
	[OtherRemarks] [varchar](250) NULL,
	[AttachmentFileName] [varchar](max) NULL,
	[AttachmentExtension] [varchar](10) NULL,
	[FinalAgreementFileName] [varchar](250) NULL,
	[FinalAgreementExtension] [varchar](10) NULL,
	[IsActive] [bit] NULL,
	[CreatedById] [int] NULL,
	[CreateDate] [datetime] NULL,
	[ModifiedById] [int] NULL,
	[ModifyDate] [datetime] NULL,
 CONSTRAINT [PK_ExecutionDetails] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

--============= UserHelpFileMapping ===========
CREATE TABLE [UserHelpFileMapping](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CompanyId] [int] NULL,
	[RoleTypeId] [int] NULL,
	[FileName] [varchar](250) NOT NULL,
	[FileExt] [varchar](10) NOT NULL,
	[Sequence] [int] NOT NULL,
	[IsActive] [int] NULL,
	[CreatedById] [int] NULL,
	[CreateDate] [datetime] NULL,
	[ModifiedById] [int] NULL,
	[ModifyDate] [datetime] NULL,
 CONSTRAINT [PK_UserHelpFileMapping] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
--============= ContractTemplate ===========
CREATE TABLE [dbo].[ContractTemplate](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CompanyId] [int] NULL,
	[FileName] [varchar](250) NULL,
	[FileExt] [varchar](10) NULL,
	[Sequence] [int] NULL,
	[IsActive] [int] NULL,
	[CreatedById] [int] NULL,
	[CreateDate] [datetime] NULL,
	[ModifiedById] [int] NULL,
	[ModifyDate] [datetime] NULL,
 CONSTRAINT [PK_ContractTemplate] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
--============= Repository Template ===========
CREATE TABLE [dbo].[RepositoryTemplate](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CompanyId] [int] NULL,
	[FileName] [varchar](250) NULL,
	[FileExt] [varchar](10) NULL,
	[Sequence] [int] NULL,
	[IsActive] [int] NULL,
	[CreatedById] [int] NULL,
	[CreateDate] [datetime] NULL,
	[ModifiedById] [int] NULL,
	[ModifyDate] [datetime] NULL,
 CONSTRAINT [PK_RepositoryTemplate] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

--============= EmailSetup  ===========
CREATE TABLE [dbo].[EmailSetup](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CompanyId] [int] NULL,
	[IPAddress] [varchar](25) NULL,
	[EmailId] [varchar](150) NULL,
	[EmailPWD] [varchar](50) NULL,
	[SMTPPort] [varchar](10) NULL,
	[IsActive] [bit] NULL,
	[CreatedById] [int] NULL,
	[CreateDate] [datetime] NULL,
	[ModifiedById] [int] NULL,
	[ModifyDate] [datetime] NULL,
 CONSTRAINT [PK_EmailSetup] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

--============= Email Template  ===========
CREATE TABLE [dbo].[EmailTemplate](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CompanyId] [int] NULL,
	[MailTypeId] [int] NULL,
	[Template] [nvarchar](max) NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedById] [int] NULL,
	[CreateDate] [datetime] NULL,
	[ModifiedById] [int] NULL,
	[ModifyDate] [datetime] NULL,
 CONSTRAINT [PK_EmailTemplate] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO