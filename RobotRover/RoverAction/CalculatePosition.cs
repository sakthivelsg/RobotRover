using RobotRover.RoverService;
using System;
using System.Collections.Generic;

namespace RobotRover.RoverAction
{
    /// <summary>
    /// Calculate the Rover movements
    /// </summary>
    public class CalculatePosition : ICalculatePosition
    {
        private readonly Dictionary<string, string> RoverDirection = new Dictionary<string, string>();

        /// <summary>
        /// Static rover location for moves
        /// </summary>
        public CalculatePosition()
        {
            RoverDirection.Add("NR", "E");
            RoverDirection.Add("NL", "W");
            RoverDirection.Add("ER", "S");
            RoverDirection.Add("EL", "N");
            RoverDirection.Add("SL", "E");
            RoverDirection.Add("SR", "W");
            RoverDirection.Add("WL", "S");
            RoverDirection.Add("WR", "N");
        }

        /// <summary>
        /// Change the rover position 
        /// </summary>
        /// <param name="x">Rover's X location</param>
        /// <param name="y">Rover's Y location</param>
        /// <param name="a">Number of steps to move</param>
        /// <param name="direction">Direction of the move</param>
        /// <returns></returns>
        public Tuple<int, int, string> ChangePosition(int x, int y, int a, string direction)
        {
            switch (direction)
            {
                case "N":
                    x = x + a;
                    break;
                case "E":
                    y = y + a;
                    break;
                case "W":
                    y = y - a;
                    break;
                case "S":
                    x = x - a;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return Tuple.Create(x, y, direction);
        }

        /// <summary>
        /// Change the direction
        /// </summary>
        /// <param name="Rotation"></param>
        /// <returns></returns>
        public string ChangeDirection(string Rotation)
        {
            return RoverDirection[Rotation];

        }
    }
}
