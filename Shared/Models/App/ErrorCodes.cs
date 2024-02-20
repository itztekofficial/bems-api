using System;

namespace Shared.Models.App
{
    public static class ErrorCodes
    {
        public static int ErrorSaving = 500;
    }

    public static class HandleException
    {
        public static Exception Handle(Exception ex)
        {
            //if (ex.GetType() == "21212")
            //{
            //}
            return new Exception(ErrorCodes.ErrorSaving.ToString());
        }
    }
}