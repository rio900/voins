using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Voins.AppCode;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Animation;

namespace Voins.Spell
{
    class SP_Alchemist_ChemicalRage : ISpell
    {
        Player _unit = new Player() {   };
        public event EventHandler StartUseSpell;
        public event EventHandler CompletedUseSpell;
        SpellDescriptionInfo _spellDescriptionInfo = new SpellDescriptionInfo()
        {
            Description = "Chemical Rage, Culdaun 21 sec. Duration 11 sec. Mana cost - 20",
            LevelDescription =
            "Level1: Speed +15, Attack Speed +25, Health regeneration +30"+Environment.NewLine+
            "Mana regeneration +25"
        };
        public SpellDescriptionInfo SpellDescriptionInfo { get { return _spellDescriptionInfo; } set { _spellDescriptionInfo = value; } }
      
        bool _culdaunBool;
        public bool CuldaunBool { get { return _culdaunBool; } set { _culdaunBool = value; } }

        public string Name { get; set; }

        int _manaCost = 20;
        public int ManaCost { get { return _manaCost; } set { _manaCost = value; } }

        double _culdaun = 21;
        public double Culdaun { get { return _culdaun; } set { _culdaun = value; } }

        public int HelthCost { get; set; }

        double _speed = 0.3;
        public double Speed { get { return _speed; } set { _speed = value; } }

        double _duration = 11;
        public double Duration { get { return _duration; } set { _duration = value; } }

        int _levelCast = 0;
        public int LevelCast { get { return _levelCast; } set { _levelCast = value; } }
        int _maxLevelCast = 1;
        public int MaxLevelCast { get { return _maxLevelCast; } set { _maxLevelCast = value; } }

        bool _isUlt = true;
        public bool IsUlt { get { return _isUlt; } set { _isUlt = value; } }

        int _oldMaxHelth = 0;
        int _adddDamage = 0;
        UC_View_ImageTileControl _imageTile;
        public UC_View_ImageTileControl ImageTile { get { return _imageTile; } set { _imageTile = value; } }

        public Storyboard _firstTimer;
        public Storyboard _secondTimer;

        public SP_Alchemist_ChemicalRage()
        {
            _imageTile = new UC_View_ImageTileControl("SP_Alchemist_ChemicalRage", this);
        }

        double _bonusSpeed = 0.15;
        double _bonusAttackSpeed = 0.25;
        double _hpRegen = 30;
        int _manaRegen = 25;
    
        /// <summary>
        /// Ульт алхимика, усиливает героя
        /// </summary>
        public void UseSpall(Map map, Game_Object_In_Call obj, IUnit unit, object property)
        {
            bool upSpell = UnitGenerator.UpPlayerSpell(unit, this);
            _unit = unit as Player;
            if (unit.UnitFrozen == false &&
                !_culdaunBool && LevelCast != 0 && !upSpell && !unit.Silenced && _unit != null)
            {
                if (unit.Mana >= ManaCost)
                ///Проверка есть ли мана на каст
                {
                    ///Флаг кулдауна
                    _culdaunBool = true;

                    ///Отнимаем нужное количество
                    unit.Mana -= ManaCost;

                 
                   _unit.OrijSpeed -= _bonusSpeed;
                   

                    ///Реген здоровъя
                    _unit.OrijHealthRegeneration += _hpRegen;

                    _unit.OrijManaRegeneration += _manaRegen;
                    ///Бонусная скорость атаки
                    _unit.OrijAttackSpeed -= _bonusAttackSpeed;


                    _firstTimer = new Storyboard() { Duration = TimeSpan.FromSeconds(Duration) };
                    _firstTimer.Completed += mouveTimer_Tick;
                    _firstTimer.Begin();

                    _secondTimer = new Storyboard() { Duration = TimeSpan.FromSeconds(Culdaun) };
                    _secondTimer.Completed +=  mouveTimerCuldaun_Tick;
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

        private void mouveTimer_Tick(object sender, object e)
        {
            _firstTimer.Completed -= mouveTimer_Tick;
            _firstTimer = null;

         
           _unit.OrijSpeed += _bonusSpeed;
          

            ///Реген здоровъя
            _unit.OrijHealthRegeneration -= _hpRegen;

            _unit.OrijManaRegeneration -= _manaRegen;
            ///Бонусная скорость атаки
            _unit.OrijAttackSpeed += _bonusAttackSpeed;

            UnitGenerator.UpdatePlayerView(_unit);
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

            if (PausedEvent != null)
                PausedEvent(this, null);

            if (_firstTimer != null)
                _firstTimer.Pause();
            if (_secondTimer != null)
                _secondTimer.Pause();

        }
        public void StopPause()
        {
            Paused = false;

            if (PausedEvent != null)
                PausedEvent(this, null);

            if (_firstTimer != null)
                _firstTimer.Resume();
            if (_secondTimer != null)
                _secondTimer.Resume();
        }
        #endregion
    }
}
