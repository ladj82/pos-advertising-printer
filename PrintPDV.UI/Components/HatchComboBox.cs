using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PrintPDV.UI.Components
{
    /// <summary>
    /// Summary description for HSComboBox.
    /// </summary>
    [ToolboxBitmap(typeof(ComboBox))]
    public class HatchComboBox : ComboBox
    {
        public HatchComboBox()
            : base()
        {
            DrawMode = DrawMode.OwnerDrawVariable;
            SetStyle(ControlStyles.DoubleBuffer, true);
            InitializeDropDown();
        }

        ~HatchComboBox()
        {
            base.Dispose();
            Dispose(true);
        }

        protected void InitializeDropDown()
        {
            Items.Add("Black");
            Items.Add("White");

            foreach (var styleName in Enum.GetNames(typeof(HatchStyle)))
                Items.Add(styleName);
        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            // The following method should generally be called before drawing.
            // It is actually superfluous here, since the subsequent drawing
            // will completely cover the area of interest.
            e.DrawBackground();
            //The system provides the context
            //into which the owner custom-draws the required graphics.
            //The context into which to draw is e.graphics.
            //The index of the item to be painted is e.Index.
            //The painting should be done into the area described by e.Bounds.

            if (e.Index != -1)
            {
                var g = e.Graphics;
                var r = e.Bounds;

                var rd = r;
                rd.Width = rd.Left + 25;
                r.X = rd.Right;
                var displayText = Items[e.Index].ToString();
                Brush b;

                if (displayText.Equals("White"))
                {
                    b = new SolidBrush(Color.White);
                }
                else if (displayText.Equals("Black"))
                {
                    b = new SolidBrush(Color.Black);
                }
                else
                {
                    var hs = (HatchStyle)Enum.Parse(typeof(HatchStyle), displayText, true);
                    b = new HatchBrush(hs, Color.Black, e.BackColor);
                }

                g.DrawRectangle(new Pen(Color.Black, 2), rd.X + 3, rd.Y + 2, rd.Width - 4, rd.Height - 4);
                g.FillRectangle(b, new Rectangle(rd.X + 3, rd.Y + 2, rd.Width - 4, rd.Height - 4));

                var sf = new StringFormat {Alignment = StringAlignment.Near};

                //If the current item has focus.
                if ((e.State & DrawItemState.Focus) == 0)
                {
                    e.Graphics.FillRectangle(new SolidBrush(SystemColors.Window), r);
                    e.Graphics.DrawString(displayText, Font, new SolidBrush(SystemColors.WindowText), r, sf);
                }
                else
                {
                    e.Graphics.FillRectangle(new SolidBrush(SystemColors.Highlight), r);
                    e.Graphics.DrawString(displayText, Font, new SolidBrush(SystemColors.HighlightText), r, sf);
                }
            }

            //Draws a focus rectangle on the specified graphics surface and within the specified bounds.
            e.DrawFocusRectangle();
        }

        protected override void OnMeasureItem(MeasureItemEventArgs e)
        {
            //Work out what the text will be
            var displayText = Items[e.Index].ToString();

            //Get width & height of string
            var stringSize = e.Graphics.MeasureString(displayText, Font);

            //Account for top margin
            stringSize.Height += 5;

            // set hight to text height
            e.ItemHeight = (int)stringSize.Height;

            // set width to text width
            e.ItemWidth = (int)stringSize.Width;
        }
    }
}
