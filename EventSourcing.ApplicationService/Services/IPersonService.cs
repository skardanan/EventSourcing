using EventSourcing.ApplicationService.Dtos;
using System.Threading.Tasks;
namespace EventSourcing.ApplicationService.Services
{
    public interface IPersonService
    {
        Task<bool> Create(PersonDto person);
        Task<PersonDto> Get(string personId);
        Task<bool> Update(string personId, PersonDto person);
        Task<bool> Delete(string personId);
    }
}