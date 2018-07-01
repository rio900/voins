using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Voins.AppCode
{
   public class Buff
    {
        private double _attackSpeedSlow;
        public double AttackSpeedSlow
        {
            get { return _attackSpeedSlow; }
            set { _attackSpeedSlow = value; }
        }

        private double _speedSlow;
        public double SpeedSlow
        {
            get { return _speedSlow; }
            set { _speedSlow = value; }
        }

       private bool _passive;
       public bool Passive
        {
            get { return _passive; }
            set { _passive = value; }
        }
       private int _stunDuratio = 0;
       /// <summary>
       /// Длительность стана как бафа который кидают
       /// а не действует
       /// </summary>
       public int StunDuratio
       {
           get { return _stunDuratio; }
           set { _stunDuratio = value; }
       }


        private int _goldBonus;
        public int GoldBonus
        {
            get { return _goldBonus; }
            set { _goldBonus = value; }
        }

        private bool _buffUsed;
        public bool BuffUsed
        {
            get { return _buffUsed; }
            set { _buffUsed = value; }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private int _minusArmor;
        public int MinusArmor
        {
            get { return _minusArmor; }
            set { _minusArmor = value; }
        }
        private bool _stun;
        public bool Stun
        {
            get { return _stun; }
            set { _stun = value; }
        }
        private double _duration = 1;
        /// <summary>
        /// Длительность воздействия
        /// </summary>
        public double Duration
        {
            get { return _duration; }
            set { _duration = value; }
        }

        public int DemageMagic { get; set; }

        public IUnit DemagedUnit { get; set; }

        public int Armor { get; set; }

        public double AttackSpeed { get; set; }
    }
}
