using Xunit;
using Moq;
using KFC.Controllers;
using KFC.UseCases.InputPort;
using KFC.UseCases.OutputPort;
using KFC.UseCases.DTOs.Input;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

public class CreateProductControllerTests
{
    [Fact]
    public async Task Create_CallsInputPortAndReturnsActionResult()
    {
        // Arrange
        var mockInputPort = new Mock<ICreateProductInputPort>();
        var mockOutputPort = new Mock<ICreateProductOutputPort>();
        var controller = new CreateProductController(mockInputPort.Object, mockOutputPort.Object);
        var dto = new CreateProductDto { Name = "Pollo Broaster", Description = "Clásico" };

        // Act
        var result = await controller.Create(dto);

        // Assert
        mockInputPort.Verify(i => i.Handle(It.IsAny<CreateProductDto>()), Times.Once);
        Assert.IsAssignableFrom<IActionResult>(result);
    }
} 