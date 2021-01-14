namespace GasSim
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>A collection of cells that share the same fluids</summary>
    public class CellGroup : ICellGroup
    {
        private bool enabled;
        private bool expand;
        private IDictionary<string, IFluid> fluids;
        private Queue<ICell> fringe;
        private ISet<ICell> group;
        private int id;
        private ISet<ICellGroup> linkedGroups;
        private bool simulate;
        private bool stable;

        /// <summary>Initialises a new instance of the <see cref="CellGroup" /> class</summary>
        public CellGroup()
        {
            this.Simulate = true;
            this.Group = new HashSet<ICell>();
            this.Fluids = new Dictionary<string, IFluid>();
            this.Fringe = new Queue<ICell>();
        }

        /// <summary>Initialises a new instance of the <see cref="CellGroup" /> class</summary>
        /// <param name="rhs">The <see cref="CellGroup" /> to copy</param>
        public CellGroup(CellGroup rhs)
        {
            this.Enabled = rhs.Enabled;
            this.Expand = rhs.Expand;
            this.Fluids = rhs.Fluids;
            this.Fringe = rhs.Fringe;
            this.Group = rhs.Group;
            this.LinkedGroups = rhs.LinkedGroups;
            this.Simulate = rhs.Simulate;
            this.Stable = rhs.Stable;
        }

        /// <summary>Initialises a new instance of the <see cref="CellGroup" /> class</summary>
        /// <param name="simulate">Is this <see cref="CellGroup" /> simulated</param>
        public CellGroup(bool simulate)
        {
            this.Simulate = simulate;
            this.Group = new HashSet<ICell>();
            this.Fluids = new Dictionary<string, IFluid>();
            this.Fringe = new Queue<ICell>();
        }

        /// <summary>Initialises a new instance of the <see cref="CellGroup" /> class</summary>
        /// <param name="simulate">Is this <see cref="CellGroup" /> simulated</param>
        /// <param name="initialCells">The initial set of <see cref="ICell" /></param>
        /// <param name="fluids">The initial set of <see cref="IFluid" /></param>
        public CellGroup(bool simulate, ISet<ICell> initialCells, IDictionary<string, IFluid> fluids)
        {
            this.Simulate = simulate;
            this.Fringe = new Queue<ICell>();

            foreach (ICell cell in initialCells ?? throw new ArgumentNullException(nameof(initialCells)))
            {
                this.Add(cell);
            }

            this.Fluids = fluids ?? throw new ArgumentNullException(nameof(fluids));
        }

        /// <summary>
        /// Gets a value indicating whether we want to perform simulation on these Cells (contents
        /// are NOT accessible)? Can be used for special areas or areas being removed
        /// </summary>
        public bool Enabled
        {
            get => this.enabled;
            private set => this.enabled = value;
        }

        /// <summary>
        /// Gets a value indicating whether this group can currently expand? Limit groups to max
        /// group size and link nearby groups
        /// </summary>
        public bool Expand
        {
            get => this.expand;
            private set => this.expand = value;
        }

        /// <summary>Gets the Fluids that exist in this group</summary>
        public IDictionary<string, IFluid> Fluids
        {
            get => this.fluids;
            private set => this.fluids = value;
        }

        /// <summary>Gets the cells that will be joined to this group</summary>
        public Queue<ICell> Fringe
        {
            get => this.fringe;
            private set => this.fringe = value;
        }

        /// <summary>Gets the set of cells in this grouping</summary>
        public ISet<ICell> Group
        {
            get => this.group;
            private set => this.group = value;
        }

        /// <summary>Gets the ID of the group</summary>
        public int ID
        {
            get => this.id;
            private set => this.id = value;
        }

        /// <summary>Gets other groups that share a border with this one, shares fluids</summary>
        public ISet<ICellGroup> LinkedGroups
        {
            get => this.linkedGroups;
            private set => this.linkedGroups = value;
        }

        /// <summary>
        /// Gets a value indicating whether we want to perform simulation on these Cells (contents
        /// are STILL accessible)? Can be used for special areas or areas being removed
        /// </summary>
        public bool Simulate
        {
            get => this.simulate;
            private set => this.simulate = value;
        }

        /// <summary>
        /// Gets a value indicating whether this group is in a stable state (no recent changes)?
        /// will ignore simulation until modified. Used for areas that aren't being changed to
        /// improve performance
        /// </summary>
        public bool Stable
        {
            get => this.stable;
            private set => this.stable = value;
        }

        /// <summary>Adds a cell to a <see cref="CellGroup" /></summary>
        /// <param name="cell">The cell to add to the group</param>
        public void Add(ICell cell)
        {
            if (cell.Group == null)
            {
                this.Group.Add(cell);
                cell.Group = this;

                foreach (ICell c in cell.Neighbours.Except(this.Group))
                {
                    if ((c.Group == null) && (!this.Fringe.Contains(c)))
                    {
                        this.Fringe.Enqueue(c);
                    }
                }
            }
        }

        /// <summary>Adds a fluid to the group</summary>
        /// <param name="fluid">The fluid to be added to the group</param>
        /// <returns>true if successful</returns>
        public bool AddFluid(IFluid fluid)
        {
            if (this.fluids.Any(p => p.Key == fluid.ID))
            {
                float temp = ((this.fluids[fluid.ID].Pressure * this.fluids[fluid.ID].Temperature)
                           + (fluid.Pressure * fluid.Temperature))
                           / (this.fluids[fluid.ID].Pressure + fluid.Pressure);

                this.fluids[fluid.ID].Pressure += fluid.Pressure;
                this.fluids[fluid.ID].Temperature = temp;
            }
            else
            {
                this.fluids.Add(fluid.ID, fluid);
            }

            return true;
        }
    }
}