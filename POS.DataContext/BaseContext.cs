using DataAccess;

namespace POS.DataContext
{
    public class BaseContext
    {
        private readonly MsSqlConnect _connect;

        public BaseContext(MsSqlConnect connect)
        {
            _connect = connect;
        }
    }
}
