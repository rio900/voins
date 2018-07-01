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
    public class SP_AttackEasyMob : ISpell
    {
        public event EventHandler StartUseSpell;
        public event EventHandler CompletedUseSpell;
        SpellDescriptionInfo _spellDescriptionInfo = new SpellDescriptionInfo();
        public SpellDescriptionInfo SpellDescriptionInfo { get { return _spellDescriptionInfo; } set { _spellDescriptionInfo = value; } }

        double _oldCuldaun;
        IUnit _unit;

        Map_Cell _currentCall;
        public Map_Cell CurrentCall { get { return _currentCall; } set { _currentCall = value; } }

        bool _culdaunBool;
        public bool CuldaunBool { get { return _culdaunBool; } set { _culdaunBool = value; } }

        public string Name { get; set; }

        int _manaCost = 15;
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

        public SP_AttackEasyMob()
        {
            _imageTile = new UC_View_ImageTileControl("SP_AttackSpeed", this);
        }

        /// <summary>
        /// Обычная атака моба
        /// </summary>
        /// <param name="property">Колонка куда наносится удар</param>
        public void UseSpall(Map map, Game_Object_In_Call obj, IUnit unit, object property)
        {
            if (unit.UnitFrozen == false &&
                !_culdaunBool)
            {
                ///Флаг кулдауна
                _culdaunBool = true;
                _unit = unit;

                CurrentCall = property as Map_Cell;
                if (CurrentCall != null)
                {
                    Bullet bullet = new Bullet()
                    {
                        UnitUsed = unit,
                        DemagePhys = unit.Demage
                    };
                    UnitGenerator.AddDamage(CurrentCall, bullet);

                    _firstTimer = new Storyboard() { Duration = TimeSpan.FromSeconds(unit.AttackSpeed) };
                    _firstTimer.Completed += _firstTimer_Completed;
                    _firstTimer.Begin();

                    if (Paused)
                        Pause();
                }
            }
            else if (CompletedUseSpell != null)
                CompletedUseSpell(this, null);

        }

        void _firstTimer_Completed(object sender, object e)
        {
            _firstTimer.Completed -= _firstTimer_Completed;
            _firstTimer = null;

            _culdaunBool = false;
            if (CompletedUseSpell != null)
                CompletedUseSpell(this, null);
        }


        /// <summary>
        /// Изучить способность хоть один раз
        /// </summary>
        public void UpSpell(Player player)
        {
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
