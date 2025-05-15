namespace SecclShared.Models
{
    public class Client
    {
        public string Id { get; set; }
        public string FirmId { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Language { get; set; }
        public string Currency { get; set; }
        public List<string> NodeId { get; set; }
        public string Status { get; set; }
        public string ClientType { get; set; }
        public List<string> ParentId { get; set; }
        public AddressDetail AddressDetail { get; set; }
        public AuditDetails AuditDetails { get; set; }
        public List<Nationality> Nationalities { get; set; }
        public List<UpdateHistory> UpdateHistory { get; set; }
        public string Email { get; set; }
        public string DateOfBirth { get; set; }
        public string Title { get; set; }
        public string Gender { get; set; }
        public BankDetails BankDetails { get; set; }
        public Mobile Mobile { get; set; }
        public AmlData AmlData { get; set; }
        public CommsStatus CommsStatus { get; set; }
        public bool? TermsAccepted { get; set; }
        public bool? MarketingConsent { get; set; }
        public bool? RequiresDebitMandate { get; set; }
        public bool? ClientSeenFaceToFace { get; set; }
        public string MaritalStatus { get; set; }
        public string EmploymentStatus { get; set; }
        public int? RetirementAge { get; set; }
        public Vulnerability Vulnerability { get; set; }
    }

    public class AddressDetail
    {
        public string BuildingNumber { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string Address4 { get; set; }
        public string Country { get; set; }
        public string PostCode { get; set; }
    }

    public class AuditDetails
    {
        public string UpdateDate { get; set; }
        public string UserFirmId { get; set; }
        public string UserId { get; set; }
        public int Version { get; set; }
        public string Application { get; set; }
    }

    public class Nationality
    {
        public string Code { get; set; }
        public List<Identifier> Identifiers { get; set; }
    }

    public class Identifier
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public bool Available { get; set; }
    }

    public class UpdateHistory
    {
        public int UpdateId { get; set; }
        public string UpdateDate { get; set; }
        public UpdateData UpdateData { get; set; }
        public List<string> UpdatedFields { get; set; }
    }

    public class UpdateData
    {
        public object FromData { get; set; }
        public object ToData { get; set; }
    }

    public class BankDetails
    {
        public string SortCode { get; set; }
        public string AccountNumber { get; set; }
        public bool? DebitPaymentMandate { get; set; }
        public string DebitPaymentMandateAgreementDate { get; set; }
        public string PaymentMethod { get; set; }
    }

    public class Mobile
    {
        public string Number { get; set; }
        public string Locale { get; set; }
        public bool IsMobile { get; set; }
    }

    public class AmlData
    {
        public string CheckDate { get; set; }
        public List<string> CheckNode { get; set; }
        public string CheckResult { get; set; }
        public bool? AmlEvidenceSeen { get; set; }
        public List<object> Checks { get; set; }
    }

    public class CommsStatus
    {
        public bool? WelcomeSent { get; set; }
    }

    public class Vulnerability
    {
        public bool? IsVulnerable { get; set; }
        public List<string> Reason { get; set; }
        public string IsVulnerableSetDate { get; set; }
    }
}
