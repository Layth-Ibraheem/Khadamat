using Khadamat_FileService.Domain.Common.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_FileService.Domain.FileAggregate.Events
{
    public record EducationFileSavedDomainEvent : IDomainEvent
    {
        private readonly Func<int> _idProvider;
        public string FullPath { get; }

        public EducationFileSavedDomainEvent(string fullPath, Func<int> idProvider)
        {
            _idProvider = idProvider;
            FullPath = fullPath;
        }
        public int FileId => _idProvider();
    }
}
