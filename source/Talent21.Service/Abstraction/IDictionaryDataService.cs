using Talent21.Service.Models;

namespace Talent21.Service.Abstraction
{
    public interface IDictionaryDataService<TEditModel, in TCreateModel, in TDeleteModel> : IDataService<TEditModel, TCreateModel, TDeleteModel>
        where TCreateModel : DictionaryViewModel
        where TEditModel : DictionaryEditViewModel
        where TDeleteModel : IdModel
    {
    }
}