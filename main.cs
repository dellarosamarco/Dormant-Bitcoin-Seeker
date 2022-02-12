using System;
using NBitcoin;

class Program {
    public static void Main () 
    {
        BitcoinWallet wallet = new BitcoinWallet(); 
        // Console.WriteLine(wallet.getBalance());
        Console.WriteLine(wallet.privateKey);
        Console.WriteLine(wallet.balance);
    }
}