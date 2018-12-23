using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Voins;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;

namespace Voins.AppCode
{
    public class Player : IUnit
    {
        private EUnitType _unitType = EUnitType.Player;

        public EUnitType UnitType
        {
            get { return _unitType; }
            set { _unitType = value; }
        }

        public Player()
        {
            StartBuffTimer();
        }
        Storyboard _manaTimer;
        Storyboard _healthTimer;
        Storyboard _respaumTimer;
        Storyboard _inviInvulnerabilityTimer;
        Storyboard _buffTimer;

        public event GetDemageDelegate GetDemageEvent;

        public event EventHandler UpdatePlayerData;
        public event EventHandler UpdateItemPlayerData;
        public event EventHandler LevelUpEvent;
        public event EventHandler ShowShop;
        public event EventHandler DeadEvent;
        /// <summary>
        /// Заклинание было изучено
        /// (Срабатывает когда игрок израсходывает UpPoint)
        /// </summary>
        public event EventHandler SpellUpedEvent;


        private StatisticData _statisticData;
        /// <summary>
        /// Статистика
        /// </summary>
        public StatisticData StatisticData
        {
            get { return _statisticData; }
            set
            {
                _statisticData = value;
            }
        }


        public Map CurrentMap { get; set; }

        private int _respaumTime = 10;
        /// <summary>
        /// Время воскрешения игрока
        /// </summary>
        public int RespaumTime
        {
            get { return _respaumTime; }
            set
            {
                _respaumTime = value;
            }
        }

        private string _previousSkill;
        /// <summary>
        /// Способность которая была прокачана в предыдущий раз
        /// название
        /// </summary>
        public string PreviousSkill
        {
            get { return _previousSkill; }
            set
            {
                _previousSkill = value;
            }
        }


        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private bool _allowUpSpell;
        /// <summary>
        /// Разришена прокачка заклинания с клавиатуры
        /// </summary>
        public bool AllowUpSpell
        {
            get { return _allowUpSpell; }
            set { _allowUpSpell = value; }
        }

        private bool _invisibility;
        public bool Invisibility
        {
            get { return _invisibility; }
            set { _invisibility = value; }
        }

        private int _health;
        public int Health
        {
            get { return _health; }
            set
            {
                _health = value;
                if (_health > MaxHealth)
                    _health = MaxHealth;
            }
        }
        private int _maxHealth;
        public int MaxHealth
        {
            get { return OrijHealth + Strength * StaticVaribl.StrConstant; }
            set { _maxHealth = value; }
        }

        private int _orijHealth;
        /// <summary>
        /// Здоровъе которое прибавляется предметами
        /// </summary>
        public int OrijHealth
        {
            get { return _orijHealth; }
            set { _orijHealth = value; }
        }

        private int _mana;
        public int Mana
        {
            get { return _mana; }
            set
            {
                _mana = value;
                if (_mana > MaxMana)
                    _mana = MaxMana;
            }
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
            get { return OrijMana + Intelligence * StaticVaribl.IntConstant; }
            set { _maxMana = value; }
        }

        private int _orijDemage;
        public int OrijDemage
        {
            get { return _orijDemage; }
            set { _orijDemage = value; }
        }

        private double _orijSpeed = 1.0;
        public double OrijSpeed
        {
            get { return _orijSpeed; }
            set
            {
                    _orijSpeed = value;
                    Speed = _orijSpeed;
            }
        }
        private int _arrmor = 0;
        public int Arrmor
        {
            get { return ItemArrmor + Agility / StaticVaribl.AgilConstant; }
            set { _arrmor = value; }
        }

        private int _itemArrmor = 0;
        public int ItemArrmor
        {
            get { return _itemArrmor; }
            set { _itemArrmor = value; }
        }

        private int _range = 5;
        /// <summary>
        /// Дальность нанесения урона
        /// </summary>
        public int Range
        {
            get { return _range; }
            set { _range = value; }
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

        private int _magicArrmor = 0;
        public int MagicArrmor
        {
            get { return _magicArrmor; }
            set { _magicArrmor = value; }
        }

        private double _speed = 1;
        /// <summary>
        /// Множетель на одну секунду
        /// стандартный 1
        /// </summary>
        public double Speed
        {
            get {
                if (_speed > StaticVaribl.SpeedMaximum)
                    return _speed;
                else
                    return StaticVaribl.SpeedMaximum;
            }
            set { _speed = value; }
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

        private double _healthRegeneration = 1;
        /// <summary>
        /// Реген который прибавляется
        /// 1 хп за сикунду
        /// </summary>
        public double HealthRegeneration
        {
            get { return OrijHealthRegeneration + (int)(Strength / StaticVaribl.StrRegenConstant); }
            set { _healthRegeneration = value; }
        }

        private int _orijManaRegeneration = 3;
        /// <summary>
        /// Оригинальный реген маны
        /// 5 
        /// </summary>
        public int OrijManaRegeneration
        {
            get { return _orijManaRegeneration; }
            set { _orijManaRegeneration = value; }
        }

        private int _manaRegeneration;
        /// <summary>
        /// Оригинальный реген маны
        /// оригинальный + итемы
        /// </summary>
        public int ManaRegeneration
        {
            get { return OrijManaRegeneration + (int)(Intelligence / StaticVaribl.IntRegenConstant); }
            set { _manaRegeneration = value; }
        }

        private double _attackSpeed = 1.00;
        public double AttackSpeed
        {
            get
            {
                if (Range > 1)
                {
                    if (_attackSpeed > StaticVaribl.AttackSpeedMaximum)
                        return _attackSpeed;
                    else
                        return StaticVaribl.AttackSpeedMaximum;
                }
                else
                {
                    if (_attackSpeed > StaticVaribl.AttackSpeedMeleMaximum)
                        return _attackSpeed;
                    else
                        return StaticVaribl.AttackSpeedMeleMaximum;
                }
            }
            set { _attackSpeed = value; }
        }

        private double _orijAttackSpeed = 1.0;
        public double OrijAttackSpeed
        {
            get { return _orijAttackSpeed; }
            set
            {
                _orijAttackSpeed = value;
                AttackSpeed = _orijAttackSpeed;
            }
        }

        private int _positionX;

        public int PositionX
        {
            get { return _positionX; }
            set { _positionX = value; }
        }

        private bool _dead;

        public bool Dead
        {
            get { return _dead; }
            set { _dead = value; }
        }

        private bool _allowRespaum = true;

        public bool AllowRespaum
        {
            get { return _allowRespaum; }
            set { _allowRespaum = value; }
        }

        private int _positionY;

        public int PositionY
        {
            get { return _positionY; }
            set { _positionY = value; }
        }

        private List<ISpell> _spells = new List<ISpell>();
        /// <summary>
        /// Список способностей юнита
        /// </summary>
        public List<ISpell> Spells { get { return _spells; } set { _spells = value; } }

        private List<Buff> _buffs = new List<Buff>();
        /// <summary>
        /// Позитивные и негативные воздействия на героя
        /// </summary>
        public List<Buff> Buffs { get { return _buffs; } set { _buffs = value; } }

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


        private bool _invulnerability;
        /// <summary>
        ///Неуязвимость
        /// </summary>
        public bool Invulnerability
        {
            get { return _invulnerability; }
            set { _invulnerability = value; }
        }


        private int _playerNumber;
        /// <summary>
        /// Номер игрока
        /// </summary>
        public int PlayerNumber
        {
            get { return _playerNumber; }
            set { _playerNumber = value; }
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


        private int _level = 1;
        /// <summary>
        /// Уровень
        /// </summary>
        public int Level
        {
            get { return _level; }
            set { _level = value; }
        }

        private int _upPoint = 0;
        /// <summary>
        /// Очко которое можно начислить одной из способностей, улучшив ее
        /// </summary>
        public int UpPoint
        {
            get { return _upPoint; }
            set
            {
                if (value < _upPoint)
                {///Значит игрок израсходовал один апнув левел 
                    (GameObject.View as UC_Player).HiddeUpLevel();

                }

                _upPoint = value;
            }
        }

        private int _exp = 0;
        /// <summary>
        /// Количество опыта
        /// </summary>
        public int Exp
        {
            get { return _exp; }
            set
            {
                _exp = value;
                if (_exp >= _maxExp && Level < 10)
                {
                    _exp = 0;
                    LevelUp();
                }
            }
        }

        private int _groupType;
        public int GroupType
        {
            get { return _groupType; }
            set { _groupType = value; }
        }

        private int _maxExp = 20;
        /// <summary>
        /// Порог опыта для получения уровня
        /// </summary>
        public int MaxExp
        {
            get { return _maxExp; }
            set { _maxExp = value; }
        }

        private int _gold = 0;
        /// <summary>
        /// Количества золота
        /// </summary>
        public int Gold
        {
            get { return _gold; }
            set { _gold = value;
        
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
                UnitGenerator.UpdatePlayerView(this);
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
                UnitGenerator.UpdatePlayerView(this);
            }
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


        private int _playerGamePoint = 0;
        /// <summary>
        /// Игровые очки игрока
        /// </summary>
        public int PlayerGamePoint
        {
            get { return _playerGamePoint; }
            set
            {
                _playerGamePoint = value;
            }
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

            if (Spells.Any(p => p.Name == name) && !Dead)
            {
                var spall = Spells.Single(p => p.Name == name);
                spall.UseSpall(CurrentMap, null, this, obj);
            }
        }

        /// <summary>
        /// Использование способности
        /// </summary>
        /// <param name="number">Номер способности</param>
        /// <param name="obj">Уникальные параметры, могут отсуцтвовать</param>
        public void UseSpall(int number, object obj)
        {
            if (Spells.Count > number && !Dead)
            {
                var spall = Spells[number];
                spall.UseSpall(CurrentMap, null, this, obj);
            }
        }

        /// <summary>
        /// Использование способности предмета
        /// </summary>
        /// <param name="number">Номер предмета</param>
        /// <param name="obj">Уникальные параметры, могут отсуцтвовать</param>
        public void UseItemSpall(int number, object obj)
        {
            if (Items.Count > number && !Dead)
            {
                var spall = Items[number].SpellItem;
                if (spall != null)
                    spall.UseSpall(CurrentMap, null, this, obj);
            }
        }

        #region Награды за убийство
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
        private ItemClass _nItem;
        public ItemClass NItem
        {
            get { return _nItem; }
            set { _nItem = value; }
        }


        #endregion
        private List<ItemClass> _item = new List<ItemClass>();
        /// <summary>
        /// Предметы данного героя
        /// </summary>
        public List<ItemClass> Items
        {
            get { return _item; }
            set { _item = value; }
        }



        #region Урон
        private int _demage;
        public int Demage
        {
            get
            {
                _demage = DemageStart + DemageItem;

                if (HeroType == EHeroType.Agility)
                    _demage += Agility / StaticVaribl.DemageD;
                else if (HeroType == EHeroType.Strength)
                    _demage += Strength / StaticVaribl.DemageD;
                else if (HeroType == EHeroType.Intelligence)
                    _demage += Intelligence / StaticVaribl.DemageD;

                return _demage;
            }
            set { _demage = value; }
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
        #endregion


        /// <summary>
        /// Получение урона
        /// </summary>
        /// <param name="phisic">физический</param>
        /// <param name="magic">магический</param>
        /// <param name="pure">чистый</param>
        public IUnit GatDamage(int phisic, int magic, int pure, IUnit demagedUnit)
        {
            IUnit removedUnit = null;
            if (!Invulnerability)
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

                #region Statistic
                StatisticData.DemageSelfs += demage;

                if (playerDemaged != null)
                    playerDemaged.StatisticData.Demage += demage;
                #endregion
                Health -= demage;

                IUnitControl chpView = GameObject.View as IUnitControl;
                if (chpView != null)
                    chpView.ShowHealth(Health, MaxHealth);

                if (GetDemageEvent != null)
                    GetDemageEvent(StaticVaribl.DemageAndArmor(phisic, Arrmor),
                        StaticVaribl.DemageAndArmor(magicDemagePlus, MagicArrmor),
                        pure, this);

                ///Отображаем сколько урона получил юнит
                (this.GameObject.View as IGameControl).GetDemage("-" + demage);

                if (Health <= 0)
                {///Значит объект унечтожен 
                    removedUnit = RemoveUnit(demagedUnit);
                }

                UpdateView();
            }
            else
                (this.GameObject.View as IGameControl).GetDemage("Invulnerability");

            return removedUnit;
        }

        public void GetAward(IUnit killedUnit)
        {
            this.Exp += killedUnit.NExp;
            int gold = 0;
            Player killedPlayer = killedUnit as Player;
            if (killedPlayer != null)
                gold += killedPlayer.Level * StaticVaribl.PlayerGoldPlus;
            else
                gold += killedUnit.NGold;

            Buff buff = Buffs.FirstOrDefault(p => p.Name == "GreevilsGreed");
            if (buff != null)
                gold += buff.GoldBonus;

            this.Gold += gold;
            StatisticData.AllGold += gold;
            this.UpdateView();
        }

        public IUnit RemoveUnit(IUnit demagedUnit)
        {
            Dead = true;
            IsUnitStun = false;

            IUnit returnUnit = null;
            if (demagedUnit != null)
                demagedUnit.Kills += 1;
            Player player = demagedUnit as Player;
            if (player != null)
            {
                player.GetAward(this);
            }
            Death += 1;

            this.Gold -= StaticVaribl.PlayerGoldMinus * this.Level;
            if (this.Gold < 0)
                this.Gold = 0;

            var call = CurrentMap.Calls.Single(p => p.IndexLeft == PositionX && p.IndexTop == PositionY);
            call.IUnits.Remove(this);
            CurrentMap.DeadPlayers.Add(this);

            StopRegenerationTimer();

            if (!call.IUnits.Any())
                call.Used = false;
            //CurrentMap.GameObjectInCall.Remove(this.GameObject);

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

                returnUnit = this;
            }
            #endregion

            ///Тут будет анимироватся
            CurrentMap.MapCanvas.Children.Remove(this.GameObject.View);

            foreach (var item in this.Spells)
            {
                IMoveSpell itemMove = item as IMoveSpell;
                if (itemMove != null)
                    itemMove.StopMoveSpell();
            }

            if (DeadEvent != null)
                DeadEvent(this, null);

            if (AllowRespaum)
            {
                ///Таймер воскрешения
                _respaumTimer = new Storyboard() { Duration = TimeSpan.FromSeconds(RespaumTime) };
                _respaumTimer.Completed += _respaumTimer_Tick;
                _respaumTimer.Begin();
            }

            return returnUnit;
        }

        void _respaumTimer_Tick(object sender, object e)
        {
            List<Map_Cell> calls = CurrentMap.Calls.Where(p => !p.Used && !p.Using && !p.Block).ToList();
            _respaumTimer.Completed -= _respaumTimer_Tick;
            _respaumTimer = null;

            if (calls.Count != 0)
            {
                Dead = false;
                CurrentMap.DeadPlayers.Remove(this);
                Health = MaxHealth;
                Mana = MaxMana;
                StartRegenerationTimer();

                Random rand = new Random((int)DateTime.Now.Ticks);
                Map_Cell respaumCall = calls[rand.Next(0, calls.Count - 1)];
                this.PositionX = respaumCall.IndexLeft;
                this.PositionY = respaumCall.IndexTop;

                CurrentMap.CreateObject(this, respaumCall.IndexLeft, respaumCall.IndexTop);
                ///Отображение
                CurrentMap.MapCanvas.Children.Add(this.GameObject.View);


                Invulnerability = true;
                ///Таймер временной неуязвимости после спаума
                _inviInvulnerabilityTimer = new Storyboard() { Duration = TimeSpan.FromSeconds(StaticVaribl.InvulnerabilityTime) };
                _inviInvulnerabilityTimer.Completed += _inviInvulnerabilityTimer_Tick;
                if (!Paused)
                    _inviInvulnerabilityTimer.Begin();
            }
            else
            {   ///Если на карте не было места где можно воскреснуть
                ///Таймер воскрешения
                _respaumTimer = new Storyboard() { Duration = TimeSpan.FromSeconds(RespaumTime) };
                _respaumTimer.Completed += _respaumTimer_Tick;
                _respaumTimer.Begin();
            }
            UpdateView();
        }
        void _inviInvulnerabilityTimer_Tick(object sender, object e)
        {
            _inviInvulnerabilityTimer.Completed -= _inviInvulnerabilityTimer_Tick;
            _inviInvulnerabilityTimer = null;
            Invulnerability = false;
        }

        #region Личные характеристики игрока
        private EHeroType _heroType;
        /// <summary>
        /// Тип героя, какая основная характеристика
        /// та что влияет на урон
        /// </summary>
        public EHeroType HeroType
        {
            get { return _heroType; }
            set { _heroType = value; }
        }

        private int _strength;
        /// <summary>
        /// Сила
        /// 10*силу = макс здоровъе
        /// </summary>
        public int Strength
        {
            get { return _strength; }
            set { _strength = value; }
        }

        private int _agility;
        /// <summary>
        /// Ловкость
        /// Скорость атаки = 1 - ловкость/100
        /// Каждые 4 ловкости - 1 к физ урону
        /// </summary>
        public int Agility
        {
            get { return _agility; }
            set { _agility = value; }
        }

        private int _intelligence;
        /// <summary>
        /// Интилект
        /// 10*интилект = макс мана
        /// </summary>
        public int Intelligence
        {
            get { return _intelligence; }
            set { _intelligence = value; }
        }

        private int _addStrength;
        /// <summary>
        /// Прирост силы за уровень
        /// </summary>
        public int AddStrength
        {
            get { return _addStrength; }
            set { _addStrength = value; }
        }

        private int _addAgility;
        /// <summary>
        /// Прирост ловкости за уровень
        /// </summary>
        public int AddAgility
        {
            get { return _addAgility; }
            set { _addAgility = value; }
        }

        private int _addIntelligence;
        /// <summary>
        /// Прирост интилекта за уровень
        /// </summary>
        public int AddIntelligence
        {
            get { return _addIntelligence; }
            set { _addIntelligence = value; }
        }
        #endregion



        public void UpdateView()
        {
            if (UpdatePlayerData != null)
                UpdatePlayerData(this, null);
        }

        public void AddItem(ItemClass item)
        {
            if (!item.Bonus)
            {
                Items.Add(item);

                #region Применяем итем

                Agility += item.Agility;
                Intelligence += item.Intelligence;
                Strength += item.Strength;

                Health += item.Strength * StaticVaribl.StrConstant;
                Mana += item.Intelligence * StaticVaribl.IntConstant;

                ItemArrmor += item.Armor;

                OrijAttackSpeed -= item.AttackSpeed;

                DemageItem += item.Demage;
                OrijHealth += item.HealthBonus;
                MaxHealth += item.HealthBonus;
                OrijHealthRegeneration += item.HealthRegen;

                OrijMana += item.ManaBonus;
                MaxMana += item.ManaBonus;
                OrijManaRegeneration += (int)item.ManaRegen;


                if (item.Boots && Items.Count(p => p.Boots) > 1)
                { }
                else
                {
               
                        OrijSpeed -= item.Speed;
                 
                }

                if (item.Buff != null)
                    Buffs.Add(item.Buff);

                IAura aura = item.AuraItem as IAura;
                if (aura != null)
                    aura.StartUseAura(CurrentMap,null,this,null);

                #endregion
                item.ItemUsed = true;



                if (UpdateItemPlayerData != null)
                    UpdateItemPlayerData(this, null);
            }
            else
            {
                if (item.Name == "Shop")///Если это итем магазин
                {
                    ///Ставим паузу
                    CurrentMap.Pause();
                    if (ShowShop != null)
                        ShowShop(this, null);
                }

                Health += item.HealthBonus;
                Mana += item.ManaBonus;

                if (item.HealthBonus != 0)
                    (this.GameObject.View as IGameControl).GetDemage("+" + item.HealthBonus + " HP");
                else if (item.ManaBonus != 0)
                    (this.GameObject.View as IGameControl).GetDemage("+" + item.ManaBonus + " MP");

            }
            UpdateView();

        }
        public void RemoveItem(ItemClass item, bool notUpdate)
        {
            if (item.ItemUsed == true)
            {
                #region Убираем действие итема

                Agility -= item.Agility;
                Intelligence -= item.Intelligence;
                Strength -= item.Strength;

                ItemArrmor -= item.Armor;

                OrijAttackSpeed += item.AttackSpeed;

                DemageItem -= item.Demage;
                OrijHealth -= item.HealthBonus;
                MaxHealth -= item.HealthBonus;
                OrijHealthRegeneration -= item.HealthRegen;

                OrijMana -= item.ManaBonus;
                MaxMana -= item.ManaBonus;
                OrijManaRegeneration -= (int)item.ManaRegen;

                if (item.Boots && Items.Count(p => p.Boots) > 1)
                { }
                else
                {
                 
                        OrijSpeed += item.Speed;
                }


                if (item.Buff != null)
                {
                    Buff buff = Buffs.FirstOrDefault(p => p.Name == item.Buff.Name);
                    if (buff != null)
                        Buffs.Remove(buff);
                }

                IAura aura = item.AuraItem as IAura;
                if (aura != null)
                    aura.StopUseAura();
            }
                #endregion
            Items.Remove(item);



            if (!notUpdate)
            {
                if (UpdateItemPlayerData != null)
                    UpdateItemPlayerData(this, null);

                UpdateView();
            }
        }
        public void UpdateItemPlayerDataCall()
        {
            if (UpdateItemPlayerData != null)
                UpdateItemPlayerData(this, null);
        }
        public bool DropItem(ItemClass item)
        {
            ///Ячейка куда будет дропнут итем
            Map_Cell dropCall = null;

            ///Выбираем ячейку куда дропнуть итем
            List<Point> poin = UnitGenerator.RoundNumber(this.PositionX, this.PositionY);

            for (int i = 0; i < poin.Count; i++)
            {

                if (CurrentMap.Calls.Any(p => p.IndexLeft == poin[i].X && p.IndexTop == poin[i].Y))
                {
                    Map_Cell call = CurrentMap.Calls.Single(p => p.IndexLeft == poin[i].X && p.IndexTop == poin[i].Y);
                    ///Проверим или можно в колонку кинуть итем
                    if (!call.Used &&
                        !call.Using &&
                        !call.Block &&
                        call.Item.Count == 0)
                    {
                        dropCall = call;
                        break;
                    }
                }
            }

            if (dropCall != null)
            {
                RemoveItem(item, false);
                dropCall.Item.Add(item);
                item.PositionX = dropCall.IndexLeft;
                item.PositionY = dropCall.IndexTop;
                item.View.Width = 50;
                item.View.Height = 50;
                item.ItemPosition();
                item.View.ApplayMargin(new Thickness(8));
                CurrentMap.MapCanvas.Children.Add(item.View);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool PlayerInShopPoint() 
        {
            if (PositionX == 0 && PositionY == 0)
                return true;

            var lastCall = CurrentMap.Calls.Last();
            if (PositionX == lastCall.IndexLeft && PositionY == lastCall.IndexTop)
                return true;

            lastCall = CurrentMap.Calls[208];
            if (PositionX == lastCall.IndexLeft && PositionY == lastCall.IndexTop)
                return true;

            lastCall = CurrentMap.Calls[12];
            if (PositionX == lastCall.IndexLeft && PositionY == lastCall.IndexTop)
                return true;

            return false;
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
        /// Увиличение уровня
        /// </summary>
        public void LevelUp()
        {
            Level += 1;
            UpPoint += 1;
            MaxExp += 100;

            if (LevelUpEvent != null)
                LevelUpEvent(this, null);

            Strength += AddStrength;
            Intelligence += AddIntelligence;
            Agility += AddAgility;

            Health += AddStrength * StaticVaribl.StrConstant;
            Mana += AddIntelligence * StaticVaribl.IntConstant;

            OrijAttackSpeed -= AddAgility * StaticVaribl.AgilityAttackSpeed;

            (GameObject.View as UC_Player).UpLevel();

            UpdateView();
        }

        void _healthTimer_Tick(object sender, object e)
        {
            Health = Health + (int)HealthRegeneration;
            _healthTimer.Begin();
        }

        void manaTimer_Tick(object sender, object e)
        {
            Mana = Mana + ManaRegeneration;
            UpdateView();
            _manaTimer.Begin();
        }
        /// <summary>
        /// Каждую секунду этот таймер проверяет какие воздействия влияют на юнит
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
                {
                    if (Buffs[i].Duration < 0)
                        StopAndRemoveBuff(Buffs[i]);
                    else
                    {
                        ///Если баф еще не влияет на данный юнит
                        if (!Buffs[i].BuffUsed)
                        {
                            BuffStartUse(Buffs[i]);

                            UpdateView();
                        }

                        Buffs[i].Duration -= 0.1;
                    }

                }
            }
            _buffTimer.Begin();
        }

        private void StopAndRemoveBuff(Buff curentBuff)
        {
            if (curentBuff.BuffUsed)
            {
                ItemArrmor += curentBuff.MinusArmor;
                ItemArrmor -= curentBuff.Armor;

               
                OrijSpeed -= curentBuff.SpeedSlow;
                    //Speed -= Buffs[i].SpeedSlow;
               
                // if (OrijAttackSpeed == AttackSpeed - Buffs[i].AttackSpeedSlow)
                ///Значит нет никаких воздействий на героя
                OrijAttackSpeed -=  curentBuff.AttackSpeedSlow;
                OrijAttackSpeed +=  curentBuff.AttackSpeed;

                curentBuff.BuffUsed = false;
            }

            Buffs.Remove(curentBuff);///Время действия бафа истекло

            if (!Buffs.Any(p => p.Stun))///Если на юнит не воздействуют другие станы
                IsUnitStun = false;

            UpdateView();
        }
        public void UseBuff(Buff buff)
        {
            BuffStartUse(buff);
            Buffs.Add(buff);
            UpdateView();
        }
        private void BuffStartUse(Buff buff)
        {
            ItemArrmor -= buff.MinusArmor;
            ItemArrmor += buff.Armor;

            buff.BuffUsed = true;
            if (buff.Stun)
                IsUnitStun = true;

            OrijSpeed += buff.SpeedSlow;
            OrijAttackSpeed += buff.AttackSpeedSlow;
            OrijAttackSpeed -= buff.AttackSpeed;

            if (buff.DemageMagic > 0)
                GatDamage(0, buff.DemageMagic, 0, buff.DemagedUnit);
        }
        /// <summary>
        /// Удалить баф по имени
        /// </summary>
        /// <param name="buff">Название бафа</param>
     public  void RemoveBuff(string buff)
        {
            Buff buffRemov = Buffs.FirstOrDefault(p => p.Name == buff);
            if (buffRemov!= null)
            StopAndRemoveBuff(buffRemov);
        }
        public void SpellUpedEventCall()
        {
            if (SpellUpedEvent != null)
                SpellUpedEvent(this, null);
        }
        #region Pause
        public event EventHandler PausedEvent;
        bool _paused;
        public bool Paused { get { return _paused; } set { _paused = value; } }

        public void Pause()
        {
            Paused = true;

            (GameObject.View as IAnimationControl).Pause();

            if (_buffTimer != null)
                _buffTimer.Pause();

            if (_respaumTimer != null && Dead)
                _respaumTimer.Pause();

            if (_inviInvulnerabilityTimer != null && Invulnerability)
                _inviInvulnerabilityTimer.Pause();

            foreach (var item in Spells)
                item.Pause();

            foreach (var item in Items)
                item.Pause();

            if (AI != null)
                AI.Pause();

            StopRegenerationTimer();

            if (PausedEvent != null)
                PausedEvent(this, null);
        }
        public void StopPause()
        {

            Paused = false;

            (GameObject.View as IAnimationControl).StopPause();

            if (_respaumTimer != null && Dead)
                _respaumTimer.Resume();
            if (_buffTimer != null)
                _buffTimer.Resume();
            if (_inviInvulnerabilityTimer != null && Invulnerability)
                _inviInvulnerabilityTimer.Resume();

            foreach (var item in Spells)
                item.StopPause();

            foreach (var item in Items)
                item.StopPause();

            if (AI != null)
                AI.StopPause();

            StartRegenerationTimer();

            if (PausedEvent != null)
                PausedEvent(this, null);
        }

        public void DeleteUnit()
        {
            if (_manaTimer != null)
                _manaTimer.Stop();
            _manaTimer = null;
            if (_healthTimer != null)
                _healthTimer.Stop();
            _healthTimer = null;
            if (_respaumTimer != null)
                _respaumTimer.Stop();
            _respaumTimer = null;
            if (_inviInvulnerabilityTimer != null)
                _inviInvulnerabilityTimer.Stop();
            _inviInvulnerabilityTimer = null;
            if (_buffTimer != null)
                _buffTimer.Stop();
            _buffTimer = null;
        }
        #endregion
    }
}
