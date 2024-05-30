namespace MvcMovie.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public int Quantity { get; set; }
        public string CartId { get; set; } = null!;

        public Movie Movie { get; set; } = null!;

    }
}