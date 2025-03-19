using CBRE.FacilityManagement.Audit.Application.Contracts.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBRE.FacilityManagement.Audit.Infrastructure
{
    public class BlobService : IBlobService
    {
        public Task<Stream> DownloadBlobAsync(string containerName, string blobName)
        {
            throw new NotImplementedException();
        }
    }
}
