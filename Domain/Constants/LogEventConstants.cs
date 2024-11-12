using Microsoft.Extensions.Logging;

namespace Domain.Constants;

public static class LogEventConstants
{
    public static EventId WaasHealthCheck = new(1000, nameof(WaasHealthCheck));

    public static EventId BackgroundProcessing = new(1001, nameof(BackgroundProcessing));

    public static EventId CheckExecutingLogTaskStep = new(1002, nameof(CheckExecutingLogTaskStep));

    public static EventId TaskExpirationCheck = new(1003, nameof(TaskExpirationCheck));

    public static EventId BlobNotFound = new(1004, nameof(BlobNotFound));

    public static EventId RetirementImport = new(1005, nameof(RetirementImport));

    public static EventId UserActions = new(1006, nameof(UserActions));

    public static EventId MoveRequest = new(1007, nameof(MoveRequest));

    public static EventId AddJobAttachment = new(1008, nameof(AddJobAttachment));

    public static EventId ClearAttachments = new(1009, nameof(ClearAttachments));

    public static EventId ProcessFailedTaskStep = new(1010, nameof(ProcessFailedTaskStep));

    public static EventId BuildFlexibleViewAssignments = new(1011, nameof(BuildFlexibleViewAssignments));

    public static EventId ExportDataConstructFile = new(1012, nameof(ExportDataConstructFile));
}

