using EventArgsLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace Positioning2WheelsNS
{
    public class Positioning2Wheels
    {
        int robotId;
        Location robotLocation = new Location(0,0,0, 0, 0, 0);
        public Positioning2Wheels(int id)
        {
            robotId = id;
        }

        public void OnOdometryRobotSpeedReceived(object sender, PolarSpeedArgs e)
        {
            /// Ajoutez votre code de calcul de la nouvelle position ici
            robotLocation.X = robotLocation.X + e.Vx*1/50;
            robotLocation.Y = robotLocation.Y + e.Vy *1/50;
            robotLocation.Theta = robotLocation.Theta + e.Vtheta * 1 / 50;

            /// Ajoutez l'appel à l'event de transmission de la position calculée ici
            OnCalculatedLocation(robotId, robotLocation);
        }

        //Output events
        public event EventHandler<LocationArgs> OnCalculatedLocationEvent;
        public virtual void OnCalculatedLocation(int id, Location locationRefTerrain)
        {
            var handler = OnCalculatedLocationEvent;
            if (handler != null)
            {
                handler(this, new LocationArgs { RobotId = id, Location = locationRefTerrain });
            }
        }
    }
}
