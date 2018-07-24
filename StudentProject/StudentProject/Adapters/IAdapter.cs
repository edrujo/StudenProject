namespace Truextend.Test.StudentProject.Adapters
{
    public interface IAdapter<T>
    {
        T Create();

        void Store(T entityToStore);

        void Delete(T entityToDelete);        
    }
}