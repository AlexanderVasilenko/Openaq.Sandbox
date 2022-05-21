using System.Collections.Generic;

namespace Openaq.Sandbox.MVP.Domain.Services.Openaq
{
    public class LocationObject
    {
        /// <summary>
        /// Location Country
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// Location City
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Location Name
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// Measurements
        /// </summary>
        public List<Measurement> Measurements { get; set; }

        /// <summary>
        /// Location Coordinates
        /// </summary>
        public Coordinate Coordinates { get; set; }
    }
}
