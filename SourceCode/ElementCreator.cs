using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace AutoClicker
{
    public partial class AutoClickerForm
    {
        private int elementPositionY;
        private const int elementPositionStartY = 10;
        private const int elementPositionSpacing = 60;


        private void CreateMacroPlaceholders()
        {
            elementPositionY = elementPositionStartY;

            for (int i = 0; i < savedMacros.Count; i++)
            {
                Label macroNameLabel = CreateLabel(SavedMacrosPanel, 30, elementPositionY, 80, 17, Color.White, "Name");
                Label macroHotkeyLabel = CreateLabel(SavedMacrosPanel, 260, elementPositionY, 80, 17, Color.White, "Hotkey");
                Label macroDelayLabel = CreateLabel(SavedMacrosPanel, 370, elementPositionY, 80, 17, Color.White, "Delay (sec)");
                Label macroRepeatLabel = CreateLabel(SavedMacrosPanel, 455, elementPositionY, 80, 17, Color.White, "Repeat");
                TextBox macroNameTextbox = CreateTextbox(SavedMacrosPanel, 30, elementPositionY + 20, 220);
                TextBox macroHotkeyTextbox = CreateTextbox(SavedMacrosPanel, 260, elementPositionY + 20, 100, "macroHotkeyTextbox_Validating", i);
                TextBox macroDelayTextbox = CreateTextbox(SavedMacrosPanel, 370, elementPositionY + 20, 75, "macroDelayTextbox_Validating", i);
                TextBox macroRepeatTextbox = CreateTextbox(SavedMacrosPanel, 455, elementPositionY + 20, 75, "macroRepeatTextbox_Validating", i);
                Button playMacroButton = CreateMacroButton(SavedMacrosPanel, 550, elementPositionY + 16, 80, 30, "Play", Color.White, i);
                Button deleteMacroButton = CreateMacroButton(SavedMacrosPanel, 640, elementPositionY + 16, 80, 30, "Delete", Color.OrangeRed, i);
                macroNameLabels.Add(macroNameLabel);
                macroHotkeyLabels.Add(macroHotkeyLabel);
                macroDelayLabels.Add(macroDelayLabel);
                macroRepeatLabels.Add(macroRepeatLabel);
                macroNameTextboxes.Add(macroNameTextbox);
                macroHotkeyTextboxes.Add(macroHotkeyTextbox);
                macroDelayTextboxes.Add(macroDelayTextbox);
                macroRepeatTextboxes.Add(macroRepeatTextbox);
                playMacroButtons.Add(playMacroButton);
                deleteMacroButtons.Add(deleteMacroButton);

                elementPositionY += elementPositionSpacing;
            }
        }


        private void CreateNewMacroPlaceholder()
        {
            if (macroNameLabels.Count == 0) { elementPositionY = elementPositionStartY; }
            else { elementPositionY = macroNameLabels[macroNameLabels.Count - 1].Location.Y + elementPositionSpacing; }

            Label macroNameLabel = CreateLabel(SavedMacrosPanel, 30, elementPositionY, 80, 17, Color.White, "Name");
            Label macroHotkeyLabel = CreateLabel(SavedMacrosPanel, 260, elementPositionY, 80, 17, Color.White, "Hotkey");
            Label macroDelayLabel = CreateLabel(SavedMacrosPanel, 370, elementPositionY, 80, 17, Color.White, "Delay (sec)");
            Label macroRepeatLabel = CreateLabel(SavedMacrosPanel, 455, elementPositionY, 80, 17, Color.White, "Repeat");
            TextBox macroNameTextbox = CreateTextbox(SavedMacrosPanel, 30, elementPositionY + 20, 220);
            TextBox macroHotkeyTextbox = CreateTextbox(SavedMacrosPanel, 260, elementPositionY + 20, 100, "macroHotkeyTextbox_Validating", savedMacros.Count);
            TextBox macroDelayTextbox = CreateTextbox(SavedMacrosPanel, 370, elementPositionY + 20, 75, "macroDelayTextbox_Validating", savedMacros.Count);
            TextBox macroRepeatTextbox = CreateTextbox(SavedMacrosPanel, 455, elementPositionY + 20, 75, "macroRepeatTextbox_Validating", savedMacros.Count);
            Button playMacroButton = CreateMacroButton(SavedMacrosPanel, 550, elementPositionY + 16, 80, 30, "Play", Color.White, macroNameLabels.Count);
            Button deleteMacroButton = CreateMacroButton(SavedMacrosPanel, 640, elementPositionY + 16, 80, 30, "Delete", Color.OrangeRed, macroNameLabels.Count);
            macroNameLabels.Add(macroNameLabel);
            macroHotkeyLabels.Add(macroHotkeyLabel);
            macroDelayLabels.Add(macroDelayLabel);
            macroRepeatLabels.Add(macroRepeatLabel);
            macroNameTextboxes.Add(macroNameTextbox);
            macroHotkeyTextboxes.Add(macroHotkeyTextbox);
            macroDelayTextboxes.Add(macroDelayTextbox);
            macroRepeatTextboxes.Add(macroRepeatTextbox);
            playMacroButtons.Add(playMacroButton);
            deleteMacroButtons.Add(deleteMacroButton);
        }


        private void DeleteMacroPlaceholder()
        {
            int index = macroNameLabels.Count() - 1;
            macroNameLabels[index].Dispose();
            macroHotkeyLabels[index].Dispose();
            macroDelayLabels[index].Dispose();
            macroRepeatLabels[index].Dispose();
            macroNameTextboxes[index].Dispose();
            macroHotkeyTextboxes[index].Dispose();
            macroDelayTextboxes[index].Dispose();
            macroRepeatTextboxes[index].Dispose();
            playMacroButtons[index].Dispose();
            deleteMacroButtons[index].Dispose();
            macroNameLabels.Remove(macroNameLabels[index]);
            macroHotkeyLabels.Remove(macroHotkeyLabels[index]);
            macroDelayLabels.Remove(macroDelayLabels[index]);
            macroRepeatLabels.Remove(macroRepeatLabels[index]);
            macroNameTextboxes.Remove(macroNameTextboxes[index]);
            macroHotkeyTextboxes.Remove(macroHotkeyTextboxes[index]);
            macroDelayTextboxes.Remove(macroDelayTextboxes[index]);
            macroRepeatTextboxes.Remove(macroRepeatTextboxes[index]);
            playMacroButtons.Remove(playMacroButtons[index]);
            deleteMacroButtons.Remove(deleteMacroButtons[index]);
        }


        private void CreateHotkeyPlaceholders()
        {
            elementPositionY = elementPositionStartY;

            for (int i = 0; i < savedHotkeys.Count; i++)
            {
                Label hotkeyNameLabel = CreateLabel(SavedHotkeysPanel, 30, elementPositionY, 80, 17, Color.White, "Name");
                Label hotkeyInputLabel = CreateLabel(SavedHotkeysPanel, 240, elementPositionY, 80, 17, Color.White, "Hotkey");
                TextBox hotkeyNameTextbox = CreateTextbox(SavedHotkeysPanel, 30, elementPositionY + 20, 200);
                TextBox hotkeyInputTextbox = CreateTextbox(SavedHotkeysPanel, 240, elementPositionY + 20, 100, "hotkeyInputTextbox_Validating", i);
                Button deleteHotkeyButton = CreateHotkeyButton(SavedHotkeysPanel, 370, elementPositionY + 16, 80, 30, "Delete", Color.OrangeRed, i);
                hotkeyNameLabels.Add(hotkeyNameLabel);
                hotkeyInputLabels.Add(hotkeyInputLabel);
                hotkeyNameTextboxes.Add(hotkeyNameTextbox);
                hotkeyInputTextboxes.Add(hotkeyInputTextbox);
                deleteHotkeyButtons.Add(deleteHotkeyButton);

                elementPositionY += elementPositionSpacing;
            }
        }


        private void CreateNewHotkeyPlaceholder()
        {
            if (hotkeyNameLabels.Count == 0) { elementPositionY = elementPositionStartY; }
            else { elementPositionY = hotkeyNameLabels[hotkeyNameLabels.Count - 1].Location.Y + elementPositionSpacing; }

            Label hotkeyNameLabel = CreateLabel(SavedHotkeysPanel, 30, elementPositionY, 80, 17, Color.White, "Name");
            Label hotkeyInputLabel = CreateLabel(SavedHotkeysPanel, 240, elementPositionY, 80, 17, Color.White, "Hotkey");
            TextBox hotkeyNameTextbox = CreateTextbox(SavedHotkeysPanel, 30, elementPositionY + 20, 200);
            TextBox hotkeyInputTextbox = CreateTextbox(SavedHotkeysPanel, 240, elementPositionY + 20, 100, "hotkeyInputTextbox_Validating", hotkeyNameLabels.Count);
            Button deleteHotkeyButton = CreateHotkeyButton(SavedHotkeysPanel, 370, elementPositionY + 16, 80, 30, "Delete", Color.OrangeRed, hotkeyNameLabels.Count);
            hotkeyNameLabels.Add(hotkeyNameLabel);
            hotkeyInputLabels.Add(hotkeyInputLabel);
            hotkeyNameTextboxes.Add(hotkeyNameTextbox);
            hotkeyInputTextboxes.Add(hotkeyInputTextbox);
            deleteHotkeyButtons.Add(deleteHotkeyButton);
        }


        private void DeleteHotkeyPlaceholder()
        {
            int index = hotkeyNameLabels.Count - 1;
            hotkeyNameLabels[index].Dispose();
            hotkeyInputLabels[index].Dispose();
            hotkeyNameTextboxes[index].Dispose();
            hotkeyInputTextboxes[index].Dispose();
            deleteHotkeyButtons[index].Dispose();
            hotkeyNameLabels.Remove(hotkeyNameLabels[index]);
            hotkeyInputLabels.Remove(hotkeyInputLabels[index]);
            hotkeyNameTextboxes.Remove(hotkeyNameTextboxes[index]);
            hotkeyInputTextboxes.Remove(hotkeyInputTextboxes[index]);
            deleteHotkeyButtons.Remove(deleteHotkeyButtons[index]);
        }


        private Label CreateLabel(Panel parent, int x, int y, int width, int height, Color color, string text)
        {
            Label label = new Label();
            parent.Controls.Add(label);
            label.Visible = false;
            label.Left = x;
            label.Top = y;
            label.Width = width;
            label.Height = height;
            label.ForeColor = color;
            label.Text = text;
            return label;
        }


        private TextBox CreateTextbox(Panel parent, int x, int y, int width, string action = "", int index = 0)
        {
            TextBox textbox = new TextBox();
            parent.Controls.Add(textbox);
            textbox.Visible = false;
            textbox.Left = x;
            textbox.Top = y;
            textbox.Width = width;
            textbox.TabStop = false;
            textbox.TabIndex = 0;
            if (action == "macroRepeatTextbox_Validating")
            {
                textbox.Validating += (sender, EventArgs) => { macroRepeatTextbox_Validating(sender, EventArgs, index); };
            }
            else if (action == "macroDelayTextbox_Validating")
            {
                textbox.Validating += (sender, EventArgs) => { macroDelayTextbox_Validating(sender, EventArgs, index); };
            }
            else if (action == "hotkeyInputTextbox_Validating")
            {
                textbox.Validating += (sender, EventArgs) => { hotkeyInputTextbox_Validating(sender, EventArgs, index); };
            }
            else if (action == "macroHotkeyTextbox_Validating")
            {
                textbox.Validating += (sender, EventArgs) => { macroHotkeyTextbox_Validating(sender, EventArgs, index); };
            }
            return textbox;
        }


        private void macroRepeatTextbox_Validating(object sender, EventArgs e, int index)
        {
            if (!macroRepeatTextboxes[index].Text.All(char.IsDigit))
            {
                macroRepeatTextboxes[index].Text = "1";
            }
        }


        private void macroDelayTextbox_Validating(object sender, EventArgs e, int index)
        {
            if (!macroDelayTextboxes[index].Text.All(char.IsDigit))
            {
                macroDelayTextboxes[index].Text = "0";
            }
        }


        private void hotkeyInputTextbox_Validating(object sender, EventArgs e, int index)
        {
            if (hotkeyInputTextboxes[index].Text.ToLower() == "none") { return; }

            if (KeyHandler.ConvertToKey(hotkeyInputTextboxes[index].Text) == Keys.None)
            {
                hotkeyInputTextboxes[index].Text = "Invalid";
            }
            else
            {
                for (int i = 0; i < savedHotkeys.Count(); i++)
                {
                    if (hotkeyInputTextboxes[index].Text == hotkeyInputTextboxes[i].Text && index != i)
                    {
                        hotkeyInputTextboxes[index].Text = "Already in use";
                    }
                }

                for (int i = 0; i < savedMacros.Count(); i++)
                {
                    if (hotkeyInputTextboxes[index].Text == macroHotkeyTextboxes[i].Text)
                    {
                        hotkeyInputTextboxes[index].Text = "Already in use";
                    }
                }
            }
        }


        private void macroHotkeyTextbox_Validating(object sender, EventArgs e, int index)
        {
            if (macroHotkeyTextboxes[index].Text.ToLower() == "none") { return; }

            if (KeyHandler.ConvertToKey(macroHotkeyTextboxes[index].Text) == Keys.None)
            {
                macroHotkeyTextboxes[index].Text = "Invalid";
            }
            else
            {
                for (int i = 0; i < savedHotkeys.Count(); i++)
                {
                    if (macroHotkeyTextboxes[index].Text == hotkeyInputTextboxes[i].Text)
                    {
                        macroHotkeyTextboxes[index].Text = "Already in use";
                    }
                }

                for (int i = 0; i < savedMacros.Count(); i++)
                {
                    if (macroHotkeyTextboxes[index].Text == macroHotkeyTextboxes[i].Text && index != i)
                    {
                        macroHotkeyTextboxes[index].Text = "Already in use";
                    }
                }
            }
        }


        private Button CreateMacroButton(Panel parent, int x, int y, int width, int height, string text, Color color, int index)
        {
            Button button = new Button();
            parent.Controls.Add(button);
            button.Visible = false;
            button.Left = x;
            button.Top = y;
            button.Width = width;
            button.Height = height;
            button.FlatStyle = FlatStyle.Flat;
            button.ForeColor = color;
            button.TabStop = false;
            button.TabIndex = 0;
            button.Text = text;
            button.Click += (sender, EventArgs) => { MacroButton_Click(sender, EventArgs, index); };
            return button;
        }


        private void MacroButton_Click(object sender, EventArgs e, int index)
        {
            Button button = (sender as Button);
            if (button.Text == "Play") { PlayMacro(index); }
            else if (button.Text == "Delete") { DeleteMacro(index); }
        }


        private Button CreateHotkeyButton(Panel parent, int x, int y, int width, int height, string text, Color color, int index)
        {
            Button button = new Button();
            parent.Controls.Add(button);
            button.Visible = false;
            button.Left = x;
            button.Top = y;
            button.Width = width;
            button.Height = height;
            button.FlatStyle = FlatStyle.Flat;
            button.ForeColor = color;
            button.TabStop = false;
            button.TabIndex = 0;
            button.Text = text;
            button.Click += (sender, EventArgs) => { HotkeyButton_Click(sender, EventArgs, index); };
            return button;
        }


        private void HotkeyButton_Click(object sender, EventArgs e, int index)
        {
            Button button = (sender as Button);
            if (button.Text == "Delete") { DeleteHotkey(index); }
        }


        private void DeleteHotkey(int index)
        {
            EnableHotkeysUIButtons(false);
            ToggleHotkeysButton.Enabled = false;
            ConfirmDeleteLabel.Text = "Are you sure you want to delete hotkey: \"" + savedHotkeys[index].Name + "\"";
            DeletePanelParent.Visible = true;
            deletedObject = "hotkey";
            deletedIndex = index;
        }


        private void DeleteMacro(int index)
        {
            EnableMacroUIButtons(false);
            NewMacroButton.Enabled = false;
            ConfirmDeleteLabel.Text = "Are you sure you want to delete macro: \"" + savedMacros[index].Name + "\"";
            DeletePanelParent.Visible = true;
            deletedObject = "macro";
            deletedIndex = index;
        }


        private void ConfirmDeleteButton_Click(object sender, EventArgs e)
        {
            if (deletedObject == "hotkey")
            {
                EnableHotkeysUIButtons(true);
                ToggleHotkeysButton.Enabled = true;
                savedHotkeys.Remove(savedHotkeys[deletedIndex]);
                UpdateHotkeysUI();
                DeleteHotkeyPlaceholder();
                SaveHotkeys();
            }
            else
            {
                EnableMacroUIButtons(true);
                NewMacroButton.Enabled = true;
                savedMacros.Remove(savedMacros[deletedIndex]);
                UpdateMacrosUI();
                DeleteMacroPlaceholder();
                SaveMacros();
            }

            DeletePanelParent.Visible = false;
        }


        private void CancelConfirmDeleteButton_Click(object sender, EventArgs e)
        {
            if (deletedObject == "hotkey")
            {
                ToggleHotkeysButton.Enabled = true;
                EnableHotkeysUIButtons(true);
            }
            else
            {
                NewMacroButton.Enabled = true;
                EnableMacroUIButtons(true);
            }

            DeletePanelParent.Visible = false;
        }


        private void PlayMacro(int index)
        {
            if (index >= macroRepeatTextboxes.Count()) { return; }

            macroRepeatCounter = int.Parse(macroRepeatTextboxes[index].Text);

            if (macroRepeatCounter <= 0)
            {
                return;
            }

            if (savedMacros[index].Instructions.Length == 0)
            {
                return;
            }

            var instructions = savedMacros[index].Instructions.Remove(savedMacros[index].Instructions.Length - 1).Split(',');

            foreach (var instruction in instructions)
            {
                var str = instruction.Split('-');

                if (!macroInstructions.ContainsKey(int.Parse(str[0])))
                {
                    macroInstructions.Add(int.Parse(str[0]), new string[] { str[1], str[2], str[3] });
                }
            }

            EnableMacroUIButtons(false);
            isPlayingMacro = true;
            macroDuration = savedMacros[index].Duration + (int.Parse(macroDelayTextboxes[index].Text) * (1000 / MacroTimer.Interval));
            MacroStatusLabel.Visible = false;
            MacroStatusLabel.Text = "Playing '" + savedMacros[index].Name + "'";
            MacroStatusLabel.Visible = true;
            MacroStatusLabel.ForeColor = Color.Green;
            StartMacroTimer();

            if (bool.Parse(settings["SameHotkeyForMacros"]))
            {
                if (settings["StartMacroHotkey"].ToLower() == "already in use" || settings["StartMacroHotkey"].ToLower() == "none")
                {
                    NewMacroButton.Text = "Stop Macro";
                }
                else
                {
                    NewMacroButton.Text = "Stop Macro\n(Hotkey: " + settings["StartMacroHotkey"] + ")";
                }
            }
            else
            {
                if (settings["StopMacroHotkey"].ToLower() == "already in use" || settings["StopMacroHotkey"].ToLower() == "none")
                {
                    NewMacroButton.Text = "Stop Macro";
                }
                else
                {
                    NewMacroButton.Text = "Stop Macro\n(Hotkey: " + settings["StopMacroHotkey"] + ")";
                }
            }
        }


        private void UpdateMacrosUI()
        {
            ClearMacrosUI();
            int index = 0;

            foreach (Macro savedMacro in savedMacros)
            {
                macroNameTextboxes[index].Text = savedMacro.Name;
                macroHotkeyTextboxes[index].Text = savedMacro.Hotkey;
                macroDelayTextboxes[index].Text = savedMacro.Delay.ToString();
                macroRepeatTextboxes[index].Text = savedMacro.RepeatFor.ToString();
                macroNameLabels[index].Visible = true;
                macroHotkeyLabels[index].Visible = true;
                macroDelayLabels[index].Visible = true;
                macroRepeatLabels[index].Visible = true;
                macroNameTextboxes[index].Visible = true;
                macroHotkeyTextboxes[index].Visible = true;
                macroDelayTextboxes[index].Visible = true;
                macroRepeatTextboxes[index].Visible = true;
                playMacroButtons[index].Visible = true;
                deleteMacroButtons[index].Visible = true;

                index++;
            }
        }


        private void ClearMacrosUI()
        {
            for (int i = 0; i < macroNameLabels.Count(); i++)
            {
                macroNameLabels[i].Visible = false;
                macroHotkeyLabels[i].Visible = false;
                macroDelayLabels[i].Visible = false;
                macroRepeatLabels[i].Visible = false;
                macroNameTextboxes[i].Visible = false;
                macroHotkeyTextboxes[i].Visible = false;
                macroDelayTextboxes[i].Visible = false;
                macroRepeatTextboxes[i].Visible = false;
                playMacroButtons[i].Visible = false;
                deleteMacroButtons[i].Visible = false;
            }
        }


        private void UpdateHotkeysUI()
        {
            ClearHotkeysUI();
            int index = 0;

            foreach (Hotkey savedHotkey in savedHotkeys)
            {
                hotkeyNameTextboxes[index].Text = savedHotkey.Name;
                hotkeyInputTextboxes[index].Text = savedHotkey.Input;
                hotkeyNameLabels[index].Visible = true;
                hotkeyInputLabels[index].Visible = true;
                hotkeyNameTextboxes[index].Visible = true;
                hotkeyInputTextboxes[index].Visible = true;
                deleteHotkeyButtons[index].Visible = true;

                index++;
            }
        }


        private void ClearHotkeysUI()
        {
            for (int i = 0; i < hotkeyNameLabels.Count(); i++)
            {
                hotkeyNameLabels[i].Visible = false;
                hotkeyInputLabels[i].Visible = false;
                hotkeyNameTextboxes[i].Visible = false;
                hotkeyInputTextboxes[i].Visible = false;
                deleteHotkeyButtons[i].Visible = false;
            }
        }
    }
}