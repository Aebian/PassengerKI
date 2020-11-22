/*
 *	Developed By: Aebian
 *	Name: PassengerKI
 *	Dependent: Rage Plugin Hook
 *	Released On: GitHub
 */

namespace PassengerKI.Utils
{
    using System.Windows.Forms;
    using Rage;

    internal static class Global
    {
        internal static class Application
        {
            public static string CurrentVersion { get; set; }
            public static string LatestVersion { get; set; }
            public static bool DebugLogging { get; set; }
            public static string ConfigPath { get; set; }
        }

        internal static class Controls
        {
            public static Keys EnterPassenger { get; set; }
            public static Keys EnterPassengerModifier { get; set; }
            public static ControllerButtons EnterPassengerController { get; set; }
            public static ControllerButtons EnterPassengerControllerModifier { get; set; }
        }

        internal static class Dynamics
        {
            public static Vehicle[] NearestVehicle { get; set; }
        }
    }
}
