using System.Collections.Generic;
using Tecnomatix.Engineering;
using Robworld.ProcessSimulate.Library.Utilities;
using Robworld.ProcessSimulate.Library.Services;
using System.Collections;
using System.Linq;
using System.IO;
using System.Text;

namespace Robworld.ProcessSimulate.CECommands.RobotDocumentation
{
    /// <summary>
    /// Base class for all Documentation commands
    /// </summary>
    internal static class RwRobotDocumentationBase
    {
        /// <summary>
        /// Base method of all documentation commands. Decide here which specific document to use
        /// </summary>
        /// <param name="specificDocument"></param>
        internal static void Create(string specificDocument)
        {
            /* 1st Step: Get the list of selected robots:
             * Remark:This 1st step block of code is identical to some other commands
             * so consider to make this block global
             */
            TxObjectList objects = TxApplication.ActiveDocument.Selection.GetAllItems();
            if (objects == null || objects.Count == 0)
            {
                TxMessageBox.ShowModal("No valid robots in the selection!", "Robot documentation error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return;
            }

            /* 2nd Step: Select the target directory
             */
            string targetDirectory = RwFileAndDirectoryUtilities.FolderBrowsingDialog(new RwFolderBrowserDialogCreationData { Description = "Select target folder", ShowNewFolderButton = true });
            if (string.IsNullOrEmpty(targetDirectory))
            {
                TxMessageBox.ShowModal("Document creation aborted due to missing target directory", "Target directory chooser", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Asterisk);
                return;
            }

            RwStudyDataService studyDataService = new RwStudyDataService();
            List<RwRobotDataService> robotDataServices = new List<RwRobotDataService>();
            foreach (TxRobot robot in objects.Cast<TxRobot>())
            {
                robotDataServices.Add(new RwRobotDataService(robot));
            }

            foreach (RwRobotDataService robotDataService in robotDataServices)
            {
                string filename = $"{ studyDataService.StudyName }___{ robotDataService.RobotName }.txt";
                string path = RwFileAndDirectoryUtilities.CombinePathSegments(targetDirectory, filename);
                StringBuilder sb = new StringBuilder();
                foreach (TxRoboticParam parameter in robotDataService.InstanceParameters)
                {
                    sb.AppendLine(parameter.Type.ToString());
                    if (parameter is TxRoboticBoolParam boolParameter)
                    {
                        sb.AppendLine(boolParameter.Value.ToString());
                    }
                    else if (parameter is TxRoboticIntParam doubleParameter)
                    {
                        sb.AppendLine(doubleParameter.Value.ToString());
                    }
                    else if (parameter is TxRoboticIntParam intParameter)
                    {
                        sb.AppendLine(intParameter.Value.ToString());
                    }
                    else if (parameter is TxRoboticStringParam stringParameter)
                    {
                        sb.AppendLine(stringParameter.Value.ToString());
                    }
                    else if (parameter is TxRoboticTransformationParam transformationParameter)
                    {
                        sb.AppendLine(transformationParameter.Value.ToString());
                    }
                    else if (parameter is TxRoboticTxObjectParam objectParameter)
                    {
                        sb.AppendLine(objectParameter.Value.ToString());
                    }
                    else if (parameter is TxRoboticXmlParam xmlParameter)
                    {
                        sb.AppendLine(xmlParameter.Value.ToString());
                    }
                    else
                    {
                        sb.AppendLine("Unknown parameter");
                    }
                }
                File.WriteAllText(path, sb.ToString());
            }

            /* 3rd Step: Choose the specific creation command and pass robots and target directory
             */
                //bool hasErrors = false;
                //switch (specificDocument)
                //{
                //    //Cases are similar to the name strings in the resx file
                //    case "Text":
                //        ArrayList parameters = RwRobotDataService.GetAllRobotInstanceParameters(objects[0] as TxRobot);
                //        break;
                //    case "RobotdataToPowerpoint":
                //        //RwRobotDocumentationPowerpoint pptDocument = new RwRobotDocumentationPowerpoint(targetDirectory, robots: objects);
                //        //hasErrors = pptDocument.HasErrors;
                //        break;
                //    case "RobotdataToExcel":
                //        //RwRobotDocumentationExcel xlsDocument = new RwRobotDocumentationExcel(targetDirectory, robots: objects);
                //        //hasErrors = xlsDocument.HasErrors;
                //        break;
                //    default:
                //        hasErrors = true;
                //        break;
                //}

                //RwGarbageCollector.CollectGarbage();

                //if (hasErrors)
                //{
                //    TxMessageBox.ShowModal("One or more errors ocurred during robot document creation!", "Robot document creation error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                //}
                //else
                //{
                //    TxMessageBox.ShowModal("Robot document creation finished successfully!", "Robot document creation result", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                //}
        }

        //internal static List<KeyValuePair<string, string>> GetRobotInformations(ITxRobot robot)
        //{
        //    List<KeyValuePair<string, string>> informations = new List<KeyValuePair<string, string>>
        //    {
        //        new KeyValuePair<string, string>("Name", robot.Name)
        //    };

        //    return informations;
        //}

        //internal static RwRobot CollectRobotData(ITxRobot robot)
        //{
        //    RwRobot robotData;
        //    switch (robot.Controller.Name)
        //    {
        //        case "Kuka-Krc":
        //        case "Kuka-Krc-Bmw":
        //        case "Kuka-Krc-Volvo":
        //            robotData = new RwKukaRobot(robot as TxRobot);
        //            break;
        //        case "Kuka-Vkrc":
        //            robotData = new RwVkrcRobot(robot as TxRobot);
        //            break;
        //        case "Fanuc-Rj-Vw":
        //            robotData = new RwFanucRobot(robot as TxRobot);
        //            break;
        //        default:
        //            robotData = null;
        //            break;
        //    }
        //    return robotData;
        //}
    }
}
