using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace combineExport.appAuth
{

    class AppStateCode
    {
        public const String INITIAL = "INITIAL";
        public const String AUTHENTICATING = "AUTHENTICATING";
        public const String AUTHORIZED = "AUTHORIZED";
        public const String UNAUTHORIZED = "UNAUTHORIZED";
        public const String NEED_TO_ACTIVATE_NEW = "NEED_TO_ACTIVATE_NEW";
        public const String DISCONNECTED = "DISCONNECTED";
        public const String ERROR = "ERROR";
    }

    public interface AppAuthState
    {
        void authenticate(AppAuthContext context);
        void authorize(AppAuthContext context);
        void authenticateActivatingNew(AppAuthContext context);
        void unauthorize(AppAuthContext context, string message);
        void disconnect(AppAuthContext context);
        void error(AppAuthContext context);
        String getStateCode();
    }

    public class IntialState : AppAuthState
    {
        void AppAuthState.authenticate(AppAuthContext context)
        {
            login loginForm = context.getLoginForm();
            loginForm.setStateMessage("로그인 시도하고 있습니다.");
        }

        void AppAuthState.authenticateActivatingNew(AppAuthContext context)
        {
            throw new NotSupportedException();
        }

        void AppAuthState.authorize(AppAuthContext context)
        {
            throw new NotSupportedException();
        }

        void AppAuthState.disconnect(AppAuthContext context)
        {
            throw new NotSupportedException();
        }

        void AppAuthState.error(AppAuthContext context)
        {
            throw new NotSupportedException();
        }

        string AppAuthState.getStateCode()
        {
            return AppStateCode.INITIAL;
        }

        void AppAuthState.unauthorize(AppAuthContext context, string message)
        {
            throw new NotSupportedException();
        }
    }

    public class AuthenticatingState : AppAuthState
    {
        void AppAuthState.authenticate(AppAuthContext context)
        {
            throw new NotSupportedException();
        }

        void AppAuthState.authenticateActivatingNew(AppAuthContext context)
        {
            throw new NotSupportedException();
        }

        void AppAuthState.authorize(AppAuthContext context)
        {
            login loginForm = context.getLoginForm();
            loginForm.setStateMessage("로그인 되었습니다.");
            loginForm.onAuthorized();
        }

        void AppAuthState.disconnect(AppAuthContext context)
        {
            throw new NotSupportedException();
        }

        void AppAuthState.error(AppAuthContext context)
        {
            throw new NotSupportedException();
        }

        string AppAuthState.getStateCode()
        {
            return AppStateCode.AUTHENTICATING;
        }

        void AppAuthState.unauthorize(AppAuthContext context, string message)
        {
            throw new NotSupportedException();
        }
    }

    public class AuthorizedState : AppAuthState
    {
        void AppAuthState.authenticate(AppAuthContext context)
        {
            throw new NotSupportedException();
        }

        void AppAuthState.authenticateActivatingNew(AppAuthContext context)
        {
            throw new NotSupportedException();
        }

        void AppAuthState.authorize(AppAuthContext context)
        {
            throw new NotSupportedException();
        }

        void AppAuthState.disconnect(AppAuthContext context)
        {
            throw new NotSupportedException();
        }

        void AppAuthState.error(AppAuthContext context)
        {
            throw new NotSupportedException();
        }

        string AppAuthState.getStateCode()
        {
            return AppStateCode.AUTHORIZED;
        }

        void AppAuthState.unauthorize(AppAuthContext context, string message)
        {
            throw new NotSupportedException();
        }
    }
}
