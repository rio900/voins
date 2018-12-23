using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Voins;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Voins.AppCode
{
    public class Map
    {
        DispatcherTimer _farmGameDispatcherTimer;
        public event EventHandler VinEvent;


        private EGameType _gameType = EGameType.Null;
        /// <summary>
        /// Тип текущей игры
        /// </summary>
        public EGameType GameType
        {
            get { return _gameType; }
            set { _gameType = value; }
        }

        private int _killLimit = 10;
        /// <summary>
        /// Лимит убийств необходимый для победы
        /// </summary>
        public int KillLimit
        {
            get { return _killLimit; }
            set { _killLimit = value; }
        }

        private int _width = 850;

        public int Width
        {
            get { return _width; }
            set { _width = value; }
        }

        private int _coopTimer = 0;
        public int CoopTimer
        {
            get { return _coopTimer; }
            set { _coopTimer = value; }
        }

        private int _step = 30;
        /// <summary>
        /// Рандом, заполнение кубиками карты
        /// шанс появления кубика в конкретном месте
        /// </summary>
        public int Step
        {
            get { return _step; }
            set { _step = value; }
        }

        private int _stepMobs = 10;
        /// <summary>
        /// Рандом, заполнение мобами карты
        /// шанс появления моба в конкретном месте
        /// </summary>
        public int StepMobs
        {
            get { return _stepMobs; }
            set { _stepMobs = value; }
        }

        private int _height = 650;

        public int Height
        {
            get { return _height; }
            set { _height = value; }
        }

        private List<Map_Cell> _calls;

        public List<Map_Cell> Calls
        {
            get { return _calls; }
            set { _calls = value; }
        }

        private List<Game_Object_In_Call> _gameObjectInCall = new List<Game_Object_In_Call>();

        public List<Game_Object_In_Call> GameObjectInCall
        {
            get { return _gameObjectInCall; }
            set { _gameObjectInCall = value; }
        }

        private Canvas _mapCanvas;
        /// <summary>
        /// Канвас карты
        /// </summary>
        public Canvas MapCanvas
        {
            get { return _mapCanvas; }
            set { _mapCanvas = value; }
        }

        public Map(Canvas mapCanvas)
        {
            _mapCanvas = mapCanvas;
        }

        private List<Player> _players = new List<Player>();

        public List<Player> Players
        {
            get { return _players; }
            set { _players = value; }
        }

        private List<Player> _deadPlayers = new List<Player>();

        public List<Player> DeadPlayers
        {
            get { return _deadPlayers; }
            set { _deadPlayers = value; }
        }

        private List<Point> _killsDeath = new List<Point>();

        public List<Point> KillsDeath
        {
            get { return _killsDeath; }
            set { _killsDeath = value; }
        }
        private List<ItemClass> _droppedItemsList = new List<ItemClass>();
        /// <summary>
        /// Список предметов которые могут выпасть на данной катрте
        /// </summary>
        public List<ItemClass> DroppedItemsList
        {
            get { return _droppedItemsList; }
            set { _droppedItemsList = value; }
        }


        #region Bonus Point
        private List<IUnit> _blockBonusUnit = new List<IUnit>();
        /// <summary>
        /// Блоки которые считаются за вырисовку цифры
        /// </summary>
        public List<IUnit> BlockBonusUnit
        {
            get { return _blockBonusUnit; }
            set { _blockBonusUnit = value; }
        }

        private bool _blockBonusGame = true;
        /// <summary>
        /// Режим игры где за блоки цифры дают очки
        /// </summary>
        public bool BlockBonusGame
        {
            get { return _blockBonusGame; }
            set { _blockBonusGame = value; }
        }

        #endregion
        int _mapNumberFarmGame;
        int _modFarmGame;

        public void CreateRandomMap(bool noBlock)
        {
            Calls = new List<Map_Cell>();
            MapCanvas.Children.Clear();
            _players = new List<Player>();
            _deadPlayers = new List<Player>();
            _killsDeath = new List<Point>();
            _gameObjectInCall = new List<Game_Object_In_Call>();

            for (int i = 0; i < Width / 50; i++)
            {
                for (int j = 0; j < Height / 50; j++)
                {
                    Map_Cell map_Call = new Map_Cell()
                    {
                        IndexLeft = i,
                        IndexTop = j
                    };

                    ///Это для размещения блока стены(не пробиваймые блоки)
                    if (i % 2 != 0 && j % 2 != 0)
                    {
                        if (!noBlock)
                        {
                            Unit unit = new Unit()
                            {
                                PositionX = i,
                                PositionY = j
                            };

                            UC_Block uc_Block = new UC_Block()
                                {
                                    Width = 50,
                                    Height = 50
                                };
                            Canvas.SetLeft(uc_Block, i * 50);
                            Canvas.SetTop(uc_Block, j * 50);

                            ///Непроходимый блок
                            Game_Object_In_Call block = new Game_Object_In_Call()
                            {
                                EnumCallType = EnumCallType.Block,
                                View = uc_Block
                            };
                            unit.GameObject = block;

                            map_Call.Block = true;
                            ///В этой ячейке находится непроходимый блок
                            map_Call.IUnits.Add(unit);
                            ///И его же добавим в масив всех объектов
                            GameObjectInCall.Add(block);
                            ///Отображение
                            MapCanvas.Children.Add(block.View);
                        }
                    }
                    Calls.Add(map_Call);
                }
            }
        }

        /// <summary>
        /// Заполнение карту блоками
        /// </summary>
        public void CreateBlock()
        {
            #region CreateBlock
            int randInt = 0;

            #region Создание блоков закрывающих спаум

            #endregion

            foreach (var item in Calls)
            {
                if (item.IndexLeft == 0 && item.IndexTop == 1 ||
                   item.IndexLeft == 1 && item.IndexTop == 0 ||
                   item.IndexLeft == Width / 50 - 2 && item.IndexTop == Height / 50 - 1 ||
                   item.IndexLeft == Width / 50 - 1 && item.IndexTop == Height / 50 - 2 ||

                   item.IndexLeft == Width / 50 - 2 && item.IndexTop == 0 ||
                   item.IndexLeft == Width / 50 - 1 && item.IndexTop == 1 ||

                   item.IndexLeft == 1 && item.IndexTop == Height / 50 - 1 ||
                   item.IndexLeft == 0 && item.IndexTop == Height / 50 - 2
                   )
                {
                    CreateObjectUnitInCall(item, UnitGenerator.GrassBlock(item.IndexLeft, item.IndexTop));

                }
                ///Проверка ячеек которые не могут быть заполнены
                else if (!item.Used &&
                     !item.Block)
                {
                    Random random = new Random((int)DateTime.Now.Ticks + randInt++);
                    int rand = random.Next(0, 100);

                    if (rand < Step)
                    {
                        Unit unit = UnitGenerator.GrassBlock(item.IndexLeft, item.IndexTop);
                        CreateObjectUnitInCall(item, unit);
                    }
                }
            }
            #endregion
        }

        public void CreateObjectUnitInCall(Map_Cell item, Unit unit)
        {
            unit.CurrentMap = this;
            ///И его же добавим в масив всех объектов
            GameObjectInCall.Add(unit.GameObject);
            ///Добавляем в список всех юнитов
            item.IUnits.Add(unit);
            item.Used = true;
            ///Отображение
            MapCanvas.Children.Add(unit.GameObject.View);
        }

        /// <summary>
        /// Заполнение карту блоками
        /// </summary>
        public void CreateMobs()
        {
            #region CreateMobs
            int randInt = 0;

            foreach (var item in Calls)
            {
                ///Проверка ячеек которые не могут быть заполнены
                if (!item.Used &&
                     !item.Block)
                {
                    Random random = new Random((int)DateTime.Now.Ticks + randInt++);
                    int rand = random.Next(0, 100);

                    if (rand < StepMobs)
                    {
                        Unit unit = UnitGenerator.M1_Ball(item.IndexLeft, item.IndexTop, this);

                        ///И его же добавим в масив всех объектов
                        GameObjectInCall.Add(unit.GameObject);
                        ///Добавляем в список всех юнитов
                        item.IUnits.Add(unit);
                        item.Used = true;

                        ///Отображение
                        MapCanvas.Children.Add(unit.GameObject.View);

                        if (unit.AI != null)
                            unit.AI.StartAI();
                    }
                }
            }
            #endregion
        }

        public void CreateNewCampaning(int missionNumber)
        {
            GameType = EGameType.Campaign;

            foreach (var item in Players)
                item.DeadEvent += item_DeadEvent;

            if (missionNumber == 0)
                MapGenerator.Level_1_Green(this);

            _farmGameDispatcherTimer = new DispatcherTimer() { Interval = TimeSpan.FromSeconds(1) };
            _farmGameDispatcherTimer.Tick += farmGameDispatcherTimer_Tick;
            _farmGameDispatcherTimer.Start();
        }

        public void CreateNewCooperative()
        {
            GameType = EGameType.Coop;
            CoopTimer = 1;

            foreach (var item in Players)
                item.DeadEvent += item_DeadEvent;

            CreateBlock();
            MapGenerator.CreateMobsCoopEasyMob(this, 8, EMobType.Ball);
            //MapGenerator.CreateMobsCoopEasyMob(this, 1, EMobType.RedBall);
            CreateDroppedItems();
            _farmGameDispatcherTimer = new DispatcherTimer() { Interval = TimeSpan.FromSeconds(1) };
            _farmGameDispatcherTimer.Tick += farmGameDispatcherTimer_Tick;
            _farmGameDispatcherTimer.Start();
        }

        public void CreateNewFarmGame(int mapNumber, int mod)
        {
            GameType = EGameType.FurmGame;
            _mapNumberFarmGame = mapNumber;
            _modFarmGame = mod;

            foreach (var item in Players)
                item.DeadEvent += item_DeadEvent;

            StepMobs = 8;
            if (_mapNumberFarmGame == 0)
            {
                CreateBlock();
                CreateMobs();
            }
            else if (_mapNumberFarmGame > 0)
                MapGenerator.FarmGameMap(this, _mapNumberFarmGame, _modFarmGame);
            else
                MapGenerator.SpecialGameMap(this, Math.Abs(_mapNumberFarmGame), _modFarmGame);

            CreateDroppedItems();

            _farmGameDispatcherTimer = new DispatcherTimer() { Interval = TimeSpan.FromSeconds(1) };
            _farmGameDispatcherTimer.Tick += farmGameDispatcherTimer_Tick;
            _farmGameDispatcherTimer.Start();
        }

        void item_DeadEvent(object sender, EventArgs e)
        {
            if (GameType == EGameType.FurmGame)
            {
                int maxDeath = int.MinValue;
                foreach (var item in Players)
                    maxDeath = Math.Max(item.Death, maxDeath);
                if (KillLimit <= maxDeath)
                {
                    if (VinEvent != null)
                        VinEvent(Players.FirstOrDefault(p => p.Death < maxDeath), null);

                    Pause();
                    if (_farmGameDispatcherTimer != null)
                    {
                        _farmGameDispatcherTimer.Tick -= farmGameDispatcherTimer_Tick;
                        _farmGameDispatcherTimer.Stop();
                        _farmGameDispatcherTimer = null;
                    }
                }
            }
            else if (GameType == EGameType.Coop)
            {///Лимит смертей двух игроков

                int maxDeath = 0;
                foreach (var item in Players)
                    maxDeath += item.Death;

                if (KillLimit <= maxDeath)
                {
                    if (VinEvent != null)
                        VinEvent(CoopTimer, null);
                    CoopTimer = 0;
                    Pause();
                    if (_farmGameDispatcherTimer != null)
                    {
                        _farmGameDispatcherTimer.Tick -= farmGameDispatcherTimer_Tick;
                        _farmGameDispatcherTimer.Stop();
                        _farmGameDispatcherTimer = null;
                    }
                }

            }

            #region StopCurrentGame



            #endregion
        }

        private void CreateDroppedItems()
        {
            List<ItemClass> droppedItemList = new List<ItemClass>();
            /// 4 кларити
            droppedItemList.Add(UnitGenerator.IBonus2_Clarity());
            droppedItemList.Add(UnitGenerator.IBonus2_Clarity());
            droppedItemList.Add(UnitGenerator.IBonus2_Clarity());
            droppedItemList.Add(UnitGenerator.IBonus2_Clarity());
            /// 4 танго
            droppedItemList.Add(UnitGenerator.IBonus4_Tango());
            droppedItemList.Add(UnitGenerator.IBonus4_Tango());
            droppedItemList.Add(UnitGenerator.IBonus4_Tango());
            droppedItemList.Add(UnitGenerator.IBonus4_Tango());
            /// 2 хилинга
            droppedItemList.Add(UnitGenerator.IBonus3_HealingSalve());
            droppedItemList.Add(UnitGenerator.IBonus3_HealingSalve());
            /// 7 лавок
           // droppedItemList.Add(UnitGenerator.IBonus1_Shop());
         //   droppedItemList.Add(UnitGenerator.IBonus1_Shop());
         //   droppedItemList.Add(UnitGenerator.IBonus1_Shop());
         //   droppedItemList.Add(UnitGenerator.IBonus1_Shop());
         //   droppedItemList.Add(UnitGenerator.IBonus1_Shop());
         //   droppedItemList.Add(UnitGenerator.IBonus1_Shop());
         //   droppedItemList.Add(UnitGenerator.IBonus1_Shop());

            for (int i = 0; i < droppedItemList.Count; i++)
            {
                Random rand = new Random(i);
                List<Map_Cell> call = Calls.Where(p => p.IUnits.Any(k => k.GameObject.EnumCallType == EnumCallType.UnitBlock)).ToList();
                call[rand.Next(0, call.Count - 1)].IUnits.Single((k => k.GameObject.EnumCallType == EnumCallType.UnitBlock)).NItem = droppedItemList[i];
            }
        }

        private void CreateCoopDroppedItems()
        {
            List<ItemClass> droppedItemList = new List<ItemClass>();
            /// 3 кларити
            droppedItemList.Add(UnitGenerator.IBonus2_Clarity());
            droppedItemList.Add(UnitGenerator.IBonus2_Clarity());
            droppedItemList.Add(UnitGenerator.IBonus2_Clarity());
            /// 3 танго
            droppedItemList.Add(UnitGenerator.IBonus4_Tango());
            droppedItemList.Add(UnitGenerator.IBonus4_Tango());
            droppedItemList.Add(UnitGenerator.IBonus4_Tango());
            /// 2 хилинга
            droppedItemList.Add(UnitGenerator.IBonus3_HealingSalve());
            droppedItemList.Add(UnitGenerator.IBonus3_HealingSalve());
            /// 3 лавок
         //   droppedItemList.Add(UnitGenerator.IBonus1_Shop());
        //    droppedItemList.Add(UnitGenerator.IBonus1_Shop());
       //     droppedItemList.Add(UnitGenerator.IBonus1_Shop());

            for (int i = 0; i < droppedItemList.Count; i++)
            {
                Random rand = new Random(i);
                List<Map_Cell> call = Calls.Where(p => p.IUnits.Any(k => k.GameObject.EnumCallType == EnumCallType.UnitBlock)).ToList();
                call[rand.Next(0, call.Count - 1)].IUnits.Single((k => k.GameObject.EnumCallType == EnumCallType.UnitBlock)).NItem = droppedItemList[i];
            }
        }

        void farmGameDispatcherTimer_Tick(object sender, object e)
        {
            if (GameType == EGameType.FurmGame)
            {
                if (GameObjectInCall.Count(p => p.EnumCallType == EnumCallType.UnitBlock) == 0 &&
                    GameObjectInCall.Count(p => p.EnumCallType == EnumCallType.Unit) == 0)
                {
                    StepMobs = 8;
                    if (_mapNumberFarmGame == 0)
                    {
                        CreateBlock();
                        CreateMobs();
                    }
                    else if(_mapNumberFarmGame > 0)
                        MapGenerator.FarmGameMap(this, _mapNumberFarmGame, _modFarmGame);
                    else
                        MapGenerator.SpecialGameMap(this, Math.Abs(_mapNumberFarmGame), _modFarmGame);

                    CreateDroppedItems();
                }
            }
            else if (GameType == EGameType.Coop)
            {

                if (GameObjectInCall.Count(p => p.EnumCallType == EnumCallType.UnitBlock) == 0 &&
                     GameObjectInCall.Count(p => p.EnumCallType == EnumCallType.Unit) == 0)
                {
                    CreateBlock();

                    CoopTimer+=1;
                    if (CoopTimer == 2)
                    {
                        MapGenerator.CreateMobsCoopEasyMob(this, 5, EMobType.RangeBall);
                        MapGenerator.CreateMobsCoopEasyMob(this, 1, EMobType.BlueBall);
                        MapGenerator.CreateMobsCoopEasyMob(this, 5, EMobType.Ball);
                    }
                    else if (CoopTimer == 3)
                    {
                        MapGenerator.CreateMobsCoopEasyMob(this, 3, EMobType.RangeBall);
                        MapGenerator.CreateMobsCoopEasyMob(this, 3, EMobType.Ball);
                        MapGenerator.CreateMobsCoopEasyMob(this, 2, EMobType.BlueBall);
                        MapGenerator.CreateMobsCoopEasyMob(this, 1, EMobType.RedBall);
                    }
                    else if (CoopTimer == 4)
                    {
                        MapGenerator.CreateMobsCoopEasyMob(this, 3, EMobType.RangeBall);
                        MapGenerator.CreateMobsCoopEasyMob(this, 3, EMobType.Ball);
                        MapGenerator.CreateMobsCoopEasyMob(this, 2, EMobType.BlueBall);
                        MapGenerator.CreateMobsCoopEasyMob(this, 2, EMobType.RedBall);
                    }
                    else if (CoopTimer == 5)
                    {
                        MapGenerator.CreateMobsCoopEasyMob(this, 3, EMobType.RangeBall);
                        MapGenerator.CreateMobsCoopEasyMob(this, 1, EMobType.Ball);
                        MapGenerator.CreateMobsCoopEasyMob(this, 4, EMobType.BlueBall);
                        MapGenerator.CreateMobsCoopEasyMob(this, 3, EMobType.RedBall);
                    }
                    else
                    {
                        MapGenerator.CreateMobsCoopEasyMob(this, 7, EMobType.RangeBall);
                        MapGenerator.CreateMobsCoopEasyMob(this, 1, EMobType.Ball);
                        MapGenerator.CreateMobsCoopEasyMob(this, 3, EMobType.BlueBall);
                        MapGenerator.CreateMobsCoopEasyMob(this, 2, EMobType.RedBall);
                    }

                    CreateCoopDroppedItems();
                }
            }
        }

        public void CreatePlayer(Player player)
        {
            Players.Add(player);
            Canvas.SetZIndex(player.GameObject.View, 1000);
            ///Если один игрок
            ///Левый верхний угол спаума
            if (Players.Count == 1)
                CreateObject(player, 0, 0);
            else if (Players.Count == 2)
            {
                player.PositionX = Calls.Last().IndexLeft;
                player.PositionY = Calls.Last().IndexTop;
                CreateObject(player, Calls.Last().IndexLeft, Calls.Last().IndexTop);
                //player.PositionX = Calls[2].IndexLeft;
                //player.PositionY = Calls[2].IndexTop;
                //CreateObject(player, Calls[2].IndexLeft, Calls[2].IndexTop);
            }
            else if (Players.Count == 3)
            {
                Map_Cell call = Calls[208];
                player.PositionX = call.IndexLeft;
                player.PositionY = call.IndexTop;
                CreateObject(player, call.IndexLeft, call.IndexTop);
            }
            else if (Players.Count == 4)
            {
                Map_Cell call = Calls[12];
                player.PositionX = call.IndexLeft;
                player.PositionY = call.IndexTop;
                CreateObject(player, call.IndexLeft, call.IndexTop);
            }

            #region Добавление игрока
            ///И его же добавим в масив всех объектов
            GameObjectInCall.Add(player.GameObject);
            ///Отображение
            MapCanvas.Children.Add(player.GameObject.View);
            #endregion
        }

        /// <summary>
        /// Добавление в ячейку объекта
        /// !без проверки зависимочстей
        /// </summary>
        /// <param name="obj">объект</param>
        /// <param name="x">позиц х</param>
        /// <param name="y">позиц у</param>
        public void CreateObject(IUnit obj, int x, int y)
        {
            Canvas.SetLeft(obj.GameObject.View, x * 50);
            Canvas.SetTop(obj.GameObject.View, y * 50);
            var call = Calls.Single(p => p.IndexLeft == x && p.IndexTop == y);
            call.IUnits.Add(obj);
            call.Used = true;
        }

        /// <summary>
        /// Проверка можно ли в ячейку переместится юниту 
        /// </summary>
        /// <param name="x">Координата х</param>
        /// <param name="y">Координата у</param>
        /// <param name="return">можно ли в ячейку ходить</param>
        public bool AllowMoveUnit(IUnit unit, int x, int y)
        {
            ///Ищем, есть ли такая ячейка
            if (Calls.Any(p => p.IndexLeft == x && p.IndexTop == y))
            {
                ///Берем ячейку
                var call = Calls.Single(p => p.IndexLeft == x && p.IndexTop == y);
                ///Проверяем или ячейка не занята и не используется(тоесть в нее кто то ходит но еще не походил)
                if (!call.Using && !call.Used && !call.Block)
                    return true;
                else if (call.Used && !call.Block && !call.Using && unit.Invisibility)
                {
                    if (call.IUnits.Any(p => p.GameObject.EnumCallType == EnumCallType.UnitBlock))
                        return true;
                }
            }
            return false;
        }

        // public bool UsingOrUsedCall(bool usiG, bool usiD) { }

        public void Pause()
        {
            foreach (var player in Players)
                player.Pause();

            foreach (var call in Calls)
            {
                foreach (var unit in call.IUnits)
                    if (unit as Player == null)
                        unit.Pause();

                foreach (var unit in call.Bullet)
                    unit.Pause();
            }
        }
        public void StopPause()
        {
            foreach (var player in Players)
                player.StopPause();

            foreach (var call in Calls)
            {
                foreach (var unit in call.IUnits)
                    if (unit as Player == null)
                        unit.StopPause();

                foreach (var unit in call.Bullet)
                    unit.StopPause();
            }
        }

        public void DeleteCurrentMap()
        {
            foreach (var item in Players)
            {
                item.DeadEvent -= item_DeadEvent;
            }
            if (Calls != null)
            {
                if (_farmGameDispatcherTimer != null)
                    _farmGameDispatcherTimer.Stop();
                Pause();

                foreach (var call in Calls)
                {
                    for (int i = 0; i < call.IUnits.Count; i++)
                    {
                        if (call.IUnits[i].AI != null)
                        {
                            call.IUnits[i].AI.StopAI();
                        }
                        Player palyer = call.IUnits[i] as Player;
                        if (palyer != null)
                        {
                            palyer.AllowRespaum = false;
                            palyer.DeleteUnit();
                        }
                        if (call.IUnits[i].GameObject.EnumCallType != EnumCallType.Block)
                            call.IUnits[i].GatDamage(int.MaxValue, int.MaxValue, int.MaxValue, null);
                    }

                    foreach (var item in call.Bullet)
                        MapCanvas.Children.Remove(item.GameObject.View);
                    call.Bullet.Clear();

                    foreach (var item in call.Item)
                        MapCanvas.Children.Remove(item.View);
                    call.Item.Clear();
                }

                DroppedItemsList.Clear();
                GameObjectInCall.Clear();
                Players.Clear();
                StopPause();
            }
        }
        /// <summary>
        /// Тут проверим какую цифру рисовали уничтоженные кусты
        /// </summary>
        /// <param name="unit">Куст</param>
        /// <returns>Бонус очков</returns>
        public int CheckNumber(Unit unit)
        {
            ///С начала добавляем куст в список уничтоженых кустов
            BlockBonusUnit.Add(unit);

            if (BlockBonusUnit.Count > 11)
                BlockBonusUnit.Remove(BlockBonusUnit.First());

            List<Point> pointUnitList = new List<Point>();
            foreach (var item in BlockBonusUnit.OrderBy(p => p.PositionX))
                pointUnitList.Add(new Point(item.PositionX, item.PositionY));

            int minX = int.MaxValue;
            int maxY = int.MinValue;

            foreach (var item in pointUnitList)
            {
                minX = Math.Min(minX, (int)item.X);
                maxY = Math.Max(maxY, (int)item.Y);
            }

            if (BlockBonusUnit.Count >= 8)
            { ///Теперь проверяем какую цифру отображают блоки

                ///Начинать проверку будем с левого нижнего края
                ///Проверяем цифру "1"
                bool one = CheckOne(pointUnitList, minX, maxY);
                if (one)
                    return 1000;

                bool three = CheckThree(pointUnitList, minX, maxY);
                if (three)
                    return 3000;

                bool five = CheckFive(pointUnitList, minX, maxY);
                if (five)
                    return 5000;
            }
            return 0;
        }
        /// <summary>
        /// Проверка цифры 1
        /// </summary>
        /// <param name="pointUnitList"></param>
        private bool CheckOne(List<Point> pointUnitList, int minX, int maxY)
        {
            Point leftBottomPoint = pointUnitList.FirstOrDefault(p => p.X == minX && p.Y == maxY);

            if (leftBottomPoint != null)
            {
                if (!pointUnitList.Any(p => p.X == minX && p.Y == maxY - 4))
                    return false;

                if (!pointUnitList.Any(p => p.X == minX + 1 && p.Y == maxY))
                    return false;

                if (!pointUnitList.Any(p => p.X == minX + 1 && p.Y == maxY - 1))
                    return false;

                if (!pointUnitList.Any(p => p.X == minX + 1 && p.Y == maxY - 2))
                    return false;

                if (!pointUnitList.Any(p => p.X == minX + 1 && p.Y == maxY - 3))
                    return false;

                if (!pointUnitList.Any(p => p.X == minX + 1 && p.Y == maxY - 4))
                    return false;

                if (!pointUnitList.Any(p => p.X == minX + 2 && p.Y == maxY))
                    return false;

                BlockBonusUnit.Clear();

                return true;
            }
            return false;
        }

        /// <summary>
        /// Проверка цифры 3
        /// </summary>
        /// <param name="pointUnitList"></param>
        private bool CheckThree(List<Point> pointUnitList, int minX, int maxY)
        {
            Point leftBottomPoint = pointUnitList.FirstOrDefault(p => p.X == minX && p.Y == maxY);

            if (leftBottomPoint != null)
            {
                if (!pointUnitList.Any(p => p.X == minX && p.Y == maxY - 4))
                    return false;

                if (!pointUnitList.Any(p => p.X == minX + 1 && p.Y == maxY - 2))
                    return false;

                if (!pointUnitList.Any(p => p.X == minX + 1 && p.Y == maxY - 4))
                    return false;

                if (!pointUnitList.Any(p => p.X == minX + 2 && p.Y == maxY - 1))
                    return false;

                if (!pointUnitList.Any(p => p.X == minX + 2 && p.Y == maxY - 2))
                    return false;

                if (!pointUnitList.Any(p => p.X == minX + 2 && p.Y == maxY - 3))
                    return false;

                if (!pointUnitList.Any(p => p.X == minX + 2 && p.Y == maxY - 4))
                    return false;
                BlockBonusUnit.Clear();
                return true;
            }
            return false;
        }


        /// <summary>
        /// Проверка цифры 5
        /// </summary>
        private bool CheckFive(List<Point> pointUnitList, int minX, int maxY)
        {
            Point leftBottomPoint = pointUnitList.FirstOrDefault(p => p.X == minX && p.Y == maxY);

            if (leftBottomPoint != null)
            {
                if (!pointUnitList.Any(p => p.X == minX && p.Y == maxY))
                    return false;

                if (!pointUnitList.Any(p => p.X == minX && p.Y == maxY - 2))
                    return false;

                if (!pointUnitList.Any(p => p.X == minX && p.Y == maxY - 3))
                    return false;

                if (!pointUnitList.Any(p => p.X == minX && p.Y == maxY - 4))
                    return false;

                if (!pointUnitList.Any(p => p.X == minX + 1 && p.Y == maxY))
                    return false;

                if (!pointUnitList.Any(p => p.X == minX + 1 && p.Y == maxY - 2))
                    return false;

                if (!pointUnitList.Any(p => p.X == minX + 1 && p.Y == maxY - 4))
                    return false;

                if (!pointUnitList.Any(p => p.X == minX + 2 && p.Y == maxY))
                    return false;

                if (!pointUnitList.Any(p => p.X == minX + 2 && p.Y == maxY - 1))
                    return false;
                if (!pointUnitList.Any(p => p.X == minX + 2 && p.Y == maxY - 2))
                    return false;
                if (!pointUnitList.Any(p => p.X == minX + 2 && p.Y == maxY - 4))
                    return false;
                BlockBonusUnit.Clear();
                return true;
            }
            return false;
        }
    }
}
