using Azure.Storage.Blobs;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using TheCuriousReaders.Services.Interfaces;
using TheCuriousReaders.Services.Services;

namespace TheCuriousReaders.ServiceLayer.Tests.Fixtures
{
    public class BlobServiceFixture
    {
        public Mock<BlobServiceClient> BlobClient { get; private set; }
        public IBlobService BlobService { get; private set; }

        public BlobServiceFixture()
        {
            this.BlobClient = new Mock<BlobServiceClient>();

            this.BlobService = new BlobService(BlobClient.Object);
        }
    }
}
