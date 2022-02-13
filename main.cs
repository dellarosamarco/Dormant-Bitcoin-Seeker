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

        Init();
        Console.ReadLine();
    }

    public static async void Init(){
        BlockchainRequestPool.pool(500);

        while(BlockchainRequestPool.isEmpty == false){
            if(BlockchainRequestPool.waitingRequests <= 1){
                await BlockchainRequestPool.request();
            }
        }

        Console.WriteLine("Finished");
    }
}