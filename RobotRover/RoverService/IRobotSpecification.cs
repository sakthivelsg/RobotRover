using System;

namespace RobotRover.RoverService
{
    public interface IRobotSpecification
    {
        Tuple<int,int,string> RobotAction(Tuple<int, int> GridSize, string RoverMoveCommand, string RoverName, Tuple<int, int, string> IntialPosition);
    }
}