using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Voins.AppCode
{
   public class SpellDescriptionInfo
    {
        private string _description;
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        private string _levelDescription;
        public string LevelDescription
        {
            get { return _levelDescription; }
            set { _levelDescription = value; }
        }

        private bool _minCd;
        /// <summary>
        /// Маленький кулдаун
        /// </summary>
        public bool MinCd
        {
            get { return _minCd; }
            set { _minCd = value; }
        }
    }
}
