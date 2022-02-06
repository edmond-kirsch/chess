namespace Chess.Core.DomainModels;

public class JwtTokenModel
{
    public string Token { get; set; }
    public DateTime ValidTo { get; set; }
}