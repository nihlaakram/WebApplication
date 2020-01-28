using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Connections;

namespace WebApplication
{
    public class MyEchoConnectionHandler : ConnectionHandler
    {
       

        public override async Task OnConnectedAsync(ConnectionContext connection)
        {
            
            while (true)
            {
                var result = await connection.Transport.Input.ReadAsync();
                var buffer = result.Buffer;

                foreach (var segment in buffer)
                {
                    await connection.Transport.Output.WriteAsync(segment);
                }

                if (result.IsCompleted)
                {
                    break;
                }

                connection.Transport.Input.AdvanceTo(buffer.End);
            }

        }
    }
}