using GMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMS.Domain.Abstractions
{
    public interface IGatheringRepository
    {
        public void Add(Gathering gathering);
        Task<Gathering> GetByIdWithCreatorAsync(Guid gatheringId, CancellationToken cancellationToken);
    }
}
