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
                        Logger.DebugLog("Enter Passenger Method called.");

                    } else if (Global.Dynamics.NearestVehicle[0].GetPedOnSeat(-1) != Game.LocalPlayer.Character && Global.Dynamics.NearestVehicle[0].GetPedOnSeat(-1) == null)
                    {

                        Global.Dynamics.SecurityDriver = new Ped(0xeb51d959, Game.LocalPlayer.Character.GetOffsetPositionRight(40), 223.9016f); // tbd change character model to Security XY
                        Global.Dynamics.SecurityDriver.SetVariation(3, 2, 0); // Top
                        Global.Dynamics.SecurityDriver.SetVariation(4, 2, 0); // Trousers
                        Global.Dynamics.SecurityDriver.SetVariation(6, 2, 0); // Shoes
                        Global.Dynamics.SecurityDriver.SetVariation(8, 2, 0); // Badge
                        Global.Dynamics.SecurityDriver.SetVariation(9, 1, 0); // Holster
                        Global.Dynamics.SecurityDriver.SetVariation(11, 1, 0); // Underwear


                        NativeFunction.Natives.SET_PED_ARMOUR(Global.Dynamics.SecurityDriver, 100);
                        NativeFunction.Natives.SET_PED_ACCURACY(Global.Dynamics.SecurityDriver, 100);
                        Global.Dynamics.SecurityDriver.Inventory.GiveNewWeapon("WEAPON_PISTOL50", 56, false);

                        Global.Dynamics.SecurityDriver.Tasks.EnterVehicle(Global.Dynamics.NearestVehicle[0], 40000, -1, EnterVehicleFlags.None);
                        Global.Dynamics.SecurityDriver.IsPersistent = true;

                        Logger.DebugLog("Driver spawned.");

                    } else
                    {
                        Global.Dynamics.SecurityDriver.IsPersistent = false;
                        Global.Dynamics.SecurityDriver.Tasks.LeaveVehicle(Global.Dynamics.NearestVehicle[0], LeaveVehicleFlags.None).WaitForCompletion(6000);
                        Global.Dynamics.SecurityDriver.Tasks.GoStraightToPosition(new Vector3(2510.524f, -341.8503f, 118.1864f), 4, 220, 100, 1000).WaitForCompletion(30000);
                        Global.Dynamics.SecurityDriver.Delete();
                    }
 
                }

                if ((Game.IsKeyDownRightNow(Global.Controls.DriveToMarkerModifier) && Game.IsKeyDown(Global.Controls.DriveToMarker) || Global.Controls.DriveToMarkerModifier == Keys.None && Game.IsKeyDown(Global.Controls.DriveToMarker) || Game.IsControllerButtonDownRightNow(Global.Controls.DriveToMarkerControllerModifier) && Game.IsControllerButtonDown(Global.Controls.DriveToMarkerController) || Global.Controls.DriveToMarkerControllerModifier == ControllerButtons.None && Game.IsControllerButtonDown(Global.Controls.DriveToMarkerController)))
                {

                 if (Global.Dynamics.NearestVehicle[0].GetPedOnSeat(-1) != Game.LocalPlayer.Character && Global.Dynamics.NearestVehicle[0].GetPedOnSeat(-1) != null)
                    {
                        UpdatePlayer();

                        if (World.GetWaypointBlip() != null)

                        {
                            Global.Dynamics.NearestVehicle[0].Driver.Tasks.DriveToPosition(World.GetNextPositionOnStreet(World.GetWaypointBlip().Position), 15f, VehicleDrivingFlags.FollowTraffic);
                        }

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

                if (Global.Dynamics.NearestVehicle[0].PassengerCapacity <= 2)
                {

                    Game.LocalPlayer.Character.Tasks.EnterVehicle(Global.Dynamics.NearestVehicle[0], 5000, -2, EnterVehicleFlags.None).WaitForCompletion(1);

                } else
                {
                    if (Global.Dynamics.NearestVehicle[0].GetPedOnSeat(2) == null)
                    {
                        Game.LocalPlayer.Character.Tasks.EnterVehicle(Global.Dynamics.NearestVehicle[0], 5000, 2, EnterVehicleFlags.None).WaitForCompletion(1);

                    } else
                    {
                        Game.LocalPlayer.Character.Tasks.EnterVehicle(Global.Dynamics.NearestVehicle[0], 5000, -2, EnterVehicleFlags.None).WaitForCompletion(1);
                    }

                }

                Logger.DebugLog("EnterPassenger Method loaded.");
            }
        }
    }

}

