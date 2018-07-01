using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Voins.AppCode
{
   public static class StaticVaribl
    {
        
        public static int HpRegenerationConstant = 4;
        public static int ManaRegenerationConstant = 4;

        public static int StrConstant = 5;
        public static int IntConstant = 5;
        public static int AgilConstant = 5;

        public static int StrRegenConstant = 5;
        public static int IntRegenConstant = 5;

        public static int PlayerGoldMinus = 5;
        public static int PlayerGoldPlus = 15;
        /// <summary>
        /// Время неуязвимости после спаума
        /// </summary>
        public static int InvulnerabilityTime = 5;
       /// <summary>
       /// На какое число делится основная характеристика
       /// </summary>
        public static int DemageD = 2;

        public static double AgilityAttackSpeed = 0.008;
        
        /// <summary>
        /// Константа, максимальная скорость атаки
        /// </summary>
        public static double AttackSpeedMaximum = 0.20;
      
        /// <summary>
        /// Константа, максимальная скорость атаки для милишных атак
        /// </summary>
        public static double AttackSpeedMeleMaximum = 0.30;

        public static double SpeedMaximum = 0.3;

        public static int DemageAndArmor(int phisic, int arrmor)
        {
            //(0,06 × Броня) ÷ (1 + 0,06 × Броня)
            double demage = phisic;
            ///Знак брони
            double polar = 1;

            int arrm = arrmor;

            if (arrm < 0)
                polar = -1;
            ///Броня всегда положительная
            arrm = Math.Abs(arrm);

            if (arrm > 0)
                demage = phisic - polar * phisic * (0.06 * (double)arrm) / (1 + 0.06 * (double)arrm);

            phisic = (int)demage;

            return phisic;
        }
        public static MainPage MainPage;
    }
}
