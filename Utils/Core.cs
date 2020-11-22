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

    internal static class Core
    {

        public static void RunPlugin()

        {

            Logger.DebugLog("Core Plugin Function Started");

            //Game loop
            while (true)
            {
                GameFiber.Yield();

                if ((Game.IsKeyDownRightNow(Global.Controls.EnterPassengerModifier) && Game.IsKeyDown(Global.Controls.EnterPassenger) || Global.Controls.EnterPassengerModifier == Keys.None && Game.IsKeyDown(Global.Controls.EnterPassenger) || Game.IsControllerButtonDownRightNow(Global.Controls.EnterPassengerControllerModifier) && Game.IsControllerButtonDown(Global.Controls.EnterPassengerController) || Global.Controls.EnterPassengerControllerModifier == ControllerButtons.None && Game.IsControllerButtonDown(Global.Controls.EnterPassengerController)))
                 {
                        UpdatePlayer();
                        Game.LocalPlayer.Character.Tasks.Clear();

                    if (Game.LocalPlayer.Character.CurrentVehicle == null)
                    {
                        EnterPassenger();

                    }
 
                }
            }

        }

        private static void UpdatePlayer()
        {
            if (Game.LocalPlayer.Character)
            {
                
                Global.Dynamics.NearestVehicle = Game.LocalPlayer.Character.GetNearbyVehicles(1);

            }
        }

        private static void EnterPassenger()
        {

            if (Global.Dynamics.NearestVehicle[0] != null)
            {
            
             Game.LocalPlayer.Character.Tasks.EnterVehicle(Global.Dynamics.NearestVehicle[0], 5000, 2, EnterVehicleFlags.None).WaitForCompletion(1);
             GameFiber.Sleep(10000);
             Game.LocalPlayer.Character.Tasks.Clear();

            }
        }
    }

}

