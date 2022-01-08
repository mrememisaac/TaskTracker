using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskTracker.ApplicationCore.Entities;
using Xunit;

namespace TaskTracker.UnitTests.ApplicationCore.Entities.TaskTests;

public class TaskCreate
{
    private readonly string _taskName = "Task 1";
    private readonly string _taskDescription = "Task 1 Description";
    private readonly string _taskCreatorId = "Task 1 Creator";
    private readonly string _taskAssigneeId = "Task 1 Assignee";
    private readonly DateTimeOffset _taskDueDate = DateTimeOffset.UtcNow.AddDays(1);
    private readonly bool _isUrgent = true;

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    public void CantCreateTaskWithBlankOrEmptyName(string name)
    {
        Assert.Throws<ArgumentException>(() => new Task(name, _taskCreatorId));
    }

    [Fact]
    public void CantCreateTaskWithNullName()
    {
        Assert.Throws<ArgumentNullException>(() => new Task(null, _taskCreatorId));
    }


    [Fact]
    public void CreatedTaskContainsSuppliedParameters()
    {
        var task = new Task(_taskName, _taskCreatorId, _taskDescription, _taskDueDate, _taskAssigneeId, _isUrgent);
        Assert.Equal(_taskName, task.Name);
        Assert.Equal(_taskCreatorId, task.CreatorId);
        Assert.Equal(_taskDescription, task.Description);
        Assert.Equal(_taskDueDate, task.DueDate);
        Assert.Equal(_taskAssigneeId, task.AssigneeId);
        Assert.Equal(_isUrgent, task.IsUrgent);
    }

    [Fact]
    public void TaskDueDateCannotBeInThePast()
    {
        var action = () => new Task(_taskName, _taskCreatorId, _taskDescription, _taskDueDate.AddDays(-1));
        Assert.Throws<ArgumentOutOfRangeException>(action);
    }

    [Fact]
    public void CanModifyTaskDescription()
    {
        var task = new Task(_taskName, _taskCreatorId, _taskDescription);
        var newDescription = $"{_taskDescription} Modified";
        task.SetDescription(newDescription);
        Assert.Equal(newDescription, task.Description);
    }

    [Fact]
    public void CanModifyTaskName()
    {
        var task = new Task(_taskName, _taskCreatorId);
        var newName = $"{_taskName} Modified";
        task.SetName(newName);
        Assert.Equal(newName, task.Name);
    }

    [Fact]
    public void CanModifyTaskDueDate()
    {
        var task = new Task(_taskName, _taskCreatorId, _taskDescription, _taskDueDate);
        var newDueDate = _taskDueDate.AddDays(1);
        task.SetDueDate(newDueDate);
        Assert.Equal(newDueDate, task.DueDate);
    }

    [Fact]
    public void CanModifyTaskAssignee()
    {
        var task = new Task(_taskName, _taskCreatorId, _taskDescription, _taskDueDate, _taskAssigneeId);
        var newAssignee = $"{_taskAssigneeId} Modified";
        task.SetAssigneeId(newAssignee);
        Assert.Equal(newAssignee, task.AssigneeId);
    }

    [Fact]
    public void CanMarkTaskComplete()
    {
        var task = new Task(_taskName, _taskCreatorId);
        
    }

    [Fact]
    public void CanReOpenCompletedTask()
    {

    }

    [Fact]
    public void NewTaskIsByDefaultNotCompleted()
    {

    }

    [Fact]
    public void NewTaskHasCreateDateEqualToCurrentTime()
    {

    }
}
