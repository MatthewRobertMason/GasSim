namespace GasSim
{
    using System.Collections.Generic;

    /// <summary>A Cell in a simulation space</summary>
    public class Cell : ICell
    {
        private ICellGroup group;
        private long id;
        private IList<ICell> neighbours;

        /// <summary>Initialises a new instance of the <see cref="Cell" /> class</summary>
        public Cell()
        {
            this.id = -1;
            this.Group = null;
            this.Neighbours = new List<ICell>();
        }

        /// <summary>Initialises a new instance of the <see cref="Cell" /> class</summary>
        /// <param name="id">The unique ID of the Cell</param>
        public Cell(long id)
        {
            this.id = id;
            this.Group = null;
            this.Neighbours = new List<ICell>();
        }

        /// <summary>Initialises a new instance of the <see cref="Cell" /> class</summary>
        /// <param name="id">The unique ID of the Cell</param>
        /// <param name="neighbours">The IList of neighbouring <see cref="ICell" /></param>
        public Cell(long id, IList<ICell> neighbours)
        {
            this.ID = id;
            this.Group = null;
            this.Neighbours = neighbours;
        }

        /// <summary>Gets or sets the group that this cell belongs to (if any)</summary>
        public ICellGroup Group
        {
            get => this.group;
            set => this.group = value;
        }

        /// <summary>Gets the unique ID of this cell</summary>
        public long ID
        {
            get => this.id;
            private set => this.id = value;
        }

        /// <summary>Gets neighbours of this cell, or cells that can receive transfer</summary>
        public IList<ICell> Neighbours
        {
            get => this.neighbours;
            private set => this.neighbours = value;
        }

        /// <summary>Adds fluid to the current cell</summary>
        /// <param name="fluid">The fluid to add to the cell's group</param>
        /// <returns>true if the cell is in a group, false otherwise</returns>
        public bool AddFluid(IFluid fluid)
        {
            if (this.group != null)
            {
                this.group.AddFluid(fluid);
                return true;
            }

            return false;
        }
    }
}