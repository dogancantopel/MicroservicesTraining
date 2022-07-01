﻿using Dapper;
using FreeCources.Shared.Dtos;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace FreeCourse.Services.Discount.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly IConfiguration _configuration;
        private readonly IDbConnection _dbConnection;

        public DiscountService(IConfiguration configuration)
        {
            _configuration = configuration;
            _dbConnection = new NpgsqlConnection(_configuration.GetConnectionString("PostgreSql"));
        }

        public async Task<Response<NoContent>> Delete(int id)
        {
            var status = await _dbConnection.ExecuteAsync("delete from  discount where id=@Id", new { Id = id});
            if (status > 0)
                return Response<NoContent>.Success(204);

            return Response<NoContent>.Fail("Discount not found", 404);
        }

        public async Task<Response<List<Models.Discount>>> GetAll()
        {
            var discountList = await _dbConnection.QueryAsync<Models.Discount>("Select * from discount");
            return Response<List<Models.Discount>>.Success(discountList.ToList(), 200);
        }

        public async Task<Response<Models.Discount>> GetByCodeAndUserId(string code, string userId)
        {
            var discount = (await _dbConnection.QueryAsync<Models.Discount>("select * from discount where userId=@UserId and code=@Code", new { UserId = userId,Code=code })).SingleOrDefault();

            if (discount == null)
                return Response<Models.Discount>.Fail("Discount not found", 404);

            return Response<Models.Discount>.Success(discount, 200);
        }

        public async Task<Response<Models.Discount>> GetById(int id)
        {
            var discount = (await _dbConnection.QueryAsync<Models.Discount>("select * from discount where id=@Id", new { Id = id })).SingleOrDefault();

            if (discount == null)
                return Response<Models.Discount>.Fail("Discount not found", 404);

            return Response<Models.Discount>.Success(discount, 200);
        }

        public async Task<Response<NoContent>> Save(Models.Discount discount)
        {
            var status = await _dbConnection.ExecuteAsync("INSERT INTO discount(userid,rate,code) VALUES(@UserId,@Rate,@Code)", discount);

            if (status > 0)
                return Response<NoContent>.Success(204);

            return Response<NoContent>.Fail("An Error occured while adding", 500);
        }

        public async Task<Response<NoContent>> Update(Models.Discount discount)
        {
            var status = await _dbConnection.ExecuteAsync("update  discount set userid=@UserId,rate=@Rate,code=@Code where id=@Id", new { Id = discount.Id, UserId = discount.UserId, Rate = discount.Rate, Code = discount.Code });
            if (status > 0)
                return Response<NoContent>.Success(204);

            return Response<NoContent>.Fail("Discount not found",404);
        }
    }
}
