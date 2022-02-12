using System.Collections.Generic;
using System;
using NBitcoin;

public class BitcoinWallet
{
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
    
    public BitcoinWallet(){
        Key privKey = new Key();

        privateKey = privKey.GetBitcoinSecret(Network.Main).ToString();

        publicKey = privKey.PubKey.ToString();
        address = privKey.PubKey.GetAddress(ScriptPubKeyType.Legacy,Network.Main).ToString();
        //Console.WriteLine(publicKey.GetAddress(ScriptPubKeyType.Legacy,Network.TestNet));
    }
}