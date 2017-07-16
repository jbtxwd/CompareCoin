public class Base
{
    public bool auth_google_enabled { get; set; }
    public bool auth_mobile_enabled { get; set; }
    public bool trade_password_enabled { get; set; }
    public string username { get; set; }
}

public class CNY
{
    public double amount { get; set; }
    public string currency { get; set; }
    public string symbol { get; set; }
}

public class BTC
{
    public int amount { get; set; }
    public string currency { get; set; }
    public string symbol { get; set; }
}

public class LTC
{
    public double amount { get; set; }
    public string currency { get; set; }
    public string symbol { get; set; }
}

public class ETH
{
    public int amount { get; set; }
    public string currency { get; set; }
    public string symbol { get; set; }
}

public class ETC
{
    public int amount { get; set; }
    public string currency { get; set; }
    public string symbol { get; set; }
}

public class BTS
{
    public double amount { get; set; }
    public string currency { get; set; }
    public string symbol { get; set; }
}

public class EOS
{
    public int amount { get; set; }
    public string currency { get; set; }
    public string symbol { get; set; }
}

public class Balance
{
    public CNY CNY { get; set; }
    public BTC BTC { get; set; }
    public LTC LTC { get; set; }
    public ETH ETH { get; set; }
    public ETC ETC { get; set; }
    public BTS BTS { get; set; }
    public EOS EOS { get; set; }
}

public class CNY2
{
    public double amount { get; set; }
    public string currency { get; set; }
    public string symbol { get; set; }
}

public class BTC2
{
    public int amount { get; set; }
    public string currency { get; set; }
    public string symbol { get; set; }
}

public class LTC2
{
    public int amount { get; set; }
    public string currency { get; set; }
    public string symbol { get; set; }
}

public class ETH2
{
    public int amount { get; set; }
    public string currency { get; set; }
    public string symbol { get; set; }
}

public class ETC2
{
    public int amount { get; set; }
    public string currency { get; set; }
    public string symbol { get; set; }
}

public class BTS2
{
    public int amount { get; set; }
    public string currency { get; set; }
    public string symbol { get; set; }
}

public class EOS2
{
    public int amount { get; set; }
    public string currency { get; set; }
    public string symbol { get; set; }
}

public class Frozen
{
    public CNY2 CNY { get; set; }
    public BTC2 BTC { get; set; }
    public LTC2 LTC { get; set; }
    public ETH2 ETH { get; set; }
    public ETC2 ETC { get; set; }
    public BTS2 BTS { get; set; }
    public EOS2 EOS { get; set; }
}

public class P2p
{
    public int inCNY { get; set; }
    public int inBTC { get; set; }
    public int inLTC { get; set; }
    public int inETH { get; set; }
    public int inETC { get; set; }
    public int inBTS { get; set; }
    public int inEOS { get; set; }
    public int outCNY { get; set; }
    public int outBTC { get; set; }
    public int outLTC { get; set; }
    public int outETH { get; set; }
    public int outETC { get; set; }
    public int outBTS { get; set; }
    public int outEOS { get; set; }
}

public class Result
{
    public Base @base { get; set; }
    public double totalAssets { get; set; }
    public double netAssets { get; set; }
    public Balance balance { get; set; }
    public Frozen frozen { get; set; }
    public P2p p2p { get; set; }
}

public class AccountInfoCHBTC
{
    public Result result { get; set; }
}