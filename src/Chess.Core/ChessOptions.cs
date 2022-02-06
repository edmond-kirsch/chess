namespace Chess.Core;

public class ChessOptions
{
    public JwtOptions Jwt { get; set; }
}

public class JwtOptions
{
    public string ValidAudience { get; set; }
    public string ValidIssuer { get; set; }
    public string Secret { get; set; }
}