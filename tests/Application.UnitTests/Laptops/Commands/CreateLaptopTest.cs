using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Laptops.Commands.CreateLaptop;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Application.UnitTests.Laptops.Commands;

public class CreateLaptopTest
{
    private Mock<IApplicationDbContext> _mockDbContext;

    private static readonly List<Laptop> _laptops = new()
    {
        new Laptop
        {
            Id = new Guid("81a130d2-502f-4cf1-a376-63edeb000e9f"),
            Brand = "Brand1",
            Price = 1,
            Series = "Series1",
            Created = new DateTime(2023, 5, 10)
        },
        new Laptop
        {
            Id = new Guid("e1038826-59d9-4e9b-a8dd-320c334a5372"),
            Brand = "Brand2",
            Price = 15,
            Series = "Series2",
            Created = new DateTime(2023, 5, 11)
        },
        new Laptop
        {
            Id = new Guid("c81ef2d8-8a22-4955-9e1f-6ce7a4a0b54f"),
            Brand = "Brand3",
            Price = 16,
            Series = "Series3",
            Created = new DateTime(2023, 5, 12)
        },
    };

    [SetUp]
    public void Setup()
    {
        _mockDbContext = new Mock<IApplicationDbContext>();
        _mockDbContext.Setup(x => x.Laptops).Returns(GetQueryableMockDbSet(_laptops));
    }

    [Test]
    public async Task CreateLaptopCommand_Handle_SavedLaptop()
    {
        //Arrange
        IApplicationDbContext dbContext = _mockDbContext.Object;
        CreateLaptopCommandHandler createLaptopCommandHandler = new(dbContext);
        //Act
        IApiResponse apiResponse = await createLaptopCommandHandler.Handle(new CreateLaptopCommand
        {
            Brand = "TestBrand",
            Price = 25,
            Series = "TestSeries"
        }, new CancellationToken());
        //Assert
        _mockDbContext.Verify(a => a.Laptops, Times.Exactly(2));
        Assert.Multiple(() =>
        {
            Assert.That(apiResponse.Result, Is.InstanceOf<Laptop>());
            Assert.That(dbContext.Laptops.ToArray(), Has.Exactly(4).Items);
            if (apiResponse.Result is Laptop laptop)
            {
                Assert.That(laptop.Brand, Is.EqualTo("TestBrand"));
                Assert.That(laptop.Price, Is.EqualTo(25));
                Assert.That(laptop.Series, Is.EqualTo("TestSeries"));
            }
        });
    }

    private static DbSet<T> GetQueryableMockDbSet<T>(List<T> sourceList) where T : class
    {
        IQueryable<T> queryable = sourceList.AsQueryable();
        Mock<DbSet<T>> dbSet = new();
        dbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
        dbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
        dbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
        dbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());
        dbSet.Setup(d => d.Add(It.IsAny<T>())).Callback<T>(sourceList.Add);
        return dbSet.Object;
    }
}
