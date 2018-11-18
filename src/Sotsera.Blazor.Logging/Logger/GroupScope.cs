﻿using System;

namespace Sotsera.Blazor.Logging.Logger
{
    internal class GroupScope: IDisposable
    {
        private bool Ignored { get; }
        private bool HasBeenShown { get; set; }

        private Logger Logger { get; }
        private string Label { get; }
        public GroupScope Parent { get; }

        public GroupScope(Logger logger, string label, GroupScope parent)
        {
            Logger = logger;
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
            if (Ignored || !HasBeenShown) return;
            Logger.LogGroupEnd(Label);
        }
    }
}
