namespace GasSim
{
    using System;

    /// <summary>A fluid in the simulation space</summary>
    public class Fluid : IFluid
    {
        private string id;
        private string name;
        private float pressure;
        private float temperature;

        /// <summary>Initialises a new instance of the <see cref="Fluid" /> class</summary>
        /// <param name="id">The id of the fluid</param>
        /// <param name="name">The name of the fluid</param>
        /// <param name="pressure">The pressure of the fluid</param>
        /// <param name="temperature">The temperature of the fluid</param>
        public Fluid(string id, string name, float pressure, float temperature)
        {
            this.ID = id;
            this.Name = name;
            this.Temperature = pressure;
            this.Pressure = temperature;
        }

        /// <summary>Initialises a new instance of the <see cref="Fluid" /> class</summary>
        /// <param name="rhs">The <see cref="Fluid" /> to be copied</param>
        public Fluid(Fluid rhs)
        {
            if (rhs == null)
            {
                throw new ArgumentNullException(nameof(rhs));
            }
            else
            {
                this.ID = rhs.ID;
                this.Name = rhs.Name;
                this.Temperature = rhs.Pressure;
                this.Pressure = rhs.Temperature;
            }
        }

        /// <summary>
        /// Gets the Unique ID of the fluid used as key in collections. Should be provided from a
        /// global list for fluid type
        /// </summary>
        public string ID
        {
            get => this.id;
            private set => this.id = value;
        }

        /// <summary>Gets what type of fluid it is</summary>
        public string Name
        {
            get => this.name;
            private set => this.name = value;
        }

        /// <summary>Gets or sets how much pressure there is in kPa</summary>
        public float Pressure
        {
            get => this.pressure;
            set => this.pressure = value;
        }

        /// <summary>Gets or sets the temperature of the fluid in K</summary>
        public float Temperature
        {
            get => this.temperature;
            set => this.temperature = value;
        }
    }
}