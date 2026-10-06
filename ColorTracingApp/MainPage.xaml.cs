using ColorTracingApp.Models;
using Microsoft.Maui.Graphics;

namespace ColorTracingApp;

public partial class MainPage : ContentPage
{
    private readonly IReadOnlyList<TracingTemplate> _templates =
    [
        new TracingTemplate
        {
            Name = "Bee",
            ImageSource = "template_bee.png",
            Description = "Draw a bee.",
        },
        new TracingTemplate
        {
            Name = "Butterfly",
            ImageSource = "template_butterfly.png",
            Description = "Draw a butterfly.",
        },
        new TracingTemplate
        {
            Name = "Caterpillar",
            ImageSource = "template_caterpillar.png",
            Description = "Draw a caterpillar.",
        },
        new TracingTemplate
        {
            Name = "Ladybird",
            ImageSource = "template_ladybird.png",
            Description = "Draw a ladybird.",
        },
        new TracingTemplate
        {
            Name = "Snail",
            ImageSource = "template_snail.png",
            Description = "Draw a snail.",
        },
        new TracingTemplate
        {
            Name = "Worm",
            ImageSource = "template_worm.png",
            Description = "Draw a worm.",
        },
    ];

    private int _index;

    public MainPage()
    {
        InitializeComponent();
        ShowTemplate(0);
        TracingCanvas.StrokesChanged += (_, _) => UpdateButtons();
        Palette.ColorSelected += OnColorSelected;
        ApplyColor(Palette.SelectedColor);
    }

    private void OnColorSelected(object? sender, Color color) => ApplyColor(color);

    private void ApplyColor(Color color)
    {
        TracingCanvas.StrokeColor = color;
        ColorLabel.Text = $"Pen colour: {Palette.SelectedName}";
    }

    private void OnNextClicked(object? sender, EventArgs e)
    {
        ShowTemplate((_index + 1) % _templates.Count);
    }

    private void OnUndoClicked(object? sender, EventArgs e) => TracingCanvas.Undo();

    private void OnClearClicked(object? sender, EventArgs e) => TracingCanvas.Clear();

    private void ShowTemplate(int index)
    {
        _index = index;
        var template = _templates[index];
        TemplateImage.Source = template.ImageSource;
        TemplateLabel.Text = template.Description;
        TracingCanvas.Clear();
        UpdateButtons();
    }

    private void UpdateButtons()
    {
        var hasStrokes = TracingCanvas.CanUndo;
        UndoButton.IsEnabled = hasStrokes;
        ClearButton.IsEnabled = hasStrokes;
    }
}
