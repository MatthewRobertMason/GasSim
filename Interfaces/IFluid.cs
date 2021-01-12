namespace GasSim
{
    /// <summary>A fluid in a simulation space</summary>
    public interface IFluid
    {
        /// <summary>
        /// Gets the unique ID of the fluid used as key in collections. Should be provided from a
        /// global list for fluid type
        /// </summary>
        public string ID
        {
            get;
        }

        /// <summary>Gets what type of fluid it is</summary>
        public string Name
        {
            get;
        }

        /// <summary>Gets or sets how much pressure there is in kPa</summary>
        public float Pressure
        {
            get;
            set;
        }

        /// <summary>Gets or sets the temperature of the fluid in K</summary>
        public float Temperature
        {
            get;
            set;
        }
    }
}