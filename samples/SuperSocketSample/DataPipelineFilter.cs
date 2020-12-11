using GBT35255;
using SuperSocket.ProtoBase;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;

namespace SuperSocketSample
{
    public class DataPipelineFilter : TerminatorPipelineFilter<DataPackage>
    {
        const byte stx = 0x02;
        const byte etx = 0x03;

        public DataPipelineFilter() : base(new byte[] { etx })
        {
        }

        protected override DataPackage DecodePackage(ref ReadOnlySequence<byte> buffer)
        {
            if (CheckHasStx(buffer, out byte[] bArray) && bArray != null)
            {
                // 解析命令
                var parser = new ParseCommand(bArray);
                var datagrams = parser.DatagramList;

                if (datagrams.Count > 0)
                {
                    return new DataPackage { Datagram = datagrams[0] };
                }
            }

            return null;
        }

        private static bool CheckHasStx(ReadOnlySequence<byte> buffer, out byte[] bArray)
        {
            var hasStx = false;
            var data = new List<byte>();

            foreach (var b in buffer.ToArray())
            {
                if (!hasStx)
                {
                    hasStx = b == stx;

                    if (!hasStx)
                    {
                        continue;
                    }
                }

                if (b == stx)
                {
                    data.Clear();
                }

                data.Add(b);
            }

            if (data.Count <= 1)
            {
                bArray = null;
                return false;
            }

            data.Add(etx);
            bArray = data.ToArray();
            return true;
        }
    }
}