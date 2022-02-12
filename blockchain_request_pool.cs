using System;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json.Linq;

public static class BlockchainRequestPool{
    private static string base_url = "https://blockchain.info/balance?cors=true&active=";
    private static int maxChunkSize = 125;

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

    public static float getPoolBalance(){
        if(isEmpty)
            throw new NullReferenceException("The pool is empty.");

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
        }
        else {
            Console.WriteLine(chunkBalance + " BTC => Chunk size : " + chunkSize);
        }

        return chunkBalance;
    }
}