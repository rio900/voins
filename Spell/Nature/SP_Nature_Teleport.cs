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
    public class SP_Nature_Teleport : ISpell, IMoveSpell
    {
        public event EventHandler StartUseSpell;
        public event EventHandler CompletedUseSpell;

        bool _culdaunBool;
        public bool CuldaunBool { get { return _culdaunBool; } set { _culdaunBool = value; } }

        public string Name { get; set; }
        SpellDescriptionInfo _spellDescriptionInfo = new SpellDescriptionInfo()
        {
            Description = "",
            LevelDescription =
            "" + Environment.NewLine

        };
        public SpellDescriptionInfo SpellDescriptionInfo { get { return _spellDescriptionInfo; } set { _spellDescriptionInfo = value; } }

        int _manaCost = 20;
        public int ManaCost { get { return _manaCost; } set { _manaCost = value; } }

        double _culdaun = 20;
        public double Culdaun { get { return _culdaun; } set { _culdaun = value; } }

        public int HelthCost { get; set; }

        double _speed = 0.3;
        public double Speed { get { return _speed; } set { _speed = value; } }

        double _duration = 1;

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
        public Storyboard _storyboard;

        IUnit _unit;
        List<Point> _callsPoints;
        Map_Cell _activeCall;
        Map _map;
        int _size = 2;
        double _bonusSpeed = 0.15;
        double _bonusAttackSpeed = 0.25;

        public SP_Nature_Teleport()
        {
            _imageTile = new UC_View_ImageTileControl("SP_Nature_Teleport", this);
        }

       
        public void UseSpall(Map map, Game_Object_In_Call obj, IUnit unit, object property)
        {
            _unit = unit;
            _map = map;
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
                    ///Флаг кулдауна
                    _culdaunBool = true;
                    ///Отнимаем нужное количество
                    unit.Mana -= ManaCost;

                    _size = 100;
                    if (LevelCast == 1)
                    {
                        _culdaun = 15;
                    }
                    else if (LevelCast == 2)
                    {
                        _culdaun = 12;
                    }
                    else if (LevelCast == 3)
                    {
                        _culdaun = 10;
                    }

                    ///Выбираем клетку куда прыгнуть
                    ///Тут кординаты ячеек в которых действует тучка
                    _callsPoints = new List<Point>();
                    ///Получаем ячейки которые находятся перед героем
                    ///использывавшим тучку
                    ///Сначала добавляем ячеку с героем
                    int xNew = unit.PositionX;
                    int yNew = unit.PositionY;
                    if (unit.Angel == EAngel.Left)
                    {
                        for (int i = 0; i <= _size; i++)
                        {
                            xNew = xNew - 1;

                            _callsPoints.Add(new Point(xNew, yNew));
                        }
                    }
                    else if (unit.Angel == EAngel.Right)
                    {
                        for (int i = 0; i <= _size; i++)
                        {
                            xNew = xNew + 1;

                            _callsPoints.Add(new Point(xNew, yNew));
                        }
                    }
                    else if (unit.Angel == EAngel.Top)
                    {
                        for (int i = 0; i <= _size; i++)
                        {
                            yNew = yNew - 1;

                            _callsPoints.Add(new Point(xNew, yNew));
                        }
                    }
                    else if (unit.Angel == EAngel.Bottom)
                    {
                        for (int i = 0; i <= _size; i++)
                        {
                            yNew = yNew + 1;

                            _callsPoints.Add(new Point(xNew, yNew));
                        }
                    }

                    _activeCall = null;
                    _callsPoints.Reverse();
                    ///Выбираем ячейку куда прыгать
                    foreach (var item in _callsPoints)
                    {
                        _activeCall = map.Calls.FirstOrDefault(p => p.IndexLeft == item.X && p.IndexTop == item.Y);
                        if (_activeCall != null &&
                            !_activeCall.Block &&
                            !_activeCall.Used &&
                            !_activeCall.Using)
                        {
                            ///Прыгаем, в первую дальнюю ячейку
                            _activeCall.Using = true;
                            break;
                        }
                    }
                    if (_activeCall != null)
                    {
                        _unit.UnitFrozen = true;



                    }
                    ///Таймер время кастования телепорта
                    _firstTimer = new Storyboard() { Duration = TimeSpan.FromSeconds(Duration) };
                    _firstTimer.Completed += _firstTimer_Completed;
                    _firstTimer.Begin();

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

        void _storyboard_Completed(object sender, object e)
        {

            _storyboard.Completed -= _storyboard_Completed;
            _storyboard = null;

            if (!_unit.Dead)
            {
                var callOld = _map.Calls.Single(p => p.IndexLeft == _unit.PositionX && p.IndexTop == _unit.PositionY);
                ///Удаляем из старой ячейки ссылку на юнит
                callOld.IUnits.Remove(_unit);

                if (!callOld.IUnits.Any(p => p.GameObject.EnumCallType == EnumCallType.UnitBlock))
                {
                    ///Делаем ячейку пустой
                    callOld.Used = false;
                }

                _unit.PositionX = _activeCall.IndexLeft;
                _unit.PositionY = _activeCall.IndexTop;
                if (!_unit.Dead)
                {
                    ///Добавляем в нее ссылку на юнит
                    _activeCall.IUnits.Add(_unit);
                    _activeCall.Using = false;
                    ///Убераем то что колонка использывалась, теперь ставим что она все время используется
                    _activeCall.Used = true;
                }
            }

            _activeCall.Using = false;
            _unit.UnitFrozen = false;
        }

        void _firstTimer_Completed(object sender, object e)
        {
            _firstTimer.Completed -= _firstTimer_Completed;
            _firstTimer = null;

            if (!_unit.Dead &&
                _activeCall != null &&
                !_unit.Silenced &&
                !_unit.Hexed)
            {
                string animationType = "(Canvas.Left)";
                double mouvePos = 0;
                ///Новая позиция
                double newPos = 0;

                if (_unit.Angel == EAngel.Left || _unit.Angel == EAngel.Right)
                {
                    animationType = "(Canvas.Left)";
                    mouvePos = Canvas.GetLeft(_unit.GameObject.View);
                    newPos = _activeCall.IndexLeft;
                }
                else
                {
                    animationType = "(Canvas.Top)";
                    mouvePos = Canvas.GetTop(_unit.GameObject.View);
                    newPos = _activeCall.IndexTop;
                }

                _storyboard = new Storyboard();
                var animation = new DoubleAnimation
                {
                    From = mouvePos,
                    To = newPos * 50,
                    Duration = TimeSpan.FromSeconds(0.3)
                };
                _storyboard.Children.Add(animation);
                Storyboard.SetTargetProperty(animation, animationType);
                Storyboard.SetTarget(_storyboard, _unit.GameObject.View);
                _storyboard.Completed += _storyboard_Completed;
                _storyboard.Begin();
            }
            else
                _unit.UnitFrozen = false;


            UnitGenerator.UpdatePlayerView(_unit);
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
            //if (_storyboard != null)
            //    _storyboard.Pause();

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
            //if (_storyboard != null)
            //    _storyboard.Resume();

            if (PausedEvent != null)
                PausedEvent(this, null);
        }
        #endregion



        public void StopMoveSpell()
        {
            //_activeCall.Using = true;
        }
    }
}
