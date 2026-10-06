using Microsoft.Maui.Graphics;

namespace ColorTracingApp.Models;

/// <summary>
/// A single freehand stroke made by the child, stored as a list of points in
/// the canvas coordinate space.
/// </summary>
public sealed class Stroke
{
    public Color Color { get; init; } = Colors.OrangeRed;

    public float Thickness { get; init; } = 12f;

    public List<PointF> Points { get; } = new();
}
