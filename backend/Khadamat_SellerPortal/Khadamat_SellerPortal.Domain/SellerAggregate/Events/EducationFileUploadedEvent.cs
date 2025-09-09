using Khadamat_SellerPortal.Domain.Common.Events;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_SellerPortal.Domain.SellerAggregate.Events
{
    public record EducationFileUploadedEvent : IDomainEvent
    {
        private readonly Func<int> _educationIdProvider;
        public int EducationId => _educationIdProvider();
        public int SellerId { get; }
        public string Institution { get; }
        public string FieldOfStudy { get; }
        public string TempPath { get; }
        public EducationFileUploadedEvent(Func<int> EducationIdProvider, int sellerId, string institution, string fieldOfStudy, string tempPath)
        {
            _educationIdProvider = EducationIdProvider;
            SellerId = sellerId;
            Institution = institution;
            FieldOfStudy = fieldOfStudy;
            TempPath = tempPath;
        }
    }
}
