using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMemoir.Components
{
    public class PlayerStats
    {
        public Dictionary<string, bool> abilities,mapPeices,treesPurified;
        public int hp;
        public PlayerStats()
        {
            //wether the player has collected the abilities
            abilities = new Dictionary<string, bool>
            {
                ["Neutral"] = false,
                ["Up"] = false,
                ["Down"] = false,
                ["Side"] = false,
                ["Green"] = false,
                ["Red"] = false,
                ["Blue"] = false
            };

            # region what mapPeices the player has visited
            mapPeices = new Dictionary<string, bool> {
            ["a1"]  = false,
            ["a2"]  = false,
            ["a3"]  = false,
            ["a4"]  = false,
            ["a5"]  = false,
            ["a6"]  = false,
            ["a7"]  = false,
            ["a8"]  = false,
            ["a9"]  = false,
            ["a10"] = false,
            ["a11"] = false,
            ["a12"] = false,
            ["a13"] = false,
            ["a14"] = false,
            ["a15"] = false,
            ["a16"] = false,
            ["a17"] = false,
            ["a18"] = false,
            ["a19"] = false,
            ["a20"] = false,
            ["a21"] = false,
            ["a22"] = false,
            ["a23"] = false,
            ["a24"] = false,
            ["a25"] = false,
            ["a26"] = false,
            ["a27"] = false
            };
            #endregion
            //what trees have been purified
            treesPurified = new Dictionary<string, bool>
            {
                ["1"] = false,
                ["2"] = false,
                ["3"] = false,
                ["4"] = false,
                ["5"] = false
            };
            hp = 3;

        }

        public void Reset()
        {
            //wether the player has collected the abilities
            abilities = new Dictionary<string, bool>
            {
                ["Neutral"] = false,
                ["Up"] = false,
                ["Down"] = false,
                ["Side"] = false,
                ["Green"] = false,
                ["Red"] = false,
                ["Blue"] = false
            };

            # region what mapPeices the player has visited
            mapPeices = new Dictionary<string, bool>
            {
                ["a1"] = false,
                ["a2"] = false,
                ["a3"] = false,
                ["a4"] = false,
                ["a5"] = false,
                ["a6"] = false,
                ["a7"] = false,
                ["a8"] = false,
                ["a9"] = false,
                ["a10"] = false,
                ["a11"] = false,
                ["a12"] = false,
                ["a13"] = false,
                ["a14"] = false,
                ["a15"] = false,
                ["a16"] = false,
                ["a17"] = false,
                ["a18"] = false,
                ["a19"] = false,
                ["a20"] = false,
                ["a21"] = false,
                ["a22"] = false,
                ["a23"] = false,
                ["a24"] = false,
                ["a25"] = false,
                ["a26"] = false,
                ["a27"] = false
            };
            #endregion
            //what trees have been purified
            treesPurified = new Dictionary<string, bool>
            {
                ["1"] = false,
                ["2"] = false,
                ["3"] = false,
                ["4"] = false,
                ["5"] = false
            };
            hp = 3;
        }
    }
}
