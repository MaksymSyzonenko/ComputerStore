
namespace ComputerStore_MSSQL.Data.Repositories.Factory
{
    public sealed class UnsupportedRepositoryTypeException : Exception
    {
        public UnsupportedRepositoryTypeException(string typeName) : base(typeName)
        {
        }
    }
}
