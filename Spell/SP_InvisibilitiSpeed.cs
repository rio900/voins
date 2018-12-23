using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Voins.AppCode;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Animation;

namespace Voins.Spell
{
    public class SP_InvisibilitiSpeed : ISpell
    {
      
        IUnit _unit;
        public event EventHandler StartUseSpell;
        public event EventHandler CompletedUseSpell;
        SpellDescriptionInfo _spellDescriptionInfo = new SpellDescriptionInfo()
        {
            Description = "Invisibiliti Speed, Mana cost - 10",
            LevelDescription =
            "Level1: Culdaun 10 sec. Duration 4 sec." + Environment.NewLine +
            "Level2: Culdaun 9 sec. Duration 5 sec." + Environment.NewLine +
            "Level3: Culdaun 8 sec. Duration 6 sec."
        };
        public SpellDescriptionInfo SpellDescriptionInfo { get { return _spellDescriptionInfo; } set { _spellDescriptionInfo = value; } }

        bool _culdaunBool;
        public bool CuldaunBool { get { return _culdaunBool; } set { _culdaunBool = value; } }

        public string Name { get; set; }

        int _manaCost = 10;
        public int ManaCost { get { return _manaCost; } set { _manaCost = value; } }

        double _culdaun = 5;
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

        public SP_InvisibilitiSpeed()
        {
            _imageTile = new UC_View_ImageTileControl("SP_InvisibilitiSpeed", this);
        }

        public Storyboard _firstTimer;
        public Storyboard _secondTimer;
        /// <summary>
        /// Заклинание прекратило использование
        /// </summary>
        bool _spellStopped;

        public void UseSpall(Map map, Game_Object_In_Call obj, IUnit unit, object property)
        {
            bool upSpell = UnitGenerator.UpPlayerSpell(unit, this);

            if (unit.UnitFrozen == false &&
                !_culdaunBool && LevelCast != 0 && !upSpell && 
                !unit.Silenced &&
                !unit.Hexed)
            {
                if (unit.Mana >= ManaCost)
                ///Проверка есть ли мана на каст
                {
                   

                    ///Флаг кулдауна
                    _culdaunBool = true;
                    _spellStopped = false;

                    ///Отнимаем нужное количество
                    unit.Mana -= ManaCost;
                    _unit = unit;

                    ///Проверка не собъет ли какойто каст нам инвиз
                    foreach (var item in unit.Spells)
                        if (item.GetType() != typeof(SP_InvisibilitiSpeed))
                            item.StartUseSpell += item_StartUseSpell;

                    unit.GameObject.View.Opacity = 0.25;
                    unit.Invisibility = true;

                    _speed = unit.Speed / 2.5;
                    unit.OrijSpeed -= _speed;

                    /// lvl 1 - 4 - 10
                    /// lvl 2 - 5 - 9
                    /// lvl 3 - 6 - 8
                    if (LevelCast == 1)
                    {
                        Duration = 4;
                        Culdaun = 10;
                    }
                    else if (LevelCast == 2)
                    {
                        Duration = 5;
                        Culdaun = 9;
                    }
                    else if (LevelCast == 3)
                    {
                        Duration = 6;
                        Culdaun = 8;
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
        private void StopSpall()
        {
            if (!_spellStopped)
            {
                _spellStopped = true;
                _unit.GameObject.View.Opacity = 1;
                _unit.Invisibility = false;
                _unit.OrijSpeed += _speed;
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
            StopSpall();
            _firstTimer.Completed -= mouveTimer_Tick;
            _firstTimer = null;
        }

        void item_StartUseSpell(object sender, EventArgs e)
        {
            if (_firstTimer != null)
            {
                _firstTimer.Stop();
                _firstTimer = null;
                StopSpall();
            }
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
