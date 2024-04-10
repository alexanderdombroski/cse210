# Welcome to the snippet manager program!

#### This program requires .Net version 8 or later... probably. 

## Main program functionalities:
* Create Snippets in your ideal IDE formmated in vscode snippet format so you don't have to manually insert each line into a JSON file.
* Display Snippets in a colorful way
* Expandable to new languages
* Use the default storage location, or change it to vscode's default snippet storage location.

#### Ready to test!
* There are already a few snippets loaded into the default folder for your testing desires

#### To create a snippet:
1. Select a language in settings > language settings
2. Navigate back to snippet manager
3. Choose the add snippet option
4. Add a title, keyword, and description
5. A file will appear in the CustomCode folder of your designated language. Insert your code.
6. Click back on the program and press ENTER
7. See your code *in color* by choosing the Display Snippet Code option

#### User vs Project Snippets
* User Snippets are typically enabled globally across vscode, they have the .json extension
* Project snippets use the .code-snippets extension. Snippet files with these extension are typically multi-language. This snippet manager can create these files and add the neccessary "scope" variable, but multi-language in one file isn't supported. 
* Name these the same as you would a user snippets file. ie python.json -> python.code-snippets. It may function if you don't, but your code won't be displayed in color.

## Helpful Notes
* VSCode snippet files by default say they are json, but they're not really. To function with this program, comments and ${1:...} placeholders must be removed.
* VSCode won't be able to find your snippets unless you give this program the full file path to their snippet location
    * Click Settings > User Snippets > Any language

## Class Overviews
* ColorMapper - Class designated to format the color data and read it in
* ConsoleUtility - static class with useful Console-related methods
* JsonIO - static class with useful Json read/write methods
* MenuUtility - Static Class with Menu builder functions
    * Also contains an interface that can be targeted by other class to require implimentation of a full menu
* Program - Start up class
* Settings - Parent Class for two groups of settings.
    * Language - Class that houses the menu system to change/add/remove languages
    * DirectoryMap - Class that serves as a way to remember many different snippet locations across the user's file system.
* Snippet - Encapsulates functions and data related to a single snippet
* SnippetManager - Menu system for managing snippets