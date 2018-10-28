namespace GameBox.Services.Models.View.Orders
{
    public class OrderViewModel
    {
        public string Username { get; set; }

        public string TimeStamp { get; set; }

        public decimal Price { get; set; }

        public int GamesCount { get; set; }
    }
}