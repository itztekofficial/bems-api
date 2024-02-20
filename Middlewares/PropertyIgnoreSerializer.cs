using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Linq;
using System.Reflection;

namespace Middlewares
{
    /// <summary>
    /// The serialize action.
    /// </summary>
    public enum SerializeAction
    {
        All = 0,
        OnlyThese = 1,
        NotThese = 2
    }

    /// <summary>
    /// The custom json serializer.
    /// </summary>
    public class CustomJsonSerializer : JsonSerializerSettings
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomJsonSerializer"/> class.
        /// </summary>
        public CustomJsonSerializer()
        {
            ContractResolver = new CustomContractResolver();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomJsonSerializer"/> class.
        /// </summary>
        /// <param name="_serializeAction">The _serialize action.</param>
        /// <param name="_Propertyes">The _ propertyes.</param>
        public CustomJsonSerializer(SerializeAction _serializeAction, params string[] _Propertyes)
        {
            ContractResolver = new CustomContractResolver(_serializeAction, _Propertyes);
        }

        /// <summary>
        /// The custom contract resolver.
        /// </summary>
        public class CustomContractResolver : DefaultContractResolver
        {
            /// <summary>
            /// Gets or sets the propertyes.
            /// </summary>
            public string[] Propertyes { get; set; }

            /// <summary>
            /// Gets or sets the serialize action.
            /// </summary>
            public SerializeAction serializeAction { get; set; }

            /// <summary>
            /// Initializes a new instance of the <see cref="CustomContractResolver"/> class.
            /// </summary>
            /// <param name="_serializeAction">The _serialize action.</param>
            /// <param name="_Propertyes">The _ propertyes.</param>
            public CustomContractResolver(SerializeAction _serializeAction = SerializeAction.All, params string[] _Propertyes)
            {
                Propertyes = _Propertyes;
                serializeAction = _serializeAction;
            }

            /// <summary>
            /// Creates the property.
            /// </summary>
            /// <param name="member">The member.</param>
            /// <param name="memberSerialization">The member serialization.</param>
            /// <returns>A JsonProperty.</returns>
            protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
            {
                JsonProperty property = base.CreateProperty(member, memberSerialization);
                try
                {
                    switch (member.Name)
                    {
                        case "Data":
                        case "status":
                        case "errorLavel":
                        case "LinkID":
                        case "httpstatuscode":
                        case "recguid":
                        case "errcode":
                        case "error":
                        case "usermasterlinkid":
                        case "Accountgroupcode":
                        case "StackTrace":
                        case "message":
                        case "Exception":
                        case "InnerException":
                        case "compData":
                        case "compLogoData":
                        case "objCompanyList":
                        case "objCompanyStaticData":
                        case "objItemList":
                        case "objUserActions":
                        case "objHsnItemList":
                        case "objLedgerList":
                        case "voucher":
                        case "accountingDetails":
                        case "PaymentDetailsList":
                        case "Items":
                        case "batchStockOpeningBalance":
                        case "UnitBatches":
                        case "MultiUnits":
                        case "expenses":
                        case "multiUnitListForClient":
                        case "loginDetailData":
                        case "gstReturns":
                            property.ShouldSerialize = instance => true;
                            return property;

                        default:
                            break;
                    }

                    if (Propertyes?.Length > 0)
                        switch (serializeAction)
                        {
                            case SerializeAction.NotThese:
                                property.ShouldSerialize = instance => { return !Propertyes.Contains(member.Name); };
                                break;

                            case SerializeAction.OnlyThese:
                                property.ShouldSerialize = instance => { return Propertyes.Contains(member.Name); };
                                break;

                            default:
                                property.ShouldSerialize = instance => true;
                                break;
                        }
                    else
                        property.ShouldSerialize = instance => true;
                }
                catch
                {
                    property.ShouldSerialize = instance => true;
                }

                return property;
            }
        }
    }
}