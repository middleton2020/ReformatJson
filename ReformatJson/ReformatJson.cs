using System;
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
        JsonReader.DeligateSetInteger setInteger;
        JsonReader.DeligateSetString setString;
        #endregion

        public string MakeReadable(string inpOriginalJson)
        {
            try
            {
                string readableJson = MakeReadable(inpOriginalJson, "    ", ":");
                return readableJson;
            }
            catch
            {
                throw;
            }
        }

        public string MakeReadable(string inpOriginalJson, string inpIndentChars)
        {
            try
            {
                string readableJson = MakeReadable(inpOriginalJson, inpIndentChars, ":");
                return readableJson;
            }
            catch
            {
                throw;
            }
        }

        public string MakeReadable(string inpOriginalJson, string inpIndentChars, string inpColonChar)
        {
            try
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
            catch
            {
                throw;
            }
        }

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
            try
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
            catch
            {
                throw;
            }
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
            try
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
            catch
            {
                throw;
            }
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
            try
            {
                string tempString = (string)inpObject;

                // This wants to line up with the open, so decrease the indent.
                indentDepth -= 1;
                // Add the new line and indent.
                tempString += AddIndent(indentChars, indentDepth);
                tempString += "}";
                return tempString;
            }
            catch
            {
                throw;
            }
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
            try
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
            catch
            {
                throw;
            }
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
            try
            {
                string tempString = (string)inpObject;

                // This wants to line up with the open, so decrease the indent.
                indentDepth -= 1;
                // Add the new line and indent.
                tempString += AddIndent(indentChars, indentDepth);
                tempString += "]";
                return tempString;
            }
            catch
            {
                throw;
            }
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
            try
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
            catch
            {
                throw;
            }
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
            try
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
            catch
            {
                throw;
            }
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
            try
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
            catch
            {
                throw;
            }
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
            try
            {
                string tempString = (string)inpObject;

                // Add the comma (if appropriate) to the end of the previous line.
                tempString = AddComma(tempString);
                // Add the new line and indent.
                tempString += AddIndent(indentChars, indentDepth);
                tempString += "\"" + inpName + "\"" + colonChar + "\"" + inpValue + "\"";
                return tempString;
            }
            catch
            {
                throw;
            }
        }
        #endregion
    }
}
