using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace AutoClicker
{
    public partial class AutoClickerForm
    {
        private void NewMacroButton_MouseDown(object sender, MouseEventArgs e)
        {
            if (isPlayingMacro) { stopMacroReplay(); }
            else
            {
                isRecordingMacro = !isRecordingMacro;
                if (isRecordingMacro) { startMacroRecording(); }
                else { stopMacroRecording(); }
            }
        }

        private void startMacroRecording()
        {
            isRecordingMacro = true;
            EnableMacroUIButtons(false);
            placeholderNewRecordedMacroInstructions = "";
            newRecordedMacroInstructions = "";
            MacroStatusLabel.Visible = false;
            MacroStatusLabel.Text = "Recoding new Macro";
            MacroStatusLabel.Visible = true;
            MacroStatusLabel.ForeColor = Color.Green;
            StartMacroTimer();
            ActivateMouseHook();

            if (bool.Parse(settings["SameHotkeyForMacros"]))
            {
                if (settings["StartMacroHotkey"].ToLower() == "already in use" || settings["StartMacroHotkey"].ToLower() == "none")
                {
                    NewMacroButton.Text = "Stop Recording";
                }
                else
                {
                    NewMacroButton.Text = "Stop Recording\n(Hotkey: " + settings["StartMacroHotkey"] + ")";
                }
            }
            else
            {
                if (settings["StopMacroHotkey"].ToLower() == "already in use" || settings["StopMacroHotkey"].ToLower() == "none")
                {
                    NewMacroButton.Text = "Stop Recording";
                }
                else
                {
                    NewMacroButton.Text = "Stop Recording\n(Hotkey: " + settings["StopMacroHotkey"] + ")";
                }
            }
        }

        private void stopMacroRecording()
        {
            isRecordingMacro = false;
            DeactivateMouseHook();
            StopMacroTimer();
            EnableMacroUIButtons(true);
            MacroStatusLabel.Visible = false;
            MacroStatusLabel.Text = "OFF";
            MacroStatusLabel.Visible = true;
            MacroStatusLabel.ForeColor = Color.Red;

            if (settings["StartMacroHotkey"].ToLower() == "already in use" || settings["StartMacroHotkey"].ToLower() == "none")
            {
                NewMacroButton.Text = "Record New Macro";
            }
            else
            {
                NewMacroButton.Text = "Record New Macro\n(Hotkey: " + settings["StartMacroHotkey"] + ")";
            }

            if (newRecordedMacroInstructions != "")
            {
                CreateNewMacroPlaceholder();
                savedMacros.Add(new Macro("New macro", newRecordedMacroInstructions, "none", 0, 99999, timerStep));
                SaveMacros();
                UpdateMacrosUI();
                if (hotkeysEnabled) { macroHotkeyTextboxes[savedMacros.Count - 1].Enabled = false; }
            }
        }

        private void stopMacroReplay()
        {
            StopMacroTimer();
            EnableMacroUIButtons(true);
            isPlayingMacro = false;
            macroInstructions.Clear();
            MacroStatusLabel.Visible = false;
            MacroStatusLabel.Text = "OFF";
            MacroStatusLabel.Visible = true;
            MacroStatusLabel.ForeColor = Color.Red;

            if (settings["StartMacroHotkey"].ToLower() == "already in use" || settings["StartMacroHotkey"].ToLower() == "none")
            {
                NewMacroButton.Text = "Record New Macro";
            }
            else
            {
                NewMacroButton.Text = "Record New Macro\n(Hotkey: " + settings["StartMacroHotkey"] + ")";
            }
        }

        private void SaveMacrosButton_Click(object sender, EventArgs e)
        {
            if (savedMacros.Count == 0) { return; }

            int index = 0;
            List<Macro> _savedMacros = new List<Macro>();

            foreach (Macro savedMacro in savedMacros)
            {
                _savedMacros.Add(new Macro(macroNameTextboxes[index].Text, savedMacro.Instructions, macroHotkeyTextboxes[index].Text, int.Parse(macroDelayTextboxes[index].Text), int.Parse(macroRepeatTextboxes[index].Text), savedMacro.Duration));
                index++;
            }

            savedMacros.Clear();
            savedMacros = _savedMacros;
            SaveMacros();
            UpdateMacrosUI();
        }

        private void EnableMacroUIButtons(bool enable)
        {
            if (enable == false)
            {
                SettingsPanel.Visible = false;
                HotkeysPanel.Visible = false;
                AutoClickerPanel.Visible = false;
                AutoKeyboardPanel.Visible = false;
                MacrosPanel.Visible = true;

                foreach (var macroHotkeyTextbox in macroHotkeyTextboxes)
                {
                    macroHotkeyTextbox.Enabled = false;
                }
            }
            else
            {
                if (!hotkeysEnabled)
                {
                    foreach (var macroHotkeyTextbox in macroHotkeyTextboxes)
                    {
                        macroHotkeyTextbox.Enabled = true;
                    }
                }
            }

            MacroToHotkeysButton.Enabled = enable;
            MacroToAutoClickerButton.Enabled = enable;
            MacroToSettingsButton.Enabled = enable;
            MacroToAutoKeyboardButton.Enabled = enable;
            SaveMacrosButton.Enabled = enable;

            foreach (var playMacroButton in playMacroButtons)
            {
                playMacroButton.Enabled = enable;
            }
            foreach (var deleteMacroButton in deleteMacroButtons)
            {
                deleteMacroButton.Enabled = enable;
            }
            foreach (var macroNameTextbox in macroNameTextboxes)
            {
                macroNameTextbox.Enabled = enable;
            }
            foreach (var macroDelayTextbox in macroDelayTextboxes)
            {
                macroDelayTextbox.Enabled = enable;
            }
            foreach (var macroRepeatTextbox in macroRepeatTextboxes)
            {
                macroRepeatTextbox.Enabled = enable;
            }
        }

        private void StartMacroTimer()
        {
            timerStep = 0;
            MacroTimer.Start();
        }

        private void StopMacroTimer()
        {
            MacroTimer.Stop();
        }

        private void MacroTimer_Tick(object sender, EventArgs e)
        {
            if (isPlayingMacro)
            {
                if (timerStep >= macroDuration)
                {
                    macroRepeatCounter--;
                    timerStep = 0;

                    if (macroRepeatCounter <= 0)
                    {
                        stopMacroReplay();
                        return;
                    }
                }

                if (macroInstructions.ContainsKey(timerStep + 1))
                {
                    if (!macroInstructions.ContainsKey(timerStep) && !macroInstructions.ContainsKey(timerStep - 1))
                    {
                        mouseLastPosition = new int[] { Cursor.Position.X, Cursor.Position.Y };
                    }
                }

                if (macroInstructions.ContainsKey(timerStep))
                {
                    if (macroInstructions.ContainsKey(timerStep - 1))
                    {
                        MoveMouseToPosition(int.Parse(macroInstructions[timerStep][1]), int.Parse(macroInstructions[timerStep][2]));
                    }

                    if (macroInstructions[timerStep][0] == "Right")
                    {
                        RightMouseClick(int.Parse(macroInstructions[timerStep][1]), int.Parse(macroInstructions[timerStep][2]));
                    }
                    else
                    {
                        LeftMouseClick(int.Parse(macroInstructions[timerStep][1]), int.Parse(macroInstructions[timerStep][2]));
                    }
                }

                if (macroInstructions.ContainsKey(timerStep + 1))
                {
                    MoveMouseToPosition(int.Parse(macroInstructions[timerStep + 1][1]), int.Parse(macroInstructions[timerStep + 1][2]));
                }

                if (macroInstructions.ContainsKey(timerStep - 1))
                {
                    if (!macroInstructions.ContainsKey(timerStep) && !macroInstructions.ContainsKey(timerStep + 1))
                    {
                        MoveMouseToPosition(mouseLastPosition[0], mouseLastPosition[1]);
                    }
                }
            }

            timerStep++;
        }
    }
}
