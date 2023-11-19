namespace GrpcNamedPipeTester;

public class NamedPipesConnectionFactory
{

    public string? PipeName { get; set; }
    
    public async ValueTask<Stream> ConnectAsync(SocketsHttpConnectionContext _,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(PipeName)) {
            throw new InvalidOperationException("PipeName not initialized");
        }
        
        var clientStream = new NamedPipeClientStream(
            serverName: ".",
            pipeName: PipeName,
            direction: PipeDirection.InOut,
            options: PipeOptions.WriteThrough | PipeOptions.Asynchronous,
            impersonationLevel: TokenImpersonationLevel.Anonymous);

        try
        {
            await clientStream.ConnectAsync(cancellationToken).ConfigureAwait(false);
            return clientStream;
        }
        catch {
            await clientStream.DisposeAsync();
            throw;
        }
    }
}
