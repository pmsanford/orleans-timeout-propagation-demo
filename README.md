# orleans-timeout-propagation-demo

1. Run `dotnet run silo` in one terminal
2. Run `dotnet run client single` in another
3. The grain request will complete successfully
4. Run `dotnet run client multi`
5. The inner grain invocation will time out after 30 seconds, causing the entire request to fail
6. Stop the silo and restart it with `dotnet run silo patient`
7. Run the above checks again, which will all complete successfully
