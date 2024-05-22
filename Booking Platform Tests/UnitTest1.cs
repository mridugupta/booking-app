using Booking_Platform.Controllers;
using Microsoft.Extensions.Logging;

namespace Booking_Platform_Tests;

public class Tests
{

    //private readonly ILogger<RoomController> _logger;
    private readonly static RoomController _roomController = new RoomController(new ILogger<RoomController>());

    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void GetAllRooms()
    {
        var rooms = _roomController.Rooms();
        
        /*Assert.IsInstanceOfType(result, typeof(ViewResult));
        var actual = ((ViewResult)result).Model as RoomDto[];
        Assert.IsNotNull(actual);
        Assert.AreEqual(3, actual.Length);*/
    }
}
