using Khadamat_FileService.Domain.Common.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_FileService.Domain.FileAggregate.Events
{
    public class ProfileImageFileSavedDomainEvent : IDomainEvent
    {
        private readonly Func<int> _fileIdProvider;
        public string FullPath { get; }

        public ProfileImageFileSavedDomainEvent(string fullPath, Func<int> fileIdProvider)
        {
            _fileIdProvider = fileIdProvider;
            FullPath = fullPath;
        }
        public int FileId => _fileIdProvider();
    }
}
