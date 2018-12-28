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
    public class SP_Sf_Ult : ISpell
    {
        public event EventHandler StartUseSpell;
        public event EventHandler CompletedUseSpell;

        SpellDescriptionInfo _spellDescriptionInfo = new SpellDescriptionInfo()
        {
            Description = "Dual Breath, Culdaun 8 sec. Duration 6 sec., Demage type: physical",
            LevelDescription =
            "Level1: Demage per second - 2, Minus armor - 4, Mana cost - 15" + Environment.NewLine +
            "Level2: Demage per second - 3, Minus armor - 6, Mana cost - 20" + Environment.NewLine +
            "Level3: Demage per second - 4, Minus armor - 8, Mana cost - 25"
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

        int _manaCost = 20;
        public int ManaCost { get { return _manaCost; } set { _manaCost = value; } }

        double _culdaun = 20;
        public double Culdaun { get { return _culdaun; } set { _culdaun = value; } }

        public int HelthCost { get; set; }

        double _speed = 0.3;

        public double Speed { get { return _speed; } set { _speed = value; } }

        double _duration = 1;

        public double Duration { get { return _duration; } set { _duration = value; } }

        int _levelCast = 0;
        public int LevelCast { get { return _levelCast; } set { _levelCast = value; } }
        int _maxLevelCast = 1;
        public int MaxLevelCast { get { return _maxLevelCast; } set { _maxLevelCast = value; } }

        bool _isUlt = true;
        public bool IsUlt { get { return _isUlt; } set { _isUlt = value; } }

        UC_View_ImageTileControl _imageTile;
        public UC_View_ImageTileControl ImageTile { get { return _imageTile; } set { _imageTile = value; } }

        public Storyboard _firstTimer;
        public Storyboard _secondTimer;
        List<Point> _callsPointl;
        List<Bullet> _boom = new List<Bullet>();
        int _damageMagic;
        Bullet _bullDualBreath;
        Player _player;
        Map _map;

        public SP_Sf_Ult()
        {
            _imageTile = new UC_View_ImageTileControl("SP_Sf_Ult", this);
        }

        public void UseSpall(Map map, Game_Object_In_Call obj, IUnit unit, object property)
        {
            bool upSpell = UnitGenerator.UpPlayerSpell(unit, this);
            _unit = unit;
            _player = _unit as Player;
            if (unit.UnitFrozen == false &&
                !_culdaunBool && LevelCast != 0 && !upSpell &&
                !unit.Silenced &&
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
                    _map = map;

                    if (LevelCast == 1)
                    {
                        _damageMagic = 12;
                    }

                    if (_player != null)
                        (_player.GameObject.View as UC_Player).ShowEffect(5, true);

                    _unit.UnitFrozen = true;

                    ///Таймер время кастования ульта
                    _secondTimer = new Storyboard() { Duration = TimeSpan.FromSeconds(Duration) };
                    _secondTimer.Completed += _secondTimer_Completed;
                    _secondTimer.Begin();

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

        private void SoulCreator(EAngel angl, Buff buff)
        {
            CreateBullet(angl, 0, 0.15, buff);

            if (buff.SoulCount == 32)
                CreateBullet(angl, 0, 0.25, buff);

            if (buff.SoulCount >= 20)
            {
                CreateBullet(angl, 1, 0.3, buff);
                CreateBullet(angl, 2, 0.3, buff);
            }
        }

        private void CreateBullet(EAngel angel, int mode, double speed, Buff buff)
        {
            Bullet bullArrow = new Bullet();

            bullArrow.GameObject = new Game_Object_In_Call()
            {
                EnumCallType = EnumCallType.Bullet,
            };

            ///Создаем визуальный объект стрела
            UC_Sf_Ult arrow = new UC_Sf_Ult();
            arrow.ChengAngel(_unit.Angel);
            bullArrow.GameObject.View = arrow;

            bullArrow.UnitUsed = _unit;
            bullArrow.PositionX = _unit.PositionX;
            bullArrow.PositionY = _unit.PositionY;
            bullArrow.Speed = speed;

            if (buff.SoulCount > _damageMagic)
                bullArrow.DemageMagic = buff.SoulCount;
            else
                bullArrow.DemageMagic = _damageMagic;

            //UnitGenerator.MKB_Bush(bullArrow, _unit);
            ///Магический урон зависит от прокача стрел
            //bullArrow.DemageMagic = 5 * (int)property;

            bullArrow.MinusArmor = 3;
            bullArrow.SpeedSlow = 0.3;

            bullArrow.Mode = mode;
            bullArrow.CurrentMap = _map;
            bullArrow.Angel = angel;

            SPB_Item_Maelstrom mel = new SPB_Item_Maelstrom() { Name = "Fly" };
            mel.HitCount = 10;
            mel.IsSfUlt = true;
            bullArrow.Range = 3;

            ///Поведение такое же как у стрел боника
            bullArrow.Spells.Add(mel);

            ///И его же добавим в масив всех объектов
            _map.GameObjectInCall.Add(bullArrow.GameObject);

            Canvas.SetLeft(bullArrow.GameObject.View, bullArrow.PositionX * 50);
            Canvas.SetTop(bullArrow.GameObject.View, bullArrow.PositionY * 50);
            ///Отображение
            _map.MapCanvas.Children.Add(bullArrow.GameObject.View);

            bullArrow.UseSpall("Fly");
        }


        /// <summary>
        /// Таймер исчезновения взрыва
        /// </summary>
        void _secondTimer_Completed(object sender, object e)
        {
            if (!_unit.Dead &&
                !_unit.Silenced &&
                !_unit.Hexed)
            {
                _secondTimer.Completed -= _secondTimer_Completed;
                _secondTimer = null;

                ///Тут кординаты ячеек в которых действует тучка
                _callsPointl = new List<Point>();
                Buff buff = _unit.Buffs.FirstOrDefault(p => p.Name == "SoulCount");

                if (buff != null)
                {
                   

                    ///Получаем ячейки которые находятся перед героем
                    ///использывавшим тучку
                    ///Сначала добавляем ячеку с героем
                    int xNew = _unit.PositionX;
                    int yNew = _unit.PositionY;

                    xNew = xNew - 1;
                    Map_Cell call = _map.Calls.FirstOrDefault(p => p.IndexLeft == xNew && p.IndexTop == yNew);
                    if (call != null && !call.Block)
                    {
                        SoulCreator(EAngel.Left, buff);
                    }

                    xNew = _unit.PositionX;
                    yNew = _unit.PositionY;
                    xNew = xNew + 1;
                    Map_Cell callR = _map.Calls.FirstOrDefault(p => p.IndexLeft == xNew && p.IndexTop == yNew);
                    if (callR != null && !callR.Block)
                    {
                        SoulCreator(EAngel.Right, buff);
                    }

                    xNew = _unit.PositionX;
                    yNew = _unit.PositionY;
                    yNew = yNew - 1;
                    Map_Cell callT = _map.Calls.FirstOrDefault(p => p.IndexLeft == xNew && p.IndexTop == yNew);
                    if (callT != null && !callT.Block)
                    {
                        SoulCreator(EAngel.Top, buff);
                    }

                    xNew = _unit.PositionX;
                    yNew = _unit.PositionY;
                    yNew = yNew + 1;
                    Map_Cell callB = _map.Calls.FirstOrDefault(p => p.IndexLeft == xNew && p.IndexTop == yNew);
                    if (callB != null && !callB.Block)
                    {
                        SoulCreator(EAngel.Bottom, buff);
                    }
                }
            }

            if (_player != null)
                (_player.GameObject.View as UC_Player).ShowEffect(5, false);
            _unit.UnitFrozen = false;
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
