using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Voins.AppCode;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;

namespace Voins.Spell
{
    public class SP_Move : ISpell, IMoveSpell
    {
        /// <summary>
        /// Событие для тестирования спелов
        /// </summary>
        public event EventHandler E_TestEvent;
        public event EventHandler StartUseSpell;
        public event EventHandler CompletedUseSpell;
        SpellDescriptionInfo _spellDescriptionInfo = new SpellDescriptionInfo();
        public SpellDescriptionInfo SpellDescriptionInfo { get { return _spellDescriptionInfo; } set { _spellDescriptionInfo = value; } }

        IUnit _unit;
        Map _map;
        int _xNew = 0;
        int _yNew = 0;
        /// <summary>
        /// Запомним что объект был инвизером
        /// </summary>
        bool _invisMuve;
        public string Name { get; set; }
        public int ManaCost { get; set; }
        public int HelthCost { get; set; }
        double _culdaun = double.MaxValue;
        public double Culdaun { get { return _culdaun; } set { _culdaun = value; } }

        double _duration = 0;
        public double Duration { get { return _duration; } set { _duration = value; } }


        bool _culdaunBool;
        public bool CuldaunBool { get { return _culdaunBool; } set { _culdaunBool = value; } }
        int _levelCast = 1;
        public int LevelCast { get { return _levelCast; } set { _levelCast = value; } }
        int _maxLevelCast = 1;
        public int MaxLevelCast { get { return _maxLevelCast; } set { _maxLevelCast = value; } }
        bool _isUlt = false;
        public bool IsUlt { get { return _isUlt; } set { _isUlt = value; } }
        UC_View_ImageTileControl _imageTile;
        public UC_View_ImageTileControl ImageTile { get { return _imageTile; } set { _imageTile = value; } }

        bool _rotation = false;
        public bool Rotation { get { return _rotation; } set { _rotation = value; } }

        public Storyboard _firstTimer;
        public Storyboard _secondTimer;
        Storyboard _storyboard;

        public SP_Move()
        {
            _imageTile = new UC_View_ImageTileControl("SP_Move", this);
        }

        /// <summary>
        /// Перемещение юнита
        /// </summary>
        /// <param name="map">Карта</param>
        /// <param name="obj">нет</param>
        /// <param name="unit">Кто перемещается</param>
        /// <param name="property"></param>
        public void UseSpall(Map map, Game_Object_In_Call obj, IUnit unit, object property)
        {
            UnitGenerator.UpdatePlayerView(unit);
            if (unit.UnitFrozen == false && !CuldaunBool)
            {
                CuldaunBool = true;
                EAngel ang = (EAngel)property;
                _map = map;
                _unit = unit;
                ///Проверим в какую сторону смотрит юнит, может его нужно просто развернуть
                ///а не перемещать
                if (unit.Angel == ang)
                {///Можно перемещать 
                    Rotation = false;

                    int xNew = 0;
                    int yNew = 0;
                    if (unit.Angel == EAngel.Left)
                    {
                        xNew = unit.PositionX - 1;
                        yNew = unit.PositionY;
                    }
                    else if (unit.Angel == EAngel.Right)
                    {
                        xNew = unit.PositionX + 1;
                        yNew = unit.PositionY;
                    }
                    else if (unit.Angel == EAngel.Top)
                    {
                        xNew = unit.PositionX;
                        yNew = unit.PositionY - 1;
                    }
                    else if (unit.Angel == EAngel.Bottom)
                    {
                        xNew = unit.PositionX;
                        yNew = unit.PositionY + 1;
                    }


                    ///Теперь проверка можно ли переместится в данную ячейку
                    if (map.AllowMoveUnit(unit, xNew, yNew))
                    {   ///Можно
                        _invisMuve = unit.Invisibility;

                        ///Отмечаем что в заданную ячейку идет перемещение юнита
                        map.Calls.Single(p => p.IndexLeft == xNew && p.IndexTop == yNew).Using = true;
                        //  unit.UnitFrozen = true;
                        _xNew = xNew;
                        _yNew = yNew;

                        ///Сохраняем ячейку с которой был выполнен переход
                        UnitGenerator.EditWay(map.Calls.Single(p => p.IndexLeft == unit.PositionX &&
                                             p.IndexTop == unit.PositionY), _unit);

                        ///Запуск анимации перемещения
                        StartStoryboard(unit, xNew, yNew);
                    }
                    else
                    {
                        CuldaunBool = false;

                        if (CompletedUseSpell != null)
                            CompletedUseSpell(this, null);
                    }
                }
                else
                {///Только развернуть 
                    IGameControl contrGame = unit.GameObject.View as IGameControl;
                    unit.Angel = ang;
                    contrGame.ChengAngel(ang);
                    Rotation = true;

                    CuldaunBool = false;

                    if (CompletedUseSpell != null)
                        CompletedUseSpell(this, null);
                }
            }
            else if (CompletedUseSpell != null)
                CompletedUseSpell(this, null);
        }

        private void StartStoryboard(IUnit unit, int xNew, int yNew)
        {
            string animationType = "(Canvas.Left)";
            double mouvePos = 0;
            ///Новая позиция
            double newPos = 0;

            if (unit.Angel == EAngel.Left || unit.Angel == EAngel.Right)
            {
                animationType = "(Canvas.Left)";
                mouvePos = Canvas.GetLeft(unit.GameObject.View);
                newPos = xNew;
            }
            else
            {
                animationType = "(Canvas.Top)";
                mouvePos = Canvas.GetTop(unit.GameObject.View);
                newPos = yNew;
            }

            _storyboard = new Storyboard();
            var animation = new DoubleAnimation
            {

                From = mouvePos,
                To = newPos * 50,
                Duration = TimeSpan.FromSeconds(1 * unit.Speed)
            };
            _storyboard.Children.Add(animation);
            Storyboard.SetTargetProperty(animation, animationType);
            Storyboard.SetTarget(_storyboard, unit.GameObject.View);
            _storyboard.Completed += storyboard_Completed;
            _storyboard.Begin();

            ///Спрайты движения
            IAnimationControl muveAnimation = _unit.GameObject.View as IAnimationControl;
            if (muveAnimation != null)
                muveAnimation.StartMuveAnimation(unit.Speed);

            ///Таймер половины перемещения юнита
            ///Необходим чтобы поставить новую ячейку useD
            _firstTimer = new Storyboard() { Duration = TimeSpan.FromSeconds((1 * unit.Speed) / 2) };
            _firstTimer.Completed += _firstTimer_Completed;
            _firstTimer.Begin();

            if (Paused)
                Pause();
        }

        void _firstTimer_Completed(object sender, object e)
        {
            _firstTimer.Completed -= _firstTimer_Completed;
            _firstTimer = null;
            //  if (_unit is Player)
            //   _unit = _unit;
            ///Так как это юнит мы убираем флаг юзед со старой ячейку
            var callOld = _map.Calls.Single(p => p.IndexLeft == _unit.PositionX && p.IndexTop == _unit.PositionY);
            ///Удаляем из старой ячейки ссылку на юнит
            callOld.IUnits.Remove(_unit);

            if (!callOld.IUnits.Any(p => p.GameObject.EnumCallType == EnumCallType.UnitBlock))
            {
                ///Делаем ячейку пустой
                callOld.Used = false;
            }

            _unit.PositionX = _xNew;
            _unit.PositionY = _yNew;
            ///Получаем новую ячейку
            var callNew = _map.Calls.Single(p => p.IndexLeft == _unit.PositionX && p.IndexTop == _unit.PositionY);

            if (!_unit.Dead)
            {
                //if (_unit is Player)
                //    callNew = callNew;

                ///Добавляем в нее ссылку на юнит
                callNew.IUnits.Add(_unit);
                ///Убераем то что колонка использывалась, теперь ставим что она все время используется
                callNew.Used = true;
            }
            ///Проверим или получил юнит урон в новой ячейке
            foreach (var item in callNew.Bullet)
            {
                if (!item.BuffDemage)
                {
                    if (!item.IsRoket)
                        item.Exept = UnitGenerator.AddDamage(callNew, item);
                    else
                        ///Только попадение, урон не наносится
                        item.Exept = UnitGenerator.AddNullDemage(callNew, item);

                    if (item.Exept)
                        UnitGenerator.AddStuneTwo(callNew, item, item.StunDuration, item.StunDuration);
                }
                else if (!item.IsRoket)///Значит баф урон, и не бомба
                    UnitGenerator.AddBufDemageOne(callNew, item, item.DemageMagic);
            }

            callNew.Using = false;

            Player player = _unit as Player;
            if (player != null && callNew.Item.Count > 0)
            {
                int playerItemCount = player.Items.Count;
                ItemClass addedItem = callNew.Item.FirstOrDefault();

                ///Смотрим можно ли данный итем во что то собрать
                ItemClass newItem = UnitGenerator.CreateNewItem(player, addedItem);
                if (newItem != null)
                {
                    ///Можно собрать
                    ///Разрешаем пропустить итем
                    playerItemCount = 0;
                    addedItem = newItem;
                }

                if (addedItem.Bonus)///Если итем бонус, то пропустить его тоже
                    playerItemCount = 0;

                if (playerItemCount < 4)
                {
                    _map.MapCanvas.Children.Remove(callNew.Item.FirstOrDefault().View);
                    ///Подбираем итем
                    player.AddItem(addedItem);
                    callNew.Item.Remove(callNew.Item.FirstOrDefault());
                }
            }

        }

        void partTimer_Tick(object sender, object e)
        {


            //if (E_TestEvent != null)
            //    E_TestEvent("PartTimer_Tick", null);
        }
        /// <summary>
        /// Анимация перемещения закончена
        /// </summary>
        void storyboard_Completed(object sender, object e)
        {
            _storyboard.Completed -= storyboard_Completed;
            _storyboard = null;

            //_unit.UnitFrozen = false;
            CuldaunBool = false;

            if (CompletedUseSpell != null)
                CompletedUseSpell(this, null);
            //if (E_TestEvent != null)
            //    E_TestEvent("Storyboard_Completed", null);
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

        public void StopMoveSpell()
        {
           //_map.Calls.Single(p => p.IndexLeft == _xNew && p.IndexTop == _yNew).Using = true;
        }
    }
}
