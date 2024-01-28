#region Usings
using System.Runtime.InteropServices;
#endregion

namespace Novetus.Core
{
    #region Discord RPC
    //code by discord obv. just renamed it to fit better.
    //TODO: add proper c# implementation.
    public class IDiscordRPC
    {
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void ReadyCallback();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void DisconnectedCallback(int errorCode, string message);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void ErrorCallback(int errorCode, string message);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void JoinCallback(string secret);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void SpectateCallback(string secret);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void RequestCallback(JoinRequest request);

        public struct EventHandlers
        {
            public ReadyCallback readyCallback;
            public DisconnectedCallback disconnectedCallback;
            public ErrorCallback errorCallback;
            public JoinCallback joinCallback;
            public SpectateCallback spectateCallback;
            public RequestCallback requestCallback;
        }

        [System.Serializable]
        public struct RichPresence
        {
            public string state;
            /* max 128 bytes */
            public string details;
            /* max 128 bytes */
            public long startTimestamp;
            public long endTimestamp;
            public string largeImageKey;
            /* max 32 bytes */
            public string largeImageText;
            /* max 128 bytes */
            public string smallImageKey;
            /* max 32 bytes */
            public string smallImageText;
            /* max 128 bytes */
            public string partyId;
            /* max 128 bytes */
            public int partySize;
            public int partyMax;
            public string matchSecret;
            /* max 128 bytes */
            public string joinSecret;
            /* max 128 bytes */
            public string spectateSecret;
            /* max 128 bytes */
            public bool instance;
        }

        [System.Serializable]
        public struct JoinRequest
        {
            public string userId;
            public string username;
            public string avatar;
        }

        public enum Reply
        {
            No = 0,
            Yes = 1,
            Ignore = 2
        }
        [DllImport("discord-rpc", EntryPoint = "Discord_Initialize", CallingConvention = CallingConvention.Cdecl)]
        public static extern void Initialize(string applicationId, ref EventHandlers handlers, bool autoRegister, string optionalSteamId);

        [DllImport("discord-rpc", EntryPoint = "Discord_Shutdown", CallingConvention = CallingConvention.Cdecl)]
        public static extern void Shutdown();

        [DllImport("discord-rpc", EntryPoint = "Discord_RunCallbacks", CallingConvention = CallingConvention.Cdecl)]
        public static extern void RunCallbacks();

        [DllImport("discord-rpc", EntryPoint = "Discord_UpdatePresence", CallingConvention = CallingConvention.Cdecl)]
        public static extern void UpdatePresence(ref RichPresence presence);

        [DllImport("discord-rpc", EntryPoint = "Discord_Respond", CallingConvention = CallingConvention.Cdecl)]
        public static extern void Respond(string userId, Reply reply);
    }

    public class DiscordRPC
    {
        public static void ReadyCallback()
        {
            Util.ConsolePrint("Discord RPC: Ready", 3);
        }

        public static void DisconnectedCallback(int errorCode, string message)
        {
            Util.ConsolePrint("Discord RPC: Disconnected. Reason - " + errorCode + ": " + message, 2);
        }

        public static void ErrorCallback(int errorCode, string message)
        {
            Util.ConsolePrint("Discord RPC: Error. Reason - " + errorCode + ": " + message, 2);
        }

        public static void JoinCallback(string secret)
        {
        }

        public static void SpectateCallback(string secret)
        {
        }

        public static void RequestCallback(IDiscordRPC.JoinRequest request)
        {
        }

        public static void StartDiscord()
        {
            if (GlobalVars.UserConfiguration.ReadSettingBool("DiscordRichPresence"))
            {
                GlobalVars.handlers = new IDiscordRPC.EventHandlers();
                GlobalVars.handlers.readyCallback = ReadyCallback;
                GlobalVars.handlers.disconnectedCallback += DisconnectedCallback;
                GlobalVars.handlers.errorCallback += ErrorCallback;
                GlobalVars.handlers.joinCallback += JoinCallback;
                GlobalVars.handlers.spectateCallback += SpectateCallback;
                GlobalVars.handlers.requestCallback += RequestCallback;
                IDiscordRPC.Initialize(GlobalVars.appid, ref GlobalVars.handlers, true, "");
                Util.ConsolePrint("Discord RPC: Initalized", 3);

#if URI
                Client.UpdateRichPresence(GlobalVars.LauncherState.LoadingURI, true);
#else
                Client.UpdateRichPresence(Client.GetStateForType(GlobalVars.GameOpened), true);
#endif
            }
        }
    }
#endregion
}
