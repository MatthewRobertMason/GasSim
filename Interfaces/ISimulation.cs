namespace GasSim
{
    using System.Collections.Generic;

    /// <summary>A simulation space</summary>
    public interface ISimulation
    {
        /// <summary>Gets or sets the set of <see cref="ICell" /> in a simulation space</summary>
        public ISet<ICell> Cells
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the collection of <see cref="ICellGroup" /> in a simulation space
        /// </summary>
        public IList<ICellGroup> Groups
        {
            get;
            set;
        }

        /// <summary>Gets or sets the maximum number of cells to attempt to simulate per tick</summary>
        public int MaxStepsToSimulate
        {
            get;
            set;
        }

        /// <summary>Expand the <see cref="ICellGroup" /> into fringe cells</summary>
        /// <param name="stepsToSimulate">The maximum number of steps to attempt to simulate</param>
        /// <returns>The number of simulation steps remaining</returns>
        public int Expand(int stepsToSimulate);

        /// <summary>Simulate the system</summary>
        /// <param name="stepsToSimulate">The maximum number of steps to attempt to simulate</param>
        public void Simulate(int stepsToSimulate);

        /// <summary>Split a large <see cref="ICellGroup" /> into two or more smaller CellGroups</summary>
        /// <param name="stepsToSimulate">The maximum number of steps to attempt to simulate</param>
        /// <returns>The number of simulation steps remaining</returns>
        public int Split(int stepsToSimulate);

        /// <summary>Transfer fluids into any linked <see cref="ICellGroup" /></summary>
        /// <param name="stepsToSimulate">The maximum number of steps to attempt to simulate</param>
        /// <returns>The number of simulation steps remaining</returns>
        public int Transfer(int stepsToSimulate);
    }
}