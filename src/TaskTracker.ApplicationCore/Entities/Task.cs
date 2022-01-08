using Ardalis.GuardClauses;

namespace TaskTracker.ApplicationCore.Entities;

public class Task : BaseEntity
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Task() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public Task(string name, string creatorId, string? description =null, DateTimeOffset? dueDate=null, string? assigneeId = null, bool? isUrgent = null)
    {
        Guard.Against.NullOrEmpty(name, nameof(name));
        Guard.Against.NullOrWhiteSpace(name, nameof(name));
        Guard.Against.NullOrEmpty(creatorId, nameof(creatorId));
        Guard.Against.NullOrWhiteSpace(creatorId, nameof(creatorId));
        Name = name;
        CreatorId = creatorId;
        IsUrgent = isUrgent;
        Description = description != null ? Guard.Against.NullOrWhiteSpace(Guard.Against.NullOrEmpty(description, nameof(description)), nameof(description)) : null;
        DueDate = dueDate != null && dueDate <= DateTimeOffset.Now ? throw new ArgumentOutOfRangeException($"{nameof(dueDate)} cannot be in the past") : dueDate;
        AssigneeId = assigneeId != null ? Guard.Against.NullOrWhiteSpace(Guard.Against.NullOrEmpty(assigneeId, nameof(assigneeId)), nameof(assigneeId)) : null;
        IsComplete = false;
        CreateDate = DateTimeOffset.Now;
    }

    public string Name { get; private set; }
    public string? Description { get; private set; }
    public string CreatorId { get; private set; }
    public bool? IsUrgent { get; private set; }
    public bool IsComplete { get; private set; }
    public DateTimeOffset CreateDate { get; private set; }
    public DateTimeOffset? DueDate { get; private set; }
    public string? AssigneeId { get; private set; }

    public void SetDescription(string description)
    {
        Description = Guard.Against.NullOrEmpty(description, nameof(description));
    }

    public void SetName(string name)
    {
        Name = Guard.Against.NullOrEmpty(name, nameof(name));
    }

    public void SetDueDate(DateTimeOffset dueDate)
    {
        DueDate = dueDate <= DateTimeOffset.Now ? throw new ArgumentOutOfRangeException($"{nameof(dueDate)} cannot be in the past") : dueDate;
    }

    public void SetAssigneeId(string assigneeId)
    {
        AssigneeId = Guard.Against.NullOrEmpty(assigneeId, nameof(assigneeId));
    }

    public void SetUrgency(bool isUrgent)
    {
        IsUrgent = isUrgent;
    }

    public void Complete() => IsComplete = true;

    public void ReOpen() => IsComplete = false;
}
