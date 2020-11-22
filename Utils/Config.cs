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

            string AttachKey, AttachKeyModifier, EnterPassengerController, EnterPassengerControllerModifier;


            AttachKey = settings.ReadString("Keybinds", "EnterPassenger", "F");
            AttachKeyModifier = settings.ReadString("Keybinds", "EnterPassengerModifier", "None");

            EnterPassengerController = settings.ReadString("Keybinds", "EnterPassengerController", "Y");
            EnterPassengerControllerModifier = settings.ReadString("Keybinds", "EnterPassengerControllerModifier", "None");

            Global.Controls.EnterPassenger = (Keys)KeyCV.ConvertFromString(AttachKey);
            Global.Controls.EnterPassengerModifier = (Keys)KeyCV.ConvertFromString(AttachKeyModifier);


            TypeConverter typeConverter = TypeDescriptor.GetConverter(Global.Controls.EnterPassengerController);
            ControllerButtons CVController = (ControllerButtons)typeConverter.ConvertFromString(EnterPassengerController);
            ControllerButtons CVControllerModifier = (ControllerButtons)typeConverter.ConvertFromString(EnterPassengerControllerModifier);

            Global.Controls.EnterPassengerController = CVController;
            Global.Controls.EnterPassengerControllerModifier = CVControllerModifier;

            Logger.DebugLog("General Config Loading Finished."); 
        }
    }
}