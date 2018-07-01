using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Voins.AppCode;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;

namespace Voins.Spell
{
    public class SP_AttackSpeed : ISpell
    {
        public event EventHandler StartUseSpell;
        public event EventHandler CompletedUseSpell;

        double _oldCuldaun;
        IUnit _unit;
        bool _culdaunBool;
        public bool CuldaunBool { get { return _culdaunBool; } set { _culdaunBool = value; } }
        SpellDescriptionInfo _spellDescriptionInfo = new SpellDescriptionInfo()
        {
            Description = "Bonus Attack Speed, +50% attack speed, Mana cost - 25",
            LevelDescription =
            "Level1: Culdaun 15 sec. Duration 3 sec." + Environment.NewLine +
            "Level2: Culdaun 15 sec. Duration 5 sec." + Environment.NewLine +
            "Level2: Culdaun 14 sec. Duration 6 sec."
        };

        public SpellDescriptionInfo SpellDescriptionInfo { get { return _spellDescriptionInfo; } set { _spellDescriptionInfo = value; } }

        public string Name { get; set; }

        int _manaCost = 25;
        public int ManaCost { get { return _manaCost; } set { _manaCost = value; } }

        double _culdaun = 10;
        public double Culdaun { get { return _culdaun; } set { _culdaun = value; } }

        public int HelthCost { get; set; }

        double _speed = 0.3;

        public double Speed { get { return _speed; } set { _speed = value; } }

        double _duration = 3;

        public double Duration { get { return _duration; } set { _duration = value; } }

        int _levelCast = 0;
        public int LevelCast { get { return _levelCast; } set { _levelCast = value; } }
        int _maxLevelCast = 3;
        public int MaxLevelCast { get { return _maxLevelCast; } set { _maxLevelCast = value; } }

        bool _isUlt = false;
        public bool IsUlt { get { return _isUlt; } set { _isUlt = value; } }

        UC_View_ImageTileControl _imageTile;
        public UC_View_ImageTileControl ImageTile { get { return _imageTile; } set { _imageTile = value; } }

        public Storyboard _firstTimer;
        public Storyboard _secondTimer;

        public SP_AttackSpeed()
        {
            _imageTile = new UC_View_ImageTileControl("SP_AttackSpeed", this);
        }

        public void UseSpall(Map map, Game_Object_In_Call obj, IUnit unit, object property)
        {
            bool upSpell = UnitGenerator.UpPlayerSpell(unit, this);

            if (unit.UnitFrozen == false &&
                !_culdaunBool && LevelCast != 0 && !upSpell && !unit.Silenced)
            {
                if (unit.Mana >= ManaCost)
                ///Проверка есть ли мана на каст
                {


                    ///Флаг кулдауна
                    _culdaunBool = true;

                    ///Отнимаем нужное количество
                    unit.Mana -= ManaCost;
                    _unit = unit;

                    foreach (var item in unit.Spells)
                    {
                        if (item.GetType() == typeof(SP_FireArrow))
                        {
                            SP_FireArrow arrowSpell = (SP_FireArrow)item;
                            _oldCuldaun = arrowSpell.Culdaun;
                            arrowSpell.Culdaun = arrowSpell.Culdaun / 3;
                        }
                    }

                    _unit.AttackSpeed = _unit.OrijAttackSpeed / 2;


                    /// lvl 1 - 3 - 15
                    /// lvl 2 - 5 - 15
                    /// lvl 3 - 6 - 14
                    if (LevelCast == 1)
                    {
                        Duration = 3;
                        Culdaun = 15;
                    }
                    else if (LevelCast == 2)
                    {
                        Duration = 5;
                        Culdaun = 15;
                    }
                    else if (LevelCast == 3)
                    {
                        Duration = 6;
                        Culdaun = 14;
                    }

                    _firstTimer = new Storyboard() { Duration = TimeSpan.FromSeconds(Duration) };
                    _firstTimer.Completed += mouveTimer_Tick;
                    _firstTimer.Begin();

                    _secondTimer = new Storyboard() { Duration = TimeSpan.FromSeconds(Culdaun) };
                    _secondTimer.Completed += mouveTimerCuldaun_Tick;
                    _secondTimer.Begin();

                    if (Paused)
                        Pause();

                    if (StartUseSpell != null)
                        StartUseSpell(this, null);
                    UnitGenerator.UpdatePlayerView(unit);
                }
            }
        }

        void mouveTimerCuldaun_Tick(object sender, object e)
        {
            _culdaunBool = false;
            _secondTimer.Completed -= mouveTimerCuldaun_Tick;
            _secondTimer = null;
        }

        void mouveTimer_Tick(object sender, object e)
        {
            _firstTimer.Completed -= mouveTimer_Tick;
            _firstTimer = null;
            foreach (var item in _unit.Spells)
            {
                if (item.GetType() == typeof(SP_FireArrow))
                {
                    SP_FireArrow arrowSpell = (SP_FireArrow)item;
                    arrowSpell.Culdaun = _oldCuldaun;
                }
            }
            _unit.AttackSpeed = _unit.OrijAttackSpeed;
        }
        /// <summary>
        /// Изучить способность хоть один раз
        /// </summary>
        public void UpSpell(Player player)
        {
            player.PreviousSkill = Name;
            LevelCast += 1;
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

            if (_firstTimer != null)
                _firstTimer.Pause();
            if (_secondTimer != null)
                _secondTimer.Pause();

            if (PausedEvent != null)
                PausedEvent(this, null);
        }
        public void StopPause()
        {
            Paused = false;

            if (_firstTimer != null)
                _firstTimer.Resume();
            if (_secondTimer != null)
                _secondTimer.Resume();

            if (PausedEvent != null)
                PausedEvent(this, null);
        }
        #endregion
    }
}
