using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biscuits.Flight
{
    public abstract class Aircraft : IAircraft
    {
        public abstract void SetControl(double thrust, double pitch, double roll, double yaw);
    }
}
