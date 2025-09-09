using Khadamat_SellerPortal.Application.Common.Interfcaes;
using Khadamat_SellerPortal.Domain.SellerAggregate;
using Khadamat_SellerPortal.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_SellerPortal.Infrastructure.Sellers.Persistence
{
    public class SellerRepository : ISellerRepository
    {
        private readonly Khadamat_SellerPortalDbContext _context;
        public SellerRepository(Khadamat_SellerPortalDbContext context)
        {
            _context = context;
        }
        public async Task AddSeller(Seller seller)
        {
            await _context.Sellers.AddAsync(seller);
        }

        public async Task<Seller?> GetSellerById(int id)
        {
            return await _context.Sellers.Where(o => o.Id == id)
                .Include(o => o.WorkExperiences)
                    .ThenInclude(w => w.Certificates)
                .Include(o => o.Educations)
                    .ThenInclude(e => e.EducationCertificate)
                .Include(o => o.PortfolioUrls)
                .Include(o => o.SocialMediaLinks)
                .FirstOrDefaultAsync();
        }

        public async Task<Seller?> GetSellerByNationalNo(string nationalNo)
        {
            return await _context.Sellers.Where(o => o.PersonalDetails.NationalNo == nationalNo)
                .Include(o => o.WorkExperiences)
                    .ThenInclude(w => w.Certificates)
                .Include(o => o.Educations)
                    .ThenInclude(e => e.EducationCertificate)
                .Include(o => o.PortfolioUrls)
                .Include(o => o.SocialMediaLinks)
                .FirstOrDefaultAsync();
        }

        public async Task UpdateSeller(Seller seller)
        {
            await Task.CompletedTask;
            _context.Sellers.Update(seller);
        }
    }
}
