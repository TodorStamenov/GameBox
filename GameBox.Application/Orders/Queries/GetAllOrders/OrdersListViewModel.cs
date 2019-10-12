using AutoMapper;
using GameBox.Application.Contracts.Mapping;
using GameBox.Domain.Entities;

namespace GameBox.Application.Orders.Queries.GetAllOrders
{
    public class OrdersListViewModel : IHaveCustomMapping
    {
        public string Username { get; set; }

        public string TimeStamp { get; set; }

        public decimal Price { get; set; }

        public int GamesCount { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration
                .CreateMap<Order, OrdersListViewModel>()
                .ForMember(cfg => cfg.Username, opt => opt.MapFrom(src => src.User.Username))
                .ForMember(cfg => cfg.GamesCount, opt => opt.MapFrom(src => src.Games.Count))
                .ForMember(cfg => cfg.TimeStamp, opt => opt.MapFrom(src => src.TimeStamp.ToShortDateString()));
        }
    }
}
