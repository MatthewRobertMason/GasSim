namespace GasSimTester
{
    using GasSim;
    using GasSimTester.Enums;
    using System;

    /// <summary>A class to hold useful methods for tests</summary>
    internal static class TestFunctions
    {
        /// <summary>Creates a 2 dimensional array of connected cells</summary>
        /// <param name="width">The width of the world</param>
        /// <param name="height">The height of the world</param>
        /// <param name="cellID">The current cellId reference to use</param>
        /// <returns>A connected 2D array of ICells</returns>
        public static ICell[,] CreateTwoDimensionalWorld(int width, int height, UniqueCellID cellID)
        {
            ICell[,] cell = new ICell[width, height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    cell[x, y] = new Cell(cellID.CellID);
                    if (x > 0)
                    {
                        cell[x, y].Neighbours.Add(cell[x - 1, y]);
                        cell[x - 1, y].Neighbours.Add(cell[x, y]);
                    }

                    if (y > 0)
                    {
                        cell[x, y].Neighbours.Add(cell[x, y - 1]);
                        cell[x, y - 1].Neighbours.Add(cell[x, y]);
                    }
                }
            }

            return cell;
        }

        /// <summary>Draws a 2D world to the console</summary>
        /// <param name="width">The width of the world</param>
        /// <param name="height">The height of the world</param>
        /// <param name="cells">the cells of the world</param>
        public static void Display2DimensionalWorld(int width, int height, ICell[,] cells)
        {
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (cells[x, y].Group == null)
                    {
                        Console.Write("..");
                    }
                    else
                    {
                        if (cells[x, y].Group.Fluids.ContainsKey(FluidTypes.Oxygen.ToString()) && cells[x, y].Group.Fluids.ContainsKey(FluidTypes.Hydrogen.ToString()))
                        {
                            Console.Write("OH");
                        }
                        else if (cells[x, y].Group.Fluids.ContainsKey(FluidTypes.Oxygen.ToString()))
                        {
                            Console.Write("O_");
                        }
                        else if (cells[x, y].Group.Fluids.ContainsKey(FluidTypes.Hydrogen.ToString()))
                        {
                            Console.Write("_H");
                        }
                        else
                        {
                            Console.Write("..");
                        }
                    }
                }

                Console.WriteLine();
            }

            Console.WriteLine();
        }
    }
}