﻿using Khadamat_UserManagement.Domain.Common.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_UserManagement.Domain.Common
{
    public abstract class Entity
    {
        protected readonly List<IDomainEvent> _domainEvents = [];
        public int Id { get; }
        protected Entity(int id = 0)
        {
            Id = id;
        }

        public override bool Equals(object? obj)
        {
            if (obj is null || obj.GetType() != GetType())
            {
                return false;
            }

            return ((Entity)obj).Id == Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public List<IDomainEvent> PopDomainEvents()
        {
            var copy = _domainEvents.ToList();
            _domainEvents.Clear();
            return copy;
        }
    }
}
