# Kids Tracing App

A cross-platform [.NET MAUI](https://learn.microsoft.com/dotnet/maui/) app that teaches
young children to trace simple shapes. A worksheet image is shown behind a transparent
drawing surface; the child drags a finger (or mouse) to trace the dashed guide lines, and
can clear or undo their work when finished.

Built with **C#** and **.NET 10**.

## Features

- Pick from several tracing worksheets (circle, star, house, number 1).
- Trace freely over the guide lines with a finger, stylus, or mouse.
- **Undo** removes the last stroke; **Clear** wipes the whole canvas.
- Strokes stay inside the canvas and are drawn with a rounded, child-friendly pen.

## Project layout

```
TracingApp/
  Controls/TracingCanvasView.cs   # GraphicsView that records and renders freehand strokes
  Models/Stroke.cs                # A single stroke (color, thickness, points)
  Models/TracingTemplate.cs       # A worksheet definition
  Resources/Images/*.svg          # Tracing worksheet images (edit or replace these)
  MainPage.xaml(.cs)              # UI: template image + tracing canvas + buttons
```

## Images

The worksheet images live in `TracingApp/Resources/Images/` as SVG files and are
referenced by file name from `MainPage.xaml.cs` (`template_circle.svg`, `template_star.svg`,
`template_house.svg`, `template_number1.svg`). They are currently placeholder worksheets.
To use your own pictures, drop image files into that folder and update the
`TracingTemplate` list in `MainPage.xaml.cs` to point at the new file names.

## Build and run

Install the [.NET 10 SDK](https://dotnet.microsoft.com/download) and the MAUI workload:

```bash
dotnet workload install maui
```

Then build or run for a target platform, for example Android:

```bash
dotnet build TracingApp/TracingApp.csproj -f net10.0-android
```

The Windows target (`net10.0-windows10.0.19041.0`) is added automatically when building on
Windows.
