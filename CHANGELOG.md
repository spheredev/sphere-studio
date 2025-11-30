Sphere Studio Changelog
=======================

v2.3.11 - November 29, 2025
---------------------------

* Fixes branding - **Where'd She Go?** - no "LLC".

v2.3.10 - December 18, 2024
---------------------------

* Rebranding! Sphere Studio is now produced by Where'd She Go? LLC.

v2.3.9 - May 30, 2024
---------------------

* Fixes a bug in the Oozaru server that could cause game files to be served to
  the browser incorrectly.

v2.3.8 - December 3, 2023
-------------------------

* Adds support for changing the IDE color scheme directly from the Settings
  menu.

v2.3.7 - November 1, 2023
-------------------------

* Adds detection for the 2023 Update for Windows 11.
* Removes neoSphere Retrograde mode from the list of engines.

v2.3.6 - May 30, 2023
---------------------

* Fixes a bug where opening a file from the File List while untitled files are
  also open could cause an unhandled exception, preventing the file from being
  opened.

v2.3.5 - January 27, 2023
-------------------------

* Adds detection for the 2022 Update of Windows 10 and 11.

v2.3.4 - August 11, 2022
------------------------

* Changes the code font size from 10pt to 11pt.
* Changes the background color in dark mode to a darker shade.

v2.3.3 - March 23, 2022
-----------------------

* Fixes a bug where tasks could be added to the Task List without a project
  open, which would then be lost when the IDE was closed.

v2.3.2 - February 4, 2022
-------------------------

* Improves the readability of Task List entries.
* Fixes a bug where items in the Task List pane couldn't be marked completed.
* Fixes a bug where clicking Cancel in the "unsaved changes" prompt didn't
  stop the IDE from closing, causing the changes to be lost.

v2.3.1 - January 23, 2022
-------------------------

* Fixes a bug where pressing F5 to start debugging after a previous debugging
  session sometimes wouldn't work.

v2.3.0 - January 22, 2022
-------------------------

* Adds a new "Find Engine" button to the neoSphere settings page as an escape
  hatch in case an installed copy of the engine can't be found.
* Adds a new "Preferred Engine" submenu to the Project menu.
* Improves the organization of the menu commands.
* Fixes an issue where renaming a file from the file tree would allow existing
  files to be overwritten by the rename.
* Fixes a bug where creating a new Cell-based project with the neoSphere path
  set incorrectly crashes the IDE.
* Fixes a bug where the "Copy Full Path" command doesn't copy the correct path.
* Fixes a bug where closing a project didn't disable the corresponding menu
  commands.


v2.2.2 - December 23, 2021
--------------------------

* Adds new "Rebuild" commands that trigger the compiler to do a full rebuild.
* Moves the plugin management UI into the Preferences dialog.
* Fixes a bug that could cause Sphere Studio to delete a non-empty task list
  file when unloading a project.

v2.2.1 - December 21, 2021
--------------------------

* Improves the Preferences dialog by replacing the tab bar with a tree view
  and splitting the settings pages into separate categories for Engines and
  Compilers.
* Improves the layout of the toolbar to make certain commands easier to find.
* Fixes a bug where performing a "Save All" command while the Start Page is
  open crashes the IDE.

v2.2.0 - December 19, 2021
--------------------------

* Adds an Oozaru support plugin that allows local testing with Oozaru.
* Adds support for launching neoSphere in Retrograde mode.


v2.1.0 - December 14, 2021
--------------------------

* Adds a new **Sphere Classic** project type which allows targeting Sphere v2
  without the use of a Cellscript or a separate `dist` directory, and which is
  also compatible with Sphere v1 projects.
* Adds support for project pages: custom tabs added to the Project Properties
  dialog box by plugins used for changing per-project settings.
* Improves the user interface of the core IDE to streamline dialog boxes and
  make features more easily discoverable.
* Fixes a bug where the **Show in Windows Explorer** command didn't open the
  correct directory.


v2.0.0 - November 29, 2021
--------------------------

This is the first formal Sphere Studio release under Spherical management.
No changelog was maintained prior to this point.
