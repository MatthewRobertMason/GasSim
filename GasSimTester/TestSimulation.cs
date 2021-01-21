namespace GasSimTester
{
    using System;

    using GasSim;
    using GasSimTester.Enums;
    using System.Collections.Generic;

    /// <summary>Class used for testing simulations</summary>
    public class TestSimulation
    {
        private static UniqueCellID cellID;

        /// <summary>Initialises a new instance of the <see cref="TestSimulation" /> class</summary>
        public TestSimulation()
        {
            cellID = new UniqueCellID();
        }

        /// <summary>Gets the current default unique ID</summary>
        public UniqueCellID CellID
        {
            get => cellID;
        }

        /// <summary>Simulation Test 1</summary>
        public void Test1()
        {
            int width = 10;
            int height = 10;

            ICell[,] cell = TestFunctions.CreateTwoDimensionalWorld(width, height, cellID);

            HashSet<ICell> cells = new HashSet<ICell>();
            foreach (Cell c in cell)
            {
                cells.Add(c);
            }

            Simulation simulation = new Simulation(cells);

            Fluid oxyFluidToAdd = new Fluid(FluidTypes.Oxygen.ToString(), FluidTypes.Oxygen.ToString(), 101.6f, 290.0f);
            Fluid hydFluidToAdd = new Fluid(FluidTypes.Hydrogen.ToString(), FluidTypes.Hydrogen.ToString(), 101.6f, 290.0f);

            CellGroup cellGroup = simulation.CreateCellGroup(true);
            cellGroup.Add(cell[0, 0]);
            cellGroup.AddFluid(oxyFluidToAdd);

            CellGroup cellGroup2 = simulation.CreateCellGroup(true);
            cellGroup2.Add(cell[9, 9]);
            cell[9, 9].AddFluid(hydFluidToAdd);

            simulation.MaxStepsToSimulate = 10;

            TestFunctions.Display2DimensionalWorld(width, height, cell);
            for (int simSteps = 0; simSteps < 3 && !simulation.Stable; simSteps++)
            {
                simulation.Simulate();
                TestFunctions.Display2DimensionalWorld(width, height, cell);
            }
        }
    }
}