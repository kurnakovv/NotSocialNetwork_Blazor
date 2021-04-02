using Moq;
using NotSocialNetwork.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using Xunit;
using System.Linq;
using NotSocialNetwork.DBContexts;
using NotSocialNetwork.Data.EFRepositories;
using NotSocialNetwork.Application.Entities.Abstract;
using NotSocialNetwork.Application.Entities;
using Microsoft.EntityFrameworkCore;

namespace NotSocialNetwork.Infrastructure.Tests.Data.EFRepositories
{
    public class RepositoryTest : IDisposable
    {
        public RepositoryTest()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase("TestDb")
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
        public void Add_AddObject_Object()
        {
            // Arrange
            var repository = new EFCoreRepository<UserEntity>(_appDbContext);

            // Act
            var result = repository.Add(_user);
            repository.Commit();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(_user, result);
            Assert.Equal("Name", result.Name);
        }

        [Fact]
        public void Delete_DeleteObject_Object()
        {
            // Arrange
            var repository = new EFCoreRepository<UserEntity>(_appDbContext);

            // Act
            var result = repository.Delete(_users.First().Id);
            repository.Commit();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(_users.First(), result);
        }

        [Fact]
        public void Get_GetObject_Object()
        {
            // Arrange
            var repository = new EFCoreRepository<UserEntity>(_appDbContext);

            // Act
            var result = repository.Get(_users.First().Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Name1", result.Name);
            Assert.Equal(_users.First().Id, result.Id);
        }

        [Fact]
        public void Update_UpdateObject_Object()
        {
            // Arrange
            var repository = new EFCoreRepository<UserEntity>(_appDbContext);

            var user = new UserEntity()
            {
                Id = _users.First().Id,
                Name = "TestName",
            };

            // Act
            var result = repository.Update(user);
            repository.Commit();

            // Assert
            Assert.NotNull(result);
            Assert.Equal("TestName", result.Name);
            Assert.NotEqual(_users.First(), result);
            Assert.Equal(_users.First().Id, result.Id);
        }

        public void Dispose()
        {
            _appDbContext.Database.EnsureDeleted();
            _appDbContext.Dispose();
        }
    }
}