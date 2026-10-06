using ColorTracingApp.Models;
using Microsoft.Maui.Controls.Shapes;
using Microsoft.Maui.Graphics;

namespace ColorTracingApp.Controls;

/// <summary>
/// A horizontal row of tappable colour swatches. The selected swatch is marked
/// with a tick, and <see cref="ColorSelected"/> reports the chosen colour.
/// </summary>
public sealed class ColorPaletteView : ContentView
{
    private const double SwatchSize = 48;

    private readonly List<PaletteColor> _colors;
    private readonly List<Swatch> _swatches = new();
    private readonly HorizontalStackLayout _row;

    public ColorPaletteView()
    {
        _colors = BuildDefaultPalette();
        _row = new HorizontalStackLayout { Spacing = 10, Padding = new Thickness(4, 2) };

        foreach (var color in _colors)
        {
            var swatch = CreateSwatch(color);
            _swatches.Add(swatch);
            _row.Add(swatch.Border);
        }

        Content = new ScrollView
        {
            Orientation = ScrollOrientation.Horizontal,
            HorizontalScrollBarVisibility = ScrollBarVisibility.Never,
            Content = _row,
        };

        Select(_colors[0]);
    }

    /// <summary>Raised when the user taps a different colour.</summary>
    public event EventHandler<Color>? ColorSelected;

    public Color SelectedColor { get; private set; } = Colors.OrangeRed;

    public string SelectedName { get; private set; } = string.Empty;

    private void Select(PaletteColor color)
    {
        foreach (var swatch in _swatches)
        {
            swatch.Color.IsSelected = ReferenceEquals(swatch.Color, color);
            ApplyVisualState(swatch);
        }

        SelectedColor = color.Color;
        SelectedName = color.Name;
        ColorSelected?.Invoke(this, color.Color);
    }

    private Swatch CreateSwatch(PaletteColor color)
    {
        var tick = new Label
        {
            Text = "\u2713",
            FontSize = 24,
            FontAttributes = FontAttributes.Bold,
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,
        };

        var border = new Border
        {
            WidthRequest = SwatchSize,
            HeightRequest = SwatchSize,
            BackgroundColor = color.Color,
            StrokeShape = new RoundRectangle { CornerRadius = new CornerRadius(SwatchSize / 2) },
            Content = tick,
        };

        var swatch = new Swatch(color, border, tick);
        ApplyVisualState(swatch);

        var tap = new TapGestureRecognizer();
        tap.Tapped += (_, _) => Select(color);
        border.GestureRecognizers.Add(tap);

        return swatch;
    }

    private static void ApplyVisualState(Swatch swatch)
    {
        var selected = swatch.Color.IsSelected;
        swatch.Border.Stroke = selected ? Colors.White : Color.FromArgb("#22000000");
        swatch.Border.StrokeThickness = selected ? 4 : 1;
        swatch.Tick.IsVisible = selected;
        swatch.Tick.TextColor = ContrastFor(swatch.Color.Color);
    }

    /// <summary>Picks a tick colour that stays readable on light and dark swatches.</summary>
    private static Color ContrastFor(Color background)
    {
        var luminance = (0.299 * background.Red) + (0.587 * background.Green) + (0.114 * background.Blue);
        return luminance > 0.6 ? Colors.Black : Colors.White;
    }

    private static List<PaletteColor> BuildDefaultPalette() =>
    [
        new PaletteColor { Name = "Red", Color = Color.FromArgb("#E53935") },
        new PaletteColor { Name = "Orange", Color = Color.FromArgb("#FB8C00") },
        new PaletteColor { Name = "Yellow", Color = Color.FromArgb("#FDD835") },
        new PaletteColor { Name = "Green", Color = Color.FromArgb("#43A047") },
        new PaletteColor { Name = "Teal", Color = Color.FromArgb("#00897B") },
        new PaletteColor { Name = "Blue", Color = Color.FromArgb("#1E88E5") },
        new PaletteColor { Name = "Purple", Color = Color.FromArgb("#8E24AA") },
        new PaletteColor { Name = "Pink", Color = Color.FromArgb("#EC407A") },
        new PaletteColor { Name = "Brown", Color = Color.FromArgb("#6D4C41") },
        new PaletteColor { Name = "Black", Color = Color.FromArgb("#212121") },
    ];

    private sealed record Swatch(PaletteColor Color, Border Border, Label Tick);
}
