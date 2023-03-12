using System;
using Tecnomatix.Engineering;

namespace Robworld.ProcessSimulate.CECommands
{
    /// <summary>
    /// RobotDokumentation command class
    /// </summary>
    public class RwRobotDokumentationCommand : TxButtonCommand
    {
        #region Fields
        private readonly TxCommandTypeFilterMultipleObjEnabler enabler;
        #endregion

        #region Properties
        /// <summary>
        /// Get the Category under which the command appears
        /// </summary>
        public override string Category => "Robworld GmbH & Co. KG";

        /// <summary>
        /// Get a short description of the command
        /// </summary>
        public override string Description => "Collect robot data and save it to the file system";

        /// <summary>
        /// Shows the tooltip of the command
        /// </summary>
        public override string Tooltip => "Collect robot data and save it to the file system";

        /// <summary>
        /// Get the name of the command
        /// </summary>
        public override string Name => "Document Robot datas";

        /// <summary>
        /// Get the 16x16 bitmap of the command
        /// </summary>
        public override string Bitmap => "Images.RobotDocumentationXls16x16.bmp";

        /// <summary>
        /// Get the 32x32 bitmap of the command
        /// </summary>
        public override string LargeBitmap => "Images.RobotDocumentationXls32x32.png";

        /// <summary>
        /// Get the command enabler
        /// </summary>
        public override ITxCommandEnabler CommandEnabler
        {
            get { return enabler; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new instance of the RobotDokumentation command
        /// </summary>
        public RwRobotDokumentationCommand()
        {
            enabler = new TxCommandTypeFilterMultipleObjEnabler(new TxTypeFilter(typeof(ITxRobot)));
        }
        #endregion

        #region Methods
        /// <summary>
        /// Executes the RobotDokumentation command
        /// </summary>
        /// <param name="cmdParams"></param>
        public override void Execute(object cmdParams)
        {
            try
            {
                RobotDocumentation.RwRobotDocumentationBase.Create("Text");
            }
            catch (TxArgumentException exception)
            {
                string caption = "RobotDokumentation argument exception!";
                TxMessageBox.ShowModal(exception.Message, caption, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
            }
            catch (Exception exception)
            {
                string caption = "RobotDokumentation exception!";
                TxMessageBox.ShowModal(exception.Message, caption, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
            }
        }

        /// <summary>
        /// Activate the command if conditions are okay
        /// </summary>
        /// <returns>The boolean value of the conditions check</returns>
        public override bool Connect()
        {
            return true;
        }
        #endregion
    }
}
