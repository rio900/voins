using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Voins.AppCode;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Voins.Spell;
using Windows.System;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Voins
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        List<Voins.Input.Input_MainClass> _inputArr = new List<Input.Input_MainClass>();
        Player _player;
        Player _player2;
        Player _player3;
        Player _player4;

        Map _map;

        EGameType _gameType = EGameType.Coop;

        public MainPage()
        {
            this.InitializeComponent();
            StaticVaribl.MainPage = this;

            C_Menu.PlayeVsPlayerStart += C_Menu_PlayeVsPlayerStart;
            C_Menu.Coop1PlayerStart += C_Menu_Coop1PlayerStart;
            C_Menu.CampaningStart += C_Menu_CampaningStart;
            C_Menu.Coop2PlayerStart += C_Menu_Coop2PlayerStart;
            CreateGame();

            C_View_SelectHeroy1.GetName("player");
            C_View_SelectHeroy2.GetName("player2");
            C_View_SelectHeroy4.GetName("player3");
            C_View_SelectHeroy5.GetName("player4");
            //ControlsKeyboard();
        }

        void C_Menu_CampaningStart(object sender, EventArgs e)
        {

            if (_map != null)
                _map.DeleteCurrentMap();

            _map.CreateRandomMap(true);
            _player = UnitGenerator.Player_Bonik("Bonik", 1, _map);
            _player.ShowShop += _player_ShowShop;

            _player.StatisticData = new StatisticData();

            _player.GroupType = 1;
            _map.CreatePlayer(_player);

            _map.KillLimit = 5;
            _map.CreateNewCampaning(0);

            _player.StartRegenerationTimers_NewObject();
            C_View_Player1.CurrentPlayer = _player;
            C_View_Player1.Show();
        }

        void C_Menu_Coop2PlayerStart(object sender, EventArgs e)
        {
            _gameType = EGameType.Coop;
            C_TwoPlayerGrid.Visibility = Windows.UI.Xaml.Visibility.Visible;
        }

        void C_Menu_Coop1PlayerStart(object sender, EventArgs e)
        {
            ShowMainMenu();
        }

        private void ShowMainMenu()
        {
            C_GameOver.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            C_OnePlayerGrid.Visibility = Windows.UI.Xaml.Visibility.Visible;

        }

        void C_Menu_PlayeVsPlayerStart(object sender, EventArgs e)
        {
            _gameType = EGameType.FurmGame;
            C_TwoPlayerGrid.Visibility = Windows.UI.Xaml.Visibility.Visible;
        }

        private void CreateGame()
        {
            C_Root.Children.Clear();
            _map = new Map(C_Root);
            _map.VinEvent += _map_VinEvent;

        }

        void _map_VinEvent(object sender, EventArgs e)
        {
            if (_map.GameType == EGameType.FurmGame)
            {
                C_GameOver.Show(_map, ((Player)sender).Name);
            }
            else if (_map.GameType == EGameType.Coop)
            {
                C_GameOver.Show(_map, "Mob attacks: " + sender.ToString());
            }

            foreach (var item in _inputArr)
                item.StopInput();
            _inputArr.Clear();

            C_Backgroung.Visibility = Windows.UI.Xaml.Visibility.Visible;
            C_Menu.Visibility = Windows.UI.Xaml.Visibility.Visible;
        }

        void _player_ShowShop(object sender, EventArgs e)
        {
            C_Buy.Show(sender as Player);
        }

        void move_E_TestEvent(object sender, EventArgs e)
        {
            C_Test.Text += "\n" + sender.ToString();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        { }

        #region Buttons
        private void MuveTop(object sender, RoutedEventArgs e)
        {
            _map.Pause();
        }

        private void MuveLeft(object sender, RoutedEventArgs e)
        {
            _map.StopPause();
        }

        private void Spell1Bottom(object sender, RoutedEventArgs e)
        {
            C_GameOver.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            C_Backgroung.Visibility = Windows.UI.Xaml.Visibility.Visible;
            C_Menu.Visibility = Windows.UI.Xaml.Visibility.Visible;
            ResetInput();
            //if (_map != null)
            //    _map.DeleteCurrentMap();

            //_map.CreateRandomMap();
            //// _player2 = UnitGenerator.Player_Alchemist("Alchemist2", 2, _map);
            //_player = UnitGenerator.Player_Sniper("Bonik", 3, _map);
            //// _player2 = UnitGenerator.Player_Alchemist("Bonik 2", 2, _map);
            //// _player2 = UnitGenerator.Player_Bonik("Bonik 2", 2, _map);

            //_player.ShowShop += _player_ShowShop;
            ////   _player2.ShowShop += _player_ShowShop;

            //_player.StatisticData = new StatisticData();
            ////  _player2.StatisticData = new StatisticData();

            ////  _player2.GroupType =2;
            //_player.GroupType = 1;

            //_map.CreatePlayer(_player);
            ////   _map.CreatePlayer(_player2);

            //_map.KillLimit = 10;
            ////  _map.CreateNewCampaning(0);
            ////_map.KillLimit = 5;
            //// _map.CreateNewFarmGame();
            //_map.CreateNewCooperative();

            //_player.StartRegenerationTimers_NewObject();
            //// _player2.StartRegenerationTimers_NewObject();

            //C_View_Player1.CurrentPlayer = _player;
            //C_View_Player1.Show();

            //// C_View_Player2.CurrentPlayer = _player2;
            //C_View_Player2.Show();
        }

        private void ResetInput()
        {
            foreach (var item in _inputArr)
                item.StopInput();
            _inputArr.Clear();
        }
        #endregion

        private void Page_ManipulationCompleted_1(object sender, Windows.UI.Xaml.Input.ManipulationCompletedRoutedEventArgs e)
        {
            // TODO: Add event handler implementation here.
        }

        private void Page_ManipulationInertiaStarting_1(object sender, Windows.UI.Xaml.Input.ManipulationInertiaStartingRoutedEventArgs e)
        {
            // TODO: Add event handler implementation here.
        }

        private void Player1Game(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            // TODO: Add event handler implementation here.
        }

        private void StartPvP(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            C_TwoPlayerGrid.Visibility = Windows.UI.Xaml.Visibility.Visible;
        }

        private void StartCoop1Player(object sender, RoutedEventArgs e)
        {
            C_OnePlayerGrid.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            C_Backgroung.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            if (_map != null)
                _map.DeleteCurrentMap();

            _map.CreateRandomMap(false);
            _player = C_View_SelectHeroy3.ReturnHero();
            _player.CurrentMap = _map;
            _player.ShowShop += _player_ShowShop;

            _player.StatisticData = new StatisticData();

            _player.GroupType = 1;

            _map.CreatePlayer(_player);

            _map.KillLimit = 10;
            _map.CreateNewCooperative();

            _player.StartRegenerationTimers_NewObject();

            C_View_Player1.CurrentPlayer = _player;
            C_View_Player1.Show();

            C_View_Player2.Show();

            Voins.Input.Input_MainClass player1_input = new Input.Input_MainClass();
            player1_input.StartTimer(_player, AppCode.Input.EInput.Joist1, C_Buy);

        }

        public void Start2PlayrGame(object sender, RoutedEventArgs e)
        {
            int playerCount = 3;

            ResetInput();
            C_Backgroung.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            C_TwoPlayerGrid.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            C_GameOver.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            if (_map != null)
                _map.DeleteCurrentMap();

            if (playerCount >= 1)
                _player = C_View_SelectHeroy1.ReturnHero();
            if (playerCount >= 2)
                _player2 = C_View_SelectHeroy2.ReturnHero();
            if (playerCount >= 3)
                _player3 = C_View_SelectHeroy4.ReturnHero();
            if (playerCount >= 4)
                _player4 = C_View_SelectHeroy5.ReturnHero();

            if (playerCount >= 1)
                _player.Gold = 100;
            if (playerCount >= 2)
                _player2.Gold = 100;
            if (playerCount >= 3)
                _player3.Gold = 100;
            if (playerCount >= 4)
                _player4.Gold = 100;

            if (playerCount >= 1)
                _player.CurrentMap = _map;
            if (playerCount >= 2)
                _player2.CurrentMap = _map;
            if (playerCount >= 3)
                _player3.CurrentMap = _map;
            if (playerCount >= 4)
                _player4.CurrentMap = _map;

            _map.CreateRandomMap(false);

            if (playerCount >= 1)
                _player.ShowShop += _player_ShowShop;
            if (playerCount >= 2)
                _player2.ShowShop += _player_ShowShop;
            if (playerCount >= 3)
                _player3.ShowShop += _player_ShowShop;
            if (playerCount >= 4)
                _player4.ShowShop += _player_ShowShop;

            if (playerCount >= 1)
                _player.StatisticData = new StatisticData();
            if (playerCount >= 2)
                _player2.StatisticData = new StatisticData();
            if (playerCount >= 3)
                _player3.StatisticData = new StatisticData();
            if (playerCount >= 4)
                _player4.StatisticData = new StatisticData();

            if (playerCount >= 1)
                _map.CreatePlayer(_player);
            if (playerCount >= 2)
                _map.CreatePlayer(_player2);
            if (playerCount >= 3)
                _map.CreatePlayer(_player3);
            if (playerCount >= 4)
                _map.CreatePlayer(_player4);

            if (_gameType == EGameType.FurmGame)
            {
                _map.KillLimit = 10;

                if (playerCount >= 1)
                    _player.GroupType = 3;// C_View_SelectHeroy1.GroupNumber;
                if (playerCount >= 2)
                    _player2.GroupType = 2;//C_View_SelectHeroy2.GroupNumber;
                if (playerCount >= 3)
                    _player3.GroupType = 1;// C_View_SelectHeroy4.GroupNumber;
                if (playerCount >= 4)
                    _player4.GroupType = 2;// C_View_SelectHeroy5.GroupNumber;

                _map.CreateNewFarmGame(0, 1);
                // _map.CreateNewFarmGame(4,2);

            }
            else if (_gameType == EGameType.Coop)
            {
                _map.KillLimit = 10;

                if (playerCount >= 1)
                    _player.GroupType = 1;
                if (playerCount >= 2)
                    _player2.GroupType = 1;
                if (playerCount >= 3)
                    _player3.GroupType = 1;
                if (playerCount >= 4)
                    _player4.GroupType = 1;
                _map.CreateNewCooperative();
            }

            if (playerCount >= 1)
                _player.StartRegenerationTimers_NewObject();
            if (playerCount >= 2)
                _player2.StartRegenerationTimers_NewObject();
            if (playerCount >= 3)
                _player3.StartRegenerationTimers_NewObject();
            if (playerCount >= 4)
                _player4.StartRegenerationTimers_NewObject();

            if (playerCount >= 1)
            {
                C_View_Player1.CurrentPlayer = _player;
                C_View_Player1.Show();
            }
            if (playerCount >= 2)
            {
                C_View_Player2.CurrentPlayer = _player2;
                C_View_Player2.Show();
            }
            if (playerCount >= 3)
            {
                C_View_Player3.CurrentPlayer = _player3;
                C_View_Player3.Show();
            }
            if (playerCount >= 4)
            {
                C_View_Player4.CurrentPlayer = _player4;
                C_View_Player4.Show();
            }
            if (playerCount >= 1)
            {
                Voins.Input.Input_MainClass player1_input = new Input.Input_MainClass();
                //player1_input.StartTimer(_player, AppCode.Input.EInput.Joist1, C_Buy);
                  player1_input.StartTimer(_player, AppCode.Input.EInput.Player1Keys, C_Buy);
                _inputArr.Add(player1_input);
            }

            if (playerCount >= 2)
            {
                Voins.Input.Input_MainClass player2_input = new Input.Input_MainClass();
                //player2_input.StartTimer(_player2, AppCode.Input.EInput.Player1Keys, C_Buy);
                player2_input.StartTimer(_player2, AppCode.Input.EInput.Joist2, C_Buy);
                _inputArr.Add(player2_input);
            }

            if (playerCount >= 3)
            {
                Voins.Input.Input_MainClass player3_input = new Input.Input_MainClass();
                player3_input.StartTimer(_player3, AppCode.Input.EInput.Joist3, C_Buy);
                _inputArr.Add(player3_input);
            }

            if (playerCount >= 4)
            {
                Voins.Input.Input_MainClass player4_input = new Input.Input_MainClass();
                player4_input.StartTimer(_player4, AppCode.Input.EInput.Joist4, C_Buy);
                _inputArr.Add(player4_input);
            }
        }
    }
}
