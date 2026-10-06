using ColorTracingApp.Models;

namespace ColorTracingApp.Controls;

/// <summary>
/// A transparent drawing surface that records freehand strokes so a child can
/// trace over a worksheet image placed behind it. Strokes can be cleared or
/// undone individually.
/// </summary>
public sealed class TracingCanvasView : GraphicsView
{
    private readonly List<Stroke> _strokes = new();
    private Stroke? _activeStroke;

    public TracingCanvasView()
    {
        Drawable = new TracingDrawable(this);
        StartInteraction += OnStartInteraction;
        DragInteraction += OnDragInteraction;
        EndInteraction += OnEndInteraction;
    }

    /// <summary>Raised whenever the set of strokes changes (add, undo or clear).</summary>
    public event EventHandler? StrokesChanged;

    public Color StrokeColor { get; set; } = Colors.OrangeRed;

    public float StrokeThickness { get; set; } = 12f;

    public bool CanUndo => _strokes.Count > 0;

    private void OnStartInteraction(object? sender, TouchEventArgs e)
    {
        var point = e.Touches[0];
        _activeStroke = new Stroke { Color = StrokeColor, Thickness = StrokeThickness };
        _activeStroke.Points.Add(point);
        _strokes.Add(_activeStroke);
        Invalidate();
        StrokesChanged?.Invoke(this, EventArgs.Empty);
    }

    private void OnDragInteraction(object? sender, TouchEventArgs e)
    {
        if (_activeStroke is null)
        {
            return;
        }

        foreach (var point in e.Touches)
        {
            _activeStroke.Points.Add(point);
        }

        Invalidate();
    }

    private void OnEndInteraction(object? sender, TouchEventArgs e)
    {
        if (_activeStroke is null)
        {
            return;
        }

        if (e.Touches.Length > 0)
        {
            _activeStroke.Points.Add(e.Touches[0]);
        }

        _activeStroke = null;
        Invalidate();
        StrokesChanged?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>Removes every stroke from the canvas.</summary>
    public void Clear()
    {
        if (_strokes.Count == 0 && _activeStroke is null)
        {
            return;
        }

        _strokes.Clear();
        _activeStroke = null;
        Invalidate();
        StrokesChanged?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>Removes the most recently drawn stroke.</summary>
    public void Undo()
    {
        if (_strokes.Count == 0)
        {
            return;
        }

        _strokes.RemoveAt(_strokes.Count - 1);
        Invalidate();
        StrokesChanged?.Invoke(this, EventArgs.Empty);
    }

    private sealed class TracingDrawable : IDrawable
    {
        private readonly TracingCanvasView _owner;

        public TracingDrawable(TracingCanvasView owner) => _owner = owner;

        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            canvas.StrokeLineCap = LineCap.Round;
            canvas.StrokeLineJoin = LineJoin.Round;

            foreach (var stroke in _owner._strokes)
            {
                if (stroke.Points.Count == 0)
                {
                    continue;
                }

                if (stroke.Points.Count == 1)
                {
                    canvas.FillColor = stroke.Color;
                    var dot = stroke.Points[0];
                    canvas.FillCircle(dot.X, dot.Y, stroke.Thickness / 2f);
                    continue;
                }

                canvas.StrokeColor = stroke.Color;
                canvas.StrokeSize = stroke.Thickness;

                var path = new PathF();
                path.MoveTo(stroke.Points[0]);
                for (var i = 1; i < stroke.Points.Count; i++)
                {
                    path.LineTo(stroke.Points[i]);
                }

                canvas.DrawPath(path);
            }
        }
    }
}
