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
    using Rage.Native;

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

                    if (Game.LocalPlayer.Character.CurrentVehicle == null)
                    {
                        EnterPassenger();

                    } else if (Global.Dynamics.NearestVehicle[0].GetPedOnSeat(-1) != Game.LocalPlayer.Character && Global.Dynamics.NearestVehicle[0].GetPedOnSeat(-1) == null)
                    {

                        Ped SecurityDriver = new Ped(0xeb51d959, Game.LocalPlayer.Character.GetOffsetPositionRight(500), 223.9016f); // tbd change character model to Security XY
                        SecurityDriver.SetVariation(3, 2, 0); // Top
                        SecurityDriver.SetVariation(4, 2, 0); // Trousers
                        SecurityDriver.SetVariation(6, 2, 0); // Shoes
                        SecurityDriver.SetVariation(8, 2, 0); // Badge
                        SecurityDriver.SetVariation(9, 1, 0); // Holster
                        SecurityDriver.SetVariation(11, 1, 0); // Underwear


                        NativeFunction.Natives.SET_PED_ARMOUR(SecurityDriver, 100);
                        NativeFunction.Natives.SET_PED_ACCURACY(SecurityDriver, 100);
                        SecurityDriver.Inventory.GiveNewWeapon("WEAPON_PISTOL50", 56, false);

                        SecurityDriver.Tasks.EnterVehicle(Global.Dynamics.NearestVehicle[0], 10000, -1, EnterVehicleFlags.None).WaitForCompletion(10);
                        SecurityDriver.IsPersistent = true;

                    }
 
                }

                if ((Game.IsKeyDownRightNow(Global.Controls.DriveToMarkerModifier) && Game.IsKeyDown(Global.Controls.DriveToMarker) || Global.Controls.DriveToMarkerModifier == Keys.None && Game.IsKeyDown(Global.Controls.DriveToMarker) || Game.IsControllerButtonDownRightNow(Global.Controls.DriveToMarkerControllerModifier) && Game.IsControllerButtonDown(Global.Controls.DriveToMarkerController) || Global.Controls.DriveToMarkerControllerModifier == ControllerButtons.None && Game.IsControllerButtonDown(Global.Controls.DriveToMarkerController)))
                {
                    //tbd - check if vehicle has driver, then task him to drive to marker, else do nothing
                    // tbd - check if marker is present, else do nothing. 
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

                if (Global.Dynamics.NearestVehicle[0].PassengerCapacity <= 2)
                {

                    Game.LocalPlayer.Character.Tasks.EnterVehicle(Global.Dynamics.NearestVehicle[0], 5000, -2, EnterVehicleFlags.None).WaitForCompletion(1);
                    GameFiber.Sleep(10000);
                    Game.LocalPlayer.Character.Tasks.Clear();

                } else
                {
                    if (Global.Dynamics.NearestVehicle[0].GetPedOnSeat(2) == null)
                    {
                        Game.LocalPlayer.Character.Tasks.EnterVehicle(Global.Dynamics.NearestVehicle[0], 5000, 2, EnterVehicleFlags.None).WaitForCompletion(1);
                        GameFiber.Sleep(10000);
                        Game.LocalPlayer.Character.Tasks.Clear();

                    } else
                    {
                        Game.LocalPlayer.Character.Tasks.EnterVehicle(Global.Dynamics.NearestVehicle[0], 5000, -2, EnterVehicleFlags.None).WaitForCompletion(1);
                        GameFiber.Sleep(10000);
                        Game.LocalPlayer.Character.Tasks.Clear();
                    }

                }
            }
        }
    }

}

