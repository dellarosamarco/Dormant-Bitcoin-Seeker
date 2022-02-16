# Dormant Bitcoin Seeker

#### Create a bitcoin wallet pool
```c#
class Program {
    public static void Main () {
        Init();
        Console.ReadLine();
    }

    public static async void Init(){
        BlockchainRequestPool.pool(500); //Generate 500 random wallets

        float balance;

        while(BlockchainRequestPool.isEmpty == false){
            if(BlockchainRequestPool.waitingRequests <= 1){
                balance = await BlockchainRequestPool.request(); //Get pooled wallets balance (max 125 wallets at time)
            }
        }

        Console.WriteLine("Finished");
    }
}
```

#### Generate a single wallet
```c#
class Program {
    public static void Main () {
        wallet = new BitcoinWallet();
        
        //Private key
        Console.WriteLine(wallet.privateKey); //L4uWyjckCyJzcYfirEUMYDsLycAkW3Kir3ps9WCjTGbLmNX95UMR
        
        //Public key
        Console.WriteLine(wallet.publicKey); //e55703d16ad6eec5fcbfaad3972f3ff4788c01b3d0fcf838a766b0847166ec0a
        
        //Address
        Console.WriteLine(wallet.address); //1ELRYmZKSjtMnbBfmDx8Un4h6LMx6dPuTy
        
        //Balance
        Console.WriteLine(wallet.balance); //0 BTC
    }
}
```
