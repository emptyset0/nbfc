﻿using StagWare.FanControl.Plugins;
using StagWare.Hardware;
using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace StagWare.Plugins.Generic
{
    [Export(typeof(ITemperatureMonitor))]
    [FanControlPluginMetadata(
        "StagWare.Plugins.SensorTemperatureMonitor",
        SupportedPlatforms.Windows | SupportedPlatforms.Unix,
        SupportedCpuArchitectures.x86 | SupportedCpuArchitectures.x64)]
    public class SensorTemperatureMonitor : ITemperatureMonitor
    {
        #region Private Fields

        private HardwareMonitor hwMon;

        #endregion

        #region ITemperatureMonitor implementation

        public bool IsInitialized
        {
            get;
            private set;
        }

        public void Initialize()
        {
            if (!this.IsInitialized)
            {
                this.IsInitialized = true;
                this.hwMon = HardwareMonitor.Instance;
            }
        }

        public double GetCpuTemperature()
        {
            KeyValuePair<string, double>[] temps = this.hwMon.CpuTemperatures;

            double temperature = 0;

            foreach (KeyValuePair<string, double> pair in temps)
            {
                temperature += pair.Value;
            }

            return temperature / temps.Length;
        }

        public double GetGpuTemperature()
        {
            KeyValuePair<string, double>[] temps = this.hwMon.GpuTemperatures;

            if (temps.Length == 0)
                return -273.15;

            double temperature = 0;
 
            foreach (KeyValuePair<string, double> pair in temps)
            {
                temperature += pair.Value;
            }

            return temperature / temps.Length;
        }

        public void Dispose()
        {
            // nothing to dispose
        }

        #endregion
    }
}
