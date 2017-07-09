using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace DataInvoice.Test.SOLUTIONS.GENERAL
{
    [TestClass]
    public class ComminicationTest
    {


        [TestMethod]
        public void testSMS()
        {
            
        }



        [TestMethod]
        public void TestMethod1()
        {
            DataInvoice.GLOBAL.DataInvoiceEnv env = new GLOBAL.DataInvoiceEnv();

            DataInvoice.SOLUTIONS.GENERAL.COMMUNICATION.CommunicationProvider communicationProvide = new DataInvoice.SOLUTIONS.GENERAL.COMMUNICATION.CommunicationProvider(env.Connector);

           var res = communicationProvide.GetLasts();



           Console.WriteLine(res.Count);



        }


        //public class logmsg
        //{
        //    long timestamp { get; set; }
        //    string version { get; set; }
        //    string Host { get; set; }
 


        //    string short_message { get; set; }
        //    string full_message { get; set; }

        //    int level { get; set; }
        //    int _user_id { get; set; }

        //    string _some_info { get; set; }

        //    string some_metric_num { get; set; }

        //    string _XOVHTOKEN { get; set; }
        //}



        //[TestMethod]
        //public void Logtest()
        //{
      

        //    Gelf.GelfMessage gmsg = new Gelf.GelfMessage();
        //    gmsg.Add("msg1", "test");
        //    gmsg.Facility="X-OVH-TOKEN:dc2c434a-08ed-46af-8555-4d1bb886a124";
        //    gmsg.FullMessage = "test";
        //    gmsg.Version = "1.1";
        //    gmsg.Host = "example.org";
        //    gmsg.ShortMessage = "msgshort test00";
        //    gmsg.TimeStamp = DateTime.Now;
        //    gmsg.Level = 1;
        //    gmsg.us
            
        //    // gelf 
        //    //echo -e '{"timestamp": 1484474999, "level": 1, "_user_id": 9001, "_some_info": "foo", "some_metric_num": 42.0, "_X-OVH-TOKEN":"dc2c434a-08ed-46af-8555-4d1bb886a124"}\0' | openssl s_client -quiet -no_ign_eof  -connect gra2.logs.ovh.com:12202


        //          //LOG_NUEGYVN
        ////echo -e '{"version":"1.1", "host": "example.org", "short_message": "A short GELF message that helps you identify what is going on", "full_message": "Backtrace here more stuff", "timestamp": 1484471859, "level": 1, "_user_id": 9001, "_some_info": "foo", "some_metric_num": 42.0, "_X-OVH-TOKEN":"dc2c434a-08ed-46af-8555-4d1bb886a124"}\0' | openssl s_client -quiet -no_ign_eof  -connect gra2.logs.ovh.com:12202
        ////dc2c434a-08ed-46af-8555-4d1bb886a124
        ////https://gra2.logs.ovh.com Identifiant  : logs-vp-87269 Mot de passe : yqSE9ywrwu7CPYsK
        //    //echo -e '<6>1 2017-01-15T09:44:18.304Z 149.202.165.20 example.org - - [exampleSDID@8485 user_id="9001"  some_info="foo" some_metric_num="42.0" X-OVH-TOKEN="dc2c434a-08ed-46af-8555-4d1bb886a124"] A short RFC 5424 message that helps you identify what is going on'\n | openssl s_client -quiet -no_ign_eof  -connect gra2.logs.ovh.com:6514
        //    //echo -e 'X-OVH-TOKEN:dc2c434a-08ed-46af-8555-4d1bb886a124	host:example.org	time:2017-01-15T09:44:54.415Z	message:A short LTSV message that helps you identify what is going on	full_message:Backtrace here more stuff	level:1	user_id:9001	some_info:foo	some_metric_num:42.0\0' | openssl s_client -quiet -no_ign_eof  -connect gra2.logs.ovh.com:12200
        //    //gra2.logs.ovh.com:12202
        //    Gelf.GelfPublisher publish = new Gelf.GelfPublisher("gra2.logs.ovh.com", 12202);
        //    publish.Publish(gmsg);

        //    Console.WriteLine(gmsg.FullMessage);
            
        //}

    }
}
