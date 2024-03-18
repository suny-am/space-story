using API.Controllers;
using API.Models;
using API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace API.Tests;

public class SaveControllerTestsc
{
    [Fact]
    public async Task Get_Returns_OKResult_With_Entity()
    {
        // ARRANGE
        var mockContext = new Mock<GameDbContext>();
        var mockRepository = new Mock<IRepository<SaveState>>();
        var targetGuid = Guid.NewGuid();
        var mockSaveState = new SaveState { Id = targetGuid, Data = "Some data here" };
        mockRepository.Setup(r => r.GetByID(targetGuid).Result).Returns(mockSaveState);
        var mockSaveStateController = new SaveStateController(mockRepository.Object);

        // ACT
        var result = await mockSaveStateController.GetSaveState(targetGuid);

        // ASSERT
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var model = Assert.IsType<ActionResult<SaveState>>(okResult.Value);
        var saveStateModel = Assert.IsType<SaveState>(model.Value);
        Assert.Equal(targetGuid, saveStateModel.Id);
        Assert.Equal("Some data here", saveStateModel.Data);

    }
}