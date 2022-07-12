namespace API.Tests;

using API.Controllers;
using API.Entities;
using API.Helpers;
using API.Interfaces;
using API.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

public class BillsControllerUnitTest
{
    [Fact]
    public async void GetBillsMustSucceed()
    {
        // Given
        Moq.Mock<IBillRepository> mockBillRepo = new Moq.Mock<IBillRepository>();
        Moq.Mock<IBillTypeRepository> mockBillTypeRepo = new Moq.Mock<IBillTypeRepository>();
        Moq.Mock<IMapper> mockMapper = new Moq.Mock<IMapper>();

        var controller = new BillsController(mockBillRepo.Object, mockBillTypeRepo.Object, mockMapper.Object);
        controller.ControllerContext.HttpContext = new DefaultHttpContext();

        var bills = new List<Bill> { new Bill { Id = 1}, new Bill { Id = 2}};
        var list = new PagedList<Bill>(bills, 2, 1, 10);

        mockBillRepo.Setup(x => x.GetBillsAsync("user1", It.IsAny<UserParams>())).Returns(Task.FromResult(list));
        mockMapper.Setup(x => x.Map<IEnumerable<BillDto>>(It.IsAny<PagedList<Bill>>())).Returns(
            new List<BillDto> { new BillDto { Id = 1}, new BillDto { Id = 2}}
        );
        
        // When
        var result = await controller.GetBills("user1", new Mock<UserParams>().Object);

        Assert.NotNull(result.Result);
        
        var resultCast = (OkObjectResult)result.Result!;
        var resultList = (IEnumerable<BillDto>)resultCast.Value!;
        // Then
        Assert.True(resultList.Count() == 2);
    }

    [Fact]
    public async void UpdateBillMustSucceed()
    {
        // Given
        Moq.Mock<IBillRepository> mockBillRepo = new Moq.Mock<IBillRepository>();
        Moq.Mock<IBillTypeRepository> mockBillTypeRepo = new Moq.Mock<IBillTypeRepository>();
        Moq.Mock<IMapper> mockMapper = new Moq.Mock<IMapper>();

        var controller = new BillsController(mockBillRepo.Object, mockBillTypeRepo.Object, mockMapper.Object);

        var bill = new Bill() { Id = 1, Value = 15.5f};
        mockBillRepo.Setup(x => x.GetBillByIdAsync(It.IsAny<int>())).Returns(Task.FromResult(bill));

        // When
        var result = await controller.Update(1, new DTOs.BillRegisterDto() {TypeId = 1, Value = 20.5f, Month = 2, Year = 2022, UserId = 1});

        // Then
        mockBillRepo.Verify(m => m.SaveAllAsync(), Times.Once);
        Assert.True(result is OkResult);
    }

    [Fact]
    public async void DeleteBillMustSucceed()
    {
        // Given
        Moq.Mock<IBillRepository> mockBillRepo = new Moq.Mock<IBillRepository>();
        Moq.Mock<IBillTypeRepository> mockBillTypeRepo = new Moq.Mock<IBillTypeRepository>();
        Moq.Mock<IMapper> mockMapper = new Moq.Mock<IMapper>();

        var controller = new BillsController(mockBillRepo.Object, mockBillTypeRepo.Object, mockMapper.Object);

        var bill = new Bill() { Id = 1, Value = 15.5f};
        mockBillRepo.Setup(x => x.GetBillByIdAsync(It.IsAny<int>())).Returns(Task.FromResult(bill));
        
        // When
        var result = await controller.Delete(1);

        // Then
        mockBillRepo.Verify(m => m.Delete(It.IsAny<Bill>()), Times.Once);
        mockBillRepo.Verify(m => m.SaveAllAsync(), Times.Once);
        Assert.True(result is OkResult);
    }
}