using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using AutoRoids.Common.Static;

namespace AutoRoids.UserFormCode
{
    internal class clsTextInput
    {
        internal Boolean IsDouble(string strName)
        {
            List<String> lstValue = new List<string> { "txtRockMinDistance", "txtRockMaxDistance",
                                                       "txtRockMinRotation", "txtRockMaxRotation",
                                                       "txtRockAngle", "txtShipRotation",
                                                       "txtShipThrust", "txtGameScale" };

            if (lstValue.Contains(strName))
                return true;
            return false;
        }

        internal Boolean DisableZero(string strName)
        {
            List<String> lstValue = new List<string> { "txtShipThrust", "txtGameScale" };

            if (lstValue.Contains(strName))
                return true;
            return false;
        }


        internal string FilterDecimal(string strText)
        {
            if (strText == ".")
                strText = "0.";

            return strText;
        }

        internal bool IsValidNumber(string strName, string text)
        {
            if (text.Length == 0)
                return true;

            if (IsDouble(strName))
            {

                if (double.TryParse(text, out double dblValue))
                {
                    if (DisableZero(strName))
                    {
                        if (dblValue != 0)
                            return true;
                    }
                    else
                        return true;
                }
            }
            else
            {
                if (int.TryParse(text, out int intValue))
                {
                    if (DisableZero(strName))
                    {
                        if (intValue != 0)
                            return true;
                    }
                    else
                        return true;
                }
            }


            return false;
        }

        internal void SaveData(String strName, string strValue)
        {
            clsReg clsReg = new clsReg();

            switch (strName)
            {
                case "txtRockCount":
                    clsReg.SetRockCount(strValue);
                    break;

                case "txtRockAngle":
                    clsReg.SetRockAngle(strValue);
                    break;

                case "txtRockMinDistance":
                    clsReg.SetRockMinDistance(strValue);
                    break;

                case "txtRockMaxDistance":
                    clsReg.SetRockMaxDistance(strValue);
                    break;

                case "txtRockMinRotation":
                    clsReg.SetRockMinRotation(strValue);
                    break;

                case "txtRockMaxRotation":
                    clsReg.SetRockMaxRotation(strValue);
                    break;

                case "txtShipRotation":
                    clsReg.SetShipRotation(strValue);
                    break;

                case "txtShipThrust":
                    clsReg.SetShipThrust(strValue);
                    break;

                case "txtGameScale":
                    clsReg.SetGameScale(strValue);
                    break;

                case "txtIdleDelay":
                    clsReg.SetIdleDelay(strValue);
                    break;

                case "txtBulletDelay":
                    clsReg.SetBulletDelay(strValue);
                    break;

                case "txtDeathBlossomDelay":
                    clsReg.SetDeathBlossomDelay(strValue);
                    break;

                default:
                    break;
            }

            StaticRock.SetStaticRegistry(false);
        }

        internal string GetData(String strName)
        {
            clsReg clsReg = new clsReg();

            double dblValue = 0;
            int intValue = 0;

            switch (strName)
            {
                case "txtRockCount":
                    return clsReg.GetRockCount(out intValue);

                case "txtRockAngle":
                    return clsReg.GetRockAngle(out dblValue);

                case "txtRockMinDistance":
                    return clsReg.GetRockMinDistance(out dblValue);

                case "txtRockMaxDistance":
                    return clsReg.GetRockMaxRotation(out dblValue);

                case "txtRockMinRotation":
                    return clsReg.GetRockMinRotation(out dblValue);

                case "txtRockMaxRotation":
                    return clsReg.GetRockMaxRotation(out dblValue);

                case "txtShipRotation":
                    return clsReg.GetShipRotation(out dblValue);

                case "txtShipThrust":
                    return clsReg.GetShipThrust(out dblValue);

                case "txtGameScale":
                    return clsReg.GetGameScale(out dblValue);

                case "txtIdleDelay":
                    return clsReg.GetIdleDelay(out intValue);

                case "txtBulletDelay":
                    return clsReg.GetBulletDelay(out intValue);

                case "txtDeathBlossomDelay":
                    return clsReg.GetDeathBlossomDelay(out intValue);

                default:
                    break;
            }

            return "";
        }

        internal string Up(string strValue)
        {
            strValue = double.TryParse(strValue, out double dblText) ? (dblText += 1.0).ToString() : "1";

            return strValue;
        }

        internal string Down(string strValue)
        {
            if (double.TryParse(strValue, out double dblText))
            {
                if (dblText > 0)
                {
                    dblText -= 1.0;

                    if (dblText < 0)
                        dblText = 0;

                    strValue = dblText.ToString();
                }
            }
            else
                strValue = "1";

            return strValue;
        }

        internal bool IsNumeric(string text)
        {
            // Use a regular expression to check if the input is numeric
            Regex regex = new Regex("[^0-9]+");
            return !regex.IsMatch(text);
        }

        internal void UpdateChecked(Boolean bolIsInitalized, object sender)
        {
            if (!bolIsInitalized) return;

            CheckBox checkBox = (CheckBox)sender;

            clsReg clsReg = new clsReg();

            if (checkBox.Name == "chkBoundingBox")
                clsReg.SetShowBoundingBox((bool)checkBox.IsChecked);

            if (checkBox.Name == "chkBlocks")
                clsReg.SetShowBlocks((bool)checkBox.IsChecked);

            if (checkBox.Name == "chkOverlay")
                clsReg.SetShowOverlay((bool)checkBox.IsChecked);

            if (checkBox.Name == "chkZoomToShip")
                clsReg.SetZoomToShip((bool)checkBox.IsChecked);

            StaticRock.SetStaticRegistry(false);
        }
    }
}