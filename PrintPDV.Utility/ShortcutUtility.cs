using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace PrintPDV.Utility
{
    public sealed class ShortcutUtility
    {
        public static bool RegisterHotKey(string keyString, EventHandler<KeyPressedEventArgs> keyPressed)
        {
            var shortcut = ShortcutHandler.Instance.GetShortcutFromString(keyString);

            return RegisterHotKey(shortcut.Key, shortcut.Value, keyPressed);
        }

        public static bool RegisterHotKey(ModifierKeys modifier, Keys key, EventHandler<KeyPressedEventArgs> keyPressed)
        {
            return ShortcutHandler.Instance.SetHotKey((uint)modifier, (uint)key, keyPressed);
        }

        public static string GetShortcut(KeyPressedEventArgs e)
        {
            var lstPressedKeys = e.Modifier.ToString().Replace(" ", "").Split(',').ToList();
            lstPressedKeys.Add(((Keys)Enum.Parse(typeof(Keys), e.Key.ToString())).ToString());

            return string.Join("+", lstPressedKeys.ToArray());
        }
    }

    public sealed class ShortcutHandler
    {
        private sealed class Window : NativeWindow, IDisposable
        {
            private const int WM_HOTKEY = 0x0312;

            public Window()
            {
                CreateHandle(new CreateParams());
            }

            protected override void WndProc(ref Message m)
            {
                base.WndProc(ref m);

                if (m.Msg != WM_HOTKEY) return;

                var key = (Keys)(((int)m.LParam >> 16) & 0xFFFF);
                var modifier = (ModifierKeys)((int)m.LParam & 0xFFFF);

                if (KeyPressed != null)
                    KeyPressed(this, new KeyPressedEventArgs(modifier, key));
            }

            public event EventHandler<KeyPressedEventArgs> KeyPressed;

            public void Dispose()
            {
                DestroyHandle();
            }
        }

        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        static ShortcutHandler _instance;
        static int _counter;
        static readonly object Locker = new object();
        static readonly Dictionary<Window, int> DicWindow = new Dictionary<Window, int>();

        private ShortcutHandler()
        {

        }

        public static ShortcutHandler Instance
        {
            get
            {
                lock (Locker)
                {
                    if (_instance == null)
                        _instance = new ShortcutHandler();

                    return _instance;
                }
            }
        }

        private int IncreaseCounter()
        {
            return ++_counter;
        }

        private int DecreaseCounter()
        {
            return --_counter;
        }

        private void ResetCounter()
        {
            _counter = 0;
        }

        public KeyValuePair<ModifierKeys, Keys> GetShortcutFromString(string keyString)
        {
            ModifierKeys modifiers = 0;
            Keys key = 0;
            var result = new KeyValuePair<ModifierKeys,Keys>();

            if (string.IsNullOrEmpty(keyString)) return result;

            keyString.Split('+').ToList().ForEach(item =>
            {
                if (item.Equals(Keys.Control.ToString()))
                    modifiers |= ModifierKeys.Control;
                else if (item.Equals(Keys.Alt.ToString()))
                    modifiers |= ModifierKeys.Alt;
                else if (item.Equals(Keys.Shift.ToString()))
                    modifiers |= ModifierKeys.Shift;
                else
                    key = (Keys)Enum.Parse(typeof(Keys), item);
            });

            result = new KeyValuePair<ModifierKeys, Keys>(modifiers, key);

            return result;
        }

        public bool SetHotKey(uint fsModifiers, uint vk, EventHandler<KeyPressedEventArgs> keyPressed)
        {
            var window = new Window();

            window.KeyPressed += delegate(object sender, KeyPressedEventArgs args)
            {
                if (keyPressed != null)
                    keyPressed(this, args);
            };

            var id = Instance.IncreaseCounter();

            DicWindow.Add(window, id);

            return RegisterHotKey(window.Handle, id, fsModifiers, vk);
        }

        public bool IsHotKeyAvailable(string keyString)
        {
            var window = new Window();
            var shortcut = GetShortcutFromString(keyString);

            var result = RegisterHotKey(window.Handle, Instance.IncreaseCounter(), (uint)shortcut.Key, (uint)shortcut.Value);

            if (!result) return false;

            var shortcutId = Instance.DecreaseCounter();
            var shortcutToRemove = DicWindow.FirstOrDefault(x => x.Value.Equals(shortcutId));
                
            DicWindow.Remove(DicWindow.FirstOrDefault(x => x.Value.Equals(shortcutId)).Key);
            UnregisterHotKey(shortcutToRemove.Key.Handle, shortcutToRemove.Value);

            return true;
        }

        public void UnsetHotKeys()
        {
            DicWindow.ToList().ForEach(item => {
                UnregisterHotKey(item.Key.Handle, item.Value);
                item.Key.Dispose();
            });

            DicWindow.Clear();
            Instance.ResetCounter();
        }
    }

    /// <summary>
    /// Event Args for the event that is fired after the hot key has been pressed.
    /// </summary>
    public class KeyPressedEventArgs : EventArgs
    {
        private readonly ModifierKeys _modifier;
        private readonly Keys _key;

        internal KeyPressedEventArgs(ModifierKeys modifier, Keys key)
        {
            _modifier = modifier;
            _key = key;
        }

        public ModifierKeys Modifier
        {
            get { return _modifier; }
        }

        public Keys Key
        {
            get { return _key; }
        }
    }

    /// <summary>
    /// The enumeration of possible modifiers.
    /// </summary>
    [Flags]
    public enum ModifierKeys : uint
    {
        Alt = 1,
        Control = 2,
        Shift = 4
    }

    public class HotKeyCls
    {
        public bool Ctrl { get; set; }

        public bool Shift { get; set; }

        public bool Alt { get; set; }

        public Keys Key { get; set; }

        public override string ToString()
        {
            return (Ctrl ? string.Format("{0}+", ModifierKeys.Control) : "")
                   + (Shift ? string.Format("{0}+", ModifierKeys.Shift) : "")
                   + (Alt ? string.Format("{0}+", ModifierKeys.Alt) : "")
                   + Key;
        }
    }
}
