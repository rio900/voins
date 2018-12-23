using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;

namespace Voins.AppCode
{
    public class Unit : IUnit
    {
        private EUnitType _unitType = EUnitType.Null;

        public EUnitType UnitType
        {
            get { return _unitType; }
            set { _unitType = value; }
        }


        public Unit()
        {
            StartBuffTimer();
        }
        public event GetDemageDelegate GetDemageEvent;
        public event EventHandler DeadEvent;

        Storyboard _buffTimer;
        Storyboard _manaTimer;
        Storyboard _healthTimer;
        Storyboard _lifeTimer;

        public Map CurrentMap { get; set; }

        private List<Buff> _buffs = new List<Buff>();
        /// <summary>
        /// Позитивные и негативные воздействия на героя
        /// </summary>
        public List<Buff> Buffs { get { return _buffs; } set { _buffs = value; } }

        private int _health;
        public int Health
        {
            get { return _health; }
            set { _health = value; }
        }
        private int _maxHealth;
        public int MaxHealth
        {
            get { return _maxHealth; }
            set
            {
                _maxHealth = value;
                if (Health > _maxHealth)
                    Health = _maxHealth;
            }
        }
        private bool _dead;

        public bool Dead
        {
            get { return _dead; }
            set { _dead = value; }
        }

        #region Награды за убийство

        private ItemClass _nItem;
        public ItemClass NItem
        {
            get { return _nItem; }
            set { _nItem = value; }
        }

        private int _nGold;
        public int NGold
        {
            get { return _nGold; }
            set { _nGold = value; }
        }

        private int _nExp;
        public int NExp
        {
            get { return _nExp; }
            set { _nExp = value; }
        }
        #endregion

        private int _orijHealth;
        public int OrijHealth
        {
            get { return _orijHealth; }
            set { _orijHealth = value; }
        }

        private int _mana;
        public int Mana
        {
            get { return _mana; }
            set { _mana = value; }
        }
        private int _orijMana;
        public int OrijMana
        {
            get { return _orijMana; }
            set { _orijMana = value; }
        }
        private int _maxMana;
        public int MaxMana
        {
            get { return _maxMana; }
            set { _maxMana = value; }
        }

        private int _orijDemage;
        public int OrijDemage
        {
            get { return _orijDemage; }
            set { _orijDemage = value; }
        }
        private int _demage;
        public int Demage
        {
            get { return _demage; }
            set { _demage = value; }
        }

        private double _orijSpeed;
        public double OrijSpeed
        {
            get { return _orijSpeed; }
            set { _orijSpeed = value; }
        }

        private double _orijHealthRegeneration = 1;
        /// <summary>
        /// Оригинальный реген
        /// 1 хп за сикунду
        /// </summary>
        public double OrijHealthRegeneration
        {
            get { return _orijHealthRegeneration; }
            set { _orijHealthRegeneration = value; }
        }

        private double _healthRegeneration;
        /// <summary>
        /// Реген который прибавляется
        /// 1 хп за сикунду
        /// </summary>
        public double HealthRegeneration
        {
            get { return _healthRegeneration; }
            set { _healthRegeneration = value; }
        }

        private int _orijManaRegeneration = 5;

        public void LifeTime(int time)
        {
            ///Регенерация проходит в единицах
            _lifeTimer = new Storyboard() { Duration = TimeSpan.FromSeconds(time) };
            _lifeTimer.Completed += lifeTimer_Completed;
            _lifeTimer.Begin();
        }

        private void lifeTimer_Completed(object sender, object e)
        {
           GatDamage(Health * 2, 0, 0, null);
        }

        /// <summary>
        /// Оригинальный реген маны
        /// 5
        /// </summary>
        public int OrijManaRegeneration
        {
            get { return _orijManaRegeneration; }
            set { _orijManaRegeneration = value; }
        }

        private int _manaRegeneration = 5;
        /// <summary>
        /// Оригинальный реген маны
        /// оригинальный + итемы
        /// </summary>
        public int ManaRegeneration
        {
            get { return _manaRegeneration; }
            set { _manaRegeneration = value; }
        }

        private int _range = 1;
        /// <summary>
        /// Дальность нанесения урона
        /// </summary>
        public int Range
        {
            get { return _range; }
            set { _range = value; }
        }

        private bool _invulnerability;
        // <summary>
        ///Неуязвимость
        /// </summary>
        public bool Invulnerability
        {
            get { return _invulnerability; }
            set { _invulnerability = value; }
        }
        private int _kills = 0;
        /// <summary>
        /// Убийств
        /// </summary>
        public int Kills
        {
            get { return _kills; }
            set { _kills = value; }
        }
        private int _death = 0;
        /// <summary>
        /// Смертей
        /// </summary>
        public int Death
        {
            get { return _death; }
            set { _death = value; }
        }
        private int _asists = 0;
        /// <summary>
        /// Помощей
        /// </summary>
        public int Asists
        {
            get { return _asists; }
            set { _asists = value; }
        }

        private double _attackSpeed = 1.0;
        public double AttackSpeed
        {
            get { return _attackSpeed; }
            set { _attackSpeed = value; }
        }

        private double _orijAttackSpeed = 1.0;
        public double OrijAttackSpeed
        {
            get { return _orijAttackSpeed; }
            set { _orijAttackSpeed = value;
            _attackSpeed = _orijAttackSpeed;
            }
        }
        private double _speed = 1;
        /// <summary>
        /// Множетель на одну сикунду
        /// стандартный 1
        /// </summary>
        public double Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }

        private int _positionX;

        public int PositionX
        {
            get { return _positionX; }
            set { _positionX = value; }
        }

        private int _positionY;

        public int PositionY
        {
            get { return _positionY; }
            set { _positionY = value; }
        }

        private List<Map_Cell> _way = new List<Map_Cell>();
        /// <summary>
        /// Список ячеек в которых прежде был юнит
        /// </summary>
        public List<Map_Cell> Way
        {
            get { return _way; }
            set { _way = value; }
        }

        private int _arrmor = 0;
        public int Arrmor
        {
            get { return _arrmor; }
            set { _arrmor = value; }
        }

        private int _magicArrmor = 0;
        public int MagicArrmor
        {
            get { return _magicArrmor; }
            set { _magicArrmor = value; }
        }

        private List<ISpell> _spells = new List<ISpell>();
        /// <summary>
        /// Список способностей юнита
        /// </summary>
        public List<ISpell> Spells { get { return _spells; } set { _spells = value; } }

        private Game_Object_In_Call _gameObject;

        public Game_Object_In_Call GameObject
        {
            get { return _gameObject; }
            set { _gameObject = value; }
        }
        private EAngel _angel;
        /// <summary>
        /// Угол поворота юнита
        /// </summary>
        public EAngel Angel
        {
            get { return _angel; }
            set { _angel = value; }
        }

        private IAI _aI;
        /// <summary>
        /// Искуствленный интелект
        /// </summary>
        public IAI AI
        {
            get { return _aI; }
            set { _aI = value; }
        }


        private bool _unitFrozen;
        /// <summary>
        /// Юнит использует какой то спел и не может воспринимать команды
        /// на другие спелы
        /// </summary>
        public bool UnitFrozen
        {
            get { return _unitFrozen; }
            set
            {
                _unitFrozen = value;
            }
        }



        private bool _isUnitStun;
        /// <summary>
        /// Стан
        /// </summary>
        public bool IsUnitStun
        {
            get
            {
                return _isUnitStun;
            }
            set
            {
                _isUnitStun = value;

                _unitFrozen = _isUnitStun;
                if (_unitFrozen)
                {
                    foreach (var item in Spells)
                        item.Pause();
                }
                else
                {
                    foreach (var item in Spells)
                        item.StopPause();
                }

            }
        }

        private bool _silenced;
        public bool Silenced
        {
            get { return _silenced; }
            set
            {
                _silenced = value;
                (GameObject.View as IGameControl).Silenced(_silenced);
            }
        }

        private bool _hexed;
        public bool Hexed
        {
            get { return _hexed; }
            set
            {
                _hexed = value;
                (GameObject.View as IGameControl).Hexed(_hexed);
            }
        }

        private bool _invisibility;
        public bool Invisibility
        {
            get { return _invisibility; }
            set { _invisibility = value; }
        }

        private int _demageStart;
        public int DemageStart
        {
            get { return _demageStart; }
            set { _demageStart = value; }
        }
        private int _demageItem;
        public int DemageItem
        {
            get { return _demageItem; }
            set { _demageItem = value; }
        }

        private int _groupType;
        public int GroupType
        {
            get { return _groupType; }
            set { _groupType = value; }
        }

        private int _nGamePoint = 0;
        /// <summary>
        /// Награда в очках за убийство игрока
        /// </summary>
        public int NGamePoint
        {
            get { return _nGamePoint; }
            set
            {
                _nGamePoint = value;
            }
        }
        /// <summary>
        /// Использование способности
        /// </summary>
        /// <param name="name">Название способности</param>
        /// <param name="obj">Уникальные параметры, могут отсуцтвовать</param>
        public void UseSpall(string name, object obj)
        {
            var spall = Spells.Single(p => p.Name == name);
            spall.UseSpall(CurrentMap, null, this, obj);
        }

        /// <summary>
        /// Получение урона
        /// </summary>
        /// <param name="phisic">физический</param>
        /// <param name="magic">магический</param>
        /// <param name="pure">чистый</param>
        public IUnit GatDamage(int phisic, int magic, int pure, IUnit demagedUnit)
        {
            Player playerDemaged = demagedUnit as Player;

            int magicDemagePlus = magic;
            ///Действие аганима
            if (playerDemaged != null)
            {
                foreach (var item in playerDemaged.Items)
                    if (item.BonusMagicDemage > 0)
                        magicDemagePlus = magicDemagePlus + (int)(magic * item.BonusMagicDemage);
            }

            int demage = StaticVaribl.DemageAndArmor(phisic, Arrmor) + StaticVaribl.DemageAndArmor(magicDemagePlus, MagicArrmor) + pure;
            Health -= demage;

            IUnitControl chpView = GameObject.View as IUnitControl;
            if (chpView != null)
                chpView.ShowHealth(Health, MaxHealth);

            if (GetDemageEvent != null)
                GetDemageEvent(StaticVaribl.DemageAndArmor(phisic, Arrmor),
                    StaticVaribl.DemageAndArmor(magicDemagePlus, MagicArrmor),
                    pure, this);

            IGameControl control = (this.GameObject.View as IGameControl);
            if (control != null)
                ///Отображаем сколько урона получил юнит
                control.GetDemage("-" + demage);

            if (Health <= 0)
            {///Значит объект унечтожен 
                return RemoveUnit(demagedUnit);
            }
            return null;
        }

        public IUnit RemoveUnit(IUnit demagedUnit)
        {
            Dead = true;
            IsUnitStun = false;

            IUnit returnUnit = null;

            Player player = demagedUnit as Player;

            if (player == null && demagedUnit is Unit)
                player = (demagedUnit as Unit).MasterPlayer;

            if (player != null)
            {
                player.GetAward(this);

                if (this.GameObject.EnumCallType == EnumCallType.UnitBlock)
                    player.StatisticData.BlockKills += 1;
                if (this.GameObject.EnumCallType == EnumCallType.Unit)
                    player.StatisticData.MobKills += 1;
            }

            var call = CurrentMap.Calls.Single(p => p.IndexLeft == PositionX && p.IndexTop == PositionY);
            call.IUnits.Remove(this);
            if (!call.IUnits.Any())
                call.Used = false;
            CurrentMap.GameObjectInCall.Remove(this.GameObject);

            #region Отображение предмета на месте убитого юнита
            if (this.NItem != null)
            {
                ItemClass item = this.NItem;
                call.Item.Add(item);
                item.PositionX = PositionX;
                item.PositionY = PositionY;
                item.ItemPosition();
                item.View.ApplayMargin(new Thickness(8));
                CurrentMap.MapCanvas.Children.Add(item.View);

                ///Тут будет анимироватся
                CurrentMap.MapCanvas.Children.Remove(this.GameObject.View);
                returnUnit = this;
            }
            #endregion
            if (DeadEvent != null)
                DeadEvent(this, null);

            if (this.GameObject.EnumCallType == EnumCallType.UnitBlock &&
                CurrentMap.BlockBonusGame &&
                player != null)
            {
                int bonusGamePoint = CurrentMap.CheckNumber(this);
                player.PlayerGamePoint = NGamePoint + bonusGamePoint;

            }
            // else
            ///Тут будет анимироватся
            CurrentMap.MapCanvas.Children.Remove(this.GameObject.View);

            foreach (var item in this.Spells)
            {
                IMoveSpell itemMove = item as IMoveSpell;
                if (itemMove != null)
                    itemMove.StopMoveSpell();
            }

            if (this.AI != null)
            {
                this.AI.StopAI();
                this.AI = null;
            }
            DeleteUnit();

            return returnUnit;
        }

        /// <summary>
        /// Таймеры регенерации хп и маны
        /// </summary>
        public void StartRegenerationTimers_NewObject()
        {
            ///Регенерация проходит в единицах
            _manaTimer = new Storyboard() { Duration = TimeSpan.FromSeconds(StaticVaribl.ManaRegenerationConstant) };
            _manaTimer.Completed += manaTimer_Tick;
            _manaTimer.Begin();

            ///Регенерация здоровъя в единицах
            _healthTimer = new Storyboard() { Duration = TimeSpan.FromSeconds(StaticVaribl.HpRegenerationConstant) };
            _healthTimer.Completed += _healthTimer_Tick;
            _healthTimer.Begin();
        }

        void _healthTimer_Tick(object sender, object e)
        {
            Health = Health + (int)HealthRegeneration;
            _healthTimer.Begin();
        }

        void manaTimer_Tick(object sender, object e)
        {
            Mana = Mana + ManaRegeneration;
            _manaTimer.Begin();
        }

        public void StopRegenerationTimer()
        {
            if (_manaTimer != null)
                _manaTimer.Pause();
            if (_healthTimer != null)
                _healthTimer.Pause();
        }

        public void StartRegenerationTimer()
        {
            if (_manaTimer != null)
                _manaTimer.Resume();
            if (_healthTimer != null)
                _healthTimer.Resume();
        }

        /// <summary>
        /// Каждую сикунду этот таймер проверяет какие воздействия влияют на юнит
        /// </summary>
        public void StartBuffTimer()
        {
            _buffTimer = new Storyboard() { Duration = TimeSpan.FromSeconds(0.1) };
            _buffTimer.Completed += _buffTimer_Tick;
            _buffTimer.Begin();
        }

        void _buffTimer_Tick(object sender, object e)
        {
            for (int i = 0; i < Buffs.Count; i++)
            {
                if (!Buffs[i].Passive)
                {///Бафы которые длятся все время(пасивки)
                    if (Buffs[i].Duration < 0)
                    {
                        StopAndRemoveBuff(Buffs[i]);
                    }
                    else
                    {
                        ///Если баф еще не влияет на данный юнит
                        if (!Buffs[i].BuffUsed)
                        {
                            BuffStartUse(Buffs[i]);
                        }
                        Buffs[i].Duration -= 0.1;
                    }
                }
            }
            if (_buffTimer != null)
                _buffTimer.Begin();
        }

        private void StopAndRemoveBuff(Buff currentBuff)
        {
            if (currentBuff.BuffUsed)
            {
                Arrmor += currentBuff.MinusArmor;
                Arrmor -= currentBuff.Armor;
                currentBuff.BuffUsed = false;
                OrijAttackSpeed -= currentBuff.AttackSpeedSlow;
                OrijAttackSpeed += currentBuff.AttackSpeed;
                Speed = Speed - currentBuff.SpeedSlow;
            }
            Buffs.Remove(currentBuff);///Время действия бафа истекло

            if (!Buffs.Any(p => p.Stun))///Если на юнит не воздействуют другие станы
                IsUnitStun = false;
        }
        public void UseBuff(Buff buff)
        {
            BuffStartUse(buff);
            Buffs.Add(buff);
        }
        private void BuffStartUse(Buff buff)
        {
            buff.BuffUsed = true;

            Arrmor -= buff.MinusArmor;
            Arrmor += buff.Armor;

            if (buff.Stun)
                IsUnitStun = true;

            Speed += buff.SpeedSlow;
            OrijAttackSpeed += buff.AttackSpeedSlow;
            OrijAttackSpeed -= buff.AttackSpeed;
            if (buff.DemageMagic > 0)
                GatDamage(0, buff.DemageMagic, 0, buff.DemagedUnit);
        }

        /// <summary>
        /// Удалить баф по имени
        /// </summary>
        /// <param name="buff">Название бафа</param>
     public   void RemoveBuff(string buff)
        {
            Buff buffRemov = Buffs.FirstOrDefault(p => p.Name == buff);
            StopAndRemoveBuff(buffRemov);
        }

        #region Pause
        public event EventHandler PausedEvent;
        bool _paused;
        public bool Paused { get { return _paused; } set { _paused = value; } }

        public Player MasterPlayer { get; internal set; }

        public void Pause()
        {
            Paused = true;
            foreach (var item in Spells)
                item.Pause();

            if (_buffTimer != null)
                _buffTimer.Pause();

            if (_lifeTimer != null)
                _lifeTimer.Pause();

            if (AI != null)
                AI.Pause();

            StopRegenerationTimer();

            if (PausedEvent != null)
                PausedEvent(this, null);
        }
        public void StopPause()
        {

            Paused = false;

            foreach (var item in Spells)
                item.StopPause();
            if (_buffTimer != null)
                _buffTimer.Resume();

            if (_lifeTimer != null)
                _lifeTimer.Resume();

            if (AI != null)
                AI.StopPause();
            StartRegenerationTimer();
            if (PausedEvent != null)
                PausedEvent(this, null);
        }

        #endregion

        public void DeleteUnit()
        {
            if (_manaTimer != null)
                _manaTimer.Stop();
            _manaTimer = null;
            if (_healthTimer != null)
                _healthTimer.Stop();
            _healthTimer = null;
            if (_buffTimer != null)
                _buffTimer.Stop();
            _buffTimer = null;

            if (_lifeTimer != null)
                _lifeTimer.Stop();
            _lifeTimer = null;
            
        }
    }
}
