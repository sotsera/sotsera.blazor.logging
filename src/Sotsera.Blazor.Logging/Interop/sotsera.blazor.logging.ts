namespace sotsera.blazor { 
    export module logging {
        const consoleLogger = !console ? (() => {}) : !console.log ? (() => {}) : console.log;
        /*
         * Chrome hides these trace and debug levels by default and allowing
         * them many other messages appear on the console
         */
        const logger: any = {
            Trace: consoleLogger,
            Debug: consoleLogger,
            Information: console.info || consoleLogger,
            Warning: console.warn || consoleLogger,
            Error: console.error || consoleLogger,
            Critical: console.error || consoleLogger,
            Group: console.group || consoleLogger,
            GroupEnd: console.groupEnd || consoleLogger
        }

        export function log(level: string, message: string): void {
            if (level === "GroupEnd") {
                logger[level]();
                return;
            }
            logger[level](message);
        }
    }
}