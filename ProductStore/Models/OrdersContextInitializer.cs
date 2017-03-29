namespace WizzAir.Models
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;

    public class OrdersContextInitializer : DropCreateDatabaseAlways<OrdersContext>
    {
        protected override void Seed(OrdersContext context)
        {
            var specialOffers = new List<SpecialOffer>()            
            {
                new SpecialOffer() { Name = "Crvena Zvezda", Place = 1, Points = 35, Wins = 15, Loses = 5, AvgPoints = 80 },
                new SpecialOffer() { Name = "Partizan", Place = 2, Points = 35, Wins = 15, Loses = 5, AvgPoints = 75 },
                new SpecialOffer() { Name = "Igokea", Place = 3, Points = 35, Wins = 15, Loses = 5, AvgPoints = 70 }
            };

            specialOffers.ForEach(p => context.SpecialOffers.Add(p));
            context.SaveChanges();

            var order = new Order() { User = "Nemanja" };
            var od = new List<OrderDetail>()
            {
                new OrderDetail() { SpecialOffer = specialOffers[0], WinPercent = 70, Order = order},
                new OrderDetail() { SpecialOffer = specialOffers[1], WinPercent = 30, Order = order }
            };
            context.Orders.Add(order);
            od.ForEach(o => context.OrderDetails.Add(o));

            context.SaveChanges();
        }
    }
}