namespace TracingApp.Models;

/// <summary>
/// A tracing worksheet the child can trace over. <see cref="ImageSource"/> points at an
/// image bundled under Resources/Images.
/// </summary>
public sealed class TracingTemplate
{
    public required string Name { get; init; }

    public required string ImageSource { get; init; }

    public required string Description { get; init; }
}
