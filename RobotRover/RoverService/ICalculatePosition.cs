using System;

namespace RobotRover.RoverService
{
    public interface ICalculatePosition
    {
        string ChangeDirection(string Rotation);
        Tuple<int, int, string> ChangePosition(int x, int y, int a, string direction);
    }
}