using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Voins.AppCode;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;

namespace Voins.Spell
{
    public class SP_Sniper_Shrapnel : ISpell
    {
        public event EventHandler StartUseSpell;
        public event EventHandler CompletedUseSpell;
        SpellDescriptionInfo _spellDescriptionInfo = new SpellDescriptionInfo()
        {
            Description = "Sniper Shrapnel, Culdaun 8 sec. Duration 4 sec., Mana cost - 15 Demage type: magic",
            LevelDescription =
            "Level1: Demage per second - 5, Slow - 20" + Environment.NewLine +
            "Level2: Demage per second - 6, Slow - 30" + Environment.NewLine +
            "Level3: Demage per second - 8, Slow - 35"
        };
        public SpellDescriptionInfo SpellDescriptionInfo { get { return _spellDescriptionInfo; } set { _spellDescriptionInfo = value; } }
           
        int _size = 2;
        double _oldCuldaun;
        IUnit _unit;
        bool _culdaunBool;
        public bool CuldaunBool { get { return _culdaunBool; } set { _culdaunBool = value; } }

        bool _culdaunBoolAttack;
        public bool CuldaunBoolAttack { get { return _culdaunBoolAttack; } set { _culdaunBoolAttack = value; } }

        double _abilityAction;
        /// <summary>
        /// Время действия способности
        /// </summary>
        public double AbilityAction { get { return _abilityAction; } set { _abilityAction = value; } }
      
        public string Name { get; set; }

        int _manaCost = 15;
        public int ManaCost { get { return _manaCost; } set { _manaCost = value; } }

        double _culdaun = 8;
        public double Culdaun { get { return _culdaun; } set { _culdaun = value; } }

        public int HelthCost { get; set; }

        double _speed = 0.3;

        public double Speed { get { return _speed; } set { _speed = value; } }

        double _duration = 4;

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
        IAnimationControl _animationControl;

        public SP_Sniper_Shrapnel()
        {
            _imageTile = new UC_View_ImageTileControl("SP_Sniper_Shrapnel", this);
        }

        public void UseSpall(Map map, Game_Object_In_Call obj, IUnit unit, object property)
        {
            bool upSpell = UnitGenerator.UpPlayerSpell(unit, this);
            _unit = unit;
            if (unit.UnitFrozen == false &&
                !_culdaunBool && LevelCast != 0 && !upSpell && !unit.Silenced && !Paused)
            {
                if (unit.Mana >= ManaCost)
                ///Проверка есть ли мана на каст
                {
                    ///Флаг кулдауна
                    _culdaunBool = true;

                    ///Отнимаем нужное количество
                    unit.Mana -= ManaCost;

                    ///Тут кординаты ячеек в которых действует тучка
                    List<Point> callsPoint = new List<Point>();
                    ///Получаем ячейки которые находятся перед героем
                    ///использывавшим тучку
                    int xNew = unit.PositionX;
                    int yNew = unit.PositionY;
                    if (unit.Angel == EAngel.Left)
                    {
                       // xNew = xNew - 1;
                        for (int i = 0; i <= _size; i++)
                        {
                            xNew = xNew - 1;
                            callsPoint.Add(new Point(xNew, yNew));
                            if (i == 0 || i == _size)
                            {
                                callsPoint.Add(new Point(xNew, yNew + 1));
                                callsPoint.Add(new Point(xNew, yNew - 1));
                            }
                            else
                            {
                                callsPoint.Add(new Point(xNew, yNew + 1));
                                callsPoint.Add(new Point(xNew, yNew + 2));
                                callsPoint.Add(new Point(xNew, yNew - 1));
                                callsPoint.Add(new Point(xNew, yNew - 2));
                            }
                        }
                    }
                    else if (unit.Angel == EAngel.Right)
                    {
                       // xNew = xNew + 1;
                        for (int i = 0; i <= _size; i++)
                        {
                            xNew = xNew + 1;
                            callsPoint.Add(new Point(xNew, yNew));
                            if (i == 0 || i == _size)
                            {
                                callsPoint.Add(new Point(xNew, yNew + 1));
                                callsPoint.Add(new Point(xNew, yNew - 1));
                            }
                            else
                            {
                                callsPoint.Add(new Point(xNew, yNew));
                                callsPoint.Add(new Point(xNew, yNew + 1));
                                callsPoint.Add(new Point(xNew, yNew + 2));
                                callsPoint.Add(new Point(xNew, yNew - 1));
                                callsPoint.Add(new Point(xNew, yNew - 2));
                            }
                        }
                    }
                    else if (unit.Angel == EAngel.Top)
                    {
                       // yNew = yNew - 1;
                        for (int i = 0; i <= _size; i++)
                        {
                            yNew = yNew - 1;
                            callsPoint.Add(new Point(xNew, yNew));
                            if (i == 0 || i == _size)
                            {
                                callsPoint.Add(new Point(xNew + 1, yNew));
                                callsPoint.Add(new Point(xNew - 1, yNew));
                            }
                            else
                            {
                                callsPoint.Add(new Point(xNew + 1, yNew));
                                callsPoint.Add(new Point(xNew + 2, yNew));
                                callsPoint.Add(new Point(xNew - 1, yNew));
                                callsPoint.Add(new Point(xNew - 2, yNew));
                            }
                        }
                    }
                    else if (unit.Angel == EAngel.Bottom)
                    {
                       // yNew = yNew + 1;
                        for (int i = 0; i <= _size; i++)
                        {
                            yNew = yNew + 1;
                            callsPoint.Add(new Point(xNew, yNew));
                            if (i == 0 || i == _size)
                            {
                                callsPoint.Add(new Point(xNew + 1, yNew));
                                callsPoint.Add(new Point(xNew - 1, yNew));
                            }
                            else
                            {
                                callsPoint.Add(new Point(xNew + 1, yNew));
                                callsPoint.Add(new Point(xNew + 2, yNew));
                                callsPoint.Add(new Point(xNew - 1, yNew));
                                callsPoint.Add(new Point(xNew - 2, yNew));
                            }
                        }
                    }

                    ///Кординаты ячеек есть
                    ///теперь спауним шрапнель
                    foreach (var item in callsPoint)
                    {
                        Map_Cell call = map.Calls.FirstOrDefault(p => p.IndexLeft == item.X && p.IndexTop == item.Y);
                        if (call != null &&
                            !call.Block)
                        {
                            ///Создаем визуальный объект шрапнель
                            UC_Sniper_Shrapnel acidSpray = new UC_Sniper_Shrapnel();
                            acidSpray.ChengAngel(unit.Angel);

                            Bullet bullAcidSpray = new Bullet() { Name = "SP_Alchemist_AcidSpray" };
                            bullAcidSpray.GameObject = new Game_Object_In_Call()
                            {
                                EnumCallType = EnumCallType.Bullet,
                                View = acidSpray
                            };
                            bullAcidSpray.UnitUsed = unit;
                            bullAcidSpray.PositionX = (int)item.X;
                            bullAcidSpray.PositionY = (int)item.Y;
                            bullAcidSpray.Speed = Speed;

                            if (LevelCast == 1)
                            {
                                bullAcidSpray.SpeedSlow = 0.2;
                                bullAcidSpray.DemageMagic = 6;
                            }
                            else if (LevelCast == 2)
                            {
                                bullAcidSpray.SpeedSlow = 0.3;
                                bullAcidSpray.DemageMagic = 8;
                            }
                            else if (LevelCast == 3)
                            {
                                bullAcidSpray.SpeedSlow = 0.35;
                                bullAcidSpray.DemageMagic = 10;
                            }

                            bullAcidSpray.CurrentMap = map;
                            bullAcidSpray.Angel = unit.Angel;
                            bullAcidSpray.Range = _unit.Range;
                            bullAcidSpray.Duration = Duration;

                            bullAcidSpray.Spells.Add(new SPB_Sniper_Shrapnel() { Name = "SniperShrapnel" });

                            ///И его же добавим в масив всех объектов
                            map.GameObjectInCall.Add(bullAcidSpray.GameObject);

                            Canvas.SetLeft(bullAcidSpray.GameObject.View, bullAcidSpray.PositionX * 50);
                            Canvas.SetTop(bullAcidSpray.GameObject.View, bullAcidSpray.PositionY * 50);
                            ///Отображение
                            map.MapCanvas.Children.Add(bullAcidSpray.GameObject.View);

                            _animationControl = (bullAcidSpray.GameObject.View as IAnimationControl);
                            _animationControl.StartMuveAnimation(0);

                            bullAcidSpray.UseSpall("SniperShrapnel");
                        }
                    }

                    if (StartUseSpell != null)
                        StartUseSpell(this, null);

                    ///Таймер кулдауна заклинания
                    _firstTimer = new Storyboard() { Duration = TimeSpan.FromSeconds(Culdaun) };
                    _firstTimer.Completed += _firstTimer_Completed; 
                    _firstTimer.Begin();
                  

                    if (Paused)
                        Pause();

                 

                    UnitGenerator.UpdatePlayerView(unit);
                }
            }
        }

        void _secondTimer_Completed(object sender, object e)
        {
            CuldaunBoolAttack = false;
            _secondTimer.Completed -= _secondTimer_Completed;
            _secondTimer = null;
        }

        void _firstTimer_Completed(object sender, object e)
        {
            _culdaunBool = false;
            _firstTimer.Completed -= _firstTimer_Completed;
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
            if (_animationControl != null)
            _animationControl.Pause();
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
            if (_animationControl != null)
            _animationControl.StopPause();

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
