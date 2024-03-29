﻿using System;
using CM.JsonTools;

namespace CM.ReformatJson.Processing
{
    public class ResformatJson
    {
        #region LocalVariables
        private string indentChars = "    ";
        private string colonChar = ":";
        private int indentDepth = 0;
        #endregion

        #region DeligateVariables
        JsonReader.DeligateMakeObject makeObject;
        JsonReader.DeligateCloseObject closeObject;
        JsonReader.DeligateMakeArray makeArray;
        JsonReader.DeligateCloseArray closeArray;
        JsonReader.DeligateSetBoolean setBoolean;
        JsonReader.DeligateSetDecimal setDecimal;
        JsonReader.DeligateSetDouble setDouble;
        JsonReader.DeligateSetInteger setInteger;
        JsonReader.DeligateSetLongInt setLongInt;
        JsonReader.DeligateSetString setString;
        #endregion

        #region Constructors
        /// <summary>
        /// Formats the Json to be more easy to read (indent is 4 characters).
        /// </summary>
        /// <param name="inpOriginalJson">Json to reformat.</param>
        /// <returns>A json string, with indents and character returns.</returns>
        public string MakeReadable(string inpOriginalJson)
        {
            string readableJson = MakeReadable(inpOriginalJson, "    ", ":");
            return readableJson;
        }
        /// <summary>
        /// Formats the Json to be more easy to read (you specify indent).
        /// </summary>
        /// <param name="inpOriginalJson">Json to reformat.</param>
        /// <param name="inpIndentChars">String to use as an indent.</param>
        /// <returns>A json string, with indents and character returns.</returns>
        public string MakeReadable(string inpOriginalJson, string inpIndentChars)
        {
            string readableJson = MakeReadable(inpOriginalJson, inpIndentChars, ":");
            return readableJson;
        }
        /// <summary>
        /// Formats the Json to be more easy to read (you specify indent).
        /// </summary>
        /// <param name="inpOriginalJson">Json to reformat.</param>
        /// <param name="inpIndentChars">String to use as an indent.</param>
        /// <param name="inpColonChar">String to use to separate name and value (instead of :).</param>
        /// <returns>A json string, with indents and character returns.</returns>
        public string MakeReadable(string inpOriginalJson, string inpIndentChars, string inpColonChar)
        {
            // Default the control variables.
            string readableJson = "";
            indentChars = inpIndentChars;
            colonChar = inpColonChar;

            // Setup the deligates.
            makeObject = MakeObject;
            closeObject = CloseObject;
            makeArray = MakeArray;
            closeArray = CloseArray;
            setBoolean = SetBoolean;
            setDecimal = SetDecimal;
            setInteger = SetInteger;
            setString = SetString;

            JsonReader reformatJson = new JsonReader(makeObject,
                                                     closeObject,
                                                     makeArray,
                                                     closeArray,
                                                     setBoolean,
                                                     setDecimal,
                                                     setInteger,
                                                     setString);
            readableJson = (string)reformatJson.ReadJson(inpOriginalJson, "");

            reformatJson.Dispose();

            return readableJson;
        }
        #endregion

        #region FormattingMethods
        /// <summary>
        /// Decide if we need to add a comma to the JSON string.
        /// </summary>
        /// <param name="inpTempString">JSON string that we're building.</param>
        /// <returns>inpTempString, with the comma added.</returns>
        private static string AddComma(string inpTempString)
        {
            // Don't add a comma if this is the first proeprty in the object or array.
            if (!inpTempString.EndsWith("{") &&
                !inpTempString.EndsWith("[") &&
                inpTempString != "")
            {
                inpTempString += ",";
            }
            return inpTempString;
        }
        /// <summary>
        /// Add the new line and build the indent to the appropriate deapth.
        /// </summary>
        /// <param name="inpIndentChars">String, the characters for a single indent (stored in indentChars).</param>
        /// <param name="inpIndentDepth">Int, how many indents for this line (stored in indentDepth).</param>
        /// <returns>The indent to add to the line.</returns>
        private static string AddIndent(string inpIndentChars, int inpIndentDepth)
        {
            // Add a new line before we do any indents.
            string indentString = System.Environment.NewLine;
            // If there are indents to do, build them up here.
            if (inpIndentDepth > 0)
            {
                for (int count = 1; count <= inpIndentDepth; count++)
                {
                    indentString += inpIndentChars;
                }
            }

            return indentString;
        }
        #endregion;

        #region DeligateMethods
        /// <summary>
        /// Open a group/class in the JSON string.
        /// </summary>
        /// <param name="inpName">Name of the property.</param>
        /// <param name="inpObject">The object to which we are adding the property.</param>
        /// <param name="inpPath">The path of item that is added.</param>
        /// <returns>The object with the property added.</returns>
        public object MakeObject(string inpName, object inpObject, string inpPath)
        {
            string tempString = (string)inpObject;

            // Add the comma (if appropriate) to the end of the previous line.
            tempString = AddComma(tempString);
            // Add the new line and indent.
            tempString += AddIndent(indentChars, indentDepth);
            tempString += inpName == "" ? "" : "\"" + inpName + "\"" + colonChar;
            tempString += "{";
            // Everything inside this object is ay a greater dealth.
            indentDepth += 1;

            return tempString;
        }
        /// <summary>
        /// Closes an group/class in the JSON string.
        /// </summary>
        /// <param name="inpName">Name of the property.</param>
        /// <param name="inpObject">The object to which we are adding the property.</param>
        /// <param name="inpPath">The path of item that is added.</param>
        /// <returns>The object with the property added.</returns>
        public object CloseObject(string inpName, object inpObject, string inpPath)
        {
            string tempString = (string)inpObject;

            // This wants to line up with the open, so decrease the indent.
            indentDepth -= 1;
            // Add the new line and indent.
            tempString += AddIndent(indentChars, indentDepth);
            tempString += "}";
            return tempString;
        }
        /// <summary>
        /// Open an array in the JSON string.
        /// </summary>
        /// <param name="inpName">Name of the property.</param>
        /// <param name="inpObject">The object to which we are adding the property.</param>
        /// <param name="inpPath">The path of item that is added.</param>
        /// <returns>The object with the property added.</returns>
        public object MakeArray(string inpName, object inpObject, string inpPath)
        {
            string tempString = (string)inpObject;

            // Add the comma (if appropriate) to the end of the previous line.
            tempString = AddComma(tempString);
            // Add the new line and indent.
            tempString += AddIndent(indentChars, indentDepth);
            tempString += inpName == "" ? "" : "\"" + inpName + "\"" + colonChar;
            tempString += "[";
            // Everything inside this object is ay a greater dealth.
            indentDepth += 1;

            return tempString;
        }
        /// <summary>
        /// Closes an array in the JSON string.
        /// </summary>
        /// <param name="inpName">Name of the property.</param>
        /// <param name="inpObject">The object to which we are adding the property.</param>
        /// <param name="inpPath">The path of item that is added.</param>
        /// <returns>The object with the property added.</returns>
        public object CloseArray(string inpName, object inpObject, string inpPath)
        {
            string tempString = (string)inpObject;

            // This wants to line up with the open, so decrease the indent.
            indentDepth -= 1;
            // Add the new line and indent.
            tempString += AddIndent(indentChars, indentDepth);
            tempString += "]";
            return tempString;
        }
        /// <summary>
        /// Add a boolean property to the JSON string.
        /// </summary>
        /// <param name="inpName">Name of the property.</param>
        /// <param name="inpValue">Value of the property.</param>
        /// <param name="inpObject">The object to which we are adding the property.</param>
        /// <param name="inpPath">The path of item that is added.</param>
        /// <returns>The object with the property added.</returns>
        public object SetBoolean(string inpName, bool inpValue, object inpObject, string inpPath)
        {
            string tempString = (string)inpObject;

            // Add the comma (if appropriate) to the end of the previous line.
            tempString = AddComma(tempString);
            // Add the new line and indent.
            tempString += AddIndent(indentChars, indentDepth);
            tempString += "\"" + inpName + "\"" + colonChar
                        + Convert.ToString(inpValue).ToLower();
            return tempString;
        }
        /// <summary>
        /// Add a decimal property to the JSON string.
        /// </summary>
        /// <param name="inpName">Name of the property.</param>
        /// <param name="inpValue">Value of the property.</param>
        /// <param name="inpObject">The object to which we are adding the property.</param>
        /// <param name="inpPath">The path of item that is added.</param>
        /// <returns>The object with the property added.</returns>
        public object SetDecimal(string inpName, decimal inpValue, object inpObject, string inpPath)
        {
            string tempString = (string)inpObject;

            // Add the comma (if appropriate) to the end of the previous line.
            tempString = AddComma(tempString);
            // Add the new line and indent.
            tempString += AddIndent(indentChars, indentDepth);
            tempString += "\"" + inpName + "\"" + colonChar
                        + Convert.ToString(inpValue);
            return tempString;
        }
        /// <summary>
        /// Add a double property to the JSON string.
        /// </summary>
        /// <param name="inpName">Name of the property.</param>
        /// <param name="inpValue">Value of the property.</param>
        /// <param name="inpObject">The object to which we are adding the property.</param>
        /// <param name="inpPath">The path of item that is added.</param>
        /// <returns>The object with the property added.</returns>
        public object SetDouble(string inpName, double inpValue, object inpObject, string inpPath)
        {
            string tempString = (string)inpObject;

            // Add the comma (if appropriate) to the end of the previous line.
            tempString = AddComma(tempString);
            // Add the new line and indent.
            tempString += AddIndent(indentChars, indentDepth);
            tempString += "\"" + inpName + "\"" + colonChar
                        + Convert.ToString(inpValue);
            return tempString;
        }
        /// <summary>
        /// Add a integer property to the JSON string.
        /// </summary>
        /// <param name="inpName">Name of the property.</param>
        /// <param name="inpValue">Value of the property.</param>
        /// <param name="inpObject">The object to which we are adding the property.</param>
        /// <param name="inpPath">The path of item that is added.</param>
        /// <returns>The object with the property added.</returns>
        public object SetInteger(string inpName, int inpValue, object inpObject, string inpPath)
        {
            string tempString = (string)inpObject;

            // Add the comma (if appropriate) to the end of the previous line.
            tempString = AddComma(tempString);
            // Add the new line and indent.
            tempString += AddIndent(indentChars, indentDepth);
            tempString += "\"" + inpName + "\"" + colonChar
                        + Convert.ToString(inpValue);
            return tempString;
        }
        /// <summary>
        /// Add a long integer property to the JSON string.
        /// </summary>
        /// <param name="inpName">Name of the property.</param>
        /// <param name="inpValue">Value of the property.</param>
        /// <param name="inpObject">The object to which we are adding the property.</param>
        /// <param name="inpPath">The path of item that is added.</param>
        /// <returns>The object with the property added.</returns>
        public object SetLongInt(string inpName, long inpValue, object inpObject, string inpPath)
        {
            string tempString = (string)inpObject;

            // Add the comma (if appropriate) to the end of the previous line.
            tempString = AddComma(tempString);
            // Add the new line and indent.
            tempString += AddIndent(indentChars, indentDepth);
            tempString += "\"" + inpName + "\"" + colonChar
                        + Convert.ToString(inpValue);
            return tempString;
        }
        /// <summary>
        /// Add a string property to the JSON string.
        /// </summary>
        /// <param name="inpName">Name of the property.</param>
        /// <param name="inpValue">Value of the property.</param>
        /// <param name="inpObject">The object to which we are adding the property.</param>
        /// <param name="inpPath">The path of item that is added.</param>
        /// <returns>The object with the property added.</returns>
        public object SetString(string inpName, string inpValue, object inpObject, string inpPath)
        {
            string tempString = (string)inpObject;

            // Add the comma (if appropriate) to the end of the previous line.
            tempString = AddComma(tempString);
            // Add the new line and indent.
            tempString += AddIndent(indentChars, indentDepth);
            tempString += "\"" + inpName + "\"" + colonChar + "\"" + inpValue + "\"";
            return tempString;
        }
        #endregion
    }
}
