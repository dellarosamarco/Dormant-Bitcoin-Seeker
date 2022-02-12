using System.Collections.Generic;
using System;
using NBitcoin;
using System.Net.Http;
using Newtonsoft.Json.Linq;

public class BitcoinWallet
{
    private const string base_url = "https://blockchain.info/balance?cors=true&active=";
    
    private string _privateKey;
    public string privateKey{
        get { return _privateKey; }
        private set { _privateKey = value; }
    }

    private string _publicKey;
    public string publicKey{
        get { return _publicKey; }
        private set { _publicKey = value; }
    }

    private string _address;
    public string address{
        get { return _address; }
        private set { _address = value; }
    }

    private float? _balance = null;
    public float? balance{
        get { 
            if(_balance == null) {
                getBalance();
            }
            return _balance; 
        }
        private set { _balance = value; }
    }
    
    public BitcoinWallet(){
        Key privKey = new Key();

        privateKey = privKey.GetBitcoinSecret(Network.Main).ToString();

        publicKey = privKey.PubKey.ToString();
        address = privKey.PubKey.GetAddress(ScriptPubKeyType.Legacy,Network.Main).ToString();
        //Console.WriteLine(publicKey.GetAddress(ScriptPubKeyType.Legacy,Network.TestNet));
    }

    private void getBalance(){        
        using (var client = new HttpClient())
        {
            var response = client.GetAsync(base_url + address).GetAwaiter().GetResult();
            if (response.IsSuccessStatusCode)
            {
                var responseContent = response.Content;
                balance = float.Parse(JObject.Parse(responseContent.ReadAsStringAsync().GetAwaiter().GetResult())[address]["final_balance"].ToString());
            }
            else{
                balance = 0;
            }
        }
    }
}