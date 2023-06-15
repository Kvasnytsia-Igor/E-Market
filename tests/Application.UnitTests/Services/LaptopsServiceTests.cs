using Application.Common.Interfaces;
using Application.Services;
using Domain.Entities;
using Moq;

namespace Application.UnitTests.Services;

public class LaptopsServiceTests : BaseServiceTest
{
    private readonly Mock<IApplicationDbContext> mockDbContext = new();

    private ILaptopsService _laptopsService;

    private readonly List<Laptop> _data = new()
    {
        new Laptop
        {
            Id = Guid.Parse("a4c31a9c-5f1f-4b23-b79e-11c1fb35e23a"),
            Brand = "Brand1",
            Price = 25,
            Series = "Series1",
        },
        new Laptop
        {
            Id = Guid.Parse("c7c8279a-6d5b-431f-a87c-997cb2f5e668"),
            Brand = "Brand2",
            Price = 25,
            Series = "Series2",
        },
        new Laptop
        {
            Id = Guid.Parse("d418826f-638d-4cde-8729-78807be9a0bb"),
            Brand = "Brand3",
            Price = 25,
            Series = "Series3",
        }
    };

    [SetUp]
    public void Setup()
    {
        mockDbContext.Setup(prop => prop.Laptops).Returns(GetQueryableMockDbSet(_data));
        mockDbContext.Setup(prop => prop.SaveChangesAsync(new CancellationToken())).ReturnsAsync(0);
        _laptopsService = new LaptopsService(mockDbContext.Object, null!);
    }

    [Test]
    public async Task GetFirstByIdAsyncReturnsNotNull()
    {
        Laptop? laptop = await _laptopsService.GetFirstByIdAsync(Guid.Parse("a4c31a9c-5f1f-4b23-b79e-11c1fb35e23a"));
        Assert.That(laptop, Is.Not.Null);
    }


    [Test]
    public async Task GetFirstByIdAsyncReturnsNull()
    {
        Laptop? laptop = await _laptopsService.GetFirstByIdAsync(Guid.Parse("a4c31a9c-5f1f-4b23-b79e-11c1fb35e23b"));
        Assert.That(laptop, Is.Null);
    }

    [Test]
    public async Task GetFirstByIdAsyncReturnsRightObject()
    {
        Laptop? laptop = await _laptopsService.GetFirstByIdAsync(Guid.Parse("a4c31a9c-5f1f-4b23-b79e-11c1fb35e23a"))
            ?? throw new NullReferenceException();
        Assert.Multiple(() =>
        {
            Assert.That(laptop.Brand, Is.EqualTo("Brand1"));
            Assert.That(laptop.Series, Is.EqualTo("Series1"));
            Assert.That(laptop.Price, Is.EqualTo(25));
        });
    }
}
