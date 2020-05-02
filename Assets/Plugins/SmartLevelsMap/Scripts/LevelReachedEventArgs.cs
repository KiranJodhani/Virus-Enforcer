using System;

namespace Assets.Plugins.SmartLevelsMap.Scripts
{
    public class LevelReachedEventArgs : EventArgs
    {
        public int Number { get; private set; }

        public LevelReachedEventArgs(int number)
        {
            Number = number;
        }
    }
}
