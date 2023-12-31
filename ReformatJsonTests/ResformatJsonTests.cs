using Microsoft.VisualStudio.TestTools.UnitTesting;
using CM.ReformatJson.Processing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.ReformatJson.Processing.Tests
{
    [TestClass()]
    public class ResformatJsonTests
    {
        /// <summary>
        /// A simple JSON to test against.
        /// </summary>
        [TestMethod()]
        public void MakeReadableSimpleTest()
        {
            ResformatJson testReader = new();

            string inputJson = "{"
                            + "\"id\":\"Bibble\""
                            + ","
                            + "\"name\":\"Test Name\""
                            + ","
                            + "\"parcels\":1"
                            + ","
                            + "\"value\":11.50"
                            + ","
                            + "\"saturdayDelivery\":true"
                            + "}";

            // JSON after formatting.
            string testJson = System.Environment.NewLine + "{"
                            + System.Environment.NewLine + "    \"id\":\"Bibble\""
                            + ","
                            + System.Environment.NewLine + "    \"name\":\"Test Name\""
                            + ","
                            + System.Environment.NewLine + "    \"parcels\":1"
                            + ","
                            + System.Environment.NewLine + "    \"value\":11.50"
                            + ","
                            + System.Environment.NewLine + "    \"saturdayDelivery\":true"
                            + System.Environment.NewLine + "}";

            string  resultJson = (string)testReader.MakeReadable(inputJson, "    ", ":");

            Assert.AreEqual(testJson, resultJson, false);
        }

        /// <summary>
        /// A more complex JSON with arrays, etc.
        /// </summary>
        [TestMethod()]
        public void MakeReadableModerateTest()
        {
            ResformatJson testReader = new();

            string inputJson = "{"
                            + "\"id\":\"Bibble\""
                            + ","
                            + "\"name\":\"Test Name\""
                            + ","
                            + "\"parcels\":1"
                            + ","
                            + "\"value\":11.50"
                            + ","
                            + "\"items\":"
                            + "["
                            + "{"
                            + "\"name\":\"Box 17\""
                            + "}"
                            + ","
                            + "{"
                            + "\"name\":\"22 Sphere\""
                            + "}"
                            + ","
                            + "{"
                            + "\"name\":\"Alphabet\""
                            + "}"
                            + "]"
                            + ","
                            + "\"saturdayDelivery\":true"
                            + "}";

            // JSON after formatting.
            string testJson = System.Environment.NewLine + "{"
                            + System.Environment.NewLine + "    \"id\":\"Bibble\""
                            + ","
                            + System.Environment.NewLine + "    \"name\":\"Test Name\""
                            + ","
                            + System.Environment.NewLine + "    \"parcels\":1"
                            + ","
                            + System.Environment.NewLine + "    \"value\":11.50"
                            + ","
                            + System.Environment.NewLine + "    \"items\":"
                            + "["
                            + System.Environment.NewLine + "        {"
                            + System.Environment.NewLine + "            \"name\":\"Box 17\""
                            + System.Environment.NewLine + "        }"
                            + ","
                            + System.Environment.NewLine + "        {"
                            + System.Environment.NewLine + "            \"name\":\"22 Sphere\""
                            + System.Environment.NewLine + "        }"
                            + ","
                            + System.Environment.NewLine + "        {"
                            + System.Environment.NewLine + "            \"name\":\"Alphabet\""
                            + System.Environment.NewLine + "        }"
                            + System.Environment.NewLine + "    ]"
                            + ","
                            + System.Environment.NewLine + "    \"saturdayDelivery\":true"
                            + System.Environment.NewLine + "}";
            string resultJson = (string)testReader.MakeReadable(inputJson, "    ", ":");

            Assert.AreEqual(testJson, resultJson, false);
        }

        /// <summary>
        /// A very complex JSON with multiple levels of object and array.
        /// </summary>
        [TestMethod()]
        public void MakeReadableComplexTest()
        {
            ResformatJson testReader = new();

            string inputJson = "{"
                            + "\"order_id\":7654321"
                            + ","
                            + "\"date_ordered\":\"2013-12-17T07:00:15.087Z\""
                            + ","
                            + "\"seller_name\":\"Robin\""
                            + ","
                            + "\"store_name\":\"RobinStore\""
                            + ","
                            + "\"buyer_name\":\"Mary\""
                            + ","
                            + "\"buyer_email\":\"mary@gmail.com\""
                            + ","
                            + "\"require_insurance\":true"
                            + ","
                            + "\"status\":\"PENDING\""
                            + ","
                            + "\"is_invoiced\":false"
                            + ","
                            + "\"total_count\":10"
                            + ","
                            + "\"unique_count\":1"
                            + ","
                            + "\"payment\": {"
                            + "\"method\":\"PayPal.com\""
                            + ","
                            + "\"currency_code\":\"USD\""
                            + ","
                            + "\"date_paid\":\"2013-12-17T09:20:02.000Z\""
                            + ","
                            + "\"status\":\"Sent\"}"
                            + ","
                            + "\"shipping\": {"
                            + "\"address\": {"
                            + "\"name\": {"
                            + "\"full\":\"Robin Button\"}"
                            + ","
                            + "\"full\":\"Button House, Ghosts, BBC1\""
                            + ","
                            + "\"country_code\":\"UK\"}"
                            + ","
                            + "\"date_shipped\":\"2013-12-17T03:00:15.087Z\"}"
                            + ","
                            + "\"cost\": {"
                            + "\"currency_code\":\"USD\""
                            + ","
                            + "\"subtotal\":\"139.9900\""
                            + ","
                            + "\"grand_total\":\"157.8000\""
                            + ","
                            + "\"disp_currency_code\":\"USD\""
                            + ","
                            + "\"disp_subtotal\":\"139.9900\""
                            + ","
                            + "\"disp_grand_total\":\"157.8000\""
                            + ","
                            + "\"etc1\":\"0.0000\""
                            + ","
                            + "\"etc2\":\"0.0000\""
                            + ","
                            + "\"insurance\":\"3.0500\""
                            + ","
                            + "\"shipping\":\"14.7600\""
                            + ","
                            + "\"credit\":\"0.0000\""
                            + ","
                            + "\"coupon\":\"0.0000\"}"
                            + "}";

            // JSON after formatting.
            string testJson = System.Environment.NewLine + "{"
                            + System.Environment.NewLine + "    \"order_id\": 7654321"
                            + ","
                            + System.Environment.NewLine + "    \"date_ordered\": \"2013-12-17T07:00:15.087Z\""
                            + ","
                            + System.Environment.NewLine + "    \"seller_name\": \"Robin\""
                            + ","
                            + System.Environment.NewLine + "    \"store_name\": \"RobinStore\""
                            + ","
                            + System.Environment.NewLine + "    \"buyer_name\": \"Mary\""
                            + ","
                            + System.Environment.NewLine + "    \"buyer_email\": \"mary@gmail.com\""
                            + ","
                            + System.Environment.NewLine + "    \"require_insurance\": true"
                            + ","
                            + System.Environment.NewLine + "    \"status\": \"PENDING\""
                            + ","
                            + System.Environment.NewLine + "    \"is_invoiced\": false"
                            + ","
                            + System.Environment.NewLine + "    \"total_count\": 10"
                            + ","
                            + System.Environment.NewLine + "    \"unique_count\": 1"
                            + ","
                            + System.Environment.NewLine + "    \"payment\": {"
                            + System.Environment.NewLine + "        \"method\": \"PayPal.com\""
                            + ","
                            + System.Environment.NewLine + "        \"currency_code\": \"USD\""
                            + ","
                            + System.Environment.NewLine + "        \"date_paid\": \"2013-12-17T09:20:02.000Z\""
                            + ","
                            + System.Environment.NewLine + "        \"status\": \"Sent\""
                            + System.Environment.NewLine + "    }"
                            + ","
                            + System.Environment.NewLine + "    \"shipping\": {"
                            + System.Environment.NewLine + "        \"address\": {"
                            + System.Environment.NewLine + "            \"name\": {"
                            + System.Environment.NewLine + "                \"full\": \"Robin Button\""
                            + System.Environment.NewLine + "            }"
                            + ","
                            + System.Environment.NewLine + "            \"full\": \"Button House, Ghosts, BBC1\""
                            + ","
                            + System.Environment.NewLine + "            \"country_code\": \"UK\""
                            + System.Environment.NewLine + "        }"
                            + ","
                            + System.Environment.NewLine + "        \"date_shipped\": \"2013-12-17T03:00:15.087Z\""
                            + System.Environment.NewLine + "    }"
                            + ","
                            + System.Environment.NewLine + "    \"cost\": {"
                            + System.Environment.NewLine + "        \"currency_code\": \"USD\""
                            + ","
                            + System.Environment.NewLine + "        \"subtotal\": \"139.9900\""
                            + ","
                            + System.Environment.NewLine + "        \"grand_total\": \"157.8000\""
                            + ","
                            + System.Environment.NewLine + "        \"disp_currency_code\": \"USD\""
                            + ","
                            + System.Environment.NewLine + "        \"disp_subtotal\": \"139.9900\""
                            + ","
                            + System.Environment.NewLine + "        \"disp_grand_total\": \"157.8000\""
                            + ","
                            + System.Environment.NewLine + "        \"etc1\": \"0.0000\""
                            + ","
                            + System.Environment.NewLine + "        \"etc2\": \"0.0000\""
                            + ","
                            + System.Environment.NewLine + "        \"insurance\": \"3.0500\""
                            + ","
                            + System.Environment.NewLine + "        \"shipping\": \"14.7600\""
                            + ","
                            + System.Environment.NewLine + "        \"credit\": \"0.0000\""
                            + ","
                            + System.Environment.NewLine + "        \"coupon\": \"0.0000\""
                            + System.Environment.NewLine + "    }"
                            + System.Environment.NewLine + "}";

            string resultJson = (string)testReader.MakeReadable(inputJson, "    ", ": ");

            Assert.AreEqual(testJson, resultJson, false);
        }
    }
}