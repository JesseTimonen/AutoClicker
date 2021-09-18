using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace AutoClicker
{
    public partial class AutoClickerForm
    {
        private void toggleHotkeysButton_Click(object sender, EventArgs e)
        {
            hotkeysEnabled = !hotkeysEnabled;

            if (hotkeysEnabled)
            {
                EnableHotkeysUIButtons(false);
                ToggleHotkeysButton.Text = "Disable Hotkeys";
                HotkeysStatusLabel.Text = "ON";
                HotkeysStatusLabel.ForeColor = Color.Green;
                ToggleHotkeysButton.FlatAppearance.BorderColor = Color.Green;
                ToggleHotkeysButton.FlatAppearance.BorderSize = 2;

                hotkeyIDCounter = 0;
                foreach (Hotkey savedHotkey in savedHotkeys)
                {
                    keyHandlers.Add(new KeyHandler(KeyHandler.ConvertToKey(hotkeyInputTextboxes[hotkeyIDCounter].Text), hotkeyIDCounter, this));
                    hotkeyIDCounter++;
                }

                hotkeyIDCounter = 1000;
                foreach (Macro savedMacro in savedMacros)
                {
                    keyHandlers.Add(new KeyHandler(KeyHandler.ConvertToKey(macroHotkeyTextboxes[hotkeyIDCounter - 1000].Text), hotkeyIDCounter, this));
                    hotkeyIDCounter++;
                }

                foreach (KeyHandler keyHandler in keyHandlers)
                {
                    keyHandler.Register();
                }
            }
            else
            {
                EnableHotkeysUIButtons(true);
                ToggleHotkeysButton.Text = "Enable Hotkeys";
                HotkeysStatusLabel.Text = "OFF";
                HotkeysStatusLabel.ForeColor = Color.Red;
                ToggleHotkeysButton.FlatAppearance.BorderColor = Color.Red;
                ToggleHotkeysButton.FlatAppearance.BorderSize = 1;

                foreach (KeyHandler keyHandler in keyHandlers)
                {
                    keyHandler.Unregister();
                }

                keyHandlers.Clear();
            }
        }

        private void SaveHotkeysButton_Click(object sender, EventArgs e)
        {
            if (savedHotkeys.Count == 0) { return; }

            int index = 0;
            List<Hotkey> _savedHotkey = new List<Hotkey>();

            foreach (Hotkey savedHotkey in savedHotkeys)
            {
                _savedHotkey.Add(new Hotkey(hotkeyNameTextboxes[index].Text, hotkeyInputTextboxes[index].Text, savedHotkey.X, savedHotkey.Y));
                index++;
            }

            savedHotkeys.Clear();
            savedHotkeys = _savedHotkey;
            SaveHotkeys();
            UpdateHotkeysUI();
        }

        private void EnableHotkeysUIButtons(bool enable)
        {
            SaveHotkeysButton.Enabled = enable;

            foreach (var deleteHotkeyButton in deleteHotkeyButtons)
            {
                deleteHotkeyButton.Enabled = enable;
            }
            foreach (var hotkeyNameTextbox in hotkeyNameTextboxes)
            {
                hotkeyNameTextbox.Enabled = enable;
            }
            foreach (var hotkeyInputTextbox in hotkeyInputTextboxes)
            {
                hotkeyInputTextbox.Enabled = enable;
            }
            foreach (var macroHotkeyTextbox in macroHotkeyTextboxes)
            {
                macroHotkeyTextbox.Enabled = enable;
            }
        }

        private void StartHotkeyTimer()
        {
            hotkeyTimerStep = 0;
            HotkeyTimer.Start();
        }

        private void StopHotkeyTimer()
        {
            HotkeyTimer.Stop();
        }

        private void HotkeyTimer_Tick(object sender, EventArgs e)
        {
            if (hotkeyTimerStep == 0)
            {
                mouseLastPosition = new int[] { Cursor.Position.X, Cursor.Position.Y };
                MoveMouseToPosition(hotkeyInstructions[0], hotkeyInstructions[1]);
            }
            else if (hotkeyTimerStep == 1)
            {
                LeftMouseClick(hotkeyInstructions[0], hotkeyInstructions[1]);
            }
            else if (hotkeyTimerStep >= 2)
            {
                MoveMouseToPosition(mouseLastPosition[0], mouseLastPosition[1]);
                StopHotkeyTimer();
            }

            hotkeyTimerStep++;
        }
    }
}