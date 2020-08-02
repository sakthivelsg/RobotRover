using RobotRover.RoverService;
using System;
using System.Collections.Generic;

namespace RobotRover.RoverAction
{
    /// <summary>
    /// Class to handle the rover movements
    /// </summary>
    public class RoverPosition : IRoverPosition
    {
        Dictionary<string, Tuple<int, int, string>> rovers = new Dictionary<string, Tuple<int, int, string>>() {  };
        /// <summary>
        /// Get the position of the Rover, if new Rover, insert the rover name and position.
        /// </summary>
        /// <param name="roverName">Rover Name</param>
        /// <param name="initialPosition">Initial Position of Rover</param>
        /// <returns></returns>
        public Tuple<int,int,string> GetPosition(string roverName, Tuple<int, int, string> initialPosition)
        {
            if (!rovers.ContainsKey(roverName)) 
            {
                rovers.Add(roverName, initialPosition);
                return rovers[roverName];
            }
            else
            {
                return rovers[roverName];
            }
            
        }

        /// <summary>
        /// Sets the rover position to the new location
        /// </summary>
        /// <param name="x">Rover's Location value of X</param>
        /// <param name="y">Rover's Location value of Y</param>
        /// <param name="direction">Direction of Rover</param>
        /// <param name="roverName">Rover Name</param>
        public void SetPosition(int x, int y, string direction, string roverName)
        {
            if (rovers[roverName] == null)
            {
                rovers.Add(roverName, new Tuple<int, int, string>(x, y, direction));
            }
            else
            {
                rovers[roverName] = new Tuple<int, int, string>(x, y, direction);
            }
        }
    }
}
