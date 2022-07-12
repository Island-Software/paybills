namespace API.Tests;

using API.Controllers;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;

public class BillsControllerUnitTest
{
    [Fact]
    public async void TestUpdate()
    {
        Moq.Mock<IBillRepository> mockBillRepo = new Moq.Mock<IBillRepository>();
        Moq.Mock<IBillTypeRepository> mockBillTypeRepo = new Moq.Mock<IBillTypeRepository>();

        Moq.Mock<IMapper> mockMapper = new Moq.Mock<IMapper>();
        var controller = new BillsController(mockBillRepo.Object, mockBillTypeRepo.Object, mockMapper.Object);

        var bill = new Bill() { Id = 1, Value = 15.5f};
        mockBillRepo.Setup(x => x.GetBillByIdAsync(It.IsAny<int>())).Returns(Task.FromResult(bill));

        ActionResult result = await controller.Update(1, new DTOs.BillRegisterDto() {TypeId = 1, Value = 20.5f, Month = 2, Year = 2022, UserId = 1});

        mockBillRepo.Verify(m => m.SaveAllAsync(), Times.Once);
        Assert.True(result is OkResult);
    }
}