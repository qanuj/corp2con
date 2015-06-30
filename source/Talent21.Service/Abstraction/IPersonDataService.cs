using Talent21.Service.Models;

namespace Talent21.Service.Abstraction
{
    public interface IPersonDataService<TEditModel, TCreateModel, TDeleteModel> : IDataService<TEditModel, TCreateModel, TDeleteModel>
        where TCreateModel : PersonViewModel
        where TEditModel : PersonEditViewModel
        where TDeleteModel : IdModel
    {
    }
}