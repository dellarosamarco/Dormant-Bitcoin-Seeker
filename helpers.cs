using System.Collections.Generic;
using System;

public static class Helpers{

    private static Random rd = new Random();
    
    public static List<int> randomBytes(){
        List<int> bytes = new List<int>();

        for(int n=0;n<32;n++){
            bytes.Add(rd.Next(0,255));
        }
        
        return bytes;
    }

    public static string bytesToHex(List<int> bytes){
        string hex = "";

        for(int n=0;n<bytes.Count;n++){
            hex += bytes[n].ToString("x2");
        }

        return hex;
    }
}