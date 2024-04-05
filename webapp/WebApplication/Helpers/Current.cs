using WebMatrix.WebData;

namespace K9.WebApplication.Helpers
{
    public static class Current
    {
        private static int? _userId;
        public static int UserId
        {
            get { return _userId ?? WebSecurity.CurrentUserId; }
        }

        private static string _userName;
        public static string UserName
        {
            get { return _userName ?? WebSecurity.CurrentUserName; }
        }

        public static void StartImpersonating(int userId, string username)
        {
            _userId = userId;
            _userName = username;
        }

        public static void StopImpersonating()
        {
            _userId = null;
            _userName = "";
        }

        public static bool IsImpersonating()
        {
            return _userId != null;
        }
    }
}