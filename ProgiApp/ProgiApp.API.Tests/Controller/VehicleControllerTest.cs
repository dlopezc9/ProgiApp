using MediatR;
using Moq;
using ProgiApp.API.Controllers;
using ProgiApp.Domain.Models;
using ProgiApp.Services.Commands.Vehicles;
using ProgiApp.Services.Querys.Vehicles;

namespace ProgiApp.API.Tests.Controller;

public class VehicleControllerTest
{
    [Fact]
    public async void GetAllGetsCalled()
    {
        //Arrange
        var mockMediatR = new Mock<IMediator>();
        var controller = new VehiclesController(mockMediatR.Object);

        //Act
        var result = await controller.GetAll();

        //Assert
        mockMediatR.Verify(x => x.Send(It.IsAny<GetAllVehiclesQuery>(), default), Times.Once);
    }

    [Fact]
    public async void InsertGetsCalled()
    {
        //Arrange
        var mockMediatR = new Mock<IMediator>();
        var controller = new VehiclesController(mockMediatR.Object);

        var mockVehicle = new Vehicle()
        {
            Id = 0,
            TypeId = 1,
            VehiclePrice = 999
        };

        //Act
        await controller.Insert(mockVehicle);

        //Assert
        mockMediatR.Verify(x => x.Send(It.IsAny<InsertVehicleCommand>(), default), Times.Once);
    }
}