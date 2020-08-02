using System;

namespace RobotRover.RoverService
{
    public interface IRoverPosition
    {
        Tuple<int, int, string> GetPosition(string rover,Tuple<int,int,string> intialPosition);
        void SetPosition(int x, int y, string direction, string rover);
    }
}