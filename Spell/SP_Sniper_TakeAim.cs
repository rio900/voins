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
    class SP_Sniper_TakeAim : ISpell
    {
        IUnit _unit;
        public event EventHandler StartUseSpell;
        public event EventHandler CompletedUseSpell;
        SpellDescriptionInfo _spellDescriptionInfo = new SpellDescriptionInfo()
        {
            Description = "Take Aim, passive",
            LevelDescription =
            "Level1: +1 bonus range, + bullet speed" + Environment.NewLine +
            "Level2: +2 bonus range, + bullet speed" + Environment.NewLine +
            "Level3: +3 bonus range, + bullet speed"
        };
        public SpellDescriptionInfo SpellDescriptionInfo { get { return _spellDescriptionInfo; } set { _spellDescriptionInfo = value; } }

        bool _culdaunBool;
        public bool CuldaunBool { get { return _culdaunBool; } set { _culdaunBool = value; } }

        public string Name { get; set; }

        int _manaCost = 20;
        public int ManaCost { get { return _manaCost; } set { _manaCost = value; } }

        double _culdaun = 35;
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



        public SP_Sniper_TakeAim()
        {
            _imageTile = new UC_View_ImageTileControl("SP_Sniper_TakeAim", this);
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
                ///Данная способность является бафом для игрока, в этом случае дальность снайпера
                _unit.Range += 1;
                
                foreach (var item in unit.Spells)
                {
                    if (item.GetType() == typeof(SP_Sniper_Headshot))
                    {
                        SP_Sniper_Headshot headshot = (SP_Sniper_Headshot)item;
                        headshot.Speed -= 0.05;
                    }
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
