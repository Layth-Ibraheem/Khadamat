using Khadamat_SellerPortal.Application.Common.Interfcaes;
using Khadamat_SellerPortal.Domain.OnlineSellerAggregate;
using Khadamat_SellerPortal.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_SellerPortal.Infrastructure.OnlineSellers.Persistence
{
    public class OnlineSellerRepository : IOnlineSellerRepository
    {
        private readonly Khadamat_SellerPortalDbContext _context;
        public OnlineSellerRepository(Khadamat_SellerPortalDbContext context)
        {
            _context = context;
        }
        public async Task AddOnlineSeller(OnlineSeller onlineSeller)
        {
            await _context.OnlineSellers.AddAsync(onlineSeller);
            await _context.SaveChangesAsync();
        }

        public async Task<OnlineSeller?> GetOnlineSellerById(int id)
        {
            return await _context.OnlineSellers.Where(o => o.Id == id)
                .Include(o => o.WorkExperiences)
                    .ThenInclude(w => w.Certificates)
                .Include(o => o.Educations)
                    .ThenInclude(e => e.EducationCertificate)
                .Include(o => o.PortfolioUrls)
                .Include(o => o.SocialMediaLinks)
                .FirstOrDefaultAsync();
        }

        public async Task UpdateOnlineSellerProfile(OnlineSeller onlineSeller)
        {
            await Task.Run(() =>
            {
                _context.OnlineSellers.Update(onlineSeller);
            });
        }
    }
}
