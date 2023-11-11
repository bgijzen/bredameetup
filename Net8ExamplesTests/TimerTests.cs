using System.Diagnostics;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Time.Testing;
using Net8Examples;

namespace Net8ExamplesTests;

public class TimerTests {
    private readonly ILoggerFactory _loggerFactory;

    public TimerTests(ITestOutputHelper testOutputHelper) {
        _loggerFactory = LoggerFactory
            .Create(builder => {
                builder
                    .AddXunit(testOutputHelper);
            });
    }
    
    [Fact]
    public async Task CallEveryMinute() {
        Stopwatch stopwatch = Stopwatch.StartNew();
        
        var fakeTimerProvider = new FakeTimeProvider();
        TimeProviderExample timer =
            new TimeProviderExample(fakeTimerProvider, _loggerFactory.CreateLogger<TimeProviderExample>());

        using var cancelationTokenSource = new CancellationTokenSource();
        var timerTask = timer.StartAsync(cancelationTokenSource.Token);
        
        fakeTimerProvider.Advance(TimeSpan.FromMinutes(1));
        await Task.Yield();
        timer.NumberOfCalls.Should().Be(1);
        
        fakeTimerProvider.Advance(TimeSpan.FromMinutes(1));
        await Task.Yield();
        timer.NumberOfCalls.Should().Be(2);
        
        stopwatch.Elapsed.Should().BeLessThan(TimeSpan.FromSeconds(5));
    }
    
    
    
    
    [Fact]
    public async Task PeriodicTimerCallEveryMinute() {
        Stopwatch stopwatch = Stopwatch.StartNew();
        
        var fakeTimerProvider = new FakeTimeProvider();
        var timer =
            new TimeProviderExampleWithPeriodicTimer(fakeTimerProvider, _loggerFactory.CreateLogger<TimeProviderExampleWithPeriodicTimer>());

        using var cancelationTokenSource = new CancellationTokenSource();
        var timerTask = timer.StartAsync(cancelationTokenSource.Token);
        
        fakeTimerProvider.Advance(TimeSpan.FromSeconds(60));
        
        await Task.Delay(TimeSpan.FromSeconds(0.1));
        timer.NumberOfCalls.Should().Be(1);
        
        fakeTimerProvider.Advance(TimeSpan.FromSeconds(60));
        
        await Task.Delay(TimeSpan.FromSeconds(0.1));
        timer.NumberOfCalls.Should().Be(2);
        
        stopwatch.Elapsed.Should().BeLessThan(TimeSpan.FromSeconds(5));
    }
}
