﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Voins.AppCode;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Animation;

namespace Voins.Spell
{
    public class SP_Nature_Trent : ISpell
    {
        double _orchidBonus = 0.5;
        public event EventHandler StartUseSpell;
        public event EventHandler CompletedUseSpell;
        SpellDescriptionInfo _spellDescriptionInfo = new SpellDescriptionInfo();
        public SpellDescriptionInfo SpellDescriptionInfo { get { return _spellDescriptionInfo; } set { _spellDescriptionInfo = value; } }

        IUnit _unit;
        bool _culdaunBool;
        public bool CuldaunBool { get { return _culdaunBool; } set { _culdaunBool = value; } }

        public string Name { get; set; }

        int _manaCost = 30;
        public int ManaCost { get { return _manaCost; } set { _manaCost = value; } }

        double _culdaun = 20;
        public double Culdaun { get { return _culdaun; } set { _culdaun = value; } }

        public int HelthCost { get; set; }

        double _speed = 0.3;

        public double Speed { get { return _speed; } set { _speed = value; } }

        double _duration = 0;

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

        /// <summary>
        /// Юниты на которых было повешено сало
        /// </summary>
        List<IUnit> _silencedUnits;
        /// <summary>
        /// Тут будет сохранен весь магический урон который получили после получения сайленса
        /// </summary>
        List<int> _saveDemage;

        public SP_Nature_Trent()
        {
            _imageTile = new UC_View_ImageTileControl("SP_Nature_Trent", this);
        }

        public void UseSpall(Map map, Game_Object_In_Call obj, IUnit unit, object property)
        {
            bool upSpell = UnitGenerator.UpPlayerSpell(unit, this);

            if (unit.UnitFrozen == false &&
                !_culdaunBool && LevelCast != 0 &&
                !upSpell && !unit.Silenced &&
                !unit.Hexed &&
                !Paused)
            {
                if (unit.Mana >= ManaCost)
                ///Проверка есть ли мана на каст
                {
                    int lifeTime = 10;
                    int trentHealth = 5;
                    int trentArmor = 0;
                    int trentDamage = 0;

                    if (LevelCast == 1)
                    {
                        _culdaun = 9;
                        trentHealth = 5;
                        lifeTime = 10;
                        trentArmor = 0;
                        trentDamage = 0;
                    }
                    else if (LevelCast == 2)
                    {
                        _culdaun = 7;
                        trentHealth = 5;
                        lifeTime = 15;
                        trentArmor = 2;
                        trentDamage = 5;

                        ManaCost = 35;
                    }
                    else if (LevelCast == 3)
                    {
                        _culdaun = 6;
                        trentHealth = 5;
                        lifeTime = 21;
                        trentDamage = 10;
                        trentArmor = 4;
                        ManaCost = 40;
                    }

                    ///Метод проверки вернет ячейку в которой произошло столкновение
                    Map_Cell call = UnitGenerator.FiratCollisionOnlyGrass(unit.PositionX, unit.PositionY,
                        unit.Range, map, unit.Angel);

                    _culdaunBool = true;
                    _unit = unit;
                    ///Отнимаем нужное количество
                    unit.Mana -= ManaCost;

                    if (call != null)
                    ///Произошло попадение
                    {
                        IUnit grass = call.IUnits.FirstOrDefault(p => p.UnitType == EUnitType.Grass);
                        grass.GatDamage(grass.Health * 2, 0, 0, grass);

                        Unit trent = UnitGenerator.M_Trent(call.IndexLeft, call.IndexTop, map, _unit as Player);
                        map.CreateObjectUnitInCall(call, trent);
                        trent.Health = trent.Health + trentHealth;
                        trent.Demage += trentDamage;
                        trent.Arrmor += trentArmor;

                        trent.AI.Farm = true;
                        trent.AI.Hunt = true;
                        trent.LifeTime(lifeTime);

                        trent.AI.StartAI();

                    }
                    else
                        (unit.GameObject.View as IGameControl).GetDemage("Miss");

                    ///Кулдаун
                    _secondTimer = new Storyboard() { Duration = TimeSpan.FromSeconds(Culdaun) };
                    _secondTimer.Completed += _secondTimer_Completed;
                    _secondTimer.Begin();

                    if (Paused)
                        Pause();

                    if (StartUseSpell != null)
                        StartUseSpell(this, null);
                    UnitGenerator.UpdatePlayerView(unit);
                }
            }
        }

        void _secondTimer_Completed(object sender, object e)
        {
            _secondTimer.Completed -= _secondTimer_Completed;
            _secondTimer = null;
            _culdaunBool = false;
        }


        void item_GetDemageEvent(int phisic, int magic, int pure, IUnit demagedUnit)
        {
            _saveDemage[_silencedUnits.IndexOf(demagedUnit)] += magic;
        }

        /// <summary>
        /// Для итема не имеет смысла
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
