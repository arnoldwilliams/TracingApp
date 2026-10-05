using TracingApp.Models;

namespace TracingApp;

public partial class MainPage : ContentPage
{
    private readonly IReadOnlyList<TracingTemplate> _templates =
    [
        new TracingTemplate
        {
            Name = "Circle",
            ImageSource = "template_circle.png",
            Description = "Trace the big circle.",
        },
        new TracingTemplate
        {
            Name = "Star",
            ImageSource = "template_star.png",
            Description = "Trace the star.",
        },
        new TracingTemplate
        {
            Name = "House",
            ImageSource = "template_house.png",
            Description = "Trace the house.",
        },
        new TracingTemplate
        {
            Name = "Number 1",
            ImageSource = "template_number1.png",
            Description = "Trace the number 1.",
        },
    ];

    private int _index;

    public MainPage()
    {
        InitializeComponent();
        ShowTemplate(0);
        TracingCanvas.StrokesChanged += (_, _) => UpdateButtons();
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
