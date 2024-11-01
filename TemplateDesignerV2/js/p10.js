var  commandStack = [];

var UndoManager = function () {
    "use strict";
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
    return {
		register: function (undoObj, undoFunc, undoParamsList, undoMsg, redoObj, redoFunc, redoParamsList, redoMsg) {
			if (undoManagerContext) {
				return;
			}
			commandStack.splice(index + 1, commandStack.length - index);
			commandStack.push(createCommand(undoObj, undoFunc, undoParamsList, undoMsg, redoObj, redoFunc, redoParamsList, redoMsg));
			index = commandStack.length - 1;
			if (callback) {
				callback();
			}
		},
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

