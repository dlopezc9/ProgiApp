using Moq;
using ProgiApp.Domain.Interfaces;
using ProgiApp.Domain.Models;
using ProgiApp.Services.Commands.Vehicles;
using ProgiApp.Services.CommandsHandler.Vehicles;

namespace ProgiApp.API.Tests.Commands;

public class InsertVehicleCommandTest
{
    [Fact]
    public async void InsertVehicleCommandHandlerIsSuccessful()
    {
        // Arrange
        var mockVehicleRepository = new Mock<IVehicleRepository>();
        mockVehicleRepository.Setup(x => x.Add(It.IsAny<Vehicle>()));

        var mockVehicle = new Vehicle()
        {
            Id = 0,
            TypeId = 1,
            VehiclePrice = 999
        };

        var mockInsertVehicleCommand = new InsertVehicleCommand(mockVehicle);
        var mockCommand = new InsertVehicleCommandHandler(mockVehicleRepository.Object);

        // Act
        await mockCommand.Handle(mockInsertVehicleCommand, default(CancellationToken));

        // Assert
        mockVehicleRepository.Verify(x => x.Add(It.IsAny<Vehicle>()), Times.Once);
    }
}
