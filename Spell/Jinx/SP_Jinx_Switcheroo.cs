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
    public class SP_Jinx_Switcheroo : ISpell
    {
        public event EventHandler StartUseSpell;
        public event EventHandler CompletedUseSpell;

        bool _culdaunBool;
        public bool CuldaunBool { get { return _culdaunBool; } set { _culdaunBool = value; } }

        public string Name { get; set; }
        SpellDescriptionInfo _spellDescriptionInfo = new SpellDescriptionInfo()
        {
            Description = "Switcheroo",
            LevelDescription =
            "" + Environment.NewLine 
        };
        public SpellDescriptionInfo SpellDescriptionInfo { get { return _spellDescriptionInfo; } set { _spellDescriptionInfo = value; } }

        int _manaCost = 5;
        public int ManaCost { get { return _manaCost; } set { _manaCost = value; } }

        double _culdaun =0;
        public double Culdaun { get { return _culdaun; } set { _culdaun = value; } }

        public int HelthCost { get; set; }

        double _speed = 0.3;
        public double Speed { get { return _speed; } set { _speed = value; } }

        double _duration = 0;

        public double Duration { get { return _duration; } set { _duration = value; } }

        bool _isUlt = false;
        public bool IsUlt { get { return _isUlt; } set { _isUlt = value; } }

        int _levelCast = 0;
        public int LevelCast { get { return _levelCast; } set { _levelCast = value; } }
        int _maxLevelCast = 3;
        public int MaxLevelCast { get { return _maxLevelCast; } set { _maxLevelCast = value; } }

        bool _attackType = true;
        /// <summary>
        /// Ракетница или шокер
        /// </summary>
        public bool AttackType { get { return _attackType; } set { _attackType = value; } }

        UC_View_ImageTileControl _imageTile;
        public UC_View_ImageTileControl ImageTile { get { return _imageTile; } set { _imageTile = value; } }

     
        public Storyboard _firstTimer;
        public Storyboard _secondTimer;

        IUnit _unit;
        public SP_Jinx_Switcheroo()
        {
            _imageTile = new UC_View_ImageTileControl("SP_Jinx_Switcheroo", this);
        }

        /// <summary>
        /// Запуск стрелы
        /// </summary>
        /// <param name="property">Уровень прокача способности, умножется на 5 и получается магический урон стрелы</param>
        public void UseSpall(Map map, Game_Object_In_Call obj, IUnit unit, object property)
        {
            _unit = unit;
            bool upSpell = UnitGenerator.UpPlayerSpell(unit, this);

            if (unit.UnitFrozen == false &&
                LevelCast != 0 &&
             //   !upSpell &&
                !unit.Silenced &&
                !Paused)
            {

                SP_Jinx_Zap activeSpell = unit.Spells.FirstOrDefault(p => p.GetType() == typeof(SP_Jinx_Zap)) as SP_Jinx_Zap;

                if (activeSpell != null)
                {
                    if (!AttackType)
                    {
                        _imageTile.ChengImage("SP_Jinx_Switcheroo");
                        activeSpell.AttackType = false;

                        if (LevelCast == 1)
                        {
                            activeSpell.Culdaun = 1;
                            activeSpell.BonusDemage = 3;
                        }
                        else if (LevelCast == 2)
                        {
                            activeSpell.Culdaun = 0.75;
                            activeSpell.BonusDemage = 6;

                        }
                        else if (LevelCast == 3)
                        {
                            activeSpell.Culdaun = 0.5;
                            activeSpell.BonusDemage = 9;

                        }
                        AttackType = true;
                    }
                    else
                    {
                        _imageTile.ChengImage("SP_Jinx_Switcheroo_Reload");
                        activeSpell.AttackType = true;
                        activeSpell.Culdaun = 1.5;

                        if (LevelCast == 1)
                        {
                            activeSpell.Splash = 0.5;
                            activeSpell.BonusRange = 1;
                            activeSpell.BonusDemage = 5;
                        }
                        else if (LevelCast == 2)
                        {
                            activeSpell.Splash = 0.75;
                            activeSpell.BonusRange = 2;
                            activeSpell.BonusDemage = 10;

                        }
                        else if (LevelCast == 3)
                        {
                            activeSpell.Splash = 1;
                            activeSpell.BonusRange = 3;
                            activeSpell.BonusDemage = 15;
                        }

                        AttackType = false;
                    }

                }

                if (Paused)
                {
                    Pause();
                }

                if (StartUseSpell != null)
                    StartUseSpell(this, null);

                UnitGenerator.UpdatePlayerView(unit);

            }
        }



        void _secondTimer_Completed(object sender, object e)
        {
            _culdaunBool = false;
            _secondTimer.Completed -= _secondTimer_Completed;
            _secondTimer = null;
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
