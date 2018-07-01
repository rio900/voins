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
    public class SPB_Alchemist_UnstableConcoction : ISpell
    {
        /// <summary>
        /// Событие для тестирования спелов
        /// </summary>
        public event EventHandler E_TestEvent;
        public event EventHandler StartUseSpell;
        public event EventHandler CompletedUseSpell;

        public string Name { get; set; }
        Bullet _bullet;
        IUnit _unit;
        Map _map;
        int _xNew = 0;
        int _yNew = 0;
        /// <summary>
        /// Если попадение то true
        /// </summary>
        bool _exept = false;
        /// <summary>
        /// Если взрыв произошол
        /// </summary>
        bool _boomTrue = false;
        SpellDescriptionInfo _spellDescriptionInfo = new SpellDescriptionInfo();
        public SpellDescriptionInfo SpellDescriptionInfo { get { return _spellDescriptionInfo; } set { _spellDescriptionInfo = value; } }

        bool _culdaunBool;
        public bool CuldaunBool { get { return _culdaunBool; } set { _culdaunBool = value; } }
        double _duration = 0;
        public double Duration { get { return _duration; } set { _duration = value; } }
        int _levelCast = 1;
        public int LevelCast { get { return _levelCast; } set { _levelCast = value; } }
        int _maxLevelCast = 1;
        public int MaxLevelCast { get { return _maxLevelCast; } set { _maxLevelCast = value; } }
        bool _isUlt = false;
        public bool IsUlt { get { return _isUlt; } set { _isUlt = value; } }
        Storyboard _storyboard;
        DoubleAnimation _animation;
        UC_View_ImageTileControl _imageTile;
        public UC_View_ImageTileControl ImageTile { get { return _imageTile; } set { _imageTile = value; } }

        public Storyboard _firstTimer;
        public Storyboard _secondTimer;
        List<Bullet> _boom = new List<Bullet>();
 
        public SPB_Alchemist_UnstableConcoction()
        {
            _imageTile = new UC_View_ImageTileControl("SP_Alchemist_UnstableConcoction", this);
        }

        public void UseSpall(Map map, Game_Object_In_Call obj, IUnit unit, object property)
        {
            _bullet = property as Bullet;
            _map = map;
            _unit = _bullet.UnitUsed;

            Map_Cell oldCall = map.Calls.Single(p => p.IndexLeft == _bullet.PositionX &&
                p.IndexTop == _bullet.PositionY);
            oldCall.Bullet.Add(_bullet);

            BulletMuve(_bullet);
        }
        private void BulletMuve(Bullet bullArrow)
        {
            int xNew = 0;
            int yNew = 0;
            if (bullArrow.Angel == EAngel.Left)
            {
                xNew = bullArrow.PositionX - 1;
                yNew = bullArrow.PositionY;
            }
            else if (bullArrow.Angel == EAngel.Right)
            {
                xNew = bullArrow.PositionX + 1;
                yNew = bullArrow.PositionY;
            }
            else if (bullArrow.Angel == EAngel.Top)
            {
                xNew = bullArrow.PositionX;
                yNew = bullArrow.PositionY - 1;
            }
            else if (bullArrow.Angel == EAngel.Bottom)
            {
                xNew = bullArrow.PositionX;
                yNew = bullArrow.PositionY + 1;
            }

            _xNew = xNew;
            _yNew = yNew;
            ///Запуск анимации перемещения
            StartStoryboard(bullArrow, xNew, yNew);
        }
        private void StartStoryboard(Bullet bullet, int xNew, int yNew)
        {
            string animationType = "(Canvas.Left)";
            double mouvePos = 0;
            ///Новая позиция
            double newPos = 0;


            if (bullet.Angel == EAngel.Left || bullet.Angel == EAngel.Right)
            {
                animationType = "(Canvas.Left)";
                mouvePos = Canvas.GetLeft(bullet.GameObject.View);
                newPos = xNew;

            }
            else
            {
                animationType = "(Canvas.Top)";
                mouvePos = Canvas.GetTop(bullet.GameObject.View);
                newPos = yNew;

            }

            _storyboard = new Storyboard();
            _animation = new DoubleAnimation
            {
                // EasingFunction =new ExponentialEase(){ EasingMode = EasingMode.EaseInOut},
                //From = mouvePos,
                To = newPos * 50,
                Duration = TimeSpan.FromSeconds(1 * bullet.Speed)
            };

            _storyboard.Children.Add(_animation);
            Storyboard.SetTargetProperty(_animation, animationType);
            Storyboard.SetTarget(_storyboard, bullet.GameObject.View);
            _storyboard.Completed += storyboard_Completed;

            _storyboard.Begin();
            ///Таймер половины перемещения пули
            ///Необходим чтобы проверить попадание
            _firstTimer = new Storyboard() { Duration = TimeSpan.FromSeconds((1 * bullet.Speed) / 2) };
            _firstTimer.Completed += _firstTimer_Completed;
            _firstTimer.Begin();

            if (Paused)
                Pause();
        }

        void _firstTimer_Completed(object sender, object e)
        {
            _firstTimer.Completed -= _firstTimer_Completed;
            _firstTimer = null;

            Map_Cell oldCall = _map.Calls.Single(p => p.IndexLeft == _bullet.PositionX &&
               p.IndexTop == _bullet.PositionY);
            oldCall.Bullet.Remove(_bullet);
            /////Сначала проверяем старую ячейку на попадение
            //Exept();
            _bullet.PositionX = _xNew;
            _bullet.PositionY = _yNew;
            Map_Cell newCall = _map.Calls.FirstOrDefault(p => p.IndexLeft == _bullet.PositionX &&
            p.IndexTop == _bullet.PositionY);

            Exept();
            if (_bullet.Exept && newCall != null && !newCall.Block &&
                !newCall.IUnits.Any(p => p.GameObject.EnumCallType == EnumCallType.UnitBlock))
            {
                BoomMethod(_bullet.PositionX, _bullet.PositionY);
                _boomTrue = true;
            }
            else if (_bullet.Exept)
            {
                BoomMethod(oldCall.IndexLeft, oldCall.IndexTop);
                _boomTrue = true;
            }


            if (_bullet.Exept == true)
            {
                (_bullet.GameObject.View as IGameControl).Remove(_map.MapCanvas);
                ///Было попадение, анимация исчезновения стрелы
                //_bullet.GameObject.View.Visibility = Visibility.Collapsed;
                ///Было попадение стрела исчезает
                ///Удаляем из масива всех объектов
                _map.GameObjectInCall.Remove(_bullet.GameObject);
                //_map.MapCanvas.Children.Remove(_bullet.GameObject.View);
            }
            else
            {
                newCall.Bullet.Add(_bullet);
            }

        }

     
        /// <summary>
        /// Банка не наносит урон, урон наносит взрыв
        /// </summary>
        private void Exept()
        {
            //!_exept
            if (_map.Calls.Any(p => p.IndexLeft == _bullet.PositionX && p.IndexTop == _bullet.PositionY))
            {
                ///Получим ячейку куда попала колба
                var call = _map.Calls.Single(p => p.IndexLeft == _bullet.PositionX && p.IndexTop == _bullet.PositionY);

                if (!call.Block)
                {
                    if (!_bullet.Exept)
                    {
                        ///Наносим урон объектам в ячейке (если они есть)
                        for (int i = 0; i < call.IUnits.Count; i++)
                        {
                            if (call.IUnits[i] != _bullet.UnitUsed &&
                                call.IUnits[i].GroupType != _bullet.UnitUsed.GroupType)
                            {
                                _bullet.Exept = true;
                            }
                        }
                    }
                }
                else
                    _bullet.Exept = true;
            }
            else
                _bullet.Exept = true;
        }
        /// <summary>
        /// Этот метод наносит урон
        /// </summary>
        private void BoomMethod(int x,int y)
        {
             ///Если колба попала или должна исчезнуть
                ///Берем все ячейки вокруг колбы
                List<Point> roundPoint = UnitGenerator.RoundNumber(x, y);
                roundPoint.Add(new Point(x, y));
                ///Теперь рисуем взрыв
                for (int i = 0; i < roundPoint.Count; i++)
                {
                    Map_Cell callBoom = _map.Calls.FirstOrDefault(p => p.IndexLeft == roundPoint[i].X && p.IndexTop == roundPoint[i].Y);
                    if (callBoom != null && !callBoom.Block)
                    {
                        UC_Alchemist_UnstableConcoction arrow = new UC_Alchemist_UnstableConcoction();
                        if (i == 0)
                            arrow.ChengAngel(EAngel.Left);
                        else if (i == 1)
                            arrow.ChengAngel(EAngel.Right);
                        else if (i == 2)
                            arrow.ChengAngel(EAngel.Top);
                        else if (i == 3)
                            arrow.ChengAngel(EAngel.Bottom);
                        else if (i == 4)
                            ///Центральная ячейка
                            arrow.ChengAngelCenter();

                        Bullet bullArrow = new Bullet();
                        bullArrow.GameObject = new Game_Object_In_Call()
                        {
                            EnumCallType = EnumCallType.Bullet,
                            View = arrow
                        };
                        bullArrow.UnitUsed = _bullet.UnitUsed;
                        bullArrow.PositionX = (int)roundPoint[i].X;
                        bullArrow.PositionY = (int)roundPoint[i].Y;
                        bullArrow.Speed = 0;
                        bullArrow.DemagePhys = _bullet.DemagePhys;
                        bullArrow.CurrentMap = _map;
                        bullArrow.Angel = _bullet.UnitUsed.Angel;
                        bullArrow.Range = 0;

                        ///И его же добавим в масив всех объектов
                        _map.GameObjectInCall.Add(bullArrow.GameObject);
                        Canvas.SetLeft(bullArrow.GameObject.View, bullArrow.PositionX * 50);
                        Canvas.SetTop(bullArrow.GameObject.View, bullArrow.PositionY * 50);
                        ///Отображение
                        _map.MapCanvas.Children.Add(bullArrow.GameObject.View);

                        UnitGenerator.AddDamage(callBoom, bullArrow);
                        UnitGenerator.AddStune(callBoom, bullArrow,_bullet.StunDuration);

                        _boom.Add(bullArrow);
                    }
                }

                _secondTimer = new Storyboard() { Duration = TimeSpan.FromSeconds(0.5) };
                _secondTimer.Completed += _secondTimer_Completed;
                _secondTimer.Begin();
        }
        /// <summary>
        /// Таймер исчезновения взрыва
        /// </summary>
        void _secondTimer_Completed(object sender, object e)
        {
            if (_secondTimer != null)
            {
                _secondTimer.Completed -= _secondTimer_Completed;
                _secondTimer = null;

                foreach (var item in _boom)
                {
                    ///Удаляем взрыв
                    _map.GameObjectInCall.Add(item.GameObject);
                    (item.GameObject.View as IGameControl).Remove(_map.MapCanvas);
                }
                _boom.Clear();
            }
        }
       

        /// <summary>
        /// Анимация перемещения закончена
        /// </summary>
        void storyboard_Completed(object sender, object e)
        {
            _storyboard.Completed -= storyboard_Completed;
            if (_bullet.Exept == true ||
                _bullet.Range == 0)
            {
                #region Эти 2 метода находятся в пуле
                ///Было попадение, анимация исчезновения стрелы
                //_bullet.GameObject.View.Visibility = Visibility.Collapsed;
                //_map.MapCanvas.Children.Remove(_bullet.GameObject.View);

                (_bullet.GameObject.View as IGameControl).Remove(_map.MapCanvas);
                #endregion
                ///Было попадение стрела исчезает
                ///Удаляем из масива всех объектов
                _map.GameObjectInCall.Remove(_bullet.GameObject);

                if (_map.Calls.Any(p => p.IndexLeft == _bullet.PositionX &&
                    p.IndexTop == _bullet.PositionY))
                {
                    Map_Cell newCall = _map.Calls.Single(p => p.IndexLeft == _bullet.PositionX &&
                p.IndexTop == _bullet.PositionY);
                    newCall.Bullet.Remove(_bullet);
                }

                if (!_boomTrue)///Если взрыва еще не было
                BoomMethod(_bullet.PositionX, _bullet.PositionY);
            }
            else
            {
                _bullet.Range--;
                ///Стрела летит дальше попадения не было
                BulletMuve(_bullet);
            }

            if (E_TestEvent != null)
                E_TestEvent("Storyboard_Completed", null);
        }

        /// <summary>
        /// Изучить способность хоть один раз
        /// </summary>
        public void UpSpell(Player player)
        {
            LevelCast += 1;
            ImageTile.UpSpell();
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
            if (_storyboard != null)
                _storyboard.Pause();

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
            if (_storyboard != null)
                _storyboard.Resume();

            if (PausedEvent != null)
                PausedEvent(this, null);
        }
        #endregion

        public int ManaCost
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
        public int HelthCost
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
        public double Culdaun
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}
