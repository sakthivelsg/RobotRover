using RobotRover.RoverAction;
using RobotRover.RoverService;
using System;

namespace RobotRover
{
    /// <summary>
    /// Initial startup class
    /// </summary>
    public class Rover
    {
        /// <summary>
        /// Starting method of the program.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            ICalculatePosition calculatePosition = new CalculatePosition();
            IRoverPosition roverPosition = new RoverPosition();
            ICommandSequence command = new CommandSequence();

            Tuple<int, int> gridSize = Tuple.Create(40,30);
            //Initial Position is set only for the new Rovers, existing Rovers use last executed position
            Tuple<int, int, string> initialPosition = Tuple.Create(10, 10, "N");

            while (true)
            {
                Console.WriteLine("Enter name of the new or existing Rover:");
                string roverName = Console.ReadLine().Trim();
                while(string.IsNullOrEmpty(roverName))
                {
                    Console.WriteLine("Please enter a valid name for Rover:");
                    roverName = Console.ReadLine().Trim();
                }
                if (roverName.Equals("exit", StringComparison.OrdinalIgnoreCase))
                {
                    break;
                }
                Console.WriteLine($"Enter the move sequence for the  Rover : {roverName}");
                string roverMovement = Console.ReadLine().Trim();
                while (string.IsNullOrEmpty(roverMovement))
                {
                    Console.WriteLine($"Please enter valid move sequence for the  Rover : {roverName}");
                    roverMovement = Console.ReadLine().Trim();
                }
                if (roverMovement.Equals("exit", StringComparison.OrdinalIgnoreCase))
                {
                    break;
                }

                try
                {                    
                    var roverNewPosition = new RobotSpecification(command, roverPosition, calculatePosition).RobotAction(gridSize, roverMovement, roverName,initialPosition);
                    Console.WriteLine($"{roverNewPosition.Item1} {roverNewPosition.Item2} {roverNewPosition.Item3}");
                }
                catch(ArgumentOutOfRangeException)
                {
                    Console.WriteLine($"The given sequence {roverMovement} for Rover {roverName} is taking it out side of the boundary which is invalid.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("There is an error during the Rover move. Detailed Exception : " + ex.ToString());
                }
            }
            Environment.Exit(0);            
        }      
    }
}
