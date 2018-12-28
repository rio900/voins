using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Voins.AI;
using Voins.Spell;
using Windows.Foundation;
using Windows.UI.Xaml.Controls;

namespace Voins.AppCode
{

    public static class UnitGenerator
    {
        #region Units&Items&Players
        public static Player Player_Jinx(string name, int number, Map map)
        {
            Player jinx = new Player()
            {
                PlayerNumber = number,
                CurrentMap = map,
                Agility = 14,
                Intelligence = 15,
                Strength = 12,
                Mana = 15 * 5,
                Health = 11 * 5,
                MaxExp = 100,
                HeroType = EHeroType.Agility,
                Name = name,
                AddAgility = 3,
                AddIntelligence = 1,
                AddStrength = 2,
                Gold = 100,
                OrijAttackSpeed = 1.0 - 14 * StaticVaribl.AgilityAttackSpeed,
                Angel = EAngel.Bottom,
                NExp = 100,
                Range = 6
            };

            SP_Move move = new SP_Move() { Name = "Move" };
            jinx.Spells.Add(move);

            SP_Jinx_Switcheroo switcheroo = new SP_Jinx_Switcheroo() { Name = "Switcheroo" };
            jinx.Spells.Add(switcheroo);
            // switcheroo.UpSpell(jinx);

            SP_Jinx_Zap zap = new SP_Jinx_Zap() { Name = "Jinx_Zap" };
            jinx.Spells.Add(zap);
            zap.UpSpell(jinx);

            SP_Jinx_Flame_Chompers flameChompers = new SP_Jinx_Flame_Chompers() { Name = "Flame_Chompers" };
            jinx.Spells.Add(flameChompers);
            // flameChompers.UpSpell(jinx);

            SP_Super_Mega_Death_Rocket superMegaDeathRocket = new SP_Super_Mega_Death_Rocket() { Name = "Super Mega Death Rocket" };
            jinx.Spells.Add(superMegaDeathRocket);
            //superMegaDeathRocket.UpSpell(jinx);

            UC_Player ucPlayer = new UC_Player();
            ucPlayer.PlayerNumber = number;
            ucPlayer.ChengAngel(EAngel.Bottom);

            Game_Object_In_Call gameObj = new Game_Object_In_Call() { EnumCallType = EnumCallType.Player, View = ucPlayer };
            jinx.GameObject = gameObj;

            return jinx;
        }

        public static Player Player_Mirana(string name, int number, Map map)
        {
            Player mirana = new Player()
            {
                PlayerNumber = number,
                CurrentMap = map,
                Agility = 14,
                Intelligence = 13,
                Strength = 12,
                Mana = 13 * 5,
                Health = 12 * 5,
                MaxExp = 100,
                HeroType = EHeroType.Agility,
                Name = name,
                AddAgility = 3,
                AddIntelligence = 1,
                AddStrength = 2,
                Gold = 100,
                OrijAttackSpeed = 1.0 - 14 * StaticVaribl.AgilityAttackSpeed,
                Angel = EAngel.Bottom,
                NExp = 100,
                Range = 6
            };

            SP_Move move = new SP_Move() { Name = "Move" };
            mirana.Spells.Add(move);

            SP_Mirana_Starstorm starstorm = new SP_Mirana_Starstorm() { Name = "Starstorm" };
            mirana.Spells.Add(starstorm);
            starstorm.UpSpell(mirana);

            SP_Mirana_SacredArrow sacredArrow = new SP_Mirana_SacredArrow() { Name = "SacredArrow" };
            mirana.Spells.Add(sacredArrow);
            //  sacredArrow.UpSpell(mirana);

            SP_Mirana_Leap leap = new SP_Mirana_Leap() { Name = "Leap" };
            mirana.Spells.Add(leap);
            //    leap.UpSpell(mirana);

            SP_Mirana_MoonlightShadow moonlightShadow = new SP_Mirana_MoonlightShadow() { Name = "Moonlight Shadow" };
            mirana.Spells.Add(moonlightShadow);
            //    moonlightShadow.UpSpell(mirana);

            UC_Player ucPlayer = new UC_Player();
            ucPlayer.PlayerNumber = number;
            ucPlayer.ChengAngel(EAngel.Bottom);

            Game_Object_In_Call gameObj = new Game_Object_In_Call() { EnumCallType = EnumCallType.Player, View = ucPlayer };
            mirana.GameObject = gameObj;

            return mirana;
        }

        public static Player Player_Jakiro(string name, int number, Map map)
        {
            Player jakiro = new Player()
            {
                PlayerNumber = number,
                CurrentMap = map,
                Agility = 6,
                Intelligence = 18,
                Strength = 16,
                Mana = 18 * 5,
                Health = 16 * 5,
                MaxExp = 100,
                HeroType = EHeroType.Intelligence,
                Name = name,
                AddAgility = 1,
                AddIntelligence = 3,
                AddStrength = 2,
                Gold = 100,
                OrijAttackSpeed = 1.0 - 6 * StaticVaribl.AgilityAttackSpeed,//0.95
                Angel = EAngel.Bottom,
                NExp = 100,
                Range = 6
            };

            SP_Move move = new SP_Move() { Name = "Move" };
            jakiro.Spells.Add(move);

            SP_Dual_Breath dualBreath = new SP_Dual_Breath() { Name = "DualBreath" };
            jakiro.Spells.Add(dualBreath);
            dualBreath.UpSpell(jakiro);

            SP_Jakiro_Ice_Path icePath = new SP_Jakiro_Ice_Path() { Name = "IcePath" };
            jakiro.Spells.Add(icePath);
            //  icePath.UpSpell(jakiro);

            SP_Jakiro_Liquid_Fire liquidFire = new SP_Jakiro_Liquid_Fire() { Name = "LiquidFire" };
            jakiro.Spells.Add(liquidFire);
            //               liquidFire.UpSpell(jakiro);

            SP_Jakiro_Macropyre macropyre = new SP_Jakiro_Macropyre() { Name = "Macropyre" };
            jakiro.Spells.Add(macropyre);
            // macropyre.UpSpell(jakiro);

            UC_Player ucPlayer = new UC_Player();
            ucPlayer.PlayerNumber = number;
            ucPlayer.ChengAngel(EAngel.Bottom);

            Game_Object_In_Call gameObj = new Game_Object_In_Call() { EnumCallType = EnumCallType.Player, View = ucPlayer };
            jakiro.GameObject = gameObj;

            return jakiro;
        }


        public static Player Player_Nature(string name, int number, Map map)
        {
            Player nature = new Player()
            {
                PlayerNumber = number,
                CurrentMap = map,
                Agility = 11,
                Intelligence = 17,
                Strength = 12,
                Mana = 17 * 5,
                Health = 12 * 5,
                MaxExp = 100,
                HeroType = EHeroType.Intelligence,
                Name = name,
                AddAgility = 2,
                AddIntelligence = 3,
                AddStrength = 2,
                Gold = 100,
                OrijAttackSpeed = 1.0 - 11 * StaticVaribl.AgilityAttackSpeed,
                Angel = EAngel.Bottom,
                NExp = 100,
                Range = 6
            };

            SP_Move move = new SP_Move() { Name = "Move" };
            nature.Spells.Add(move);

            SP_Nature_Sprout natureSprout = new SP_Nature_Sprout() { Name = "NatureSprout" };
            nature.Spells.Add(natureSprout);
            natureSprout.UpSpell(nature);

            SP_Nature_Teleport natureTeleport = new SP_Nature_Teleport() { Name = "NatureTeleport" };
            nature.Spells.Add(natureTeleport);
            // natureTeleport.UpSpell(nature);

            SP_Nature_Trent natureTrent = new SP_Nature_Trent() { Name = "NatureTrent" };
            nature.Spells.Add(natureTrent);
            // natureTrent.UpSpell(nature);

            SP_Nature_Wrath natureWrath = new SP_Nature_Wrath() { Name = "NatureWrath" };
            nature.Spells.Add(natureWrath);
            // natureWrath.UpSpell(nature);

            UC_Player ucPlayer = new UC_Player();
            ucPlayer.PlayerNumber = number;
            ucPlayer.ChengAngel(EAngel.Bottom);

            Game_Object_In_Call gameObj = new Game_Object_In_Call() { EnumCallType = EnumCallType.Player, View = ucPlayer };
            nature.GameObject = gameObj;

            return nature;
        }

        public static Player Player_Sf(string name, int number, Map map)
        {
            Player sf = new Player()
            {
                PlayerNumber = number,
                CurrentMap = map,
                Agility = 14,
                Intelligence = 12,
                Strength = 10,
                Mana = 12 * 5,
                Health = 10 * 5,
                MaxExp = 100,
                HeroType = EHeroType.Agility,
                Name = name,
                AddAgility = 2,
                AddIntelligence = 2,
                AddStrength = 2,
                Gold = 100,
                OrijAttackSpeed = 1.0 - 14 * StaticVaribl.AgilityAttackSpeed,
                Angel = EAngel.Bottom,
                NExp = 100,
                Range = 6
            };

            SP_Move move = new SP_Move() { Name = "Move" };
            sf.Spells.Add(move);

            SP_Sf_Coil sfCoil = new SP_Sf_Coil(1) { Name = "SP_Sf_Coil" };
            sf.Spells.Add(sfCoil);
            //sfCoil.UpSpell(sf);

            SP_Sf_Coil sfCoil2 = new SP_Sf_Coil(2) { Name = "SP_Sf_Coil2" };
            sf.Spells.Add(sfCoil2);
            //sfCoil2.UpSpell(sf);

            SP_Sf_Attack sfAttack = new SP_Sf_Attack() { Name = "SP_Sf_Attack" };
            sf.Spells.Add(sfAttack);
            sfAttack.UpSpell(sf);

            SP_Sf_Ult sfUlt = new SP_Sf_Ult() { Name = "SP_Sf_Ult" };
            sf.Spells.Add(sfUlt);
            //sfUlt.UpSpell(sf);

            UC_Player ucPlayer = new UC_Player();
            ucPlayer.PlayerNumber = number;
            ucPlayer.ChengAngel(EAngel.Bottom);

            Game_Object_In_Call gameObj = new Game_Object_In_Call() { EnumCallType = EnumCallType.Player, View = ucPlayer };
            sf.GameObject = gameObj;

            return sf;
        }


        public static Player Player_Sniper(string name, int number, Map map)
        {
            Player sniper = new Player()
            {
                PlayerNumber = number,
                CurrentMap = map,
                Agility = 14,
                Intelligence = 10,
                Strength = 11,
                Mana = 10 * 5,
                Health = 11 * 5,
                MaxExp = 100,
                HeroType = EHeroType.Agility,
                Name = name,
                AddAgility = 3,
                AddIntelligence = 2,
                AddStrength = 1,
                Gold = 100,
                OrijAttackSpeed = 1.0 - 14 * StaticVaribl.AgilityAttackSpeed,
                Angel = EAngel.Bottom,
                //NGold = 100,
                NExp = 100,
                Range = 6
            };

            SP_Move move = new SP_Move() { Name = "Move" };
            sniper.Spells.Add(move);

            SP_Sniper_Shrapnel sniperShrapnel = new SP_Sniper_Shrapnel() { Name = "SniperShrapnel" };
            sniper.Spells.Add(sniperShrapnel);
            sniperShrapnel.UpSpell(sniper);

            SP_Sniper_Headshot sniperHeadshot = new SP_Sniper_Headshot() { Name = "SniperHeadshot" };
            sniper.Spells.Add(sniperHeadshot);
            //  sniperHeadshot.UpSpell(sniper);

            SP_Sniper_TakeAim invisSpeed = new SP_Sniper_TakeAim() { Name = "TakeAim" };
            sniper.Spells.Add(invisSpeed);
            //  invisSpeed.UpSpell(sniper);

            SP_Sniper_Assassinate assassinate = new SP_Sniper_Assassinate() { Name = "Assassinate" };
            sniper.Spells.Add(assassinate);
            //assassinate.UpSpell(sniper);

            UC_Player ucPlayer = new UC_Player();
            ucPlayer.PlayerNumber = number;
            ucPlayer.ChengAngel(EAngel.Bottom);

            Game_Object_In_Call gameObj = new Game_Object_In_Call() { EnumCallType = EnumCallType.Player, View = ucPlayer };
            sniper.GameObject = gameObj;

            return sniper;
        }

        public static Player Player_Bonik(string name, int number, Map map)
        {
            Player bonik = new Player()
            {
                PlayerNumber = number,
                CurrentMap = map,
                Agility = 15,
                Intelligence = 10,
                Strength = 10,
                Mana = 10 * 5,
                Health = 10 * 5,
                MaxExp = 100,
                HeroType = EHeroType.Agility,
                Name = name,
                AddAgility = 3,
                AddIntelligence = 1,
                AddStrength = 2,
                Gold = 100,
                OrijAttackSpeed = 1.0 - 20 * StaticVaribl.AgilityAttackSpeed,
                Angel = EAngel.Bottom,
                //NGold = 100,
                NExp = 100,
                Range = 6
            };

            SP_Move move = new SP_Move() { Name = "Move" };
            bonik.Spells.Add(move);

            SP_AttackSpeed attackSpeed = new SP_AttackSpeed() { Name = "AttackSpeed" };
            bonik.Spells.Add(attackSpeed);
            //    attackSpeed.UpSpell(bonik);

            ///По умолчанию прокачаны огненные стрелы
            SP_FireArrow fireArrow = new SP_FireArrow() { Name = "Arrow" };
            bonik.Spells.Add(fireArrow);
            fireArrow.UpSpell(bonik);

            SP_InvisibilitiSpeed invisSpeed = new SP_InvisibilitiSpeed() { Name = "InvisSpeed" };
            bonik.Spells.Add(invisSpeed);
            //  invisSpeed.UpSpell(bonik);

            SP_BonikUlt bonikUlt = new SP_BonikUlt() { Name = "BonikUlt" };
            bonik.Spells.Add(bonikUlt);
            //     bonikUlt.UpSpell(bonik);

            UC_Player ucPlayer = new UC_Player();
            ucPlayer.PlayerNumber = number;
            ucPlayer.ChengAngel(EAngel.Bottom);

            Game_Object_In_Call gameObj = new Game_Object_In_Call() { EnumCallType = EnumCallType.Player, View = ucPlayer };
            bonik.GameObject = gameObj;

            return bonik;
        }

        public static Player Player_Alchemist(string name, int number, Map map)
        {
            Player alchemist = new Player()
            {
                PlayerNumber = number,
                CurrentMap = map,
                Agility = 7,
                Intelligence = 15,
                Strength = 16,
                Mana = 15 * 5,
                Health = 16 * 5,
                MaxExp = 100,
                HeroType = EHeroType.Strength,
                Name = name,
                AddAgility = 1,
                AddIntelligence = 2,
                AddStrength = 2,
                Gold = 100,
                OrijAttackSpeed = 1.15 - 10 * StaticVaribl.AgilityAttackSpeed,
                Angel = EAngel.Bottom,
                //NGold = 100,
                NExp = 100,
                Range = 1
            };

            SP_Move move = new SP_Move() { Name = "Move" };
            alchemist.Spells.Add(move);
            ///По умолчанию прокачана тучка
            SP_Alchemist_AcidSpray acidSpray = new SP_Alchemist_AcidSpray() { Name = "Acid Spray" };
            alchemist.Spells.Add(acidSpray);
            acidSpray.UpSpell(alchemist);

            SP_Alchemist_UnstableConcoction unstableConcoction = new SP_Alchemist_UnstableConcoction() { Name = "Unstable Concoction" };
            alchemist.Spells.Add(unstableConcoction);
            //  unstableConcoction.UpSpell(alchemist);

            SP_Alchemist_GreevilsGreed greevilsGreed = new SP_Alchemist_GreevilsGreed() { Name = "Greevils Greed" };
            alchemist.Spells.Add(greevilsGreed);
            //   greevilsGreed.UpSpell(alchemist);

            SP_Alchemist_ChemicalRage chemicalRage = new SP_Alchemist_ChemicalRage() { Name = "ChemicalRage" };
            alchemist.Spells.Add(chemicalRage);
            // chemicalRage.UpSpell(alchemist);

            UC_Player ucPlayer = new UC_Player();
            ucPlayer.PlayerNumber = number;
            ucPlayer.ChengAngel(EAngel.Bottom);

            Game_Object_In_Call gameObj = new Game_Object_In_Call() { EnumCallType = EnumCallType.Player, View = ucPlayer };
            alchemist.GameObject = gameObj;

            return alchemist;
        }

        public static Unit GrassBlock(int x, int y)
        {
            Random rand = new Random(x + y);
            Unit unit = new Unit()
            {
                UnitType = EUnitType.Grass,
                PositionX = x,
                PositionY = y,
                MaxHealth = 15,
                Health = 15,
                NExp = 15,
                //NExp = 1500,
                NGold = rand.Next(4, 6),
                GroupType = 50
            };

            //    unit.NItem = I12_Maelstrom();

            UC_CrushBlock view = new UC_CrushBlock();
            Canvas.SetLeft(view, x * 50);
            Canvas.SetTop(view, y * 50);

            unit.GameObject = new Game_Object_In_Call()
            {
                EnumCallType = EnumCallType.UnitBlock,
                View = view
            };
            return unit;
        }

        public static Unit M_Trent(int x, int y, Map map, Player player)
        {
            Random rand = new Random(x + y);
            Unit unit = new Unit()
            {
                UnitType = EUnitType.Enemy,
                PositionX = x,
                PositionY = y,
                MaxHealth = 60,
                Health = 60,
                NExp = 35,
                NGold = rand.Next(25, 30),
                Angel = EAngel.Bottom,
                CurrentMap = map,
                GroupType = player.GroupType,
                MasterPlayer = player,
                Demage = 15,
                Speed = 0.7
            };

            SP_Move move = new SP_Move() { Name = "Move" };
            unit.Spells.Add(move);
            SP_AttackEasyMob attackEasyMob = new SP_AttackEasyMob() { Name = "Attack Easy Mob" };
            unit.Spells.Add(attackEasyMob);

            AI_CoopAi ai = new AI_CoopAi() { CurrentMap = map, CurrentUnit = unit };
            unit.AI = ai;

            UC_Mob_1_Trent view = new UC_Mob_1_Trent();
            Canvas.SetLeft(view, x * 50);
            Canvas.SetTop(view, y * 50);
            view.ChengAngel(EAngel.Bottom);

            unit.GameObject = new Game_Object_In_Call()
            {
                EnumCallType = EnumCallType.Unit,
                View = view
            };
            return unit;
        }

        public static Unit M1_Ball(int x, int y, Map map)
        {
            Random rand = new Random(x + y);
            Unit unit = new Unit()
            {
                UnitType = EUnitType.Enemy,
                PositionX = x,
                PositionY = y,
                MaxHealth = 60,
                Health = 60,
                NExp = 35,
                NGold = rand.Next(25, 30),
                Angel = EAngel.Bottom,
                CurrentMap = map,
                GroupType = 100,
                Demage = 15,
                Speed = 0.7
            };

            SP_Move move = new SP_Move() { Name = "Move" };
            unit.Spells.Add(move);
            SP_AttackEasyMob attackEasyMob = new SP_AttackEasyMob() { Name = "Attack Easy Mob" };
            unit.Spells.Add(attackEasyMob);

            AI_EasyAi ai = new AI_EasyAi() { CurrentMap = map, CurrentUnit = unit };
            unit.AI = ai;

            UC_Mob_1_Ball view = new UC_Mob_1_Ball();
            Canvas.SetLeft(view, x * 50);
            Canvas.SetTop(view, y * 50);
            view.ChengAngel(EAngel.Bottom);

            unit.GameObject = new Game_Object_In_Call()
            {
                EnumCallType = EnumCallType.Unit,
                View = view
            };
            return unit;
        }

        public static Unit M1_BlackHunter(int x, int y, Map map)
        {
            Random rand = new Random(x + y);
            Unit unit = new Unit()
            {
                UnitType = EUnitType.Enemy,
                PositionX = x,
                PositionY = y,
                MaxHealth = 170,
                Health = 170,
                NExp = 70,
                NGold = rand.Next(150, 250),
                Angel = EAngel.Bottom,
                CurrentMap = map,
                GroupType = 100,
                Demage = 30,
                Speed = 0.6
            };

            SP_Move move = new SP_Move() { Name = "Move" };
            unit.Spells.Add(move);
            SP_AttackEasyMob attackEasyMob = new SP_AttackEasyMob() { Name = "Attack Easy Mob" };
            unit.Spells.Add(attackEasyMob);

            AI_EasyAi ai = new AI_EasyAi() { CurrentMap = map, CurrentUnit = unit };
            unit.AI = ai;

            UC_Mob_BlackHunter view = new UC_Mob_BlackHunter();
            Canvas.SetLeft(view, x * 50);
            Canvas.SetTop(view, y * 50);
            view.ChengAngel(EAngel.Bottom);

            unit.GameObject = new Game_Object_In_Call()
            {
                EnumCallType = EnumCallType.Unit,
                View = view
            };
            return unit;
        }

        public static Unit M1_BlackHunterBoss(int x, int y, Map map)
        {
            Random rand = new Random(x + y);
            Unit unit = new Unit()
            {
                UnitType = EUnitType.Boss,
                PositionX = x,
                PositionY = y,
                MaxHealth = 400,
                Health = 400,
                NExp = 100,
                NGold = rand.Next(500, 700),
                Angel = EAngel.Bottom,
                CurrentMap = map,
                GroupType = 100,
                Arrmor = 10,
                Demage = 50,
                Speed = 0.6,
                Range = 5
            };

            SP_Move move = new SP_Move() { Name = "Move" };
            unit.Spells.Add(move);
            SP_AttackEasyMob attackEasyMob = new SP_AttackEasyMob() { Name = "Attack Easy Mob" };
            unit.Spells.Add(attackEasyMob);
            SP_HunterBoss attackEasyMobRange = new SP_HunterBoss() { Name = " Hunter Boss" };
            unit.Spells.Add(attackEasyMobRange);

            AI_EasyAi ai = new AI_EasyAi() { CurrentMap = map, CurrentUnit = unit };
            unit.AI = ai;

            UC_Mob_BlackHunterBoss view = new UC_Mob_BlackHunterBoss();
            Canvas.SetLeft(view, x * 50);
            Canvas.SetTop(view, y * 50);
            view.ChengAngel(EAngel.Bottom);

            unit.GameObject = new Game_Object_In_Call()
            {
                EnumCallType = EnumCallType.Unit,
                View = view
            };
            return unit;
        }

        public static Unit M1_BallRange(int x, int y, Map map)
        {
            Random rand = new Random(x + y);
            Unit unit = new Unit()
            {
                UnitType = EUnitType.Enemy,
                PositionX = x,
                PositionY = y,
                MaxHealth = 200,
                Health = 200,
                NExp = 40,
                NGold = rand.Next(50, 80),
                Angel = EAngel.Bottom,
                CurrentMap = map,
                GroupType = 100,
                Demage = 20,
                Speed = 0.7,
                Range = 10
            };

            SP_Move move = new SP_Move() { Name = "Move" };
            unit.Spells.Add(move);
            SP_AttackEasyMob attackEasyMob = new SP_AttackEasyMob() { Name = "Attack Easy Mob" };
            unit.Spells.Add(attackEasyMob);
            SP_EasyMobArrow attackEasyMobRange = new SP_EasyMobArrow() { Name = "Attack Easy Mob Range" };
            unit.Spells.Add(attackEasyMobRange);

            UC_Mob_1_Ball view = new UC_Mob_1_Ball();
            Canvas.SetLeft(view, x * 50);
            Canvas.SetTop(view, y * 50);
            view.ChengAngel(EAngel.Bottom);

            unit.GameObject = new Game_Object_In_Call()
            {
                EnumCallType = EnumCallType.Unit,
                View = view
            };
            return unit;
        }
        public static Unit M1_Ball_BlueRange(int x, int y, Map map)
        {
            Random rand = new Random(x + y);
            Unit unit = new Unit()
            {
                UnitType = EUnitType.Enemy,
                PositionX = x,
                PositionY = y,
                MaxHealth = 200,
                Health = 200,
                NExp = 80,
                NGold = rand.Next(100, 150),
                Angel = EAngel.Bottom,
                CurrentMap = map,
                GroupType = 100,
                Demage = 25,
                Speed = 0.7,
                Range = 10
            };

            SP_Move move = new SP_Move() { Name = "Move" };
            unit.Spells.Add(move);
            SP_AttackEasyMob attackEasyMob = new SP_AttackEasyMob() { Name = "Attack Easy Mob" };
            unit.Spells.Add(attackEasyMob);
            SP_EasyMobArrowStun attackEasyMobRange = new SP_EasyMobArrowStun() { Name = "Attack Easy Mob Range" };
            unit.Spells.Add(attackEasyMobRange);

            UC_Mob_1_Ball_Blue view = new UC_Mob_1_Ball_Blue();
            Canvas.SetLeft(view, x * 50);
            Canvas.SetTop(view, y * 50);
            view.ChengAngel(EAngel.Bottom);

            unit.GameObject = new Game_Object_In_Call()
            {
                EnumCallType = EnumCallType.Unit,
                View = view
            };
            return unit;
        }

        public static Unit M1_Ball_FireRange(int x, int y, Map map)
        {
            Random rand = new Random(x + y);
            Unit unit = new Unit()
            {
                UnitType = EUnitType.Boss,
                PositionX = x,
                PositionY = y,
                MaxHealth = 250,
                Health = 250,
                Arrmor = 10,
                NExp = 100,
                NGold = rand.Next(300, 400),
                Angel = EAngel.Bottom,
                CurrentMap = map,
                GroupType = 100,
                Demage = 25,
                Speed = 0.7,
                Range = 10
            };

            SP_Move move = new SP_Move() { Name = "Move" };
            unit.Spells.Add(move);
            SP_AttackEasyMob attackEasyMob = new SP_AttackEasyMob() { Name = "Attack Easy Mob" };
            unit.Spells.Add(attackEasyMob);
            SP_EasyMobArrowFifeBall attackEasyMobRange = new SP_EasyMobArrowFifeBall() { Name = "Easy Mob Arrow Fife Ball" };
            unit.Spells.Add(attackEasyMobRange);

            UC_Mob_1_Fire view = new UC_Mob_1_Fire();
            Canvas.SetLeft(view, x * 50);
            Canvas.SetTop(view, y * 50);
            view.ChengAngel(EAngel.Bottom);

            unit.GameObject = new Game_Object_In_Call()
            {
                EnumCallType = EnumCallType.Unit,
                View = view
            };
            return unit;
        }



        public static ItemClass I1_SageMask()
        {
            ItemClass item = new ItemClass()
            {
                Name = "Sage Mask",
                ManaRegen = 3,
                Price = 50
            };
            item.Info.ShortDescription = "Sage Mask Description";

            item.View = new UC_View_ItemImage(0);
            return item;
        }

        public static ItemClass I2_RobeOfTheMagi()
        {
            ItemClass item = new ItemClass()
            {
                Name = "Robe Of The Magi",
                Intelligence = 4,
                Price = 75
            };
            item.Info.ShortDescription = "Robe Of The Magi Description";
            item.View = new UC_View_ItemImage(1);
            return item;
        }

        public static ItemClass I3_Quarterstuff()
        {
            ItemClass item = new ItemClass()
            {
                Name = "Quarterstuff",
                Demage = 3,
                AttackSpeed = 0.05,
                Price = 100
            };
            item.Info.ShortDescription = "Quarterstuff Description";

            item.View = new UC_View_ItemImage(2);
            return item;
        }

        public static ItemClass I4_OblivionStuff()
        {
            ItemClass item = new ItemClass()
            {
                Name = "Oblivion Stuff",
                Demage = 5,
                AttackSpeed = 0.05,
                Intelligence = 4,
                ManaRegen = 5,
                Price = 225
            };
            item.Info.ShortDescription = "Oblivion Stuff Description";

            item.Parts = new List<ItemClass>() {
            I1_SageMask(),
            I2_RobeOfTheMagi(),
            I3_Quarterstuff()
            };
            item.View = new UC_View_ItemImage(3);
            return item;
        }

        public static ItemClass I5_Orchid()
        {
            ItemClass item = new ItemClass()
            {
                Name = "Orchid",
                Demage = 10,
                AttackSpeed = 0.15,
                Intelligence = 15,
                ManaRegen = 10,
                Price = 550
            };

            item.Info.ShortDescription = "Orchid Description";

            SP_OrchidSilenc orchid = new SP_OrchidSilenc() { Name = "Orchid Silenc" };
            item.SpellItem = orchid;

            item.Parts = new List<ItemClass>() {
            I4_OblivionStuff(),
            I4_OblivionStuff(),
            I5_OrchidRecept()
            };

            item.View = new UC_View_ItemImage(4);
            item.View.UsingItem(item.SpellItem);
            return item;
        }

        public static ItemClass I5_OrchidRecept()
        {
            ItemClass item = new ItemClass()
            {
                Name = "Orchid Recept",
                Recept = true,
                Price = 100
            };

            item.Info.ShortDescription = "Orchid Recept Description";
            item.Info.BuffDescription = "Can be used" + Environment.NewLine + "Silence 4 sec."
                + Environment.NewLine + "+50% magic demage.";
            item.View = new UC_View_ItemImage(4);
            item.View.Recept();
            return item;
        }
        public static ItemClass I6_BootsOfSpeed()
        {
            ItemClass item = new ItemClass()
            {
                Name = "Boots Of Speed",
                Speed = 0.25, //0.35
                Boots = true,
                Price = 75
            };

            item.Info.ShortDescription = "Boots Of Speed";
            item.View = new UC_View_ItemImage(5);
            return item;
        }

        public static ItemClass I7_GlovesOfHaste()
        {
            ItemClass item = new ItemClass()
            {
                Name = "Gloves Of Haste",
                AttackSpeed = 0.075,
                Price = 75
            };
            item.Info.ShortDescription = "Gloves Of Haste Description";

            item.View = new UC_View_ItemImage(6);
            return item;
        }

        public static ItemClass I8_BeltOfStreangth()
        {
            ItemClass item = new ItemClass()
            {
                Name = "Belt Of Streangth",
                Strength = 4,
                Price = 75
            };
            item.Info.ShortDescription = "Belt Of Streangth Description";

            item.View = new UC_View_ItemImage(7);
            return item;
        }

        public static ItemClass I9_PowerTreads()
        {
            ItemClass item = new ItemClass()
            {
                Name = "Power Treads",
                Strength = 5,
                Speed = 0.30,
                AttackSpeed = 0.15,
                Boots = true,
                Price = 225
            };
            item.Info.ShortDescription = "Power Treads Description";

            item.Parts = new List<ItemClass>() {
            I6_BootsOfSpeed(),
            I7_GlovesOfHaste(),
            I8_BeltOfStreangth()
            };

            item.View = new UC_View_ItemImage(8);
            return item;
        }

        public static ItemClass I10_MithrilHammer()
        {
            ItemClass item = new ItemClass()
            {
                Name = "Mithril Hammer",
                Demage = 8,
                Price = 200
            };
            item.Info.ShortDescription = "Mithril Hammer Description";

            item.View = new UC_View_ItemImage(13);
            return item;
        }
        public static ItemClass I12_Maelstrom()
        {
            ItemClass item = new ItemClass()
            {
                Name = "Maelstrom",
                AttackSpeed = 0.125,
                Demage = 8,
                Price = 325
            };
            item.Info.ShortDescription = "Maelstrom Description";
            item.Info.BuffDescription = "Can be used" + Environment.NewLine + "Uses a chain lightning "
             + Environment.NewLine + "on 2 purposes";

            item.Parts = new List<ItemClass>() {
            I10_MithrilHammer(),
            I7_GlovesOfHaste(),
            I12_MaelstromRecep()
            };

            SP_Item_Maelstrom maelstrom = new SP_Item_Maelstrom() { Name = "Item Maelstrom" };
            item.SpellItem = maelstrom;

            item.View = new UC_View_ItemImage(14);
            item.View.UsingItem(item.SpellItem);

            return item;
        }

        public static ItemClass I12_MaelstromRecep()
        {
            ItemClass item = new ItemClass()
            {
                Name = "Maelstrom Recept",
                Recept = true,
                Price = 50
            };
            item.Info.ShortDescription = "Maelstrom Recept Description";

            item.View = new UC_View_ItemImage(14);
            item.View.Recept();
            return item;
        }

        public static ItemClass I13_Hyperstone()
        {
            ItemClass item = new ItemClass()
            {
                Name = "Hyperstone",
                AttackSpeed = 0.275,
                Price = 275
            };
            item.Info.ShortDescription = "Hyperstone Description";

            item.View = new UC_View_ItemImage(15);
            return item;
        }

        public static ItemClass I14_Mjollnir()
        {
            ItemClass item = new ItemClass()
            {
                Name = "Mjollnir",
                AttackSpeed = 0.4,
                Demage = 8,
                Price = 650
            };
            item.Info.ShortDescription = "Mjollnir Description";
            item.Info.BuffDescription = "Can be used" + Environment.NewLine + "Uses a chain lightning "
                + Environment.NewLine + "on 3 purposes";

            item.Parts = new List<ItemClass>() {
            I12_Maelstrom(),
            I13_Hyperstone(),
            I14_MjollnirRecep()
            };

            SP_Item_Maelstrom mjollnir = new SP_Item_Maelstrom() { Name = "Item Maelstrom" };
            mjollnir.LevelCast = 2;
            item.SpellItem = mjollnir;

            item.View = new UC_View_ItemImage(16);
            item.View.UsingItem(item.SpellItem);

            return item;
        }

        public static ItemClass I14_MjollnirRecep()
        {
            ItemClass item = new ItemClass()
            {
                Name = "Mjollnir Recept",
                Recept = true,
                Price = 50
            };
            item.Info.ShortDescription = "Mjollnir Recept Description";

            item.View = new UC_View_ItemImage(16);
            item.View.Recept();
            return item;
        }

        public static ItemClass I15_DemonEdge()
        {
            ItemClass item = new ItemClass()
            {
                Name = "Demon Edge",
                Demage = 15,
                Price = 325
            };
            item.Info.ShortDescription = "Demon Edge Description";

            item.View = new UC_View_ItemImage(17);
            return item;
        }

        public static ItemClass I16_Javelin()
        {
            ItemClass item = new ItemClass()
            {
                Name = "Javelin",
                Demage = 7,
                Price = 175
            };
            item.Info.ShortDescription = "Javelin Description";

            item.View = new UC_View_ItemImage(18);
            return item;
        }

        public static ItemClass I17_MonkeyKingBar()
        {
            ItemClass item = new ItemClass()
            {
                Name = "Monkey King Bar",
                Demage = 30,
                AttackSpeed = 0.075,
                Price = 675
            };
            item.Info.ShortDescription = "Monkey King Bar Description";
            item.Info.BuffDescription = "Probability 25%" + Environment.NewLine + "To deafen the victim on 1sec";

            item.Parts = new List<ItemClass>() {
            I15_DemonEdge(),
            I16_Javelin(),
            I16_Javelin()
            };



            Buff alchBuff = new Buff()
            {
                Passive = true,
                StunDuratio = 1,
                Duration = 0,
                Name = "MonkeyKingBar"
            };
            ///Баф микробаша
            item.Buff = alchBuff;

            item.View = new UC_View_ItemImage(19);
            return item;
        }

        public static ItemClass I18_VitalityBooster()
        {
            ItemClass item = new ItemClass()
            {
                Name = "Vitality Booster",
                HealthBonus = 25,
                Price = 125
            };
            item.Info.ShortDescription = "Vitality Booster Description";

            item.View = new UC_View_ItemImage(20);
            return item;
        }

        public static ItemClass I19_Reaver()
        {
            ItemClass item = new ItemClass()
            {
                Name = "Reaver",
                Strength = 15,
                Price = 450
            };
            item.Info.ShortDescription = "Reaver Description";

            item.View = new UC_View_ItemImage(21);
            return item;
        }

        public static ItemClass I20_HeartOfTarrasque()
        {
            ItemClass item = new ItemClass()
            {
                Name = "Heart Of Tarrasque",
                Strength = 24,
                HealthBonus = 30,
                HealthRegen = 20,
                Price = 700
            };
            item.Info.ShortDescription = "Heart Of Tarrasque Description";

            item.Parts = new List<ItemClass>() {
            I18_VitalityBooster(),
            I19_Reaver(),
            I20_HeartOfTarrasqueRecept()
            };

            item.View = new UC_View_ItemImage(22);
            return item;
        }

        public static ItemClass I20_HeartOfTarrasqueRecept()
        {
            ItemClass item = new ItemClass()
            {
                Name = "Heart Of Tarrasque Recept",
                Recept = true,
                Price = 125
            };
            item.Info.ShortDescription = "Heart Of Tarrasque Recept Description";

            item.View = new UC_View_ItemImage(22);
            item.View.Recept();
            return item;
        }
        public static ItemClass I21_BladesOfAttack()
        {
            ItemClass item = new ItemClass()
            {
                Name = "Blades Of Attack",
                Demage = 3,
                Price = 60
            };
            item.Info.ShortDescription = "Blades Of Attack Description";

            item.View = new UC_View_ItemImage(23);
            return item;
        }

        public static ItemClass I22_PhaseBoots()
        {
            ItemClass item = new ItemClass()
            {
                Name = "Phase Boots",
                Demage = 8,
                Boots = true,
                Speed = 0.30,
                Price = 195
            };
            item.Info.ShortDescription = "Phase Boots Description";
            item.Info.BuffDescription = "Can be used" + Environment.NewLine + "Speed +10";

            item.Parts = new List<ItemClass>() {
            I21_BladesOfAttack(),
            I21_BladesOfAttack(),
            I6_BootsOfSpeed()
            };



            SP_Item_PhaseBoots phaseBoots = new SP_Item_PhaseBoots() { Name = "Phase Boots" };
            item.SpellItem = phaseBoots;

            item.View = new UC_View_ItemImage(24);
            item.View.UsingItem(item.SpellItem);
            return item;
        }

        public static ItemClass I23_Boots_Of_Elvenskin()
        {
            ItemClass item = new ItemClass()
            {
                Name = "Boots Of Elvenskin",
                Agility = 4,
                Price = 75
            };
            item.Info.ShortDescription = "Boots Of Elvenskin Description";

            item.View = new UC_View_ItemImage(25);
            return item;
        }

        public static ItemClass I24_Blade_Of_Alacrity()
        {
            ItemClass item = new ItemClass()
            {
                Name = "Blade Of Alacrity",
                Agility = 6,
                Price = 100
            };
            item.Info.ShortDescription = "Blade Of Alacrity Description";

            item.View = new UC_View_ItemImage(26);
            return item;
        }

        public static ItemClass I25_Yasha()
        {
            ItemClass item = new ItemClass()
            {
                Name = "Yasha",
                Agility = 10,
                AttackSpeed = 0.075,
                Speed = 0.1,
                IsYasha = true,
                Price = 300
            };
            item.Info.ShortDescription = "Yasha Description";

            item.Parts = new List<ItemClass>() {
            I23_Boots_Of_Elvenskin(),
            I24_Blade_Of_Alacrity(),
            I25_YashaRecep()
            };
            item.View = new UC_View_ItemImage(27);
            return item;
        }

        public static ItemClass I25_YashaRecep()
        {
            ItemClass item = new ItemClass()
            {
                Name = "Yasha Recep",
                Recept = true,
                Price = 125
            };
            item.Info.ShortDescription = "Yasha Recep Description";

            item.View = new UC_View_ItemImage(27);
            item.View.Recept();
            return item;
        }

        public static ItemClass I26_Ogre_Club()
        {
            ItemClass item = new ItemClass()
            {
                Name = "Ogre Club",
                Strength = 6,
                Price = 100
            };
            item.Info.ShortDescription = "Ogre Club Description";

            item.View = new UC_View_ItemImage(28);
            return item;
        }

        public static ItemClass I27_Sange()
        {
            ItemClass item = new ItemClass()
            {
                Name = "Sange",
                Strength = 10,
                Demage = 4,
                Price = 275
            };
            item.Info.ShortDescription = "Sange Description";

            item.Parts = new List<ItemClass>() {
            I8_BeltOfStreangth(),
            I26_Ogre_Club(),
            I28_SangeRecept()
            };

            item.View = new UC_View_ItemImage(29);
            return item;
        }
        public static ItemClass I28_SangeRecept()
        {
            ItemClass item = new ItemClass()
            {
                Name = "Sange Recept",
                Recept = true,
                Price = 100
            };
            item.Info.ShortDescription = "Sange Recept Description";


            item.View = new UC_View_ItemImage(29);
            item.View.Recept();
            return item;
        }

        public static ItemClass I29_Sange_And_Yasha()
        {
            ItemClass item = new ItemClass()
            {
                Name = "Sange And Yasha",
                Strength = 10,
                Agility = 10,
                AttackSpeed = 0.08,
                Speed = 0.1,
                Demage = 5,
                IsYasha = true,
                Price = 575
            };
            item.Info.ShortDescription = "Sange And Yasha Description";

            item.Parts = new List<ItemClass>() {
            I27_Sange(),
            I25_Yasha()
            };
            item.View = new UC_View_ItemImage(30);
            return item;
        }

        public static ItemClass I30_ShadowAmulet()
        {
            ItemClass item = new ItemClass()
            {
                Name = "Shadow Amulet",
                AttackSpeed = 0.15,
                Price = 225
            };
            item.Info.ShortDescription = "Shadow Amulet Description";

            item.View = new UC_View_ItemImage(31);
            return item;
        }

        public static ItemClass I31_Claymore()
        {
            ItemClass item = new ItemClass()
            {
                Name = "Claymore",
                Demage = 5,
                Price = 175
            };
            item.Info.ShortDescription = "Claymore Description";

            item.View = new UC_View_ItemImage(32);
            return item;
        }

        public static ItemClass I32_ShadowBlade()
        {
            ItemClass item = new ItemClass()
            {
                Name = "Shadow Blade",
                Demage = 7,
                AttackSpeed = 0.15,
                Price = 400
            };
            item.Info.ShortDescription = "Shadow Blade Description";
            item.Info.BuffDescription = "Can be used" + Environment.NewLine + "Invisibility 3 sec.";
            item.Parts = new List<ItemClass>() {
            I30_ShadowAmulet(),
            I31_Claymore()
            };

            SP_Item_ShadowBlade phaseBoots = new SP_Item_ShadowBlade() { Name = "Shadow Blade" };
            item.SpellItem = phaseBoots;

            item.View = new UC_View_ItemImage(33);
            item.View.UsingItem(item.SpellItem);
            return item;
        }

        public static ItemClass I33_Point_Booster()
        {
            ItemClass item = new ItemClass()
            {
                Name = "Point Booster",
                // BonusMagicDemage = 0.15,
                HealthBonus = 18,
                ManaBonus = 18,
                Price = 120
            };
            item.Info.ShortDescription = "Point Booster Description";
            item.Info.BuffDescription = "";
            item.View = new UC_View_ItemImage(34);
            return item;
        }

        public static ItemClass I34_Staff_Of_Wizardry()
        {
            ItemClass item = new ItemClass()
            {
                Name = "Staff Of Wizardry",
                Intelligence = 6,
                Price = 100
            };
            item.Info.ShortDescription = "Staff Of Wizardry Description";

            item.View = new UC_View_ItemImage(35);
            return item;
        }

        public static ItemClass I35_Aghanims_Scepter()
        {
            ItemClass item = new ItemClass()
            {
                Name = "Aghanims Scepter",
                // BonusMagicDemage = 0.30,
                HealthBonus = 18,
                Intelligence = 6,
                Strength = 6,
                Agility = 6,
                ManaBonus = 18,
                Price = 420
            };
            item.Info.ShortDescription = "Aghanims Scepter Description";
            item.Info.BuffDescription = "Update hero skills.";

            SP_Aghanim aghanim = new SP_Aghanim() { Name = "Aghanim" };
            item.SpellItem = aghanim;

            // Alchemist
            // Jakiro
            // Jinx
            // Mirana
            // Nature
            // Bonik
            // Sniper

            item.Parts = new List<ItemClass>() {
            I33_Point_Booster(),
            I24_Blade_Of_Alacrity(),
            I26_Ogre_Club(),
            I34_Staff_Of_Wizardry()
            };

            item.View = new UC_View_ItemImage(36);
            return item;
        }

        public static ItemClass I36_DesolatorRecep()
        {
            ItemClass item = new ItemClass()
            {
                Name = "Desolator Recep",
                Recept = true,
                Price = 75
            };
            item.Info.ShortDescription = "Desolator Recep Description";

            item.View = new UC_View_ItemImage(39);
            item.View.Recept();
            return item;
        }

        public static ItemClass I36_Desolator()
        {
            ItemClass item = new ItemClass()
            {
                Name = "Desolator",
                Demage = 20,
                Price = 475
            };
            item.Info.ShortDescription = "Desolator Description";
            item.Info.BuffDescription = "Armor reductionk -8";

            Buff alchBuff = new Buff()
            {
                Passive = true,
                MinusArmor = 8,
                Duration = 8,
                Name = "DesolatorSelf"
            };
            ///Баф минус армора
            item.Buff = alchBuff;

            item.Parts = new List<ItemClass>() {
            I10_MithrilHammer(),
            I10_MithrilHammer(),
            I36_DesolatorRecep()
            };

            item.View = new UC_View_ItemImage(39);
            return item;
        }

        public static ItemClass I38_Platemail()
        {
            ItemClass item = new ItemClass()
            {
                Name = "Platemail",
                Armor = 8,
                Price = 175
            };
            item.Info.ShortDescription = "Platemail Description";

            item.View = new UC_View_ItemImage(40);
            return item;
        }
        public static ItemClass I37_Chainmail()
        {
            ItemClass item = new ItemClass()
            {
                Name = "Chainmail",
                Armor = 4,
                Price = 75
            };
            item.Info.ShortDescription = "Chainmail Description";

            item.View = new UC_View_ItemImage(41);
            return item;
        }
        public static ItemClass I38_AssaultCuirass()
        {
            ItemClass item = new ItemClass()
            {
                Name = "Assault Cuirass",
                Armor = 8,
                AttackSpeed = 0.175,
                Price = 650
            };
            item.Info.ShortDescription = "Assault Cuirass Description";

            item.Info.BuffDescription = "Aura :" + Environment.NewLine + "Attack speed: 10"
              + Environment.NewLine + "Armor: 4"
              + Environment.NewLine + "Armor reduction: 5";

            SP_Item_AssaultCuirass assaultCuirass = new SP_Item_AssaultCuirass() { Name = "Item Assault Cuirass" };
            item.AuraItem = assaultCuirass;

            item.Parts = new List<ItemClass>() {
            I38_Platemail(),
            I37_Chainmail(),
            I13_Hyperstone(),
            I38_AssaultCuirassRecep()
            };

            item.View = new UC_View_ItemImage(42);
            return item;
        }

        public static ItemClass I38_AssaultCuirassRecep()
        {
            ItemClass item = new ItemClass()
            {
                Name = "Assault Cuirass Recept",
                Recept = true,
                Price = 125
            };
            item.Info.ShortDescription = "Assault Cuirass Recept Description";

            item.View = new UC_View_ItemImage(42);
            item.View.Recept();
            return item;
        }

        public static ItemClass I39_VoidStone()
        {
            ItemClass item = new ItemClass()
            {
                Name = "Void Stone",
                ManaRegen = 7,
                Price = 120
            };
            item.Info.ShortDescription = "Void Stone Description";

            item.View = new UC_View_ItemImage(43);
            return item;
        }

        public static ItemClass I40_Ultimate_Orb()
        {
            ItemClass item = new ItemClass()
            {
                Name = "Ultimate Orb",
                Intelligence = 6,
                Strength = 6,
                Agility = 6,
                Price = 210
            };
            item.Info.ShortDescription = "Ultimate Orb Description";

            item.View = new UC_View_ItemImage(44);
            return item;
        }

        public static ItemClass I41_Mystic_Staff()
        {
            ItemClass item = new ItemClass()
            {
                Name = "Mystic Staff",
                Intelligence = 15,
                Price = 270
            };
            item.Info.ShortDescription = "Mystic Staff Description";

            item.View = new UC_View_ItemImage(45);
            return item;
        }

        public static ItemClass I42_Scythe_Of_Vyse()
        {
            ItemClass item = new ItemClass()
            {
                Name = "Scythe Of Vyse",
                Intelligence = 20,
                Strength = 6,
                Agility = 6,
                ManaRegen = 7,
                Price = 600
            };

            item.Info.ShortDescription = "Scythe Of Vyse Description";

            SP_Hex hex = new SP_Hex() { Name = "Scythe Of Vyse Hex" };
            item.SpellItem = hex;

            item.Parts = new List<ItemClass>() {
            I39_VoidStone(),
            I40_Ultimate_Orb(),
            I41_Mystic_Staff()
            };

            item.View = new UC_View_ItemImage(46);
            item.View.UsingItem(item.SpellItem);
            return item;
        }

        public static ItemClass I43_Skadi()
        {
            ItemClass item = new ItemClass()
            {
                Name = "Skadi",
                Intelligence = 15,
                Strength = 15,
                Agility = 15,
                ManaBonus = 25,
                HealthBonus = 20,
                Price = 540
            };

            item.Info.ShortDescription = "Skadi Description";

            item.Info.BuffDescription = "Speed slow: 0.25\nAttack slow: 0.2";

            Buff skadiBuff = new Buff()
            {
                Passive = true,
                SpeedSlow = 0.25,
                AttackSpeedSlow = 0.2,
                Duration = 3,
                Name = "SkadiSelf"
            };

            ///Баф замедления
            item.Buff = skadiBuff;

            item.Parts = new List<ItemClass>() {
            I40_Ultimate_Orb(),
            I40_Ultimate_Orb(),
            I33_Point_Booster()
            };

            item.View = new UC_View_ItemImage(47);
            return item;
        }

        public static ItemClass I44_TrevelBootsRecep()
        {
            ItemClass item = new ItemClass()
            {
                Name = "Trevel Boots Recep",
                Recept = true,
                Price = 200
            };
            item.Info.ShortDescription = "Trevel Boots Recep Description";

            item.View = new UC_View_ItemImage(48);
            item.View.Recept();
            return item;
        }



        public static ItemClass I45_TrevelBoots()
        {
            ItemClass item = new ItemClass()
            {
                Name = "Trevel Boots",
                Speed = 0.35,
                Boots = true,
                Price = 275
            };
            item.Info.ShortDescription = "Trevel Boots Description";
            item.Info.BuffDescription = "Teleport to grass.";

            SP_TrevelBoots_Teleport teleport = new SP_TrevelBoots_Teleport() { Name = " Trevel Boots Teleport" };
            item.SpellItem = teleport;

            item.Parts = new List<ItemClass>() {
                        I6_BootsOfSpeed(),
                        I44_TrevelBootsRecep()
            };

            item.View = new UC_View_ItemImage(48);
            item.View.UsingItem(item.SpellItem);
            return item;
        }

        public static ItemClass I46_TrevelBoots2Recep()
        {
            ItemClass item = new ItemClass()
            {
                Name = "Trevel Boots 2 Recep",
                Recept = true,
                Price = 200
            };
            item.Info.ShortDescription = "Trevel Boots 2 Recep Description";

            item.View = new UC_View_ItemImage(49);
            item.View.Recept();
            return item;
        }

        public static ItemClass I47_TrevelBoots2()
        {
            ItemClass item = new ItemClass()
            {
                Name = "Trevel Boots 2",
                Speed = 0.37,
                Boots = true,
                Price = 475
            };
            item.Info.ShortDescription = "Trevel Boots 2 Description";
            item.Info.BuffDescription = "Faster teleport to grass.";

            SP_TrevelBoots_Teleport teleport = new SP_TrevelBoots_Teleport() { Name = " Trevel Boots 2 Teleport" };
            teleport.LevelCast = 2;

            item.SpellItem = teleport;

            item.Parts = new List<ItemClass>() {
                        I45_TrevelBoots(),
                        I46_TrevelBoots2Recep()
            };

            item.View = new UC_View_ItemImage(49);
            item.View.UsingItem(item.SpellItem);
            return item;
        }

        #region Power items
        /// <summary>
        /// Итемы разового использования
        /// </summary>
        public static ItemClass IBonus1_Shop()
        {
            ItemClass item = new ItemClass()
            {
                Name = "Shop",
                Bonus = true
            };

            item.View = new UC_View_ItemImage(9);
            return item;
        }

        public static ItemClass IBonus2_Clarity()
        {
            ItemClass item = new ItemClass()
            {
                Name = "Clarity",
                Bonus = true,
                ManaBonus = 30
            };

            item.View = new UC_View_ItemImage(10);
            return item;
        }

        public static ItemClass IBonus3_HealingSalve()
        {
            ItemClass item = new ItemClass()
            {
                Name = "Healing Salve",
                Bonus = true,
                HealthBonus = 60
            };

            item.View = new UC_View_ItemImage(11);
            return item;
        }

        public static ItemClass IBonus4_Tango()
        {
            ItemClass item = new ItemClass()
            {
                Name = "Tango",
                Bonus = true,
                HealthBonus = 30
            };

            item.View = new UC_View_ItemImage(12);
            return item;
        }
        #endregion
        #endregion


        /// <summary>
        /// Собирает новы предмет из частей
        /// </summary>
        public static ItemClass CreateNewItem(Player player, ItemClass itemNew)
        {
            ItemClass retItem = null;
            player.Items.Add(itemNew);

            ///Собераем предметы 
            ///I4_OblivionStuff
            if (player.Items.Any(p => p.Name == "Robe Of The Magi") &&
                player.Items.Any(p => p.Name == "Sage Mask") &&
                player.Items.Any(p => p.Name == "Quarterstuff"))
            {
                player.RemoveItem(player.Items.First(p => p.Name == "Robe Of The Magi"), true);
                player.RemoveItem(player.Items.First(p => p.Name == "Sage Mask"), true);
                player.RemoveItem(player.Items.First(p => p.Name == "Quarterstuff"), true);

                retItem = I4_OblivionStuff();
            }

            ///I5_Orchid
            if (player.Items.Count(p => p.Name == "Oblivion Stuff") > 1 &&
                player.Items.Any(p => p.Name == "Orchid Recept")
               )
            {
                player.RemoveItem(player.Items.First(p => p.Name == "Oblivion Stuff"), true);
                player.RemoveItem(player.Items.First(p => p.Name == "Oblivion Stuff"), true);
                player.RemoveItem(player.Items.First(p => p.Name == "Orchid Recept"), true);

                retItem = I5_Orchid();
            }

            ///Собираем предметы 
            ///I9_PowerTreads
            if (player.Items.Any(p => p.Name == "Belt Of Streangth") &&
                player.Items.Any(p => p.Name == "Boots Of Speed") &&
                player.Items.Any(p => p.Name == "Gloves Of Haste"))
            {
                player.RemoveItem(player.Items.First(p => p.Name == "Belt Of Streangth"), true);
                player.RemoveItem(player.Items.First(p => p.Name == "Gloves Of Haste"), true);
                player.RemoveItem(player.Items.First(p => p.Name == "Boots Of Speed"), true);

                retItem = I9_PowerTreads();
            }

            ///I12_Maelstrom
            if (player.Items.Any(p => p.Name == "Maelstrom Recept") &&
                player.Items.Any(p => p.Name == "Mithril Hammer") &&
                player.Items.Any(p => p.Name == "Gloves Of Haste"))
            {
                player.RemoveItem(player.Items.First(p => p.Name == "Maelstrom Recept"), true);
                player.RemoveItem(player.Items.First(p => p.Name == "Gloves Of Haste"), true);
                player.RemoveItem(player.Items.First(p => p.Name == "Mithril Hammer"), true);

                retItem = I12_Maelstrom();
            }

            ///I14_Mjollnir
            if (player.Items.Any(p => p.Name == "Hyperstone") &&
                player.Items.Any(p => p.Name == "Maelstrom") &&
                player.Items.Any(p => p.Name == "Mjollnir Recept"))
            {
                player.RemoveItem(player.Items.First(p => p.Name == "Hyperstone"), true);
                player.RemoveItem(player.Items.First(p => p.Name == "Maelstrom"), true);
                player.RemoveItem(player.Items.First(p => p.Name == "Mjollnir Recept"), true);

                retItem = I14_Mjollnir();
            }

            ///I17_MonkeyKingBar
            if (player.Items.Count(p => p.Name == "Javelin") > 1 &&
                player.Items.Any(p => p.Name == "Demon Edge")
               )
            {
                player.RemoveItem(player.Items.First(p => p.Name == "Javelin"), true);
                player.RemoveItem(player.Items.First(p => p.Name == "Javelin"), true);
                player.RemoveItem(player.Items.First(p => p.Name == "Demon Edge"), true);

                retItem = I17_MonkeyKingBar();
            }

            ///I19_HeartOfTarrasque
            if (player.Items.Any(p => p.Name == "Vitality Booster") &&
                player.Items.Any(p => p.Name == "Reaver") &&
                player.Items.Any(p => p.Name == "Heart Of Tarrasque Recept"))
            {
                player.RemoveItem(player.Items.First(p => p.Name == "Vitality Booster"), true);
                player.RemoveItem(player.Items.First(p => p.Name == "Reaver"), true);
                player.RemoveItem(player.Items.First(p => p.Name == "Heart Of Tarrasque Recept"), true);

                retItem = I20_HeartOfTarrasque();
            }

            ///I22_PhaseBoots
            if (player.Items.Count(p => p.Name == "Blades Of Attack") > 1 &&
                player.Items.Any(p => p.Name == "Boots Of Speed"))
            {
                player.RemoveItem(player.Items.First(p => p.Name == "Blades Of Attack"), true);
                player.RemoveItem(player.Items.First(p => p.Name == "Blades Of Attack"), true);
                player.RemoveItem(player.Items.First(p => p.Name == "Boots Of Speed"), true);

                retItem = I22_PhaseBoots();
            }

            ///I25_Yasha
            if (player.Items.Any(p => p.Name == "Yasha Recep") &&
                player.Items.Any(p => p.Name == "Blade Of Alacrity") &&
                player.Items.Any(p => p.Name == "Boots Of Elvenskin"))
            {
                player.RemoveItem(player.Items.First(p => p.Name == "Yasha Recep"), true);
                player.RemoveItem(player.Items.First(p => p.Name == "Blade Of Alacrity"), true);
                player.RemoveItem(player.Items.First(p => p.Name == "Boots Of Elvenskin"), true);

                retItem = I25_Yasha();
            }

            ///I27_Sange
            if (player.Items.Any(p => p.Name == "Belt Of Streangth") &&
                player.Items.Any(p => p.Name == "Ogre Club") &&
                player.Items.Any(p => p.Name == "Sange Recept"))
            {
                player.RemoveItem(player.Items.First(p => p.Name == "Belt Of Streangth"), true);
                player.RemoveItem(player.Items.First(p => p.Name == "Ogre Club"), true);
                player.RemoveItem(player.Items.First(p => p.Name == "Sange Recept"), true);

                retItem = I27_Sange();
            }

            ///I29_Sange_And_Yasha
            if (player.Items.Any(p => p.Name == "Sange") &&
                player.Items.Any(p => p.Name == "Yasha"))
            {
                player.RemoveItem(player.Items.First(p => p.Name == "Sange"), true);
                player.RemoveItem(player.Items.First(p => p.Name == "Yasha"), true);

                retItem = I29_Sange_And_Yasha();
            }

            ///I32_ShadowBlade
            if (player.Items.Any(p => p.Name == "Shadow Amulet") &&
                player.Items.Any(p => p.Name == "Claymore"))
            {
                player.RemoveItem(player.Items.First(p => p.Name == "Shadow Amulet"), true);
                player.RemoveItem(player.Items.First(p => p.Name == "Claymore"), true);

                retItem = I32_ShadowBlade();
            }


            ///I35_Aghanims_Scepter
            if (player.Items.Any(p => p.Name == "Staff Of Wizardry") &&
                player.Items.Any(p => p.Name == "Point Booster") &&
                player.Items.Any(p => p.Name == "Ogre Club") &&
                player.Items.Any(p => p.Name == "Blade Of Alacrity"))
            {
                player.RemoveItem(player.Items.First(p => p.Name == "Staff Of Wizardry"), true);
                player.RemoveItem(player.Items.First(p => p.Name == "Point Booster"), true);
                player.RemoveItem(player.Items.First(p => p.Name == "Ogre Club"), true);
                player.RemoveItem(player.Items.First(p => p.Name == "Blade Of Alacrity"), true);

                retItem = I35_Aghanims_Scepter();
            }

            ///I36_Desolator
            if (player.Items.Count(p => p.Name == "Mithril Hammer") > 1 &&
                player.Items.Any(p => p.Name == "Desolator Recep")
               )
            {
                player.RemoveItem(player.Items.First(p => p.Name == "Mithril Hammer"), true);
                player.RemoveItem(player.Items.First(p => p.Name == "Mithril Hammer"), true);
                player.RemoveItem(player.Items.First(p => p.Name == "Desolator Recep"), true);

                retItem = I36_Desolator();
            }

            ///I38_AssaultCuirass
            if (player.Items.Any(p => p.Name == "Hyperstone") &&
              player.Items.Any(p => p.Name == "Platemail") &&
              player.Items.Any(p => p.Name == "Chainmail") &&
              player.Items.Any(p => p.Name == "Assault Cuirass Recept"))
            {
                player.RemoveItem(player.Items.First(p => p.Name == "Hyperstone"), true);
                player.RemoveItem(player.Items.First(p => p.Name == "Platemail"), true);
                player.RemoveItem(player.Items.First(p => p.Name == "Chainmail"), true);
                player.RemoveItem(player.Items.First(p => p.Name == "Assault Cuirass Recept"), true);

                retItem = I38_AssaultCuirass();
            }


            ///I42_Scythe_Of_Vyse
            if (player.Items.Any(p => p.Name == "Void Stone") &&
                player.Items.Any(p => p.Name == "Ultimate Orb") &&
                player.Items.Any(p => p.Name == "Mystic Staff")
               )
            {
                player.RemoveItem(player.Items.First(p => p.Name == "Void Stone"), true);
                player.RemoveItem(player.Items.First(p => p.Name == "Ultimate Orb"), true);
                player.RemoveItem(player.Items.First(p => p.Name == "Mystic Staff"), true);

                retItem = I42_Scythe_Of_Vyse();
            }

            ///I43_Skadi
            if (player.Items.Count(p => p.Name == "Ultimate Orb") > 1 &&
                player.Items.Any(p => p.Name == "Point Booster")
               )
            {
                player.RemoveItem(player.Items.First(p => p.Name == "Ultimate Orb"), true);
                player.RemoveItem(player.Items.First(p => p.Name == "Ultimate Orb"), true);
                player.RemoveItem(player.Items.First(p => p.Name == "Point Booster"), true);

                retItem = I43_Skadi();
            }

            ///I45_TrevelBoots
            if (player.Items.Any(p => p.Name == "Boots Of Speed") &&
                 player.Items.Any(p => p.Name == "Trevel Boots Recep")
               )
            {
                player.RemoveItem(player.Items.First(p => p.Name == "Boots Of Speed"), true);
                player.RemoveItem(player.Items.First(p => p.Name == "Trevel Boots Recep"), true);

                retItem = I45_TrevelBoots();
            }

            ///I47_TrevelBoots2
            if (player.Items.Any(p => p.Name == "Trevel Boots") &&
                 player.Items.Any(p => p.Name == "Trevel Boots 2 Recep")
               )
            {
                player.RemoveItem(player.Items.First(p => p.Name == "Trevel Boots"), true);
                player.RemoveItem(player.Items.First(p => p.Name == "Trevel Boots 2 Recep"), true);

                retItem = I47_TrevelBoots2();
            }

            ///Если новы предмет сложен не был, то удаляем предмет добавленный в инвинтарь
            if (retItem == null)
                player.Items.Remove(itemNew);

            return retItem;
        }

        public static void UpdatePlayerView(IUnit unit)
        {
            Player pl = unit as Player;
            if (pl != null)
                pl.UpdateView();
        }

        public static bool UpPlayerSpell(IUnit unit, ISpell spell)
        {
            Player player = unit as Player;
            if (player.Level > 5)
                player.PreviousSkill = "";

            if (player != null && player.AllowUpSpell &&
                spell.LevelCast < spell.MaxLevelCast &&
                player.PreviousSkill != spell.Name)
            {
                if (player.Level < 5 && spell.IsUlt)
                    return false;

                player.UpPoint -= 1;
                spell.UpSpell(player);
                return true;
            }
            return false;
        }

        public static Point NewX_NewY(IUnit unit)
        {

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
            return new Point(xNew, yNew);
        }

        /// <summary>
        /// Номера всех ячеек по кругу заданной
        /// </summary>
        /// <returns>Номера всех ячеек по кругу заданной,
        /// без проверки зависимостей</returns>
        public static List<Point> RoundNumber(int x, int y)
        {
            List<Point> points = new List<Point>();
            points.Add(new Point(x - 1, y));
            points.Add(new Point(x + 1, y));
            points.Add(new Point(x, y - 1));
            points.Add(new Point(x, y + 1));
            return points;
        }
        public static List<Point> BigRoundNumber(int x, int y)
        {
            List<Point> points = new List<Point>();
            points.Add(new Point(x - 1, y));
            points.Add(new Point(x - 2, y));
            points.Add(new Point(x + 1, y));
            points.Add(new Point(x + 2, y));
            points.Add(new Point(x, y - 1));
            points.Add(new Point(x, y - 2));
            points.Add(new Point(x, y + 1));
            points.Add(new Point(x, y + 2));
            return points;
        }
        /// <summary>
        /// Вычисление первой занятой колонки
        /// </summary>
        /// <param name="x/y">Кординаты начала пути, на них попадение не проверяется</param>
        /// <param name="ang">угол направления диагонали попадения</param>
        /// <returns>Занятая колонка. С ней произошло столкновение</returns>
        public static Map_Cell FiratCollision(int x, int y, Map map, EAngel ang)
        {
            int xNew = x;
            int yNew = y;

            int forCount = 0;
            if (ang == EAngel.Left || ang == EAngel.Right)
                forCount = map.Width / 50;
            else
                forCount = map.Height / 50;

            for (int i = 0; i < forCount; i++)
            {
                ///Получение кординат следующей ячейки
                NextCord(ang, ref xNew, ref yNew);

                if (map.Calls.Any(p => p.IndexLeft == xNew && p.IndexTop == yNew))
                {
                    Map_Cell retCall = map.Calls.Single(p => p.IndexLeft == xNew && p.IndexTop == yNew);
                    if (retCall.Used)
                        //&& retCall.IUnits.Any(p => p.GameObject.EnumCallType == EnumCallType.Unit || 
                        //                        p.GameObject.EnumCallType == EnumCallType.Player))
                        ///Есть попадение
                        return retCall;
                }
                else
                    ///Тупик, дальше ячеек нету 
                    return null;
            }
            return null;
        }

        public static Map_Cell FiratCollisionOnlyGrass(int x, int y, int range, Map map, EAngel ang)
        {
            int xNew = x;
            int yNew = y;

            //int forCount = 0;
            //if (ang == EAngel.Left || ang == EAngel.Right)
            //    forCount = map.Width / 50;
            //else
            //    forCount = map.Height / 50;

            for (int i = 0; i < range; i++)
            {
                ///Получение кординат следующей ячейки
                NextCord(ang, ref xNew, ref yNew);

                if (map.Calls.Any(p => p.IndexLeft == xNew && p.IndexTop == yNew))
                {
                    Map_Cell retCall = map.Calls.Single(p => p.IndexLeft == xNew && p.IndexTop == yNew);
                    if (retCall.Used &&
                        retCall.IUnits.Any(p => p.UnitType == EUnitType.Grass))
                        //&& retCall.IUnits.Any(p => p.GameObject.EnumCallType == EnumCallType.Unit || 
                        //                        p.GameObject.EnumCallType == EnumCallType.Player))
                        ///Есть попадение
                        return retCall;
                }
                else
                    ///Тупик, дальше ячеек нету 
                    return null;
            }
            return null;
        }

        public static void NextCord(EAngel ang, ref int xNew, ref int yNew)
        {
            if (ang == EAngel.Left)
                xNew = xNew - 1;
            else if (ang == EAngel.Right)
                xNew = xNew + 1;
            else if (ang == EAngel.Top)
                yNew = yNew - 1;
            else if (ang == EAngel.Bottom)
                yNew = yNew + 1;
        }

        /// <summary>
        /// Выбираем рандомную ячейку
        /// Если там пусто или есть игрок оставим ее
        /// </summary>
        /// <returns></returns>
        /// CurrentUnit.PositionX, CurrentUnit.PositionY, CurrentMap, CurrentUnit.GroupType, CurrentUnit.Way, true
        public static Map_Cell RandonCell(IUnit unit, Map map,
                                          bool playerAttacker, bool farm, bool hunt)
        {
            ///Ячейки куда можно походить
            List<Map_Cell> allowCall = new List<Map_Cell>();

            List<Point> points = new List<Point>(){
            new Point(unit.PositionX-1,unit.PositionY),
            new Point(unit.PositionX+1,unit.PositionY),
            new Point(unit.PositionX,unit.PositionY-1),
            new Point(unit.PositionX,unit.PositionY+1)
            };


            ///Проверяем колонки оставляя те куда можно походить
            foreach (var item in points)
            {
                Map_Cell call = map.Calls.FirstOrDefault(p => p.IndexLeft == item.X && p.IndexTop == item.Y);
                if (
                    farm &&
                    call != null &&
                    ///Если в ячейке нету блоков
                    !call.IUnits.Any(p => p.GameObject.EnumCallType == EnumCallType.Block ||
                    p.GroupType == unit.GroupType)
                    ||
                    !farm &&
                    call != null &&
                    /// Тут еще проверка кустов
                    !call.IUnits.Any(p => p.GameObject.EnumCallType == EnumCallType.UnitBlock ||
                    p.GameObject.EnumCallType == EnumCallType.Block ||
                    p.GroupType == unit.GroupType)
                    ||   ///Если есть блоки на которые залез юнит(инвизер)
                    call != null &&
                    call.IUnits.Any(p => p.GameObject.EnumCallType == EnumCallType.UnitBlock) &&
                    call.IUnits.Any(p => p.GameObject.EnumCallType == EnumCallType.Player &&
                    p.GroupType != unit.GroupType)
                    )
                {
                    ///Под каким углом должен быть повернуть юнит чтобы переместится сюда
                    EAngel angelMuve = (EAngel)points.IndexOf(item);

                    call.Angel = angelMuve;
                    allowCall.Add(call);
                }
            }


            /// Проверим есть ли на горизонте игрок, чтобы на него охотится
            if (hunt)
            {
                #region Find enemyes
                ///Поиск цели

                int xNew = unit.PositionX;
                int yNew = unit.PositionY;

                if (allowCall.Any(p => p.Angel == EAngel.Left))
                {
                    for (int i = 0; i <= map.Width / 50; i++)
                    {
                        xNew = xNew - 1;
                        Map_Cell call = map.Calls.FirstOrDefault(p => p.IndexLeft == xNew && p.IndexTop == yNew);
                        if (call != null && !call.Block)
                        {
                            if (call.IUnits.Any(p => p.GameObject.EnumCallType == EnumCallType.Player && p.GroupType != unit.GroupType))
                            {
                                return allowCall.FirstOrDefault(p => p.Angel == EAngel.Left);
                                //break;
                            }
                        }
                    }
                }

                if (allowCall.Any(p => p.Angel == EAngel.Right))
                {
                    xNew = unit.PositionX;
                    yNew = unit.PositionY;
                    for (int i = 0; i <= map.Width / 50; i++)
                    {
                        xNew = xNew + 1;
                        Map_Cell call = map.Calls.FirstOrDefault(p => p.IndexLeft == xNew && p.IndexTop == yNew);
                        if (call != null && !call.Block)
                        {
                            if (call.IUnits.Any(p => p.GameObject.EnumCallType == EnumCallType.Player && p.GroupType != unit.GroupType))
                            {
                                return allowCall.FirstOrDefault(p => p.Angel == EAngel.Right);
                                // break;
                            }
                        }
                    }
                }
                if (allowCall.Any(p => p.Angel == EAngel.Top))
                {
                    xNew = unit.PositionX;
                    yNew = unit.PositionY;
                    for (int i = 0; i <= map.Height / 50; i++)
                    {
                        yNew = yNew - 1;
                        Map_Cell call = map.Calls.FirstOrDefault(p => p.IndexLeft == xNew && p.IndexTop == yNew);
                        if (call != null && !call.Block)
                        {
                            if (call.IUnits.Any(p => p.GameObject.EnumCallType == EnumCallType.Player && p.GroupType != unit.GroupType))
                            {
                                return allowCall.FirstOrDefault(p => p.Angel == EAngel.Top);
                                // break;
                            }
                        }
                    }
                }

                if (allowCall.Any(p => p.Angel == EAngel.Bottom))
                {
                    xNew = unit.PositionX;
                    yNew = unit.PositionY;
                    for (int i = 0; i <= map.Height / 50; i++)
                    {
                        yNew = yNew + 1;
                        Map_Cell call = map.Calls.FirstOrDefault(p => p.IndexLeft == xNew && p.IndexTop == yNew);
                        if (call != null && !call.Block)
                        {
                            if (call.IUnits.Any(p => p.GameObject.EnumCallType == EnumCallType.Player && p.GroupType != unit.GroupType))
                            {
                                return allowCall.FirstOrDefault(p => p.Angel == EAngel.Bottom);
                                // break;
                            }
                        }
                    }
                }

                #endregion



            }


            if (allowCall.Count != 0)
            {
                if (unit.Way != null && unit.Way.Count > 0 && allowCall.Count > 1)
                    allowCall.Remove(unit.Way.Last());

                if (playerAttacker)
                {
                    Map_Cell withPlayer = allowCall.FirstOrDefault(p =>
                            p.IUnits.Any(k => k.GroupType != unit.GroupType));

                    if (withPlayer != null)
                    {
                        return withPlayer;
                    }
                }

                Random randomOb = new Random();
                int randomNumber = randomOb.Next(0, allowCall.Count);
                return allowCall[randomNumber];
            }
            else
                return null;
        }

        public static void EditWay(Map_Cell call, IUnit unit)
        {
            if (unit.Way == null)
                unit.Way = new List<Map_Cell>();

            unit.Way.Add(call);
            if (unit.Way.Count > 5)
                unit.Way.Remove(unit.Way.First());
        }

        public static bool AddDamage(Map_Cell call, Bullet bullet)
        {
            bool exept = false;
            ///Наносим урон объектам в ячейке (если они есть)
            for (int i = 0; i < call.IUnits.Count; i++)
            {
                if (call.IUnits[i] != bullet.UnitUsed &&
                    call.IUnits[i].GroupType != bullet.UnitUsed.GroupType)
                {
                    exept = true;

                    #region Nature_Sprout
                    if (call.IUnits[i].UnitType == EUnitType.Grass &&
                        bullet.GrassDamage != 0)
                    {
                        bullet.DemageMagic += bullet.GrassDamage;
                    }
                    #endregion

                    ///Объект был унечтожен
                    IUnit unitRmoved = call.IUnits[i].GatDamage(bullet.DemagePhys, bullet.DemageMagic, bullet.DemagePure, bullet.UnitUsed);

                    bullet.RemoveUnit = unitRmoved;

                }
            }
            return exept;
        }

        public static void Soul(IUnit attackerUnit, IUnit unitRmoved)
        {
            #region Soul
            if (unitRmoved != null)
            {
                Buff buff = attackerUnit.Buffs.FirstOrDefault(p => p.Name == "SoulCount");
                if (buff != null)
                {
                    int addSoul = buff.SoulCount + 1;
                    if (unitRmoved is Player)
                        addSoul = buff.SoulCount + StaticVaribl.HeroSoulCount;

                    if (addSoul <= buff.MaxSoulCount)
                        buff.SoulCount = addSoul;
                    (attackerUnit as Player).UpdateView();
                }

                Buff buffRemove = unitRmoved.Buffs.FirstOrDefault(p => p.Name == "SoulCount");
                if (buffRemove != null)
                {
                    buffRemove.SoulCount = buffRemove.SoulCount / 2;
                    (unitRmoved as Player).UpdateView();
                }
            }
            #endregion
        }



        /// <summary>
        /// Only Hero
        /// </summary>
        public static bool AddNullDemage(Map_Cell call, Bullet bullet)
        {
            bool exept = false;
            ///Наносим урон объектам в ячейке (если они есть)
            for (int i = 0; i < call.IUnits.Count; i++)
            {
                if (call.IUnits[i] != bullet.UnitUsed &&
                    call.IUnits[i].GroupType != bullet.UnitUsed.GroupType &&
                    call.IUnits[i].GameObject.EnumCallType != EnumCallType.UnitBlock)// &&
                                                                                     //  call.IUnits[i].GameObject.EnumCallType == EnumCallType.Player)
                {
                    exept = true;
                }
            }
            return exept;
        }

        public static bool AddStune(Map_Cell call, Bullet bullet, int stunDuration)
        {
            bool exept = false;

            if (stunDuration != 0)
                /// Стан обектам в конкретной ячейке
                for (int i = 0; i < call.IUnits.Count; i++)
                {
                    if (call.IUnits[i] != bullet.UnitUsed &&
                        call.IUnits[i].GroupType != bullet.UnitUsed.GroupType)
                    {
                        exept = true;
                        Buff alchBuff = new Buff()
                        {
                            Stun = true,
                            MinusArmor = bullet.MinusArmor,
                            Duration = stunDuration,
                            Name = "Stun" + bullet.Name
                        };
                        call.IUnits[i].Buffs.Add(alchBuff);

                        (call.IUnits[i].GameObject.View as IGameControl).GetDemage("Stun " + stunDuration);
                    }
                }

            return exept;
        }

        public static bool AddStuneOne(Map_Cell call, Bullet bullet, int stunDuration, int culdaunStun)
        {
            bool exept = false;

            if (stunDuration != 0)
                /// Стан обектам в конкретной ячейке
                for (int i = 0; i < call.IUnits.Count; i++)
                {
                    if (call.IUnits[i] != bullet.UnitUsed &&
                        call.IUnits[i].GroupType != bullet.UnitUsed.GroupType)
                    {
                        exept = true;

                        if (call.IUnits[i].Buffs.Any(p => p.Name == "Stun" + bullet.Name))
                        {

                        }
                        else
                        {
                            Buff alchBuff = new Buff()
                            {
                                Stun = true,
                                MinusArmor = bullet.MinusArmor,
                                Duration = stunDuration,
                                Name = "Stun" + bullet.Name
                            };
                            call.IUnits[i].Buffs.Add(alchBuff);
                            (call.IUnits[i].GameObject.View as IGameControl).GetDemage("Stun " + stunDuration);
                        }
                    }
                }

            return exept;
        }
        /// <summary>
        /// Баф стрелы мираны
        /// </summary>
        public static bool AddStuneTwo(Map_Cell call, Bullet bullet, int stunDuration, int culdaunStun)
        {
            bool exept = false;

            if (stunDuration != 0)
                /// Стан обектам в конкретной ячейке
                for (int i = 0; i < call.IUnits.Count; i++)
                {
                    if (call.IUnits[i] != bullet.UnitUsed &&
                        call.IUnits[i].GroupType != bullet.UnitUsed.GroupType)
                    {
                        exept = true;

                        if (!call.IUnits[i].Buffs.Any(p => p.Name == "StunA" + bullet.Name))
                        {
                            Buff alchBuff = new Buff()
                            {
                                Stun = true,
                                MinusArmor = bullet.MinusArmor,
                                Duration = stunDuration,
                                Name = "Stun" + bullet.Name
                            };
                            call.IUnits[i].Buffs.Add(alchBuff);

                            Buff alchBuffcd = new Buff()
                            {
                                Duration = culdaunStun,
                                Name = "StunA" + bullet.Name
                            };
                            call.IUnits[i].Buffs.Add(alchBuffcd);

                            (call.IUnits[i].GameObject.View as IGameControl).GetDemage("Stun " + stunDuration);

                        }

                    }
                }

            return exept;
        }
        /// <summary>
        /// Приписка One значит что баф наложить можно только раз
        /// </summary>
        public static bool AddBufDemageOne(Map_Cell call, Bullet bullet, int demage)
        {
            bool exept = false;

            if (demage != 0)
                /// Урон обектам в конкретной ячейке
                for (int i = 0; i < call.IUnits.Count; i++)
                {
                    if (call.IUnits[i] != bullet.UnitUsed &&
                        call.IUnits[i].GroupType != bullet.UnitUsed.GroupType)
                    {
                        exept = true;

                        if (!call.IUnits[i].Buffs.Any(p => p.Name == "Demage" + bullet.Name))
                        {
                            Buff alchBuff = new Buff()
                            {
                                DemageMagic = demage,
                                DemagedUnit = bullet.UnitUsed,
                                MinusArmor = bullet.MinusArmor,
                                Duration = 1,
                                Name = "Demage" + bullet.Name
                            };
                            call.IUnits[i].Buffs.Add(alchBuff);
                            //(call.IUnits[i].GameObject.View as IGameControl).GetDemage(stunDuration);
                        }
                    }
                }

            return exept;
        }

        public static bool AddDemageUnit(IUnit unit, Bullet bullet)
        {
            unit.GatDamage(bullet.DemagePhys, bullet.DemageMagic, bullet.DemagePure, bullet.UnitUsed);
            return true;
        }
        /// <summary>
        /// Получаем ячейку на которую смотрит юнит
        /// </summary>
        /// <returns>Кординаты</returns>
        public static Point AngelCallPoint(EAngel angel, int x, int y)
        {
            Point returnPoint = new Point();
            int xNew = 0;
            int yNew = 0;
            if (angel == EAngel.Left)
            {
                xNew = x - 1;
                yNew = y;
            }
            else if (angel == EAngel.Right)
            {
                xNew = x + 1;
                yNew = y;
            }
            else if (angel == EAngel.Top)
            {
                xNew = x;
                yNew = y - 1;
            }
            else if (angel == EAngel.Bottom)
            {
                xNew = x;
                yNew = y + 1;
            }
            returnPoint.X = xNew;
            returnPoint.Y = yNew;

            return returnPoint;
        }
        /// <summary>
        /// Мкб
        /// </summary>
        public static void MKB_Bush(Bullet bullArrow, IUnit unit)
        {
            Random rand = new Random((int)DateTime.Now.Ticks);

            if (25 > rand.Next(0, 100))
            {
                Buff buff = unit.Buffs.FirstOrDefault(p => p.Name == "MonkeyKingBar");
                if (buff != null)
                    bullArrow.StunDuration = buff.StunDuratio;
            }
        }

        /// <summary>
        /// Дезоль
        /// </summary>
        public static void Desolator(IUnit unit, IUnit goal)
        {
            Buff buff = unit.Buffs.FirstOrDefault(p => p.Name == "DesolatorSelf");
            if (buff != null)
            {
                Buff buffGoal = goal.Buffs.FirstOrDefault(p => p.Name == "Desolator");
                if (buffGoal != null)
                {
                    buffGoal.Duration = buff.Duration;
                }
                else
                {
                    Buff alchBuff = new Buff()
                    {
                        Duration = buff.Duration,
                        MinusArmor = buff.MinusArmor,
                        Name = "Desolator"
                    };
                    // goal.Buffs.Add(alchBuff);
                    goal.UseBuff(alchBuff);
                }
            }
        }

        /// <summary>
        /// Скади
        /// </summary>
        public static void Skadi(IUnit unit, IUnit goal)
        {
            Buff buff = unit.Buffs.FirstOrDefault(p => p.Name == "SkadiSelf");
            if (buff != null)
            {
                Buff buffGoal = goal.Buffs.FirstOrDefault(p => p.Name == "Skadi");
                if (buffGoal != null)
                {
                    buffGoal.Duration = buff.Duration;
                }
                else
                {
                    Buff alchBuff = new Buff()
                    {
                        Duration = buff.Duration,
                        AttackSpeedSlow = buff.AttackSpeedSlow,
                        SpeedSlow = buff.SpeedSlow,
                        Name = "Skadi"
                    };
                    // goal.Buffs.Add(alchBuff);
                    goal.UseBuff(alchBuff);
                }
            }
        }

        /// <summary>
        /// Sf Ult
        /// </summary>
        public static void SfUlt(IUnit unit, IUnit goal)
        {
            Buff buffGoal = goal.Buffs.FirstOrDefault(p => p.Name == "SfUlt");
            if (buffGoal != null)
            {
                buffGoal.Duration = 4;
            }
            else
            {
                Buff alchBuff = new Buff()
                {
                    Duration = 4,
                    SpeedSlow = 0.25,
                    Name = "SfUlt"
                };
                // goal.Buffs.Add(alchBuff);
                goal.UseBuff(alchBuff);
            }

        }

        /// <summary>
        /// Дезоль
        /// </summary>
        public static void UnitDesolator(IUnit unit, IUnit goal, int minusArrmor)
        {
                Buff buffGoal = goal.Buffs.FirstOrDefault(p => p.Name == "UnitDesolator");
                if (buffGoal != null)
                {
                    buffGoal.Duration = 4;
                }
                else
                {
                    Buff alchBuff = new Buff()
                    {
                        Duration = 4,
                        MinusArmor = minusArrmor,
                        Name = "UnitDesolator"
                    };
                    goal.UseBuff(alchBuff);
                }
        }

        /// <summary>
        /// Добавить баф c использованием
        /// </summary>
        public static void AddEasyBuff(IUnit unit, Buff buff)
        {
            if (buff != null)
            {
                Buff buffGoal = unit.Buffs.FirstOrDefault(p => p.Name == buff.Name);
                if (buffGoal != null)
                {
                    buffGoal.Duration = buff.Duration;
                }
                else
                {
                    // goal.Buffs.Add(alchBuff);
                    unit.UseBuff(buff);
                }
            }
        }

        /// <summary>
        /// Метод проверки линии на наличие цели
        /// Проверка проводится перпендикулярно
        /// </summary>
        /// <param name="xNew">кординаты линии</param>
        /// <param name="yNew">кординаты линии</param>
        /// <param name="unit"></param>
        public static bool CheckLine(int xNew, int yNew, IUnit unit)
        {
            ///Тут отмечается с какой стороны цели точно нету
            bool Bool1 = true;
            bool Bool2 = true;

            int xNewLocalLeft = xNew;
            int xNewLocalRoight = xNew;

            int yNewLocalTop = yNew;
            int yNewLocalBottom = yNew;

            if (unit.Angel == EAngel.Left || unit.Angel == EAngel.Right)
            {
                for (int i = 0; i <= unit.CurrentMap.Height / 50; i++)
                {
                    yNewLocalTop = yNewLocalTop - 1;
                    yNewLocalBottom = yNewLocalBottom + 1;

                    ///Поиск цели на верху
                    Map_Cell callTop = unit.CurrentMap.Calls.FirstOrDefault(p => p.IndexLeft == xNew && p.IndexTop == yNewLocalTop);
                    if (callTop != null
                        && !callTop.Block
                        && Bool1
                        && !callTop.IUnits.Any(p => p.GameObject.EnumCallType == EnumCallType.UnitBlock)
                        && !callTop.IUnits.Any(p => p.GameObject.EnumCallType == EnumCallType.Unit)
                        )
                    {
                        IUnit aimUnit = callTop.IUnits.FirstOrDefault(p => p.GameObject.EnumCallType == EnumCallType.Player);
                        if (aimUnit != null)
                        {
                            unit.Angel = EAngel.Top;
                            return true;
                        }
                    }
                    else///Блок приграждает путь к целе, не разворачиватся
                        Bool1 = false;

                    ///Поиск цели cнизу
                    Map_Cell callBottom = unit.CurrentMap.Calls.FirstOrDefault(p => p.IndexLeft == xNew && p.IndexTop == yNewLocalBottom);
                    if (callBottom != null && !callBottom.Block && Bool2 && !callBottom.IUnits.Any(p => p.GameObject.EnumCallType == EnumCallType.UnitBlock)
                         && !callBottom.IUnits.Any(p => p.GameObject.EnumCallType == EnumCallType.Unit))
                    {
                        IUnit aimUnit = callBottom.IUnits.FirstOrDefault(p => p.GameObject.EnumCallType == EnumCallType.Player);
                        if (aimUnit != null)
                        {
                            unit.Angel = EAngel.Bottom;
                            return true;

                        }
                    }
                    else///Блок приграждает путь к целе, не разворачиватся
                        Bool2 = false;

                }
            }
            else if (unit.Angel == EAngel.Top || unit.Angel == EAngel.Bottom)
            {
                for (int i = 0; i <= unit.CurrentMap.Width / 50; i++)
                {
                    xNewLocalLeft = xNewLocalLeft - 1;
                    xNewLocalRoight = xNewLocalRoight + 1;

                    ///Поиск цели с лева
                    Map_Cell callLeft = unit.CurrentMap.Calls.FirstOrDefault(p => p.IndexLeft == xNewLocalLeft && p.IndexTop == yNew);
                    if (callLeft != null && !callLeft.Block && Bool1 && !callLeft.IUnits.Any(p => p.GameObject.EnumCallType == EnumCallType.UnitBlock)
                         && !callLeft.IUnits.Any(p => p.GameObject.EnumCallType == EnumCallType.Unit))
                    {
                        IUnit aimUnit = callLeft.IUnits.FirstOrDefault(p => p.GameObject.EnumCallType == EnumCallType.Player);
                        if (aimUnit != null)
                        {
                            unit.Angel = EAngel.Left;
                            return true;

                        }
                    }
                    else///Блок приграждает путь к целе, не разворачиватся
                        Bool1 = false;

                    ///Поиск цели c права
                    Map_Cell callRight = unit.CurrentMap.Calls.FirstOrDefault(p => p.IndexLeft == xNewLocalRoight && p.IndexTop == yNew);
                    if (callRight != null && !callRight.Block && Bool2 && !callRight.IUnits.Any(p => p.GameObject.EnumCallType == EnumCallType.UnitBlock)
                         && !callRight.IUnits.Any(p => p.GameObject.EnumCallType == EnumCallType.Unit))
                    {
                        IUnit aimUnit = callRight.IUnits.FirstOrDefault(p => p.GameObject.EnumCallType == EnumCallType.Player);
                        if (aimUnit != null)
                        {
                            unit.Angel = EAngel.Right;
                            return true;
                        }
                    }
                    else///Блок приграждает путь к целе, не разворачиватся
                        Bool2 = false;
                }
            }
            return false;
        }

        /// <summary>
        /// Проверка на наличие аганима
        /// </summary>
        /// <param name="unit">У кого проверяем</param>
        /// <returns>Есть/Нет</returns>
        public static bool HasAghanim(IUnit unit)
        {
            Player player = unit as Player;
            if (player != null)
            {
                foreach (var item in player.Items)
                    if (item.Name == "Aghanims Scepter")
                        return true;
            }

            return false;
        }

        public static bool HasSkadi(IUnit unit)
        {
            Player player = unit as Player;
            if (player != null)
            {
                foreach (var item in player.Items)
                    if (item.Name == "Skadi")
                        return true;
            }

            return false;
        }
    }
}
