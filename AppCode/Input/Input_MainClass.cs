using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using SharpDX;
using Microsoft.Xna.Framework.Input;
using Voins.AppCode.Input;
using Voins.AppCode;
using Voins.Spell;

namespace Voins.Input
{
    public class Input_MainClass
    {
        private EInput _inputType;

        public EInput InputType
        {
            get { return _inputType; }
            set { _inputType = value; }
        }

        private Player _player;

        public Player Player
        {
            get { return _player; }
            set { _player = value; }
        }

        private UC_View_BuyControl _bay;
        public UC_View_BuyControl Bay
        {
            get { return _bay; }
            set { _bay = value; }
        }

        DispatcherTimer _keyTimerPlayer;

        KeyboardState _keyboardState;
        GamePadState _gamepadeState;
        List<Windows.System.VirtualKey> _pressedKeys = new List<Windows.System.VirtualKey>();

        public void StartTimer(Player player, EInput inputType, UC_View_BuyControl bay)
        {
            Player = player;
            InputType = inputType;
            Bay = bay;

            if (inputType == EInput.Player1Keys || inputType == EInput.Player2Keys)
            {
                StaticVaribl.MainPage.KeyDown += MainPage_KeyDown;
                StaticVaribl.MainPage.KeyUp += MainPage_KeyUp;
                StaticVaribl.MainPage.LostFocus += MainPage_LostFocus;
            }

            //if (inputType == EInput.Joist1 ||
            //    inputType == EInput.Joist2 ||
            //    inputType == EInput.Joist3 ||
            //    inputType == EInput.Joist4)
            //{
            //    _keyTimerPlayer = new DispatcherTimer() { Interval = TimeSpan.FromSeconds(0.15) };
            //    _keyTimerPlayer.Tick += _keyTimerPlayer_Tick;
            //    _keyTimerPlayer.Start();
            //}
            _keyTimerPlayer = new DispatcherTimer() { Interval = TimeSpan.FromSeconds(0.1) };
            _keyTimerPlayer.Tick += _keyTimerPlayer_Tick;
            _keyTimerPlayer.Start();
        }

        void MainPage_LostFocus(object sender, RoutedEventArgs e)
        {
            _pressedKeys.Clear();
        }

        void MainPage_KeyUp(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
            _pressedKeys.Remove(e.Key);
        }

        void MainPage_KeyDown(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
            if (!_pressedKeys.Any(p => p == e.Key))
                _pressedKeys.Add(e.Key);
        }

        void _keyTimerPlayer_Tick(object sender, object e)
        {
            Gamepad();

            if (_inputType == EInput.Player1Keys || _inputType == EInput.Player2Keys)
            {
                StaticVaribl.MainPage.Focus(FocusState.Keyboard);

                if (_inputType == EInput.Player1Keys)
                {
                    if (Bay.Visibility == Visibility.Collapsed)
                    {
                        if (_pressedKeys.Any(p => p == Windows.System.VirtualKey.G))
                            ///Left
                            _player.UseSpall(0, EAngel.Left);
                        else if (_pressedKeys.Any(p => p == Windows.System.VirtualKey.Y))
                            ///Top
                            _player.UseSpall(0, EAngel.Top);
                        else if (_pressedKeys.Any(p => p == Windows.System.VirtualKey.J))
                            ///Right
                            _player.UseSpall(0, EAngel.Right);
                        else if (_pressedKeys.Any(p => p == Windows.System.VirtualKey.H))
                            ///Bottom
                            _player.UseSpall(0, EAngel.Bottom);

                        if (_pressedKeys.Any(p => p == Windows.System.VirtualKey.Q))
                            _player.UseSpall(1, 1);
                        if (_pressedKeys.Any(p => p == Windows.System.VirtualKey.W))
                            _player.UseSpall(2, 1);
                        if (_pressedKeys.Any(p => p == Windows.System.VirtualKey.E))
                            _player.UseSpall(3, 1);
                        if (_pressedKeys.Any(p => p == Windows.System.VirtualKey.R))
                            _player.UseSpall(4, 1);

                        if (_pressedKeys.Any(p => p == Windows.System.VirtualKey.C))
                            UpSpell();

                        if (_pressedKeys.Any(p => p == Windows.System.VirtualKey.V))
                            ItemChengPosition();

                        if (_pressedKeys.Any(p => p == Windows.System.VirtualKey.A))
                            _player.UseItemSpall(0, null);
                        if (_pressedKeys.Any(p => p == Windows.System.VirtualKey.S))
                            _player.UseItemSpall(1, null);
                        if (_pressedKeys.Any(p => p == Windows.System.VirtualKey.D))
                            _player.UseItemSpall(2, null);
                        if (_pressedKeys.Any(p => p == Windows.System.VirtualKey.F))
                            _player.UseItemSpall(3, null);

                        if (_pressedKeys.Any(p => p == Windows.System.VirtualKey.Z))
                            Bay.Show(_player);
                    }
                    else if (Bay.CurrentPlayer == _player)
                    {
                        if (_pressedKeys.Any(p => p == Windows.System.VirtualKey.Q))
                            Bay.BuyItem();

                        if (_pressedKeys.Any(p => p == Windows.System.VirtualKey.Y))
                            Bay.IndexLeft();

                        if (_pressedKeys.Any(p => p == Windows.System.VirtualKey.H))
                            Bay.IndexRight();

                        if (_pressedKeys.Any(p => p == Windows.System.VirtualKey.Z))
                            Bay.Close();
                    }
                }


                if (_inputType == EInput.Player2Keys)
                {
                    if (Bay.Visibility == Visibility.Collapsed)
                    {
                        if (_pressedKeys.Any(p => p == Windows.System.VirtualKey.NumberPad4))
                            ///Left
                            _player.UseSpall(0, EAngel.Left);
                        else if (_pressedKeys.Any(p => p == Windows.System.VirtualKey.NumberPad8))
                            ///Top
                            _player.UseSpall(0, EAngel.Top);
                        else if (_pressedKeys.Any(p => p == Windows.System.VirtualKey.NumberPad6))
                            ///Right
                            _player.UseSpall(0, EAngel.Right);
                        else if (_pressedKeys.Any(p => p == Windows.System.VirtualKey.NumberPad5))
                            ///Bottom
                            _player.UseSpall(0, EAngel.Bottom);

                        if (_pressedKeys.Any(p => p == Windows.System.VirtualKey.Left))
                            _player.UseSpall(1, 1);
                        if (_pressedKeys.Any(p => p == Windows.System.VirtualKey.Down))
                            _player.UseSpall(2, 1);
                        if (_pressedKeys.Any(p => p == Windows.System.VirtualKey.Right))
                            _player.UseSpall(3, 1);
                        if (_pressedKeys.Any(p => p == Windows.System.VirtualKey.Up))
                            _player.UseSpall(4, 1);

                        if (_pressedKeys.Any(p => p == Windows.System.VirtualKey.NumberPad0))
                            UpSpell();

                        if (_pressedKeys.Any(p => p == Windows.System.VirtualKey.NumberPad3))
                            ItemChengPosition();

                        if (_pressedKeys.Any(p => p == Windows.System.VirtualKey.NumberPad7))
                            _player.UseItemSpall(0, null);
                        if (_pressedKeys.Any(p => p == Windows.System.VirtualKey.NumberPad9))
                            _player.UseItemSpall(1, null);
                        if (_pressedKeys.Any(p => p == Windows.System.VirtualKey.NumberPad1))
                            _player.UseItemSpall(2, null);
                        if (_pressedKeys.Any(p => p == Windows.System.VirtualKey.NumberPad2))
                            _player.UseItemSpall(3, null);

                        if (_pressedKeys.Any(p => p == Windows.System.VirtualKey.Delete))
                            Bay.Show(_player);
                    }
                    else if (Bay.CurrentPlayer == _player)
                    {
                        if (_pressedKeys.Any(p => p == Windows.System.VirtualKey.NumberPad7))
                            Bay.BuyItem();

                        if (_pressedKeys.Any(p => p == Windows.System.VirtualKey.NumberPad8))
                            Bay.IndexLeft();

                        if (_pressedKeys.Any(p => p == Windows.System.VirtualKey.NumberPad5))
                            Bay.IndexRight();

                        if (_pressedKeys.Any(p => p == Windows.System.VirtualKey.Delete))
                            Bay.Close();
                    }
                }
            }
        }

        private void Gamepad()
        {
            if (_inputType == EInput.Joist1)
                _gamepadeState = GamePad.GetState(Microsoft.Xna.Framework.PlayerIndex.One);
            else if (_inputType == EInput.Joist2)
                _gamepadeState = GamePad.GetState(Microsoft.Xna.Framework.PlayerIndex.Two);
            else if (_inputType == EInput.Joist3)
                _gamepadeState = GamePad.GetState(Microsoft.Xna.Framework.PlayerIndex.Three);
            else if (_inputType == EInput.Joist4)
                _gamepadeState = GamePad.GetState(Microsoft.Xna.Framework.PlayerIndex.Four);

            if (_gamepadeState.IsConnected)
            {
                if (Player != null)
                {
                    if (Bay.Visibility == Visibility.Collapsed)
                    {
                        if (_gamepadeState.ThumbSticks.Left.X > 0.5)
                            ///Right
                            _player.UseSpall(0, EAngel.Right);
                        else if (_gamepadeState.ThumbSticks.Left.Y > 0.5)
                            ///Top
                            _player.UseSpall(0, EAngel.Top);
                        else if (_gamepadeState.ThumbSticks.Left.X < -0.5)
                            ///Left
                            _player.UseSpall(0, EAngel.Left);
                        else if (_gamepadeState.ThumbSticks.Left.Y < -0.5)
                            ///Bottom
                            _player.UseSpall(0, EAngel.Bottom);

                        if (_gamepadeState.IsButtonDown(Buttons.A))
                            _player.UseSpall(2, 1);
                        if (_gamepadeState.IsButtonDown(Buttons.X))
                            _player.UseSpall(1, 1);
                        if (_gamepadeState.IsButtonDown(Buttons.B))
                            _player.UseSpall(3, 1);
                        if (_gamepadeState.IsButtonDown(Buttons.Y))
                            _player.UseSpall(4, 1);

                        if (_gamepadeState.IsButtonDown(Buttons.RightShoulder))
                            ///Юзаем ап спела
                            UpSpell();

                        if (_gamepadeState.IsButtonDown(Buttons.LeftShoulder))
                            ///Юзаем смену мест предметам
                            ItemChengPosition();

                        if (_gamepadeState.DPad.Left == ButtonState.Pressed)
                            _player.UseItemSpall(0, null);
                        if (_gamepadeState.DPad.Up == ButtonState.Pressed)
                            _player.UseItemSpall(1, null);
                        if (_gamepadeState.DPad.Right == ButtonState.Pressed)
                            _player.UseItemSpall(2, null);
                        if (_gamepadeState.DPad.Down == ButtonState.Pressed)
                            _player.UseItemSpall(3, null);

                        if (_gamepadeState.Triggers.Right > 0.5)
                        { ///Юзаем спел с самым маленьким кулдауном
                            if (_player.Spells.Count > 0 && !_player.Dead)
                            {
                                ISpell minCdSpell = _player.Spells.FirstOrDefault(p => p.SpellDescriptionInfo.MinCd);
                                if (minCdSpell != null)
                                    minCdSpell.UseSpall(_player.CurrentMap, null, _player, null);
                            }
                        }

                        if (_gamepadeState.Triggers.Left > 0.5)
                            _player.UseItemSpall(0, null);

                        if (_gamepadeState.IsButtonDown(Buttons.Start))
                            StaticVaribl.MainPage.Start2PlayrGame(null, null);

                        if (_gamepadeState.IsButtonDown(Buttons.Back))
                            Bay.Show(_player);

                        if (_player.AllowUpSpell &&
                            _player.UpPoint > 0)
                        {
                            if (_gamepadeState.ThumbSticks.Right.X > 0.5)
                                ///Left
                                _player.UseSpall(3, 1);
                            else if (_gamepadeState.ThumbSticks.Right.Y > 0.5)
                                ///Top
                                _player.UseSpall(4, 1);
                            else if (_gamepadeState.ThumbSticks.Right.X < -0.5)
                                ///Right
                                _player.UseSpall(1, 1);
                            else if (_gamepadeState.ThumbSticks.Right.Y < -0.5)
                                ///Bottom
                                _player.UseSpall(2, 1);
                        }
                    }
                    else if (Bay.CurrentPlayer == _player)
                    {
                        if (_gamepadeState.IsButtonDown(Buttons.A))
                            Bay.BuyItem();

                        if (_gamepadeState.ThumbSticks.Left.Y > 0.5)
                            Bay.IndexLeft();

                        if (_gamepadeState.ThumbSticks.Left.Y < -0.5)
                            Bay.IndexRight();

                        if (_gamepadeState.IsButtonDown(Buttons.Back))
                            Bay.Close();
                    }

                }
            }
        }

        private void ItemChengPosition()
        {
            for (int i = 0; i < _player.Items.Count; i++)
            {
                if (i != _player.Items.Count - 1)
                {
                    var item = _player.Items.FirstOrDefault();
                    _player.RemoveItem(item, false);
                    _player.AddItem(item);
                }
            }
        }

        private void UpSpell()
        {
            if (!_player.AllowUpSpell &&
                _player.UpPoint > 0)
            {
                //if (_player.AllowUpSpell)
                //    _player.AllowUpSpell = false;
                //else
                _player.AllowUpSpell = true;

                _player.UpdateView();
            }
        }

        public void StopTimer()
        {
            _keyTimerPlayer.Tick -= _keyTimerPlayer_Tick;
            _keyTimerPlayer.Stop();
            _keyTimerPlayer = null;
        }

        public void StopInput()
        {
            StopTimer();
            if (_inputType == EInput.Player1Keys || _inputType == EInput.Player2Keys)
            {
                StaticVaribl.MainPage.KeyDown -= MainPage_KeyDown;
                StaticVaribl.MainPage.KeyUp -= MainPage_KeyUp;
                StaticVaribl.MainPage.LostFocus -= MainPage_LostFocus;
            }
        }

    }
}
