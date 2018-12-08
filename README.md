# Sotsera.Blazor.Logging

Blazor interop for the browser's console APIs.

It is a very simple ILogger implementation tied with a custom configuration source to allow the minimum LogLevel to be changed at runtime.

## Changes

- version 0.7.2
  - fix for the parent scope management. **NOT** suitable for parallel job executions
- version 0.7.1
  - workaround for the group scopes chain across async requests. **NOT** suitable for parallel job executions
- version 0.7.0
  - updated to Blazor v0.7.0
  - ILogLevelManager renamed to ILogManager **(breaking change)**
    - ILogManager exposes an event with the logged message (useful on mobile when the console is not available)
- version 0.6.1
  - ability to map ILogger scopes to console's groups: a log message will be added to the last group scope created regardles of the thread context. **MUST** investigate a solution for handling parallel jobs

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
@inject ILogManager manager

<select bind="manager.CurrentLevelName">
    @foreach (var level in manager.ValidLogLevels)
    {
        <option value=@level>@level</option>
    }
</select>
```