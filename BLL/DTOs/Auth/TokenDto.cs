namespace Ecommerce.BLL
{
    public record TokenDto(string Token, int DurationInMinutes, string TokenType = "Bearer");
  
}
