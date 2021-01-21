namespace GasSim
{
    using System.Collections.Generic;

    /// <summary>A set of cells inside a simulation space</summary>
    public interface ICellGroup
    {
        /// <summary>
        /// Gets a value indicating whether or not to perform simulation on these Cells (contents
        /// are NOT accessible)? Can be used for special areas or areas being removed
        /// </summary>
        public bool Enabled
        {
            get;
        }

        /// <summary>
        /// Gets or sets a value indicating whether this group can currently expand? Limit groups to
        /// max group size and link nearby groups
        /// </summary>
        public bool Expand
        {
            get;
            set;
        }

        /// <summary>Gets the Fluids that exist in this group</summary>
        public IDictionary<string, IFluid> Fluids
        {
            get;
        }

        /// <summary>Gets the cells that will be joined to this group</summary>
        public Queue<ICell> Fringe
        {
            get;
        }

        /// <summary>Gets the set of cells in this grouping</summary>
        public ISet<ICell> Group
        {
            get;
        }

        /// <summary>Gets the ID of the group</summary>
        public int ID
        {
            get;
        }

        /// <summary>Gets the other groups that share a border with this one, shares fluids</summary>
        public ISet<ICellGroup> LinkedGroups
        {
            get;
        }

        /// <summary>
        /// Gets a value indicating whether or not to perform simulation on these Cells (contents
        /// are STILL accessible)? Can be used for special areas or areas being removed
        /// </summary>
        public bool Simulate
        {
            get;
        }

        /// <summary>
        /// Gets a value indicating whether this group in a stable state (no recent changes)? will
        /// ignore simulation until modified. Used for areas that aren't being changed to improve performance
        /// </summary>
        public bool Stable
        {
            get;
        }

        /// <summary>Adds a cell to a <see cref="CellGroup" /></summary>
        /// <param name="cell">The cell to add to the group</param>
        public void Add(ICell cell);

        /// <summary>Adds a fluid to the group</summary>
        /// <param name="fluid">The fluid to be added to the group</param>
        /// <returns>true if successful</returns>
        public bool AddFluid(IFluid fluid);
    }
}