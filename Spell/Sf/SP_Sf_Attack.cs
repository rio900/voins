﻿using System;
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
    public class SP_Sf_Attack : ISpell
    {
        public event EventHandler StartUseSpell;
        public event EventHandler CompletedUseSpell;

        bool _culdaunBool;
        public bool CuldaunBool { get { return _culdaunBool; } set { _culdaunBool = value; } }

        public string Name { get; set; }
        SpellDescriptionInfo _spellDescriptionInfo = new SpellDescriptionInfo()
        {
            MinCd = true,
            Description = "Sniper Headshot, Mana cost - 3, Demage type: physical",
            LevelDescription =
            "Level1: Chans bush 25%, +5 Demage" + Environment.NewLine +
            "Level2: Chans bush 30%, +10 Demage" + Environment.NewLine +
            "Level3: Chans bush 35%, +15 Demage"
        };
        public SpellDescriptionInfo SpellDescriptionInfo { get { return _spellDescriptionInfo; } set { _spellDescriptionInfo = value; } }

        int _manaCost = 3;
        public int ManaCost { get { return _manaCost; } set { _manaCost = value; } }

        double _culdaun = 1;
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

        UC_View_ImageTileControl _imageTile;
        public UC_View_ImageTileControl ImageTile { get { return _imageTile; } set { _imageTile = value; } }

        public Storyboard _firstTimer;
        public Storyboard _secondTimer;

        IUnit _unit;
        public SP_Sf_Attack()
        {
            _imageTile = new UC_View_ImageTileControl("SP_Sf_Attack", this);
        }

        /// <summary>
        /// Запуск пули
        /// </summary>
        /// <param name="property">Уровень прокача способности, умножется на 5 и получается магический урон стрелы</param>
        public void UseSpall(Map map, Game_Object_In_Call obj, IUnit unit, object property)
        {
            _unit = unit;
            bool upSpell = UnitGenerator.UpPlayerSpell(unit, this);

            if (unit.UnitFrozen == false &&
                !_culdaunBool && LevelCast != 0 &&
                !upSpell &&
                !unit.Hexed &&
                !Paused)
            {
                if (unit.Mana >= ManaCost)
                ///Проверка есть ли мана на каст
                {
                    //Speed = unit.AttackSpeed;

                    ///Флаг кулдауна
                    _culdaunBool = true;

                    ///Отнимаем нужное количество
                    unit.Mana -= ManaCost;

                    Culdaun = unit.AttackSpeed * 2;
                    if (Culdaun < StaticVaribl.AttackSpeedRangeMaximum)
                        Culdaun = StaticVaribl.AttackSpeedRangeMaximum;

                    Bullet bullArrow = new Bullet();

                    bullArrow.GameObject = new Game_Object_In_Call()
                    {
                        EnumCallType = EnumCallType.Bullet,
                    };

                    /// Если есть скади
                    if (UnitGenerator.HasSkadi(unit))
                    {
                        UC_Skadi skadiArrow = new UC_Skadi();
                        skadiArrow.ChengAngel(unit.Angel);
                        bullArrow.GameObject.View = skadiArrow;
                    }
                    else
                    {
                        ///Создаем визуальный объект стрела
                        UC_Sf_Bullet arrow = new UC_Sf_Bullet();
                        arrow.ChengAngel(unit.Angel);
                        bullArrow.GameObject.View = arrow;
                    }


                    bullArrow.UnitUsed = unit;
                    bullArrow.PositionX = unit.PositionX;
                    bullArrow.PositionY = unit.PositionY;
                    bullArrow.Speed = Speed;
                    bullArrow.DemagePhys = unit.Demage;

                    UnitGenerator.MKB_Bush(bullArrow, unit);
                    ///Магический урон зависит от прокача стрел
                    //bullArrow.DemageMagic = 5 * (int)property;


                    #region SoulCount
                    Buff buff = _unit.Buffs.FirstOrDefault(p => p.Name == "SoulCount");
                    if (buff == null)
                    {
                        buff = new Buff()
                        {
                            Passive = true,
                            Duration = LevelCast,
                            Name = "SoulCount"
                        };
                        _unit.Buffs.Add(buff);
                    }
                    bullArrow.DemagePhys += buff.SoulCount;
                    #endregion

                    if (LevelCast == 1)
                    {
                        buff.MaxSoulCount = 12;
                        bullArrow.MinusArmor = 1;
                    }
                    else if (LevelCast == 2)
                    {
                        buff.MaxSoulCount = 20;
                        bullArrow.MinusArmor = 2;
                    }
                    else if (LevelCast == 3)
                    {
                        buff.MaxSoulCount = 32;
                        bullArrow.MinusArmor = 3;
                    }

                    bullArrow.CurrentMap = map;
                    bullArrow.Angel = unit.Angel;
                    bullArrow.Range = _unit.Range;
                    ///Поведение такое же как у стрел боника
                    bullArrow.Spells.Add(new SPB_FireArrow() { Name = "Fly" });

                    ///И его же добавим в масив всех объектов
                    map.GameObjectInCall.Add(bullArrow.GameObject);

                    Canvas.SetLeft(bullArrow.GameObject.View, bullArrow.PositionX * 50);
                    Canvas.SetTop(bullArrow.GameObject.View, bullArrow.PositionY * 50);
                    ///Отображение
                    map.MapCanvas.Children.Add(bullArrow.GameObject.View);

                    bullArrow.UseSpall("Fly");

                    ///Таймер кулдауна заклинания
                    _secondTimer = new Storyboard() { Duration = TimeSpan.FromSeconds(Culdaun) };
                    _secondTimer.Completed += _secondTimer_Completed;
                    _secondTimer.Begin();

                    if (Paused)
                    {
                        Pause();
                    }

                    if (StartUseSpell != null)
                        StartUseSpell(this, null);

                    UnitGenerator.UpdatePlayerView(unit);
                }
                else
                ///Маны нету
                {

                }
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
