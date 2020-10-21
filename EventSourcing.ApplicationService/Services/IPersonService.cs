using EventSourcing.ApplicationService.Dtos;
using System.Threading.Tasks;
namespace EventSourcing.ApplicationService.Services
{
    public interface IPersonService
    {
        Task Create(PersonDto person);
        Task<PersonDto> Get(string personId);

    }
}