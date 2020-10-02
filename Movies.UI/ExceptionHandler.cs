using System;
using System.Windows;
using Movies.Abstractions.Errors;

namespace Movies.UI
{
    public class ExceptionHandler
    {
        public void HandleException(object exception)
        {
            if (exception is CommonException commonException)
            {
                HandleCommonException(commonException);
                return;
            }

            ShowUnknownUnhandledExceptionMessage();
        }

        private void HandleCommonException(CommonException exception)
        {
            switch (exception.ErrorCode)
            {
                case ErrorCode.RemoteApiRequestError:
                    MessageBox.Show("Во время получения данных произошла ошибка, попробуйте повторить попытку позже.");
                    break;
                case ErrorCode.RemoteApiNotFoundMovies:
                    MessageBox.Show(exception.Message);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(ErrorCode));
            }
        }

        private void ShowUnknownUnhandledExceptionMessage()
        {
            MessageBox.Show("Произошла ошибка. Обратитесь к администратору системы.");
        }
    }
}