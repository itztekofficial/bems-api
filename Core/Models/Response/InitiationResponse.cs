using Domain;
using System.Collections.Generic;

namespace Core.Models.Response;

public class InitiationResponse
{
    public InitiationDetail InitiationDetail { get; set; }
    public IEnumerable<InitiationAttachment> InitiationAttachments { get; set; }
    public IEnumerable<InitiationLegalHead> InitiationLegalHeads { get; set; }
    public IEnumerable<InitiationLegalUser> InitiationLegalUsers { get; set; }
    public IEnumerable<InitiationPreExecution> InitiationPreExecutions { get; set; }
    public IEnumerable<InitiationExecution> InitiationExecutions { get; set; }
}