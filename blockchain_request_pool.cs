using System;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

public static class BlockchainRequestPool{
    private static string base_url = "https://blockchain.info/balance?cors=true&active=";
    private static int maxChunkSize = 125;
    private static int apiLimit = 1000;
    private static int _waitingRequests = 0;
    public static int waitingRequests{
        get { return _waitingRequests; }
        private set { _waitingRequests = value; }
    }

    public static bool isEmpty{
        get {
            return walletsPool.Count == 0;
        }
    }

    private static List<BitcoinWallet> walletsPool = new List<BitcoinWallet>();

    public static void pool(int totalWallets){
        for(int i=0;i<totalWallets;i++){
            walletsPool.Add(new BitcoinWallet());
        }
    }

    public static async Task<float> request(){
        if(waitingRequests == 0){
            return getPoolBalance();
        }
        else{
            return await wait();
        }
    }

    public static float getPoolBalance(){
        waitingRequests+=1;
        if(isEmpty){
            Console.WriteLine("The pool is empty.");
            return 0;
        }
        

        string requestUrl = base_url;
        List<BitcoinWallet> chunk = new List<BitcoinWallet>();
        float chunkBalance = 0;
        int chunkSize;

        if(walletsPool.Count >= maxChunkSize-1){
            chunkSize = maxChunkSize;
            for(int i=maxChunkSize-1;i>=0;i--){
                requestUrl += walletsPool[i].address + ",";
                chunk.Add(walletsPool[i]);
                walletsPool.Remove(walletsPool[i]);
            }
        }
        else{
            chunkSize = walletsPool.Count;
            for(int i=walletsPool.Count-1;i>=0;i--){
                requestUrl += walletsPool[i].address + ",";
                chunk.Add(walletsPool[i]);
                walletsPool.Remove(walletsPool[i]);
            }
        }

        using (var client = new HttpClient())
        {
            var response = client.GetAsync(requestUrl).GetAwaiter().GetResult();
            var responseContent = response.Content;

            for(int i=0;i<chunk.Count;i++){
                chunkBalance += float.Parse(JObject.Parse(responseContent.ReadAsStringAsync().GetAwaiter().GetResult())[chunk[i].address]["final_balance"].ToString());
            }
        }

        if(chunkBalance > 0){
            for(int i=0;i<chunk.Count;i++){
                Console.WriteLine(chunk[i].privateKey);
            }

            Console.WriteLine(chunkBalance + " BTC => Total wallets : " + chunkSize);
        }
        else {
            Console.WriteLine(chunkBalance + " BTC => Total wallets : " + chunkSize);
        }

        return chunkBalance;
    }

    public static async Task<float> wait(){
        await Task.Run(() =>
        {
            Task.Delay(apiLimit * waitingRequests).Wait();
            waitingRequests -= 1;
            return getPoolBalance();
        });
        return 0;
    }
}