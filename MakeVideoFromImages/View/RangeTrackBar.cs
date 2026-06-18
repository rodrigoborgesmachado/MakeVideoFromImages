namespace MakeVideoFromImages;

public sealed class RangeTrackBar : Control
{
    private const int ThumbWidth = 12;
    private const int ThumbHeight = 22;
    private const int TrackHeight = 4;
    private int _maximum = 100;
    private int _startValue;
    private int _endValue = 100;
    private DragTarget _dragTarget = DragTarget.None;

    public RangeTrackBar()
    {
        SetStyle(
            ControlStyles.AllPaintingInWmPaint |
            ControlStyles.OptimizedDoubleBuffer |
            ControlStyles.ResizeRedraw |
            ControlStyles.UserPaint,
            true);

        Height = 38;
        MinimumSize = new Size(120, 34);
    }

    public event EventHandler? RangeChanged;

    public event EventHandler? RangeChangeCommitted;

    public int Maximum
    {
        get => _maximum;
        set
        {
            _maximum = Math.Max(0, value);
            _startValue = Math.Clamp(_startValue, 0, _maximum);
            _endValue = Math.Clamp(_endValue, _startValue, _maximum);
            Invalidate();
        }
    }

    public int StartValue
    {
        get => _startValue;
        set
        {
            var newValue = Math.Clamp(value, 0, EndValue);
            if (_startValue == newValue)
            {
                return;
            }

            _startValue = newValue;
            Invalidate();
            RangeChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    public int EndValue
    {
        get => _endValue;
        set
        {
            var newValue = Math.Clamp(value, StartValue, Maximum);
            if (_endValue == newValue)
            {
                return;
            }

            _endValue = newValue;
            Invalidate();
            RangeChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    public void SetRange(int maximum, int startValue, int endValue)
    {
        _maximum = Math.Max(0, maximum);
        _startValue = Math.Clamp(startValue, 0, _maximum);
        _endValue = Math.Clamp(endValue, _startValue, _maximum);
        Invalidate();
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
        base.OnMouseDown(e);
        if (!Enabled || e.Button != MouseButtons.Left)
        {
            return;
        }

        Focus();
        var startX = ValueToX(StartValue);
        var endX = ValueToX(EndValue);
        _dragTarget = Math.Abs(e.X - startX) <= Math.Abs(e.X - endX)
            ? DragTarget.Start
            : DragTarget.End;
        SetValueFromMouse(e.X);
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
        base.OnMouseMove(e);
        if (!Enabled)
        {
            return;
        }

        Cursor = _dragTarget != DragTarget.None || IsNearThumb(e.X)
            ? Cursors.Hand
            : Cursors.Default;

        if (_dragTarget != DragTarget.None)
        {
            SetValueFromMouse(e.X);
        }
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
        base.OnMouseUp(e);
        var wasDragging = _dragTarget != DragTarget.None;
        _dragTarget = DragTarget.None;
        if (wasDragging)
        {
            RangeChangeCommitted?.Invoke(this, EventArgs.Empty);
        }
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);

        var bounds = ClientRectangle;
        var trackY = bounds.Top + (bounds.Height / 2) - (TrackHeight / 2);
        var trackRect = new Rectangle(GetTrackLeft(), trackY, GetTrackWidth(), TrackHeight);
        var startX = ValueToX(StartValue);
        var endX = ValueToX(EndValue);
        var selectedRect = Rectangle.FromLTRB(startX, trackY, endX, trackY + TrackHeight);

        using var backgroundBrush = new SolidBrush(Enabled ? Color.FromArgb(210, 210, 210) : Color.FromArgb(235, 235, 235));
        using var selectedBrush = new SolidBrush(Enabled ? Color.FromArgb(0, 120, 215) : Color.FromArgb(160, 160, 160));
        using var thumbBrush = new SolidBrush(Enabled ? Color.FromArgb(0, 120, 215) : Color.FromArgb(150, 150, 150));
        using var thumbBorderPen = new Pen(Color.FromArgb(30, 90, 160));

        e.Graphics.FillRectangle(backgroundBrush, trackRect);
        e.Graphics.FillRectangle(selectedBrush, selectedRect);

        DrawThumb(e.Graphics, startX, thumbBrush, thumbBorderPen);
        DrawThumb(e.Graphics, endX, thumbBrush, thumbBorderPen);
    }

    private void SetValueFromMouse(int x)
    {
        var value = XToValue(x);
        if (_dragTarget == DragTarget.Start)
        {
            StartValue = value;
        }
        else if (_dragTarget == DragTarget.End)
        {
            EndValue = value;
        }
    }

    private bool IsNearThumb(int x)
    {
        return Math.Abs(x - ValueToX(StartValue)) <= ThumbWidth ||
            Math.Abs(x - ValueToX(EndValue)) <= ThumbWidth;
    }

    private void DrawThumb(Graphics graphics, int centerX, Brush brush, Pen borderPen)
    {
        var top = (Height - ThumbHeight) / 2;
        var thumbRect = new Rectangle(centerX - (ThumbWidth / 2), top, ThumbWidth, ThumbHeight);
        graphics.FillRectangle(brush, thumbRect);
        graphics.DrawRectangle(borderPen, thumbRect);
    }

    private int ValueToX(int value)
    {
        if (Maximum <= 0)
        {
            return GetTrackLeft();
        }

        var ratio = value / (double)Maximum;
        return GetTrackLeft() + (int)Math.Round(ratio * GetTrackWidth());
    }

    private int XToValue(int x)
    {
        if (Maximum <= 0)
        {
            return 0;
        }

        var ratio = (x - GetTrackLeft()) / (double)GetTrackWidth();
        return Math.Clamp((int)Math.Round(ratio * Maximum), 0, Maximum);
    }

    private int GetTrackLeft() => ThumbWidth / 2;

    private int GetTrackWidth() => Math.Max(1, Width - ThumbWidth);

    private enum DragTarget
    {
        None,
        Start,
        End
    }
}
