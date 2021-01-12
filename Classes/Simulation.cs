namespace GasSim
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>A simulation space for fluid simulation</summary>
    public class Simulation : ISimulation
    {
        private ISet<ICell> cells;
        private IList<ICellGroup> groups;
        private int maxStepsToSimulate;

        /// <summary>Initialises a new instance of the <see cref="Simulation" /> class</summary>
        /// <param name="initialCells">The initial set of cells in the simulation</param>
        public Simulation(ISet<ICell> initialCells)
        {
            this.Cells = new HashSet<ICell>(initialCells);
            this.Groups = new List<ICellGroup>();
            this.Stable = false;
        }

        /// <summary>Gets or sets the cells in this simulation</summary>
        public ISet<ICell> Cells
        {
            get => this.cells;
            set => this.cells = value;
        }

        /// <summary>Gets or sets the groups in this simulation</summary>
        public IList<ICellGroup> Groups
        {
            get => this.groups;
            set => this.groups = value;
        }

        /// <summary>Gets or sets the maximum number of cells in a group</summary>
        public int MaxGroupSize
        {
            get;
            set;
        }

        /// <summary>Gets or sets the maximum number of cells to attempt to operate on per tick</summary>
        public int MaxStepsToSimulate
        {
            get => this.maxStepsToSimulate;
            set => this.maxStepsToSimulate = value;
        }

        /// <summary>Gets a value indicating whether the simulation is stable</summary>
        public bool Stable
        {
            get;
            private set;
        }

        /// <summary>Creates a new <see cref="CellGroup" /> in the simulation space</summary>
        /// <param name="simulate">Is this group simulated</param>
        /// <returns>A new <see cref="CellGroup" /> that is part of this simulation space</returns>
        public CellGroup CreateCellGroup(bool simulate)
        {
            CellGroup cellGroup = new CellGroup(simulate);
            this.Groups.Add(cellGroup);

            return cellGroup;
        }

        /// <summary>Creates a new <see cref="CellGroup" /> in the simulation space</summary>
        /// <param name="simulate">Is this group simulated</param>
        /// <param name="initialCells">
        /// The initial set of <see cref="Cell" /> to include in the <see cref="CellGroup" />
        /// </param>
        /// <param name="fluids">
        /// The initial <see cref="Fluid" /> in the <see cref="CellGroup" />
        /// </param>
        /// <returns>A new <see cref="CellGroup" /> that is part of this simulation space</returns>
        public CellGroup CreateCellGroup(bool simulate, ISet<ICell> initialCells, IDictionary<string, IFluid> fluids)
        {
            CellGroup cellGroup = new CellGroup(simulate, initialCells, fluids);
            this.Groups.Add(cellGroup);

            return cellGroup;
        }

        /// <summary>Expand the <see cref="CellGroup" /> into fringe cells</summary>
        /// <param name="stepsToSimulate">The maximum number of steps to attempt to simulate</param>
        /// <returns>The number of simulation steps remaining</returns>
        public int Expand(int stepsToSimulate)
        {
            ICellGroup currentGroup = this.groups.FirstOrDefault(p => p.Fringe != null ? p.Fringe.Any() : false);

            while ((stepsToSimulate > 0) && (currentGroup != null))
            {
                ICell cell = currentGroup.Fringe.FirstOrDefault();
                currentGroup.Fringe.Remove(cell);
                currentGroup.Add(cell);

                foreach (ICell c in cell.Neighbours)
                {
                    currentGroup.Fringe.Add(c);
                }

                stepsToSimulate -= cell.Neighbours.Count + 1;
            }

            return stepsToSimulate;
        }

        /// <summary>Method called to simulate the simulation space</summary>
        public void Simulate()
        {
            this.Simulate(this.MaxStepsToSimulate);
        }

        /// <summary>Method called to simulate the simulation space</summary>
        /// <param name="stepsToSimulate">The maximum number of steps to attempt to simulate</param>
        public void Simulate(int stepsToSimulate)
        {
            int initSteps = stepsToSimulate / 3;
            int stepsRemaining = stepsToSimulate;

            stepsRemaining -= this.Expand(initSteps);
            stepsRemaining -= this.Transfer(initSteps);
            stepsRemaining -= this.Split(initSteps);

            if (stepsRemaining > 0)
            {
                stepsRemaining -= this.Expand(stepsRemaining);
            }

            if (stepsRemaining > 0)
            {
                stepsRemaining -= this.Transfer(stepsRemaining);
            }

            if (stepsRemaining > 0)
            {
                stepsRemaining -= this.Split(stepsRemaining);
            }

            this.Stable = stepsRemaining == stepsToSimulate;
        }

        /// <summary>Split a large <see cref="CellGroup" /> into two or more smaller CellGroups</summary>
        /// <param name="stepsToSimulate">The maximum number of steps to attempt to simulate</param>
        /// <returns>The number of simulation steps remaining</returns>
        public int Split(int stepsToSimulate)
        {
            return stepsToSimulate;
        }

        /// <summary>Transfer fluids into any linked <see cref="CellGroup" /></summary>
        /// <param name="stepsToSimulate">The maximum number of steps to attempt to simulate</param>
        /// <returns>The number of simulation steps remaining</returns>
        public int Transfer(int stepsToSimulate)
        {
            return stepsToSimulate;
        }
    }
}