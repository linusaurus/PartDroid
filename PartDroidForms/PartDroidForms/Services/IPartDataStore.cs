using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PartDroidForms.Model;

namespace PartDroidForms.Services
{
    public interface IPartDataService
    {
        Task<Part> GetPartAsync(int id);
        Task<IList<Part>> GetPartsAsync(string term);
    }
}
