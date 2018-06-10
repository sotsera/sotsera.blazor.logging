(function (browserConsole, blazor) {
    "use strict";
    
    var console = {};
    console.log = browserConsole.log || function () { };
    /*
        Chrome hides these levels by default and allowing them many other messages appear on the console
        console.Trace = browserConsole.trace || console.log;
        console.Debug = browserConsole.debug || console.log;
    */
    console.Trace = console.log;
    console.Debug = console.log;
    console.Information = browserConsole.info || console.log;
    console.Warning = browserConsole.warn || console.log;
    console.Error = browserConsole.error || console.log;
    console.Critical = browserConsole.error || console.log;
    
    blazor.registerFunction("sotsera.blazor.log", function (level, message) {
        console[level](message);
        return true;
    });

})(console || {}, Blazor);

