using Rebus.Auditing.Sagas;
using Rebus.AzureBlobs.Sagas;
using Rebus.Logging;
using System;
using Azure.Storage.Blobs;

// ReSharper disable UnusedMember.Global

namespace Rebus.Config;

/// <summary>
/// Configuration extensions for Azure storage
/// </summary>
public static class AzureStorageSagaConfigurationExtensions
{
    /// <summary>
    /// Configures Rebus to store saga data snapshots in blob storage
    /// </summary>
    public static void StoreInBlobStorage(this StandardConfigurer<ISagaSnapshotStorage> configurer, string storageAccountConnectionString, string containerName = "RebusSagaStorage")
    {
        if (configurer == null) throw new ArgumentNullException(nameof(configurer));
        if (storageAccountConnectionString == null) throw new ArgumentNullException(nameof(storageAccountConnectionString));
        if (containerName == null) throw new ArgumentNullException(nameof(containerName));

        var blobContainerClient = new BlobContainerClient(storageAccountConnectionString, containerName);

        configurer.Register(c => new AzureStorageSagaSnapshotStorage(blobContainerClient, c.Get<IRebusLoggerFactory>()));
    }
}