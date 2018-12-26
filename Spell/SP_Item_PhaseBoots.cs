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
    public class SP_Item_PhaseBoots : ISpell
    {
      
        IUnit _unit;
        public event EventHandler StartUseSpell;
        public event EventHandler CompletedUseSpell;

        bool _culdaunBool;
        public bool CuldaunBool { get { return _culdaunBool; } set { _culdaunBool = value; } }
        SpellDescriptionInfo _spellDescriptionInfo = new SpellDescriptionInfo();
        public SpellDescriptionInfo SpellDescriptionInfo { get { return _spellDescriptionInfo; } set { _spellDescriptionInfo = value; } }

        public string Name { get; set; }

        int _manaCost = 0;
        public int ManaCost { get { return _manaCost; } set { _manaCost = value; } }

        double _culdaun = 7;
        public double Culdaun { get { return _culdaun; } set { _culdaun = value; } }

        public int HelthCost { get; set; }

        double _speed = 0.1;
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
        /// <summary>
        /// Бонус скорости не использовался
        /// </summary>
        bool _bonusSpeedNoUseg = false;
        public SP_Item_PhaseBoots()
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
                    _unit = unit;

            if (unit.UnitFrozen == false &&
                !_culdaunBool && !_unit.Hexed)
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



                    unit.OrijSpeed -= _speed;
              

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
