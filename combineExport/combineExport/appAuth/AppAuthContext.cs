using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json; // install-package Newtonsoft.Json
using EasyNetQ; // Install-Package EasyNetQ
using System.Net.NetworkInformation;
using RabbitMQ.Client;


//Update-Package -reinstall

namespace combineExport.appAuth
{
    public class AppAuthContext
    {
        AppAuthState appAuthState;
        Me me;

        public AppAuthState getAppAuthState()
        {
            return this.appAuthState;
        }
        public void setAppAuthState(AppAuthState appAuthState)
        {
            this.appAuthState = appAuthState;
        }

        login loginForm;
        public login getLoginForm()
        {
            return this.loginForm;
        }
        IAdvancedBus advBus;

        public AppAuthState AppAuthState()
        {
            return this.appAuthState;
        }

        public AppAuthContext(login loginForm)
        {
            this.loginForm = loginForm;
            this.me = new Me();
            this.appAuthState = new IntialState();
        }


        public void authenticate(String username, String password)
        {
            lock (appAuthState)
            {
                this.appAuthState.authenticate(this);
                this.appAuthState = new AuthenticatingState();
            }

            advBus = RabbitHutch.CreateBus("host=207.148.88.116:5672; virtualHost=created-docs-vhost; username=created-docs-dev; password=rlaehdgus",
                x => x.Register(c => new AdvancedBusEventHandlers((s, e) => {/*onConnected, 연결성공*/},
                (s, e) =>
                {/*onDisconnected, 연결이 끊김, 기본적으로 라이브러리에셔 여러번 retry함.*/
                    var advancedBus = (IAdvancedBus)s;
                    advancedBus.Dispose();
                    this.disconnect();
                }))).Advanced;


            String clientId = getMacAddress();
            // exchange는 메세지를 분류해 주는 곳으로, 이미 있는 exchange를 새로 declare해도 문제 없다.
            var exchange = advBus.ExchangeDeclare("created-docs.direct", ExchangeType.Direct);

            //ExchangeDeclare는 client가 사용할 임시 큐(접속이 끊기면 없어짐)를 생성한다. 여기서 선언한 큐는 auth 메세지에 대한 답장을 받기위해 쓰인다.
            //서버가 이 큐로 답장 메세지를 보낸다.
            var queue = advBus.QueueDeclare(
                "client." + clientId + ".auth",
                durable: false,
                exclusive: false,
                autoDelete: true
            );

            // 이 큐는 다른 곳에서 로그인 했을 때, 이 클라이언트로 접속금지 메세지를 보내기 위해 선언한다. 이것도 임시 큐
            var unauthQueue = advBus.QueueDeclare(
                "client." + clientId + ".unauth",
                durable: false,
                exclusive: false,
                autoDelete: true
            );
            // unauthQueue와 created-docs.direct exchange를 bind 해준다.
            //서버에서 default exchange 발송해야한다. 이렇게 bind하면 auto delete가 안된다.
            //advBus.Bind(exchange, unauthQueue, "client." + clientId + ".unauth");

            lock (me)
            {
                me.username = username;
                me.setEncryptedPasswordByPalinPassword(password);
                Authentication appAuthMessage = new Authentication { username = me.username, password = me.encryptedPassword, type = Authentication.NORMAL, clientId = clientId };

                string jsonMsg = JsonConvert.SerializeObject(appAuthMessage);
                var body = Encoding.UTF8.GetBytes(jsonMsg);

                //메세지의 속성을 입력하는 코드
                var messageProperties = new MessageProperties();
                messageProperties.ContentEncoding = "UTF-8"; //반드시 필요하다.
                messageProperties.ContentType = "application/json"; //반드시 필요하다.
                messageProperties.MessageId = System.Guid.NewGuid().ToString(); // Optional 메세지 관리를 위한 것
                messageProperties.CorrelationId = System.Guid.NewGuid().ToString(); // 답장을 위해 반드시 필요하다.
                messageProperties.ReplyTo = queue.Name; // 이 큐로 답장을 보내달라는 것으로 반드시 필요하다.

                var msg = new Message<Authentication>(appAuthMessage);
                msg.SetProperties(messageProperties);

                advBus.Publish(
                    exchange,
                    "app.auth",
                    false,
                    message: msg
                );
            }

            // 해당 큐 (여기서는 아까 만든 답장을 위한 임시큐) 로 메세지가 도착하면 처리하는 코드
            advBus.Consume(queue, (bbody, properties, info) => Task.Factory.StartNew(() =>
            {
                string message = Encoding.UTF8.GetString(bbody);
                AuthenticationResult authResult = JsonConvert.DeserializeObject<AuthenticationResult>(message);
                string authResultCode = authResult.resultCode;

                if (authResultCode.Equals(AuthenticationResult.AUTHORIZED))
                {
                    authoirze();
                }
                else if (authResultCode.Equals(AuthenticationResult.NEED_TO_ACTIVATE_NEW))
                {
                    authenticateActivatingNew();
                }
                else if (authResultCode.Equals(AuthenticationResult.UNAUHORIZED))
                {
                    unauthorize();
                }
                else if (authResultCode.Equals(AuthenticationResult.ERROR))
                {
                    error();
                }
                else
                {
                    error();
                }
            }));

            advBus.Consume(unauthQueue, (bbody, properties, info) => Task.Factory.StartNew(() =>
            {
                //server makes this client stop
                string message = Encoding.UTF8.GetString(bbody);
                //do something with message
                unauthorize();
            }));

        }

        public String getMacAddress()
        {
            String firstMacAddress = NetworkInterface
                .GetAllNetworkInterfaces()
                .Where(nic => nic.OperationalStatus == OperationalStatus.Up && nic.NetworkInterfaceType != NetworkInterfaceType.Loopback)
                .Select(nic => nic.GetPhysicalAddress().ToString())
                .FirstOrDefault();
            return firstMacAddress;
        }
        public void authoirze()
        {
            lock(appAuthState)
            {
                appAuthState.authorize(this);
                setAppAuthState(new AuthorizedState());
            }
        }
        public void authenticateActivatingNew()
        {

        }
        public void unauthorize()
        {

        }
        public void disconnect()
        {
            lock(appAuthState)
            {
                appAuthState.disconnect(this);
                setAppAuthState(new IntialState());
            }
        }
        public void error()
        {

        }
        public String getMessage()
        {
            return this.appAuthState.getStateCode();
        }
    }
}
