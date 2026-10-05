# Kids Tracing App

A cross-platform [.NET MAUI](https://learn.microsoft.com/dotnet/maui/) app that teaches
young children to trace simple shapes. A worksheet image is shown behind a transparent
drawing surface; the child drags a finger (or mouse) to trace the dashed guide lines, and
can clear or undo their work when finished.

Built with **C#** and **.NET 10**.

## Features

- Pick from six EYFS pencil-control worksheets (bee, butterfly, caterpillar,
  ladybird, snail, worm).
- Trace freely over the guide lines with a finger, stylus, or mouse.
- **Undo** removes the last stroke; **Clear** wipes the whole canvas.
- Strokes stay inside the canvas and are drawn with a rounded, child-friendly pen.

## Project layout

```
TracingApp/
  Controls/TracingCanvasView.cs   # GraphicsView that records and renders freehand strokes
  Models/Stroke.cs                # A single stroke (color, thickness, points)
  Models/TracingTemplate.cs       # A worksheet definition
  Resources/Images/*.png          # Tracing worksheet images (edit or replace these)
  MainPage.xaml(.cs)              # UI: template image + tracing canvas + buttons
Worksheets/*.pdf                  # Original EYFS worksheets the images came from
```

## Images

The worksheet images live in `TracingApp/Resources/Images/` as PNG files and are
referenced by file name from `MainPage.xaml.cs` (`template_bee.png`,
`template_butterfly.png`, `template_caterpillar.png`, `template_ladybird.png`,
`template_snail.png`, `template_worm.png`). They were rendered from the EYFS pencil-control
PDFs in `Worksheets/` at high resolution and downscaled for mobile.

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
