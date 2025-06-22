using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandManager
{
    private bool _isExecuting;
    private Queue<ICommand> _commands = new ();

    public void EnqueueCommand(ICommand command)
    {
        _commands.Enqueue(command);

        if (_isExecuting)
            return;

        ExecuteNextCommand();
    }

    private void ExecuteNextCommand()
    {
        if (_commands.Count == 0)
        {
            _isExecuting = false;
            return;
        }

        _isExecuting = true;
        ICommand command =  _commands.Dequeue();
        command.Execute(() =>
        {
            ExecuteNextCommand();
        });
    }
}
