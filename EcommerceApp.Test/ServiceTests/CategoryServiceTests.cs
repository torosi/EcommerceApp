using EcommerceApp.Domain.Interfaces.Repositories;
using EcommerceApp.Domain.Models.Category;
using EcommerceApp.Service.Contracts;
using EcommerceApp.Service.Implementations;
using Moq;

namespace EcommerceApp.Test.ServiceTests
{
    public class CategoryServiceTests
    {
        private readonly ICategoryService _categoryService;
        private readonly Mock<ICategoryRepository> _categoryRepositoryMock;
        private readonly Mock<IProductRepository> _productRepositoryMock;

        public CategoryServiceTests()
        {
            _categoryRepositoryMock = new Mock<ICategoryRepository>();
            _productRepositoryMock = new Mock<IProductRepository>();

            _categoryService = new CategoryService(_categoryRepositoryMock.Object, _productRepositoryMock.Object);
        }

        [Fact]
        public async Task AddAsync_Should_Add_Category_Correctly()
        {
            // Arrange
            var categoryModel = new CreateCategoryModel
            {
                Id = 0, 
                Created = DateTime.Now,
                Updated = DateTime.Now,
                Name = "Test category",
                Description = "test description",
                ImageUrl = "testimageurl"
            };

            _categoryRepositoryMock.Setup(r => r.AddAsync(It.IsAny<CreateCategoryModel>())).Returns(Task.CompletedTask);
            _categoryRepositoryMock.Setup(r => r.SaveChangesAsync()).Returns(Task.CompletedTask);

            // Act
            await _categoryService.AddAsync(categoryModel);

            // Assert
            _categoryRepositoryMock.Verify(r => r.AddAsync(It.IsAny<CreateCategoryModel>()), Times.Once);
            _categoryRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task AddAsync_NullCategory_Should_Throw_ArgumentNullException()
        {

            // Arrange
            // arrange not needed as method should throw exception before repositories are called.
            
            // Act
            await Assert.ThrowsAsync<ArgumentNullException>(() => _categoryService.AddAsync(null));

            // Assert
            _categoryRepositoryMock.Verify(r => r.AddAsync(It.IsAny<CreateCategoryModel>()), Times.Never);
            _categoryRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Never);
        }

        [Fact]
        public async Task AddAsync_Calls_SaveChangesAsync_After_AddAsync()
        {
            // Arrange
            var categoryModel = new CreateCategoryModel
            {
                Id = 0,
                Created = DateTime.Now,
                Updated = DateTime.Now,
                Name = "Test category",
                Description = "test description",
                ImageUrl = "testimageurl"
            };

            var sequence = new MockSequence();
            _categoryRepositoryMock.InSequence(sequence).Setup(r => r.AddAsync(It.IsAny<CreateCategoryModel>()));
            _categoryRepositoryMock.InSequence(sequence).Setup(r => r.SaveChangesAsync());

            // Act
            await _categoryService.AddAsync(categoryModel);

            // Assert
            _categoryRepositoryMock.Verify(r => r.AddAsync(It.IsAny<CreateCategoryModel>()), Times.Once);
            _categoryRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

    }
}
