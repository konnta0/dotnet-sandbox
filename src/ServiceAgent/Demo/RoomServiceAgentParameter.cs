namespace ServiceAgent.Demo;

internal struct RoomServiceAgentParameter
{
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required int Capacity { get; init; }
    
    public required string GameType { get; init; }
}