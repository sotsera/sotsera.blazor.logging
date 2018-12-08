using System;

namespace Sotsera.Blazor.Logging.Logger
{
    internal class GroupScope: IDisposable
    {
        private bool Ignored { get; }
        private bool HasBeenShown { get; set; }

        private Logger Logger { get; }
        public LogManager LogManager { get; }
        private string Label { get; }
        public GroupScope Parent { get; }

        public GroupScope(Logger logger, LogManager logManager, string label, GroupScope parent)
        {
            Logger = logger;
            LogManager = logManager;
            Label = label;
            Parent = parent;
            Ignored = string.IsNullOrWhiteSpace(label);
        }

        public void EnsureHasBeenShown()
        {
            if (HasBeenShown) return;
            HasBeenShown = true;

            Parent?.EnsureHasBeenShown();

            if (!Ignored) Logger.Log("Group", Label);
        }

        public void Dispose()
        {
            LogManager.CurrentScope = Parent;
            if (Ignored || !HasBeenShown) return;
            Logger.LogGroupEnd(Label);
        }
    }
}
