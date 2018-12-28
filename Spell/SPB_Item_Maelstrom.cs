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
    public class SPB_Item_Maelstrom : ISpell
    {
        /// <summary>
        /// Событие для тестирования спелов
        /// </summary>
        public event EventHandler E_TestEvent;
        public event EventHandler StartUseSpell;
        public event EventHandler CompletedUseSpell;
        SpellDescriptionInfo _spellDescriptionInfo = new SpellDescriptionInfo();
        public SpellDescriptionInfo SpellDescriptionInfo { get { return _spellDescriptionInfo; } set { _spellDescriptionInfo = value; } }

        public string Name { get; set; }
        Bullet _bullet;
        IUnit _unit;
        Map _map;
        int _xNew = 0;
        int _yNew = 0;
        /// <summary>
        /// Если попадение и снаряду литеть дальше не нужно
        /// </summary>
        bool _exept = false;
        /// <summary>
        /// Если нужно литеть прямо
        /// </summary>
        bool _exeptHit = false;

        /// <summary>
        /// Угол в который нельзя литеть молнии
        /// </summary>
        EAngel _stopAngels;

        /// <summary>
        /// Сколько попадений максимум может сделать молния
        /// </summary>
        int _hitCount = 2;
        int _hitCountOrij = 2;

        public int HitCount { get { return _hitCountOrij; } set { _hitCountOrij = value; _hitCount = _hitCountOrij; } }

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

        /// <summary>
        /// На сколько увеличить урон после отбивания магии
        /// </summary>
        public int Multiply { get; set; }
        public bool IsSfUlt { get; set; }
        Storyboard _storyboard;
        DoubleAnimation _animation;
        UC_View_ImageTileControl _imageTile;
        public UC_View_ImageTileControl ImageTile { get { return _imageTile; } set { _imageTile = value; } }

        public Storyboard _firstTimer;
        public Storyboard _secondTimer;
        int _moveCount;

        public SPB_Item_Maelstrom()
        {
            _imageTile = new UC_View_ImageTileControl("SPB_Item_Maelstrom", this);
        }

        public SPB_Item_Maelstrom(int viewType)
        {
            if (viewType == 1)
            {
                _imageTile = new UC_View_ImageTileControl("SPB_Nature_Wrath", this);
            }
            else
                _imageTile = new UC_View_ImageTileControl("SPB_Item_Maelstrom", this);
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
            _moveCount++;

            if (IsSfUlt)
            SfUlt(bullArrow.Angel);

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

            ///Проверяем не край экрана ли блок
            Map_Cell newCall = _map.Calls.FirstOrDefault(p => p.IndexLeft == xNew &&
            p.IndexTop == yNew);

            if (_hitCountOrij != _hitCount && _exeptHit)
            {///Если было попадение в цель
                if (newCall != null && !newCall.Block && _stopAngels != _bullet.Angel)
                {
                    _exeptHit = false;
                    ///Запуск анимации перемещения
                    StartStoryboard(bullArrow, xNew, yNew);
                }
                else
                {

                    if (bullArrow.Angel == (EAngel)0)
                        bullArrow.Angel = (EAngel)3;
                    else
                        bullArrow.Angel = _bullet.Angel - 1;

                    bullArrow.DemageMagic += Multiply;


                    BulletMuve(bullArrow);
                }
            }
            else
                StartStoryboard(bullArrow, xNew, yNew);
        }

        private void SfUlt(EAngel angle)
        {
            if (_bullet.Mode == 1 && _moveCount == 3 && (angle == EAngel.Left || angle == EAngel.Right))
                _bullet.Angel = EAngel.Top;

            if (_bullet.Mode == 2 && _moveCount == 3 && (angle == EAngel.Left || angle == EAngel.Right))
                _bullet.Angel = EAngel.Bottom;

            if (_bullet.Mode == 3 && _moveCount == 3 && (angle == EAngel.Left || angle == EAngel.Right))
                _bullet.Angel = EAngel.Left;

            if (_bullet.Mode == 4 && _moveCount == 3 && (angle == EAngel.Left || angle == EAngel.Right))
                _bullet.Angel = EAngel.Right;
        }

        private void StartStoryboard(Bullet bullet, int xNew, int yNew)
        {
            (bullet.GameObject.View as IGameControl).ChengAngel(_bullet.Angel);

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
            _firstTimer = new Storyboard() { Duration = TimeSpan.FromSeconds(bullet.Speed / 2) };
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
            Exept();



            ///Проверяем или молния попала
            if (_bullet.Exept == true)
                _hitCount--;

            Map_Cell newCall = _map.Calls.FirstOrDefault(p => p.IndexLeft == _bullet.PositionX &&
             p.IndexTop == _bullet.PositionY);

            /// Если есть аганим
            if (Name == "SP_Nature_Wrath" && UnitGenerator.HasAghanim(_unit)
                && _bullet.RemoveUnit != null)
                CreateTrent(newCall, _bullet.RemoveUnit);

            if (_hitCount == 0 || newCall == null ||
                ///Или новая ячейка блок
                newCall != null && newCall.Block)
            {
                (_bullet.GameObject.View as IGameControl).Remove(_map.MapCanvas);
                ///Было попадение, анимация исчезновения стрелы
                //_bullet.GameObject.View.Visibility = Visibility.Collapsed;
                ///Было попадение стрела исчезает
                ///Удаляем из масива всех объектов
                _map.GameObjectInCall.Remove(_bullet.GameObject);
                _exept = true;

                //_map.MapCanvas.Children.Remove(_bullet.GameObject.View);
            }
            else
            {
                newCall.Bullet.Add(_bullet);
            }

        }

        private void CreateTrent(Map_Cell newCall, IUnit target)
        {
            if (newCall == null)
                return;

            Unit trent = UnitGenerator.M_Trent(_bullet.PositionX, _bullet.PositionY, _map, _unit as Player);
            _map.CreateObjectUnitInCall(newCall, trent);

            trent.Health += target.MaxHealth / 3;
            trent.Demage += _bullet.TrentDamage;
            trent.Arrmor += _bullet.TrentArmor;

            trent.AI.Farm = true;
            trent.AI.Hunt = true;
            trent.LifeTime(_bullet.LifeTime);

            trent.AI.StartAI();

            _bullet.RemoveUnit = null;
        }

        private void Exept()
        {
            //!_exept
            if (_map.Calls.Any(p => p.IndexLeft == _bullet.PositionX && p.IndexTop == _bullet.PositionY))
            {
                ///Получим ячейку куда попала пуля
                var call = _map.Calls.Single(p => p.IndexLeft == _bullet.PositionX && p.IndexTop == _bullet.PositionY);
                if (!call.Block)
                {
                    if (!_bullet.Exept)
                        _bullet.Exept = UnitGenerator.AddDamage(call, _bullet);
                    else
                        UnitGenerator.AddDamage(call, _bullet);


                    #region IsSfUlt
                    if (IsSfUlt) { 
                    for (int i = 0; i < call.IUnits.Count; i++)
                    {
                        if (call.IUnits[i] != _bullet.UnitUsed &&
                            call.IUnits[i].GroupType != _bullet.UnitUsed.GroupType)
                        {
                            UnitGenerator.SfUlt(_bullet.UnitUsed, call.IUnits[i]);
                        }
                    }
                    }
                    #endregion
                }
                else
                    _bullet.Exept = true;

            }
            else
                _bullet.Exept = true;
        }


        /// <summary>
        /// Анимация перемещения закончена
        /// </summary>
        void storyboard_Completed(object sender, object e)
        {
            _storyboard.Completed -= storyboard_Completed;
            _storyboard = null;

            if (_hitCount == 0 ||
                _bullet.Range == 0 ||
                _exept)
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
            }
            else
            {
                _bullet.Range--;
                if (_bullet.Exept)
                {
                    _exeptHit = true;

                    if (_bullet.Angel == EAngel.Left)
                        _stopAngels = EAngel.Right;
                    else if (_bullet.Angel == EAngel.Right)
                        _stopAngels = EAngel.Left;
                    else if (_bullet.Angel == EAngel.Top)
                        _stopAngels = EAngel.Bottom;
                    else if (_bullet.Angel == EAngel.Bottom)
                        _stopAngels = EAngel.Top;

                    ///Выбераем рандомную сторону куда отобется молния
                    Random rand = new Random((int)DateTime.Now.Ticks);
                    _bullet.Angel = (EAngel)rand.Next(0, 3);
                    _bullet.Exept = false;
                }
                ///Молния летит дальше, у нее не кончелся ренж 
                ///или небыло максимального количества попадений
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
