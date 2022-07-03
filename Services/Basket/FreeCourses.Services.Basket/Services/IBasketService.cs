using FreeCourse.Shared.Dtos;
using FreeCourses.Services.Basket.Dtos;
using System.Threading.Tasks;

namespace FreeCourses.Services.Basket.Services
{
    public interface IBasketService
    {
        Task<Response<BasketDto>> GetBasket(string userId);

        Task<Response<bool>> SaveOrUpdate(BasketDto basketDto);

        Task<Response<bool>> DeleteBasket(string userId);
    }
}
