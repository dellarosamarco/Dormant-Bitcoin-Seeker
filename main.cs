using System;
using NBitcoin;
using System.Collections.Generic;

class Program {
    public static void Main () {
        BitcoinWallet wallet = new BitcoinWallet();
        Console.WriteLine(wallet.privateKey);
        Console.WriteLine(wallet.publicKey);
        Console.WriteLine(wallet.address);
        Console.WriteLine(wallet.balance);
        
        Init();
        Console.ReadLine();
    }

    public static async void Init(){
        BlockchainRequestPool.pool(500);

        float balance;

        while(BlockchainRequestPool.isEmpty == false){
            if(BlockchainRequestPool.waitingRequests <= 1){
                balance = await BlockchainRequestPool.request();
            }
        }

        Console.WriteLine("Finished");
    }
}