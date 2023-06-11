namespace Loupedeck.HWInfoPlugin
{
    using System;
    using System.Diagnostics;

    // This class implements an example command that counts button presses.

    public class ProcessorUsageCommand : PluginDynamicCommand
    {
        private readonly PerformanceCounter _perfCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
        private readonly System.Timers.Timer _timer;

        // Initializes the command class.
        public ProcessorUsageCommand()
            : base(displayName: "Processor", description: "Display processor usage", groupName: "Stats")
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
            $"CPU\n{this._perfCounter.NextValue().ToString("F2")}%";
    }
}
