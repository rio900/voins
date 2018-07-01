using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Voins.AppCode;
using Windows.Foundation;
using Windows.UI.Xaml;

namespace Voins.Spell
{
    class SP_Alchemist_GreevilsGreed : ISpell
    {
        IUnit _unit;
        public event EventHandler StartUseSpell;
        public event EventHandler CompletedUseSpell;

        bool _culdaunBool;
        public bool CuldaunBool { get { return _culdaunBool; } set { _culdaunBool = value; } }
        SpellDescriptionInfo _spellDescriptionInfo = new SpellDescriptionInfo()
        {
            Description = "Greevils Greed, passive skill.",
            LevelDescription =
            "Level1: Bonus gold for kill +2 gold" + Environment.NewLine +
            "Level2: Bonus gold for kill +4 gold" + Environment.NewLine +
            "Level3: Bonus gold for kill +6 gold"
        };
        public SpellDescriptionInfo SpellDescriptionInfo { get { return _spellDescriptionInfo; } set { _spellDescriptionInfo = value; } }

        public string Name { get; set; }

        int _manaCost = 20;
        public int ManaCost { get { return _manaCost; } set { _manaCost = value; } }

        double _culdaun = 100;
        public double Culdaun { get { return _culdaun; } set { _culdaun = value; } }

        public int HelthCost { get; set; }

        double _speed = 1;
        public double Speed { get { return _speed; } set { _speed = value; } }

        double _duration = 30;
        public double Duration { get { return _duration; } set { _duration = value; } }

        int _levelCast = 0;
        public int LevelCast { get { return _levelCast; } set { _levelCast = value; } }
        int _maxLevelCast = 3;
        public int MaxLevelCast { get { return _maxLevelCast; } set { _maxLevelCast = value; } }

        bool _isUlt = false;
        public bool IsUlt { get { return _isUlt; } set { _isUlt = value; } }

      
        UC_View_ImageTileControl _imageTile;
        public UC_View_ImageTileControl ImageTile { get { return _imageTile; } set { _imageTile = value; } }

      

        public SP_Alchemist_GreevilsGreed()
        {
            _imageTile = new UC_View_ImageTileControl("SP_Alchemist_GreevilsGreed", this);
        }

        /// <summary>
        /// Счастливая монетка, пасивная способновть
        /// </summary>
        public void UseSpall(Map map, Game_Object_In_Call obj, IUnit unit, object property)
        {
            bool upSpell = UnitGenerator.UpPlayerSpell(unit, this);
            _unit = unit;

            if (upSpell && !CuldaunBool)
            {
                CuldaunBool = true;
                ///Данная способность является бафом для игрока, в этом случае алхимика

                Buff buff = unit.Buffs.FirstOrDefault(p => p.Name == "GreevilsGreed");
                if (buff != null)
                {
                    buff.GoldBonus = LevelCast*2;
                }
                else
                {
                    Buff alchBuff = new Buff()
                    {
                        Passive = true,
                        GoldBonus = LevelCast * 2,
                        Duration = LevelCast,
                        Name = "GreevilsGreed"
                    };
                    unit.Buffs.Add(alchBuff);
                }

                UnitGenerator.UpdatePlayerView(unit);
            }
        }

        /// <summary>
        /// Изучить способность хоть один раз
        /// </summary>
        public void UpSpell(Player player)
        {
            player.PreviousSkill = Name;
            LevelCast += 1;
            CuldaunBool = false;
            UseSpall(null, null, player, null);
            ImageTile.UpSpell();
            player.SpellUpedEventCall();
        }

        #region Pause
        public event EventHandler PausedEvent;
        bool _paused;
        public bool Paused { get { return _paused; } set { _paused = value; } }

        public void Pause()
        {
            Paused = true;


            if (PausedEvent != null)
                PausedEvent(this, null);
        }
        public void StopPause()
        {
            Paused = false;

         

            if (PausedEvent != null)
                PausedEvent(this, null);
        }
        #endregion
    }
}
