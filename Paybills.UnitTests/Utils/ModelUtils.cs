using Paybills.API.DTOs;
using Paybills.API.Entities;
using Paybills.API.Helpers;

namespace Paybills.UnitTests.Utils;

static class ModelUtils
{
    #region Bill
    public static Bill GenerateRandomBill()
    {
        return new Bill { 
            Id = ((int)new Random().NextInt64(1, 99999)),
            Value = (new Random().NextSingle() * 1000), 
            Month = ((int)new Random().NextInt64(1, 13)),
            Year = ((int)new Random().NextInt64(2020, 2030))
        };
    }

    public static List<Bill> GenerateRandomBillsList(int size)
    {
        var result = new List<Bill>();

        while (size > 0)
        {
            result.Add(GenerateRandomBill());
            size--;
        }

        return result;
    }

    public static PagedList<Bill> GenerateRandomPagedBillsList(int size)
    {
        return new PagedList<Bill>(GenerateRandomBillsList(size), size, 1, 10);
    }
    #endregion

    #region BillDto
    public static BillDto GenerateRandomBillDto()
    {
        return new BillDto {
            Id = ((int)new Random().NextInt64(1, 99999)),
            Value = (new Random().NextSingle() * 1000), 
            Month = ((int)new Random().NextInt64(1, 13)),
            Year = ((int)new Random().NextInt64(2020, 2030))
        };
    }

    public static List<BillDto> GenerateRandomBillDtoList(int size)
    {
        var result = new List<BillDto>();

        while (size > 0)
        {
            result.Add(GenerateRandomBillDto());
            size--;
        }

        return result;
    }
    #endregion

    #region BillType
    public static BillType GenerateRandomBillType()
    {
        return new BillType {
            Id = DataUtils.RandomInt(1, 50),
            Description = DataUtils.RandomString(50),
            Active = true
        };
    }
    #endregion
}