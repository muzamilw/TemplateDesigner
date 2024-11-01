var  commandStack = [];

var UndoManager = function () {
    "use strict";

    // private
   
	    var index = -1,
		undoManagerContext = false,
		callback;

	function execute(command) {
		if (!command) {
			return;
		}
		undoManagerContext = true;
		command.f.apply(command.o, command.p);
		undoManagerContext = false;
	}

	function createCommand(undoObj, undoFunc, undoParamsList, undoMsg, redoObj, redoFunc, redoParamsList, redoMsg) {
		return {
			undo: {o: undoObj, f: undoFunc, p: undoParamsList, m: undoMsg},
			redo: {o: redoObj, f: redoFunc, p: redoParamsList, m: redoMsg}
		};
	}

    // public
    return {

		/*
		Registers an undo and redo command. Both commands are passed as parameters and turned into command objects.
		param undoObj: caller of the undo function
		param undoFunc: function to be called at myUndoManager.undo
		param undoParamsList: (array) parameter list
		param undoMsg: message to be used
		*/
		register: function (undoObj, undoFunc, undoParamsList, undoMsg, redoObj, redoFunc, redoParamsList, redoMsg) {
			if (undoManagerContext) {
				return;
			}

			// if we are here after having called undo,
			// invalidate items higher on the stack
			commandStack.splice(index + 1, commandStack.length - index);

			commandStack.push(createCommand(undoObj, undoFunc, undoParamsList, undoMsg, redoObj, redoFunc, redoParamsList, redoMsg));

			// set the current index to the end
			index = commandStack.length - 1;
			if (callback) {
				callback();
			}
		},

		/*
		Pass a function to be called on undo and redo actions.
		*/
		setCallback: function (callbackFunc) {
			callback = callbackFunc;
		},

		undo: function () {
			var command = commandStack[index];
			if (!command) {
				return;
			}
			execute(command.undo);
			index -= 1;
			if (callback) {
				callback();
			}
		},

		redo: function () {
			var command = commandStack[index + 1];
			if (!command) {
				return;
			}
			execute(command.redo);
			index += 1;
			if (callback) {
				callback();
			}
		},

		/*
		Clears the memory, losing all stored states.
		*/
		clear: function () {
			var prev_size = commandStack.length;

			commandStack = [];
			index = -1;

			if ( callback && ( prev_size > 0 ) ) {
				callback();
			}
		},

		hasUndo: function () {
			return index !== -1;
		},

		hasRedo: function () {
			return index < (commandStack.length - 1);
		}
	};
};

