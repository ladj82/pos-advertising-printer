using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace PrintPDV.UI.Components
{
    public sealed partial class ImageButton : Control, IButtonControl
    {
        private enum MouseStates
        {
            Up,
            Over,
            Down, 
            Disabled
        }

        public enum TextDrawOptions
        {
            ByAlignment,
            ByLocation
        }
        
        #region Fields
        //Images
        private Image _upImage;                 //Image for idle state

        //Text
        private ContentAlignment _textAlign;        //Alignment of text
        private Point _textLocation;                //Network of text
        private TextDrawOptions _useTextLocation;   //Determine how to draw text
        private int _textMarginHeight;              //Margin height - create text box
        private int _textMarginWidth;               //Margin width  - create text box

        //Text Color in down/over states
        private Color _downForeColor;               //ForeColor when the button is down
        private Color _overForeColor;               //ForeColor when the button is down

        private bool _selected;  //Flag indicating if the button is in selected state

        private MouseStates _mouseState = MouseStates.Up;   //State of the interaction with button

        //For IButtonControl interface
        private DialogResult _dialogResult;

        #endregion Fields

        #region Constructor/s
        public ImageButton()
            : this(null)
        { }

        public ImageButton(IContainer container)
        {
            if(container != null)
                container.Add(this);

            InitializeComponent();
            SetStyle();
            BackColor = Color.Transparent;
            TextAlign = ContentAlignment.MiddleCenter;
            TextLocationOverride = TextDrawOptions.ByAlignment;
        }
        #endregion Constructor/s
        
        #region Properties
        /// <summary>
        /// Get/Set the image displayed when the button is in idle state (no mouse)
        /// </summary>
        [Category("Appearance"), Description("Specify the idle state image")]
        public Image ImageUp
        {
            get { return _upImage; }
            set 
            {
                _upImage = value;
                if (value != null)
                {
                    Height = value.Height;
                    Width = value.Width;
                }
                Invalidate();
            }
        }

        /// <summary>
        /// Get/Set the image displayed when the mouse is over the button (mouse-over)
        /// </summary>
        [Category("Appearance"), Description("The mouse-over state image")]
        public Image ImageOver { get; set; }

        /// <summary>
        /// Get/Set the image displayed when the button is clicked (mouse-down)
        /// </summary>
        [Category("Appearance"), Description("The mouse-down state image")]
        public Image ImageDown { get; set; }

        /// <summary>
        /// Get/Set the image displayed when the button is disabled
        /// </summary>
        [Category("Appearance"), Description("The image when the control is disabled")]
        public Image ImageDisabled { get; set; }

        /// <summary>
        /// Get/Set the image displayed when the button is selected (similar to checked state in Checkox)
        /// </summary>
        [Category("Appearance"), Description("The image when the control is checked")]
        public Image ImageSelected { get; set; }

        /// <summary>
        /// Get/Set the alignment of the text
        /// </summary>
        [Category("Appearance"), Description("The alignment of the text (when TextLocationOverride is ByAlignment)")]
        public ContentAlignment TextAlign
        {
            get { return _textAlign;  }
            set 
            { 
                _textAlign = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Get/Set the alignment of the text
        /// </summary>
        [Category("Appearance"), Description("The location for text (when TextLocationOverride is ByLocation)")]
        public Point TextLocation
        {
            get { return _textLocation; }
            set 
            { 
                _textLocation = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Get/Set whether the location should be determined by TextAlign or TextLocation
        /// </summary>
        [Category("Appearance"), Description("Determine if the TextAlign or the TextLocation property is used to specify text's location")]
        public TextDrawOptions TextLocationOverride
        {
            get { return _useTextLocation; }
            set 
            { 
                _useTextLocation = value;
                Invalidate();
            }
        }


        public int TextMarginHeight
        {
            get { return _textMarginHeight; }
            set
            {
                _textMarginHeight = value;
                Invalidate();
            }
        }


        public int TextMarginWidth
        {
            get { return _textMarginWidth; }
            set
            {
                _textMarginWidth = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Get/Set the selected state of the button
        /// </summary>
        [Category("Appearance"), Description("Determine if the button is in selected state")]
        public bool Selected
        {
            get { return _selected; }
            set 
            { 
                _selected = value;
                Invalidate();
            }
        }

        [Category("Appearance"), Description("Determine the color of text in Down State")]
        public Color ForeColorDown
        {
            get { return _downForeColor; }
            set { _downForeColor = value; }
        }

        [Category("Appearance"), Description("Determine the color of text in Over State")]
        public Color ForeColorOver
        {
            get { return _overForeColor; }
            set { _overForeColor = value; }
        }

        /// <summary>
        /// Get/Set the text displayed on the button
        /// </summary>
        public override string Text
        {
            get { return base.Text; }
            set
            {
                base.Text = value;
                Invalidate();
            }
        }
        #endregion Properties
        
        #region Functionality
        private void SetStyle()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer | 
                          ControlStyles.UserPaint | 
                          ControlStyles.AllPaintingInWmPaint | 
                          ControlStyles.ResizeRedraw |
                          ControlStyles.SupportsTransparentBackColor, true);
        }

        #endregion Functionality

        #region Override Events

        protected override void OnPaint(PaintEventArgs e)
        {
            var drawRect = new Rectangle(0, 0, Width, Height);

            var img = ImageUp;
            var textColor = ForeColor;

            if (!Enabled)
            {
                if(ImageDisabled != null)
                    img = ImageDisabled;
            }
            else
            {
                switch (_mouseState)
                {
                    case MouseStates.Up:
                        if (Selected)
                        {
                            if (ImageSelected != null)
                                img = ImageSelected;
                        }
                        else
                        {
                            if (ImageUp != null)
                                img = ImageUp;
                        }
                        textColor = ForeColor;
                        break;
                    case MouseStates.Over:
                        if (ImageOver != null)
                            img = ImageOver;
                        textColor = ForeColorOver;
                        break;
                    case MouseStates.Down:
                        if (ImageDown != null)
                            img = ImageDown;
                        textColor = ForeColorDown;
                        break;
                    default:
                        img = ImageUp;
                        break;
                }
            }

            if (img != null)
            {
                e.Graphics.DrawImage(img, drawRect, 0, 0, Width, Height, GraphicsUnit.Pixel);
            }

            #region Draw text
            if (!string.IsNullOrEmpty(base.Text))
            {
                int x, y;

                var s = e.Graphics.MeasureString(base.Text, Font);
                if (s.Width > Width)
                {
                    //Need to warp the text to fit
                }
                #region Figure the text location according to alignment or specified point
                if (TextLocationOverride == TextDrawOptions.ByAlignment)
                {
                    switch (TextAlign)
                    {
                        case ContentAlignment.TopCenter:
                        case ContentAlignment.TopLeft:
                        case ContentAlignment.TopRight:
                            y = 0;
                            break;
                        case ContentAlignment.MiddleCenter:
                        case ContentAlignment.MiddleLeft:
                        case ContentAlignment.MiddleRight:
                            y = (int)((Height - s.Height) / 2);
                            break;
                        case ContentAlignment.BottomCenter:
                        case ContentAlignment.BottomLeft:
                        case ContentAlignment.BottomRight:
                            y = (int)(Height - s.Height);
                            break;
                        default:
                            y = 0;
                            break;
                    }

                    switch (TextAlign)
                    {
                        case ContentAlignment.BottomLeft:
                        case ContentAlignment.MiddleLeft:
                        case ContentAlignment.TopLeft:
                            x = 0;
                            break;
                        case ContentAlignment.BottomCenter:
                        case ContentAlignment.MiddleCenter:
                        case ContentAlignment.TopCenter:
                            x = (int)((Width - s.Width) / 2);
                            break;
                        case ContentAlignment.BottomRight:
                        case ContentAlignment.MiddleRight:
                        case ContentAlignment.TopRight:
                            x = (int)(Width - s.Width);
                            break;
                        default:
                            x = 0;
                            break;
                    }
                }
                else 
                {
                    x = TextLocation.X;
                    y = TextLocation.Y;
                }
                #endregion 
                Brush brush = new SolidBrush(textColor);
                e.Graphics.DrawString(base.Text, Font, brush, x, y);
                brush.Dispose();
            #endregion Draw text

            }
        }

        protected override void OnMove(EventArgs e)
        {
            Invalidate();
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            _mouseState = MouseStates.Over;
            Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            _mouseState = MouseStates.Down;
            Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            _mouseState = MouseStates.Over;
            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            _mouseState = MouseStates.Up;
            Invalidate();
        }
        #endregion Override Events

        #region IButtonControl Interface implmentation

        public bool IsDefault { get; set; }

        public DialogResult DialogResult
        {
            get { return _dialogResult; }
            set 
            {
                if (Enum.IsDefined(typeof(DialogResult), value))
                {
                    _dialogResult = value;
                }
            }
        }

        public void NotifyDefault(bool value)
        {
            IsDefault = value;
        }

        public void PerformClick()
        {
            if (CanSelect)
            {
                OnClick(EventArgs.Empty);
            }
        }
        #endregion IButtonControl Interface implmentation
    }
}
