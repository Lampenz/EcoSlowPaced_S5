using Eco.Gameplay.Objects;
using Eco.Gameplay.Occupancy;
using Eco.Shared.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoPulse.StatueMod
{
    public class LionStatue
    {
        public static List<BlockOccupancy> GetOccupency()
        {
            return new List<BlockOccupancy>()
            {
                 new BlockOccupancy(new Vector3i(0, 0, 0), typeof(BuildingWorldObjectBlock)), 
                new BlockOccupancy(new Vector3i(1, 0, 0), typeof(BuildingWorldObjectBlock)), 
                new BlockOccupancy(new Vector3i(0, 0, 1), typeof(BuildingWorldObjectBlock)), 
                new BlockOccupancy(new Vector3i(1, 0, 1), typeof(BuildingWorldObjectBlock)), 

                new BlockOccupancy(new Vector3i(0, 1, 0), typeof(BuildingWorldObjectBlock)), 
                new BlockOccupancy(new Vector3i(1, 1, 0), typeof(BuildingWorldObjectBlock)), 
                new BlockOccupancy(new Vector3i(0, 1, 1), typeof(BuildingWorldObjectBlock)), 
                new BlockOccupancy(new Vector3i(1, 1, 1), typeof(BuildingWorldObjectBlock)), 

                new BlockOccupancy(new Vector3i(0, 2, 0), typeof(BuildingWorldObjectBlock)), 
                new BlockOccupancy(new Vector3i(1, 2, 0), typeof(BuildingWorldObjectBlock)), 
                new BlockOccupancy(new Vector3i(0, 2, 1), typeof(BuildingWorldObjectBlock)), 
                new BlockOccupancy(new Vector3i(1, 2, 1), typeof(BuildingWorldObjectBlock))  
   
            };
        }
    }
}
