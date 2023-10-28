using Azure.Storage.Blobs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace CBITechMeetUpCloudSite.Controllers
{
    public class AzureStorage
    {
        public Stream AttachmentStream { get; private set; }
        BlobContainerClient _blobClient;

        public void Main()
        {
            string _blobConnectionString = "";
            string _blobContainerName = "";
            string CloudPath = "";
            var _blobClient = new BlobContainerClient(_blobConnectionString, _blobContainerName);
            var blob = _blobClient.GetBlobClient(CloudPath);
            blob.Upload(AttachmentStream);
        }
        public void download()
        {
            var getBlob = _blobClient.GetBlobClient("itemname");
            MemoryStream ms = new MemoryStream();
            getBlob.DownloadTo(ms);
        }

    }
}