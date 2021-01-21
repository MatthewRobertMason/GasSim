namespace GasSim
{
    using System.Collections.Generic;

    /// <summary>Interface for cells in a simulation space</summary>
    public interface ICell
    {
        /// <summary>Gets or sets the group that this cell belongs to (if any)</summary>
        public ICellGroup Group
        {
            get;
            set;
        }

        /// <summary>Gets the unique ID of this cell</summary>
        public long ID
        {
            get;
        }

        /// <summary>Gets the Neighbours of this cell, or cells that can receive transfer</summary>
        public IList<ICell> Neighbours
        {
            get;
        }

        /// <summary>Adds <see cref="IFluid" /> to the current cell</summary>
        /// <param name="fluid">The <see cref="IFluid" /> to add to the <see cref="ICell" /></param>
        /// <returns>True if fluid was successfully added to cell</returns>
        public bool AddFluid(IFluid fluid);
    }
}