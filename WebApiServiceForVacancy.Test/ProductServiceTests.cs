using AutoMapper;
using MediatR;
using Moq;
using WebApiServiceForVacancy.Core.DTOs;
using WebApiServiceForVacancy.CQRS.Models.Commands.ProductCommands;
using WebApiServiceForVacancy.CQRS.Models.Queries.ProductQueries;
using WebApiServiceForVacancy.Domain.Services;

namespace WebApiServiceForVacancy.Test
{
    public class ProductServiceTests
    {
        [Fact]
        public async Task GetByIdAsync_ReturnsProduct()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var mapperMock = new Mock<IMapper>();
            var expectedProduct = new ProductDto { Id = 1, Name = "TestProduct" };
            mediatorMock.Setup(m => m.Send(It.IsAny<GetProductByIdQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedProduct);

            var productService = new ProductService(mapperMock.Object, mediatorMock.Object);

            // Act
            var result = await productService.GetByIdAsync(1);

            // Assert
            Assert.Equal(expectedProduct.Id, result.Id);
            Assert.Equal(expectedProduct.Name, result.Name);
        }

        [Fact]
        public async Task GetByIdAsync_ThrowsException()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var mapperMock = new Mock<IMapper>();
            var productService = new ProductService(mapperMock.Object, mediatorMock.Object);

            var expectedExceptionMessage = "Test exception message";

            mediatorMock.Setup(m => m.Send(It.IsAny<GetProductByIdQuery>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception(expectedExceptionMessage));
            

            // Act & Assert
            var ex = await Assert.ThrowsAsync<InvalidOperationException>(async () => await productService.GetByIdAsync(1));
            Assert.Equal(expectedExceptionMessage, ex.Message);
        }


        [Fact]
        public async Task CreateAsync_ValidDto_CallsMediatorSend()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var mapperMock = new Mock<IMapper>();
            var productService = new ProductService(mapperMock.Object, mediatorMock.Object);

            var dto = new CreateNewProductDto { Name = "Tom", IsAvailable = true };

            // Act
            await productService.CreateAsync(dto);

            // Assert
            mediatorMock.Verify(m => m.Send(It.IsAny<CreateProductCommand>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task CreateAsync_ExceptionThrown_LogsErrorAndThrowsInvalidOperationException()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var mapperMock = new Mock<IMapper>();
            var productService = new ProductService(mapperMock.Object, mediatorMock.Object);

            var dto = new CreateNewProductDto { Name = "Tom", IsAvailable = true };
            var exceptionMessage = "Something went wrong";

            mediatorMock.Setup(m => m.Send(It.IsAny<CreateProductCommand>(), It.IsAny<CancellationToken>())).ThrowsAsync(new Exception(exceptionMessage));

            // Act & Assert
            var ex = await Assert.ThrowsAsync<InvalidOperationException>(async () => await productService.CreateAsync(dto));
            Assert.Equal(exceptionMessage, ex.Message);
        }

        [Fact]
        public async Task GetAllProductsAvailableForOrderAsync_ReturnsProducts()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var mapperMock = new Mock<IMapper>();
            var productService = new ProductService(mapperMock.Object, mediatorMock.Object);

            var expectedProducts = new List<ProductDto>
            {
                new ProductDto { Id = 1, Name = "TestProduct1" },
                new ProductDto { Id = 2, Name = "TestProduct2" }
            };

            mediatorMock.Setup(m => m.Send(It.IsAny<GetAllProductsAvailableForOrderQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedProducts);

            // Act
            var result = await productService.GetAllProductsAvailableForOrderAsync();

            // Assert
            Assert.Collection(result,
                item => Assert.Contains(expectedProducts, p => p.Id == item.Id && p.Name == item.Name),
                item => Assert.Contains(expectedProducts, p => p.Id == item.Id && p.Name == item.Name));
        }

        [Fact]
        public async Task GetAllProductsAvailableForOrderAsync_ThrowsException()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var mapperMock = new Mock<IMapper>();
            var productService = new ProductService(mapperMock.Object, mediatorMock.Object);

            var expectedExceptionMessage = "Test exception message";
            mediatorMock.Setup(m => m.Send(It.IsAny<GetAllProductsAvailableForOrderQuery>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception(expectedExceptionMessage));

            // Act & Assert
            var ex = await Assert.ThrowsAsync<InvalidOperationException>(async () => await productService.GetAllProductsAvailableForOrderAsync());
            Assert.Equal(expectedExceptionMessage, ex.Message);
        }

        [Fact]
        public async Task EditAsync_CallsMediatorWithCorrectCommand()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var mapperMock = new Mock<IMapper>();
            var productService = new ProductService(mapperMock.Object, mediatorMock.Object);

            var dto = new ProductDto { Id = 1, Name = "TestProduct" };

            // Act
            await productService.EditAsync(dto);

            // Assert
            mediatorMock.Verify(m => m.Send(It.Is<EditProductCommand>(dto => dto == dto), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task EditAsync_LogsAndThrowsException_WhenMediatorThrowsException()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var mapperMock = new Mock<IMapper>();
            var productService = new ProductService(mapperMock.Object, mediatorMock.Object);

            var expectedExceptionMessage = "Test exception message";
            mediatorMock.Setup(m => m.Send(It.IsAny<EditProductCommand>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception(expectedExceptionMessage));

            // Act & Assert
            var ex = await Assert.ThrowsAsync<InvalidOperationException>(async () => await productService.EditAsync(new ProductDto()));
            Assert.Equal(expectedExceptionMessage, ex.Message);
        }
    }
}
