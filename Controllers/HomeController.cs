using DotnetcoreDynamicJSONRPC;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using ElementsMVC.Models;
using System.Linq; //Only needed if you are using Linq directly in your project code
using Newtonsoft.Json; //Only needed if you are using Linq directly in your project code
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Converters;
using Microsoft.AspNetCore.Mvc;

namespace ElementsMVC.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
		    // We will be using an Elements node in this example.
    	// It is easy to switch to use a Bitcoin, Liquid node.
    	// You need to change these to make sure you can authenticate against the daemon you are running:
    	string rpcUrl = "http://localhost";
    	string rpcPort = "18884";
    	string rpcUsername = "user1";
    	string rpcPassword = "password1";

    	// For examples and notes on how to use the dotnetcoreDynamicJSON-RPC tool and its JSON helper methods please see:
    	// https://github.com/wintercooled/dotnetcoreDynamicJSON-RPC
    	// Initialise an instance of the dynamic dotnetcoreDynamicJSON_RPC class.
    	dynamic dynamicRPC_e1 = new DynamicRPC(rpcUrl, rpcPort, rpcUsername, rpcPassword);
	
	
	
	
	rpcUrl = "http://localhost";
        rpcPort = "18885";
        rpcUsername = "user2";
        rpcPassword = "password2";

	dynamic dynamicRPC_e2 = new DynamicRPC(rpcUrl, rpcPort, rpcUsername, rpcPassword);
    	// Initialise our model that will be passed to the view
    	var nodeInfo = new ExampleNodeInfo();



    	if (dynamicRPC_e1.DaemonIsRunning())
    	{
       	 try
       	 {
	     string networkinfo = dynamicRPC_e1.getnetworkinfo();
	     string networkactive = networkinfo.GetProperty("result.networkactive");
	     string timeoffset = networkinfo.GetProperty("result.timeoffset");
	     string connections = networkinfo.GetProperty("result.connections");
	     nodeInfo.Networkactive = networkactive;
	     nodeInfo.Timeoffset = timeoffset;
	     nodeInfo.Connections = connections;



             string blockNumber = dynamicRPC_e1.getblockcount();
             blockNumber = blockNumber.GetProperty("result");
	     nodeInfo.Blocknumber = blockNumber;

	     string chaininfo = dynamicRPC_e1.getblockchaininfo();
	     string chain = chaininfo.GetProperty("result.chain");
	     string blocks = chaininfo.GetProperty("Blocks");
	     nodeInfo.Blocks = blocks;
	     nodeInfo.Chain = chain;

	
       	     // Get the JSON result of the 'getwalletinfo' RPC on the Elements node.
       	     string wallet_info = dynamicRPC_e1.getwalletinfo();
       	     // Use the DotnetcoreDynamicJSONRPC 'GetProperty' string helper to return the property value we want.
       	     string balance = wallet_info.GetProperty("result.balance.bitcoin");
       	     // Populate the model
       	     nodeInfo.Balance = balance;
	     Rootwallet wallet_i = new Rootwallet();
	     ITraceWriter traceWriter = new MemoryTraceWriter();
	     JsonConvert.PopulateObject(wallet_info, wallet_i, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, TraceWriter = traceWriter, Converters = { new JavaScriptDateTimeConverter() } });
	     
	     nodeInfo.wallet = wallet_i;
	     Console.WriteLine("wallet_i object: " , nodeInfo.wallet);

	     string assetslist = dynamicRPC_e1.listissuances();
	     RootIssux isslist = new RootIssux();

	     JsonConvert.PopulateObject(assetslist, isslist, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, TraceWriter = traceWriter, Converters = { new JavaScriptDateTimeConverter() } });
	     JsonConvert.SerializeObject(assetslist, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, TraceWriter = traceWriter, Converters = { new JavaScriptDateTimeConverter() } });
	     nodeInfo.Issuances = isslist;
	     Console.WriteLine("issuex object: " , nodeInfo.Issuances);

	     string txlist_response = dynamicRPC_e1.listtransactions();
	     Console.WriteLine(txlist_response);
	     Roottx txlist = new Roottx();
	     JsonConvert.PopulateObject(txlist_response, txlist, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, TraceWriter = traceWriter, Converters = { new JavaScriptDateTimeConverter() } });
	     nodeInfo.Transactions = txlist;


       	 }
       	 catch (Exception e)
       	 {
       	     nodeInfo.Message = e.Message;
       	 }
 	   }
 	   else
	    {
       	 nodeInfo.Message = "Could not communicate with daemon";
    	}
	       	// Return the view and the associated model we have populated
    	return View(nodeInfo);

        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

	[HttpGet]
	public ActionResult CreateAsset()
	{
	    return View();
	}

	[HttpPost]
	public ActionResult CreateAsset(Asset_C asset)
	{
        string rpcUrl = "http://localhost";
        string rpcPort = "18884";
        string rpcUsername = "user1";
        string rpcPassword = "password1";
        // Initialise an instance of the dynamic dotnetcoreDynamicJSON_RPC class.
        dynamic dynamicRPC_e1 = new DynamicRPC(rpcUrl, rpcPort, rpcUsername, rpcPassword);

		try{

			dynamicRPC_e1.issueasset( asset.assetamount, asset.tokenamount, asset.blind);
			dynamicRPC_e1.generate(1);
			return RedirectToAction("Index");
		}catch{
			
			return View("Error");
		}
	}

	[HttpPost]
        public ActionResult Sendasset(Assettransfer transfer)
        {
        string rpcUrl = "http://localhost";
        string rpcPort = "18884";
        string rpcUsername = "user1";
        string rpcPassword = "password1";
        // Initialise an instance of the dynamic dotnetcoreDynamicJSON_RPC class.
        dynamic dynamicRPC_e1 = new DynamicRPC(rpcUrl, rpcPort, rpcUsername, rpcPassword);

                try{

                        dynamicRPC_e1.sendtoaddress( transfer.address, transfer.amount, transfer.comment, transfer.commentto, transfer.substractfee, transfer.ignoreblindfail);
                        dynamicRPC_e1.generate(1);
                        return RedirectToAction("Index");
                }catch{
                        return View("Error");
                }
        }

	[HttpGet]
	public ActionResult Sendasset()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
