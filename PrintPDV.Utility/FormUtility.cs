using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace PrintPDV.Utility
{
    public class FormUtility
    {
        [DllImport("user32.dll")]
        public static extern bool SetCursorPos(int X, int Y);

        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(out POINT lpPoint);

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;

            public static implicit operator Point(POINT point)
            {
                return new Point(point.X, point.Y);
            }
        }

        public static T GetForm<T>() where T : Form
        {
            var frm = Application.OpenForms.OfType<T>().FirstOrDefault();

            return frm;
        }

        public static Form GetFormByName(string name)
        {
            return Application.OpenForms.OfType<Form>().FirstOrDefault(x => x.Name.Equals(name));
        }

        public static void RemoveClickEvent(Button b)
        {
            FieldInfo f1 = typeof(Control).GetField("EventClick", BindingFlags.Static | BindingFlags.NonPublic);
            object obj = f1.GetValue(b);

            PropertyInfo pi = b.GetType().GetProperty("Events", BindingFlags.NonPublic | BindingFlags.Instance);
            EventHandlerList list = (EventHandlerList)pi.GetValue(b, null);

            list.RemoveHandler(obj, list[obj]);
        }

        public static class ThreadFormTextControlHelper
        {
            public delegate void SetTextCallback(Form f, Control ctrl, string text);
            public delegate void SetVisibleCallback(Form f, Control ctrl, bool visible);
            public delegate void SetEnabledCallback(Form f, string controlName, bool enabled);

            public static void SetText(Form form, Control ctrl, string text)
            {
                if (ctrl.InvokeRequired)
                {
                    var d = new SetTextCallback(SetText);
                    form.Invoke(d, form, ctrl, text);
                }
                else
                {
                    ctrl.Text = text;
                }
            }

            public static void SetVisible(Form form, Control ctrl, bool visible)
            {
                if (ctrl.InvokeRequired)
                {
                    var d = new SetVisibleCallback(SetVisible);
                    form.Invoke(d, form, ctrl, visible);
                }
                else
                {
                    ctrl.Visible = visible;
                }
            }

            public static void SetEnabled(Form form, string controlName, bool enabled)
            {
                Control ctrl = GetControlByName(form, controlName);

                if (ctrl.InvokeRequired)
                {
                    var d = new SetEnabledCallback(SetEnabled);
                    form.Invoke(d, form, ctrl, enabled);
                }
                else
                {
                    ctrl.Enabled = enabled;
                }
            }

            private static Control GetControlByName(Form form, string controlName)
            {
                foreach (Control control in form.Controls)
                {
                    if (control.Name.Equals(controlName))
                        return control;
                }

                return null;
            }
        }
    }
}
