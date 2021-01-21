namespace GasSim
{
    /// <summary>Provides a unique id for a cell</summary>
    public class UniqueCellID
    {
        private long cellID;

        /// <summary>Gets a unique CellID</summary>
        public long CellID
        {
            get
            {
                return ++this.cellID;
            }
        }
    }
}