using System.Collections.Generic;
using RobotRover.RoverService;

namespace RobotRover.RoverAction
{
    /// <summary>
    /// Class to parse the input from commmand line
    /// </summary>
    public class CommandSequence : ICommandSequence
    {
        /// <summary>
        /// Parse the input from command line.
        /// </summary>
        /// <param name="commands">input from command line</param>
        /// <returns>Returns the movement direction with count</returns>
        public List<string> GetRoverMovementCommand(string commands)
        {
            List<string> result = new List<string>();
            string temp = string.Empty;
            foreach (char c in commands)
            {
                if (c == 'L')
                {
                    if (!string.IsNullOrEmpty(temp))
                        result.Add(temp);
                    temp = "L-";
                }
                else if (c == 'R')
                {
                    if (!string.IsNullOrEmpty(temp))
                        result.Add(temp);
                    temp = "R-";
                }
                else
                {
                    temp += c;
                }
            }
            if (!string.IsNullOrEmpty(temp))
                result.Add(temp);
            return result;
        }
    }
}
