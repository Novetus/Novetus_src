using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Novetus.Core
{
    #region Settings
    public class Settings
    {
        public enum Mode
        {
            Automatic = 0,
            OpenGLStable = 1,
            OpenGLExperimental = 2,
            DirectX = 3
        }

        public enum Level
        {
            Automatic = 0,
            VeryLow = 1,
            Low = 2,
            Medium = 3,
            High = 4,
            Ultra = 5,
            Custom = 6
        }

        public enum Style
        {
            None = 0,
            Extended = 1,
            Compact = 2,
            Stylish = 3
        }
    }

    #endregion
}
