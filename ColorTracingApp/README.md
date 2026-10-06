# Kids Color Tracing App

A cross-platform [.NET MAUI](https://learn.microsoft.com/dotnet/maui/) app that teaches
young children to trace simple shapes **in colour**. It is built from the same base as the
`TracingApp` project, adding a colour palette so the child can pick the pen colour before
tracing.

Built with **C#** and **.NET 10**.

## Features

- Pick from six EYFS pencil-control worksheets (bee, butterfly, caterpillar,
  ladybird, snail, worm).
- **Choose a pen colour** from a row of tappable swatches (red, orange, yellow,
  green, teal, blue, purple, pink, brown, black).
- Trace freely over the guide lines with a finger, stylus, or mouse.
- **Undo** removes the last stroke; **Clear** wipes the whole canvas.
- Each stroke keeps the colour that was selected when it was drawn, so a picture
  can use several colours at once.

## Project layout

```
ColorTracingApp/
  Controls/TracingCanvasView.cs   # GraphicsView that records and renders freehand strokes
  Controls/ColorPaletteView.cs    # Horizontal row of selectable colour swatches
  Models/Stroke.cs                # A single stroke (color, thickness, points)
  Models/TracingTemplate.cs       # A worksheet definition
  Models/PaletteColor.cs          # A named pen colour and its selected state
  Resources/Images/*.png          # Tracing worksheet images (edit or replace these)
  MainPage.xaml(.cs)              # UI: worksheet + tracing canvas + palette + buttons
```

## Images

The worksheet images live in `Resources/Images/` as PNG files and are referenced by file
name from `MainPage.xaml.cs`. They were rendered from the EYFS pencil-control PDFs kept in
the repository's `Worksheets/` folder.

## Build and run

Install the [.NET 10 SDK](https://dotnet.microsoft.com/download) and the MAUI workload:

```bash
dotnet workload install maui
```

Then build or run for a target platform, for example Android:

```bash
dotnet build ColorTracingApp/ColorTracingApp.csproj -f net10.0-android
```
