using System.Collections.Generic;

namespace RobotRover.RoverService
{
    public interface ICommandSequence
    {
        List<string> GetRoverMovementCommand(string commands);
    }
}