using System;
using NBitcoin;
using System.Collections.Generic;

class Program {
    public static void Main () 
    {
        //BitcoinWallet wallet = new BitcoinWallet(); 
        // Console.WriteLine(wallet.getBalance());
        // Console.WriteLine(wallet.privateKey);
        // Console.WriteLine(wallet.balance);

        BlockchainRequestPool.pool(125);

        while(BlockchainRequestPool.isEmpty == false){
            BlockchainRequestPool.getPoolBalance();
        }
    }
}