//using Moq;
//using System.IO.Compression;
//using TradingApp.StooqProvider.Abstraction;
//using TradingApp.StooqProvider.Services;
//using TradingApp.Ports.Enums;
//using TradingApp.Modules.Application.Models;

//namespace TradingApp.StooqProvider.Tests.Services
//{
//    public class FileServiceTests
//    {
//        [Fact]
//        public async Task ReadHistoryQuotaFile_ShouldReturnQuotes_WhenFileExists()
//        {
//            // Arrange
//            var timeFrame = new TimeFrame(Granularity.Daily, null, null);
//            var asset = new Asset(AssetName.USDPLN, AssetType.Currencies);
//            var fileContent = "20220101,000000,100.00,110.00,90.00,105.00,1000\n" +
//                              "20220102,000000,105.00,115.00,95.00,110.00,2000";

//            var memoryStream = new MemoryStream();
//            var writer = new StreamWriter(memoryStream);
//            await writer.WriteAsync(fileContent);
//            writer.Flush();
//            memoryStream.Position = 0;

//            var mockZipArchiveProvider = new Mock<IZipArchiveProvider>();
//            mockZipArchiveProvider.Setup(z => z.OpenRead(It.IsAny<Granularity>())).Returns(CreateMockZipArchive(memoryStream));

//            var fileService = new FileService(mockZipArchiveProvider.Object);

//            // Act
//            var result = await fileService.ReadHistoryQuotaFile(timeFrame, asset);

//            // Assert
//            Assert.True(result.IsSuccess);
//            Assert.Equal(2, result.Value.Count);
//            Assert.Equal(100.00m, result.Value.First().Open);
//            Assert.Equal(2000m, result.Value.Last().Volume);
//        }

//        [Fact]
//        public async Task ReadHistoryQuotaFile_ShouldReturnFailure_WhenFileDoesNotExist()
//        {
//            // Arrange
//            var timeFrame = new TimeFrame(Granularity.Daily, null, null);
//            var asset = new Asset(AssetName.USDPLN, AssetType.Currencies);
//            var mockZipArchiveProvider = new Mock<IZipArchiveProvider>();
//            var mockZipArchive = new Mock<ZipArchive>();

//            mockZipArchiveProvider.Setup(z => z.OpenRead(It.IsAny<Granularity>())).Returns(mockZipArchive.Object);
//            mockZipArchiveProvider.Setup(z => z.GetEntry(It.IsAny<ZipArchive>(), It.IsAny<Granularity>(), It.IsAny<AssetType>(), It.IsAny<AssetName>())).Returns<ZipArchiveEntry>(null);

//            var fileService = new FileService(mockZipArchiveProvider.Object);

//            // Act
//            var result = await fileService.ReadHistoryQuotaFile(timeFrame, asset);

//            // Assert
//            Assert.True(result.IsFailed);
//            Assert.Contains("Can not found file.", result.Errors.First().Message);
//            mockZipArchiveProvider.Verify(z => z.OpenRead(It.IsAny<Granularity>()), Times.Once);
//            mockZipArchiveProvider.Verify(z => z.GetEntry(It.IsAny<ZipArchive>(), It.IsAny<Granularity>(), It.IsAny<AssetType>(), It.IsAny<AssetName>()), Times.Once);
//        }

//        private ZipArchive CreateMockZipArchive(Stream stream)
//        {
//            var memoryStream = new MemoryStream();
//            stream.CopyTo(memoryStream);
//            memoryStream.Position = 0;
//            var tempFileName = "mock.zip";
//            var tempPath = Path.GetTempPath();
//            var zipFilePath = Path.Combine(tempPath, tempFileName);
//            ZipFile.CreateFromDirectory(tempFileName, tempPath);
//            File.WriteAllBytes(zipFilePath, memoryStream.ToArray());

//            return ZipFile.OpenRead(zipFilePath);
//        }
//    }
//}
