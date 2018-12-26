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
    public class SP_Sniper_Assassinate : ISpell
    {
        public event EventHandler StartUseSpell;
        public event EventHandler CompletedUseSpell;
        SpellDescriptionInfo _spellDescriptionInfo = new SpellDescriptionInfo()
        {
            Description = "Assassinate, Culdaun 5 sec., Demage type: magic",
            LevelDescription =
            "Level1: Hero demage + 30 demage."
        };
        public SpellDescriptionInfo SpellDescriptionInfo { get { return _spellDescriptionInfo; } set { _spellDescriptionInfo = value; } }

        bool _culdaunBool;
        public bool CuldaunBool { get { return _culdaunBool; } set { _culdaunBool = value; } }

        public string Name { get; set; }

        int _manaCost = 30;
        public int ManaCost { get { return _manaCost; } set { _manaCost = value; } }

        double _culdaun = 10;
        public double Culdaun { get { return _culdaun; } set { _culdaun = value; } }

        public int HelthCost { get; set; }

        double _speed = 0.3;
        public double Speed { get { return _speed; } set { _speed = value; } }

        double _duration = 0;

        public double Duration { get { return _duration; } set { _duration = value; } }

        bool _isUlt = true;
        public bool IsUlt { get { return _isUlt; } set { _isUlt = value; } }

        int _levelCast = 0;
        public int LevelCast { get { return _levelCast; } set { _levelCast = value; } }
        int _maxLevelCast = 1;
        public int MaxLevelCast { get { return _maxLevelCast; } set { _maxLevelCast = value; } }

        UC_View_ImageTileControl _imageTile;
        public UC_View_ImageTileControl ImageTile { get { return _imageTile; } set { _imageTile = value; } }

        public Storyboard _firstTimer;
        public Storyboard _secondTimer;
      
        IUnit _unit;
        public SP_Sniper_Assassinate()
        {
            _imageTile = new UC_View_ImageTileControl("SP_Sniper_Assassinate", this);
        }

        /// <summary>
        /// Запуск пули
        /// </summary>
        /// <param name="property">Уровень прокача способности, умножется на 5 и получается магический урон стрелы</param>
        public void UseSpall(Map map, Game_Object_In_Call obj, IUnit unit, object property)
        {
            _unit = unit;
            bool upSpell = UnitGenerator.UpPlayerSpell(unit, this);


            /// Если есть аганим
            if (UnitGenerator.HasAghanim(unit))
            {
                /// То ульт у снайпера быстрее востанавливается
                _culdaun = _culdaun / 2;
            }


            if (unit.UnitFrozen == false &&
                !_culdaunBool && LevelCast != 0 &&
                !upSpell && !unit.Silenced &&
                !unit.Hexed &&
                !Paused)
            {
                if (unit.Mana >= ManaCost)
                ///Проверка есть ли мана на каст
                {
                    Speed = unit.AttackSpeed;

                    ///Флаг кулдауна
                    _culdaunBool = true;

                    ///Отнимаем нужное количество
                    unit.Mana -= ManaCost;

                    ///Создаем визуальный объект стрела
                    UC_Sniper_Headshot arrow = new UC_Sniper_Headshot();
                    arrow.ChengAngel(unit.Angel);
                    Bullet bullArrow = new Bullet();
                    bullArrow.GameObject = new Game_Object_In_Call()
                    {
                        EnumCallType = EnumCallType.Bullet,
                        View = arrow
                    };
                    bullArrow.UnitUsed = unit;
                    bullArrow.PositionX = unit.PositionX;
                    bullArrow.PositionY = unit.PositionY;
                    bullArrow.Speed = 0.3;
                    bullArrow.DemagePhys = unit.Demage;

                    if (LevelCast == 1)
                    {
                         bullArrow.DemageMagic = 30;
                    }


                    bullArrow.CurrentMap = map;
                    bullArrow.Angel = unit.Angel;
                    bullArrow.Range = 30;

                    ///Теперь выбераем цель пули
                    ///Это первый юнит на которого была наведена пуля

                    Map_Cell callsPoint = null;
                    ///Получаем ячейки которые находятся перед героем
                    #region Select first unit
                    int xNew = unit.PositionX;
                    int yNew = unit.PositionY;
                    if (unit.Angel == EAngel.Left)
                    {
                        for (int i = 0; i <= map.Width / 50; i++)
                        {
                            xNew = xNew - 1;
                            Map_Cell call = map.Calls.FirstOrDefault(p => p.IndexLeft == xNew && p.IndexTop == yNew);
                            if (call != null && !call.Block)
                            {
                                if (call.IUnits.Any(p => p.GameObject.EnumCallType == EnumCallType.Unit ||
                                   p.GameObject.EnumCallType == EnumCallType.Player))
                                {
                                    callsPoint = call;
                                    break;
                                }
                            }
                        }
                    }
                    else if (unit.Angel == EAngel.Right)
                    {
                        for (int i = 0; i <= map.Width / 50; i++)
                        {
                            xNew = xNew + 1;
                            Map_Cell call = map.Calls.FirstOrDefault(p => p.IndexLeft == xNew && p.IndexTop == yNew);
                            if (call != null && !call.Block)
                            {
                                if (call.IUnits.Any(p => p.GameObject.EnumCallType == EnumCallType.Unit ||
                                   p.GameObject.EnumCallType == EnumCallType.Player))
                                {
                                    callsPoint = call;
                                    break;
                                }
                            }
                        }
                    }
                    else if (unit.Angel == EAngel.Top)
                    {
                        for (int i = 0; i <= map.Height / 50; i++)
                        {
                            yNew = yNew - 1;
                            Map_Cell call = map.Calls.FirstOrDefault(p => p.IndexLeft == xNew && p.IndexTop == yNew);
                            if (call != null && !call.Block)
                            {
                                if (call.IUnits.Any(p => p.GameObject.EnumCallType == EnumCallType.Unit ||
                                   p.GameObject.EnumCallType == EnumCallType.Player))
                                {
                                    callsPoint = call;
                                    break;
                                }
                            }
                        }
                    }
                    else if (unit.Angel == EAngel.Bottom)
                    {
                        for (int i = 0; i <= map.Height / 50; i++)
                        {
                            yNew = yNew + 1;
                            Map_Cell call = map.Calls.FirstOrDefault(p => p.IndexLeft == xNew && p.IndexTop == yNew);
                            if (call != null && !call.Block)
                            {
                                if (call.IUnits.Any(p => p.GameObject.EnumCallType == EnumCallType.Unit ||
                                   p.GameObject.EnumCallType == EnumCallType.Player))
                                {
                                    callsPoint = call;
                                    break;
                                }
                            }
                        }
                    }

                    if (callsPoint != null)
                    {
                        bullArrow.Aim = callsPoint.IUnits.FirstOrDefault(p => p.GameObject.EnumCallType == EnumCallType.Player);
                        if (bullArrow.Aim == null)
                            bullArrow.Aim = callsPoint.IUnits.FirstOrDefault(p => p.GameObject.EnumCallType == EnumCallType.Unit);
                    }
                    #endregion

                    bullArrow.Spells.Add(new SPB_Sniper_Assassinate() { Name = "Fly" });

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
