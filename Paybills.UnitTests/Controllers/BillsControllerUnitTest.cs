using Moq;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Paybills.API.Controllers;
using Paybills.API.DTOs;
using Paybills.API.Entities;
using Paybills.API.Helpers;
using Paybills.API.Interfaces;
using FluentAssertions;
using System.Net;
using Paybills.UnitTests.Utils;

namespace Paybills.UnitTests.Controllers
{
    public class BillsControllerUnitTest
    {
        [Fact]
        public async void GetBills_MustSucceed()
        {
            var expectedSize = 2;
            var userName = DataUtils.RandomString(20);

            // Given
            Mock<IBillRepository> mockBillRepo = new Mock<IBillRepository>();
            Mock<IBillTypeRepository> mockBillTypeRepo = new Mock<IBillTypeRepository>();
            Mock<IMapper> mockMapper = new Mock<IMapper>();

            var controller = new BillsController(mockBillRepo.Object, mockBillTypeRepo.Object, mockMapper.Object);
            controller.ControllerContext.HttpContext = new DefaultHttpContext();

            var list = ModelUtils.GenerateRandomPagedBillsList(expectedSize);

            mockBillRepo.Setup(repo => repo.GetBillsAsync(
                userName, It.IsAny<UserParams>())).Returns(Task.FromResult(list));
            
            mockMapper.Setup(mapper => mapper.Map<IEnumerable<BillDto>>(It.IsAny<PagedList<Bill>>())).Returns(
                ModelUtils.GenerateRandomBillDtoList(expectedSize)
            );

            // When
            var result = await controller.GetBills(userName, new Mock<UserParams>().Object);

            Assert.NotNull(result.Result);

            var resultCast = (OkObjectResult)result.Result!;
            var resultList = (IEnumerable<BillDto>)resultCast.Value!;
            
            // Then
            Assert.True(resultList.Count() == expectedSize);
            result.Result.Should().BeOfType<OkObjectResult>().Which.StatusCode.Should().Be((int)HttpStatusCode.OK);
        }

        [Fact]
        public async void GetBillsByDate_MustSucceed()
        {
             // Given
            var userName = DataUtils.RandomString(20);

            Mock<IBillRepository> mockBillRepo = new Mock<IBillRepository>();
            Mock<IBillTypeRepository> mockBillTypeRepo = new Mock<IBillTypeRepository>();
            Mock<IMapper> mockMapper = new Mock<IMapper>();

            var controller = new BillsController(mockBillRepo.Object, mockBillTypeRepo.Object, mockMapper.Object);
            controller.ControllerContext.HttpContext = new DefaultHttpContext();

            var bills = new List<Bill> { new Bill { Id = 1 }, new Bill { Id = 2 } };
            var list = new PagedList<Bill>(bills, 2, 1, 10);

            mockBillRepo.Setup(x => x.GetBillsByDateAsync(
                userName, It.IsAny<int>(), It.IsAny<int>(), It.IsAny<UserParams>())).Returns(Task.FromResult(list));
            mockMapper.Setup(x => x.Map<IEnumerable<BillDto>>(It.IsAny<PagedList<Bill>>())).Returns(
                new List<BillDto> { new BillDto { Id = 1 }, new BillDto { Id = 2 } }
            );

            // When
            var result = await controller.GetBillsByDate(userName, 1, 2022, new Mock<UserParams>().Object);

            // Then
            result.Result.Should().BeOfType<OkObjectResult>().Which.StatusCode.Should().Be((int)HttpStatusCode.OK);
        }

        [Fact]
        public async void GetBill_MustSucceed()
        {
            // Given
            var billId = DataUtils.RandomInt(1, 100);
            Mock<IBillRepository> mockBillRepo = new Mock<IBillRepository>();
            Mock<IBillTypeRepository> mockBillTypeRepo = new Mock<IBillTypeRepository>();
            Mock<IMapper> mockMapper = new Mock<IMapper>();

            var controller = new BillsController(mockBillRepo.Object, mockBillTypeRepo.Object, mockMapper.Object);
            
            // When
            var result = await controller.GetBill(billId);
        
            // Then
            mockBillRepo.Verify(m => m.GetBillByIdAsync(billId));
            result.Result.Should().BeOfType<OkObjectResult>().Which.StatusCode.Should().Be((int)HttpStatusCode.OK);
        }

        [Fact]
        public async void UpdateBill_MustSucceed()
        {
            // Given
            Mock<IBillRepository> mockBillRepo = new Moq.Mock<IBillRepository>();
            Mock<IBillTypeRepository> mockBillTypeRepo = new Moq.Mock<IBillTypeRepository>();
            Mock<IMapper> mockMapper = new Moq.Mock<IMapper>();

            var controller = new BillsController(mockBillRepo.Object, mockBillTypeRepo.Object, mockMapper.Object);

            var bill = new Bill() { Id = 1, Value = 15.5f };
            mockBillRepo.Setup(x => x.GetBillByIdAsync(It.IsAny<int>())).Returns(Task.FromResult(bill));

            // When
            var result = await controller.Update(1, new BillRegisterDto() { 
                TypeId = 1, Value = 20.5f, Month = 2, Year = 2022, UserId = 1 });

            // Then
            mockBillRepo.Verify(m => m.SaveAllAsync(), Times.Once);
            Assert.True(result is OkResult);
        }

        [Fact]
        public async void DeleteBill_MustSucceed()
        {
            // Given
            Mock<IBillRepository> mockBillRepo = new Moq.Mock<IBillRepository>();
            Mock<IBillTypeRepository> mockBillTypeRepo = new Moq.Mock<IBillTypeRepository>();
            Mock<IMapper> mockMapper = new Moq.Mock<IMapper>();

            var controller = new BillsController(mockBillRepo.Object, mockBillTypeRepo.Object, mockMapper.Object);

            var bill = new Bill() { Id = 1, Value = 15.5f };
            mockBillRepo.Setup(x => x.GetBillByIdAsync(It.IsAny<int>())).Returns(Task.FromResult(bill));

            // When
            var result = await controller.Delete(1);

            // Then
            mockBillRepo.Verify(m => m.Delete(It.IsAny<Bill>()), Times.Once);
            mockBillRepo.Verify(m => m.SaveAllAsync(), Times.Once);
            Assert.True(result is OkResult);
        }

        [Fact]
        public async void DeleteInexistentBill_ShouldReturnNotFound()
        {
            // Given
            Mock<IBillRepository> mockBillRepo = new Moq.Mock<IBillRepository>();
            Mock<IBillTypeRepository> mockBillTypeRepo = new Moq.Mock<IBillTypeRepository>();
            Mock<IMapper> mockMapper = new Moq.Mock<IMapper>();

            var controller = new BillsController(mockBillRepo.Object, mockBillTypeRepo.Object, mockMapper.Object);

            Bill bill = null;
            mockBillRepo.Setup(x => x.GetBillByIdAsync(It.IsAny<int>())).Returns(Task.FromResult(bill));

            // When
            var result = await controller.Delete(1);

            // Then
            mockBillRepo.Verify(m => m.Delete(It.IsAny<Bill>()), Times.Never);
            mockBillRepo.Verify(m => m.SaveAllAsync(), Times.Never);
            Assert.True(result is NotFoundResult);
        }

        [Fact]
        public async void CreateBill_MustSucceed()
        {
            // Given
            var billRegDto = new BillRegisterDto() { TypeId = DataUtils.RandomInt(1, 50)};
            var billType = ModelUtils.GenerateRandomBillType();
            Mock<IBillRepository> mockBillRepo = new Moq.Mock<IBillRepository>();
            Mock<IBillTypeRepository> mockBillTypeRepo = new Moq.Mock<IBillTypeRepository>();
            Mock<IMapper> mockMapper = new Moq.Mock<IMapper>();

            var controller = new BillsController(mockBillRepo.Object, mockBillTypeRepo.Object, mockMapper.Object);

            mockBillTypeRepo.Setup(m => m.GetBillTypeByIdAsync(billRegDto.TypeId)).Returns(Task.FromResult(billType));

            // When
            var result = await controller.Create(billRegDto);
        
            // Then
            mockBillTypeRepo.Verify(m => m.GetBillTypeByIdAsync(billRegDto.TypeId), Times.Once);
            mockBillRepo.Verify(m => m.Create(It.IsAny<Bill>()));
        }

        [Fact]
        public async void CopyBillsToNextMonth_MustSucceed()
        {
            // Given
            Mock<IBillRepository> mockBillRepo = new Moq.Mock<IBillRepository>();
            Mock<IBillTypeRepository> mockBillTypeRepo = new Moq.Mock<IBillTypeRepository>();
            Mock<IMapper> mockMapper = new Moq.Mock<IMapper>();
            var controller = new BillsController(mockBillRepo.Object, mockBillTypeRepo.Object, mockMapper.Object);
            var periodData = new PeriodDataDto() { UserId = 1, CurrentMonth = 8, CurrentYear = 2022 };
            
            // When
            var result = await controller.CopyBillsToNextMonth(periodData);
            
            // Then
            mockBillRepo.Verify(m => m.CopyBillsToNextMonth(periodData.UserId, periodData.CurrentMonth, periodData.CurrentYear), Times.Once);
            mockBillRepo.Verify(m => m.SaveAllAsync(), Times.Once);
        }
    }
}