using Xunit;
using Moq;
using KFC.UseCases.Interactor;
using KFC.UseCases.Interface;
using KFC.UseCases.DTOs.Input;
using KFC.UseCases.InputPort;
using KFC.UseCases.OutputPort;
using System.Threading.Tasks;

public class CreateProductInteractorTests
{
    [Fact]
    public async Task Handle_ValidInput_CreatesProduct()
    {
        // Arrange
        var mockRepo = new Mock<IProductRepository>();
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        var mockAccountRepo = new Mock<IAccountRepository>();
        var mockOutputPort = new Mock<ICreateProductOutputPort>();
        var mockValidator = new Mock<IInputPortValidator<CreateProductDto>>();
        mockValidator.Setup(v => v.IsValid(It.IsAny<CreateProductDto>())).ReturnsAsync(true);

        var interactor = new CreateProductInteractor(
            mockUnitOfWork.Object,
            mockRepo.Object,
            mockAccountRepo.Object,
            mockOutputPort.Object,
            mockValidator.Object
        );

        var dto = new CreateProductDto { Name = "Pollo Broaster", Description = "Clásico" };

        // Act
        await interactor.Handle(dto);

        // Assert
        mockRepo.Verify(r => r.CreateAsync(It.IsAny<KFC.Entities.Product>()), Times.Once);
        mockUnitOfWork.Verify(u => u.SaveChanges(), Times.Once);
    }
} 