using Moq;
using NUnit.Framework;
using RDI.Challenge.DataContext;
using RDI.Challenge.Domain.Entities;
using RDI.Challenge.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RDI.Challenge.Test
{
    public class OrderUnitTest
    {

        public TestContext TestContext { get; set; }

        /// <summary>
        /// Our Mock Orders Repository for use in testing
        /// </summary>
        public IOrderRepository MockOrdersRepository;
        public List<MenuItem> lista;
        private long OrderId { get; set; }


        [SetUp]
        public void Setup()
        {

            Mock<IOrderRepository> _mockOrderRepository;
            Mock<ChallengeContext> mockContext;
            lista = new List<MenuItem>() {
            new MenuItem
            {
                MenuItemId = 1, Name = "French Fries", Area = "fries", Description = "Potato Fries"
            },
            new MenuItem
            {
                MenuItemId = 2, Name = "T-Bone", Area = "grill", Description = "T-Bone Steak"
            },
            new MenuItem
            {
                MenuItemId = 3, Name = "Ceaser", Area = "salad", Description = "Salad with Ceaser Sauce"
            },
            new MenuItem
            {
                MenuItemId = 4, Name = "Coca Cola", Area = "drink", Description = "Soda"
            },
            new MenuItem
            {
                MenuItemId = 5, Name = "Ice Cream", Area = "desert", Description = "Ice Cream"
            }};          

            OrderId = 1;
            var orderList = new List<Order>() {
            new Order() { OrderId = OrderId, MenuItems = lista },
            new Order() { OrderId = 2, MenuItems = lista },
            new Order() { OrderId = 3, MenuItems = lista }
            };             


            _mockOrderRepository = new Mock<IOrderRepository>();
            mockContext = new Mock<ChallengeContext>();
            _mockOrderRepository.Setup(x => x.GetAll()).Returns(
                Task.FromResult<IEnumerable<Order>>(
                   orderList
                    ));  

            _mockOrderRepository.Setup(x => x.Get(
              It.IsAny<long>())).Returns(
                Task.FromResult<Order>(
                    orderList.Where(x => x.OrderId == OrderId).Single()
                    ));

            this.MockOrdersRepository = _mockOrderRepository.Object;
        }
        [Test]
        public void AddOrder()
        {
            this.MockOrdersRepository.Add(new Order() { OrderId = OrderId, MenuItems = lista });            
        }
        [Test]
        public void CanOrderGetAll()
        {
            var ret = this.MockOrdersRepository.GetAll();

            Assert.IsTrue(ret.Result.Count() > 0);

        }
        [Test]
        public void CanOrderGetById()
        {
            var ret = this.MockOrdersRepository.Get(OrderId);

            Assert.IsNotNull(ret);
        }
    }
}