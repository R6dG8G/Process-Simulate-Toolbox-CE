using Tecnomatix.Engineering;

namespace Robworld.ProcessSimulate.Library.Services
{
    public class RwStudyDataService
    {
        #region Properties
        public string StudyName { get; private set; }
        #endregion

        #region Constructors
        public RwStudyDataService()
        {
            StudyName = TxApplication.ActiveDocument.CurrentStudy.Name;
        }
        #endregion


    }
}
