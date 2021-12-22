; Sphere Studio Setup script for Inno Setup
; (c) 2012-2022 Sphere Engine Group

#define AppName "Sphere Studio"
#define AppPublisher "Spherical"
#define AppVersion3 "2.2.1+"
#define AppVersion4 "0.0.0.0"

[Setup]
OutputBaseFilename=SphereStudioSetup-{#AppVersion3}-msw
OutputDir=.
AppId={{F40892B0-C96E-48B7-B1E9-8C2BFB6C167D}
AppCopyright=(c) 2012-2022 Sphere Engine Group
AppName={#AppName}
AppPublisher={#AppPublisher}
AppPublisherURL=http://www.spheredev.org/
AppSupportURL=http://forums.spheredev.org/
AppUpdatesURL=http://forums.spheredev.org/index.php/topic,24.0.html
AppVerName={#AppName} {#AppVersion3}
AppVersion={#AppVersion4}
ArchitecturesInstallIn64BitMode=x64 ia64
ChangesAssociations=yes
Compression=lzma2
DefaultDirName={autopf}\{#AppName}
DisableProgramGroupPage=yes
LicenseFile=LICENSE.txt
SetupIconFile=SphereStudioApp\Sphere Studio.ico
SolidCompression=yes
UninstallDisplayName={#AppName}
UninstallDisplayIcon={app}\SphereStudioApp.exe,0
VersionInfoDescription={#AppName} {#AppVersion3} Setup for Windows
VersionInfoVersion={#AppVersion4}

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Files]
Source: "SphereStudioApp\bin\Release\SphereStudioApp.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "SphereStudioApp\bin\Release\SphereStudioBase.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "SphereStudioApp\bin\Release\*.config"; DestDir: "{app}"; Flags: ignoreversion
Source: "SphereStudioApp\bin\Release\*.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "SphereStudioApp\bin\Release\*.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "SphereStudioApp\bin\Release\Dictionary\*"; DestDir: "{app}\Dictionary"; Flags: ignoreversion
Source: "SphereStudioApp\bin\Release\Plugins\*.dll"; DestDir: "{app}\Plugins"; Flags: ignoreversion

[Registry]
Root: HKCR; Subkey: ".ssproj"; ValueType: string; ValueName: ""; ValueData: "SphereStudio.SSPROJ"; Flags: uninsdeletevalue
Root: HKCR; Subkey: "SphereStudio.SSPROJ"; ValueType: string; ValueName: ""; ValueData: "Sphere Studio Project"; Flags: uninsdeletekey
Root: HKCR; Subkey: "SphereStudio.SSPROJ\DefaultIcon"; ValueType: string; ValueName: ""; ValueData: "{app}\SphereStudioApp.exe,0"
Root: HKCR; Subkey: "SphereStudio.SSPROJ\shell\open\command"; ValueType: string; ValueName: ""; ValueData: """{app}\SphereStudioApp.exe"" ""%1"""

[Icons]
Name: "{commonprograms}\{#AppName}"; Filename: "{app}\SphereStudioApp.exe"

[Run]
Filename: "{app}\SphereStudioApp.exe"; Description: "{cm:LaunchProgram,{#StringChange(AppName, '&', '&&')}}"; Flags: nowait postinstall skipifsilent

[Code]
procedure InitializeWizard;
begin
  WizardForm.LicenseAcceptedRadio.Checked := True;
end;
