namespace RadianceStandard.Utilities
{
#warning convert to singleton
    public class ToolKit
    {
        #region Ctors
        public ToolKit(IFileIO fileIO)
        {
            FileIO = fileIO;
        }
        #endregion

        #region fields

        #endregion

        #region Props
        public IFileIO FileIO { get; private set; }
        #endregion
    }
}
