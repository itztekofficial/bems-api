using Company.Repositories.Contracts;
using Core.AppConst;
using Core.Enums;
using Core.Models.Response;
using Dapper;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Net.Http.Headers;

namespace Company.Repositories
{
    /// <summary>
    /// InitiationDetailRepository
    /// </summary>
    public class InitiationDetailRepository : IInitiationDetailRepository
    {
        #region ===[ Private Members ]===================================
        private readonly SqlConnection _sqlConnection;
        private readonly IDbTransaction _dbTransaction;
        private readonly ILogger<InitiationDetailRepository> _logger;
        #endregion

        #region ===[ Constructor ]=======================================
        public InitiationDetailRepository(ILogger<InitiationDetailRepository> logger, SqlConnection sqlConnection, IDbTransaction dbTransaction)
        {
            _logger = logger;
            _dbTransaction = dbTransaction;
            _sqlConnection = sqlConnection;
            _logger.LogInformation("Initiation Detail Repository Initialized");
        }
        #endregion

        #region ===[ InitiationDetailRepository Methods ]==================================================

        public async Task<IEnumerable<InitiationDetail>> GetAllAsync()
        {
            var param = new DynamicParameters();
            param.Add("ActionType", "getAll");

            return await _sqlConnection.QueryAsync<InitiationDetail>(AppConst.usp_InitiationDetail, param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);
        }

        public async Task<InitiationResponse> GetByIdAsync(int id)
        {
            InitiationResponse initiationResponse = new();
            var param = new DynamicParameters();
            param.Add("ActionType", "getById");
            param.Add("Id", id);

            var cmd = new CommandDefinition(AppConst.usp_InitiationDetail, param, transaction: _dbTransaction, commandType: CommandType.StoredProcedure, flags: CommandFlags.NoCache);
            var res = await _sqlConnection.QueryMultipleAsync(cmd);

            initiationResponse.InitiationDetail = res.ReadFirstOrDefault<InitiationDetail>();
            initiationResponse.InitiationAttachments = res.Read<InitiationAttachment>();
            initiationResponse.InitiationLegalHeads = res.Read<InitiationLegalHead>();
            initiationResponse.InitiationLegalUsers = res.Read<InitiationLegalUser>();
            initiationResponse.InitiationPreExecutions = res.Read<InitiationPreExecution>();
            initiationResponse.InitiationExecutions = res.Read<InitiationExecution>();

            return initiationResponse;
        }

        public async Task<string> CreateAsync(InitiationDetail initiationDetail)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("ActionType", "insert");
                param.Add("Id", initiationDetail.Id);
                param.Add("DepartmentId", initiationDetail.DepartmentId);
                param.Add("RoleTypeId", initiationDetail.RoleTypeId);
                param.Add("RefId", initiationDetail.RefId);
                param.Add("CustomerName", !string.IsNullOrEmpty(initiationDetail.CustomerName) ? initiationDetail.CustomerName.ToUpper() : string.Empty);
                param.Add("EntityTypeId", initiationDetail.EntityTypeId);
                param.Add("OtherCustomerType", !string.IsNullOrEmpty(initiationDetail.OtherCustomerType) ? initiationDetail.OtherCustomerType : string.Empty);
                param.Add("EntityId", initiationDetail.EntityId);
                param.Add("CustomerTypeId", initiationDetail.CustomerTypeId);
                param.Add("AgreementId", initiationDetail.AgreementId);
                param.Add("AgreementOthers", !string.IsNullOrEmpty(initiationDetail.AgreementOthers) ? initiationDetail.AgreementOthers : string.Empty);
                param.Add("PaymentTermId", initiationDetail.PaymentTermId);
                param.Add("PaymentTermOthers", !string.IsNullOrEmpty(initiationDetail.PaymentTermOthers) ? initiationDetail.PaymentTermOthers : string.Empty);
                param.Add("OfficeAddress", !string.IsNullOrEmpty(initiationDetail.OfficeAddress) ? initiationDetail.OfficeAddress : string.Empty);
                param.Add("OfficeAddress1", !string.IsNullOrEmpty(initiationDetail.OfficeAddress1) ? initiationDetail.OfficeAddress1 : string.Empty);
                param.Add("OfficeCountryId", initiationDetail.OfficeCountryId);
                param.Add("OfficeOtherCountry", !string.IsNullOrEmpty(initiationDetail.OfficeOtherCountry) ? initiationDetail.OfficeOtherCountry : string.Empty);
                param.Add("OfficeStateId", initiationDetail.OfficeStateId);
                param.Add("OfficeOtherState", !string.IsNullOrEmpty(initiationDetail.OfficeOtherState) ? initiationDetail.OfficeOtherState : string.Empty);
                param.Add("OfficeCityId", initiationDetail.OfficeCityId);
                param.Add("OfficeOtherCity", !string.IsNullOrEmpty(initiationDetail.OfficeOtherCity) ? initiationDetail.OfficeOtherCity : string.Empty);
                param.Add("OfficePinNo", !string.IsNullOrEmpty(initiationDetail.OfficePinNo) ? initiationDetail.OfficePinNo : string.Empty);
                param.Add("DLNumber", !string.IsNullOrEmpty(initiationDetail.DLNumber) ? initiationDetail.DLNumber.ToUpper() : string.Empty);

                param.Add("IsOfficeDLAddressSame", initiationDetail.IsOfficeDLAddressSame);

                param.Add("DLAddress", !string.IsNullOrEmpty(initiationDetail.DLAddress) ? initiationDetail.DLAddress : string.Empty);
                param.Add("DLAddress1", !string.IsNullOrEmpty(initiationDetail.DLAddress1) ? initiationDetail.DLAddress1 : string.Empty);
                param.Add("DLCountryId", initiationDetail.DLCountryId);
                param.Add("DLOtherCountry", !string.IsNullOrEmpty(initiationDetail.DLOtherCountry) ? initiationDetail.DLOtherCountry : string.Empty);
                param.Add("DLStateId", initiationDetail.DLStateId);
                param.Add("DLOtherState", !string.IsNullOrEmpty(initiationDetail.DLOtherState) ? initiationDetail.DLOtherState : string.Empty);
                param.Add("DLCityId", initiationDetail.DLCityId);
                param.Add("DLOtherCity", !string.IsNullOrEmpty(initiationDetail.DLOtherCity) ? initiationDetail.DLOtherCity : string.Empty);
                param.Add("DLPinNo", !string.IsNullOrEmpty(initiationDetail.DLPinNo) ? initiationDetail.DLPinNo : string.Empty);
                param.Add("PANNumber", !string.IsNullOrEmpty(initiationDetail.PANNumber) ? initiationDetail.PANNumber.ToUpper() : string.Empty);
                param.Add("GSTNumber", !string.IsNullOrEmpty(initiationDetail.GSTNumber) ? initiationDetail.GSTNumber.ToUpper() : string.Empty);

                param.Add("IsDLBillingAddressSame", initiationDetail.IsDLBillingAddressSame);

                param.Add("BillingAddress", !string.IsNullOrEmpty(initiationDetail.BillingAddress) ? initiationDetail.BillingAddress : string.Empty);
                param.Add("BillingAddress1", !string.IsNullOrEmpty(initiationDetail.BillingAddress1) ? initiationDetail.BillingAddress1 : string.Empty);
                param.Add("BillingCountryId", initiationDetail.BillingCountryId);
                param.Add("BillingOtherCountry", !string.IsNullOrEmpty(initiationDetail.BillingOtherCountry) ? initiationDetail.BillingOtherCountry : string.Empty);
                param.Add("BillingStateId", initiationDetail.BillingStateId);
                param.Add("BillingOtherState", !string.IsNullOrEmpty(initiationDetail.BillingOtherState) ? initiationDetail.BillingOtherState : string.Empty);
                param.Add("BillingCityId", initiationDetail.BillingCityId);
                param.Add("BillingOtherCity", !string.IsNullOrEmpty(initiationDetail.BillingOtherCity) ? initiationDetail.BillingOtherCity : string.Empty);
                param.Add("BillingPinNo", !string.IsNullOrEmpty(initiationDetail.BillingPinNo) ? initiationDetail.BillingPinNo : string.Empty);

                param.Add("IsBillingDeliveryAddressSame", initiationDetail.IsBillingDeliveryAddressSame);

                param.Add("DeliveryAddress", !string.IsNullOrEmpty(initiationDetail.DeliveryAddress) ? initiationDetail.DeliveryAddress : string.Empty);
                param.Add("DeliveryAddress1", !string.IsNullOrEmpty(initiationDetail.DeliveryAddress1) ? initiationDetail.DeliveryAddress1 : string.Empty);
                param.Add("DeliveryCountryId", initiationDetail.DeliveryCountryId);
                param.Add("DeliveryOtherCountry", !string.IsNullOrEmpty(initiationDetail.DeliveryOtherCountry) ? initiationDetail.DeliveryOtherCountry : string.Empty);
                param.Add("DeliveryStateId", initiationDetail.DeliveryStateId);
                param.Add("DeliveryOtherState", !string.IsNullOrEmpty(initiationDetail.DeliveryOtherState) ? initiationDetail.DeliveryOtherState : string.Empty);
                param.Add("DeliveryCityId", initiationDetail.DeliveryCityId);
                param.Add("DeliveryOtherCity", !string.IsNullOrEmpty(initiationDetail.DeliveryOtherCity) ? initiationDetail.DeliveryOtherCity : string.Empty);
                param.Add("DeliveryPinNo", !string.IsNullOrEmpty(initiationDetail.BillingPinNo) ? initiationDetail.BillingPinNo : string.Empty);

                param.Add("MarketedById", initiationDetail.MarketedById);
                param.Add("MarketedAddress", !string.IsNullOrEmpty(initiationDetail.MarketedAddress) ? initiationDetail.MarketedAddress : string.Empty);

                param.Add("AuthorisedSignatoryName", !string.IsNullOrEmpty(initiationDetail.AuthorisedSignatoryName) ? initiationDetail.AuthorisedSignatoryName.ToUpper() : string.Empty);
                param.Add("ContactPersonName", !string.IsNullOrEmpty(initiationDetail.ContactPersonName) ? initiationDetail.ContactPersonName.ToUpper() : string.Empty);
                param.Add("ContactPersonMobile", !string.IsNullOrEmpty(initiationDetail.ContactPersonMobile) ? initiationDetail.ContactPersonMobile : string.Empty);
                param.Add("ContactPersonEmail", !string.IsNullOrEmpty(initiationDetail.ContactPersonEmail) ? initiationDetail.ContactPersonEmail : string.Empty);

                if (initiationDetail.PartyProfileSheet != null && initiationDetail.PartyProfileSheet.Length > 0)
                {
                    string trimFileName = ContentDispositionHeaderValue.Parse(initiationDetail.PartyProfileSheet.ContentDisposition).FileName.Trim('"');
                    param.Add("PartyProfileSheet_FileName", trimFileName);
                    param.Add("PartyProfileSheet_Extension", Path.GetExtension(trimFileName));
                }

                if (initiationDetail.DraftAgreement != null && initiationDetail.DraftAgreement.Length > 0)
                {
                    string trimDraftFileName = ContentDispositionHeaderValue.Parse(initiationDetail.DraftAgreement.ContentDisposition).FileName.Trim('"');
                    param.Add("DraftAgreement_FileName", trimDraftFileName);
                    param.Add("DraftAgreement_Extension", Path.GetExtension(trimDraftFileName));
                }

                param.Add("Comment", !string.IsNullOrEmpty(initiationDetail.Comment) ? initiationDetail.Comment : string.Empty);
                param.Add("RequestStatusId", initiationDetail.RequestStatusId == 0 ? (int)EnumRequestStatus.Requested : initiationDetail.RequestStatusId);
                param.Add("IsActive", initiationDetail.IsActive);
                param.Add("CreatedById", initiationDetail.CreatedById);
                param.Add("CreateDate", DateTime.UtcNow);
                param.Add("ModifiedById", initiationDetail.CreatedById);
                param.Add("ModifyDate", DateTime.UtcNow);

                string InitiationId = await _sqlConnection.QueryFirstOrDefaultAsync<string>(AppConst.usp_InitiationDetail, param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);
                _dbTransaction.Commit();

                if (!string.IsNullOrEmpty(InitiationId) && initiationDetail.InitiationAttachments != null && initiationDetail.InitiationAttachments.Count > 0)
                {
                    SaveInitiationAttachment(InitiationId, initiationDetail, false);                    
                }

                if (initiationDetail.RoleTypeId >= (int)EnumRoleType.LegalHOD)
                {
                    SaveLegalHODDetails(InitiationId, initiationDetail);
                }

                //try
                //{

                //    //initiationDetail.CreatedById
                //    MailSMS mail = new();
                //    EmailHtmlBodyData emaildata = new EmailHtmlBodyData();
                //    emaildata.Header = "Initiation Request Details";
                //    emaildata.WelcomeMessage = "Dear " + initiationDetail.CustomerName + " ,";
                //    emaildata.MailBodyMessage = "Initiation contract request has been successfully submitting";
                //    //emaildata.MailBodyMessage = "Successfully logged in";
                //    emaildata.IdMessage = "Your Initiation request contract id : ";
                //    emaildata.IdNumber = InitiationId;
                //    string htmldata = mail.CrateEmailHTMLTemplate(emaildata);
                //    mail.SendEMail("mkmanoo@gmail.com", "Initiation Request", htmldata.ToString(), "File name", null);
                //    // Email Testing Code END		
                //}
                //catch
                //{ }

                return InitiationId;
            }
            catch
            {
                _dbTransaction.Rollback();
                return string.Empty;
            }
        }

        private void SaveInitiationAttachment(string InitiationId, InitiationDetail initiationDetail, bool isEdit)
        {
            int index = 1;
            initiationDetail.InitiationAttachments.ForEach(inputFile =>
            {
                string trimFileName = ContentDispositionHeaderValue.Parse(inputFile.ContentDisposition).FileName.Trim('"');
                string inputFileName = Path.GetFileName(trimFileName);
                string inputFileExt = Path.GetExtension(inputFileName);
                string[] splitInputFileName = inputFileName.Split("@@@");

                using var transaction = _sqlConnection.BeginTransaction();
                try
                {
                    if (isEdit)
                    {
                        if (inputFile.Length > 0)
                        {
                            var param = new DynamicParameters();
                            param.Add("ActionType", "insert");
                            param.Add("Id", splitInputFileName[0]);
                            param.Add("DocumentId", splitInputFileName[1]);
                            param.Add("InitiationId", InitiationId);
                            param.Add("AttachmentName", splitInputFileName[2]);
                            param.Add("FileName", splitInputFileName[3]);
                            param.Add("Extension", inputFileExt);
                            param.Add("Sequence", index);
                            param.Add("IsApproved", 0);
                            param.Add("IsActive", initiationDetail.IsActive);
                            param.Add("CreatedById", initiationDetail.CreatedById);
                            param.Add("CreateDate", DateTime.UtcNow);
                            param.Add("ModifiedById", initiationDetail.UpdatedById);
                            param.Add("ModifyDate", DateTime.UtcNow);

                            var res = _sqlConnection.Execute(AppConst.usp_InitiationAttachment, param, transaction: transaction, null, commandType: CommandType.StoredProcedure);

                            transaction.Commit();
                        }
                    }
                    else
                    {
                        var param = new DynamicParameters();
                        param.Add("ActionType", "insert");
                        param.Add("Id", splitInputFileName[0]);
                        param.Add("DocumentId", splitInputFileName[1]);
                        param.Add("InitiationId", InitiationId);
                        param.Add("AttachmentName", splitInputFileName[2]);
                        param.Add("FileName", splitInputFileName[3]);
                        param.Add("Extension", inputFileExt);
                        param.Add("Sequence", index);
                        param.Add("IsApproved", 0);
                        param.Add("IsActive", initiationDetail.IsActive);
                        param.Add("CreatedById", initiationDetail.CreatedById);
                        param.Add("CreateDate", DateTime.UtcNow);
                        param.Add("ModifiedById", initiationDetail.UpdatedById);
                        param.Add("ModifyDate", DateTime.UtcNow);

                        var res = _sqlConnection.Execute(AppConst.usp_InitiationAttachment, param, transaction: transaction, null, commandType: CommandType.StoredProcedure);

                        transaction.Commit();
                    }
                }
                catch
                {
                    transaction.Rollback();
                }
                index++;
            });
        }

        public async Task<bool> UpdateAsync(InitiationDetail initiationDetail)
        {
            var param = new DynamicParameters();
            param.Add("ActionType", "update");
            param.Add("Id", initiationDetail.Id);
            param.Add("RoleTypeId", initiationDetail.RoleTypeId);
            param.Add("CustomerName", !string.IsNullOrEmpty(initiationDetail.CustomerName) ? initiationDetail.CustomerName.ToUpper() : string.Empty);
            param.Add("EntityTypeId", initiationDetail.EntityTypeId);
            param.Add("OtherCustomerType", !string.IsNullOrEmpty(initiationDetail.OtherCustomerType) ? initiationDetail.OtherCustomerType : string.Empty);
            param.Add("EntityId", initiationDetail.EntityId);
            param.Add("CustomerTypeId", initiationDetail.CustomerTypeId);
            param.Add("AgreementId", initiationDetail.TypeOfAgreementId > 0 ? initiationDetail.TypeOfAgreementId : initiationDetail.AgreementId);
            param.Add("AgreementOthers", !string.IsNullOrEmpty(initiationDetail.AgreementOthers) ? initiationDetail.AgreementOthers : string.Empty);
            param.Add("PaymentTermId", initiationDetail.PaymentTermId);
            param.Add("PaymentTermOthers", !string.IsNullOrEmpty(initiationDetail.PaymentTermOthers) ? initiationDetail.PaymentTermOthers : string.Empty);
            param.Add("OfficeAddress", !string.IsNullOrEmpty(initiationDetail.OfficeAddress) ? initiationDetail.OfficeAddress : string.Empty);
            param.Add("OfficeAddress1", !string.IsNullOrEmpty(initiationDetail.OfficeAddress1) ? initiationDetail.OfficeAddress1 : string.Empty);
            param.Add("OfficeCountryId", initiationDetail.OfficeCountryId);
            param.Add("OfficeOtherCountry", !string.IsNullOrEmpty(initiationDetail.OfficeOtherCountry) ? initiationDetail.OfficeOtherCountry : string.Empty);
            param.Add("OfficeStateId", initiationDetail.OfficeStateId);
            param.Add("OfficeOtherState", !string.IsNullOrEmpty(initiationDetail.OfficeOtherState) ? initiationDetail.OfficeOtherState : string.Empty);
            param.Add("OfficeCityId", initiationDetail.OfficeCityId);
            param.Add("OfficeOtherCity", !string.IsNullOrEmpty(initiationDetail.OfficeOtherCity) ? initiationDetail.OfficeOtherCity : string.Empty);
            param.Add("OfficePinNo", !string.IsNullOrEmpty(initiationDetail.OfficePinNo) ? initiationDetail.OfficePinNo : string.Empty);
            param.Add("DLNumber", !string.IsNullOrEmpty(initiationDetail.DLNumber) ? initiationDetail.DLNumber.ToUpper() : string.Empty);

            param.Add("IsOfficeDLAddressSame", initiationDetail.IsOfficeDLAddressSame);

            param.Add("DLAddress", !string.IsNullOrEmpty(initiationDetail.DLAddress) ? initiationDetail.DLAddress : string.Empty);
            param.Add("DLAddress1", !string.IsNullOrEmpty(initiationDetail.DLAddress1) ? initiationDetail.DLAddress1 : string.Empty);
            param.Add("DLCountryId", initiationDetail.DLCountryId);
            param.Add("DLOtherCountry", !string.IsNullOrEmpty(initiationDetail.DLOtherCountry) ? initiationDetail.DLOtherCountry : string.Empty);
            param.Add("DLStateId", initiationDetail.DLStateId);
            param.Add("DLOtherState", !string.IsNullOrEmpty(initiationDetail.DLOtherState) ? initiationDetail.DLOtherState : string.Empty);
            param.Add("DLCityId", initiationDetail.DLCityId);
            param.Add("DLOtherCity", !string.IsNullOrEmpty(initiationDetail.DLOtherCity) ? initiationDetail.DLOtherCity : string.Empty);
            param.Add("DLPinNo", !string.IsNullOrEmpty(initiationDetail.DLPinNo) ? initiationDetail.DLPinNo : string.Empty);
            param.Add("PANNumber", !string.IsNullOrEmpty(initiationDetail.PANNumber) ? initiationDetail.PANNumber.ToUpper() : string.Empty);
            param.Add("GSTNumber", !string.IsNullOrEmpty(initiationDetail.GSTNumber) ? initiationDetail.GSTNumber.ToUpper() : string.Empty);

            param.Add("IsDLBillingAddressSame", initiationDetail.IsDLBillingAddressSame);

            param.Add("BillingAddress", !string.IsNullOrEmpty(initiationDetail.BillingAddress) ? initiationDetail.BillingAddress : string.Empty);
            param.Add("BillingAddress1", !string.IsNullOrEmpty(initiationDetail.BillingAddress1) ? initiationDetail.BillingAddress1 : string.Empty);
            param.Add("BillingCountryId", initiationDetail.BillingCountryId);
            param.Add("BillingOtherCountry", !string.IsNullOrEmpty(initiationDetail.BillingOtherCountry) ? initiationDetail.BillingOtherCountry : string.Empty);
            param.Add("BillingStateId", initiationDetail.BillingStateId);
            param.Add("BillingOtherState", !string.IsNullOrEmpty(initiationDetail.BillingOtherState) ? initiationDetail.BillingOtherState : string.Empty);
            param.Add("BillingCityId", initiationDetail.BillingCityId);
            param.Add("BillingOtherCity", !string.IsNullOrEmpty(initiationDetail.BillingOtherCity) ? initiationDetail.BillingOtherCity : string.Empty);
            param.Add("BillingPinNo", !string.IsNullOrEmpty(initiationDetail.BillingPinNo) ? initiationDetail.BillingPinNo : string.Empty);

            param.Add("IsBillingDeliveryAddressSame", initiationDetail.IsBillingDeliveryAddressSame);

            param.Add("DeliveryAddress", !string.IsNullOrEmpty(initiationDetail.DeliveryAddress) ? initiationDetail.DeliveryAddress : string.Empty);
            param.Add("DeliveryAddress1", !string.IsNullOrEmpty(initiationDetail.DeliveryAddress1) ? initiationDetail.DeliveryAddress1 : string.Empty);
            param.Add("DeliveryCountryId", initiationDetail.DeliveryCountryId);
            param.Add("DeliveryOtherCountry", !string.IsNullOrEmpty(initiationDetail.DeliveryOtherCountry) ? initiationDetail.DeliveryOtherCountry : string.Empty);
            param.Add("DeliveryStateId", initiationDetail.DeliveryStateId);
            param.Add("DeliveryOtherState", !string.IsNullOrEmpty(initiationDetail.DeliveryOtherState) ? initiationDetail.DeliveryOtherState : string.Empty);
            param.Add("DeliveryCityId", initiationDetail.DeliveryCityId);
            param.Add("DeliveryOtherCity", !string.IsNullOrEmpty(initiationDetail.DeliveryOtherCity) ? initiationDetail.DeliveryOtherCity : string.Empty);
            param.Add("DeliveryPinNo", !string.IsNullOrEmpty(initiationDetail.DeliveryPinNo) ? initiationDetail.DeliveryPinNo : string.Empty);

            param.Add("MarketedById", initiationDetail.MarketedById);
            param.Add("MarketedAddress", !string.IsNullOrEmpty(initiationDetail.MarketedAddress) ? initiationDetail.MarketedAddress : string.Empty);

            param.Add("AuthorisedSignatoryName", !string.IsNullOrEmpty(initiationDetail.AuthorisedSignatoryName) ? initiationDetail.AuthorisedSignatoryName.ToUpper() : string.Empty);
            param.Add("ContactPersonName", !string.IsNullOrEmpty(initiationDetail.ContactPersonName) ? initiationDetail.ContactPersonName.ToUpper() : string.Empty);
            param.Add("ContactPersonMobile", !string.IsNullOrEmpty(initiationDetail.ContactPersonMobile) ? initiationDetail.ContactPersonMobile : string.Empty);
            param.Add("ContactPersonEmail", !string.IsNullOrEmpty(initiationDetail.ContactPersonEmail) ? initiationDetail.ContactPersonEmail : string.Empty);

            if (initiationDetail.PartyProfileSheet != null && initiationDetail.PartyProfileSheet.Length > 0)
            {
                string trimFileName = ContentDispositionHeaderValue.Parse(initiationDetail.PartyProfileSheet.ContentDisposition).FileName.Trim('"');
                param.Add("PartyProfileSheet_FileName", trimFileName);
                param.Add("PartyProfileSheet_Extension", Path.GetExtension(trimFileName));
            }

            if (initiationDetail.DraftAgreement != null && initiationDetail.DraftAgreement.Length > 0)
            {
                string trimDraftFileName = ContentDispositionHeaderValue.Parse(initiationDetail.DraftAgreement.ContentDisposition).FileName.Trim('"');
                param.Add("DraftAgreement_FileName", trimDraftFileName);
                param.Add("DraftAgreement_Extension", Path.GetExtension(trimDraftFileName));
            }

            param.Add("Comment", !string.IsNullOrEmpty(initiationDetail.Comment) ? initiationDetail.Comment : string.Empty);
            param.Add("RequestStatusId", initiationDetail.RequestStatusId);
            param.Add("ReplySendBackRemark", !string.IsNullOrEmpty(initiationDetail.ReplySendBackRemark) ? initiationDetail.ReplySendBackRemark : string.Empty);
            param.Add("IsActive", initiationDetail.IsActive);
            param.Add("CreatedById", initiationDetail.CreatedById);
            param.Add("CreateDate", DateTime.UtcNow);
            param.Add("ModifiedById", initiationDetail.CreatedById);
            param.Add("ModifyDate", DateTime.UtcNow);

            string InitiationId = await _sqlConnection.QueryFirstOrDefaultAsync<string>(AppConst.usp_InitiationDetail, param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);
            _dbTransaction.Commit();

            if (!string.IsNullOrEmpty(InitiationId) && initiationDetail.InitiationAttachments != null && initiationDetail.InitiationAttachments.Count > 0)
            {
                SaveInitiationAttachment(InitiationId, initiationDetail, true);               
            }

            if (initiationDetail.RoleTypeId == (int)EnumRoleType.DepartmentUser
                || initiationDetail.RoleTypeId == (int)EnumRoleType.DepartmentHOD
                || initiationDetail.RoleTypeId == (int)EnumRoleType.LegalHOD)
            {
                if (initiationDetail.RequestStatusId == (int)EnumRequestStatus.Negotiation)
                    SaveNegotiationReply(InitiationId, initiationDetail.ReplyAttachment, initiationDetail.ReplyRemarks, initiationDetail.CreatedById);

                if (initiationDetail.RequestStatusId == (int)EnumRequestStatus.Customer
                    || initiationDetail.RequestStatusId == (int)EnumRequestStatus.Legal
                    || initiationDetail.RequestStatusId == (int)EnumRequestStatus.SubmissionOfCustomerSignedCopy
                    || initiationDetail.RequestStatusId == (int)EnumRequestStatus.ConfirmationOfHardCopy)
                    UpdateBeforeExecutionSteps(initiationDetail);
            }

            if (initiationDetail.RoleTypeId >= (int)EnumRoleType.LegalHOD)
            {
                SaveLegalHODDetails(InitiationId, initiationDetail);
            }

            if (initiationDetail.RoleTypeId == (int)EnumRoleType.LegalUser)
            {
                SaveLegalUserDetails(InitiationId, initiationDetail);

                if ((initiationDetail.ReqRaisedRoleTypeId == (int)EnumRoleType.LegalUser
                    && initiationDetail.RoleTypeId == (int)EnumRoleType.LegalUser
                    && initiationDetail.RequestStatusId >= (int)EnumRequestStatus.Customer)
                    || initiationDetail.RequestStatusId == (int)EnumRequestStatus.ConfirmationOfSignedCopy
                    || initiationDetail.RequestStatusId == (int)EnumRequestStatus.SubmissionOfHardCopy)
                    UpdateBeforeExecutionSteps(initiationDetail);
            }

            if (initiationDetail.RoleTypeId == (int)EnumRoleType.LegalUser && initiationDetail.RequestStatusId == (int)EnumRequestStatus.Executed)
            {
                SaveExecutionDetails(InitiationId, initiationDetail);
            }

            return true;
        }

        private bool UpdateBeforeExecutionSteps(InitiationDetail initiationDetail)
        {
            int res = 0; string fileName = string.Empty;//, fileNameExt = string.Empty
            using var transaction = _sqlConnection.BeginTransaction();
            try
            {
                int Id = 0;
                if (initiationDetail.PreExecutionAttachments != null && initiationDetail.PreExecutionAttachments.Count > 0)
                {
                    string sFileName = ContentDispositionHeaderValue.Parse(initiationDetail.PreExecutionAttachments.FirstOrDefault().ContentDisposition).FileName.Trim('"');
                    if (!string.IsNullOrEmpty(sFileName))
                    {
                        string[] splitFileName = sFileName.Split("@@@");
                        Id = 0;
                    }

                    initiationDetail.PreExecutionAttachments.ForEach(inputFile =>
                    {
                        if (inputFile.Length > 0)
                        {
                            string trimFileName = ContentDispositionHeaderValue.Parse(inputFile.ContentDisposition).FileName.Trim('"');
                            fileName += trimFileName + "###";
                            //fileNameExt += Path.GetExtension(inputFile.FileName) + "###";
                        }
                    });
                }

                int tempSequence = (initiationDetail.RequestStatusId == (int)EnumRequestStatus.Customer
                    || initiationDetail.RequestStatusId == (int)EnumRequestStatus.Legal) ? 1 : 2;

                int tempStepId = (initiationDetail.RequestStatusId == (int)EnumRequestStatus.Customer
                    || initiationDetail.RequestStatusId == (int)EnumRequestStatus.Legal) ? initiationDetail.PreExecutionStepId : 0;

                DateTime tmpPreExecutionDate = new();
                if (!string.IsNullOrEmpty(initiationDetail.PreExecutionDate))
                {
                    DateTime date = DateTime.Now;
                    tmpPreExecutionDate = DateTime.ParseExact(initiationDetail.PreExecutionDate, "dd/MM/yyyy", CultureInfo.InvariantCulture).Add(date.TimeOfDay);
                }
                var param = new DynamicParameters();
                param.Add("ActionType", "insert");
                param.Add("Id", Id);
                param.Add("InitiationId", initiationDetail.Id);
                param.Add("PreExecutionStepId", tempStepId);
                param.Add("PreExecutionProcessName", initiationDetail.PreExecutionProcessName);
                param.Add("RequestStatusId", initiationDetail.RequestStatusId);
                param.Add("FileName", !string.IsNullOrEmpty(fileName) ? fileName[..fileName.LastIndexOf("###")] : "");
                param.Add("Remarks", initiationDetail.PreExecutionRemarks);
                param.Add("ActionDate", tmpPreExecutionDate);
                param.Add("Sequence", tempSequence);
                param.Add("IsActive", initiationDetail.IsActive);
                param.Add("CreatedById", initiationDetail.CreatedById);
                param.Add("CreateDate", DateTime.UtcNow);
                param.Add("ModifiedById", initiationDetail.UpdatedById);
                param.Add("ModifyDate", DateTime.UtcNow);

                res = _sqlConnection.Execute(AppConst.usp_PreExecutionSteps, param, transaction: transaction, null, commandType: CommandType.StoredProcedure);

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
            }
            return res > 0;
        }

        private bool SaveNegotiationReply(string InitiationId, List<IFormFile>? replyAttachments, string replyRemarks, int updatedById)
        {
            int res = 0; string fileName = string.Empty, fileNameExt = string.Empty;
            using var transaction = _sqlConnection.BeginTransaction();
            try
            {
                int legalId = 0;
                if (replyAttachments != null && replyAttachments.Count > 0)
                {
                    string sFileName = ContentDispositionHeaderValue.Parse(replyAttachments.FirstOrDefault().ContentDisposition).FileName.Trim('"');
                    if (!string.IsNullOrEmpty(sFileName))
                    {
                        string[] splitFileName = sFileName.Split("@@@");
                        legalId = int.Parse(splitFileName[0]);
                    }

                    replyAttachments.ForEach(inputFile =>
                    {
                        if (inputFile.Length > 0)
                        {
                            string trimFileName = ContentDispositionHeaderValue.Parse(inputFile.ContentDisposition).FileName.Trim('"');
                            fileName += trimFileName + "###";
                            fileNameExt += Path.GetExtension(inputFile.FileName) + "###";
                        }
                    });
                }

                var param = new DynamicParameters();
                param.Add("ActionType", "replyAttachments");
                param.Add("Id", legalId);
                param.Add("InitiationId", InitiationId);
                param.Add("FileName", !string.IsNullOrEmpty(fileName) ? fileName[..fileName.LastIndexOf("###")] : "");
                param.Add("Extension", !string.IsNullOrEmpty(fileNameExt) ? fileNameExt[..fileNameExt.LastIndexOf("###")] : "");
                param.Add("ReplyRemarks", replyRemarks);
                param.Add("ModifiedById", updatedById);
                param.Add("ModifyDate", DateTime.UtcNow);

                res = _sqlConnection.Execute(AppConst.usp_LegalUserApproval, param, transaction: transaction, null, commandType: CommandType.StoredProcedure);
                transaction.Commit();

            }
            catch
            {
                transaction.Rollback();
            }
            return res > 0;
        }

        private bool SaveLegalHODDetails(string InitiationId, InitiationDetail initiationDetail)
        {
            int res = 0;
            using var transaction = _sqlConnection.BeginTransaction();
            try
            {
                var param = new DynamicParameters();
                param.Add("ActionType", "insert");
                param.Add("Id", initiationDetail.Id);
                param.Add("InitiationId", InitiationId);
                param.Add("TypeOfAgreementId", initiationDetail.TypeOfAgreementId);
                param.Add("CommentAgreementType", initiationDetail.CommentAgreementType);
                param.Add("SubCategoryAgreementId", initiationDetail.SubCategoryAgreementId);
                param.Add("CommentSubCategoryAgreement", initiationDetail.CommentSubCategoryAgreement);
                param.Add("AssignLegalUserId", initiationDetail.AssignLegalUserId);
                param.Add("CommentAssignLegalUser", initiationDetail.CommentAssignLegalUser);
                param.Add("IsActive", initiationDetail.IsActive);
                param.Add("RoleTypeId", initiationDetail.RoleTypeId);
                param.Add("RequestStatusId", initiationDetail.RequestStatusId);
                param.Add("CreatedById", initiationDetail.CreatedById);
                param.Add("CreateDate", DateTime.UtcNow);
                param.Add("ModifiedById", initiationDetail.UpdatedById);
                param.Add("ModifyDate", DateTime.UtcNow);

                res = _sqlConnection.Execute(AppConst.usp_LegalHeadApproval, param, transaction: transaction, null, commandType: CommandType.StoredProcedure);
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
            }
            return res > 0;
        }

        private bool SaveLegalUserDetails(string InitiationId, InitiationDetail initiationDetail)
        {
            int res = 0; string legalFileName = string.Empty, legalFileNameExt = string.Empty;
            using var transaction = _sqlConnection.BeginTransaction();
            try
            {
                //if (initiationDetail.PendingDocIds != null)
                //{
                var param = new DynamicParameters();
                param.Add("ActionType", "insert");
                param.Add("Id", initiationDetail.Id);
                param.Add("InitiationId", InitiationId);
                param.Add("RoleTypeId", initiationDetail.RoleTypeId);
                param.Add("RequestStatusId", initiationDetail.RequestStatusId);

                //if (initiationDetail.AttachmentLegalUser != null && initiationDetail.AttachmentLegalUser.Length > 0)
                //{
                //    param.Add("FileName", initiationDetail.AttachmentLegalUser.FileName);
                //    param.Add("Extension", Path.GetExtension(initiationDetail.AttachmentLegalUser.FileName));
                //}

                if (initiationDetail.AttachmentLegalUser != null && initiationDetail.AttachmentLegalUser.Count > 0)
                {
                    initiationDetail.AttachmentLegalUser.ForEach(inputFile =>
                    {
                        if (inputFile.Length > 0)
                        {
                            string trimFileName = ContentDispositionHeaderValue.Parse(inputFile.ContentDisposition).FileName.Trim('"');
                            legalFileName += trimFileName + "###";
                            legalFileNameExt += Path.GetExtension(inputFile.FileName) + "###";
                        }
                    });

                    param.Add("FileName", legalFileName[..legalFileName.LastIndexOf("###")]);
                    param.Add("Extension", legalFileNameExt[..legalFileNameExt.LastIndexOf("###")]);
                }

                param.Add("Comment", initiationDetail.CommentLegalUser);
                param.Add("PendingDocIds", initiationDetail.PendingDocIds);
                param.Add("Remarks", initiationDetail.RemarksLegalUser);
                param.Add("IsActive", initiationDetail.IsActive);
                param.Add("CreatedById", initiationDetail.CreatedById);
                param.Add("CreateDate", DateTime.UtcNow);
                param.Add("ModifiedById", initiationDetail.UpdatedById);
                param.Add("ModifyDate", DateTime.UtcNow);

                res = _sqlConnection.Execute(AppConst.usp_LegalUserApproval, param, transaction: transaction, null, commandType: CommandType.StoredProcedure);
                transaction.Commit();
                //}
            }
            catch
            {
                transaction.Rollback();
            }
            return res > 0;
        }

        private bool SaveExecutionDetails(string InitiationId, InitiationDetail initiationDetail)
        {
            int res = 0; string executionFileName = string.Empty, executionFileNameExt = string.Empty;
            using var transaction = _sqlConnection.BeginTransaction();
            try
            {
                DateTime tmpAgreementDate = new(); DateTime tmpEffectiveDate = new(); DateTime tmpRenewalDueDate = new();
                if (!string.IsNullOrEmpty(initiationDetail.AgreementDate))
                    tmpAgreementDate = DateTime.ParseExact(initiationDetail.AgreementDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                if (!string.IsNullOrEmpty(initiationDetail.EffectiveDate))
                    tmpEffectiveDate = DateTime.ParseExact(initiationDetail.EffectiveDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                if (!string.IsNullOrEmpty(initiationDetail.RenewalDueDate))
                    tmpRenewalDueDate = DateTime.ParseExact(initiationDetail.RenewalDueDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                var param = new DynamicParameters();
                param.Add("ActionType", "insert");
                param.Add("Id", initiationDetail.Id);
                param.Add("RoleTypeId", initiationDetail.RoleTypeId);
                param.Add("RequestStatusId", initiationDetail.RequestStatusId);
                param.Add("InitiationId", InitiationId);
                param.Add("AgreementDate", tmpAgreementDate);
                param.Add("EffectiveDate", tmpEffectiveDate);
                param.Add("TermValidityId", initiationDetail.TermValidityId);
                param.Add("TermValidityNo", initiationDetail.TermValidityNo);
                param.Add("RenewalDueDate", tmpRenewalDueDate);
                param.Add("YearNotice", initiationDetail.YearNotice);
                param.Add("MonthNotice", initiationDetail.MonthNotice);
                param.Add("DayNotice", initiationDetail.DayNotice);
                param.Add("CountryId", initiationDetail.CountryId);
                param.Add("StateId", initiationDetail.StateId);
                param.Add("CityId", initiationDetail.CityId);
                param.Add("OtherExecutionCity", initiationDetail.OtherExecutionState);
                param.Add("OtherExecutionState", initiationDetail.OtherExecutionState);
                param.Add("CommercialApprovedBy", initiationDetail.CommercialApprovedBy);
                param.Add("LegalApprovedBy", initiationDetail.LegalApprovedBy);
                param.Add("ProductIds", initiationDetail.ProductIds);
                param.Add("Comment", initiationDetail.CommentExecution);
                param.Add("OtherRemarks", initiationDetail.OtherRemarks);

                //if (initiationDetail.AttachmentExecution != null && initiationDetail.AttachmentExecution.Length > 0)
                //{
                //    string trimFileName = ContentDispositionHeaderValue.Parse(initiationDetail.AttachmentExecution.ContentDisposition).FileName.Trim('"');
                //    param.Add("AttachmentFileName", trimFileName);
                //    param.Add("AttachmentExtension", Path.GetExtension(trimFileName));
                //}

                if (initiationDetail.AttachmentExecution != null && initiationDetail.AttachmentExecution.Count > 0)
                {
                    initiationDetail.AttachmentExecution.ForEach(inputFile =>
                    {
                        if (inputFile.Length > 0)
                        {
                            string trimFileName = ContentDispositionHeaderValue.Parse(inputFile.ContentDisposition).FileName.Trim('"');
                            executionFileName += trimFileName + "###";
                            executionFileNameExt += Path.GetExtension(inputFile.FileName) + "###";
                        }
                    });

                    param.Add("AttachmentFileName", executionFileName[..executionFileName.LastIndexOf("###")]);
                    param.Add("AttachmentExtension", executionFileNameExt[..executionFileNameExt.LastIndexOf("###")]);
                }

                if (initiationDetail.FinalAgreementFile != null && initiationDetail.FinalAgreementFile.Length > 0)
                {
                    string trimFinalFileName = ContentDispositionHeaderValue.Parse(initiationDetail.FinalAgreementFile.ContentDisposition).FileName.Trim('"');
                    param.Add("FinalAgreementFileName", trimFinalFileName);
                    param.Add("FinalAgreementExtension", Path.GetExtension(trimFinalFileName));
                }

                param.Add("IsActive", initiationDetail.IsActive);
                param.Add("CreatedById", initiationDetail.CreatedById);
                param.Add("CreateDate", DateTime.UtcNow);
                param.Add("ModifiedById", initiationDetail.UpdatedById);
                param.Add("ModifyDate", DateTime.UtcNow);

                res = _sqlConnection.Execute(AppConst.usp_ExecutionDetails, param, transaction: transaction, null, commandType: CommandType.StoredProcedure);
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
            }
            return res > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var param = new DynamicParameters();
            param.Add("ActionType", "delete");
            param.Add("Id", id);
            param.Add("IsActive", false);
            param.Add("ModifiedById", 1);
            param.Add("ModifyDate", DateTime.UtcNow);

            var res = await _sqlConnection.ExecuteAsync(AppConst.usp_InitiationDetail, param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);
            _dbTransaction.Commit();

            return res > 0;
        }

        public async Task<bool> UpdateStatusAsync(InitiationUpdateStatus updateStatus)
        {
            var param = new DynamicParameters();
            param.Add("ActionType", "updateStatus");
            param.Add("Id", updateStatus.Id);
            param.Add("RoleTypeId", updateStatus.RoleTypeId);
            param.Add("SubStatusId", updateStatus.RequestStatusId);
            param.Add("SendBackFrom", updateStatus.SendBackFrom);
            param.Add("SendBackTo", updateStatus.SendBackTo);
            param.Add("Remarks", updateStatus.Remarks);
            param.Add("ModifiedById", updateStatus.UpdatedById);
            param.Add("ModifyDate", DateTime.UtcNow);

            var res = await _sqlConnection.ExecuteAsync(AppConst.usp_InitiationDetail, param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);
            _dbTransaction.Commit();

            return res > 0;
        }

        public async Task<bool> UpdateBeforeExecutionStepsAsync(BeforeExecutionModel beforeExecutionModel)
        {
            var param = new DynamicParameters();
            param.Add("ActionType", "updateBeforeExecutionSteps");
            param.Add("Id", beforeExecutionModel.Id);
            param.Add("RequestStatusId", beforeExecutionModel.RequestStatusId);
            param.Add("ModifiedById", beforeExecutionModel.UpdatedById);
            param.Add("ModifyDate", DateTime.UtcNow);

            var res = await _sqlConnection.ExecuteAsync(AppConst.usp_InitiationDetail, param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);
            _dbTransaction.Commit();

            return res > 0;
        }

        public async Task<bool> SharedWithCustomerAsync(SharedWithCustomerModel sharedWithCustomer)
        {
            var param = new DynamicParameters();
            param.Add("ActionType", "sharedDocument");
            param.Add("Id", sharedWithCustomer.Id);
            param.Add("RequestStatusId", sharedWithCustomer.RequestStatusId);
            param.Add("SharedDocumentModeId", sharedWithCustomer.SharedDocumentModeId);
            param.Add("ModifiedById", sharedWithCustomer.UpdatedById);
            param.Add("ModifyDate", DateTime.UtcNow);

            var res = await _sqlConnection.ExecuteAsync(AppConst.usp_InitiationDetail, param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);
            _dbTransaction.Commit();

            return res > 0;
        }

        public async Task<bool> SharedWithLegalAcknowlegeAsync(SharedWithLegalAcknowlegeModel sharedWithLegal)
        {
            var param = new DynamicParameters();
            param.Add("ActionType", "sharedWithLegalAcknowlege");
            param.Add("Id", sharedWithLegal.Id);
            param.Add("RequestStatusId", sharedWithLegal.RequestStatusId);
            //param.Add("SubStatusId", sharedWithLegal.RequestStatusId);
            param.Add("ModifiedById", sharedWithLegal.UpdatedById);
            param.Add("ModifyDate", DateTime.UtcNow);

            var res = await _sqlConnection.ExecuteAsync(AppConst.usp_InitiationDetail, param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);
            _dbTransaction.Commit();

            return res > 0;
        }

        public async Task<bool> UpdateLegalUserAsync(UpdateLegaluserModel legaluserModel)
        {
            var param = new DynamicParameters();
            param.Add("ActionType", "updateLegalUser");
            param.Add("Id", legaluserModel.Id);
            param.Add("LegalUserId", legaluserModel.LegalUserId);
            param.Add("Remarks", legaluserModel.Remarks);
            param.Add("ModifiedById", legaluserModel.UpdatedById);
            param.Add("ModifyDate", DateTime.UtcNow);

            var res = await _sqlConnection.ExecuteAsync(AppConst.usp_InitiationDetail, param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);
            _dbTransaction.Commit();

            return res > 0;
        }

        Task<bool> IRepository<InitiationDetail>.CreateAsync(InitiationDetail entity)
        {
            throw new NotImplementedException();
        }

        Task<InitiationDetail> IRepository<InitiationDetail>.GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
        #endregion

        //#region "Const Veriables"
        //public const string usp_InitiationDetail = "usp_InitiationDetail";
        //#endregion
    }
}