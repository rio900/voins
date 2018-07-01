using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Voins.AI;
using Windows.Foundation;
using Windows.UI.Xaml.Controls;

namespace Voins.AppCode
{
    public class MapGenerator
    {
        public void CreateFarmingMap(Map map, int step, int stepUnit, List<Player> players)
        {
            ///Создаются ячейки
            ///и не пробиваймые блоки
            map.CreateRandomMap(true);
            ///Размещаем игроков
            foreach (var item in players)
            {
                map.KillsDeath.Add(new Point(0, 0));
                map.CreatePlayer(item);
            }

            map.Step = step;
            map.StepMobs = stepUnit;

            map.CreateBlock();
            map.CreateMobs();
        }

        /// <summary>
        /// Создает на карте мобов монетки
        /// которые если видят идут атаковать игроков
        /// </summary>
        /// <param name="mopStep">Шаг появления мобов</param>
        public static void CreateMobsCoopEasyMob_Random(Map map, int mopStep, EMobType mobType)
        {
            #region CreateMobs
            int randInt = 0;

            foreach (var item in map.Calls)
            {
                ///Проверка ячеек которые не могут быть заполнены
                if (!item.Used &&
                     !item.Block)
                {
                    Random random = new Random((int)DateTime.Now.Ticks + randInt++);
                    int rand = random.Next(0, 100);

                    if (rand < mopStep)
                    {
                        Unit unit;

                        if (mobType == EMobType.RangeBall)
                        {
                            unit = UnitGenerator.M1_BallRange(item.IndexLeft, item.IndexTop, map);
                            unit.AI = new AI_CoopAiRange() { CurrentMap = map, CurrentUnit = unit };
                        }
                        else
                        {
                            unit = UnitGenerator.M1_Ball(item.IndexLeft, item.IndexTop, map);
                            unit.AI = new AI_CoopAi() { CurrentMap = map, CurrentUnit = unit };
                        }

                        unit.MaxHealth = unit.MaxHealth * map.CoopTimer;
                        unit.Health = unit.Health * map.CoopTimer;
                        unit.Demage = unit.Demage * map.CoopTimer / 2;

                        ///И его же добавим в масив всех объектов
                        map.GameObjectInCall.Add(unit.GameObject);
                        ///Добавляем в список всех юнитов
                        item.IUnits.Add(unit);
                        item.Used = true;
                        unit.AI.StartAI();

                        ///Отображение
                        map.MapCanvas.Children.Add(unit.GameObject.View);


                    }
                }
            }
            #endregion
        }


        /// <summary>
        /// Создает на карте мобов монетки
        /// которые если видят идут атаковать игроков
        /// </summary>
        /// <param name="mopStep">Шаг появления мобов</param>
        public static void CreateMobsCoopEasyMob(Map map, int mopCount, EMobType mobType)
        {
            #region CreateMobs
            int randInt = 0;

            for (int i = 0; i < mopCount; i++)
            {
                ///Свободные ячейки
                List<Map_Cell> call = map.Calls.Where(p => !p.Used &&
                     !p.Block).ToList();

                Random random = new Random((int)DateTime.Now.Ticks + randInt++);
                int rand = random.Next(0, call.Count);

                Unit unit;
                if (mobType == EMobType.RangeBall)
                {
                    unit = UnitGenerator.M1_BallRange(call[rand].IndexLeft, call[rand].IndexTop, map);
                    unit.AI = new AI_CoopAiRange() { CurrentMap = map, CurrentUnit = unit };
                }
                else if (mobType == EMobType.BlueBall)
                {
                    unit = UnitGenerator.M1_Ball_BlueRange(call[rand].IndexLeft, call[rand].IndexTop, map);
                    unit.AI = new AI_CoopAiRange() { CurrentMap = map, CurrentUnit = unit };
                }
                else if (mobType == EMobType.RedBall)
                {
                    unit = UnitGenerator.M1_Ball_FireRange(call[rand].IndexLeft, call[rand].IndexTop, map);
                    unit.AI = new AI_CoopAiRange() { CurrentMap = map, CurrentUnit = unit };
                }
                else
                {
                    unit = UnitGenerator.M1_Ball(call[rand].IndexLeft, call[rand].IndexTop, map);
                    unit.AI = new AI_CoopAi() { CurrentMap = map, CurrentUnit = unit };
                }

                unit.MaxHealth = unit.MaxHealth * map.CoopTimer;
                unit.Health = unit.Health * map.CoopTimer;
                unit.Demage = unit.Demage * map.CoopTimer / 2;
                unit.NGold = unit.NGold;

                ///И его же добавим в масив всех объектов
                map.GameObjectInCall.Add(unit.GameObject);
                ///Добавляем в список всех юнитов
                call[rand].IUnits.Add(unit);
                call[rand].Used = true;
                unit.AI.StartAI();

                ///Отображение
                map.MapCanvas.Children.Add(unit.GameObject.View);
            }

            #endregion
        }

        /// <summary>
        /// Первый уровень кампании
        /// </summary>
        public static void Level_1_Green(Map map)
        {
            #region CreateMobs
            List<int> unitIndex1 = new List<int>() { 19, 23, 32, 33, 34, 35, 36, 49, 58, 62, 71, 73, 75, 84, 85, 86, 87, 88, 110, 111, 112, 125, 136, 137, 138, 139, 140, 162, 163, 164, 166, 175, 177, 179, 188, 190, 191, 192, };

            foreach (var item in unitIndex1)
            {
                Unit block1 = UnitGenerator.GrassBlock(map.Calls[item].IndexLeft, map.Calls[item].IndexTop);
                map.CreateObjectUnitInCall(map.Calls[item], block1);
            }

            //for (int i = 0; i < mopCount; i++)
            //{



            //    ///Свободные ячейки
            //    List<Map_Cell> call = map.Calls.Where(p => !p.Used &&
            //         !p.Block).ToList();

            //    Random random = new Random((int)DateTime.Now.Ticks + randInt++);
            //    int rand = random.Next(0, call.Count);

            //    Unit unit;
            //    if (mobType == EMobType.RangeBall)
            //    {
            //        unit = UnitGenerator.M1_BallRange(call[rand].IndexLeft, call[rand].IndexTop, map);
            //        unit.AI = new AI_CoopAiRange() { CurrentMap = map, CurrentUnit = unit };
            //    }
            //    else
            //    {
            //        unit = UnitGenerator.M1_Ball(call[rand].IndexLeft, call[rand].IndexTop, map);
            //        unit.AI = new AI_CoopAi() { CurrentMap = map, CurrentUnit = unit };
            //    }

            //    unit.MaxHealth = unit.MaxHealth * map.CoopTimer;
            //    unit.Health = unit.Health * map.CoopTimer;
            //    unit.Demage = unit.Demage * map.CoopTimer / 2;

            //    ///И его же добавим в масив всех объектов
            //    map.GameObjectInCall.Add(unit.GameObject);
            //    ///Добавляем в список всех юнитов
            //    call[rand].IUnits.Add(unit);
            //    call[rand].Used = true;
            //    unit.AI.StartAI();

            //    ///Отображение
            //    map.MapCanvas.Children.Add(unit.GameObject.View);

            //}

            #endregion
        }

        /// <summary>
        /// Фарм карты
        /// </summary>
        public static void FarmGameMap(Map map, int mapNumber, int mod)
        {
            List<int> unitIndex1 = new List<int>();
            List<int> unitIndex3 = new List<int>();
            #region CreateMobs
            if (mapNumber == 1)
            {
                unitIndex1 = new List<int>() { 6, 15, 23, 27, 29, 32, 35, 37, 41, 49, 53, 55, 58, 61, 63, 67, 75, 79, 81, 84, 87, 89, 93, 101, 104, 105, 107, 110, 113, 115, 116, 119, 127, 131, 133, 136, 139, 141, 145, 153, 157, 159, 162, 165, 167, 171, 179, 183, 185, 188, 191, 193, 197, 205, 214 };
                unitIndex3 = new List<int>() { 56, 60, 71, 97, 108, 112, 123, 149, 160, 164 };
                CreateBlock(map, mod, unitIndex1);
            }
            else if (mapNumber == 2)
            {
                unitIndex1 = new List<int>() { 6, 26, 27, 28, 29, 30, 31, 33, 34, 35, 36, 37, 38, 45, 58, 71, 84, 97, 104, 106, 107, 108, 109, 111, 112, 113, 114, 116, 123, 136, 149, 162, 175, 182, 183, 184, 185, 186, 187, 189, 190, 191, 192, 193, 194, 214 };
                unitIndex3 = new List<int>() { 55, 61, 81, 87, 133, 139, 159, 165 };

                List<int> unitIndex2 = new List<int>() { 110 };
                CreateBlock(map, mod, unitIndex1);
                foreach (var item in unitIndex2)
                {
                    Unit mob1 = UnitGenerator.M1_Ball_BlueRange(map.Calls[item].IndexLeft, map.Calls[item].IndexTop, map);
                    mob1.NExp = 100 * mod;
                    mob1.NGold = 100 * mod;
                    mob1.MaxHealth = 1000;
                    mob1.Health = 1000;
                    mob1.Demage = 50;
                    mob1.AI = new AI_CoopAiRange() { CurrentMap = map, CurrentUnit = mob1 };
                    mob1.AI.StartAI();
                    map.CreateObjectUnitInCall(map.Calls[item], mob1);
                }
            }
            else if (mapNumber == 3) 
            {
                unitIndex1 = new List<int>() { 2, 6, 10, 19, 27, 28, 29, 32, 35, 36, 37, 41, 45, 49, 54, 58, 62, 67, 71, 75, 80, 84, 88, 93, 95, 97, 99, 101, 104, 105, 106, 109, 111, 114, 115, 116, 119, 121, 123, 125, 127, 132, 136, 140, 145, 149, 153, 158, 162, 166, 171, 175, 179, 183, 184, 185, 188, 191, 192, 193, 201, 210, 214, 218 };
                unitIndex3 = new List<int>() { 31, 33, 83, 85, 135, 137, 187, 189 };
                List<int> unitIndex2 = new List<int>() { 108, 110, 112 };
                CreateBlock(map, mod, unitIndex1);
                foreach (var item in unitIndex2)
                {
                    Unit mob1 = UnitGenerator.M1_Ball_BlueRange(map.Calls[item].IndexLeft, map.Calls[item].IndexTop, map);
                    mob1.NExp = 50 * mod;
                    mob1.NGold = 50 * mod;
                    mob1.MaxHealth = 100;
                    mob1.Health = 1000;
                    mob1.Demage = 50;
                    mob1.AI = new AI_CoopAiRange() { CurrentMap = map, CurrentUnit = mob1 };
                    mob1.AI.StartAI();
                    map.CreateObjectUnitInCall(map.Calls[item], mob1);
                }
            }
            else if (mapNumber == 4)
            {
                unitIndex1 = new List<int>() { 6, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 53, 55, 56, 57, 58, 59, 60, 61, 63, 79, 81, 83, 84, 85, 87, 89, 104, 105, 107, 109, 111, 113, 115, 116, 131, 133, 135, 136, 137, 139, 141, 157, 159, 160, 161, 162, 163, 165, 167, 183, 184, 185, 186, 187, 188, 189, 190, 191, 192, 193, 214 };
                unitIndex3 = new List<int>() { 54, 62,  108, 112, 158, 166 };
                List<int> unitIndex2 = new List<int>() { 110 };
                CreateBlock(map, mod, unitIndex1);
                foreach (var item in unitIndex2)
                {
                    Unit mob1 = UnitGenerator.M1_Ball_FireRange(map.Calls[item].IndexLeft, map.Calls[item].IndexTop, map);
                    mob1.NExp = 50 * mod;
                    mob1.NGold = 50 * mod;
                    mob1.MaxHealth = 1000;
                    mob1.Health = 1000;
                    mob1.Demage = 70;
                    mob1.AI = new AI_CoopAiRangeFire() { CurrentMap = map, CurrentUnit = mob1 };
                    mob1.AI.StartAI();
                    map.CreateObjectUnitInCall(map.Calls[item], mob1);
                }

                List<int> unitIndex4 = new List<int>() { 71, 149 };
                foreach (var item in unitIndex4)
                {
                    Unit mob1 = UnitGenerator.M1_Ball_BlueRange(map.Calls[item].IndexLeft, map.Calls[item].IndexTop, map);
                    mob1.NExp = 50 * mod;
                    mob1.NGold = 50 * mod;
                    mob1.MaxHealth = 1000;
                    mob1.Health = 1000;
                    mob1.Demage = 50;
                    mob1.AI = new AI_CoopAiRange() { CurrentMap = map, CurrentUnit = mob1 };
                    mob1.AI.StartAI();
                    map.CreateObjectUnitInCall(map.Calls[item], mob1);
                }
            }
           
            CreateMobs(map, mod, unitIndex3);
            //for (int i = 0; i < mopCount; i++)
            //{

            //    ///Свободные ячейки
            //    List<Map_Cell> call = map.Calls.Where(p => !p.Used &&
            //         !p.Block).ToList();

            //    Random random = new Random((int)DateTime.Now.Ticks + randInt++);
            //    int rand = random.Next(0, call.Count);

            //    Unit unit;
            //    if (mobType == EMobType.RangeBall)
            //    {
            //        unit = UnitGenerator.M1_BallRange(call[rand].IndexLeft, call[rand].IndexTop, map);
            //        unit.AI = new AI_CoopAiRange() { CurrentMap = map, CurrentUnit = unit };
            //    }
            //    else
            //    {
            //        unit = UnitGenerator.M1_Ball(call[rand].IndexLeft, call[rand].IndexTop, map);
            //        unit.AI = new AI_CoopAi() { CurrentMap = map, CurrentUnit = unit };
            //    }

            //    unit.MaxHealth = unit.MaxHealth * map.CoopTimer;
            //    unit.Health = unit.Health * map.CoopTimer;
            //    unit.Demage = unit.Demage * map.CoopTimer / 2;

            //    ///И его же добавим в масив всех объектов
            //    map.GameObjectInCall.Add(unit.GameObject);
            //    ///Добавляем в список всех юнитов
            //    call[rand].IUnits.Add(unit);
            //    call[rand].Used = true;
            //    unit.AI.StartAI();

            //    ///Отображение
            //    map.MapCanvas.Children.Add(unit.GameObject.View);

            //}

            #endregion
        }

        /// <summary>
        /// Дота карты
        /// </summary>
        public static void SpecialGameMap(Map map, int mapNumber, int mod)
        {
            List<int> unitIndex1 = new List<int>();
            List<int> unitIndex3 = new List<int>();
            #region CreateMobs
            if (mapNumber == 1)
            {
                List<int> unitIndex0 = new List<int>() { 0, 1, 3, 5, 7, 9, 13, 24, 28, 30, 31, 33, 35, 39, 44, 51, 55, 59, 62, 65, 68, 70, 77, 86, 88, 91, 95, 98, 101, 103, 106, 117, 122, 127, 129, 132, 133, 138, 148, 149, 155, 156, 158, 165, 174, 176, 181, 185, 189, 191, 196, 207, 211, 213, 216, 220 };
                CreateStoun(map, mod, unitIndex0);

                unitIndex1 = new List<int>() { 2, 4, 6, 8, 26, 32, 41, 42, 46, 47, 52, 54, 57, 71, 75, 78, 80, 82, 90, 94, 100, 104, 109, 112, 113, 114, 116, 119, 120, 121, 130, 139, 140, 142, 143, 161, 163, 166, 168, 175, 178, 179, 186, 187, 194, 212, 214, 215, 217, 218, 219 };
                unitIndex3 = new List<int>() { 43, 81, 83, 177, 188 };

                List<int> unitIndex2 = new List<int>() { 123 };
                CreateBlock(map, mod, unitIndex1);
                foreach (var item in unitIndex2)
                {
                    Unit mob1 = UnitGenerator.M1_Ball_FireRange(map.Calls[item].IndexLeft, map.Calls[item].IndexTop, map);
                    mob1.NExp = 50 * mod;
                    mob1.NGold = 50 * mod;
                    mob1.MaxHealth = 1000;
                    mob1.Health = 1000;
                    mob1.Demage = 70;
                    mob1.AI = new AI_CoopAiRangeFire() { CurrentMap = map, CurrentUnit = mob1 };
                    mob1.AI.StartAI();
                    map.CreateObjectUnitInCall(map.Calls[item], mob1);
                }
            }
            else if (mapNumber == 2)
            {
           
            }
            else if (mapNumber == 3)
            {
             
            }
            else if (mapNumber == 4)
            {
             
            }

            CreateMobs(map, mod, unitIndex3);
            //for (int i = 0; i < mopCount; i++)
            //{

            //    ///Свободные ячейки
            //    List<Map_Cell> call = map.Calls.Where(p => !p.Used &&
            //         !p.Block).ToList();

            //    Random random = new Random((int)DateTime.Now.Ticks + randInt++);
            //    int rand = random.Next(0, call.Count);

            //    Unit unit;
            //    if (mobType == EMobType.RangeBall)
            //    {
            //        unit = UnitGenerator.M1_BallRange(call[rand].IndexLeft, call[rand].IndexTop, map);
            //        unit.AI = new AI_CoopAiRange() { CurrentMap = map, CurrentUnit = unit };
            //    }
            //    else
            //    {
            //        unit = UnitGenerator.M1_Ball(call[rand].IndexLeft, call[rand].IndexTop, map);
            //        unit.AI = new AI_CoopAi() { CurrentMap = map, CurrentUnit = unit };
            //    }

            //    unit.MaxHealth = unit.MaxHealth * map.CoopTimer;
            //    unit.Health = unit.Health * map.CoopTimer;
            //    unit.Demage = unit.Demage * map.CoopTimer / 2;

            //    ///И его же добавим в масив всех объектов
            //    map.GameObjectInCall.Add(unit.GameObject);
            //    ///Добавляем в список всех юнитов
            //    call[rand].IUnits.Add(unit);
            //    call[rand].Used = true;
            //    unit.AI.StartAI();

            //    ///Отображение
            //    map.MapCanvas.Children.Add(unit.GameObject.View);

            //}

            #endregion
        }


        private static void CreateMobs(Map map, int mod, List<int> unitIndex3)
        {

            foreach (var item in unitIndex3)
            {
                Unit mob1 = UnitGenerator.M1_Ball(map.Calls[item].IndexLeft, map.Calls[item].IndexTop, map);
                mob1.NExp *= mod;
                mob1.NGold *= mod;
                mob1.AI = new AI_EasyAi() { CurrentMap = map, CurrentUnit = mob1 };
                mob1.AI.StartAI();
                map.CreateObjectUnitInCall(map.Calls[item], mob1);
            }
        }

        private static void CreateBlock(Map map, int mod, List<int> unitIndex1)
        {
            foreach (var item in unitIndex1)
            {
                Unit block1 = UnitGenerator.GrassBlock(map.Calls[item].IndexLeft, map.Calls[item].IndexTop);
                block1.NExp *= mod;
                block1.NGold *= mod;
                map.CreateObjectUnitInCall(map.Calls[item], block1);
            }
        }

        private static void CreateStoun(Map map, int mod, List<int> unitIndex1)
        {
            foreach (var item in unitIndex1)
            {
                Unit unit = new Unit()
                {
                    PositionX = map.Calls[item].IndexLeft,
                    PositionY = map.Calls[item].IndexTop
                };

                UC_Block uc_Block = new UC_Block()
                {
                    Width = 50,
                    Height = 50
                };
                Canvas.SetLeft(uc_Block, map.Calls[item].IndexLeft * 50);
                Canvas.SetTop(uc_Block, map.Calls[item].IndexTop * 50);

                ///Непроходимый блок
                Game_Object_In_Call block = new Game_Object_In_Call()
                {
                    EnumCallType = EnumCallType.Block,
                    View = uc_Block
                };
                unit.GameObject = block;

                map.Calls[item].Block = true;
                ///В этой ячейке находится непроходимый блок
                map.Calls[item].IUnits.Add(unit);
                ///И его же добавим в масив всех объектов
                map.GameObjectInCall.Add(block);
                ///Отображение
                map.MapCanvas.Children.Add(block.View);
            }
        }
    }
}
