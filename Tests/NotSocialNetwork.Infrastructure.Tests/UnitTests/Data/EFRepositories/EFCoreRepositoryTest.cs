using Microsoft.EntityFrameworkCore;
using NotSocialNetwork.Application.Entities;
using NotSocialNetwork.Data.EFRepositories;
using NotSocialNetwork.DBContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace NotSocialNetwork.Infrastructure.Tests.UnitTests.Data.EFRepositories
{
    public class EFCoreRepositoryTest : IDisposable
    {
        public EFCoreRepositoryTest()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _appDbContext = new AppDbContext(options);

            _appDbContext.Database.EnsureCreated();

            Seed(_appDbContext);
        }

        private readonly AppDbContext _appDbContext;

        private void Seed(AppDbContext appDbContext)
        {
            appDbContext.AddRange(_users);

            appDbContext.SaveChanges();
        }

        private readonly IQueryable<UserEntity> _users = new List<UserEntity>()
        {
            new UserEntity()
            {
                Name = "Name1",
            },
            new UserEntity()
            {
                Name = "Name2",
            },
        }.AsQueryable();

        private readonly UserEntity _user = new UserEntity()
        {
            Id = Guid.NewGuid(),
            Name = "Name",
        };

        [Fact]
        public void GetAll_GetAllObjects_Objects()
        {
            // Arrange
            var repository = new EFCoreRepository<UserEntity>(_appDbContext);

            // Act
            var result = repository.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(_users, result);
            Assert.Equal(2, result.Count());
            Assert.Equal("Name2", result
                                    .ToList()
                                    .ElementAt(1).Name);
        }

        [Fact]
        public async Task AddAsync_AddObject_Object()
        {
            // Arrange
            var repository = new EFCoreRepository<UserEntity>(_appDbContext);

            // Act
            var result = await repository.AddAsync(_user);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(_user, result);
            Assert.Equal("Name", result.Name);
        }

        [Fact]
        public async Task DeleteAsync_DeleteObject_Object()
        {
            // Arrange
            var repository = new EFCoreRepository<UserEntity>(_appDbContext);

            // Act
            var result = await repository.DeleteAsync(_users.First().Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(_users.First(), result);
        }

        [Fact]
        public async Task GetAsync_GetObject_Object()
        {
            // Arrange
            var repository = new EFCoreRepository<UserEntity>(_appDbContext);

            // Act
            var result = await repository.GetAsync(_users.First().Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Name1", result.Name);
            Assert.Equal(_users.First().Id, result.Id);
        }

        [Fact]
        public async Task UpdateAsync_UpdateObject_Object()
        {
            // Arrange
            var repository = new EFCoreRepository<UserEntity>(_appDbContext);

            var user = new UserEntity()
            {
                Id = _users.First().Id,
                Name = "TestName",
            };

            // Act
            var result = await repository.UpdateAsync(user);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("TestName", result.Name);
            Assert.NotEqual(_users.First(), result);
            Assert.Equal(_users.First().Id, result.Id);
        }

        public void Dispose()
        {
            _appDbContext.Database.EnsureDeleted();
            _appDbContext.DisposeAsync();
        }
    }
}
