/*
 *	Developed By: Aebian
 *	Name: PassengerKI
 *	Dependent: Rage Plugin Hook
 *	Released On: GitHub
 */


namespace PassengerKI.Utils
{
    using System;
    using System.ComponentModel;
    using System.Windows.Forms;
    using Rage;

    internal static class Config
    {
        private static InitializationFile initialiseFile(string filepath)
        {
            InitializationFile ini = new InitializationFile(filepath);
            ini.Create();
            return ini;
        }

        public static void LoadConfig()
        {
            InitializationFile settings = initialiseFile(Global.Application.ConfigPath + "PassengerKI.ini");
            Logger.DebugLog("General Config Loading Started.");

            Global.Application.DebugLogging = (settings.ReadBoolean("General", "DebugLogging", false));
            KeysConverter KeyCV = new KeysConverter();

            string EnterPassenger, EnterPassengerModifier, EnterPassengerController, EnterPassengerControllerModifier, DriveToMarker, DriveToMarkerModifier, DriveToMarkerController, DriveToMarkerControllerModifier;

            // Fetch settings from file / set default values
            EnterPassenger = settings.ReadString("Keybinds", "EnterPassenger", "E");
            EnterPassengerModifier = settings.ReadString("Keybinds", "EnterPassengerModifier", "None");

            EnterPassengerController = settings.ReadString("Keybinds", "EnterPassengerController", "Y");
            EnterPassengerControllerModifier = settings.ReadString("Keybinds", "EnterPassengerControllerModifier", "DPadLeft");

            DriveToMarker = settings.ReadString("Keybinds", "DriveToMarker", "Space");
            DriveToMarkerModifier = settings.ReadString("Keybinds", "DriveToMarkerModifier", "None");

            DriveToMarkerController = settings.ReadString("Keybinds", "DriveToMarkerController", "X");
            DriveToMarkerControllerModifier = settings.ReadString("Keybinds", "DriveToMarkerControllerModifier", "DPadLeft");

            // Assign Keyboard Buttons to Global Variable
            Global.Controls.EnterPassenger = (Keys)KeyCV.ConvertFromString(EnterPassenger);
            Global.Controls.EnterPassengerModifier = (Keys)KeyCV.ConvertFromString(EnterPassengerModifier);

            Global.Controls.DriveToMarker = (Keys)KeyCV.ConvertFromString(DriveToMarker);
            Global.Controls.DriveToMarkerModifier = (Keys)KeyCV.ConvertFromString(DriveToMarkerModifier);

            // Convert Controller Buttons to the right format
            TypeConverter typeConverter = TypeDescriptor.GetConverter(Global.Controls.EnterPassengerController); // get type of variable
            ControllerButtons EPController = (ControllerButtons)typeConverter.ConvertFromString(EnterPassengerController);
            ControllerButtons EPControllerModifier = (ControllerButtons)typeConverter.ConvertFromString(EnterPassengerControllerModifier);

            ControllerButtons DMController = (ControllerButtons)typeConverter.ConvertFromString(DriveToMarkerController);
            ControllerButtons DMControllerModifier = (ControllerButtons)typeConverter.ConvertFromString(DriveToMarkerControllerModifier);

            // Assign Controller Buttons to Global Variable
            Global.Controls.EnterPassengerController = EPController;
            Global.Controls.EnterPassengerControllerModifier = EPControllerModifier;

            Global.Controls.DriveToMarkerController = DMController;
            Global.Controls.DriveToMarkerControllerModifier = DMControllerModifier;

            Logger.DebugLog("General Config Loading Finished."); 
        }
    }
}