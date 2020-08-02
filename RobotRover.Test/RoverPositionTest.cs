using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RobotRover.RoverService;
using RobotRover.RoverAction;

namespace RobotRover.Test
{
    [TestClass]
    public class Robot_Rover_Position_Validation
    {   
        ICalculatePosition calculatePosition = new CalculatePosition();
        IRoverPosition roverPosition = new RoverPosition();
        ICommandSequence command = new CommandSequence();
        Tuple<int, int> gridSize = Tuple.Create(40, 30);
        Tuple<int, int, string> intialPosition = Tuple.Create(10, 10, "N");

        [TestMethod]
        public void Rover_Position_Change_Test()
        {
            //Arrange
            var expectedPosition = Tuple.Create(8, 12, "E");

            //Act
            var position = new RobotSpecification(command, roverPosition, calculatePosition).RobotAction(gridSize, "L1L2L3", "R1", intialPosition);

            //Assert
            Assert.IsNotNull(position);
            Assert.AreEqual(expectedPosition, position);
        }

        [TestMethod]
        public void Rover_Direction_Change_Test()
        {
            //Arrange
            var expectedPosition = Tuple.Create(10, 10, "W");
            
            //Act
            var position = new RobotSpecification(command, roverPosition, calculatePosition).RobotAction(gridSize, "L0", "R1", intialPosition);

            //Assert
            Assert.IsNotNull(position);
            Assert.AreEqual(expectedPosition, position);
        }

        [TestMethod]
        public void Rover_Same_Direction_Move_Test()
        {
            //Arrange
            var expectedPosition = Tuple.Create(10, 10, "N");
            
            //Act
            var position = new RobotSpecification(command, roverPosition, calculatePosition).RobotAction(gridSize, "L0R0", "R1", intialPosition);
            
            //Assert
            Assert.IsNotNull(position);
            Assert.AreEqual(expectedPosition, position);
        }

        [TestMethod]
        public void Single_Rover_Multiple_Moves_Test()
        {
            //Arrange
            RobotSpecification robotSpecification = new RobotSpecification(command, roverPosition, calculatePosition);
            var expectedM1Position = Tuple.Create(19, 27, "N");
            var expectedM2Position = Tuple.Create(17, 29, "W");
            var intialPositionR1 = Tuple.Create(20, 20, "S");

            //Act
            var positionR1 = robotSpecification.RobotAction(gridSize, "L2R3L5L2", "R1", intialPositionR1);
            var positionR2 = robotSpecification.RobotAction(gridSize, "R1L1R2R3R1", "R1", positionR1);

            //Assert
            Assert.IsNotNull(positionR1);
            Assert.IsNotNull(positionR2);
            Assert.AreEqual(expectedM1Position, positionR1);
            Assert.AreEqual(expectedM2Position, positionR2);
        }

        [TestMethod]
        public void Single_Rover_Moves_From_Existing_Position_Test()
        {
            //Arrange
            RobotSpecification robotSpecification = new RobotSpecification(command, roverPosition, calculatePosition);
            var expectedR1Position = Tuple.Create(19, 27, "N");
            var expectedR2Position = Tuple.Create(17, 29, "W");
            var intialPositionR1 = Tuple.Create(20, 20, "S");

            //Act
            var positionR1 = robotSpecification.RobotAction(gridSize, "L2R3L5L2", "R1", intialPositionR1);
            var positionR2 = robotSpecification.RobotAction(gridSize, "R1L1R2R3R1", "R1", null);

            //Assert
            Assert.IsNotNull(positionR1);
            Assert.IsNotNull(positionR2);
            Assert.AreEqual(expectedR1Position, positionR1);
            Assert.AreEqual(expectedR2Position, positionR2);
        }

        [TestMethod]
        public void Multiple_Rover_Different_Position_Multiple_Moves_Test()
        {
            //Arrange
            RobotSpecification robotSpecification = new RobotSpecification(command, roverPosition, calculatePosition);
            var expectedR1Position = Tuple.Create(19, 27, "N");
            var expectedR2Position = Tuple.Create(10, 10, "N");
            var intialPositionR1 = Tuple.Create(20,20,"S");
            var intialPositionR2 = Tuple.Create(12, 12, "E");

            //Act
            var positionR1 = robotSpecification.RobotAction(gridSize, "L2R3L5L2", "R1", intialPositionR1);
            var positionR2 = robotSpecification.RobotAction(gridSize, "R1L1R2R3R1", "R2", intialPositionR2);

            //Assert
            Assert.IsNotNull(positionR1);
            Assert.IsNotNull(positionR2);
            Assert.AreEqual(expectedR1Position, positionR1);
            Assert.AreEqual(expectedR2Position, positionR2);
        }

        [TestMethod]
        public void Multiple_Rover_Same_Position_Same_Moves_Test()
        {
            //Arrange
            RobotSpecification robotSpecification = new RobotSpecification(command, roverPosition, calculatePosition);
            var intialPositionR1 = Tuple.Create(12, 12, "E");
            var intialPositionR2 = Tuple.Create(12, 12, "E");

            //Act
            var positionR1 = robotSpecification.RobotAction(gridSize, "R1L1R2R3R1", "R1", intialPositionR1);
            var positionR2 = robotSpecification.RobotAction(gridSize, "R1L1R2R3R1", "R2", intialPositionR2);

            //Assert
            Assert.IsNotNull(positionR1);
            Assert.IsNotNull(positionR2);
            Assert.AreEqual(positionR2, positionR1);
        }

        [TestMethod]
        public void Rover_OutOfRange_Move_Test()
        {
            //Arrange
            RobotSpecification robotSpecification = new RobotSpecification(command, roverPosition, calculatePosition);
            var expectedPosition = Tuple.Create(10, 10, "N");            
            var position = Tuple.Create(10, 10, "N");
            
            try
            {
                //Act
                position = robotSpecification.RobotAction(gridSize, "L25", "R1", intialPosition);
            }
            catch(ArgumentOutOfRangeException)
            {
                //Assert
                Assert.IsNotNull(position);
                Assert.AreEqual(expectedPosition, position);
            }           
        }
    }
}
