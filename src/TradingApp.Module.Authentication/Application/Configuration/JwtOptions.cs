﻿namespace TradingApp.Module.Quotes.Authentication.Configuration;

public class JwtOptions
{
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public string SecretKey { get; set; }
}
