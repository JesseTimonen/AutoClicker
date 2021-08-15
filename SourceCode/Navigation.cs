using System;

namespace AutoClicker
{
    public partial class AutoClickerForm
    {
        // Macros Window
        private void MacroToHotkeysButton_Click(object sender, EventArgs e)
        {
            MacrosPanel.Visible = false;
            HotkeysPanel.Visible = true;
            MacrosSavedFeedbackLabel.Visible = false;
        }

        private void MacroToAutoClickerButton_Click(object sender, EventArgs e)
        {
            MacrosPanel.Visible = false;
            AutoClickerPanel.Visible = true;
            MacrosSavedFeedbackLabel.Visible = false;
        }

        private void MacroToKeyboardButton_Click(object sender, EventArgs e)
        {
            MacrosPanel.Visible = false;
            AutoKeyboardPanel.Visible = true;
            MacrosSavedFeedbackLabel.Visible = false;
        }

        private void MacroToSettingsButton_Click(object sender, EventArgs e)
        {
            MacrosPanel.Visible = false;
            SettingsPanel.Visible = true;
            MacrosSavedFeedbackLabel.Visible = false;

            SettingsClickWhileMovingCheckbox.Checked = bool.Parse(settings["ClickWhileMoving"]);
            SettingsDelayAutoClickerCheckbox.Checked = bool.Parse(settings["DelayAutoClicker"]);
            SettingsSameMacroHotkeyCheckbox.Checked = bool.Parse(settings["SameHotkeyForMacros"]);
            SettingsSameAutoClickerHotkeyCheckbox.Checked = bool.Parse(settings["SameHotkeyForAutoClicker"]);
            SettingsSameAutoKeyboardHotkeyCheckbox.Checked = bool.Parse(settings["SameHotkeyForAutoKeyboard"]);
            SettingsStartMacroTextbox.Text = settings["StartMacroHotkey"];
            SettingsStopMacroTextbox.Text = settings["StopMacroHotkey"];
            SettingsCreateHotkeyTextbox.Text = settings["CreateHotkeyHotkey"];
            SettingsStartAutoClickerTextbox.Text = settings["StartAutoClickerHotkey"];
            SettingsStopAutoClickerTextbox.Text = settings["StopAutoClickerHotkey"];
            SettingsStartAutoKeyboardTextbox.Text = settings["StartAutoKeyboardHotkey"];
            SettingsStopAutoKeyboardTextbox.Text = settings["StopAutoKeyboardHotkey"];

            if (settings["EngineSpeed"] == "fast")
            {
                SettingsSafeModeRadioButton.Checked = false;
                SettingsFastModeRadioButton.Checked = true;
            }
            else
            {
                SettingsFastModeRadioButton.Checked = false;
                SettingsSafeModeRadioButton.Checked = true;
            }
        }

        // Hotkeys Window
        private void HotkeysToMacroButton_Click(object sender, EventArgs e)
        {
            HotkeysPanel.Visible = false;
            MacrosPanel.Visible = true;
            HotkeysSavedFeedbackLabel.Visible = false;
        }

        private void HotkeyToAutoClickerButton_Click(object sender, EventArgs e)
        {
            HotkeysPanel.Visible = false;
            AutoClickerPanel.Visible = true;
            HotkeysSavedFeedbackLabel.Visible = false;
        }

        private void HotkeyToKeyboardButton_Click(object sender, EventArgs e)
        {
            HotkeysPanel.Visible = false;
            AutoKeyboardPanel.Visible = true;
            HotkeysSavedFeedbackLabel.Visible = false;
        }

        private void HotkeysToSettingsButton_Click(object sender, EventArgs e)
        {
            HotkeysPanel.Visible = false;
            SettingsPanel.Visible = true;
            HotkeysSavedFeedbackLabel.Visible = false;

            SettingsClickWhileMovingCheckbox.Checked = bool.Parse(settings["ClickWhileMoving"]);
            SettingsDelayAutoClickerCheckbox.Checked = bool.Parse(settings["DelayAutoClicker"]);
            SettingsSameMacroHotkeyCheckbox.Checked = bool.Parse(settings["SameHotkeyForMacros"]);
            SettingsSameAutoClickerHotkeyCheckbox.Checked = bool.Parse(settings["SameHotkeyForAutoClicker"]);
            SettingsSameAutoKeyboardHotkeyCheckbox.Checked = bool.Parse(settings["SameHotkeyForAutoKeyboard"]);
            SettingsStartMacroTextbox.Text = settings["StartMacroHotkey"];
            SettingsStopMacroTextbox.Text = settings["StopMacroHotkey"];
            SettingsCreateHotkeyTextbox.Text = settings["CreateHotkeyHotkey"];
            SettingsStartAutoClickerTextbox.Text = settings["StartAutoClickerHotkey"];
            SettingsStopAutoClickerTextbox.Text = settings["StopAutoClickerHotkey"];
            SettingsStartAutoKeyboardTextbox.Text = settings["StartAutoKeyboardHotkey"];
            SettingsStopAutoKeyboardTextbox.Text = settings["StopAutoKeyboardHotkey"];

            if (settings["EngineSpeed"] == "fast")
            {
                SettingsSafeModeRadioButton.Checked = false;
                SettingsFastModeRadioButton.Checked = true;
            }
            else
            {
                SettingsFastModeRadioButton.Checked = false;
                SettingsSafeModeRadioButton.Checked = true;
            }
        }

        // Auto Clicker Window
        private void AutoClickerToMacroButton_Click(object sender, EventArgs e)
        {
            AutoClickerPanel.Visible = false;
            MacrosPanel.Visible = true;
        }

        private void AutoClickerToHotkeysButton_Click(object sender, EventArgs e)
        {
            AutoClickerPanel.Visible = false;
            HotkeysPanel.Visible = true;
        }

        private void AutoClickerToKeyboardButton_Click(object sender, EventArgs e)
        {
            AutoClickerPanel.Visible = false;
            AutoKeyboardPanel.Visible = true;
        }

        private void AutoClickerToSettingsButton_Click(object sender, EventArgs e)
        {
            AutoClickerPanel.Visible = false;
            SettingsPanel.Visible = true;

            SettingsClickWhileMovingCheckbox.Checked = bool.Parse(settings["ClickWhileMoving"]);
            SettingsDelayAutoClickerCheckbox.Checked = bool.Parse(settings["DelayAutoClicker"]);
            SettingsSameMacroHotkeyCheckbox.Checked = bool.Parse(settings["SameHotkeyForMacros"]);
            SettingsSameAutoClickerHotkeyCheckbox.Checked = bool.Parse(settings["SameHotkeyForAutoClicker"]);
            SettingsSameAutoKeyboardHotkeyCheckbox.Checked = bool.Parse(settings["SameHotkeyForAutoKeyboard"]);
            SettingsStartMacroTextbox.Text = settings["StartMacroHotkey"];
            SettingsStopMacroTextbox.Text = settings["StopMacroHotkey"];
            SettingsCreateHotkeyTextbox.Text = settings["CreateHotkeyHotkey"];
            SettingsStartAutoClickerTextbox.Text = settings["StartAutoClickerHotkey"];
            SettingsStopAutoClickerTextbox.Text = settings["StopAutoClickerHotkey"];
            SettingsStartAutoKeyboardTextbox.Text = settings["StartAutoKeyboardHotkey"];
            SettingsStopAutoKeyboardTextbox.Text = settings["StopAutoKeyboardHotkey"];

            if (settings["EngineSpeed"] == "fast")
            {
                SettingsSafeModeRadioButton.Checked = false;
                SettingsFastModeRadioButton.Checked = true;
            }
            else
            {
                SettingsFastModeRadioButton.Checked = false;
                SettingsSafeModeRadioButton.Checked = true;
            }
        }

        // Keyboard Window
        private void KeyboardToMacroButton_Click(object sender, EventArgs e)
        {
            AutoKeyboardPanel.Visible = false;
            MacrosPanel.Visible = true;
        }

        private void KeyboardToHotkeysButton_Click(object sender, EventArgs e)
        {
            AutoKeyboardPanel.Visible = false;
            HotkeysPanel.Visible = true;
        }

        private void KeyboardToAutoClickerButton_Click(object sender, EventArgs e)
        {
            AutoKeyboardPanel.Visible = false;
            AutoClickerPanel.Visible = true;
        }

        private void KeyboardToSettingsButton_Click(object sender, EventArgs e)
        {
            AutoKeyboardPanel.Visible = false;
            SettingsPanel.Visible = true;

            SettingsClickWhileMovingCheckbox.Checked = bool.Parse(settings["ClickWhileMoving"]);
            SettingsDelayAutoClickerCheckbox.Checked = bool.Parse(settings["DelayAutoClicker"]);
            SettingsSameMacroHotkeyCheckbox.Checked = bool.Parse(settings["SameHotkeyForMacros"]);
            SettingsSameAutoClickerHotkeyCheckbox.Checked = bool.Parse(settings["SameHotkeyForAutoClicker"]);
            SettingsSameAutoKeyboardHotkeyCheckbox.Checked = bool.Parse(settings["SameHotkeyForAutoKeyboard"]);
            SettingsStartMacroTextbox.Text = settings["StartMacroHotkey"];
            SettingsStopMacroTextbox.Text = settings["StopMacroHotkey"];
            SettingsCreateHotkeyTextbox.Text = settings["CreateHotkeyHotkey"];
            SettingsStartAutoClickerTextbox.Text = settings["StartAutoClickerHotkey"];
            SettingsStopAutoClickerTextbox.Text = settings["StopAutoClickerHotkey"];
            SettingsStartAutoKeyboardTextbox.Text = settings["StartAutoKeyboardHotkey"];
            SettingsStopAutoKeyboardTextbox.Text = settings["StopAutoKeyboardHotkey"];

            if (settings["EngineSpeed"] == "fast")
            {
                SettingsSafeModeRadioButton.Checked = false;
                SettingsFastModeRadioButton.Checked = true;
            }
            else
            {
                SettingsFastModeRadioButton.Checked = false;
                SettingsSafeModeRadioButton.Checked = true;
            }
        }

        // Settings Window
        private void SettingsToMacroButton_Click(object sender, EventArgs e)
        {
            SettingsPanel.Visible = false;
            MacrosPanel.Visible = true;
            SettingsSavedFeedbackLabel.Visible = false;
        }

        private void SettingsToHotkeysButton_Click(object sender, EventArgs e)
        {
            SettingsPanel.Visible = false;
            HotkeysPanel.Visible = true;
            SettingsSavedFeedbackLabel.Visible = false;
        }

        private void SettingsToAutoClickerButton_Click(object sender, EventArgs e)
        {
            SettingsPanel.Visible = false;
            AutoClickerPanel.Visible = true;
            SettingsSavedFeedbackLabel.Visible = false;
        }

        private void SettingsToKeyboardButton_Click(object sender, EventArgs e)
        {
            SettingsPanel.Visible = false;
            AutoKeyboardPanel.Visible = true;
            SettingsSavedFeedbackLabel.Visible = false;
        }
    }
}