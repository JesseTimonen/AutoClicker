using System;
using System.Windows.Forms;
using System.Collections.Generic;

namespace AutoClicker
{
    public partial class AutoClickerForm : Form
    {
        private bool hotkeysEnabled = false;
        private bool isPlayingMacro = false;
        private bool isRecordingMacro = false;
        private bool autoClickerEnabled = false;
        private bool autoKeyboardEnabled = false;
        private bool autoClickerDelayMode = false;
        private bool autoKeyboardDelayMode = false;
        private int macroDuration;
        private int hotkeyTimerStep;
        private int hotkeyIDCounter;
        private int macroRepeatCounter;
        private int AutoClickerDelayCounter;
        private int[] mouseLastPosition;
        private string[] autoKeyboardPattern;
        private int autoKeyboardPatternStep;
        private static int timerStep;
        private static string newRecordedMacroInstructions;
        private static string placeholderNewRecordedMacroInstructions;
        private int[] hotkeyInstructions;
        private string deletedObject;
        private int deletedIndex;
        private bool limitAutoClicker = false;
        private int limitAutoClickerTime;
        private float autoClickerActiveTime = 0;
        private bool autoClickerClickWhileMoving = false;
        private Dictionary<int, string[]> macroInstructions = new Dictionary<int, string[]>();
        private Dictionary<string, string> settings = new Dictionary<string, string>();
        private List<Hotkey> savedHotkeys = new List<Hotkey>();
        private List<Macro> savedMacros = new List<Macro>();
        private List<Label> macroNameLabels = new List<Label>();
        private List<Label> macroHotkeyLabels = new List<Label>();
        private List<Label> macroDelayLabels = new List<Label>();
        private List<Label> macroRepeatLabels = new List<Label>();
        private List<TextBox> macroNameTextboxes = new List<TextBox>();
        private List<TextBox> macroHotkeyTextboxes = new List<TextBox>();
        private List<TextBox> macroDelayTextboxes = new List<TextBox>();
        private List<TextBox> macroRepeatTextboxes = new List<TextBox>();
        private List<Button> playMacroButtons = new List<Button>();
        private List<Button> deleteMacroButtons = new List<Button>();
        private List<Label> hotkeyNameLabels = new List<Label>();
        private List<Label> hotkeyInputLabels = new List<Label>();
        private List<TextBox> hotkeyNameTextboxes = new List<TextBox>();
        private List<TextBox> hotkeyInputTextboxes = new List<TextBox>();
        private List<Button> deleteHotkeyButtons = new List<Button>();
        private List<KeyHandler> keyHandlers = new List<KeyHandler>();
        private KeyHandler newHotkeyKeyHandler;
        private KeyHandler startMacroKeyHandler;
        private KeyHandler stopMacroKeyHandler;
        private KeyHandler startAutoKeyboardKeyHandler;
        private KeyHandler stopAutoKeyboardKeyHandler;
        private KeyHandler startAutoClickerKeyHandler;
        private KeyHandler stopAutoClickerKeyHandler;

        public struct Hotkey
        {
            public string Name;
            public string Input;
            public int X;
            public int Y;

            public Hotkey(string name, string input, int x, int y)
            {
                Name = name;
                Input = input;
                X = x;
                Y = y;
            }
        }

        public struct Macro
        {
            public string Name;
            public string Instructions;
            public string Hotkey;
            public int Delay;
            public int RepeatFor;
            public int Duration;

            public Macro(string name, string instructions, string hotkey, int delay, int repeatFor, int duration)
            {
                Name = name;
                Instructions = instructions;
                Hotkey = hotkey;
                Delay = delay;
                RepeatFor = repeatFor;
                Duration = duration;
            }
        }

        public AutoClickerForm()
        {
            InitializeComponent();
        }

        private void MacroManagerForm_Load(object sender, EventArgs e)
        {
            InitSave();
            LoadData();
            UpdateTimerInterval();
            CreateMacroPlaceholders();
            CreateHotkeyPlaceholders();
            UpdateMacrosUI();
            UpdateHotkeysUI();
            UpdateHotkeyHelpTexts();

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
        }

        private void MacroManagerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            DeactivateMouseHook();

            foreach (KeyHandler keyHandler in keyHandlers)
            {
                keyHandler.Unregister();
            }

            startMacroKeyHandler.Unregister();
            stopMacroKeyHandler.Unregister();
            newHotkeyKeyHandler.Unregister();
            startAutoClickerKeyHandler.Unregister();
            stopAutoClickerKeyHandler.Unregister();
            startAutoKeyboardKeyHandler.Unregister();
            stopAutoKeyboardKeyHandler.Unregister();
        }

        private void MinimizeApplicationButton_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void CloseApplicationButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        // ========================================== Keyboard hotkeys ========================================= \\
        // Check for keyboard inputs
        protected override void WndProc(ref Message m)
        {
            if (DeletePanelParent.Visible) { return; }
            if (m.Msg == 0x0312)
            {
                int id = m.WParam.ToInt32();

                // Start macro hotkey
                if (id == 10001)
                {
                    if (bool.Parse(settings["SameHotkeyForMacros"]) == false)
                    {
                        if (!isPlayingMacro && !isRecordingMacro && !autoClickerEnabled && !autoKeyboardEnabled) { startMacroRecording(); }
                    }
                    else
                    {
                        if (isPlayingMacro)
                        {
                            stopMacroReplay();
                        }
                        else if (isRecordingMacro)
                        {
                            stopMacroRecording();
                        }
                        else if (!autoClickerEnabled && !autoKeyboardEnabled)
                        {
                            startMacroRecording();
                        }
                    }
                }
                // Stop macro hotkey
                else if (id == 10002)
                {
                    if (bool.Parse(settings["SameHotkeyForMacros"]) == false)
                    {
                        if (isPlayingMacro) { stopMacroReplay(); }
                        else if (isRecordingMacro) { stopMacroRecording(); }
                    }
                }
                // "Create hotkey" -hotkey
                else if (id == 10003)
                {
                    if (!hotkeysEnabled)
                    {
                        CreateNewHotkeyPlaceholder();
                        savedHotkeys.Add(new Hotkey("New Hotkey", "none", Cursor.Position.X, Cursor.Position.Y));
                        SaveHotkeys();
                        UpdateHotkeysUI();
                    }
                }
                // Start auto clicker hotkey
                else if (id == 10004)
                {
                    if (!isPlayingMacro && !isRecordingMacro && !autoKeyboardEnabled)
                    {
                        if (bool.Parse(settings["SameHotkeyForAutoClicker"]))
                        {
                            ToggleAutoClickerButton_Click(null, null);
                        }
                        else
                        {
                            StartAutoClicker();
                        }
                    }
                }
                // Stop auto clicker hotkey
                else if (id == 10005)
                {
                    if (autoClickerEnabled && bool.Parse(settings["SameHotkeyForAutoClicker"]) == false)
                    {
                        StopAutoClicker();
                    }
                }
                // Start Auto Keyboard
                else if (id == 10006)
                {
                    if (!isPlayingMacro && !isRecordingMacro && !autoClickerEnabled)
                    {
                        if (bool.Parse(settings["SameHotkeyForAutoKeyboard"]))
                        {
                            ToggleAutoKeyboardButton_Click(null, null);
                        }
                        else
                        {
                            StartAutoKeyboard();
                        }
                    }
                }
                // Stop Auto Keyboard
                else if (id == 10007)
                {
                    if (autoKeyboardEnabled && bool.Parse(settings["SameHotkeyForAutoKeyboard"]) == false)
                    {
                        StopAutoKeyboard();
                    }
                }
                else
                {
                    // Macro hotkeys
                    if (id >= 1000)
                    {
                        if (!isPlayingMacro && !isRecordingMacro && !autoClickerEnabled)
                        {
                            id -= 1000;
                            PlayMacro(id);
                        }
                        else if (isPlayingMacro)
                        {
                            stopMacroReplay();
                        }
                    }
                    // Normal hotkeys
                    else
                    {
                        hotkeyInstructions = new int[] { savedHotkeys[id].X, savedHotkeys[id].Y };
                        StartHotkeyTimer();
                    }
                }
            }
            base.WndProc(ref m);
        }
        // ======================================== End Keyboard hotkeys ======================================= \\



        // =========================================== Miscellaneous =========================================== \\
        private void UpdateHotkeyHelpTexts()
        {
            HotkeyGuideLabel2.Text = "Hover your mouse over\nthe point you want to\nactivate with the hotkey\nand press [" + ((settings["CreateHotkeyHotkey"].ToLower() != "already in use" && settings["CreateHotkeyHotkey"].ToLower() != "none") ? settings["CreateHotkeyHotkey"] : "None") + "] and the\nhotkey will be created.\n\nGive the hotkey a input\nkey and click the save\nhotkeys button.\n\nNote: You can only\ncreate hotkeys when\nhotkeys module is not\nactive.\n";
           
            if (settings["StartAutoClickerHotkey"].ToLower() == "already in use" || settings["StartAutoClickerHotkey"].ToLower() == "none")
            {
                ToggleAutoClickerButton.Text = "Start AutoClicker";
            }
            else
            {
                ToggleAutoClickerButton.Text = "Start AutoClicker\n(Hotkey: " + settings["StartAutoClickerHotkey"] + ")";
            }

            if (settings["StartMacroHotkey"].ToLower() == "already in use" || settings["StartMacroHotkey"].ToLower() == "none")
            {
                NewMacroButton.Text = "Record New Macro";
            }
            else
            {
                NewMacroButton.Text = "Record New Macro\n(Hotkey: " + settings["StartMacroHotkey"] + ")";
            }

            if (settings["StartAutoKeyboardHotkey"].ToLower() == "already in use" || settings["StartAutoKeyboardHotkey"].ToLower() == "none")
            {
                ToggleAutoKeyboardButton.Text = "Start AutoKeyboard";
            }
            else
            {
                ToggleAutoKeyboardButton.Text = "Start AutoKeyboard\n(Hotkey: " + settings["StartAutoKeyboardHotkey"] + ")";
            }
        }
        // ========================================= End Miscellaneous ========================================= \\
    }
}