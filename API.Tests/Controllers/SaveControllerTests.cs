using API.Controllers;
using API.Models;
using API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace API.Tests;

public class SaveControllerTests
{

    private Mock<GameDbContext> _mockContext = new();
    private Mock<IRepository<SaveState>> _mockRepository = new();
    private Guid _targetGuid;
    private SaveState _mockSaveState = null!;
    private SaveStateController _mockSaveStateController = null!;

    private void Setup()
    {
        _mockContext = new Mock<GameDbContext>();
        _mockRepository = new Mock<IRepository<SaveState>>();
        _targetGuid = Guid.NewGuid();
        _mockSaveState = new SaveState { Id = _targetGuid, Data = "Some data here" };
        _mockRepository.Setup(r => r.GetByID(_targetGuid).Result).Returns(_mockSaveState);
        _mockSaveStateController = new SaveStateController(_mockRepository.Object);
    }

    [Fact]
    public async Task Get_Returns_OKResult()
    {
        // ARRANGE
        Setup();

        // ACT
        var response = await _mockSaveStateController.Get(_targetGuid);

        // ASSERT
        Assert.IsType<OkObjectResult>(response.Result);
    }

    [Fact]
    public async Task Get_Returns_ActionResult_SaveState()
    {
        // ARRANGE
        Setup();

        // ACT
        var response = await _mockSaveStateController.Get(_targetGuid);
        var result = response.Result as OkObjectResult;
        Assert.IsType<ActionResult<SaveState>>(result!.Value);
    }

    [Fact]
    public async Task Get_Returns_SaveState()
    {
        // ARRANGE
        Setup();

        // ACT
        var response = await _mockSaveStateController.Get(_targetGuid);
        var result = response.Result as OkObjectResult;
        var model = result!.Value as ActionResult<SaveState>;
        var saveStateModel = Assert.IsType<SaveState>(model!.Value);

        Assert.IsType<SaveState>(saveStateModel);
    }
    [Fact]
    public async Task Get_Returns_SaveState_With_Correct_Data()
    {
        // ARRANGE
        Setup();

        // ACT
        var response = await _mockSaveStateController.Get(_targetGuid);
        var result = response.Result as OkObjectResult;
        var model = result!.Value as ActionResult<SaveState>;
        var saveStateModel = model!.Value;

        Assert.Equal(_targetGuid, saveStateModel!.Id);
        Assert.Equal("Some data here", saveStateModel!.Data);

    }
}