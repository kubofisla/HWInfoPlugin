namespace Loupedeck.HWInfoPlugin
{
    using System;
    using System.Diagnostics;
    using System.Globalization;

    // This class implements an example command that counts button presses.

    public class MemoryUsageCommand : PluginDynamicCommand
    {
        private readonly PerformanceCounter _perfCounter = new PerformanceCounter("Memory", "Available MBytes");
        private readonly System.Timers.Timer _timer;

        // Initializes the command class.
        public MemoryUsageCommand()
            : base(displayName: "Memory", description: "Display memory usage in MB", groupName: "Stats")
        {
            // Initialize timer to refresh the button icon every second
            this._timer = new System.Timers.Timer(1000);
            this._timer.Elapsed += (sender, e) =>
            {
                this.ActionImageChanged();
            };
            this._timer.AutoReset = true;
            this._timer.Start();
        }

        // This method is called when Loupedeck needs to show the command on the console or the UI.
        protected override String GetCommandDisplayName(String actionParameter, PluginImageSize imageSize) =>
            $"Free RAM\n{this._perfCounter.NextValue().ToString("N0", CultureInfo.CreateSpecificCulture("cs-CZ"))} MB";
    }
}
