using System;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;

namespace AutoClicker
{
    public partial class AutoClickerForm
    {
        private string saveFilePath;


        private void InitSave()
        {
            saveFilePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\AutoClicker\\";

            if (!Directory.Exists(@"" + saveFilePath))
            {
                Directory.CreateDirectory(@"" + saveFilePath);
            }
        }


        private void LoadData()
        {
            // Load settings
            settings.Clear();
            if (File.Exists(@"" + saveFilePath + "Settings.txt"))
            {
                string[] lines = File.ReadAllLines(@"" + saveFilePath + "Settings.txt");

                for (int i = 0; i < lines.Length; i++)
                {
                    var str = lines[i].Split('=');
                    settings.Add(str[0], str[1]);
                }

                // Make sure settings are intact
                if (!settings.ContainsKey("EngineSpeed")) { settings.Add("EngineSpeed", "safe"); }
                if (!settings.ContainsKey("StartAutoClickerHotkey")) { settings.Add("StartAutoClickerHotkey", "F1"); }
                if (!settings.ContainsKey("StopAutoClickerHotkey")) { settings.Add("StopAutoClickerHotkey", "F2"); }
                if (!settings.ContainsKey("StartMacroHotkey")) { settings.Add("StartMacroHotkey", "F3"); }
                if (!settings.ContainsKey("StopMacroHotkey")) { settings.Add("StopMacroHotkey", "F4"); }
                if (!settings.ContainsKey("StartAutoKeyboardHotkey")) { settings.Add("StartAutoKeyboardHotkey", "F5"); }
                if (!settings.ContainsKey("StopAutoKeyboardHotkey")) { settings.Add("StopAutoKeyboardHotkey", "F6"); }
                if (!settings.ContainsKey("CreateHotkeyHotkey")) { settings.Add("CreateHotkeyHotkey", "F7"); }

                if (KeyHandler.ConvertToFKey(settings["StartMacroHotkey"]) == Keys.None) { settings["StartMacroHotkey"] = "None"; }
                if (KeyHandler.ConvertToFKey(settings["StopMacroHotkey"]) == Keys.None) { settings["StopMacroHotkey"] = "None"; }
                if (KeyHandler.ConvertToFKey(settings["CreateHotkeyHotkey"]) == Keys.None) { settings["CreateHotkeyHotkey"] = "None"; }
                if (KeyHandler.ConvertToFKey(settings["StartAutoClickerHotkey"]) == Keys.None) { settings["StartAutoClickerHotkey"] = "None"; }
                if (KeyHandler.ConvertToFKey(settings["StopAutoClickerHotkey"]) == Keys.None) { settings["StopAutoClickerHotkey"] = "None"; }
                if (KeyHandler.ConvertToFKey(settings["StartAutoKeyboardHotkey"]) == Keys.None) { settings["StartAutoKeyboardHotkey"] = "None"; }
                if (KeyHandler.ConvertToFKey(settings["StopAutoKeyboardHotkey"]) == Keys.None) { settings["StopAutoKeyboardHotkey"] = "None"; }

                if (!settings.ContainsKey("SameHotkeyForMacros")) { settings.Add("SameHotkeyForMacros", "false"); }
                else if (settings["SameHotkeyForMacros"].ToLower() != "false" && settings["SameHotkeyForMacros"].ToLower() != "true")
                {
                    settings.Remove("SameHotkeyForMacros");
                    settings.Add("SameHotkeyForMacros", "false");
                }

                if (!settings.ContainsKey("SameHotkeyForAutoClicker")) { settings.Add("SameHotkeyForAutoClicker", "false"); }
                else if (settings["SameHotkeyForAutoClicker"].ToLower() != "false" && settings["SameHotkeyForAutoClicker"].ToLower() != "true")
                {
                    settings.Remove("SameHotkeyForAutoClicker");
                    settings.Add("SameHotkeyForAutoClicker", "false");
                }

                if (!settings.ContainsKey("SameHotkeyForAutoKeyboard")) { settings.Add("SameHotkeyForAutoKeyboard", "false"); }
                else if (settings["SameHotkeyForAutoKeyboard"].ToLower() != "false" && settings["SameHotkeyForAutoKeyboard"].ToLower() != "true")
                {
                    settings.Remove("SameHotkeyForAutoKeyboard");
                    settings.Add("SameHotkeyForAutoKeyboard", "false");
                }

                if (!settings.ContainsKey("ClickWhileMoving")) { settings.Add("ClickWhileMoving", "false"); }
                else if (settings["ClickWhileMoving"].ToLower() != "false" && settings["ClickWhileMoving"].ToLower() != "true")
                {
                    settings.Remove("ClickWhileMoving");
                    settings.Add("ClickWhileMoving", "false");
                }

                if (!settings.ContainsKey("DelayAutoClicker")) { settings.Add("DelayAutoClicker", "true"); }
                else if (settings["DelayAutoClicker"].ToLower() != "false" && settings["DelayAutoClicker"].ToLower() != "true")
                {
                    settings.Remove("DelayAutoClicker");
                    settings.Add("DelayAutoClicker", "true");
                }
            }
            else
            {
                // Default settings
                settings.Add("EngineSpeed", "safe");
                settings.Add("ClickWhileMoving", "false");
                settings.Add("DelayAutoClicker", "true");
                settings.Add("StartAutoClickerHotkey", "F1");
                settings.Add("StopAutoClickerHotkey", "F2");
                settings.Add("StartMacroHotkey", "F3");
                settings.Add("StopMacroHotkey", "F4");
                settings.Add("StartAutoKeyboardHotkey", "F5");
                settings.Add("StopAutoKeyboardHotkey", "F6");
                settings.Add("CreateHotkeyHotkey", "F7");
                settings.Add("SameHotkeyForMacros", "false");
                settings.Add("SameHotkeyForAutoClicker", "false");
                settings.Add("SameHotkeyForAutoKeyboard", "false");
                SaveSettings();
            }

            // Load macros
            savedMacros.Clear();
            if (File.Exists(@"" + saveFilePath + "Saved_" + settings["EngineSpeed"] + "_Macros.txt"))
            {
                string[] lines = File.ReadAllLines(@"" + saveFilePath + "Saved_" + settings["EngineSpeed"] + "_Macros.txt");

                for (int i = 0; i < lines.Length; i++)
                {
                    if ((i + 1) % 6 == 0)
                    {
                        savedMacros.Add(new Macro(lines[i - 5], lines[i - 4], lines[i - 3], int.Parse(lines[i - 2]), int.Parse(lines[i - 1]), int.Parse(lines[i])));
                    }
                }
            }

            // Load hotkeys
            savedHotkeys.Clear();
            if (File.Exists(@"" + saveFilePath + "Saved_Hotkeys.txt"))
            {
                string[] lines = File.ReadAllLines(@"" + saveFilePath + "Saved_Hotkeys.txt");

                for (int i = 0; i < lines.Length; i++)
                {
                    if ((i + 1) % 4 == 0)
                    {
                        savedHotkeys.Add(new Hotkey(lines[i - 3], lines[i - 2], int.Parse(lines[i - 1]), int.Parse(lines[i])));
                    }
                }
            }
        }


        private void SaveHotkeys()
        {
            using (FileStream fs = new FileStream(@"" + saveFilePath + "Saved_Hotkeys.txt", FileMode.Create, FileAccess.Write))
            using (StreamWriter sw = new StreamWriter(fs))
            {
                foreach (Hotkey savedHotkey in savedHotkeys)
                {
                    sw.WriteLine(savedHotkey.Name);
                    sw.WriteLine(savedHotkey.Input);
                    sw.WriteLine(savedHotkey.X);
                    sw.WriteLine(savedHotkey.Y);
                }
            }

            HotkeysSavedFeedbackLabel.Visible = true;
        }


        private void SaveMacros()
        {
            using (FileStream fs = new FileStream(@"" + saveFilePath + "Saved_" + settings["EngineSpeed"] + "_Macros.txt", FileMode.Create, FileAccess.Write))
            using (StreamWriter sw = new StreamWriter(fs))
            {
                foreach (Macro savedMacro in savedMacros)
                {
                    sw.WriteLine(savedMacro.Name);
                    sw.WriteLine(savedMacro.Instructions);
                    sw.WriteLine(savedMacro.Hotkey);
                    sw.WriteLine(savedMacro.Delay);
                    sw.WriteLine(savedMacro.RepeatFor);
                    sw.WriteLine(savedMacro.Duration);
                }
            }

            MacrosSavedFeedbackLabel.Visible = true;
        }


        private void SaveSettings()
        {
            using (FileStream fs = new FileStream(@"" + saveFilePath + "Settings.txt", FileMode.Create, FileAccess.Write))
            using (StreamWriter sw = new StreamWriter(fs))
            {
                foreach (KeyValuePair<string, string> setting in settings)
                {
                    sw.WriteLine(setting.Key + "=" + setting.Value);
                }
            }

            SettingsSavedFeedbackLabel.Visible = true;
        }
    }
}