# Sotsera.Blazor.Logging

Blazor interop for the browser's console APIs.

It is a very simple ILogger implementation tied with a custom configuration source to allow the minimum LogLevel to be changed at runtime.

## Configuration

The *AddBlazorLogger* extension method allows the logger to be added to the dependency injection system specifying the minimum log level to be shown in the browser console.

```c#
public class Program
{
    static void Main(string[] args)
    {
        var serviceProvider = new BrowserServiceProvider(services =>
        {
            services.AddBlazorLogger(() =>
            {
                return new BrowserUriHelper().GetBaseUri().StartsWith("http://localhost")
                    ? LogLevel.Debug
                    : LogLevel.Warning;
            });
        });

        new BrowserRenderer(serviceProvider).AddComponent<App>("app");
    }
}
```

## Usage

```c#
@inject ILogger<Index> logger

@functions {
  protected override void OnInit()
  {
    logger.LogDebug("OnInit");
  }
}
```

## Changing the minimum log level

The following snippet shows how to control the log level with a select box:

```c#
@inject ILogLevelManager manager

<select bind="manager.CurrentLevelName">
    @foreach (var level in manager.ValidLogLevels)
    {
        <option value=@level>@level</option>
    }
</select>
```