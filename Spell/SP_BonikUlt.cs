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
    class SP_BonikUlt : ISpell
    {
        IUnit _unit;
        public event EventHandler StartUseSpell;
        public event EventHandler CompletedUseSpell;
        SpellDescriptionInfo _spellDescriptionInfo = new SpellDescriptionInfo()
        {
            Description = "Bonik Ult, Culdaun 35 sec. Duration 30 sec. Mana cost - 20",
            LevelDescription =
            "Level1: Bonus health +100%, Bonus demage +30%, "
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

        double _speed = 0.3;
        public double Speed { get { return _speed; } set { _speed = value; } }

        double _duration = 30;
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

        public SP_BonikUlt()
        {
            _imageTile = new UC_View_ImageTileControl("SP_BonikUlt",this);
        }

        /// <summary>
        /// Бони к съедает крипа и получает бонус к максимальному здоровю и 
        /// урону
        /// </summary>
        /// <param name="property">Множетель прироста здоровъя и урона масив double 2 элемента
        /// 0 - множетель на здорове, 1 - на урон</param>
        public void UseSpall(Map map, Game_Object_In_Call obj, IUnit unit, object property)
        {
            bool upSpell = UnitGenerator.UpPlayerSpell(unit, this);

            if (unit.UnitFrozen == false &&
                !_culdaunBool && LevelCast != 0 && !upSpell && !unit.Silenced &&
                !unit.Hexed)
            {
                if (unit.Mana >= ManaCost)
                ///Проверка есть ли мана на каст
                {
                    ///Получаем клеточку перед боником
                    Point newPos = UnitGenerator.NewX_NewY(unit);

                    if (map.Calls.Any(p => p.IndexLeft == newPos.X && p.IndexTop == newPos.Y))
                    {
                        var call = map.Calls.Single(p => p.IndexLeft == newPos.X && p.IndexTop == newPos.Y);
                        ///Теперь проверим содержит ли колонка юнит который можно съесть
                        if (call.IUnits.Any(p => p.GameObject.EnumCallType == EnumCallType.UnitBlock ||
                            p.GameObject.EnumCallType == EnumCallType.Unit))
                        {
                            ///Флаг кулдауна
                            _culdaunBool = true;

                            ///Отнимаем нужное количество
                            unit.Mana -= ManaCost;
                            _unit = unit;

                            ///Берем первого попавшегося юнита
                            IUnit unitCrush = call.IUnits.FirstOrDefault(p => p.GameObject.EnumCallType == EnumCallType.UnitBlock ||
                            p.GameObject.EnumCallType == EnumCallType.Unit);
                            ///Юнит который подвергся спелу унечтожен
                            unitCrush.RemoveUnit(unit);
                          
                            double[] prop = new double[] { 1, 0.3 };

                            _oldMaxHelth = unit.OrijHealth;
                            int healBonus = (int)(unitCrush.Health * prop[0]);
                            if (healBonus > 150)
                                healBonus = 150;

                            unit.OrijHealth = unit.OrijHealth + healBonus;
                            unit.Health = unit.Health + healBonus;

                            _adddDamage = (int)(unitCrush.Health * prop[1]);

                            if (_adddDamage > 50)
                                _adddDamage = 50;

                            unit.DemageItem = unit.DemageItem + _adddDamage;

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
                        }
                    }
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
            _unit.DemageItem -= _adddDamage;
            _unit.OrijHealth = _oldMaxHelth;

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
