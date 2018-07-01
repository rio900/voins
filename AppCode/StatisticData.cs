using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Voins.AppCode
{
   public class StatisticData
    {
        public int Kills { get; set; }
        public int Death { get; set; }

        public int Demage { get; set; }
        /// <summary>
        /// Сколько получено урона
        /// </summary>
        public int DemageSelfs { get; set; }
        public int BlockKills { get; set; }
        public int MobKills { get; set; }

        public int AllGold { get; set; }

    }
}
