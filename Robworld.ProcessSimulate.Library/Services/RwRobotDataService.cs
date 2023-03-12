using System.Collections;
using System.Collections.Generic;
using Tecnomatix.Engineering;

namespace Robworld.ProcessSimulate.Library.Services
{
    public class RwRobotDataService
    {
        #region Fields
        private readonly TxRobot robot;
        #endregion

        #region Properties
        public string ControllerName { get; }

        public string RobotName { get; }

        public ArrayList InstanceParameters { get; private set; }

        public TxObjectList SystemFrames { get; private set; }
        #endregion

        #region Constructors
        public RwRobotDataService(TxRobot robot)
        {
            this.robot = robot;
            ControllerName = robot.Controller.Name;
            RobotName = robot.Name;
            GetAllRobotInstanceParameters();
            GetAllSystemFrames();
        }
        #endregion
        private void GetAllRobotInstanceParameters() 
        {
            InstanceParameters = robot.GetAllInstanceParameters();
        }

        private void GetAllSystemFrames()
        {
            SystemFrames = robot.GetAllSystemFrames();
        }
    }
}
