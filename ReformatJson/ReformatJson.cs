using System;

namespace CM.ReformatJson.Processing
{
    public class ResformatJson
    {
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
                string readableJson = inpOriginalJson;

                return readableJson;
            }
            catch
            {
                throw;
            }
        }
    }
}
