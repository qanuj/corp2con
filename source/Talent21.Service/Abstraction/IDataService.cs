namespace Talent21.Service.Abstraction
{
    public interface IDataService<TEditModel, TCreateModel, TDeleteModel>
    {
        bool Delete(TDeleteModel model);
        TEditModel Create(TCreateModel model);
        TEditModel Update(TEditModel model);
    }
}