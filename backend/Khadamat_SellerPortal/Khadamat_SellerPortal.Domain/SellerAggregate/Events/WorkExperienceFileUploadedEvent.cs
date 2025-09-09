using Khadamat_SellerPortal.Domain.Common.Events;

namespace Khadamat_SellerPortal.Domain.SellerAggregate.Events
{
    public record WorkExperienceFileUploadedEvent : IDomainEvent
    {
        private readonly Func<int> _workExperienceIdProvider;
        private readonly Func<int> _workExperienceCertificateIdProvider;
        public int SellerId { get; }
        public string CompanyName { get; }
        public string Position { get; }
        public string TempPath { get;  }
        public int CertificateId => _workExperienceCertificateIdProvider();
        public int WorkExperienceId => _workExperienceIdProvider();
        public WorkExperienceFileUploadedEvent(
            Func<int> workExperienceIdProvider,
            Func<int> workExperienceCertificateIdProvider,
            int sellerId,
            string companyName,
            string position,
            string tempPath)
        {
            _workExperienceCertificateIdProvider = workExperienceCertificateIdProvider;
            _workExperienceIdProvider = workExperienceIdProvider;
            SellerId = sellerId;
            CompanyName = companyName;
            Position = position;
            TempPath = tempPath;
        }
    }
}
