namespace salary.host.command
{
    public sealed class GetManyEmployeeCommand
    {
        public long Limit { get; set; }
        public long Offset { get; set; }
    }
}