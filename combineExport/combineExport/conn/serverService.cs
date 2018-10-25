using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.NetworkInformation;
using Newtonsoft.Json; // install-package Newtonsoft.Json
using RabbitMQ.Client;
using EasyNetQ;
using combineExport.conn;

namespace combineExport.conn
{
    class serverService
    {
        public static string connect_server(string userid, string userpwd )
        {
            string result = "";

            //Mac Address를 얻는 복붙코드
            String firstMacAddress = NetworkInterface
                .GetAllNetworkInterfaces()
                .Where(nic => nic.OperationalStatus == OperationalStatus.Up && nic.NetworkInterfaceType != NetworkInterfaceType.Loopback)
                .Select(nic => nic.GetPhysicalAddress().ToString())
                .FirstOrDefault();

            // Mac Address를 clientId에 할당한다. clinetId는 중복로그인 구분을 위한 key 값으로 쓰인다.
            var clientId = firstMacAddress;

            //create Bus로 rabbitMQ 브로커에 접속한다.
            var advBus = RabbitHutch.CreateBus("host=207.148.88.116:5672; virtualHost=created-docs-vhost; username=created-docs-dev; password=rlaehdgus",
                x => x.Register(c => new AdvancedBusEventHandlers((s, e) => {//onConnected, 연결성공
                }, (s, e) => {//onDisconnected, 연결이 끊김, 기본적으로 라이브러리에셔 여러번 retry함.
                    var advancedBus = (IAdvancedBus)s;
                    Console.WriteLine(advancedBus.IsConnected); // This will print false.

                    //기본적으로 app 클라이언트가 생성한 큐가 모두 제거되기 때문에 버스를 버리고 다시 만드는게 좋다.
                    advancedBus.Dispose();
                }))).Advanced;

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

            // Authentication이란 클래스를 json으로 만들어 메세지의 body로 쓸것이다.
            Authentication appAuthMessage = new Authentication { username = userid, password = userpwd, type = Authentication.NORMAL, clientId = clientId };
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

            // 메세지 발송
            advBus.Publish(
                exchange,
                "app.auth",
                false,
                message: msg
            );

            // 발송한 메세지 출력
            Console.WriteLine(" [x] Sent {0}", jsonMsg);

            // 해당 큐 (여기서는 아까 만든 답장을 위한 임시큐) 로 메세지가 도착하면 처리하는 코드
            advBus.Consume(queue, (bbody, properties, info) => Task.Factory.StartNew(() =>
            {
                var message = Encoding.UTF8.GetString(bbody);
                //  Console.WriteLine("Got message: '{0}'", message);
                result = message.Split(',')[0];
                Console.WriteLine(result);
            }));

            return result;// 비동기 리턴해야되는곳??
        }
    }
}
