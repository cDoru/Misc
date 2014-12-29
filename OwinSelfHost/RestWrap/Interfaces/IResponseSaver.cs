namespace RestWrap.Interfaces
{
    public interface IResponseSaver
    {
        void Save(string path, IResponse response);
    }
}