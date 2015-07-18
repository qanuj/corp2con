namespace Talent21.Service.Abstraction
{
    public interface IDataService<TEditModel, in TCreateModel, in TDeleteModel>
    {
        bool Delete(TDeleteModel model);
        TEditModel Create(TCreateModel model);
        TEditModel Update(TEditModel model);
    }
}