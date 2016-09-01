using System;
using MoneySharp.Contract.Settings;
using MoneySharp.Internal;

namespace MoneySharp
{
    public class Configurator
    {
        private Configurator()
        {
            Settings = new MoneyBirdSettings();
        }

        public static Configurator With => new Configurator();

        public Configurator ApplySettings(Action<ISettings> settingsProvider)
        {
            settingsProvider.Invoke(this.Settings);
            return this;
        }

        public ISettings Settings { get; set; }
    }
}