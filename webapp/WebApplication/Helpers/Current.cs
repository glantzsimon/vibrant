using WebMatrix.WebData;

namespace K9.WebApplication.Helpers
{
    public static class Current
    {
        private static int? _userId;
        public static int UserId
        {
            get { return _userId ?? WebSecurity.CurrentUserId; }
            set { _userId = value; }
        }

        public static void StopImpersonating()
        {
            _userId = null;
        }

        public static bool IsImpersonating()
        {
            return _userId != null;
        }
    }
}