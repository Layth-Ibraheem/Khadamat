using Khadamat_SharedKernal.Khadamat_FilesService;
using Khadamat_SharedKernal.Khadamat_SellerPortal;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Khadamat_SharedKernal.Common
{
    [JsonDerivedType(typeof(SellerCreatedIntegrationEvent), typeDiscriminator: nameof(SellerCreatedIntegrationEvent))]
    [JsonDerivedType(typeof(EducationFileSavedIntegrationEvent), typeDiscriminator: nameof(EducationFileSavedIntegrationEvent))]
    [JsonDerivedType(typeof(EducationFileUploadedIntegrationEvent), typeDiscriminator: nameof(EducationFileUploadedIntegrationEvent))]
    [JsonDerivedType(typeof(WorkExperienceFileSavedIntegrationEvent), typeDiscriminator: nameof(WorkExperienceFileSavedIntegrationEvent))]
    [JsonDerivedType(typeof(WorkExperienceFileUploadedIntegrationEvent), typeDiscriminator: nameof(WorkExperienceFileUploadedIntegrationEvent))]
    [JsonDerivedType(typeof(ProfileImageCreatedIntegrationEvent), typeDiscriminator: nameof(ProfileImageCreatedIntegrationEvent))]
    [JsonDerivedType(typeof(ProfileImageFileSavedIntegrationEvent), typeDiscriminator: nameof(ProfileImageFileSavedIntegrationEvent))]
    public interface IIntegrationEvent : INotification { }
}
