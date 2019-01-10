using System;
using System.Collections.Generic;
using System.Linq; //Only needed if you are using Linq directly in your project code
using Newtonsoft.Json; //Only needed if you are using Linq directly in your project code
using System.ComponentModel.DataAnnotations;

namespace ElementsMVC.Models
{
        public class Issuance
    {
	[JsonProperty("isreissuance")]
        public bool Isreissuance { get; set; }

        [JsonProperty("entropy")]
        public string Entropy { get; set; }

        [JsonProperty("txid")]
        public string Txid { get; set; }

        [JsonProperty("vin")]
        public long Vin { get; set; }

        [JsonProperty("asset")]
        public string Asset { get; set; }

        [JsonProperty("assetlabel", NullValueHandling = NullValueHandling.Ignore)]
        public string Assetlabel { get; set; }

        [JsonProperty("assetamount")]
        public long Assetamount { get; set; }

        [JsonProperty("assetblinds")]
        public string Assetblinds { get; set; }

        [JsonProperty("token", NullValueHandling = NullValueHandling.Ignore)]
        public string Token { get; set; }

        [JsonProperty("tokenamount", NullValueHandling = NullValueHandling.Ignore)]
        public long? Tokenamount { get; set; }

        [JsonProperty("tokenblinds", NullValueHandling = NullValueHandling.Ignore)]
        public string Tokenblinds { get; set; }

	[JsonProperty("Error", NullValueHandling = NullValueHandling.Ignore)]
        public object Error { get; set; }

        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public object Id { get; set; }
    }

    public class RootIssux
    {
	public List<Issuance> result { get; set; }
 	public object error { get; set; }
	public object id { get; set; }
    }

    public class Transaction
    {
	public string account { get; set; }
	public string address { get; set; }
	public string category { get; set; }
	public double amount { get; set; }
	public string amountblinder { get; set; }
	public string asset { get; set; }
	public string assetblinder { get; set; }
	public int vout { get; set; }
	public double fee { get; set; }
	public int confirmations { get; set; }
	public string blockhash { get; set; }
	public int blockindex { get; set; }
	public int blocktime { get; set; }
	public string txid { get; set; }
	public List<object> walletconflicts { get; set; }
	public int time { get; set; }
	public int timereceived { get; set; }
	public bool bip125_replaceable { get; set; }
	public string comment { get; set; }
	public string to { get; set; }
	public bool abandoned { get; set; }
	public string label { get; set; }
	public bool generated { get; set; }
    }

    public class Roottx
    {
        public List<Transaction> result { get; set; }
        public object error { get; set; }
        public object id { get; set; }
    }

    public class Asset_C
    {
	[Required]
	[Display(Name="Asset amount")]
	public double assetamount { get; set; }
	[Required]
	[Display(Name="Token amount")]
	public double tokenamount { get; set; }
	[Display(Name="Blind")]
	public bool blind { get; set; }

    }

    public class Assettransfer
    {
	[Required]
		// TODO Bicoin address validation
        [StringLength(81,ErrorMessage = "Invalid address")]
        [Display(Name="Address")]
        public string address { get; set; }
	[Required]
        [Display(Name="Amount")]
        public double amount { get; set; }
	[Display(Name="Comment")]
	[StringLength(100)]
	public string comment {get; set; }
	[StringLength(100)]
	[Display(Name="Comment to")]
        public string commentto {get; set; }
	[Display(Name="Substract fee from amount")]
        public bool? substractfee { get; set; }
	[StringLength(30)]
        [Display(Name="Label")]
        public string assetlabel { get; set; }
        [Display(Name="Ignore blind fails")]
        public bool? ignoreblindfail {get; set; }

    }

    public class Wallet
    {
	public int walletversion { get; set; }
    	public dynamic balance { get; set; }
    	public dynamic unconfirmed_balance { get; set; }
    	public dynamic immature_balance { get; set; }
    	public int txcount { get; set; }
    	public int keypoololdest { get; set; }
    	public int keypoolsize { get; set; }
    	public double paytxfee { get; set; }
    	public string hdmasterkeyid { get; set; }

    }

    public class Rootwallet
    {
        public Wallet result { get; set; }
        public object error { get; set; }
        public object id { get; set; }
    }

    public class ExampleNodeInfo
    {
	    public ExampleNodeInfo(){
		    this.Issuances = new RootIssux();
		    this.Transactions = new Roottx();
		    this.wallet = new Rootwallet();
	    }
        public string Balance { get; set; }
	public string Blocks { get; set; }
	public string Chain { get; set; }
	[Display(Name="Number of Connections")]
	public string Connections { get; set; }
	[Display(Name="Network active")]
	public string Networkactive { get; set; }
	public string Timeoffset { get; set; }
	[Display(Name="Block Number")]
	public string Blocknumber { get; set; }
        public string Message { get; set; }
        //Add whatever other properties you want here
	public Rootwallet wallet { get; set; }
	public RootIssux  Issuances { get; set; }
	public Roottx Transactions { get; set; }
	public int txcount { get; set; }
	public double unconfirmed_balance { get; set; }
	public double immature_balance { get; set; }
    }
}
