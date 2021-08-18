using System;
using System.Windows.Forms;

namespace AutoClicker
{
    public partial class AutoClickerForm
    {
        private void SettingsStartMacroHotkeyTextbox_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SettingsStartMacroTextbox.Text = ValidateSettingsHotkeys(SettingsStartMacroTextbox.Text);
        }

        private void SettingsStopMacroTextbox_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SettingsStopMacroTextbox.Text = ValidateSettingsHotkeys(SettingsStopMacroTextbox.Text);
        }

        private void SettingsCreateHotkeyTextbox_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SettingsCreateHotkeyTextbox.Text = ValidateSettingsHotkeys(SettingsCreateHotkeyTextbox.Text);
        }

        private void SettingsStartAutoClickerTextbox_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SettingsStartAutoClickerTextbox.Text = ValidateSettingsHotkeys(SettingsStartAutoClickerTextbox.Text);
        }

        private void SettingsStopAutoClickerTextbox_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SettingsStopAutoClickerTextbox.Text = ValidateSettingsHotkeys(SettingsStopAutoClickerTextbox.Text);
        }

        private void SettingsStartAutoKeyboardTextbox_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SettingsStartAutoKeyboardTextbox.Text = ValidateSettingsHotkeys(SettingsStartAutoKeyboardTextbox.Text);
        }

        private void SettingsStopAutoKeyboardTextbox_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SettingsStopAutoKeyboardTextbox.Text = ValidateSettingsHotkeys(SettingsStopAutoKeyboardTextbox.Text);
        }

        private void SettingsSameMacroKeyCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            SettingsStopMacroTextbox.Enabled = !SettingsSameMacroHotkeyCheckbox.Checked;
            SettingsStopMacroTextbox.Text = "None";
        }

        private void SettingsSameAutoClickerKeyCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            SettingsStopAutoClickerTextbox.Enabled = !SettingsSameAutoClickerHotkeyCheckbox.Checked;
            SettingsStopAutoClickerTextbox.Text = "None";
        }

        private void SettingsSameAutoKeyboardHotkeyCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            SettingsStopAutoKeyboardTextbox.Enabled = !SettingsSameAutoKeyboardHotkeyCheckbox.Checked;
            SettingsStopAutoKeyboardTextbox.Text = "None";
        }

        private string ValidateSettingsHotkeys(string hotkey)
        {
            if (hotkey.ToLower() == "none") { return hotkey; }

            if (KeyHandler.ConvertToFKey(hotkey) == Keys.None)
            {
                return "Invalid";
            }
            else
            {
                int hotkeysFound = 0;
                if (hotkey.ToLower() == SettingsStartMacroTextbox.Text.ToLower()) { hotkeysFound++; }
                if (hotkey.ToLower() == SettingsStopMacroTextbox.Text.ToLower()) { hotkeysFound++; }
                if (hotkey.ToLower() == SettingsCreateHotkeyTextbox.Text.ToLower()) { hotkeysFound++; }
                if (hotkey.ToLower() == SettingsStartAutoClickerTextbox.Text.ToLower()) { hotkeysFound++; }
                if (hotkey.ToLower() == SettingsStopAutoClickerTextbox.Text.ToLower()) { hotkeysFound++; }
                if (hotkey.ToLower() == SettingsStartAutoKeyboardTextbox.Text.ToLower()) { hotkeysFound++; }
                if (hotkey.ToLower() == SettingsStopAutoKeyboardTextbox.Text.ToLower()) { hotkeysFound++; }
                if (hotkeysFound >= 2) { return "Already in use"; }
            }

            return hotkey.ToUpper();
        }

        private void SaveSettingsButton_Click(object sender, EventArgs e)
        {
            settings.Clear();

            if (SettingsFastModeRadioButton.Checked)
            {
                settings.Add("EngineSpeed", "fast");
            }
            else
            {
                settings.Add("EngineSpeed", "safe");
            }

            settings.Add("ClickWhileMoving", SettingsClickWhileMovingCheckbox.Checked.ToString());
            settings.Add("DelayAutoClicker", SettingsDelayAutoClickerCheckbox.Checked.ToString());
            settings.Add("SameHotkeyForMacros", SettingsSameMacroHotkeyCheckbox.Checked.ToString());
            settings.Add("SameHotkeyForAutoClicker", SettingsSameAutoClickerHotkeyCheckbox.Checked.ToString());
            settings.Add("SameHotkeyForAutoKeyboard", SettingsSameAutoKeyboardHotkeyCheckbox.Checked.ToString());
            settings.Add("StartMacroHotkey", SettingsStartMacroTextbox.Text);
            settings.Add("StopMacroHotkey", SettingsStopMacroTextbox.Text);
            settings.Add("StartAutoClickerHotkey", SettingsStartAutoClickerTextbox.Text);
            settings.Add("StopAutoClickerHotkey", SettingsStopAutoClickerTextbox.Text);
            settings.Add("StartAutoKeyboardHotkey", SettingsStartAutoKeyboardTextbox.Text);
            settings.Add("StopAutoKeyboardHotkey", SettingsStopAutoKeyboardTextbox.Text);
            settings.Add("CreateHotkeyHotkey", SettingsCreateHotkeyTextbox.Text);

            SaveSettings();
            LoadData();
            UpdateTimerInterval();

            startMacroKeyHandler.Unregister();
            stopMacroKeyHandler.Unregister();
            newHotkeyKeyHandler.Unregister();
            startAutoClickerKeyHandler.Unregister();
            stopAutoClickerKeyHandler.Unregister();
            startAutoKeyboardKeyHandler.Unregister();
            stopAutoKeyboardKeyHandler.Unregister();
            startMacroKeyHandler = new KeyHandler(KeyHandler.ConvertToFKey(settings["StartMacroHotkey"]), 10001, this);
            stopMacroKeyHandler = new KeyHandler(KeyHandler.ConvertToFKey(settings["StopMacroHotkey"]), 10002, this);
            newHotkeyKeyHandler = new KeyHandler(KeyHandler.ConvertToFKey(settings["CreateHotkeyHotkey"]), 10003, this);
            startAutoClickerKeyHandler = new KeyHandler(KeyHandler.ConvertToFKey(settings["StartAutoClickerHotkey"]), 10004, this);
            stopAutoClickerKeyHandler = new KeyHandler(KeyHandler.ConvertToFKey(settings["StopAutoClickerHotkey"]), 10005, this);
            startAutoKeyboardKeyHandler = new KeyHandler(KeyHandler.ConvertToFKey(settings["StartAutoKeyboardHotkey"]), 10006, this);
            stopAutoKeyboardKeyHandler = new KeyHandler(KeyHandler.ConvertToFKey(settings["StopAutoKeyboardHotkey"]), 10007, this);
            startMacroKeyHandler.Register();
            stopMacroKeyHandler.Register();
            newHotkeyKeyHandler.Register();
            startAutoClickerKeyHandler.Register();
            stopAutoClickerKeyHandler.Register();
            startAutoKeyboardKeyHandler.Register();
            stopAutoKeyboardKeyHandler.Register();

            foreach (var macroNameLabel in macroNameLabels) { macroNameLabel.Dispose(); }
            foreach (var macroHotkeyLabel in macroHotkeyLabels) { macroHotkeyLabel.Dispose(); }
            foreach (var macroDelayLabel in macroDelayLabels) { macroDelayLabel.Dispose(); }
            foreach (var macroRepeatLabel in macroRepeatLabels) { macroRepeatLabel.Dispose(); }
            foreach (var macroNameTextbox in macroNameTextboxes) { macroNameTextbox.Dispose(); }
            foreach (var macroHotkeyTextbox in macroHotkeyTextboxes) { macroHotkeyTextbox.Dispose(); }
            foreach (var macroDelayTextbox in macroDelayTextboxes) { macroDelayTextbox.Dispose(); }
            foreach (var macroRepeatTextbox in macroRepeatTextboxes) { macroRepeatTextbox.Dispose(); }
            foreach (var playMacroButton in playMacroButtons) { playMacroButton.Dispose(); }
            foreach (var deleteMacroButton in deleteMacroButtons) { deleteMacroButton.Dispose(); }
            macroNameLabels.Clear();
            macroHotkeyLabels.Clear();
            macroDelayLabels.Clear();
            macroRepeatLabels.Clear();
            macroNameTextboxes.Clear();
            macroHotkeyTextboxes.Clear();
            macroDelayTextboxes.Clear();
            macroRepeatTextboxes.Clear();
            playMacroButtons.Clear();
            deleteMacroButtons.Clear();
            CreateMacroPlaceholders();
            UpdateMacrosUI();
            UpdateHotkeyHelpTexts();
        }

        private void UpdateTimerInterval()
        {
            if (settings.ContainsKey("EngineSpeed"))
            {
                if (settings["EngineSpeed"] == "fast")
                {
                    MacroTimer.Interval = 20;
                    HotkeyTimer.Interval = 20;
                }
                else
                {
                    MacroTimer.Interval = 50;
                    HotkeyTimer.Interval = 50;
                }
            }
            else
            {
                MacroTimer.Interval = 50;
                HotkeyTimer.Interval = 50;
            }
        }
    }
}