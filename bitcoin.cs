using System.Collections.Generic;
using System;

public static class Bitcoin
{
    public static string randomPrivateKey(){
        List<int> bytes = Helpers.randomBytes();
        string hex = Helpers.bytesToHex(bytes);

        for(int n=0; n< bytes.Count; n++){
            Console.WriteLine(bytes[n]);
        }
        
        return hex;
    }
}