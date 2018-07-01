using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Voins.AppCode;
using Voins.Spell;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Voins
{
    public sealed partial class UC_View_PlaerPanel : UserControl
    {
        private Player _player;
        public Player CurrentPlayer
        {
            get { return _player; }
            set { _player = value; }
        }
        DispatcherTimer _respaumTimer;
        int _secondCounter = 0;

        public UC_View_PlaerPanel()
        {
            this.InitializeComponent();
        }

        public void Show()
        {
            if (CurrentPlayer != null)
            {

                CurrentPlayer.UpdatePlayerData += CurrentPlayer_UpdatePlayerData;
                CurrentPlayer.DeadEvent += CurrentPlayer_DeadEvent;

                ShowSkills();
                UpdateView();
                ShowItem();
            }
        }

        void CurrentPlayer_DeadEvent(object sender, EventArgs e)
        {
            if (CurrentPlayer.AllowRespaum)
            {
                C_Respaum.Visibility = Windows.UI.Xaml.Visibility.Visible;
                _secondCounter = 0;
                C_Respaum.Text = "Reap: " + (CurrentPlayer.RespaumTime - _secondCounter).ToString() + " s";

                _respaumTimer = new DispatcherTimer() { Interval = TimeSpan.FromSeconds(1) };
                _respaumTimer.Tick += _respaumTimer_Tick;
                _respaumTimer.Start();
            }
        }

        void _respaumTimer_Tick(object sender, object e)
        {
            if (!CurrentPlayer.Paused)
            {
                if (CurrentPlayer.RespaumTime > _secondCounter)
                {
                    C_Respaum.Text = "Reap: " + (CurrentPlayer.RespaumTime - _secondCounter).ToString() + " s";
                    _secondCounter++;
                }
                else
                {
                    _respaumTimer.Tick -= _respaumTimer_Tick;
                    _respaumTimer.Stop();
                    C_Respaum.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                }
            }
        }

        private void ShowItem()
        {
            C_Items.CurrentPlayer = CurrentPlayer;
            C_Items.Show();
        }

        /// <summary>
        /// Отображение скилов 
        /// </summary>
        private void ShowSkills()
        {
            C_Spells.CurrentPlayer = CurrentPlayer;
            C_Spells.Show();
        }

        void CurrentPlayer_UpdatePlayerData(object sender, EventArgs e)
        {
            UpdateView();
        }
        private void UpdateView()
        {
            C_Name.Text = CurrentPlayer.Name;
            C_Level.Text = "Lvl: " + CurrentPlayer.Level;
            C_HelthPr.Maximum = (double)CurrentPlayer.MaxHealth;
            C_HelthPr.Value = (double)CurrentPlayer.Health;
            C_Helth.Text = "Helth: " + CurrentPlayer.Health.ToString() + "//" + CurrentPlayer.MaxHealth.ToString();

            C_ManaPr.Maximum = (double)CurrentPlayer.MaxMana;
            C_ManaPr.Value = (double)CurrentPlayer.Mana;
            C_Mana.Text = "Mana: " + CurrentPlayer.Mana.ToString() + "//" + CurrentPlayer.MaxMana.ToString();

            C_ExpPr.Maximum = (double)CurrentPlayer.MaxExp;
            C_ExpPr.Value = (double)CurrentPlayer.Exp;
            C_Exp.Text = "Exp: " + CurrentPlayer.Exp.ToString() + "//" + CurrentPlayer.MaxExp.ToString();

            C_Attack.Text = CurrentPlayer.Demage.ToString();
            C_Speed.Text = CurrentPlayer.Speed.ToString();
            C_Arrmor.Text = CurrentPlayer.Arrmor.ToString();

            C_AttackSpeed.Text = CurrentPlayer.AttackSpeed.ToString();
            C_ManaRegeneration.Text = CurrentPlayer.ManaRegeneration.ToString();
            C_HelthRegeneration.Text = CurrentPlayer.HealthRegeneration.ToString();

            C_Gold.Text = CurrentPlayer.Gold.ToString();
            C_Kills.Text = CurrentPlayer.Kills.ToString();
            C_Death.Text = CurrentPlayer.Death.ToString();

            C_Spells.KeyUpSpellView(CurrentPlayer.AllowUpSpell);

            if (CurrentPlayer.Silenced)
                C_Silenc.Visibility = Windows.UI.Xaml.Visibility.Visible;
            else
                C_Silenc.Visibility = Windows.UI.Xaml.Visibility.Collapsed;

            if (CurrentPlayer.Buffs.Any(p => p.Stun))
                C_Stun.Visibility = Windows.UI.Xaml.Visibility.Visible;
            else
                C_Stun.Visibility = Windows.UI.Xaml.Visibility.Collapsed;

        }


    }
}
