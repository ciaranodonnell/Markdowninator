// The module 'vscode' contains the VS Code extensibility API
// Import the module and reference it with the alias vscode in your code below
import * as vscode from 'vscode';
import { TextDecoder, TextEncoder } from 'util';

// This method is called when your extension is activated
// Your extension is activated the very first time the command is executed
export function activate(context: vscode.ExtensionContext) {

	// Use the console to output diagnostic information (console.log) and errors (console.error)
	// This line of code will only be executed once when your extension is activated
	console.log('Markdowninator is activating');

	// The command has been defined in the package.json file
	// Now provide the implementation of the command with registerCommand
	// The commandId parameter must match the command field in package.json
	let disposable = vscode.commands.registerCommand('markdowninator.helloWorld', () => {
		// The code you place here will be executed every time your command is executed
		// Display a message box to the user
		vscode.window.showInformationMessage('Hello World from Markdowninator!');
	});

	context.subscriptions.push(disposable);
	const bracketDecorationType = vscode.window.createTextEditorDecorationType({
		light: {
			backgroundColor: 'rgba(255, 100, 0, .2)'
		},
		dark: {
			backgroundColor: 'rgba(0, 100, 255, .2)'
		}
	});

	const codeBlockDecorationType = vscode.window.createTextEditorDecorationType({
		light: {
			backgroundColor: 'rgba(100,100,100,0.1)'
		},
		dark: {
			backgroundColor: 'rgba(220,220,220,0.1)'
		}
	});

	let activeEditor = vscode.window.activeTextEditor;
	if (activeEditor) {
		triggerUpdateDecorations();
	}

	vscode.window.onDidChangeActiveTextEditor(editor => {
		activeEditor = editor;
		if (editor) {
			triggerUpdateDecorations();
		}
	}, null, context.subscriptions);

	vscode.workspace.onDidChangeTextDocument(event => {
		if (activeEditor && event.document === activeEditor.document) {
			triggerUpdateDecorations();
		}
	}, null, context.subscriptions);

	var timeout: NodeJS.Timeout | null;
	function triggerUpdateDecorations() {
		if (timeout) {
			clearTimeout(timeout);
		}
		timeout = setTimeout(updateDecorations, 200);
	}

	function updateDecorations() {
		if (!activeEditor || !isMDGFile(activeEditor)) {
			return;
		}
		const regEx = /(<#@|<#\+|<#=|<#)|(#>)+/g;
		const text = activeEditor.document.getText();
		const brackets: vscode.DecorationOptions[] = [];
		let match: RegExpExecArray | null;
		while (match = regEx.exec(text)) {
			const startPos = activeEditor.document.positionAt(match.index);
			const endPos = activeEditor.document.positionAt(match.index + match[0].length);
			const decoration = { range: new vscode.Range(startPos, endPos), hoverMessage: 'T4 Bracket **' + match[0] + '**' };

			brackets.push(decoration);
		}


		const blocks: vscode.DecorationOptions[] = [];

		let index = 0;
		let max = brackets.length;
		brackets.forEach(element => {

			if (index + 1 < max) {
				const start = brackets[index];
				const end = brackets[index + 1];

				const decoration = { range: new vscode.Range(start.range.end, end.range.start), hoverMessage: "" };

				blocks.push(decoration);
			}

			index += 2;
		});

		activeEditor.setDecorations(bracketDecorationType, brackets);
		activeEditor.setDecorations(codeBlockDecorationType, blocks);
	}

	function isMDGFile(editor: vscode.TextEditor): boolean {
		return editor.document.languageId == "mdg";
	}

}

// This method is called when your extension is deactivated
export function deactivate() { }
