using System;
using RobotRover.RoverService;

namespace RobotRover.RoverAction
{
    /// <summary>
    /// Class to handle and executes the move.
    /// </summary>
    public class RobotSpecification : IRobotSpecification
    {
        private readonly ICommandSequence _commandSequence;
        private readonly IRoverPosition _roverPosition;
        private readonly ICalculatePosition _calculatePosition;

        /// <summary>
        /// Constructor for the support class initialization
        /// </summary>
        /// <param name="commandSequence">commandSequence class initializer</param>
        /// <param name="roverPosition">roverPosition class initializer</param>
        /// <param name="calculatePosition">calculatePosition class initializer</param>
        public RobotSpecification(ICommandSequence commandSequence,IRoverPosition roverPosition,ICalculatePosition calculatePosition)
        {
            _commandSequence = commandSequence;
            _roverPosition = roverPosition;
            _calculatePosition = calculatePosition;
        }

        /// <summary>
        /// Main method which handles the split the command and executes Rover moves
        /// </summary>
        /// <param name="gridSize">Size of the grid that Rover moves</param>
        /// <param name="roverMoveCommand">Command for Rover moves</param>
        /// <param name="roverName">Name of the Rover</param>
        /// <param name="intialPosition">Starting position of the Rover.</param>
        /// <returns>Returns the new position of the Rover</returns>
        public Tuple<int, int, string> RobotAction(Tuple<int,int> gridSize,string roverMoveCommand,string roverName,Tuple<int,int,string> intialPosition)
        {
            var roverCommands = _commandSequence.GetRoverMovementCommand(roverMoveCommand);
            var roverCurrentPosition = _roverPosition.GetPosition(roverName, intialPosition);
            Tuple<int, int, string> position = roverCurrentPosition;
            foreach(string cmd in roverCommands)
            {
                var direction = cmd.Split('-');
                string currentDirection = position.Item3 + direction[0];
                string value = _calculatePosition.ChangeDirection(currentDirection);
                var newPosition = _calculatePosition.ChangePosition(position.Item1, position.Item2, Convert.ToInt32(direction[1]), value);
                if (newPosition.Item2 >= 0 && newPosition.Item1 >= 0 && newPosition.Item1 <= gridSize.Item1 && newPosition.Item2 <= gridSize.Item2)
                {
                    position = newPosition;
                }
                else
                {
                    position = roverCurrentPosition;
                    throw new ArgumentOutOfRangeException();
                }
            }
            _roverPosition.SetPosition(position.Item1,position.Item2,position.Item3, roverName);
            return position;

        }
    }
}
