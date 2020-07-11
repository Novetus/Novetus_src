#region Settings
public class Settings
{
    #region Graphics Options

    public class GraphicsOptions
    {
        public enum Mode
        {
            None = 0,
            OpenGL = 1,
            DirectX = 2
        }

        public enum Level
        {
            VeryLow = 1,
            Low = 2,
            Medium = 3,
            High = 4,
            Ultra = 5
        }

        public static Mode GetModeForInt(int level)
        {
            switch (level)
            {
                case 1:
                    return Mode.OpenGL;
                case 2:
                    return Mode.DirectX;
                default:
                    return Mode.None;
            }
        }

        public static int GetIntForMode(Mode level)
        {
            switch (level)
            {
                case Mode.OpenGL:
                    return 1;
                case Mode.DirectX:
                    return 2;
                default:
                    return 0;
            }
        }

        public static Level GetLevelForInt(int level)
        {
            switch (level)
            {
                case 1:
                    return Level.VeryLow;
                case 2:
                    return Level.Low;
                case 3:
                    return Level.Medium;
                case 4:
                    return Level.High;
                case 5:
                default:
                    return Level.Ultra;
            }
        }

        public static int GetIntForLevel(Level level)
        {
            switch (level)
            {
                case Level.VeryLow:
                    return 1;
                case Level.Low:
                    return 2;
                case Level.Medium:
                    return 3;
                case Level.High:
                    return 4;
                case Level.Ultra:
                default:
                    return 5;
            }
        }
    }
    #endregion

    #region UI Options
    public static class UIOptions
    {
        public enum Style
        {
            None = 0,
            Extended = 1,
            Compact = 2
        }

        public static Style GetStyleForInt(int level)
        {
            switch (level)
            {
                case 1:
                    return Style.Extended;
                case 2:
                    return Style.Compact;
                default:
                    return Style.None;
            }
        }

        public static int GetIntForStyle(Style level)
        {
            switch (level)
            {
                case Style.Extended:
                    return 1;
                case Style.Compact:
                    return 2;
                default:
                    return 0;
            }
        }
    }
    #endregion
}
#endregion
