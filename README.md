# Kids Tracing Apps

A pair of cross-platform [.NET MAUI](https://learn.microsoft.com/dotnet/maui/) apps that
teach young children to trace simple shapes. A worksheet image is shown behind a
transparent drawing surface; the child drags a finger (or mouse) to trace the guide lines,
and can clear or undo their work when finished.

Built with **C#** and **.NET 10**.

| Project | Description |
| --- | --- |
| `TracingApp` | The base tracing app. |
| `ColorTracingApp` | Built on the same base, adding a colour palette so the child can pick the pen colour. |

## Features (ColorTracingApp)

- Pick from six EYFS pencil-control worksheets (bee, butterfly, caterpillar,
  ladybird, snail, worm).
- **Choose a pen colour** from a row of tappable swatches.
- Trace freely over the guide lines with a finger, stylus, or mouse.
- **Undo** removes the last stroke; **Clear** wipes the whole canvas.
- Each stroke keeps the colour it was drawn with, so a picture can mix colours.

## Project layout

```
TracingApp/
  Controls/TracingCanvasView.cs   # GraphicsView that records and renders freehand strokes
  Models/Stroke.cs                # A single stroke (color, thickness, points)
  Models/TracingTemplate.cs       # A worksheet definition
  Resources/Images/*.png          # Tracing worksheet images (edit or replace these)
  MainPage.xaml(.cs)              # UI: template image + tracing canvas + buttons
ColorTracingApp/                  # Same base plus:
  Controls/ColorPaletteView.cs    # Horizontal row of selectable colour swatches
  Models/PaletteColor.cs          # A named pen colour and its selected state
Worksheets/*.pdf                  # Original EYFS worksheets the images came from
```

## Images

The worksheet images live in each project's `Resources/Images/` as PNG files and are
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
dotnet build ColorTracingApp/ColorTracingApp.csproj -f net10.0-android
```

The Windows target (`net10.0-windows10.0.19041.0`) is added automatically when building on
Windows.
