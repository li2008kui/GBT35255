using GBT35255;
using GBT35255.ProtoBase;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SuperSocket;
using System.Threading.Tasks;

namespace SuperSocketSample
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var host = CreateSocketServerBuilder(args).Build();
            await host.RunAsync();
        }

        static IHostBuilder CreateSocketServerBuilder(string[] args)
        {
            return SuperSocketHostBuilder.Create<DataPackage, DataPipelineFilter>()
                .UseSessionHandler(s => SendCommand(s))
                .UsePackageHandler(async (s, p) =>
                {
                    await s.SendAsync(p.Datagram.GetDatagram());
                })
                .ConfigureSuperSocket(options =>
                {
                    options.Name = "GBT35255 Server";
                    options.AddListener(new ListenOptions
                    {
                        Ip = "Any",
                        Port = 4040
                    });
                })
                .ConfigureLogging((hostCtx, loggingBuilder) =>
                {
                    loggingBuilder.AddConsole();
                });
        }

        static ValueTask SendCommand(IAppSession s)
        {
            // 创建命令
            var creater = new CreateCommand(MessageType.Command, 0x00000001);
            var cmd = creater.GetRequestCommand(MessageId.RealTimeControlLuminaire, ParameterType.Luminance, "100");

            return s.SendAsync(cmd);
        }
    }
}